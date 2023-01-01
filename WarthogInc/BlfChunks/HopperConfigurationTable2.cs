using Newtonsoft.Json;
using Sewer56.BitStream;
using Sewer56.BitStream.ByteStreams;
using System;
using System.IO;
using System.Text;
using WarthogInc.BlfChunks;
using Sunrise.BlfTool.Extensions;

namespace Sunrise.BlfTool
{
    class HopperConfigurationTable8 : IBLFChunk
    {
        [JsonIgnore]
        public byte configurationsCount { get { return (byte)configurations.Length; } }
        [JsonIgnore]
        public byte categoryCount { get { return (byte)categories.Length; } }
        public HopperCategory[] categories;
        public HopperConfiguration[] configurations;

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
            return "mhcf";
        }

        public ushort GetVersion()
        {
            return 2;
        }

        public void ReadChunk(ref BitStream<StreamByteStream> hoppersStream)
        {
            throw new NotImplementedException();
        }

        public void WriteChunk(ref BitStream<StreamByteStream> hoppersStream)
        {
            hoppersStream.Write<byte>(categoryCount, 3);
            bool validCategoriesCount = categoryCount >= 0 && categoryCount <= 4;

            if (!validCategoriesCount)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Too many hopper categories to convert! ${categoryCount}/4");
                throw new InvalidDataException("Too many hopper categories to convert!");
            }

            for (int i = 0; validCategoriesCount && i < categoryCount; i++)
            {
                HopperCategory category = categories[i];

                hoppersStream.Write<ushort>(category.identifier, 16);
                hoppersStream.WriteString(category.name, 32, Encoding.UTF8);

                categories[i] = category;
            }

            bool validConfigurationsCount = configurationsCount >= 0 && configurationsCount <= 32;

