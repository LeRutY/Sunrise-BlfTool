using Sunrise.BlfTool;
using System;
using System.Collections.Generic;
using WarthogInc.BlfChunks;

namespace SunriseBlfTool.BlfChunks
{
    public class BlfChunkNameMap10015 : AbstractBlfChunkNameMap
    {
        public BlfChunkNameMap10015()
        {
            RegisterChunks();
        }

        private void RegisterChunks()
        {
            RegisterChunk<StartOfFile>();
            RegisterChunk<EndOfFile>();
            RegisterChunk<HopperConfigurationTable9>();
            RegisterChunk<MatchmakingHopperDescriptions2>();
            RegisterChunk<UserBanhammer>();
            RegisterChunk<Author>();
            RegisterChunk<ServiceRecordIdentity>();
            RegisterChunk<MessageOfTheDayPopup>();
            RegisterChunk<NagMessage>();
            RegisterChunk<GameSet>();
            RegisterChunk<Manifest>();
            RegisterChunk<MatchmakingTips>();
            RegisterChunk<MatchmakingBanhammerMessages>();
            RegisterChunk<MapManifest>();
            RegisterChunk<MatchmakingHopperStatistics>();
            //RegisterChunk<NetworkConfiguration>();
            RegisterChunk<PackedGameVariant>();
            RegisterChunk<PackedMapVariant>();
            RegisterChunk<ContentHeader>();
            RegisterChunk<FileQueue>();
            RegisterChunk<UserPlayerData>();
            RegisterChunk<RecentPlayers>();
            RegisterChunk<MachineNetworkStatistics>();
            RegisterChunk<MessageOfTheDay>();
            RegisterChunk<MultiplayerPlayers>();
        }

        public override string GetVersion()
        {
            return "10015";
        }
    }
}
