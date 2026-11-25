// Standalone checks for biomes: every tracked item belongs to at least one biome, and the panel shows only the current biome's items.
using System;
using System.Linq;
using HexResourceTracker.Core;
public class Heightmap { [Flags] public enum Biome { None = 0, Meadows = 1, Swamp = 2, Mountain = 4, BlackForest = 8, Plains = 16, AshLands = 32, DeepNorth = 64, Ocean = 256, Mistlands = 512 } }
public class Room { public enum Theme { ForestCrypt, SunkenCrypt, Cave, DvergerTown, MorkHalla, Hole } }
public class Minimap { public enum PinType { Icon2, Icon3 } }
namespace HexResourceTracker.Core { internal static class TarPitResourcePinService { internal const string ResourcePrefabName="TarLiquid"; } }
static class Checks {
 static int failures;
 static void Check(bool ok,string name) { Console.WriteLine((ok?"PASS ":"FAIL ")+name); if(!ok) failures++; }
 static int Main() {
  Check(TrackedResources.AllTrackedResources.All(r=>TrackedBiomes.ForResource(r)!=Heightmap.Biome.None),"every resource has a biome");
  Check(TrackedDungeons.AllTrackedDungeons.All(d=>TrackedBiomes.ForDungeon(d.Theme)!=Heightmap.Biome.None),"every dungeon has a biome");
  Check(TrackedCreatures.AllTrackedCreatures.All(c=>TrackedBiomes.ForCreature(c.PrefabName)!=Heightmap.Biome.None),"every creature has a biome");
  Check(TrackedLocations.All.Keys.All(p=>TrackedBiomes.ForLocations(new[] { p })!=Heightmap.Biome.None),"every location has a biome");

  var turnip=TrackedResources.AllTrackedResources.Single(r=>r.ResourcePrefabName=="Pickable_SeedTurnip");
  Check(TrackedBiomes.ForResource(turnip)==Heightmap.Biome.Swamp,"a pickable's biome comes from its biome name");
  var copper=TrackedResources.AllTrackedResources.Single(r=>r.ResourcePrefabName=="rock4_copper");
  Check(TrackedBiomes.ForResource(copper)==Heightmap.Biome.BlackForest,"a deposit's biome comes from the table");

  var trader=TrackedLocations.Groups().Single(g=>g.Key=="Trader").Value;
  Check(TrackedBiomes.ForLocations(trader)==(Heightmap.Biome.Meadows|Heightmap.Biome.BlackForest|Heightmap.Biome.Swamp),"a shared toggle covers every biome its locations are in");
  var altar=TrackedBiomes.ForLocations(TrackedLocations.Groups().Single(g=>g.Key=="Boss Altar").Value);
  Check(TrackedBiomes.IsShown(altar,Heightmap.Biome.Meadows)&&TrackedBiomes.IsShown(altar,Heightmap.Biome.AshLands),"boss altars show in each boss's biome");

  var deer=TrackedBiomes.ForCreature("Deer");
  Check(TrackedBiomes.IsShown(deer,Heightmap.Biome.Meadows)&&TrackedBiomes.IsShown(deer,Heightmap.Biome.BlackForest),"an item in several biomes shows in each");
  Check(!TrackedBiomes.IsShown(deer,Heightmap.Biome.Swamp),"an item is hidden outside its biomes");
  Check(!TrackedBiomes.IsShown(deer,Heightmap.Biome.None),"nothing shows before the biome is known");

  var everything=TrackedResources.AllTrackedResources.Select(TrackedBiomes.ForResource)
   .Concat(TrackedDungeons.AllTrackedDungeons.Select(d=>TrackedBiomes.ForDungeon(d.Theme)))
   .Concat(TrackedCreatures.AllTrackedCreatures.Select(c=>TrackedBiomes.ForCreature(c.PrefabName)))
   .Concat(TrackedLocations.All.Keys.Select(p=>TrackedBiomes.ForLocations(new[] { p })));
  Check(everything.All(b=>!TrackedBiomes.IsShown(b,Heightmap.Biome.Ocean)),"nothing is tracked on the Ocean yet");

  Check(TrackedBiomes.IsShown(TrackedBiomes.MemorialSiteSearch,Heightmap.Biome.DeepNorth),"the memorial site search shows in the Deep North");
  Check(TrackedBiomes.DisplayName(Heightmap.Biome.BlackForest)=="Black Forest"&&TrackedBiomes.DisplayName(Heightmap.Biome.Mountain)=="Mountains"
   &&TrackedBiomes.DisplayName(Heightmap.Biome.AshLands)=="Ashlands"&&TrackedBiomes.DisplayName(Heightmap.Biome.DeepNorth)=="Deep North"
   &&TrackedBiomes.DisplayName(Heightmap.Biome.Ocean)=="Ocean","biome display names");
  Check(TrackedBiomes.DisplayName(Heightmap.Biome.None)=="","no name before the biome is known");
  Console.WriteLine(failures==0?"All biome checks passed.":failures+" check(s) failed.");
  return failures==0?0:1;
 }
}
