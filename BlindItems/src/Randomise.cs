using System;
using System.Collections.Generic;
using RoR2;

namespace BlindItems;

public class Randomise{
  public Randomise(){
    On.RoR2.Stage.Start += ShuffleItemOrder;
  }

  public void ShuffleItemOrder(On.RoR2.Stage.orig_Start orig, Stage self){
    orig(self);
    if (Main.RandomiseOrder.Value){
      List<ItemIndex> itemOrder = PlayerCharacterMasterController._instances[0].master.inventory.itemAcquisitionOrder;
      Util.ShuffleList(itemOrder);
      PlayerCharacterMasterController._instances[0].master.inventory.itemAcquisitionOrder = itemOrder;
    }
  }
}