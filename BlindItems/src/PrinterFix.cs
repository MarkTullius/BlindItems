using RoR2;
using UnityEngine;

namespace BlindItems;

public class PrinterFix{
  public PrinterFix(){
    On.RoR2.InteractableSpawnCard.Spawn += CorrectPickupPosition;
  }

  public void CorrectPickupPosition(On.RoR2.InteractableSpawnCard.orig_Spawn orig, InteractableSpawnCard self, Vector3 position, Quaternion rotation, DirectorSpawnRequest directorSpawnRequest, ref SpawnCard.SpawnResult result){
    orig(self, position, rotation, directorSpawnRequest, ref result);
    GameObject spawned = result.spawnedInstance;
    if (spawned.name.Contains("Duplicator")){
      Vector3 printerPos = spawned.transform.GetChild(0).GetChild(2).transform.localPosition;
      Vector3 newPos = new Vector3(printerPos.x, 2.5f, printerPos.z);
      spawned.transform.GetChild(0).GetChild(2).transform.localPosition = newPos;
    }
  }
}