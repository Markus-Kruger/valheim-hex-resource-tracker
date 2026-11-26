using System.Collections.Generic;

namespace HexResourceTracker.Core
{
    internal static class TrackedLocations
    {
        // Prefab name to label. Prefabs that share a label share one toggle, one config entry and one label setting.
        internal static readonly Dictionary<string, string> All = new Dictionary<string, string>
        {
            { "NorthMemorialPlace", "Memorial Site" },
            { "CharredFortress", "Charred Fortress" },

            { "TrollCave02", "Troll Cave" },

            { "GoblinCamp2", "Fuling Village" },
            { "GoblinCamp2_1", "Fuling Village" },

            { "Vendor_BlackForest", "Trader" },
            { "Hildir_camp", "Trader" },
            { "BogWitch_Camp", "Trader" },

            { "Eikthyrnir", "Boss Altar" },
            { "GDKing", "Boss Altar" },
            { "Bonemass", "Boss Altar" },
            { "Dragonqueen", "Boss Altar" },
            { "GoblinKing", "Boss Altar" },
            { "Mistlands_DvergrBossEntrance1", "Boss Altar" },
            { "FaderLocation", "Boss Altar" },

            { "Mistlands_GuardTower1_new", "Active Dvergr" },
            { "Mistlands_GuardTower2_new", "Active Dvergr" },
            { "Mistlands_GuardTower3_new", "Active Dvergr" },
            { "Mistlands_Lighthouse1_new", "Active Dvergr" },
            { "Mistlands_Harbour1", "Active Dvergr" },
            { "Mistlands_Excavation1", "Active Dvergr" },
            { "Mistlands_Excavation2", "Active Dvergr" },
            { "Mistlands_Excavation3", "Active Dvergr" },

            { "Mistlands_GuardTower1_ruined_new", "Abandoned Dvergr" },
            { "Mistlands_GuardTower1_ruined_new2", "Abandoned Dvergr" },
            { "Mistlands_GuardTower3_ruined_new", "Abandoned Dvergr" },

            { "Mistlands_DvergrTownEntrance1", "Infested Mine" },
            { "Mistlands_DvergrTownEntrance2", "Infested Mine" },

            { "Hildir_crypt", "Smouldering Tombs" },
            { "Hildir_cave", "Howling Cavern" },
            { "Hildir_plainsfortress", "Sealed Tower" },

            { "BearCave", "Bear Cave" },
            { "FireHole", "Firehole" },

            { "MorgenHole1", "Putrid Hole" },
            { "MorgenHole2", "Putrid Hole" },
            { "MorgenHole3", "Putrid Hole" },

            { "NorthVillage", "Village" }
        };

        // Labels in first-seen order, each with its prefab names.
        internal static List<KeyValuePair<string, List<string>>> Groups()
        {
            var groups = new List<KeyValuePair<string, List<string>>>();
            var byLabel = new Dictionary<string, List<string>>();
            foreach (var location in All)
            {
                if (!byLabel.TryGetValue(location.Value, out List<string> prefabNames))
                {
                    prefabNames = new List<string>();
                    byLabel[location.Value] = prefabNames;
                    groups.Add(new KeyValuePair<string, List<string>>(location.Value, prefabNames));
                }

                prefabNames.Add(location.Key);
            }

            return groups;
        }

        internal static Minimap.PinType GetPinType(string prefabName)
        {
            return prefabName == "CharredFortress" ? Minimap.PinType.Icon2 : Minimap.PinType.Icon3;
        }
    }
}
