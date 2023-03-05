using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Sewer56.BitStream;
using Sewer56.BitStream.ByteStreams;
using Sunrise.BlfTool.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sunrise.BlfTool.BlfChunks.GameEngineVariants
{
    public class PackedSlayerGameVariant2 : PackedBaseGameVariant2
    {
        public PackedSlayerGameVariant2() { }

        public PackedSlayerGameVariant2(ref BitStream<StreamByteStream> hoppersStream)
        {
            Read(ref hoppersStream);
        }

        public byte teamScoring; // 2
        public short scoreToWin; // 10
        [JsonConverter(typeof(StringEnumConverter))]
        public Points killPoints; // 5
        [JsonConverter(typeof(StringEnumConverter))]
        public Points assistPoints; // 5
        [JsonConverter(typeof(StringEnumConverter))]
        public Points deathPoints; // 5 
        [JsonConverter(typeof(StringEnumConverter))]
        public Points suicidePoints; // 5 
        [JsonConverter(typeof(StringEnumConverter))]
        public Points betrayalPoints; // 5
        [JsonConverter(typeof(StringEnumConverter))]
        public Points leaderKilledPoints; // 5
        [JsonConverter(typeof(StringEnumConverter))]
        public Points eliminationPoints; // 5
        [JsonConverter(typeof(StringEnumConverter))]
        public Points assassinationPoints; // 5 
        [JsonConverter(typeof(StringEnumConverter))]
        public Points headshotPoints; // 5
        [JsonConverter(typeof(StringEnumConverter))]
        public Points meleePoints; // 5
        [JsonConverter(typeof(StringEnumConverter))]
        public Points stickyPoints; // 5
        [JsonConverter(typeof(StringEnumConverter))]
        public Points splatterPoints; // 5 
        [JsonConverter(typeof(StringEnumConverter))]
        public Points killingSpreePoints; // 5
        public PlayerTraits unknownTraits1;
        public PlayerTraits unknownTraits2;

        public void Read(ref BitStream<StreamByteStream> hoppersStream)
        {
            throw new NotImplementedException();
        }

        public void Write(ref BitStream<StreamByteStream> hoppersStream)
        {
            base.Write(ref hoppersStream);
            hoppersStream.WriteBitswapped(teamScoring, 2);
            hoppersStream.WriteBitswapped(scoreToWin, 10);
            hoppersStream.WriteBitswapped((byte)killPoints, 5);
            hoppersStream.WriteBitswapped((byte)assistPoints, 5);
            hoppersStream.WriteBitswapped((byte)deathPoints, 5);
            hoppersStream.WriteBitswapped((byte)suicidePoints, 5);
            hoppersStream.WriteBitswapped((byte)betrayalPoints, 5);
            hoppersStream.WriteBitswapped((byte)leaderKilledPoints, 5);
            hoppersStream.WriteBitswapped((byte)eliminationPoints, 5);
            hoppersStream.WriteBitswapped((byte)assassinationPoints, 5);
            hoppersStream.WriteBitswapped((byte)headshotPoints, 5);
            hoppersStream.WriteBitswapped((byte)meleePoints, 5);
            hoppersStream.WriteBitswapped((byte)stickyPoints, 5);
            hoppersStream.WriteBitswapped((byte)splatterPoints, 5);
            hoppersStream.WriteBitswapped((byte)killingSpreePoints, 5);
            unknownTraits1.Write(ref hoppersStream);
            unknownTraits2.Write(ref hoppersStream);
        }
    }
}
