﻿using SunriseBlfTool.BlfChunks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarthogInc.BlfChunks;

namespace Sunrise.BlfTool.TitleConverters
{
    public class TitleConverter_10015 : ITitleConverter
    {
        private static readonly AbstractBlfChunkNameMap chunkNameMap = new BlfChunkNameMap10015();
        public void ConvertBlfToJson(string blfFolder, string jsonFolder)
        {
            throw new NotImplementedException();
        }

        public void ConvertJsonToBlf(string jsonFolder, string blfFolder)
        {
            jsonFolder += "\\";
            blfFolder += "\\";

            Console.WriteLine("Converting JSON files to BLF...");

            var hoppersFolderEnumerator = Directory.EnumerateDirectories(jsonFolder, "*", SearchOption.TopDirectoryOnly).GetEnumerator();

            while (hoppersFolderEnumerator.MoveNext())
            {
                var hopperFolder = hoppersFolderEnumerator.Current;
                var hopperFolderName = hopperFolder.Substring(hopperFolder.LastIndexOf("\\") + 1);

                Console.WriteLine("Converting " + hopperFolderName);

                var jsonFileEnumerator = Directory.EnumerateFiles(hopperFolder, "*.*", SearchOption.AllDirectories).GetEnumerator();

                var fileHashes = new Dictionary<string, byte[]>();

                while (jsonFileEnumerator.MoveNext())
                {
                    string fileName = jsonFileEnumerator.Current;
                    if (fileName.Contains("\\"))
                        fileName = fileName.Substring(fileName.LastIndexOf("\\") + 1);

                    string fileRelativePath = jsonFileEnumerator.Current.Replace(jsonFolder, "");
                    string fileDirectoryRelativePath;
                    if (fileRelativePath.Contains("\\"))
                    {
                        fileDirectoryRelativePath = fileRelativePath.Substring(0, fileRelativePath.LastIndexOf("\\"));
                        Directory.CreateDirectory(blfFolder + fileDirectoryRelativePath);
                    }

                    if (fileName.EndsWith(".bin") || fileName.EndsWith(".jpg"))
                    {
                        File.Copy(jsonFileEnumerator.Current, blfFolder + fileRelativePath, true);
                        Console.WriteLine("Copied file: " + fileRelativePath);

                        continue;
                    }

                    BlfFile blfFile = BlfFile.FromJSON(File.ReadAllText(jsonFileEnumerator.Current), chunkNameMap);

                    blfFile.WriteFile(blfFolder + fileRelativePath.Replace(".json", ".bin"));

                    Console.WriteLine("Converted file: " + fileRelativePath);

                    if (blfFile.HasChunk<MatchmakingHopperDescriptions2>()
                        || blfFile.HasChunk<MatchmakingTips>()
                        || blfFile.HasChunk<MapManifest>()
                        || blfFile.HasChunk<MatchmakingBanhammerMessages>())
                    {
                        fileHashes.Add("/title/default_hoppers/" + fileRelativePath.Replace("\\", "/").Replace(".json", ".bin"), BlfFile.ComputeHash(blfFolder + fileRelativePath.Replace(".json", ".bin")));
                    }
                }

                fileHashes.Add("/title/default_hoppers/matchmaking_hopper_009.bin", BlfFile.ComputeHash(blfFolder + "\\default_hoppers\\matchmaking_hopper_009.bin"));
                fileHashes.Add("/title/default_hoppers/network_configuration_088.bin", BlfFile.ComputeHash(blfFolder + "\\default_hoppers\\network_configuration_088.bin"));

                Manifest.FileEntry[] fileEntries = new Manifest.FileEntry[fileHashes.Count];
                int i = 0;
                foreach (KeyValuePair<string, byte[]> fileNameHash in fileHashes)
                {
                    fileEntries[i] = new Manifest.FileEntry()
                    {
                        filePath = fileNameHash.Key,
                        fileHash = fileNameHash.Value
                    };
                    i++;
                }

                var onfm = new Manifest()
                {
                    files = fileEntries
                };

                BlfFile manifestFile = new BlfFile();
                manifestFile.AddChunk(onfm);
                manifestFile.WriteFile(blfFolder + "\\default_hoppers\\manifest_001.bin");

                Console.WriteLine("Created file: manifest_001.bin");
            }
        }

        public virtual string GetVersion()
        {
            return "10015";
        }
    }
}
