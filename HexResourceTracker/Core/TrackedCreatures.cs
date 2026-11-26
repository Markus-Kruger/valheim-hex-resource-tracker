using HexResourceTracker.Models;
using System.Collections.Generic;

namespace HexResourceTracker.Core
{
    internal static class TrackedCreatures
    {
        internal static readonly List<TrackedCreatureDefinitionModel> AllTrackedCreatures =
            new List<TrackedCreatureDefinitionModel>
            {
                new TrackedCreatureDefinitionModel("Deer", "Deer", "TrophyDeer", 0, "HexDeerSprite_Golden_Bold"),
                new TrackedCreatureDefinitionModel("Boar", "Boar", "TrophyBoar", 1),
                new TrackedCreatureDefinitionModel("Hare", "Hare", "TrophyHare", 2),
                new TrackedCreatureDefinitionModel("Lox", "Lox", "TrophyLox", 3),

                // Off by default: players switch on what they hunt.
                new TrackedCreatureDefinitionModel("Neck", "Neck", "TrophyNeck", 10, enabledByDefault: false),
                new TrackedCreatureDefinitionModel("Bjorn", "Bear", "TrophyBjorn", 20, enabledByDefault: false, alsoPrefabNames: new[] { "Bjorn_sleeping" }),
                new TrackedCreatureDefinitionModel("Abomination", "Abomination", "TrophyAbomination", 30, enabledByDefault: false),
                new TrackedCreatureDefinitionModel("Wraith", "Wraith", "TrophyWraith", 31, enabledByDefault: false),
                new TrackedCreatureDefinitionModel("Surtling", "Surtling", "TrophySurtling", 32, enabledByDefault: false),
                new TrackedCreatureDefinitionModel("Wolf", "Wolf", "TrophyWolf", 40, enabledByDefault: false),
                new TrackedCreatureDefinitionModel("Hatchling", "Drake", "TrophyHatchling", 41, enabledByDefault: false),
                new TrackedCreatureDefinitionModel("Deathsquito", "Deathsquito", "TrophyDeathsquito", 50, enabledByDefault: false),
                new TrackedCreatureDefinitionModel("Unbjorn", "Vile", "TrophyBjornUndead", 51, enabledByDefault: false),
                new TrackedCreatureDefinitionModel("Asksvin", "Asksvin", "TrophyAsksvin", 70, enabledByDefault: false),
                new TrackedCreatureDefinitionModel("FallenValkyrie", "Fallen Valkyrie", "TrophyFallenValkyrie", 71, enabledByDefault: false),
                new TrackedCreatureDefinitionModel("Morgen", "Morgen", "TrophyMorgen", 72, enabledByDefault: false, alsoPrefabNames: new[] { "Morgen_NonSleeping" }),
                new TrackedCreatureDefinitionModel("BonemawSerpent", "Bonemaw", "TrophyBonemawSerpent", 73, enabledByDefault: false),
                new TrackedCreatureDefinitionModel("Moose", "Moose", "TrophyMoose", 80, enabledByDefault: false),
                new TrackedCreatureDefinitionModel("Seal", "Seal", "TrophySeal", 81, enabledByDefault: false),
                // The live Gammeltroll has no trophy of its own; it borrows the Troll's.
                new TrackedCreatureDefinitionModel("TrollFrost", "Gammeltroll", "TrophyFrostTroll", 82, enabledByDefault: false),
                new TrackedCreatureDefinitionModel("Serpent", "Serpent", "TrophySerpent", 90, enabledByDefault: false),

                // Fish: each fish is its own item, so its icon is the fish itself.
                new TrackedCreatureDefinitionModel("Fish1", "Perch", "Fish1", 100, enabledByDefault: false),
                new TrackedCreatureDefinitionModel("Fish2", "Pike", "Fish2", 101, enabledByDefault: false),
                new TrackedCreatureDefinitionModel("Fish3", "Tuna", "Fish3", 102, enabledByDefault: false),
                new TrackedCreatureDefinitionModel("Fish4_cave", "Tetra", "Fish4_cave", 103, enabledByDefault: false),
                new TrackedCreatureDefinitionModel("Fish5", "Trollfish", "Fish5", 104, enabledByDefault: false),
                new TrackedCreatureDefinitionModel("Fish6", "Giant Herring", "Fish6", 105, enabledByDefault: false),
                new TrackedCreatureDefinitionModel("Fish7", "Grouper", "Fish7", 106, enabledByDefault: false),
                new TrackedCreatureDefinitionModel("Fish8", "Coral Cod", "Fish8", 107, enabledByDefault: false),
                new TrackedCreatureDefinitionModel("Fish9", "Anglerfish", "Fish9", 108, enabledByDefault: false),
                new TrackedCreatureDefinitionModel("Fish10", "Northern Salmon", "Fish10", 109, enabledByDefault: false),
                new TrackedCreatureDefinitionModel("Fish11", "Magmafish", "Fish11", 110, enabledByDefault: false),
                new TrackedCreatureDefinitionModel("Fish12", "Pufferfish", "Fish12", 111, enabledByDefault: false)
            };

        private static readonly Dictionary<string, TrackedCreatureDefinitionModel> DefinitionsByPrefabName =
            new Dictionary<string, TrackedCreatureDefinitionModel>();

        static TrackedCreatures()
        {
            foreach (var definition in AllTrackedCreatures)
            {
                DefinitionsByPrefabName[definition.PrefabName] = definition;
                DefinitionsByPrefabName[definition.PrefabName + "_0"] = definition;

                foreach (string alsoPrefabName in definition.AlsoPrefabNames)
                {
                    DefinitionsByPrefabName[alsoPrefabName] = definition;
                }
            }
        }

        internal static bool TryGetDefinition(string prefabName, out TrackedCreatureDefinitionModel definition)
        {
            return DefinitionsByPrefabName.TryGetValue(prefabName, out definition);
        }
    }
}