using RoR2;
using BepInEx;
using BepInEx.Bootstrap;
using BepInEx.Configuration;
using System.Collections.Generic;
using RiskOfOptions;
using RiskOfOptions.Options;

namespace BlindItems;
[BepInPlugin(PluginGUID, PluginName, PluginVersion)]

public class Main : BaseUnityPlugin{
  public const string PluginGUID = PluginAuthor + "." + PluginName;
  public const string PluginAuthor = "MarkTullius";
  public const string PluginName = "BlindItems";
  public const string PluginVersion = "0.3.0";

  public static ConfigEntry<bool> RandomiseOrder { get; set; }
  public static ConfigEntry<bool> ObscureEquipment { get; set; }

  private Obfuscate _obfuscate;
  private Notifications _notifs;
  private Randomise _randomise;
  private PrinterFix _printerFix;
  public static List<ItemDef> itemNotifications;
  public static List<EquipmentDef> equipNotifications;

  public void Awake(){
    InitConfig();
    if (Chainloader.PluginInfos.ContainsKey("com.rune580.riskofoptions"))
        BuildSettings();
    itemNotifications = new List<ItemDef>();
    equipNotifications = new List<EquipmentDef>();
    _obfuscate = new Obfuscate();
    _notifs = new Notifications();
    _randomise = new Randomise();
    _printerFix = new PrinterFix();
  }

  public void InitConfig(){
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
  }

  public void BuildSettings(){
    ModSettingsManager.AddOption(new CheckBoxOption(RandomiseOrder));
    ModSettingsManager.AddOption(new CheckBoxOption(ObscureEquipment, true));
}
}