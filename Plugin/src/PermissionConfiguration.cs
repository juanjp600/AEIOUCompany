using System;
using System.Collections.Generic;

namespace AEIOU_Company;

static class PermissionConfiguration
{
    public enum Permissions
    {
        None = 0,
        UseTts = 1 << 0,
        UseInlineCommands = UseTts | (1 << 1)
    }

    public struct Settings
    {
        public Permissions DefaultPermissions;
        public readonly Dictionary<ulong, Permissions> PermissionsPerSteamId;

        public Settings(Permissions defaultPermissions, Dictionary<ulong, Permissions> permissionsPerSteamId)
        {
            DefaultPermissions = defaultPermissions;
            PermissionsPerSteamId = permissionsPerSteamId;
        }

        public static readonly Settings Default = new Settings(
            defaultPermissions: Permissions.None,
            permissionsPerSteamId: new Dictionary<ulong, Permissions>());
    }

    public static Settings CurrentSettings = Settings.Default;
}