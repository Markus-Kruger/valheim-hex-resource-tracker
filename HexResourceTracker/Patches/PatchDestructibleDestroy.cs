using HarmonyLib;
using HexResourceTracker.Core;

namespace HexResourceTracker.Patches
{
    [HarmonyPatch(typeof(Destructible), nameof(Destructible.Destroy))]
    internal static class PatchDestructibleDestroy
    {
        private static void Prefix(Destructible __instance)
        {
            if (!PluginConfig.IsModEnabled.Value || __instance == null)
            {
                return;
            }

            LogMudpileDiagnostic(__instance);
            OreResourcePinService.TryRemoveResourcePinFromDestructibleOre(__instance);
        }

        // Temporary diagnostic (Markus Kruger, ticket 02): names what a muddy scrap pile turns into when hit.
        private static void LogMudpileDiagnostic(Destructible destructible)
        {
            string prefabName = Utils.GetPrefabName(destructible.gameObject);
            if (prefabName.IndexOf("mudpile", System.StringComparison.OrdinalIgnoreCase) < 0)
            {
                return;
            }

            UnityEngine.GameObject spawn = destructible.m_spawnWhenDestroyed;
            if (spawn == null)
            {
                Plugin.Log.LogInfo($"[MudpileDiag] {prefabName} destroyed, spawns nothing.");
                return;
            }

            MineRock5 mineRock5 = spawn.GetComponent<MineRock5>();
            MineRock mineRock = spawn.GetComponent<MineRock>();
            Destructible nextDestructible = spawn.GetComponent<Destructible>();
            Plugin.Log.LogInfo(
                $"[MudpileDiag] {prefabName} destroyed, spawns '{spawn.name}'" +
                $" MineRock5.m_name='{mineRock5?.m_name}' MineRock.m_name='{mineRock?.m_name}'" +
                $" Destructible='{(nextDestructible != null ? spawn.name : string.Empty)}'.");
        }
    }
}