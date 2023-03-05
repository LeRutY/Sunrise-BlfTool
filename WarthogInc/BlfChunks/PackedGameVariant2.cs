using Newtonsoft.Json;
using Sewer56.BitStream;
using Sewer56.BitStream.ByteStreams;
using Sunrise.BlfTool.BlfChunks.GameEngineVariants;
using Sunrise.BlfTool.Extensions;
using System;
using System.IO;
using WarthogInc.BlfChunks;

namespace Sunrise.BlfTool
{
    public class PackedGameVariant2 : IBLFChunk
    {
        public enum VariantGameEngine : byte {
            CTF = 1,
            SLAYER = 2,
            ODDBALL = 3,
            KOTH = 4,
            FORGE = 5,
            VIP = 6,
            JUGGERNAUT = 7,
            TERRITORIES = 8,
            ASSAULT = 9,
            INFECTION = 10,
        }

        [JsonIgnore]
        public VariantGameEngine variantGameEngineIndex { 
            get {
                if (slayer != null)
                    return VariantGameEngine.SLAYER;
                else
                    throw new Exception("No variant found.");
            } 
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public PackedSlayerGameVariant2 slayer;
        public byte descriptionIndex;

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
            return "gvar";
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

            hoppersStream.WriteBitswapped((byte)variantGameEngineIndex, 4);
            switch (variantGameEngineIndex)
            {
                case VariantGameEngine.SLAYER:
                    slayer.Write(ref hoppersStream);
                    break;
                default:
                    throw new Exception("Unsupported game engine " + variantGameEngineIndex.ToString());
            }

            hoppersStream.WriteBitswapped(descriptionIndex, 8);

            ms.Seek(0, SeekOrigin.Begin);
            while (ms.Position < ms.Length)
            {
                stream.WriteBitswapped((byte)ms.ReadByte(), 8);
            }
        }
    }
}
