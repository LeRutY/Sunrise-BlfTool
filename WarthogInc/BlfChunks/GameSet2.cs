using Newtonsoft.Json;
using Sewer56.BitStream;
using Sewer56.BitStream.ByteStreams;
using Sunrise.BlfTool.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarthogInc.BlfChunks;

namespace Sunrise.BlfTool
{
    public class GameSet2 : IBLFChunk
    {
        [JsonIgnore]
        public byte gameEntryCount { get { return (byte)gameEntries.Length; } }
        public GameEntry[] gameEntries;

        public ushort GetAuthentication()
        {
            return 1;
        }

        public uint GetLength()
        {
            var ms = new BitStream<StreamByteStream>(new StreamByteStream(new MemoryStream()));
            WriteChunk(ref ms);
            return (uint)ms.NextByteIndex;
        }

        public string GetName()
        {
            return "gset";
        }

        public ushort GetVersion()
        {
            return 1;
        }

        public void ReadChunk(ref BitStream<StreamByteStream> hoppersStream)
        {
            throw new NotImplementedException();
        }

        public void WriteChunk(ref BitStream<StreamByteStream> stream)
        {
            var ms = new MemoryStream();
            var hoppersStream = new BitStream<StreamByteStream>(new StreamByteStream(ms));

            var count = gameEntryCount;

            if (gameEntries.Length > 63)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Too many game entries! I can only write the first 63 :(");
                Console.ResetColor();
                count = 63;
            }

            hoppersStream.WriteBitswapped(count, 6);

            for (int i = 0; i < count; i++)
            {
                GameEntry entry = gameEntries[i];

                hoppersStream.WriteBitswapped(entry.gameEntryWeight, 32);
                hoppersStream.WriteBitswapped(entry.minimumPlayerCount, 4);
                hoppersStream.WriteBitswapped(entry.skipAfterVeto ? (byte) 1 : (byte) 0, 1);
                hoppersStream.WriteBitswapped(entry.mapID, 32);
                hoppersStream.WriteBitswappedString(entry.gameVariantFileName, 32, Encoding.UTF8);
                hoppersStream.WriteBitswappedString(entry.mapVariantFileName, 32, Encoding.UTF8);
            }

            if (hoppersStream.BitIndex % 8 != 0)
                hoppersStream.WriteBitswapped((byte)0, 8 - (hoppersStream.BitIndex % 8));

            ms.Seek(0, SeekOrigin.Begin);
            while (ms.Position < ms.Length)
            {
                stream.WriteBitswapped((byte)ms.ReadByte(), 8);
            }
        }

        public class GameEntry
        {
            public int gameEntryWeight;
            public byte minimumPlayerCount;
            public bool skipAfterVeto;
            [JsonIgnore]
            public int mapID;
            public string gameVariantFileName;
            public string mapVariantFileName;
        }
    }
}
