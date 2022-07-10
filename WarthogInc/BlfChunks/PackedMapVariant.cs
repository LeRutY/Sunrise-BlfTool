﻿using Newtonsoft.Json;
using Sewer56.BitStream;
using Sewer56.BitStream.ByteStreams;
using Sunrise.BlfTool.BlfChunks.GameEngineVariants;
using System;
using System.IO;
using WarthogInc.BlfChunks;
using Sunrise.BlfTool.Extensions;
using System.Linq;
using System.Collections.Generic;

namespace Sunrise.BlfTool
{
    public class PackedMapVariant : IBLFChunk
    {
        public BaseGameVariant.VariantMetadata metadata;
        public byte mapVariantVersion;
        public int mapVariantChecksum; // 32
        public short numberOfScenarioObjects; // 10
        [JsonIgnore]
        public short numberOfVariantObjects { get { return (short)objects.Length; } } // 10
        [JsonIgnore]
        public short numberOfPlacableObjectQuotas { get { return (short)budget.Length; } } // 9
        public int mapID;
        public bool builtIn;
        public WorldBounds worldBounds;
        public byte gameEngineSubtype;
        public float maximumBudget;
        public float spentBudget;
        public VariantObject[] objects; // * 640
        public short[] objectTypes; // 9 * 14
        public VariantBudgetEntry[] budget; // * 256

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
            return "mvar";
        }

        public ushort GetVersion()
        {
            return 10;
        }

        public void ReadChunk(ref BitStream<StreamByteStream> hoppersStream)
        {
            metadata = new BaseGameVariant.VariantMetadata(ref hoppersStream);
            mapVariantVersion = hoppersStream.Read<byte>(8);
            mapVariantChecksum = hoppersStream.Read<int>(32);
            numberOfScenarioObjects = hoppersStream.Read<short>(10);
            short numberOfVariantObjects = hoppersStream.Read<short>(10);
            short numberOfPlacableObjectQuotas = hoppersStream.Read<short>(9);
            mapID = hoppersStream.Read<int>(32);
            builtIn = hoppersStream.Read<byte>(1) > 0;
            worldBounds = new WorldBounds(ref hoppersStream);
            gameEngineSubtype = hoppersStream.Read<byte>(4);
            maximumBudget = hoppersStream.ReadFloat(32);
            spentBudget = hoppersStream.ReadFloat(32);

            List<VariantObject> objectsList = new List<VariantObject>();
            for (int i = 0; i < numberOfVariantObjects; i++)
            {
                bool objectExists = hoppersStream.Read<byte>(1) > 0;
                if (!objectExists)
                    continue;
                objectsList.Add(new VariantObject(ref hoppersStream));
            }

            objects = objectsList.ToArray();

            objectTypes = new short[14];
            for (int i = 0; i < 14; ++i)
            {
                objectTypes[i] = hoppersStream.Read<short>(9);
            }

            budget = new VariantBudgetEntry[numberOfPlacableObjectQuotas];
            for (int i = 0; i < numberOfPlacableObjectQuotas; i++)
            {
                budget[i] = new VariantBudgetEntry(ref hoppersStream);
            }

            hoppersStream.Seek(hoppersStream.NextByteIndex, 0);

            if (mapVariantVersion == 13)
                ConvertMCCMap();
        }

        public void WriteChunk(ref BitStream<StreamByteStream> hoppersStream)
        {
            throw new NotImplementedException();
        }

        public class WorldBounds
        {
            public int xMin;
            public int xMax;
            public int yMin;
            public int yMax;
            public int zMin;
            public int zMax;

            public WorldBounds() { }

            public WorldBounds(ref BitStream<StreamByteStream> hoppersStream)
            {
                Read(ref hoppersStream);
            }

            public void Read(ref BitStream<StreamByteStream> hoppersStream)
            {
                xMin = hoppersStream.Read<int>(32);
                xMax = hoppersStream.Read<int>(32);
                yMin = hoppersStream.Read<int>(32);
                yMax = hoppersStream.Read<int>(32);
                zMin = hoppersStream.Read<int>(32);
                zMax = hoppersStream.Read<int>(32);
            }

            public void Write(ref BitStream<StreamByteStream> hoppersStream)
            {
                hoppersStream.Write(xMin, 32);
                hoppersStream.Write(xMax, 32);
                hoppersStream.Write(yMin, 32);
                hoppersStream.Write(yMax, 32);
                hoppersStream.Write(zMin, 32);
                hoppersStream.Write(zMax, 32);
            }
        }
        public class VariantObject {
            public enum SHAPE_TYPE : byte
            {
                UNKNOWN_1 = 1,
                UNKNOWN_2,
                UNKNOWN_3,
            }

            public short flags;
            public int definitionIndex;
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public long? parentObjectIdentifier;
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public Position position;
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public Axes axis;
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public byte? propertiesCachedObjectType;
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public byte? propertiesFlags;
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public short? propertiesGameEngineFlags;
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public byte? propertiesSharedStorage;
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public byte? propertiesSpawnTime;
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public byte? propertiesTeamAffiliation;
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public SHAPE_TYPE? propertiesShapeType;
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public short? propertiesShapeRadiusWidth;
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public short? propertiesShapeDepth;
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public short? propertiesShapeTop;
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public short? propertiesShapeBottom;

