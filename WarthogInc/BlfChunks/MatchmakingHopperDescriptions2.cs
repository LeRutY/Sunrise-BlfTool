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
    class MatchmakingHopperDescriptions2 : IBLFChunk
    {
        [JsonIgnore]
        public byte descriptionCount { get { return (byte)descriptions.Length; } }
        public HopperDescription[] descriptions;

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
            return "mhdf";
        }

        public ushort GetVersion()
        {
            return 2;
        }

        public void ReadChunk(ref BitStream<StreamByteStream> hoppersStream)
        {
            throw new NotImplementedException();
        }

        public void WriteChunk(ref BitStream<StreamByteStream> stream)
        {
            var ms = new MemoryStream();
            var hoppersStream = new BitStream<StreamByteStream>(new StreamByteStream(ms));

            hoppersStream.WriteBitswapped<byte>(descriptionCount, 6);

            for (int i = 0; i < descriptionCount; i++)
            {
                HopperDescription description = descriptions[i];
                hoppersStream.WriteBitswapped<ushort>(description.identifier, 16);
                hoppersStream.WriteBitswapped<byte>(description.type ? (byte)1 : (byte)0, 1);
                hoppersStream.WriteBitswappedString(description.description, 256, Encoding.UTF8);
            }

            if (hoppersStream.BitIndex % 8 != 0)
                hoppersStream.WriteBitswapped((byte)0, 8 - (hoppersStream.BitIndex % 8));

            ms.Seek(0, SeekOrigin.Begin);
            while (ms.Position < ms.Length)
            {
                stream.WriteBitswapped((byte)ms.ReadByte(), 8);
            }
        }

        public class HopperDescription
        {
            public ushort identifier;
            public bool type;
            public string description;
        }
    }
}
