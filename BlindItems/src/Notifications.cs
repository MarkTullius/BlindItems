using RoR2;
using UnityEngine;
using System.Collections.Generic;

namespace BlindItems;

public class Notifications
{
  public static List<ItemDef> itemNotifs;
  public static List<EquipmentDef> equipNotifs;
  private const float AddedNotificationDuration = 6f;
  private const float RemovedNotificationDuration = 3f;

  public Notifications(List<ItemDef> itemNotifications, List<EquipmentDef> equipNotifications)
  {
    itemNotifs = itemNotifications;
    equipNotifs = equipNotifications;
    Awake();
  }

  public void Awake()
  {
    PushItemNotification();
    PushItemRemovalNotification();
    PushEquipmentNotification();
  }

  public void PushItemNotification()
  {
    On.RoR2.CharacterMasterNotificationQueue.PushItemNotification += (orig, characterMaster, itemIndex) => {
      orig(characterMaster, ItemIndex.None);

      ItemDef itemDef = GetOrigItemDef(itemIndex);
      CharacterMasterNotificationQueue notificationQueueForMaster = CharacterMasterNotificationQueue.GetNotificationQueueForMaster(characterMaster);

      if (ItemNotificationHandler(characterMaster, itemIndex, itemDef, notificationQueueForMaster))
      {
        float duration = AddedNotificationDuration;
        notificationQueueForMaster.PushNotification(new CharacterMasterNotificationQueue.NotificationInfo(itemDef, null), duration);
      }
    };
  }

  public void PushItemRemovalNotification()
  {
    On.RoR2.PurchaseInteraction.CreateItemTakenOrb += (orig, effectOrigin, targetObject, itemIndex) => {
      orig(effectOrigin, targetObject, itemIndex);
      BuildItemRemovalNotification(itemIndex);
    };
    On.RoR2.ScrapperController.CreateItemTakenOrb += (orig, effectOrigin, targetObject, itemIndex) => {
      orig(effectOrigin, targetObject, itemIndex);
      BuildItemRemovalNotification(itemIndex);
    };
  }

  public void BuildItemRemovalNotification(ItemIndex itemIndex)
  {
    CharacterMaster characterMaster = PlayerCharacterMasterController._instances[0].master;
    ItemDef itemDef = GetOrigItemDef(itemIndex);
    CharacterMasterNotificationQueue notificationQueueForMaster = CharacterMasterNotificationQueue.GetNotificationQueueForMaster(characterMaster);

    if (ItemNotificationHandler(characterMaster, itemIndex, itemDef, notificationQueueForMaster)){
      float duration = RemovedNotificationDuration;
      CharacterMasterNotificationQueue.TransformationInfo transformation = new CharacterMasterNotificationQueue.TransformationInfo(
        CharacterMasterNotificationQueue.TransformationType.Suppressed,
        itemDef
      );
      CharacterMasterNotificationQueue.NotificationInfo info = new CharacterMasterNotificationQueue.NotificationInfo(ItemCatalog.GetItemDef(itemIndex), transformation);
      notificationQueueForMaster.PushNotification(info, duration);
    }
  }

  public void PushEquipmentNotification()
  {
    On.RoR2.CharacterMasterNotificationQueue.PushEquipmentNotification += (orig, characterMaster, equipmentIndex) => {
      orig(characterMaster, EquipmentIndex.None);
      EquipmentDef equipDef = GetOrigEquipDef(equipmentIndex);
      if (!characterMaster.hasAuthority){
        Debug.LogError("Can't PushItemNotification for " + Util.GetBestMasterName(characterMaster) + " because they aren't local.");
        return;
      }
      CharacterMasterNotificationQueue notificationQueueForMaster = CharacterMasterNotificationQueue.GetNotificationQueueForMaster(characterMaster);
      if (notificationQueueForMaster && equipmentIndex != EquipmentIndex.None)
      {
        if (equipDef == null)
        {
          return;
        }
        float duration = AddedNotificationDuration;
        notificationQueueForMaster.PushNotification(new CharacterMasterNotificationQueue.NotificationInfo(equipDef, null), duration);
      }
    };
  }

  public static ItemDef GetOrigItemDef(ItemIndex itemIndex)
  {
    return itemNotifs.Find(itemDef => itemDef.itemIndex == itemIndex);
  }

  public static EquipmentDef GetOrigEquipDef(EquipmentIndex equipmentIndex)
  {
    return equipNotifs.Find(equipDef => equipDef.equipmentIndex == equipmentIndex);
  }

  public bool ItemNotificationHandler(CharacterMaster characterMaster, ItemIndex itemIndex, ItemDef itemDef, CharacterMasterNotificationQueue notificationQueue)
  {
    if (!characterMaster.hasAuthority)
    {
      Debug.LogError("Can't PushItemNotification for " + Util.GetBestMasterName(characterMaster) + " because they aren't local.");
      return false;
    }
    if (notificationQueue && itemIndex != ItemIndex.None)
    {
      if (itemDef == null || itemDef.hidden)
      {
        return false;
      }
    }
    return true;      
  }
}