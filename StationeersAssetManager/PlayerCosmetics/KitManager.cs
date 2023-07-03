using Assets.Scripts.Util;
using CharacterCustomisation;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace SFXArtAssetManager
{
    static partial class KitManager
    {
        public static Dictionary<string, CharacterKit> Current_CharacterKits = new Dictionary<string, CharacterKit>();
        public static Dictionary<string, KitItem> Current_KitItems = new Dictionary<string, KitItem>();
        public static Dictionary<Species, List<List<CharacterKit>>> Current_KitSets = new Dictionary<Species, List<List<CharacterKit>>>();

        public static FieldInfo FIELDINFO_characterKits = typeof(PlayerCosmeticsBehaviour).GetField("_characterKits", BindingFlags.NonPublic | BindingFlags.Instance);
        public static FieldInfo FIELDINFO_UniqueItem_Id = typeof(UniqueItem).GetField("_id", BindingFlags.NonPublic | BindingFlags.Instance);

        private static bool Builtin_CharacterKits_Loaded = false;
        private static bool Builtin_KitSets_Loaded = false;

        public static void Initialize_KitManager()
        {
            Initialize_CharacterKits();
            Initialize_KitSets();
        }
        public static void Read_CharacterKitData(CharacterKitData characterKitData)
        {
            //flag to varify success
            var valid = false;
            switch (characterKitData.Type)
            {
                case CharacterKitType.Create:
                    valid = CreateCosmetic.CharacterKit(characterKitData);
                    break;
                case CharacterKitType.CreateUsingExisting:
                    Debug.LogError("=!=CreateUsingExisting Is Not Implimented Yet=!=");
                    //ToDo
                    break;
                case CharacterKitType.AddToExisting:
                    Debug.LogError("=!=AddToExisting Is Not Implimented Yet=!=");
                    //ToDo
                    break;
                case CharacterKitType.ModifyExisting:
                    Debug.LogError("=!=ModifyExisting Is Not Implimented Yet=!=");
                    //ToDo
                    break;
                default:
                    Debug.LogError("=!=Invalid Asset Type=!=");
                    break;
            }
        }
        public static PlayerCosmeticsBehaviour[] Scan_PlayerCosmeticsBehaviours()
        {
            return Resources.FindObjectsOfTypeAll<PlayerCosmeticsBehaviour>();
        }
        public static void Force_Identity_Update(PlayerCosmeticsBehaviour playerCosmeticsBehaviour)
        {
            var fieldInfo_loadCosmeticsOnStart = typeof(PlayerCosmeticsBehaviour).GetField("_loadCosmeticsOnStart", BindingFlags.NonPublic | BindingFlags.Instance);
            if (playerCosmeticsBehaviour.gameObject.scene.name != "Base")
                return;
            bool _loadCosmeticsOnStart = (bool)fieldInfo_loadCosmeticsOnStart.GetValue(playerCosmeticsBehaviour);
            if (_loadCosmeticsOnStart)
            {
                playerCosmeticsBehaviour.UpdateIdentity(Singleton<Assets.Scripts.GameManager>.Instance.CustomCosmeticsSlot);
            }
        }
    }
}
