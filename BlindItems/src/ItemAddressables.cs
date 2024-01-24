using BepInEx;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace BlindItems;

public class ItemAddressables{
  // Custom assets to obscure equipments, lunars and void boss items
  public static readonly AssetBundle assets = AssetBundle.LoadFromFile(System.IO.Path.Combine(Paths.PluginPath, "MarkTullius-BlindItems/blinditemsbundle"));

  // Inventory icons
  public Sprite whiteSprite = Addressables.LoadAssetAsync<Sprite>("RoR2/Base/Scrap/texScrapWhiteIcon.png").WaitForCompletion();
  public Sprite greenSprite = Addressables.LoadAssetAsync<Sprite>("RoR2/Base/Scrap/texScrapGreenIcon.png").WaitForCompletion();
  public Sprite redSprite = Addressables.LoadAssetAsync<Sprite>("RoR2/Base/Scrap/texScrapRedIcon.png").WaitForCompletion();
  public Sprite bossSprite = Addressables.LoadAssetAsync<Sprite>("RoR2/Base/Scrap/texScrapYellowIcon.png").WaitForCompletion();
  public Sprite voidWhiteSprite = Addressables.LoadAssetAsync<Sprite>("RoR2/DLC1/ScrapVoid/texVoidScrapWhiteIcon.png").WaitForCompletion();
  public Sprite voidGreenSprite = Addressables.LoadAssetAsync<Sprite>("RoR2/DLC1/ScrapVoid/texVoidScrapGreenIcon.png").WaitForCompletion();
  public Sprite voidRedSprite = Addressables.LoadAssetAsync<Sprite>("RoR2/DLC1/ScrapVoid/texVoidScrapRedIcon.png").WaitForCompletion();

  // Custom icons
  public Sprite lunarSprite = assets.LoadAsset<Sprite>("texScrapLunarIcon.png");
  public Sprite equipSprite = assets.LoadAsset<Sprite>("texScrapEquipmentIcon.png");
  public Sprite voidBossSprite = assets.LoadAsset<Sprite>("texVoidScrapBossIcon.png");
}