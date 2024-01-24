using RoR2;
using UnityEngine;
using System.Collections.Generic;

namespace BlindItems;

public class Obfuscate{    
  public const string itemName = "???";
  private const string itemDescription = "?????";
  
  public ItemAddressables _addressables;
  List<ItemDef> items = Main.itemNotifications;
  List<EquipmentDef> equips = Main.equipNotifications;

  public Obfuscate(){    
    _addressables = new ItemAddressables();
    Awake(items, equips);
  }

  public void Awake(List<ItemDef> items, List<EquipmentDef> equips){
    ObfuscateItems(items);
    ObfuscateEquipments(equips);
  }

  public void ObfuscateItems(List<ItemDef> items)
  {
    On.RoR2.ItemCatalog.SetItemDefs += (orig, itemDefs) => {
      orig(itemDefs);
      foreach (ItemDef itemDef in itemDefs)
      {
        ItemDef itemCopy = ScriptableObject.CreateInstance<ItemDef>();      
        itemCopy.itemIndex = itemDef.itemIndex;
        itemCopy.pickupIconSprite = itemDef.pickupIconSprite;
        itemCopy.nameToken = itemDef.nameToken;
        itemCopy.pickupToken = itemDef.pickupToken;
        items.Add(itemCopy);

    #pragma warning disable CS0618 // 'ItemDef.deprecatedTier' is incorrectly marked as obsolete
        switch (itemDef.deprecatedTier)
    #pragma warning restore CS0618
        {
          case ItemTier.Tier1:
            itemDef.pickupIconSprite = _addressables.whiteSprite;
            break;
          case ItemTier.Tier2:
            itemDef.pickupIconSprite = _addressables.greenSprite;
            break;
          case ItemTier.Tier3:
            itemDef.pickupIconSprite = _addressables.redSprite;
            break;
          case ItemTier.Boss:
            itemDef.pickupIconSprite = _addressables.bossSprite;
            break;
          case ItemTier.Lunar:
            itemDef.pickupIconSprite = _addressables.lunarSprite;
            break;
          case ItemTier.VoidTier1:
            itemDef.pickupIconSprite = _addressables.voidWhiteSprite;
            break;
          case ItemTier.VoidTier2:
            itemDef.pickupIconSprite = _addressables.voidGreenSprite;
            break;
          case ItemTier.VoidTier3:
            itemDef.pickupIconSprite = _addressables.voidRedSprite;
            break;
          case ItemTier.VoidBoss:
            itemDef.pickupIconSprite = _addressables.voidBossSprite;
            break;
        }

        itemDef.pickupModelPrefab = PickupCatalog.GetHiddenPickupDisplayPrefab();
        itemDef.nameToken = itemName;
        itemDef.pickupToken = itemDescription;
      }
    };
  }

  public void ObfuscateEquipments(List<EquipmentDef> equips)
  {
    On.RoR2.EquipmentCatalog.SetEquipmentDefs += (orig, equipDefs) => {
      orig(equipDefs);
      foreach (EquipmentDef equipDef in equipDefs)
      {
        EquipmentDef equipCopy = ScriptableObject.CreateInstance<EquipmentDef>();      
        equipCopy.equipmentIndex = equipDef.equipmentIndex;
        equipCopy.pickupIconSprite = equipDef.pickupIconSprite;
        equipCopy.nameToken = equipDef.nameToken;
        equipCopy.pickupToken = equipDef.pickupToken;
        equips.Add(equipCopy);

        if (Main.ObscureEquipment.Value){
          equipDef.pickupIconSprite = _addressables.equipSprite;
        }
        
        equipDef.pickupModelPrefab = PickupCatalog.GetHiddenPickupDisplayPrefab();
        equipDef.nameToken = itemName;
        equipDef.pickupToken = itemDescription;
        equipDef.descriptionToken = itemDescription;
      }
    };
  }
}