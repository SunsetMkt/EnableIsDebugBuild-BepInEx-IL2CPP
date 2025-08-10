using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using UnityEngine;

namespace EnableIsDebugBuild
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    public class Plugin : BasePlugin
    {
        internal static new ManualLogSource Log;

        public override void Load()
        {
            Log = base.Log;
            Log.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");

            // 初始化 Harmony
            Harmony harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);
            harmony.PatchAll();

            // 立刻测试 Debug.isDebugBuild
            Log.LogInfo($"[Test] Debug.isDebugBuild = {Debug.isDebugBuild}");
        }
    }

    // Patch Debug.isDebugBuild getter
    [HarmonyPatch(typeof(Debug), nameof(Debug.isDebugBuild), MethodType.Getter)]
    public static class DebugIsDebugBuildPatch
    {
        public static bool Prefix(ref bool __result)
        {
            __result = true; // 强制返回 true
            return false;    // 跳过原始 getter
        }
    }
}
