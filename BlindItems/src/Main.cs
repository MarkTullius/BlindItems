using BepInEx;
using BlindItems;
using RoR2;
using UnityEngine;

namespace ModTemplate;
[BepInPlugin(PluginGUID, PluginName, PluginVersion)]

public class Main : BaseUnityPlugin{
  public const string PluginGUID = PluginAuthor + "." + PluginName;
  public const string PluginAuthor = "MarkTullius";
  public const string PluginName = "BlindItems";
  public const string PluginVersion = "0.1.1";
  
  public const string itemName = "???";
  private const string itemDescription = "?????";
  public ItemAddressables _addressables;
  public NotificationInfo _notificationInfo;

  public void Awake(){
    _addressables = new ItemAddressables();
    _notificationInfo = new NotificationInfo();
    On.RoR2.ItemCatalog.SetItemDefs += ObfuscateItems;
  }

  public void ObfuscateItems(On.RoR2.ItemCatalog.orig_SetItemDefs orig, ItemDef[] itemDefs)
  {
    orig(itemDefs);
    foreach (ItemDef itemDef in itemDefs)
    {
      _notificationInfo.ItemInfos.Add(new ItemInfo{
        itemIndex = itemDef.itemIndex,
        icon = itemDef.pickupIconSprite,
        name = itemDef.nameToken,
        pickupToken = itemDef.pickupToken,
        description = itemDef.descriptionToken
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
          itemDef.pickupIconSprite = _addressables.voidRedSprite;
          itemDef.pickupModelPrefab = _addressables.voidBossPrefab;
          break;
      }

      itemDef.nameToken = itemName;
      itemDef.pickupToken = itemDescription;
      itemDef.descriptionToken = itemDescription;
    }
  }
}