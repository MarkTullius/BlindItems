using RoR2;
using UnityEngine;

namespace BlindItems;

public class PrinterFix
{
  private const float yPos = 2.5f;
  public PrinterFix()
  {
    On.RoR2.InteractableSpawnCard.Spawn += CorrectPickupPosition;
  }

  // When printers are spawned with the "?" pickup model it is buried halfway inside the printer, this class just lifts it up slightly in the +y direction
  public void CorrectPickupPosition(
    On.RoR2.InteractableSpawnCard.orig_Spawn orig,
    InteractableSpawnCard self,
    Vector3 position,
    Quaternion rotation,
    DirectorSpawnRequest directorSpawnRequest,
    ref SpawnCard.SpawnResult result
  )
  {
    orig(self, position, rotation, directorSpawnRequest, ref result);
    GameObject spawned = result.spawnedInstance;
    if (spawned.name.Contains("Duplicator"))
    {
      Transform printerTransform = spawned.transform.GetChild(0);
      if (printerTransform != null && printerTransform.childCount > 2)
      {
        Transform pickupTransform = printerTransform.GetChild(2);
        if (pickupTransform != null)
        {
          Vector3 pickupPos = pickupTransform.transform.localPosition;
          Vector3 newPos = new Vector3(pickupPos.x, yPos, pickupPos.z);
          spawned.transform.GetChild(0).GetChild(2).transform.localPosition = newPos;
        }
        else
        {
          Debug.LogError("Pickup display not found. Unable to adjust model height.");
        }
      }
      else
      {
        Debug.LogError("Printer does not appear to have the correct number of children.");
      }
    }
  }
}