            if (!validConfigurationsCount)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Too many hopper configurations to convert! ${configurationsCount}/32");
                throw new InvalidDataException("Too many hopper configurations to convert!");
            }

            hoppersStream.Write<byte>(configurationsCount, 5);

            for (int i = 0; validConfigurationsCount && i < configurationsCount; i++)
            {
                HopperConfiguration configuration = configurations[i];

                hoppersStream.WriteString(configuration.name, 32, Encoding.UTF8);

                hoppersStream.Write<ushort>(configuration.identifier, 16);

                hoppersStream.Write<ushort>(configuration.category, 16);
                hoppersStream.Write<byte>(configuration.type, 2);
                hoppersStream.Write<ushort>(configuration.sortKey, 10);
                hoppersStream.Write<byte>(configuration.imageIndex, 6);
                hoppersStream.Write<byte>(configuration.xLastIndex, 5);
                hoppersStream.Write<uint>(configuration.startTime, 25);
                hoppersStream.Write<uint>(configuration.endTime, 25);
                hoppersStream.Write<uint>(configuration.regions, 32);
                hoppersStream.Write<uint>(configuration.minimumExperienceRank, 4);
                hoppersStream.Write<uint>(configuration.maximumExperienceRank, 4);
                hoppersStream.Write<byte>(configuration.minimumPartySize, 4);
                hoppersStream.Write<byte>(configuration.maximumPartySize, 4); //ok
                hoppersStream.Write<byte>(configuration.hopperAccessBit, 4);
                hoppersStream.Write<byte>(configuration.accountTypeAccess, 2);
                hoppersStream.Write<byte>(configuration.require_all_party_members_meet_base_xp_requirements ? (byte)1 : (byte)0, 1);
                hoppersStream.Write<byte>(configuration.require_all_party_members_meet_access_requirements ? (byte)1 : (byte)0, 1);
                hoppersStream.Write<byte>(configuration.require_all_party_members_meet_live_account_access_requirements ? (byte)1 : (byte)0, 1); // seems wrong
                hoppersStream.Write<byte>(configuration.hide_hopper_from_xp_restricted_players ? (byte)1 : (byte)0, 1);
                hoppersStream.Write<byte>(configuration.hide_hopper_from_access_restricted_players ? (byte)1 : (byte)0, 1);
                hoppersStream.Write<byte>(configuration.hide_hopper_from_live_account_access_restricted_players ? (byte)1 : (byte)0, 1);
                hoppersStream.Write<byte>(configuration.requires_beta_rights? (byte)1 : (byte)0, 1);
                hoppersStream.Write<byte>(configuration.requires_all_downloadable_maps ? (byte)1 : (byte)0, 1);
                hoppersStream.Write<byte>(configuration.veto_enabled ? (byte)1 : (byte)0, 1);
                hoppersStream.Write<byte>(configuration.guests_allowed ? (byte)1 : (byte)0, 1);
                hoppersStream.Write<byte>(configuration.stats_write, 2);
                hoppersStream.Write<byte>(configuration.language_filter, 2);
                hoppersStream.Write<byte>(configuration.country_code_filter, 2);
                hoppersStream.Write<byte>(configuration.gamerzone_filter, 2);
                hoppersStream.Write<byte>(configuration.quitter_filter_percentage, 7);
                hoppersStream.Write<byte>(configuration.quitter_filter_maximum_party_size, 4);
                hoppersStream.Write<ushort>(configuration.rematch_countdown_timer, 10);
                hoppersStream.Write<byte>(configuration.rematch_group_formation, 2);
                hoppersStream.Write<byte>(configuration.repeated_opponents_to_consider_for_penalty, 2);
                hoppersStream.Write<ushort>(configuration.maximum_total_matchmaking_seconds, 10);
                hoppersStream.Write<ushort>(configuration.gather_start_game_early_seconds, 10);
                hoppersStream.Write<ushort>(configuration.gather_give_up_seconds, 10);

                for (int k = 0; k < 8; k++)
                    hoppersStream.Write<byte>(configuration.chance_of_gathering[k], 7);

                hoppersStream.Write<byte>(configuration.experience_points_per_win, 2);
                hoppersStream.Write<byte>(configuration.experience_penalty_per_drop, 2);

                for (int l = 0; l < 49; l++)
                    hoppersStream.WriteFloat(configuration.minimum_mu_per_level[l], 32);

                for (int m = 0; m < 50; m++)
                    hoppersStream.Write<byte>(configuration.maximum_skill_level_match_delta[m], 6);

                hoppersStream.WriteFloat(configuration.trueskill_sigma_multiplier, 32);
                hoppersStream.WriteFloat(configuration.trueskill_beta_performance_variation, 32);
                hoppersStream.WriteFloat(configuration.trueskill_tau_dynamics_factor, 32);
                hoppersStream.Write<byte>(configuration.trueskill_draw_probability, 32);
                hoppersStream.Write<byte>(configuration.trueskill_hillclimb_w0, 32);
                hoppersStream.Write<byte>(configuration.trueskill_hillclimb_w100, 32);


                if (configuration.type <= 1)
                {
                    hoppersStream.Write<byte>(configuration.minimum_player_count, 4);
                    hoppersStream.Write<byte>(configuration.maximum_player_count, 4);

                }
                else if (configuration.type == 3)
                {
                    hoppersStream.Write<byte>(configuration.team_count, 3);
                    hoppersStream.Write<byte>(configuration.minimum_team_size, 3);
                    hoppersStream.Write<byte>(configuration.maximum_team_size, 3);
                    hoppersStream.Write<byte>(configuration.maximum_team_imbalance, 3);
                    hoppersStream.Write<byte>(configuration.big_squad_size_threshold, 4);
                    hoppersStream.Write<byte>(configuration.maximum_big_squad_imbalance, 3);
                    hoppersStream.Write<byte>(configuration.enable_big_squad_mixed_skill_restrictions ? (byte)1 : (byte)0, 1);
                }
                else
                {
                    hoppersStream.Write<byte>(configuration.team_count, 3);
                    hoppersStream.Write<byte>(configuration.minimum_team_size, 3);
                    hoppersStream.Write<byte>(configuration.maximum_team_size, 3);
                    hoppersStream.Write<byte>(configuration.allow_uneven_teams ? (byte)1 : (byte)0, 1);
                }
            }
        }

        public class HopperCategory
        {
            public ushort identifier;
            public string name;
        }

        public class HopperConfiguration
        {
            public string name;
            public ushort identifier;
            public ushort category;
            public byte type;
            public ushort sortKey;
            public byte imageIndex;
            public byte xLastIndex;
            public uint startTime;
            public uint endTime;
            public uint regions;
            public uint minimumExperienceRank;
            public uint maximumExperienceRank;
            public byte minimumPartySize;
            public byte maximumPartySize;
            public byte hopperAccessBit;
            public byte accountTypeAccess;
            public bool require_all_party_members_meet_base_xp_requirements;
            public bool require_all_party_members_meet_access_requirements;
            public bool require_all_party_members_meet_live_account_access_requirements;
            public bool hide_hopper_from_xp_restricted_players;
            public bool hide_hopper_from_access_restricted_players;
            public bool hide_hopper_from_live_account_access_restricted_players;
            public bool requires_beta_rights;
            public bool requires_all_downloadable_maps;
            public bool veto_enabled;
            public bool guests_allowed;
            public byte stats_write;
            public byte language_filter;
            public byte country_code_filter;
            public byte gamerzone_filter;
            public byte quitter_filter_percentage;
            public byte quitter_filter_maximum_party_size;
            public ushort rematch_countdown_timer;
            public byte rematch_group_formation;
            public byte repeated_opponents_to_consider_for_penalty;
            public ushort maximum_total_matchmaking_seconds;
            public ushort gather_start_game_early_seconds;
            public ushort gather_give_up_seconds;
            [JsonConverter(typeof(ByteArrayConverter))]
            public byte[] chance_of_gathering;
            public byte experience_points_per_win;
            public byte experience_penalty_per_drop;
            public float[] minimum_mu_per_level;
            [JsonConverter(typeof(ByteArrayConverter))]
            public byte[] maximum_skill_level_match_delta;
            public float trueskill_sigma_multiplier;
            public float trueskill_beta_performance_variation;
            public float trueskill_tau_dynamics_factor;
            public byte trueskill_draw_probability;
            public byte trueskill_hillclimb_w0;
            public byte trueskill_hillclimb_w100;
            public byte minimum_player_count;
            public byte maximum_player_count;
            public byte team_count;
            public byte minimum_team_size;
            public byte maximum_team_size;
            public byte maximum_team_imbalance;
            public byte big_squad_size_threshold;
            public byte maximum_big_squad_imbalance;
            public bool enable_big_squad_mixed_skill_restrictions;
            public bool allow_uneven_teams;
        }
    }
}
