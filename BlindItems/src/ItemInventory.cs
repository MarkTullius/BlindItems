using Mono.Cecil.Cil;
using MonoMod.Cil;
using RoR2;
using UnityEngine;
using System.Reflection;

namespace BlindItems;

public class ItemInventory{
  public ItemInventory()
  {
    On.RoR2.UI.GameEndReportPanelController.SetDisplayData += (orig, self, newDisplayData) => {
      IL.RoR2.UI.ItemIcon.SetItemIndex += AllocateItemIcons;
      orig(self, newDisplayData);
      IL.RoR2.UI.ItemIcon.SetItemIndex -= AllocateItemIcons;
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
}