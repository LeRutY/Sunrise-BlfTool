# Sunrise BLF Tool [![Build status](https://ci.appveyor.com/api/projects/status/uq8ueptsbwwkegt2?svg=true)](https://ci.appveyor.com/project/craftycodie/sunrise-blftool)
This tool allows you to edit blf files used by Halo, which include data like network configuration and matchmaking playlists.

## How To Use
The tool is currently setup to deal with title storage data as a whole, and built for Halo 3.<br>
The launch arguments are as follows:

`<desired output type (blf or json)> <input folder> <output folder> <game version>`

For example<br>
`blf ".\title storage\json" ".\title stoage\blf" 12070`

## What is this tool for?
The primary objectives of this tool are to enable creation of server files for third-generation halo games (and potentially halo 2 in future).
This involves converting human readable JSON files to BLFs which the game can understand, and allowing users to import content made in game such as map & game variants, by converting them back to JSON.

The secondary objectives of this tool are around research. This program provides a good foundation for studying unrelated chunks such as saved content like films and screenshots.

## Supported Versions
- 12070 - Halo 3 ODST
- 12070 - Halo 3 Retail
- 11902 - Halo 3 Retail (untested)
- 11856 - Halo 3 Epsilon
- 11855 - Halo 3 Retail (untested)
- 11729 - Halo 3 Epsilon
- 10015 - Halo 3 Beta (WIP)
- 09699 - Halo 3 Beta (WIP)
- 08172 - Halo 3 Delta (WIP)
- 06481 - Halo 3 Alpha (WIP)

## What is a BLF?
BLF is a binary file format used by various halo (blam) games. The BLF format allows you to define blf "chunks", different types of data separated within a file. Each BLF chunk starts with a chunk header, including the 4 character name, length and version of the chunk. Each BLF file starts with a header (_blf) chunk, and ends with an end-of-file (_eof) chunk.

A BLF chunk usualy contains either a game data structure, an example of this is a netc (network configuration) chunk. BLF chunks can also contain bit aligned 'packed' data as well, for example mhcf (matchmaking hopper configuration file). Along with bit alignment, you may also see byteswapped and/or bitswapped data.

## Chain of Integrity
Halo 3's title storage files, which are the primary focus of this tool, include a lot of hashes as a means of integrity checking. These are salted SHA-256 hashes, the salt is hardcoded within the blf tool.
- First the manifest file is read, this contains hashes for the hopper configuration, hopper descriptions, network configuration, and some other files.
- The hopper configuration file contains hashes for each hopper's game set file.
- The hopper game set files contain hashes for each game and map variant within the set.
- Map variants contain a map variant checksum. This is a CRC32 of a 256 Byte buffer, I've not worked out what that buffer is but what I do know is that every map (not variant, map) has the same map variant checksum, so I've hardcoded these.
