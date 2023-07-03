using CharacterCustomisation;
using System.Collections.Generic;
using UnityEngine;

namespace SFXArtAssetManager
{
    static partial class CreateCosmetic
    {

        public static bool KitItem(KitItemData kitItemData)
        {
            return TryCreatingCosmetic(kitItemData, Validate_KitItem, Generate_KitItem, Collect_KitItem);
        }
        private static bool Validate_KitItem(object data)
        {
            KitItemData kitItemData = (KitItemData)data; // Unsafe Cast
            return !KitManager.Has_KitItem(kitItemData.Name);
        }
        private static KitItem Generate_KitItem(object data)
        {
            KitItemData kitItemData = (KitItemData)data; // Unsafe Cast
            KitItem kitItem = ScriptableObject.CreateInstance<KitItem>();
            kitItem.name = kitItemData.Name;
            if (string.IsNullOrEmpty(kitItemData.ID))
                KitManager.FIELDINFO_UniqueItem_Id.SetValue(kitItem, Utils.GenerateUUID(kitItemData.Name));
            else
                KitManager.FIELDINFO_UniqueItem_Id.SetValue(kitItem, Utils.GenerateUUID(kitItemData.ID));
            var assetData = AssetManager.Get_AssetData(kitItemData.Asset);
            if (assetData != null)
            {
                kitItem.Item = assetData.Asset;
                if (kitItemData.Slot == KitItemSlot.Body || kitItemData.Slot == KitItemSlot.Head)
                    kitItem.SkinnedMeshRenderer = (kitItem.Item as GameObject).GetComponent<SkinnedMeshRenderer>();
            }
            return kitItem;
        }
        private static void Collect_KitItem(object data)
        {
            KitManager.Add_KitItem((KitItem)data); // Unsafe Cast
        }
    }
    //Extension of KitManager, Primarily for general utility functions.
    static partial class KitManager
    {
        public static void Add_KitItem(KitItem kitItem)
        {
            if (!Has_KitItem(kitItem.name))
                Current_KitItems.Add(kitItem.name, kitItem);
        }
        public static void Add_KitItem_Array(KitItem[] kitItems)
        {
            foreach (KitItem kitItem in kitItems)
            {
                Add_KitItem(kitItem);
            }
        }
        public static KitItem Get_KitItem(string name)
        {
            KitItem kitItem = null;
            Current_KitItems.TryGetValue(name, out kitItem);
            return kitItem;
        }
        public static bool Has_KitItem(string name)
        {
            return Current_KitItems.ContainsKey(name);
        }
        //Convert KitItems into Workable array (for CharacterKits)
        public static KitItem[] Generate_KitItem_Array(KitItemSlot slot, List<KitItemData> kitItemDatas)
        {
            List<KitItem> kitItemArray = new List<KitItem>();
            foreach (KitItemData kitItemData in kitItemDatas)
            {
                if (kitItemData.Slot == slot)
                {
                    CreateCosmetic.KitItem(kitItemData);
                    var kititem = Get_KitItem(kitItemData.Name);
                    if (kititem != null)
                        kitItemArray.Add(kititem);
                }
            }
            return kitItemArray.ToArray();
        }
    }
}
