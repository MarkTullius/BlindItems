using RoR2;
using BepInEx;
using BepInEx.Bootstrap;
using BepInEx.Configuration;
using System.Collections.Generic;
using RiskOfOptions;
using RiskOfOptions.Options;
using UnityEngine;

namespace BlindItems;
[BepInPlugin(PluginGUID, PluginName, PluginVersion)]

public class Main : BaseUnityPlugin
{
  public const string PluginGUID = PluginAuthor + "." + PluginName;
  public const string PluginAuthor = "MarkTullius";
  public const string PluginName = "BlindItems";
  public const string PluginVersion = "0.5.1";

  public static ConfigEntry<bool> RandomiseOrder { get; set; }
  public static ConfigEntry<bool> ObscureEquipment { get; set; }
  public static ConfigEntry<bool> ObscureNotifs { get; set; }

  private Obfuscate _obfuscate;
  private Notifications _notifs;
  private Randomise _randomise;
  private PrinterFix _printerFix;
  private ItemInventory _itemInventory;
  public static List<ItemDef> itemNotifications;
  public static List<EquipmentDef> equipNotifications;
  public const string desc = "Obscures the pickup models and inventory icons, for items and equipments, by replacing them with '?' to add a cursed challenge where you don't know what you're picking up (scrapping/printing) until you have done so.";

  public void Awake()
  {
    InitConfig();
    // Only set up Risk of Options settings if the mod is installed
    if (Chainloader.PluginInfos.ContainsKey("com.rune580.riskofoptions"))
    {
      BuildSettings();
    }
    else
    {
      Debug.Log("Risk of Options is not installed, skipping setup.");
    }

    itemNotifications = new List<ItemDef>();
    equipNotifications = new List<EquipmentDef>();

    _obfuscate = new Obfuscate(itemNotifications, equipNotifications);
    if(!ObscureNotifs.Value)
      _notifs = new Notifications(itemNotifications, equipNotifications);
    _randomise = new Randomise();
    _printerFix = new PrinterFix();
    _itemInventory = new ItemInventory();
  }

  public void InitConfig()
  {
    RandomiseOrder = Config.Bind(
        "General"
    ,   "Randomise Item Order Each Stage"
    ,   false
    ,   "Turn on to randomise the order of your items every time you load into a new stage."
    );
    ObscureEquipment = Config.Bind(
        "General"
    ,   "Obscure Current Equipment"
    ,   true
    ,   "Turn off to retain the icon for your currently held equipment(s). Requires a restart"
    );
    ObscureNotifs = Config.Bind(
        "General"
    ,   "Obscure Notifications"
    ,   false
    ,   "Turn off to retain the icon and descriptions upon pickup/printing/scrapping. Requires a restart"
    );
  }

  public void BuildSettings()
  {
    ModSettingsManager.SetModDescription(desc, PluginGUID, PluginName);
    ModSettingsManager.SetModIcon(ItemAddressables.modIcon);
    ModSettingsManager.AddOption(new CheckBoxOption(RandomiseOrder));
    ModSettingsManager.AddOption(new CheckBoxOption(ObscureEquipment, true));
}
}