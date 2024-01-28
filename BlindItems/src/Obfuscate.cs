using RoR2;
using UnityEngine;
using System.Collections.Generic;

namespace BlindItems;

public class Obfuscate
{
  public const string itemName = "???";
  private const string itemDescription = "?????";
  
  private readonly ItemAddressables _addressables;

  public Obfuscate(List<ItemDef> itemNotifications, List<EquipmentDef> equipNotifications)
  {    
    _addressables = new ItemAddressables();
    Awake(itemNotifications, equipNotifications);
  }

  public void Awake(List<ItemDef> itemNotifications, List<EquipmentDef> equipNotifications)
  {
    ObfuscateItems(itemNotifications);
    ObfuscateEquipments(equipNotifications);
  }

  public void ObfuscateItems(List<ItemDef> itemNotifications)
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
          itemCopy.hidden = itemDef.hidden;
          itemNotifications.Add(itemCopy);
#pragma warning disable CS0618 // 'ItemDef.deprecatedTier' is incorrectly marked as obsolete
        if (itemDef.deprecatedTier != ItemTier.NoTier){
          itemDef.pickupModelPrefab = PickupCatalog.GetHiddenPickupDisplayPrefab();
          itemDef.nameToken = itemName;
          itemDef.pickupToken = itemDescription;
          itemDef.pickupIconSprite = GetPickupIconSprite(itemDef.deprecatedTier);
        }
#pragma warning restore CS0618
      }
    };
  }

  public void ObfuscateEquipments(List<EquipmentDef> equipNotifications)
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
        equipNotifications.Add(equipCopy);        
        equipDef.pickupModelPrefab = PickupCatalog.GetHiddenPickupDisplayPrefab();
        equipDef.nameToken = itemName;
        equipDef.pickupToken = itemDescription;
        equipDef.descriptionToken = itemDescription;
        // If ObscureEquipment bool is set to false then we don't want to overwrite the Equip icon
        if (Main.ObscureEquipment.Value)
        {
          equipDef.pickupIconSprite = _addressables.equipSprite;
        }
      }
    };
  }

  public Sprite GetPickupIconSprite(ItemTier tier)
  {
    switch (tier)
    {
      case ItemTier.Tier1:
        return _addressables.whiteSprite;
      case ItemTier.Tier2:
        return  _addressables.greenSprite;
      case ItemTier.Tier3:
        return  _addressables.redSprite;
      case ItemTier.Boss:
        return  _addressables.bossSprite;
      case ItemTier.Lunar:
        return  _addressables.lunarSprite;
      case ItemTier.VoidTier1:
        return  _addressables.voidWhiteSprite;
      case ItemTier.VoidTier2:
        return  _addressables.voidGreenSprite;
      case ItemTier.VoidTier3:
        return  _addressables.voidRedSprite;
      case ItemTier.VoidBoss:
        return  _addressables.voidBossSprite;
    }
    return null;
  }
}