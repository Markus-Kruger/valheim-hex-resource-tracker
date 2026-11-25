// Standalone checks for location groups: prefabs sharing a label give one toggle and one setting.
using System;
using System.Collections.Generic;
using System.Linq;
using HexResourceTracker.Core;
public class Minimap { public enum PinType { Icon2, Icon3 } }
static class Checks {
 static int failures;
 static void Check(bool ok,string name) { Console.WriteLine((ok?"PASS ":"FAIL ")+name); if(!ok) failures++; }
 static int Main() {
  var groups=TrackedLocations.Groups();
  var labels=groups.Select(g=>g.Key).ToArray();
  Check(labels.SequenceEqual(new[] { "Memorial Site","Charred Fortress","Troll Cave","Fuling Village","Trader",
   "Boss Altar","Active Dvergr","Abandoned Dvergr","Infested Mine","Smouldering Tombs","Howling Cavern","Sealed Tower" }),"one group per label, in first-seen order");
  Check(labels.Distinct().Count()==labels.Length,"no label appears twice");
  Check(groups.Sum(g=>g.Value.Count)==TrackedLocations.All.Count,"every prefab is in exactly one group");
  var size=groups.ToDictionary(g=>g.Key,g=>g.Value.Count);
  Check(size["Boss Altar"]==7&&size["Active Dvergr"]==8&&size["Abandoned Dvergr"]==3&&size["Infested Mine"]==2
   &&size["Trader"]==3&&size["Fuling Village"]==2&&size["Troll Cave"]==1,"group sizes");
  Check(groups.Single(g=>g.Key=="Infested Mine").Value.SequenceEqual(new[] { "Mistlands_DvergrTownEntrance1","Mistlands_DvergrTownEntrance2" }),"group keeps its prefab names");
  Check(groups.Single(g=>g.Key=="Memorial Site").Value.Single()=="NorthMemorialPlace"
   &&groups.Single(g=>g.Key=="Charred Fortress").Value.Single()=="CharredFortress","existing single locations unchanged");
  Check(groups.Single(g=>g.Key=="Smouldering Tombs").Value.Single()=="Hildir_crypt"
   &&groups.Single(g=>g.Key=="Howling Cavern").Value.Single()=="Hildir_cave"
   &&groups.Single(g=>g.Key=="Sealed Tower").Value.Single()=="Hildir_plainsfortress","Hildir's quest dungeons, one toggle each");
  Check(TrackedLocations.GetPinType("CharredFortress")==Minimap.PinType.Icon2
   &&TrackedLocations.GetPinType("Eikthyrnir")==Minimap.PinType.Icon3,"pin types");
  Console.WriteLine(failures==0?"All location group checks passed.":failures+" check(s) failed.");
  return failures==0?0:1;
 }
}
