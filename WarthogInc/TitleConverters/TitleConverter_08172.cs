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
    public class TitleConverter_08172 : ITitleConverter
    {
        private static readonly AbstractBlfChunkNameMap chunkNameMap = new BlfChunkNameMap08172();
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

            File.Copy(jsonFolder + "\\matchmaking_nightmap.jpg", blfFolder + "\\matchmaking_nightmap.jpg", true);
            File.Copy(jsonFolder + "\\network_configuration_080.bin", blfFolder + "\\network_configuration_080.bin", true);


            while (hoppersFolderEnumerator.MoveNext())
            {
                var hopperFolder = hoppersFolderEnumerator.Current;
                var hopperFolderName = hopperFolder.Substring(hopperFolder.LastIndexOf("\\") + 1);

                Console.WriteLine("Converting " + hopperFolderName);

                var jsonFileEnumerator = Directory.EnumerateFiles(hopperFolder, "*.*", SearchOption.AllDirectories).GetEnumerator();

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

                    if (fileName == "matchmaking_hopper_008.json")
                        continue; // Handle after game sets


                    blfFile.WriteFile(blfFolder + fileRelativePath.Replace(".json", ".bin"));

                    Console.WriteLine("Converted file: " + fileRelativePath);
                }

                // And now for the manual ones!
                // First up, matchmaking playlists.
                var hopperConfigurationTableBlfFile = BlfFile.FromJSON(File.ReadAllText(jsonFolder + "default_hoppers\\matchmaking_hopper_008.json"), chunkNameMap);
                var mhcf = hopperConfigurationTableBlfFile.GetChunk<HopperConfigurationTable8>();

                BlfFile hoppersFile = new BlfFile();
                hoppersFile.AddChunk(mhcf);
                hoppersFile.WriteFile(blfFolder + "\\default_hoppers\\matchmaking_hopper_008.bin");

                Console.WriteLine("Converted file: default_hoppers\\matchmaking_hopper_008.json");

            }

            var fileHashes = new Dictionary<string, byte[]>();

            fileHashes.Add("/title/default_hoppers/matchmaking_hopper_008.bin", BlfFile.ComputeHash(blfFolder + "\\default_hoppers\\matchmaking_hopper_008.bin"));
            fileHashes.Add("/title/default_hoppers/matchmaking_hopper_descriptions_002.bin", BlfFile.ComputeHash(blfFolder + "\\default_hoppers\\matchmaking_hopper_descriptions_002.bin"));

            fileHashes.Add("/title/network_configuration_080.bin", BlfFile.ComputeHash(blfFolder + "\\network_configuration_080.bin"));

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
            manifestFile.WriteFile(blfFolder + "\\manifest_001.bin");

            Console.WriteLine("Created file: manifest_001.bin");
        }

        public string GetVersion()
        {
            return "08172";
        }
    }
}
