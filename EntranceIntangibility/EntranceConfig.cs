using BepInEx.Configuration;
using CSync.Extensions;
using CSync.Lib;

namespace EntranceIntangibility;

public class EntranceConfig : SyncedConfig2<EntranceConfig>
{
    
    [SyncedEntryField] public SyncedEntry<float> IntangibilityDuration;

    public EntranceConfig(ConfigFile configFile) : base(Plugin.GUID)
    {
        IntangibilityDuration = configFile.BindSyncedEntry( "Settings", "IntangibilityDuration", 2.5f, 
            "How long the player is intangible for after entering or leaving the facility, in seconds.");
        
        ConfigManager.Register(this); 
    }
}