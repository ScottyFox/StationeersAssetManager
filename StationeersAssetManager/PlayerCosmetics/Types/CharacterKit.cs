using CharacterCustomisation;
using UnityEngine;

namespace SFXArtAssetManager
{
    static partial class CreateCosmetic
    {

        public static bool CharacterKit(CharacterKitData characterKitData)
        {
            return TryCreatingCosmetic(characterKitData, Validate_CharacterKit, Generate_CharacterKit, Collect_CharacterKit);
        }
        private static bool Validate_CharacterKit(object data)
        {
            CharacterKitData characterKitData = (CharacterKitData)data; // Unsafe Cast
            return !KitManager.Has_CharacterKit(characterKitData.Name);
        }
        private static CharacterKit Generate_CharacterKit(object data)
        {
            CharacterKitData characterKitData = (CharacterKitData)data; // Unsafe Cast
            CharacterKit characterKit = ScriptableObject.CreateInstance<CharacterKit>();
            characterKit.name = characterKitData.Name;
            if (string.IsNullOrEmpty(characterKitData.ID))
                KitManager.FIELDINFO_UniqueItem_Id.SetValue(characterKit, Utils.GenerateUUID(characterKitData.Name));
            else
                KitManager.FIELDINFO_UniqueItem_Id.SetValue(characterKit, Utils.GenerateUUID(characterKitData.ID));
            characterKit.Bodies = KitManager.Generate_KitItem_Array(KitItemSlot.Body, characterKitData.KitItems);
            characterKit.Heads = KitManager.Generate_KitItem_Array(KitItemSlot.Head, characterKitData.KitItems);
            characterKit.Eyes = KitManager.Generate_KitItem_Array(KitItemSlot.Eyes, characterKitData.KitItems);
            characterKit.EyeColours = KitManager.Generate_KitItem_Array(KitItemSlot.EyeColour, characterKitData.KitItems);
            characterKit.SkinColours = KitManager.Generate_KitItem_Array(KitItemSlot.SkinColour, characterKitData.KitItems);
            characterKit.Hairs = KitManager.Generate_KitItem_Array(KitItemSlot.Hair, characterKitData.KitItems);
            characterKit.HairColours = KitManager.Generate_KitItem_Array(KitItemSlot.HairColour, characterKitData.KitItems);
            characterKit.FacialHairs = KitManager.Generate_KitItem_Array(KitItemSlot.FacialHair, characterKitData.KitItems);
            return characterKit;
        }
        private static void Collect_CharacterKit(object data)
        {
            KitManager.Add_CharacterKit((CharacterKit)data); // Unsafe Cast
        }
    }
    //Extension of KitManager, Primarily for general utility functions.
    static partial class KitManager
    {
        public static void Initialize_CharacterKits()
        {
            var humanPrefab = Scan_PlayerCosmeticsBehaviours()[0];
            CharacterKit[] _characterKits = FIELDINFO_characterKits.GetValue(humanPrefab.GetComponent<PlayerCosmeticsBehaviour>()) as CharacterKit[];
            foreach (CharacterKit characterKit in _characterKits)
            {
                Add_CharacterKit_Recursive(characterKit);
            }
        }
        public static void Update_Cosmetics_CharacterKits()
        {
            var playerCosmeticsBehaviours_list = Scan_PlayerCosmeticsBehaviours();
            foreach (PlayerCosmeticsBehaviour playerCosmeticsBehaviour in playerCosmeticsBehaviours_list)
            {
                Apply_CharacterKits(playerCosmeticsBehaviour);
                Force_Identity_Update(playerCosmeticsBehaviour);
            }
        }
        public static void Apply_CharacterKits(PlayerCosmeticsBehaviour playerCosmeticsBehaviour)
        {
            CharacterKit[] characterKits = new CharacterKit[Current_CharacterKits.Count];
            Current_CharacterKits.Values.CopyTo(characterKits, 0);
            FIELDINFO_characterKits.SetValue(playerCosmeticsBehaviour, characterKits);
        }
        public static void Add_CharacterKit(CharacterKit characterKit)
        {
            if (!Has_CharacterKit(characterKit.name))
                Current_CharacterKits.Add(characterKit.name, characterKit);
        }
        //Mainly used for built-in characterkits, adds characterkits and their kititems.
        public static void Add_CharacterKit_Recursive(CharacterKit characterKit)
        {
            Add_CharacterKit(characterKit);
            Add_KitItem_Array(characterKit.Heads);
            Add_KitItem_Array(characterKit.Bodies);
            Add_KitItem_Array(characterKit.Eyes);
            Add_KitItem_Array(characterKit.EyeColours);
            Add_KitItem_Array(characterKit.SkinColours);
            Add_KitItem_Array(characterKit.Hairs);
            Add_KitItem_Array(characterKit.HairColours);
            Add_KitItem_Array(characterKit.FacialHairs);
        }
        public static CharacterKit Get_CharacterKit(string name)
        {
            CharacterKit characterKit = null;
            Current_CharacterKits.TryGetValue(name, out characterKit);
            return characterKit;
        }
        public static bool Has_CharacterKit(string name)
        {
            return Current_CharacterKits.ContainsKey(name);
        }
    }
}
