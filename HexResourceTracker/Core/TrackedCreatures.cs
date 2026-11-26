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
                new TrackedCreatureDefinitionModel("Serpent", "Serpent", "TrophySerpent", 90, enabledByDefault: false)
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