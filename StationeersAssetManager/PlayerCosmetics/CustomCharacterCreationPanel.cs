using CharacterCustomisation;
using System.Reflection;
using UnityEngine;

namespace SFXArtAssetManager
{
    //Todo Refactor into proper class.
    class CustomCharacterCreationPanel
    {
        public static CharacterCustomisationManager current_manager;

        private static FieldInfo FIELDINFO_creationPanel = typeof(CharacterCustomisationManager).GetField("_creationPanel", BindingFlags.NonPublic | BindingFlags.Instance);
        private static FieldInfo FIELDINFO_kitSets = typeof(CharacterCreationPanel).GetField("_kitSets", BindingFlags.NonPublic | BindingFlags.Instance);

        public static void InitializeCustomCharacterCreationPanel()
        {
            CharacterCustomisationManager.OnSceneLoaded += AddCurrentManager;
            CharacterCustomisationManager.OnSceneUnloaded += RemoveCurrentManager;
        }
        private static bool HasCurrentManager()
        {
            return current_manager != null;
        }
        private static void AddCurrentManager()
        {
            current_manager = Object.FindObjectOfType<CharacterCustomisationManager>();
            ScanTest();
        }
        private static void RemoveCurrentManager()
        {
            current_manager = null;
        }
        private static void ScanTest()
        {
            var _creationPanel = FIELDINFO_creationPanel.GetValue(current_manager) as CharacterCreationPanel;
            var _kitSets = FIELDINFO_kitSets.GetValue(_creationPanel) as KitSets[];
            KitManager.Add_KitSet_Array(_kitSets, true);
            KitManager.Apply_CharacterKitData_To_KitSets();
            var _newKitSets = KitManager.Generate_KitSet_Array();
            FIELDINFO_kitSets.SetValue(_creationPanel, _newKitSets);
        }
    }
}
