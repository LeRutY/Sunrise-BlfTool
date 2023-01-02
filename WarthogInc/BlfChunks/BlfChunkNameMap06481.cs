using Sunrise.BlfTool;
using System;
using System.Collections.Generic;
using WarthogInc.BlfChunks;

namespace SunriseBlfTool.BlfChunks
{
    public class BlfChunkNameMap06481 : AbstractBlfChunkNameMap
    {
        public BlfChunkNameMap06481()
        {
            RegisterChunks();
        }

        private void RegisterChunks()
        {
            RegisterChunk<StartOfFile>();
            RegisterChunk<EndOfFile>();
            RegisterChunk<HopperConfigurationTable2>();
            RegisterChunk<MatchmakingHopperDescriptions1>();
            RegisterChunk<Author>();
            RegisterChunk<GameSet1>();
            RegisterChunk<MatchmakingTips>();
            RegisterChunk<MatchmakingHopperStatistics>();
            //RegisterChunk<NetworkConfiguration>();
            RegisterChunk<PackedGameVariant>();
            RegisterChunk<PackedMapVariant>();
        }

        public override string GetVersion()
        {
            return "06481";
        }
    }
}
