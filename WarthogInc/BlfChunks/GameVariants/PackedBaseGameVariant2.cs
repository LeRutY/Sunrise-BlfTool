using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Sewer56.BitStream;
using Sewer56.BitStream.ByteStreams;
using Sunrise.BlfTool.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sunrise.BlfTool.BlfChunks.GameEngineVariants
{
    public class PackedBaseGameVariant2 : IGameVariant
    {
        public VariantMetadata metadata;
        public bool builtIn; 
        public MiscellaneousOptions miscellaneousOptions;
        public RespawnOptions respawnOptions;
        public SocialOptions socialOptions;
        public MapOverrides mapOverrides;
        [JsonConverter(typeof(StringEnumConverter))]
        public TeamScoringMethod teamScoringMethod; // 3

        public enum Points : byte
        {
            ZERO_POINTS,
            ONE_POINT,
            TWO_POINTS,
            THREE_POINTS,
            FOUR_POINTS,
            FIVE_POINTS,
            SIX_POINTS,
            SEVEN_POINTS,
            EIGHT_POINTS,
            NINE_POINTS,
            TEN_POINTS,
            MINUS_TEN_POINTS = 22,
            MINUS_NINE_POINTS,
            MINUS_EIGHT_POINTS,
            MINUS_SEVEN_POINTS,
            MINUS_SIX_POINTS,
            MINUS_FIVE_POINTS,
            MINUS_FOUR_POINTS,
            MINUS_THREE_POINTS,
            MINUS_TWO_POINTS,
            MINUS_ONE_POINT,
        }

        public class VariantMetadata
        {
            public VariantMetadata() { }

            public VariantMetadata(ref BitStream<StreamByteStream> hoppersStream)
            {
                Read(ref hoppersStream);
            }

            public void Read(ref BitStream<StreamByteStream> hoppersStream)
            {
                throw new NotImplementedException();
            }

            public void Write(ref BitStream<StreamByteStream> hoppersStream)
            {
                hoppersStream.WriteBitswapped(0, 4);
                hoppersStream.WriteBitswappedString(name, 32, Encoding.BigEndianUnicode);
                hoppersStream.WriteBitswappedString(description, 32, Encoding.BigEndianUnicode);
            }

            public string name;
            public string description;
        }

        public class PlayerTraits
        {
            public PlayerTraits() { }

            public PlayerTraits(ref BitStream<StreamByteStream> hoppersStream)
            {
                Read(ref hoppersStream);
            }

            public void Read(ref BitStream<StreamByteStream> hoppersStream)
            {
                throw new NotImplementedException();
            }

            public void Write(ref BitStream<StreamByteStream> hoppersStream)
            {
                hoppersStream.WriteBitswapped((byte)damageResistance, 4);
                hoppersStream.WriteBitswapped((byte)shieldRechargeRate, 4);
                hoppersStream.WriteBitswapped((byte)vampirism, 3);
                hoppersStream.WriteBitswapped((byte)headshotImmunity, 2);
                hoppersStream.WriteBitswapped((byte)shieldMultiplier, 3);
                hoppersStream.WriteBitswapped((byte)damageModifier, 4);
                hoppersStream.WriteBitswapped((byte)primaryWeapon, 8);
                hoppersStream.WriteBitswapped((byte)secondaryWeapon, 8);
                hoppersStream.WriteBitswapped((byte)grenadeType, 8);
                hoppersStream.WriteBitswapped((byte)grenadeCount, 3);
                hoppersStream.WriteBitswapped((byte)infiniteAmmo, 2);
                hoppersStream.WriteBitswapped((byte)rechargingGrenades, 2);
                hoppersStream.WriteBitswapped((byte)weaponPickupAllowed, 2);
                hoppersStream.WriteBitswapped((byte)playerSpeed, 4);
                hoppersStream.WriteBitswapped((byte)playerGravity, 3);
                hoppersStream.WriteBitswapped((byte)vehicleUsage, 2);
            }

            [JsonConverter(typeof(StringEnumConverter))]
            public DamageResistance damageResistance; // 4
            [JsonConverter(typeof(StringEnumConverter))]
            public ShieldRechargeRate shieldRechargeRate; // 4
            [JsonConverter(typeof(StringEnumConverter))]
            public Vampirism vampirism; // 3
            [JsonConverter(typeof(StringEnumConverter))]
            public TraitBoolean headshotImmunity; // 2
            [JsonConverter(typeof(StringEnumConverter))]
            public ShieldMultiplier shieldMultiplier; // 3
            [JsonConverter(typeof(StringEnumConverter))]
            public DamageModifier damageModifier; // 4
            [JsonConverter(typeof(StringEnumConverter))]
            public Weapon primaryWeapon;
            [JsonConverter(typeof(StringEnumConverter))]
            public Weapon secondaryWeapon;
            public byte grenadeType;
            [JsonConverter(typeof(StringEnumConverter))]
            public GrenadeCount grenadeCount;
            [JsonConverter(typeof(StringEnumConverter))]
            public TraitBoolean infiniteAmmo;
            [JsonConverter(typeof(StringEnumConverter))]
            public TraitBoolean rechargingGrenades;
            [JsonConverter(typeof(StringEnumConverter))]
            public TraitBoolean weaponPickupAllowed;
            [JsonConverter(typeof(StringEnumConverter))]
            public PlayerSpeed playerSpeed;
            [JsonConverter(typeof(StringEnumConverter))]
            public PlayerGravity playerGravity;
            [JsonConverter(typeof(StringEnumConverter))]
            public VehicleUsage vehicleUsage;
            [JsonConverter(typeof(StringEnumConverter))]
            public ActiveCamo activeCamo;
            [JsonConverter(typeof(StringEnumConverter))]
            public PlayerWaypoint waypoint;
            [JsonConverter(typeof(StringEnumConverter))]
            public PlayerAura playerAura;
            [JsonConverter(typeof(StringEnumConverter))]
            public ForcedColorChange forcedColorChange;
            [JsonConverter(typeof(StringEnumConverter))]
            public MotionTacker motionTacker;
            [JsonConverter(typeof(StringEnumConverter))]
            public MotionTrackerRange motionTrackerRange;

            public enum DamageResistance : byte
            {
                UNCHANGED,
                PERCENT_10,
                PERCENT_50,
                PERCENT_90,
                NORMAL,
                PERCENT_110,
                PERCENT_150,
                PERCENT_200,
                PERCENT_300,
                PERCENT_500,
                PERCENT_1000,
                PERCENT_2000,
                INVULNERABLE,
            }

            public enum ShieldRechargeRate : byte
            {
                UNCHANGED,
                NEGATIVE_25_PERCENT,
                NEGATIVE_10_PERCENT,
                NEGATIVE_5_PERCENT,
                NO_RECHARGE,
                PERCENT_50,
                PERCENT_90,
                PERCENT_100,
                PERCENT_110,
                PERCENT_200,
            }

            public enum ShieldMultiplier : byte
            {
                UNCHANGED,
                NO_SHIELDS,
                NORMAL_SHIELDS,
                TIMES_2,
                TIMES_3,
                TIMES_4,
            }

            public enum Vampirism : byte
            {
                UNCHANGED,
                DISABLED,
                PERCENT_10,
                PERCENT_25,
                PERCENT_50,
                PERCENT_100,
            }

            public enum DamageModifier : byte
            {
                UNCHANGED,
                PERCENT_0,
                PERCENT_25,
                PERCENT_50,
                PERCENT_75,
                PERCENT_90,
                PERCENT_100,
                PERCENT_110,
                PERCENT_125,
                PERCEMT_150,
                PERCENT_200,
                PERCENT_300,
                INSTANT_KILL,
            }

            public enum Weapon : byte
            {
                BATTLE_RIFLE,
                ASSAULT_RIFLE,
                PLASMA_PISTOL,
                SPIKER,
                SMG,
                COVENANT_CARBINE,
                ENERGY_SWORD,
                MAGNUM,
                NEEDLER,
                PLASMA_RIFLE,
                ROCKET_LAUNCHER,
                SHOTGUN,
                SNIPER_RIFLE,
                BRUTE_SHOT,
                UNARMED,
                BEAM_RIFLE,
                SPARTAN_LASER,
                NONE,
                GRAVITY_HAMMER,
                MAULER,
                FLEMETHROWER,
                MISSILE_POD,
                RANDOM = 253,
                UNCHANGED = 254,
                MAP_DEFAULT = 255,
            }

            public enum GrenadeCount : byte
            {
                UNCHANGED,
                MAP_DEFAULT,
                NONE
            }

            public enum TraitBoolean : byte
            {
                UNCHANGED,
                DISABLED,
                ENABLED
            }

            public enum PlayerSpeed : byte
            {
                UNCHANGED,
                PERCENT_25,
                PERCENT_50,
                PERCENT_75,
                PERCENT_90,
                PERCENT_100,
                PERCENT_110,
                PERCENT_125,
                PERCENT_150,
                PERCENT_200,
                PERCENT_300,
            }

            public enum PlayerGravity : byte
            {
                UNCHANGED,
                PERCENT_50,
                PERCENT_75,
                PERCENT_100,
                PERCENT_150,
                PERCENT_200,
            }

            public enum VehicleUsage : byte
            {
                UNCHANGED,
                NONE,
                PASSENGER_ONLY,
                FULL_USE,
            }

            public enum ActiveCamo : byte
            {
                UNCHANGED,
                OFF,
                VERY_POOR,
                POOR,
                GOOD
            }

            public enum PlayerWaypoint : byte
            {
                UNCHANGED,
                NO_WAYPOINT,
                VISIBLE_TO_ALLIES,
                VISIBLE_TO_EVERYONE,
            }

            public enum PlayerAura : byte
            {
                UNCHANGED,
                OFF,
                TEAM_COLOR,
                BLACK,
                WHITE,
            }

            public enum ForcedColorChange : byte
            {
                UNCHANGED,
                OFF,
                RED,
                BLUE,
                GREEN,
                ORANGE,
                PURPLE,
                GOLD,
                BROWN,
                PINK,
                WHITE,
                BLACK,
                ZOMBIE,
            }

            public enum MotionTacker : byte
            {
                UNCHANGED,
                OFF,
                ALLIES_ONLY,
                NORMAL,
                ENHANCED
            }

            public enum MotionTrackerRange : byte
            {
                UNCHANGED,
                METERS_10,
                METERS_15,
                METERS_25,
                METERS_50,
                METERS_75,
                METERS_100,
                METERS_150
            }
        }

        public class MiscellaneousOptions
        {
            public MiscellaneousOptions() { }

            public MiscellaneousOptions(ref BitStream<StreamByteStream> hoppersStream)
            {
                Read(ref hoppersStream);
            }

            public void Read(ref BitStream<StreamByteStream> hoppersStream)
            {
                throw new NotImplementedException();
            }

            public void Write(ref BitStream<StreamByteStream> hoppersStream)
            {
                hoppersStream.WriteBitswapped(teams ? 1 : 0, 1);
                hoppersStream.WriteBitswapped(roundResetPlayers ? 1 : 0, 1);
                hoppersStream.WriteBitswapped(roundResetMap ? 1 : 0, 1);
                hoppersStream.WriteBitswapped(roundTimeLimitMinutes, 8);
                hoppersStream.WriteBitswapped(roundLimit, 4);
                hoppersStream.WriteBitswapped(earlyVictoryWinCount, 4);
            }

            public bool teams;
            public bool roundResetPlayers;
            public bool roundResetMap;
            public byte roundTimeLimitMinutes;
            public byte roundLimit; // 4
            public byte earlyVictoryWinCount; // 4
        }

        public class RespawnOptions
        {
            public RespawnOptions() { }

            public RespawnOptions(ref BitStream<StreamByteStream> hoppersStream)
            {
                Read(ref hoppersStream);
            }

            public void Read(ref BitStream<StreamByteStream> hoppersStream)
            {
                throw new NotImplementedException();
            }

            public void Write(ref BitStream<StreamByteStream> hoppersStream)
            {
                hoppersStream.WriteBitswapped(inheritRespawnTime ? 1 : 0, 1);
                hoppersStream.WriteBitswapped(respawnWithTeammate ? 1 : 0, 1);
                hoppersStream.WriteBitswapped(respawnAtLocation ? 1 : 0, 1);
                hoppersStream.WriteBitswapped(respawnOnKills ? 1 : 0, 1);
                hoppersStream.WriteBitswapped(livesPerRound, 6);
                hoppersStream.WriteBitswapped(teamLivesPerRound, 7);
                hoppersStream.WriteBitswapped(respawnTime, 8);
                hoppersStream.WriteBitswapped(suicideTime, 8);
                hoppersStream.WriteBitswapped(betrayalTime, 8);
                hoppersStream.WriteBitswapped(respawnGrowthTime, 4);
                hoppersStream.WriteBitswapped(respawnOptionsPlayerTraitsDuration, 6);
                respawnTraits.Write(ref hoppersStream);
            }

            public bool inheritRespawnTime;
            public bool respawnWithTeammate;
            public bool respawnAtLocation;
            public bool respawnOnKills;
            public byte livesPerRound; // 6
            public byte teamLivesPerRound; // 7
            public byte respawnTime;
            public byte suicideTime;
            public byte betrayalTime;
            public byte respawnGrowthTime; // 4
            public byte respawnOptionsPlayerTraitsDuration; // 6
            public PlayerTraits respawnTraits;
        }

        public class SocialOptions
        {
            public SocialOptions() { }

            public SocialOptions(ref BitStream<StreamByteStream> hoppersStream)
            {
                Read(ref hoppersStream);
            }

            public void Read(ref BitStream<StreamByteStream> hoppersStream)
            {
                throw new NotImplementedException();
            }

            public void Write(ref BitStream<StreamByteStream> hoppersStream)
            {
                hoppersStream.WriteBitswapped(observers ? 1 : 0, 1);
                hoppersStream.WriteBitswapped(teamChanging ? 1 : 0, 1);
                hoppersStream.WriteBitswapped(teamChangingBalancingOnly ? 1 : 0, 1);
                hoppersStream.WriteBitswapped(friendlyFire ? 1 : 0, 1);
                hoppersStream.WriteBitswapped(betrayalBooting ? 1 : 0, 1);
                hoppersStream.WriteBitswapped(enemyVoice ? 1 : 0, 1);
                hoppersStream.WriteBitswapped(openChannelVoice ? 1 : 0, 1);
                hoppersStream.WriteBitswapped(deadPlayerVoice ? 1 : 0, 1);
            }

            public bool observers;
            public bool teamChanging;
            public bool teamChangingBalancingOnly;
            public bool friendlyFire;
            public bool betrayalBooting;
            public bool enemyVoice;
            public bool openChannelVoice;
            public bool deadPlayerVoice;
        }

        public enum TeamScoringMethod : byte
        {
            SUM_OF_TEAM,
            MINIMUM_SCORE,
            MAXIMUM_SCORE,
        }

        public class MapOverrides
        {
            public MapOverrides() { }

            public MapOverrides(ref BitStream<StreamByteStream> hoppersStream)
            {
                Read(ref hoppersStream);
            }

            public void Read(ref BitStream<StreamByteStream> hoppersStream)
            {
                throw new NotImplementedException();
            }

            public void Write(ref BitStream<StreamByteStream> hoppersStream)
            {
                hoppersStream.WriteBitswapped(grenadesOnMap ? 1 : 0, 1);
                hoppersStream.WriteBitswapped(indestructibleVehicles ? 1 : 0, 1);
                baseTraits.Write(ref hoppersStream);
                hoppersStream.WriteBitswapped((byte)weaponSet, 8);
                hoppersStream.WriteBitswapped((byte)vehicleSet, 8);
                redPowerupTraits.Write(ref hoppersStream);
                bluePowerupTraits.Write(ref hoppersStream);
                yellowPowerupTraits.Write(ref hoppersStream);
                hoppersStream.WriteBitswapped(redPowerupDuration, 7);
                hoppersStream.WriteBitswapped(bluePowerupDuration, 7);
                hoppersStream.WriteBitswapped(yellowPowerupDuration, 7);
            }

            public bool grenadesOnMap;
            public bool indestructibleVehicles;
            public PlayerTraits baseTraits;
            [JsonConverter(typeof(StringEnumConverter))]
            public WeaponSet weaponSet;
            [JsonConverter(typeof(StringEnumConverter))]
            public VehicleSet vehicleSet;
            public PlayerTraits redPowerupTraits;
            public PlayerTraits bluePowerupTraits;
            public PlayerTraits yellowPowerupTraits;
            public byte redPowerupDuration; // 7
            public byte bluePowerupDuration; // 7
            public byte yellowPowerupDuration; // 7

            public enum WeaponSet : byte
            {
                ASSAULT_RIFLES,
                DUALS,
                GRAVITY_HAMMERS,
                SPARTAN_LASER,
                ROCKET_LAUNCHERS,
                SHOTGUNS,
                SNIPER_RIFLES,
                ENERGY_SWORDS,
                RANDOM,
                NO_POWER_WEAPONS,
                NO_SNIPERS,
                NO_WEAPONS,
                MAP_DEFAULT = 255,
            }

            public enum VehicleSet : byte
            {
                NO_VEHICLES = 1,
                MONGOOSES_ONLY,
                LIGHT_GROUND_ONLY,
                TANKS_ONLY,
                AIRCRAFT_ONLY,
                NO_LIGHT_GROUND,
                NO_TANKS,
                NO_AIRCRAFT,
                ALL_VEHICLES,
                MAP_DEFAULT = 255,
            }
        }

        public void Read(ref BitStream<StreamByteStream> hoppersStream)
        {
            metadata = new VariantMetadata(ref hoppersStream);
            builtIn = hoppersStream.Read<byte>(1) > 0;
            miscellaneousOptions = new MiscellaneousOptions(ref hoppersStream);
            respawnOptions = new RespawnOptions(ref hoppersStream);
            socialOptions = new SocialOptions(ref hoppersStream);
            mapOverrides = new MapOverrides(ref hoppersStream);
            teamScoringMethod = (TeamScoringMethod)hoppersStream.Read<byte>(3);
        }

        public void Write(ref BitStream<StreamByteStream> hoppersStream)
        {
            metadata.Write(ref hoppersStream);
            //hoppersStream.Write(builtIn ? 1 : 0, 1);
            miscellaneousOptions.Write(ref hoppersStream);
            respawnOptions.Write(ref hoppersStream);
            socialOptions.Write(ref hoppersStream);
            mapOverrides.Write(ref hoppersStream);
            //hoppersStream.Write((byte)teamScoringMethod, 3);
        }
    }
}
