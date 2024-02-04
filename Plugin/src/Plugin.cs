using System.Collections.Generic;
using System.Linq;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace AEIOU_Company
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        public static Harmony Harmony = null;
        protected static new ManualLogSource Logger = null;
        public static bool PlayStartingUpMessage = false;
        public static float TTSVolume = 0f;
        public static float TTSDopperLevel;
        public static int ChatSize;
        public static bool EnableDeadChat = true;
        public static string BlacklistPrefix = "/";

        public static void Log(object data)
        {
            Logger?.LogInfo(data);
        }
        public static void LogError(object data)
        {
            Logger?.LogError(data);
        }

        public void Awake()
        {
            Harmony harmony = new Harmony(PluginInfo.PLUGIN_GUID);
            Harmony = harmony;
            Logger = base.Logger;

            PlayStartingUpMessage = Config.Bind<bool>("General", "StartingUpMessage", true, "Enables \"starting up\" sound effect.").Value;
            TTSVolume = Config.Bind<float>("General", "Volume", 1f, "Volume scale of text-to-speech-voice. Values range from 0 to 1").Value;
            TTSDopperLevel = Config.Bind<float>("General", "Doppler Effect Level", 1f, "Values range from 0 to 1").Value;
            ChatSize = Config.Bind<int>("Advanced", "Chat Character Limit", 1024, "WARNING: Everybody must have the same value set for this!").Value;
            EnableDeadChat = Config.Bind<bool>("General", "Enable Dead Chat", true, "Enables chatting after dead").Value;
            BlacklistPrefix = Config.Bind<string>("General", "Blacklist Prefix", "/", "TTS Ignores messages starting with this").Value;

            PermissionConfiguration.CurrentSettings.DefaultPermissions = Config.Bind<PermissionConfiguration.Permissions>(
                    section: "Permissions",
                    key: "DefaultPermissions",
                    defaultValue: PermissionConfiguration.Permissions.None,
                    description: "Default permissions.")
                .Value;

            IEnumerable<ulong> stringToSteamIds(string str)
            {
                foreach (var value in str.Split(','))
                {
                    if (ulong.TryParse(value, out var steamId))
                    {
                        yield return steamId;
                    }
                }
            }

            var steamIdsThatCanUseTts = stringToSteamIds(Config.Bind<string>(
                    section: "Permissions",
                    key: "CanUseTts",
                    defaultValue: "",
                    description: "Comma-separated list of SteamIDs of players that can use TTS.")
                .Value).ToArray();
            var steamIdsThatCanUseInlineCommands = stringToSteamIds(Config.Bind<string>(
                    section: "Permissions",
                    key: "CanUseInlineCommands",
                    defaultValue: "",
                    description: "Comma-separated list of SteamIDs of players that can use inline commands.")
                .Value).ToArray();
            foreach (var steamId in steamIdsThatCanUseTts)
            {
                PermissionConfiguration.CurrentSettings.PermissionsPerSteamId[steamId] = PermissionConfiguration.Permissions.UseTts;
            }
            foreach (var steamId in steamIdsThatCanUseInlineCommands)
            {
                PermissionConfiguration.CurrentSettings.PermissionsPerSteamId[steamId] = PermissionConfiguration.Permissions.UseInlineCommands;
            }

            TTS.Init();
            base.Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
            Harmony.PatchAll();
            base.Logger.LogInfo($"Plugin total patches appled: {Harmony.GetPatchedMethods().Count()}");
        }
        public void OnDestroy()
        {
            //EnableTestMode();
            if (PlayStartingUpMessage)
            {
                TTS.Speak("Starting Up");
            }
        }
        private void EnableTestMode()
        {
            LCModUtils modUtils = new LCModUtils(Harmony);
            modUtils.DisableFullscreen();
            modUtils.BootToLANMenu();
        }
    }
}
