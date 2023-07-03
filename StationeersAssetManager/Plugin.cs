using BepInEx;

namespace SFXArtAssetManager
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        private void Awake()
        {
            // Plugin startup logic
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
            Assets.Scripts.Objects.Prefab.OnPrefabsLoaded += InitializeCustomAssetManager;
        }

        public void InitializeCustomAssetManager()
        {
            Logger.LogInfo("Prefabs Finished Loading, Time to Load Custom Assets!");
            FileManager.CheckBepInExPluginsForCustomAssetMods();
            FileManager.CheckEnabledModsForCustomAssetMods();
            KitManager.Initialize_KitManager();
            FileManager.InitializeCustomAssetMods();
            KitManager.Update_Cosmetics_CharacterKits();
            CustomCharacterCreationPanel.InitializeCustomCharacterCreationPanel();
        }
    }
}
