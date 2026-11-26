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
  var thistle=TrackedBiomes.ForResource(TrackedResources.AllTrackedResources.Single(r=>r.ResourcePrefabName=="Pickable_Thistle"));
  Check(TrackedBiomes.IsShown(thistle,Heightmap.Biome.BlackForest)&&TrackedBiomes.IsShown(thistle,Heightmap.Biome.Swamp),"Thistle shows in the Black Forest and the Swamp");
  var mushrooms=TrackedBiomes.ForResource(TrackedResources.AllTrackedResources.Single(r=>r.ResourcePrefabName=="Pickable_Mushroom"));
  Check(TrackedBiomes.IsShown(mushrooms,Heightmap.Biome.Meadows)&&TrackedBiomes.IsShown(mushrooms,Heightmap.Biome.Swamp),"Mushrooms show in the Meadows and the Swamp");
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

  Check(TrackedBiomes.IsShown(TrackedBiomes.ForCreature("Serpent"),Heightmap.Biome.Ocean),"the Serpent shows on the Ocean");
  var leviathan=TrackedResources.AllTrackedResources.Single(r=>r.ResourcePrefabName=="Leviathan");
  Check(TrackedBiomes.IsShown(TrackedBiomes.ForResource(leviathan),Heightmap.Biome.Ocean),"the Leviathan shows on the Ocean");
  var bonemaw=TrackedBiomes.ForCreature("BonemawSerpent");
  Check(TrackedBiomes.IsShown(bonemaw,Heightmap.Biome.AshLands)&&TrackedBiomes.IsShown(bonemaw,Heightmap.Biome.Ocean),"the Bonemaw shows in the Ashlands and on the Ocean");

  Check(Enumerable.Range(1,12).All(n=>TrackedCreatures.TryGetDefinition(n==4?"Fish4_cave":"Fish"+n,out _)),"all twelve fish are tracked");
  var puffer=TrackedBiomes.ForCreature("Fish12");
  Check(TrackedBiomes.IsShown(puffer,Heightmap.Biome.Ocean)&&TrackedBiomes.IsShown(puffer,Heightmap.Biome.Mistlands),"the Pufferfish shows on the Ocean and in the Mistlands");
  Check(TrackedBiomes.ForCreature("Fish4_cave")==Heightmap.Biome.Mountain,"the Tetra shows in the Mountains (Frost Cave lakes)");
  Check(TrackedCreatures.TryGetDefinition("Draugr_Ranged_sleeping",out var archer)&&archer.PrefabName=="Draugr","a sleeping Draugr archer counts as a Draugr");
  Check(TrackedCreatures.TryGetDefinition("Greydwarf_Frozen",out var frozen)&&frozen.PrefabName=="Greydwarf"
   &&TrackedBiomes.IsShown(TrackedBiomes.ForCreature("Greydwarf"),Heightmap.Biome.DeepNorth),"a frozen Greydwarf counts as a Greydwarf, in the Deep North too");
  Check(TrackedCreatures.TryGetDefinition("JotunWarriorDualWield",out var krigen)&&krigen.DisplayName=="Krigen","both Krigen share one toggle");
  Check(TrackedCreatures.TryGetDefinition("Seagal",out var gull)&&TrackedBiomes.IsShown(TrackedBiomes.ForCreature("Seagal"),Heightmap.Biome.Ocean),"gulls are tracked, on the Ocean too");
  string[] authorCreatures={"Deer","Boar","Hare","Lox"};
  Check(TrackedCreatures.AllTrackedCreatures.Where(c=>authorCreatures.Contains(c.PrefabName)).All(c=>c.EnabledByDefault),"the author's creatures stay on by default");
  Check(TrackedCreatures.AllTrackedCreatures.Where(c=>!authorCreatures.Contains(c.PrefabName)).All(c=>!c.EnabledByDefault),"every new creature is off by default");
  Check(TrackedCreatures.TryGetDefinition("Bjorn_sleeping",out var sleepingBear)&&sleepingBear.PrefabName=="Bjorn","a sleeping bear counts as a bear");
  Check(TrackedCreatures.TryGetDefinition("Morgen_NonSleeping",out var wakingMorgen)&&wakingMorgen.PrefabName=="Morgen","a waking Morgen counts as a Morgen");
  Check(TrackedCreatures.AllTrackedCreatures.Select(c=>c.PrefabName).Distinct().Count()==TrackedCreatures.AllTrackedCreatures.Count,"no creature is listed twice");

  var near=new[]{ new float[]{0,0}, new float[]{10,0}, new float[]{25,5}, new float[]{200,200} };
  var packs=CreaturePacks.Group(near,p=>p[0],p=>p[1],30f);
  Check(packs.Count==2&&packs[0].Count==3&&packs[1].Count==1,"creatures close together share one pack, a far one stands alone");
  Check(CreaturePacks.Group(near,p=>p[0],p=>p[1],0f).Count==4,"a radius of zero leaves every creature on its own");
  Check(CreaturePacks.Group(new float[0][],p=>p[0],p=>p[1],30f).Count==0,"no creatures, no packs");

  Check(TrackedBiomes.IsShown(TrackedBiomes.MemorialSiteSearch,Heightmap.Biome.DeepNorth),"the memorial site search shows in the Deep North");
  Check(TrackedBiomes.DisplayName(Heightmap.Biome.BlackForest)=="Black Forest"&&TrackedBiomes.DisplayName(Heightmap.Biome.Mountain)=="Mountains"
   &&TrackedBiomes.DisplayName(Heightmap.Biome.AshLands)=="Ashlands"&&TrackedBiomes.DisplayName(Heightmap.Biome.DeepNorth)=="Deep North"
   &&TrackedBiomes.DisplayName(Heightmap.Biome.Ocean)=="Ocean","biome display names");
  Check(TrackedBiomes.DisplayName(Heightmap.Biome.None)=="","no name before the biome is known");
  Console.WriteLine(failures==0?"All biome checks passed.":failures+" check(s) failed.");
  return failures==0?0:1;
 }
}
