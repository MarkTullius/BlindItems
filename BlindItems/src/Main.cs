using BepInEx;
using RoR2;
using UnityEngine;
using System.Collections.Generic;

namespace BlindItems;
[BepInPlugin(PluginGUID, PluginName, PluginVersion)]

public class Main : BaseUnityPlugin{
  public const string PluginGUID = PluginAuthor + "." + PluginName;
  public const string PluginAuthor = "MarkTullius";
  public const string PluginName = "BlindItems";
  public const string PluginVersion = "0.3.0";
  
  public const string itemName = "???";
  private const string itemDescription = "?????";
  public ItemAddressables _addressables;
  public List<ItemDef> itemNotifications;
  public List<EquipmentDef> equipNotifications;

  public void Awake(){
    _addressables = new ItemAddressables();
    itemNotifications = new List<ItemDef>();
    equipNotifications = new List<EquipmentDef>();
    On.RoR2.ItemCatalog.SetItemDefs += ObfuscateItems;
    On.RoR2.EquipmentCatalog.SetEquipmentDefs += ObfuscateEquipments;
    On.RoR2.CharacterMasterNotificationQueue.PushItemNotification += PushItemNotification;
    On.RoR2.CharacterMasterNotificationQueue.PushEquipmentNotification += PushEquipmentNotification;
  }

  public void ObfuscateItems(On.RoR2.ItemCatalog.orig_SetItemDefs orig, ItemDef[] itemDefs)
  {
    orig(itemDefs);
    foreach (ItemDef itemDef in itemDefs)
    {
      itemNotifications.Add(new ItemDef
      {
        itemIndex = itemDef.itemIndex,
        pickupIconSprite = itemDef.pickupIconSprite,
        nameToken = itemDef.nameToken,
        pickupToken = itemDef.pickupToken,
        descriptionToken = itemDef.descriptionToken
      });

  #pragma warning disable CS0618 // 'ItemDef.deprecatedTier' is incorrectly marked as obsolete
      switch (itemDef.deprecatedTier)
  #pragma warning restore CS0618
      {
        case ItemTier.Tier1:
          itemDef.pickupIconSprite = _addressables.whiteSprite;
          itemDef.pickupModelPrefab = _addressables.scrapPrefab;
          break;
        case ItemTier.Tier2:
          itemDef.pickupIconSprite = _addressables.greenSprite;
          itemDef.pickupModelPrefab = _addressables.scrapPrefab;
          break;
        case ItemTier.Tier3:
          itemDef.pickupIconSprite = _addressables.redSprite;
          itemDef.pickupModelPrefab = _addressables.scrapPrefab;
          break;
        case ItemTier.Boss:
          itemDef.pickupIconSprite = _addressables.bossSprite;
          itemDef.pickupModelPrefab = _addressables.scrapPrefab;
          break;
        case ItemTier.Lunar:
          itemDef.pickupIconSprite = _addressables.whiteSprite;
          itemDef.pickupModelPrefab = _addressables.scrapPrefab;
          break;
        case ItemTier.VoidTier1:
          itemDef.pickupIconSprite = _addressables.voidWhiteSprite;
          itemDef.pickupModelPrefab = _addressables.voidWhitePrefab;
          break;
        case ItemTier.VoidTier2:
          itemDef.pickupIconSprite = _addressables.voidGreenSprite;
          itemDef.pickupModelPrefab = _addressables.voidGreenPrefab;
          break;
        case ItemTier.VoidTier3:
          itemDef.pickupIconSprite = _addressables.voidRedSprite;
          itemDef.pickupModelPrefab = _addressables.voidRedPrefab;
          break;
        case ItemTier.VoidBoss:
          // itemDef.pickupIconSprite = _addressables.voidRedSprite;
          itemDef.pickupModelPrefab = _addressables.voidBossPrefab;
          break;
      }

      itemDef.nameToken = itemName;
      itemDef.pickupToken = itemDescription;
      itemDef.descriptionToken = itemDescription;
    }
  }

  public void ObfuscateEquipments(On.RoR2.EquipmentCatalog.orig_SetEquipmentDefs orig, EquipmentDef[] equipDefs)
  {
    orig(equipDefs);
    foreach (EquipmentDef equipDef in equipDefs)
    {
      equipNotifications.Add(new EquipmentDef
      {
        equipmentIndex = equipDef.equipmentIndex,
        pickupIconSprite = equipDef.pickupIconSprite,
        nameToken = equipDef.nameToken,
        pickupToken = equipDef.pickupToken,
        descriptionToken = equipDef.descriptionToken
      });

      equipDef.pickupIconSprite = _addressables.whiteSprite;
      equipDef.pickupModelPrefab = _addressables.scrapPrefab;
      equipDef.nameToken = itemName;
      equipDef.pickupToken = itemDescription;
      equipDef.descriptionToken = itemDescription;
    }
  }

  public void PushItemNotification(On.RoR2.CharacterMasterNotificationQueue.orig_PushItemNotification orig, CharacterMaster characterMaster, ItemIndex itemIndex)
  {
    orig(characterMaster, ItemIndex.None);
    ItemDef itemDef = itemNotifications.Find(itemDef => itemDef.itemIndex == itemIndex);
    if (!characterMaster.hasAuthority){
      Debug.LogError("Can't PushItemNotification for " + Util.GetBestMasterName(characterMaster) + " because they aren't local.");
      return;
    }
    CharacterMasterNotificationQueue notificationQueueForMaster = CharacterMasterNotificationQueue.GetNotificationQueueForMaster(characterMaster);
    if (notificationQueueForMaster && itemIndex != ItemIndex.None){
      if (itemDef == null || itemDef.hidden){
        return;
      }
      float duration = 6f;
      notificationQueueForMaster.PushNotification(new CharacterMasterNotificationQueue.NotificationInfo(itemDef, null), duration);
    }
  }

  public void PushEquipmentNotification(On.RoR2.CharacterMasterNotificationQueue.orig_PushEquipmentNotification orig, CharacterMaster characterMaster, EquipmentIndex equipmentIndex)
  {
    orig(characterMaster, EquipmentIndex.None);
    EquipmentDef equipDef = equipNotifications.Find(equipDef => equipDef.equipmentIndex == equipmentIndex);
    if (!characterMaster.hasAuthority){
      Debug.LogError("Can't PushItemNotification for " + Util.GetBestMasterName(characterMaster) + " because they aren't local.");
      return;
    }
    CharacterMasterNotificationQueue notificationQueueForMaster = CharacterMasterNotificationQueue.GetNotificationQueueForMaster(characterMaster);
    if (notificationQueueForMaster && equipmentIndex != EquipmentIndex.None){
      if (equipDef == null){
        return;
      }
      float duration = 6f;
      notificationQueueForMaster.PushNotification(new CharacterMasterNotificationQueue.NotificationInfo(equipDef, null), duration);
    }
  }
}