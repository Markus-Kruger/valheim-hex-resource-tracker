using HarmonyLib;
using HexResourceTracker.Core;
using HexResourceTracker.Core.Tracking;
using HexResourceTracker.Core.Tracking.Monoscripts;
using System.Collections.Generic;

namespace HexResourceTracker.Patches
{
    [HarmonyPatch(typeof(Container), nameof(Container.Awake))]
    internal static class PatchContainerAwake
    {
        private static void Postfix(Container __instance)
        {
            if (!PluginConfig.IsModEnabled.Value || __instance == null)
            {
                return;
            }

            string prefabName = __instance.gameObject.name.Replace("(Clone)", string.Empty).Trim();

            if (!TrackedResources.IsWorldContainer(
                    __instance.GetComponent<Piece>() != null,
                    __instance.GetComponent<TombStone>() != null))
            {
                return;
            }

            TrackedMapObject.TryAdd(__instance.gameObject, prefabName);

            if (PluginConfig.TrackingMode.Value != TrackingModeEnum.ZoneBased)
            {
                return;
            }

            if (__instance.gameObject.TryGetComponent(out TrackedMapObject trackedObject))
            {
                ContainerResourcePinService.ReconcileTrackedContainer(trackedObject);
            }
        }
    }
}