using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace LurkBaitAPIMod.Patches
{
    [HarmonyPatch(typeof(TwitchConnector))]

    internal class TwitchConnectorPatches
    {
        private static ManualLogSource Logger { get; set; }

        public static void Init(ManualLogSource logger)
        {
            Logger = logger;
        }

        [HarmonyPatch(typeof(TwitchConnector), nameof(TwitchConnector.Start))]
        [HarmonyPrefix]
        static bool StartPreFix()
        {
            Logger.LogInfo($"Attempting to stop twitch connector");
            return false;
        }
    }
}
