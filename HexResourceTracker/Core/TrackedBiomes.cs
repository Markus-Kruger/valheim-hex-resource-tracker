using HexResourceTracker.Models;
using System.Collections.Generic;

namespace HexResourceTracker.Core
{
    // Which biomes each tracked item belongs to. The map overlay shows only the items of the player's current biome.
    // An item in several biomes shows in each; a shared toggle covers every biome its prefabs are in.
    internal static class TrackedBiomes
    {
        internal const Heightmap.Biome MemorialSiteSearch = Heightmap.Biome.DeepNorth;

        private static readonly Dictionary<Heightmap.Biome, string> Names = new Dictionary<Heightmap.Biome, string>
        {
            { Heightmap.Biome.Meadows, "Meadows" },
            { Heightmap.Biome.BlackForest, "Black Forest" },
            { Heightmap.Biome.Swamp, "Swamp" },
            { Heightmap.Biome.Mountain, "Mountains" },
            { Heightmap.Biome.Plains, "Plains" },
            { Heightmap.Biome.Mistlands, "Mistlands" },
            { Heightmap.Biome.AshLands, "Ashlands" },
            { Heightmap.Biome.DeepNorth, "Deep North" },
            { Heightmap.Biome.Ocean, "Ocean" }
        };

        // Resources without a biome name (deposits).
        private static readonly Dictionary<string, Heightmap.Biome> Resources = new Dictionary<string, Heightmap.Biome>
        {
            { "Beehive", Heightmap.Biome.Meadows },
            { "rock4_copper", Heightmap.Biome.BlackForest },
            { "MineRock_Tin", Heightmap.Biome.BlackForest },
            { "mudpile", Heightmap.Biome.Swamp },
            { "silvervein", Heightmap.Biome.Mountain },
            { "MineRock_Obsidian", Heightmap.Biome.Mountain },
            { TarPitResourcePinService.ResourcePrefabName, Heightmap.Biome.Plains },
            { "giant_skull", Heightmap.Biome.Mistlands },
            { "LeviathanLava", Heightmap.Biome.AshLands }
        };

        private static readonly Dictionary<Room.Theme, Heightmap.Biome> Dungeons = new Dictionary<Room.Theme, Heightmap.Biome>
        {
            { Room.Theme.ForestCrypt, Heightmap.Biome.BlackForest },
            { Room.Theme.SunkenCrypt, Heightmap.Biome.Swamp },
            { Room.Theme.Cave, Heightmap.Biome.Mountain },
            { Room.Theme.DvergerTown, Heightmap.Biome.Mistlands },
            { Room.Theme.MorkHalla, Heightmap.Biome.DeepNorth },
            { Room.Theme.Hole, Heightmap.Biome.DeepNorth }
        };

        private static readonly Dictionary<string, Heightmap.Biome> Creatures = new Dictionary<string, Heightmap.Biome>
        {
            { "Deer", Heightmap.Biome.Meadows | Heightmap.Biome.BlackForest },
            { "Boar", Heightmap.Biome.Meadows },
            { "Hare", Heightmap.Biome.Mistlands },
            { "Lox", Heightmap.Biome.Plains }
        };

        private static readonly Dictionary<string, Heightmap.Biome> Locations = new Dictionary<string, Heightmap.Biome>
        {
            { "NorthMemorialPlace", Heightmap.Biome.DeepNorth },
            { "CharredFortress", Heightmap.Biome.AshLands },
            { "TrollCave02", Heightmap.Biome.BlackForest },
            { "GoblinCamp2", Heightmap.Biome.Plains },
            { "GoblinCamp2_1", Heightmap.Biome.Plains },
            { "Vendor_BlackForest", Heightmap.Biome.BlackForest },
            { "Hildir_camp", Heightmap.Biome.Meadows },
            { "BogWitch_Camp", Heightmap.Biome.Swamp },
            { "Eikthyrnir", Heightmap.Biome.Meadows },
            { "GDKing", Heightmap.Biome.BlackForest },
            { "Bonemass", Heightmap.Biome.Swamp },
            { "Dragonqueen", Heightmap.Biome.Mountain },
            { "GoblinKing", Heightmap.Biome.Plains },
            { "Mistlands_DvergrBossEntrance1", Heightmap.Biome.Mistlands },
            { "FaderLocation", Heightmap.Biome.AshLands },
            { "Mistlands_GuardTower1_new", Heightmap.Biome.Mistlands },
            { "Mistlands_GuardTower2_new", Heightmap.Biome.Mistlands },
            { "Mistlands_GuardTower3_new", Heightmap.Biome.Mistlands },
            { "Mistlands_Lighthouse1_new", Heightmap.Biome.Mistlands },
            { "Mistlands_Harbour1", Heightmap.Biome.Mistlands },
            { "Mistlands_Excavation1", Heightmap.Biome.Mistlands },
            { "Mistlands_Excavation2", Heightmap.Biome.Mistlands },
            { "Mistlands_Excavation3", Heightmap.Biome.Mistlands },
            { "Mistlands_GuardTower1_ruined_new", Heightmap.Biome.Mistlands },
            { "Mistlands_GuardTower1_ruined_new2", Heightmap.Biome.Mistlands },
            { "Mistlands_GuardTower3_ruined_new", Heightmap.Biome.Mistlands },
            { "Mistlands_DvergrTownEntrance1", Heightmap.Biome.Mistlands },
            { "Mistlands_DvergrTownEntrance2", Heightmap.Biome.Mistlands },
            { "Hildir_crypt", Heightmap.Biome.BlackForest },
            { "Hildir_cave", Heightmap.Biome.Mountain },
            { "Hildir_plainsfortress", Heightmap.Biome.Plains }
        };

        internal static bool IsShown(Heightmap.Biome itemBiomes, Heightmap.Biome currentBiome)
        {
            return (itemBiomes & currentBiome) != Heightmap.Biome.None;
        }

        internal static string DisplayName(Heightmap.Biome biome)
        {
            return Names.TryGetValue(biome, out string name) ? name : string.Empty;
        }

        internal static Heightmap.Biome ForResource(TrackedResourceDefinitionModel resource)
        {
            if (!string.IsNullOrWhiteSpace(resource.BiomeName))
            {
                foreach (var name in Names)
                {
                    if (name.Value == resource.BiomeName)
                    {
                        return name.Key;
                    }
                }
            }

            return Resources.TryGetValue(resource.ResourcePrefabName, out Heightmap.Biome biome) ? biome : Heightmap.Biome.None;
        }

        internal static Heightmap.Biome ForDungeon(Room.Theme theme)
        {
            return Dungeons.TryGetValue(theme, out Heightmap.Biome biome) ? biome : Heightmap.Biome.None;
        }

        internal static Heightmap.Biome ForCreature(string prefabName)
        {
            return Creatures.TryGetValue(prefabName, out Heightmap.Biome biome) ? biome : Heightmap.Biome.None;
        }

        internal static Heightmap.Biome ForLocations(IEnumerable<string> prefabNames)
        {
            Heightmap.Biome biomes = Heightmap.Biome.None;
            foreach (string prefabName in prefabNames)
            {
                if (Locations.TryGetValue(prefabName, out Heightmap.Biome biome))
                {
                    biomes |= biome;
                }
            }

            return biomes;
        }
    }
}
