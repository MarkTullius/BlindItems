using System.Collections.Generic;
using RoR2;

namespace BlindItems;

public class Randomise
{
  public Randomise()
  {
    On.RoR2.Stage.Start += ShuffleItemOrder;
  }

  // If RandomiseOrder bool is set to true then we want to shuffle the order of the player's inventory at the start of each stage
  public void ShuffleItemOrder(On.RoR2.Stage.orig_Start orig, Stage self)
  {
    orig(self);
    if (Main.RandomiseOrder.Value)
    {
      List<ItemIndex> itemOrder = PlayerCharacterMasterController._instances[0].master.inventory.itemAcquisitionOrder;
      Util.ShuffleList(itemOrder);
      PlayerCharacterMasterController._instances[0].master.inventory.itemAcquisitionOrder = itemOrder;
    }
  }
}