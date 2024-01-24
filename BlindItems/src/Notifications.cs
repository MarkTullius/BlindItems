using RoR2;
using UnityEngine;
using System.Collections.Generic;

namespace BlindItems;

public class Notifications{
  List<ItemDef> items = Main.itemNotifications;
  List<EquipmentDef> equips = Main.equipNotifications;

  public Notifications(){
    Awake(items, equips);
  }

  public void Awake(List<ItemDef> items, List<EquipmentDef> equips){
    PushItemNotification(items);
    PushEquipmentNotification(equips);
  }
  public void PushItemNotification(List<ItemDef> items)
  {
    On.RoR2.CharacterMasterNotificationQueue.PushItemNotification += (orig, characterMaster, itemIndex) => {
      orig(characterMaster, ItemIndex.None);
      ItemDef itemDef = items.Find(itemDef => itemDef.itemIndex == itemIndex);
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
    };
  }

  public void PushEquipmentNotification(List<EquipmentDef> equips)
  {
    On.RoR2.CharacterMasterNotificationQueue.PushEquipmentNotification += (orig, characterMaster, equipmentIndex) => {
      orig(characterMaster, EquipmentIndex.None);
      EquipmentDef equipDef = equips.Find(equipDef => equipDef.equipmentIndex == equipmentIndex);
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
    };
  }
}