            public VariantObject() { }

            public VariantObject(ref BitStream<StreamByteStream> hoppersStream)
            {
                Read(ref hoppersStream);
            }

            public void Read(ref BitStream<StreamByteStream> hoppersStream)
            {
                flags = hoppersStream.Read<short>(16);
                definitionIndex = hoppersStream.Read<int>(32);
                bool parentObjectExists = hoppersStream.Read<byte>(1) > 0;

                if (parentObjectExists)
                    parentObjectIdentifier = hoppersStream.Read<long>(64);

                bool positionExists = hoppersStream.Read<byte>(1) > 0;

                if (positionExists)
                {
                    position = new Position(ref hoppersStream);
                    axis = new Axes(ref hoppersStream);
                    propertiesCachedObjectType = hoppersStream.Read<byte>(8);
                    propertiesFlags = hoppersStream.Read<byte>(8);
                    propertiesGameEngineFlags = hoppersStream.Read<short>(16);
                    propertiesSharedStorage = hoppersStream.Read<byte>(8);
                    propertiesSpawnTime = hoppersStream.Read<byte>(8);
                    propertiesTeamAffiliation = hoppersStream.Read<byte>(8);
                    propertiesShapeType = (SHAPE_TYPE)hoppersStream.Read<byte>(8);
                    switch(propertiesShapeType)
                    {
                        case SHAPE_TYPE.UNKNOWN_1:
                            propertiesShapeRadiusWidth = hoppersStream.Read<short>(16);
                            break;
                        case SHAPE_TYPE.UNKNOWN_2:
                            propertiesShapeRadiusWidth = hoppersStream.Read<short>(16);
                            propertiesShapeDepth = hoppersStream.Read<short>(16);
                            propertiesShapeTop = hoppersStream.Read<short>(16);
                            break;
                        case SHAPE_TYPE.UNKNOWN_3:
                            propertiesShapeRadiusWidth = hoppersStream.Read<short>(16);
                            propertiesShapeDepth = hoppersStream.Read<short>(16);
                            propertiesShapeTop = hoppersStream.Read<short>(16);
                            propertiesShapeBottom = hoppersStream.Read<short>(16);
                            break;
                    }
                }
            }
        }

        public class VariantBudgetEntry
        {
            [JsonConverter(typeof(ObjectIndexConverter))]
            public uint objectDefinitionIndex;
            public byte minimumCount;
            public byte maximumCount;
            public byte placedOnMap;
            public byte maximumAllowed;
            public float pricePerItem;

            public VariantBudgetEntry() { }

            public VariantBudgetEntry(ref BitStream<StreamByteStream> hoppersStream)
            {
                Read(ref hoppersStream);
            }

            public void Read(ref BitStream<StreamByteStream> hoppersStream)
            {
                objectDefinitionIndex = hoppersStream.Read<uint>(32);
                minimumCount = hoppersStream.Read<byte>(8);
                maximumCount = hoppersStream.Read<byte>(8);
                placedOnMap = hoppersStream.Read<byte>(8);
                maximumAllowed = hoppersStream.Read<byte>(8);
                pricePerItem = hoppersStream.ReadFloat(32);
            }
        }

        public class Position
        {
            public short x;
            public short y;
            public short z;

            public Position() { }

            public Position(ref BitStream<StreamByteStream> hoppersStream)
            {
                Read(ref hoppersStream);
            }

            public void Read(ref BitStream<StreamByteStream> hoppersStream)
            {
                x = hoppersStream.Read<short>(16);
                y = hoppersStream.Read<short>(16);
                z = hoppersStream.Read<short>(16);
            }
        }

        public class Axes
        {
            public bool upIsGlobalUp3d;
            public int upQuantization;
            public byte forwardAngle;

            public Axes() { }

            public Axes(ref BitStream<StreamByteStream> hoppersStream)
            {
                Read(ref hoppersStream);
            }

            public void Read(ref BitStream<StreamByteStream> hoppersStream)
            {
                upIsGlobalUp3d = hoppersStream.Read<byte>(1) > 0;

                if (!upIsGlobalUp3d)
                {
                    upQuantization = hoppersStream.Read<int>(19);
                }
                forwardAngle = hoppersStream.Read<byte>(8);
            }
        }

        private void ConvertMCCMap()
        {
            BudgetObjectIndexConverter objectIndexMap = new BudgetObjectIndexConverter(mapID);

            List<VariantBudgetEntry> newBudget = new List<VariantBudgetEntry>();

            foreach(VariantBudgetEntry entry in budget) {
                short objectGroup = (short)(entry.objectDefinitionIndex >> 16);
                short objectIndex = (short)(entry.objectDefinitionIndex & 0xffff);

                if (objectGroup > 7)
                    continue;

                try
                {
                    entry.objectDefinitionIndex = objectIndexMap.Get360ObjectIndex(objectGroup, objectIndex);
                } catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    Console.WriteLine($"Unknown 360 object index for [{objectGroup},{objectIndex}]");
                }

                newBudget.Add(entry);
            }

            budget = newBudget.ToArray();
            mapVariantVersion = 12;
        }
    }
}
