using BepInEx;
using BepInEx.Bootstrap;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using WebSocketSharp.Server;

namespace LurkBaitAPIMod;

[BepInPlugin(modGUID, modName, modVersion)]
public class Plugin : BaseUnityPlugin
{
    private const string modGUID = "Landie.LurkBaitAPIMod";
    private const string modName = "LurkBaitAPIMod";
    private const string modVersion = "1.0.0";

    private ConfigEntry<int> c_Port;

    private readonly Harmony harmony = new Harmony(modGUID);

    private static Plugin Instance;

    internal ManualLogSource mls;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        c_Port = Config.Bind("General", "Port", 8592, "The port the websocket server will be hosted on!");

        Data.Port = c_Port.Value;

        mls = BepInEx.Logging.Logger.CreateLogSource(modGUID);

        mls.LogInfo($"{modName} is loaded.    vers.{modVersion}");

        Data.Init(Logger); //important this is initialized first as it has the websocket server

        harmony.PatchAll(typeof(Patches.TwitchConnectorPatches));
    }

    void OnDestroy()
    {
        mls.LogInfo("Destroying websocket server");
        Data.wss.Stop();
    }
}
