using BepInEx;
using HarmonyLib;
using System.IO;
using System.Reflection;

/// <summary>
/// This is the main plugin class that BepInEx injects and executes.
/// This class provides MonoBehaviour methods and additional BepInEx-specific services like logging and
/// configuration system.
/// </summary>
/// <remarks>
/// BepInEx plugins are MonoBehaviours. Refer to Unity documentation for information on how to use various Unity
/// events: https://docs.unity3d.com/Manual/class-MonoBehaviour.html
///
/// To get started, check out the plugin writing walkthrough:
/// https://bepinex.github.io/bepinex_docs/master/articles/dev_guide/plugin_tutorial/index.html
/// </remarks>
[BepInPlugin(PluginInfo.PLUGIN_ID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
public class FoodSpoilagePlugin : BaseUnityPlugin
{
    //Declare our config options
    public static BepInEx.Configuration.ConfigEntry<bool> fullStackSpoil;
    public static BepInEx.Configuration.ConfigEntry<bool> foodSpoilageEnabled;
    public static BepInEx.Configuration.ConfigEntry<float> toolbeltPenalty;
    public static BepInEx.Configuration.ConfigEntry<float> backpackPenalty;
    public static BepInEx.Configuration.ConfigEntry<float> containerPenalty;
    public static BepInEx.Configuration.ConfigEntry<int> minimumSpoilage;
    public static BepInEx.Configuration.ConfigEntry<int> ticksPerLoss;
    public static BepInEx.Configuration.ConfigEntry<string> spoiledItem;
    public static BepInEx.Configuration.ConfigEntry<bool> enableLogging;

    //Create our logging source
    public static new BepInEx.Logging.ManualLogSource Logger = new BepInEx.Logging.ManualLogSource(PluginInfo.PLUGIN_ID);

    public FoodSpoilagePlugin()
    {
        foodSpoilageEnabled = Config.Bind<bool>("Food Spoilage", "Enabled", true, "Food spoils over time");
        foodSpoilageEnabled = Config.Bind<bool>("Food Spoilage", "FullStackSpoil", true, "Full stack spoil?");
        toolbeltPenalty = Config.Bind<float>("Food Spoilage", "Toolbelt", 6, "Toolbelt spoilage penalty");
        backpackPenalty = Config.Bind<float>("Food Spoilage", "Backpack", 5, "Toolbelt spoilage penalty");
        containerPenalty = Config.Bind<float>("Food Spoilage", "Container", 4, "Toolbelt spoilage penalty");
        minimumSpoilage = Config.Bind<int>("Food Spoilage", "MinimumSpoilage", 1, "Toolbelt spoilage multiplier");
        ticksPerLoss = Config.Bind<int>("Food Spoilage", "TicksPerLoss", 10, "Absolute minimum spoilage per tick");
        spoiledItem = Config.Bind<string>("Food Spoilage", "SpoiledItem", "foodRottingFlesh", "When spoiled, this item will turn into this item");
        enableLogging = Config.Bind<bool>("Food Spoilage", "Enable Logging", false, "Enable advanced logging details");
    }

    private void Awake()
    {
        //Loads all Harmony patches in this dll
        new Harmony(GetType().ToString()).PatchAll(Assembly.GetExecutingAssembly());
    }
}