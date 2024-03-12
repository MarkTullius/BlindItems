using Mono.Cecil.Cil;
using MonoMod.Cil;
using RoR2;

using System.Reflection;

namespace BlindItems;

public class ItemInventory{
  public ItemInventory()
  {
    On.RoR2.UI.GameEndReportPanelController.SetDisplayData += (orig, self, newDisplayData) => {
      IL.RoR2.UI.ItemIcon.SetItemIndex += AllocateItemIcons;
      On.RoR2.EquipmentCatalog.GetEquipmentDef += AllocateEquipIcon;
      orig(self, newDisplayData);
      IL.RoR2.UI.ItemIcon.SetItemIndex -= AllocateItemIcons;
      On.RoR2.EquipmentCatalog.GetEquipmentDef -= AllocateEquipIcon;
    };
  }

  public void AllocateItemIcons(ILContext context)
  {
    ILCursor cursor = new(context);
    MethodInfo method = typeof(ItemCatalog).GetMethod(nameof(ItemCatalog.GetItemDef));

    cursor.GotoNext(( Instruction i ) => i.MatchCall(method));
    cursor.Remove();

    method = typeof(Notifications).GetMethod(nameof(Notifications.GetOrigItemDef));
    cursor.Emit(OpCodes.Call, method);
  }

  public EquipmentDef AllocateEquipIcon(On.RoR2.EquipmentCatalog.orig_GetEquipmentDef orig, EquipmentIndex equipmentIndex)
  {
    return Notifications.GetOrigEquipDef(equipmentIndex);
  }
}