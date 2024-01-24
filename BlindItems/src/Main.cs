using BepInEx;
using RoR2;
using System.Collections.Generic;

namespace BlindItems;
[BepInPlugin(PluginGUID, PluginName, PluginVersion)]

public class Main : BaseUnityPlugin{
  public const string PluginGUID = PluginAuthor + "." + PluginName;
  public const string PluginAuthor = "MarkTullius";
  public const string PluginName = "BlindItems";
  public const string PluginVersion = "0.3.0";

  private Obfuscate _obfuscate;
  private Notifications _notifs;
  private Randomise _randomise;
  public static List<ItemDef> itemNotifications;
  public static List<EquipmentDef> equipNotifications;

  public void Awake(){
    itemNotifications = new List<ItemDef>();
    equipNotifications = new List<EquipmentDef>();
    _obfuscate = new Obfuscate();
    _notifs = new Notifications();
    _randomise = new Randomise();
  }
}