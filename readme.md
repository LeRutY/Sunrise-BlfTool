# Sunrise BLF Tool [![Build status](https://ci.appveyor.com/api/projects/status/uq8ueptsbwwkegt2?svg=true)](https://ci.appveyor.com/project/craftycodie/sunrise-blftool)
This tool allows you to edit blf files used by Halo, which include data like network configuration and matchmaking playlists.

## How To Use
The tool is currently setup to deal with title storage data as a whole, and built for Halo 3.<br>
The launch arguments are as follows:

`<desired output type (blf or json)> <input folder> <output folder>`

For example<br>
`blf ".\title storage\json" ".\title stoage\blf"`

## How it works
The tool will convert files to and from json using the Newtonsoft json library.
Hashes are ommitted from the JSON files, instead these are couputed as the tool converts to BLF.
The online manifest fime (onmf) is not converted, instead it is rebuilt as it only contains hashes.
Map variants are not currently supported and are copied as is.

## Chain of Integrity
Halo 3's title storage files, which are the primary focus of this tool, include a lot of hashes as a means of integrity checking. These are salted SHA-256 hashes, the salt is hardcoded within the blf tool.
- First the manifest file is read, this contains hashes for the hopper configuration, hopper descriptions, network configuration, and some other files.
- The hopper configuration file contains hashes for each hopper's game set file.
- The hopper game set files contain hashes for each game and map variant within the set.
- Map variants contain a map variant checksum. This is a CRC32 of a 256 Byte buffer, I've not worked out what that buffer is but what I do know is that every map (not variant, map) has the same map variant checksum, so I've hardcoded these.
