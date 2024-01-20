using RoR2;
using UnityEngine;
using System.Collections.Generic;

namespace BlindItems;

public class NotificationInfo{
    public List<ItemInfo> ItemInfos { get; set; }

    public NotificationInfo(){
        ItemInfos = new List<ItemInfo>();
    }
}

public struct ItemInfo{
    public ItemIndex itemIndex { get; set; }
    public Sprite icon { get; set; }
    public string name { get; set; }
    public string pickupToken { get; set; }
    public string description { get; set; }
}
