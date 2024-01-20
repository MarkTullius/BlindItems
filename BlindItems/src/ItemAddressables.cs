using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace BlindItems;

public class ItemAddressables{

  // Inventory icons
  public Sprite whiteSprite = Addressables.LoadAssetAsync<Sprite>("RoR2/Base/Scrap/texScrapWhiteIcon.png").WaitForCompletion();
  public Sprite greenSprite = Addressables.LoadAssetAsync<Sprite>("RoR2/Base/Scrap/texScrapGreenIcon.png").WaitForCompletion();
  public Sprite redSprite = Addressables.LoadAssetAsync<Sprite>("RoR2/Base/Scrap/texScrapRedIcon.png").WaitForCompletion();
  public Sprite bossSprite = Addressables.LoadAssetAsync<Sprite>("RoR2/Base/Scrap/texScrapYellowIcon.png").WaitForCompletion();
  public Sprite voidWhiteSprite = Addressables.LoadAssetAsync<Sprite>("RoR2/DLC1/ScrapVoid/texVoidScrapWhiteIcon.png").WaitForCompletion();
  public Sprite voidGreenSprite = Addressables.LoadAssetAsync<Sprite>("RoR2/DLC1/ScrapVoid/texVoidScrapGreenIcon.png").WaitForCompletion();
  public Sprite voidRedSprite = Addressables.LoadAssetAsync<Sprite>("RoR2/DLC1/ScrapVoid/texVoidScrapRedIcon.png").WaitForCompletion();
  // TODO: Add void boss sprite
  // public Sprite voidBossSprite = Addressables.LoadAssetAsync<Sprite>("Assets/RoR2/Junk_DLC1/Items/ScrapVoid/texVoidScrapIcon.png").WaitForCompletion();
  // TODO: Add lunar sprite
  // public Sprite lunarSprite = Addressables.LoadAssetAsync<Sprite>("RoR2/Base/Scrap/texScrapWhiteIcon.png").WaitForCompletion();
  // TODO: Add equipment sprite
  // public Sprite equipSprite = Addressables.LoadAssetAsync<Sprite>("RoR2/Base/Scrap/texScrapWhiteIcon.png").WaitForCompletion();

  // Pickup models
  public GameObject scrapPrefab = Addressables.LoadAssetAsync<ItemDef>("RoR2/Base/Scrap/ScrapWhite.asset").WaitForCompletion().pickupModelPrefab;
  public GameObject voidWhitePrefab = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/ScrapVoid/PickupScrapVoidWhite.prefab").WaitForCompletion();
  public GameObject voidGreenPrefab = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/ScrapVoid/PickupScrapVoidGreen.prefab").WaitForCompletion();
  public GameObject voidRedPrefab = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/ScrapVoid/PickupScrapVoidRed.prefab").WaitForCompletion();
  public GameObject voidBossPrefab = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/ScrapVoid/PickupScrapVoid.prefab").WaitForCompletion();
}