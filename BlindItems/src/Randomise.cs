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

    List<ItemIndex> itemOrder = PlayerCharacterMasterController._instances[0].master.inventory.itemAcquisitionOrder;
    Random random = new Random();
    int n = itemOrder.Count;

    for (int i = n - 1; i > 0; i--)
    {
      int randIndex = random.Next(0, i + 1);

      ItemIndex temp = itemOrder[i];
      itemOrder[i] = itemOrder[randIndex];
      itemOrder[randIndex] = temp;
    }
    
    PlayerCharacterMasterController._instances[0].master.inventory.itemAcquisitionOrder = itemOrder;
  }
}