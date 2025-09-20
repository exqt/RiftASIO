using BepInEx;
using BepInEx.Logging;
using UnityEngine;
using HarmonyLib;

namespace RiftASIO;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class Plugin : BaseUnityPlugin
{
    internal static new ManualLogSource Logger;

    public bool IsInitialized { get; private set; } = false;
    internal static Plugin Instance;

    public int bufferLength = 0;

    private void Awake()
    {
        Instance = this;

        Logger = base.Logger;
        Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");

        bufferLength = Config.Bind("Audio", "DSPBufferLength", 0, "Override FMOD DSP buffer length. Set to 0 to use default.").Value;
        Logger.LogInfo($"Configured DSPBufferLength: {bufferLength}");

        var harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);
        harmony.PatchAll();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            Logger.LogInfo("Setting output to ASIO");
            var coreSystem = FMODUnity.RuntimeManager.CoreSystem;
            coreSystem.setOutput(FMOD.OUTPUTTYPE.ASIO);
        }
        else if (Input.GetKeyDown(KeyCode.F6))
        {
            Logger.LogInfo("Setting output to WASAPI");
            var coreSystem = FMODUnity.RuntimeManager.CoreSystem;
            coreSystem.setOutput(FMOD.OUTPUTTYPE.WASAPI);
        }
        else if (Input.GetKeyDown(KeyCode.F7))
        {
            var coreSystem = FMODUnity.RuntimeManager.CoreSystem;
            coreSystem.getDSPBufferSize(out uint bufferLength, out int bufferCount);
            Logger.LogInfo($"Initial DSP Buffer Size: Length={bufferLength}, Count={bufferCount}");
        }
    }
}

// Harmony patch to override DSPBufferLength property
[HarmonyPatch]
public class DSPBufferLengthPatch
{
    // Patch the DSPBufferLength getter in Platform class
    [HarmonyPatch(typeof(FMODUnity.Platform), "DSPBufferLength", MethodType.Getter)]
    [HarmonyPostfix]
    static void OverrideDSPBufferLength(ref int __result)
    {
        int cfg = Plugin.Instance.bufferLength;
        __result = cfg != 0 ? cfg : __result;
    }
}
