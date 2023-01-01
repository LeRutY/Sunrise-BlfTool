using Newtonsoft.Json;
using Sewer56.BitStream;
using Sewer56.BitStream.ByteStreams;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarthogInc.BlfChunks;

namespace Sunrise.BlfTool
{
    class MatchmakingHopperDescriptions2 : MatchmakingHopperDescriptions3
    {
        public ushort GetVersion()
        {
            return 2;
        }
    }
}
