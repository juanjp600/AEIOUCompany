# AEIOU Company
Adds DECTalk (More widely known as the Text-To-Speech from Moonbase Alpha) to Lethal Company

![image](https://i.imgur.com/99exaIE.png)
## Description
Just type in chat and your character will speak or sing.

To use different voices, you need to prepend a message with the desired voice command.

The available voice commands are:
- [:nb], [:nd], [:nf], [:nh], [:nk], [:np], [:nr], [:nu], and [:nw]

Example: `[:nh] We need the money by monday!`

For help with singing check out the guides below

### Useful Guides
- [**Steam** | Text To Speech Songs](https://steamcommunity.com/sharedfiles/filedetails/?id=919364352)

- [**Steam** | Moonbase Alpha - ULTIMATE Text to Speech Handbook](https://steamcommunity.com/sharedfiles/filedetails/?id=482628855)
### Relevant Videos
- [**Youtube** | Moonbase Alpha: The Musical](https://www.youtube.com/watch?v=CNPKXfb3rws)

- [**Youtube** | Moonbase Alpha provides a realistic simulation of life on a natural satellite](https://www.youtube.com/watch?v=Hv6RbEOlqRo)

## Manual Install Instructions
Place the `aeioucompany` folder into your `BepInEx/plugins` folder

# Special Thanks To
- [Dennis Klatt](https://en.wikipedia.org/wiki/Dennis_H._Klatt) - For all his hard work and research towards speech synthesis
- [whatsecretproject](https://github.com/whatsecretproject) - [SharpTalk](https://github.com/whatsecretproject/SharpTalk)
- [ZeekerssRBLX](https://twitter.com/ZeekerssRBLX) - For the very epic game
- All of you guys - For enjoying this and providing feedback
- My girlfriend and friends for helping me test in early stages
- and anybody else who supported this mod or helped create anything that it uses

# Feedback / [Discord](https://discord.gg/QPAt6fHExW)
[AEIOUCompany Discord | Report Bugs, Request Features, Provide Feedback](https://discord.gg/QPAt6fHExW)
# Notes for users
- [:tone] messages do not work currently
- The mod increases the max chat length to 1023 characters
- Some voice features are currently not available, so some voices may sound a little different than expected.
# Changelog
## v1.2.0
- Fixed chatting while holding another object while having a pocketed active walkie talkie
- Fixed issue where player may not be heard if they are more than 50 meters away when the message is sent
- Line breaks no longer cut off songs
- Updated README
## v1.1.2 and v1.1.3
- Fixed walkie talkie behavior when pocketed
- Disabled test code I accidentally left enabled
## v1.1.1
- Fixed unpleasant S sounds when using the walkie talkie
## v1.1.0
- Added walkie talkie integration
- Mouth Dogs and other enemies now hear the voice too
- Fixed bug that would sometimes cause player to not be heard
- Added config options for the startup message, volume, and doppler effect
- Added changelog to README
- Fixed a link in README
## v1.0.1 and v1.0.2
- Fixed dependency issue
- Removed test code
- Added discord link to the README
## v1.0.0
- Initial Release