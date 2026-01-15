#if UNITY_EDITOR
namespace NappyCat.Foundation.Editor
{
    /// <summary>
    /// Shared menu path constants for Nappy Cat editor menus.
    /// Keep strings here, and use <see cref="NcMenuOrder"/> for priorities.
    /// </summary>
    internal static class NcMenuPaths
    {
        public const string ROOT      = "Nappy Cat/";

        public const string SETTINGS  = ROOT + "Settings/";
        public const string TOOLS     = ROOT + "Tools/";

        public const string CORE      = ROOT + "Core/";
        public const string APP       = ROOT + "App/";

        public const string LOCALE    = ROOT + "Locale/";
        public const string AUDIO     = ROOT + "Audio/";
        public const string UI        = ROOT + "UI/";
        public const string SHAPES    = ROOT + "Shapes/";
        public const string SHOP      = ROOT + "Shop/";
        public const string DEBUG     = ROOT + "Debug/";
        public const string PERF      = ROOT + "Perf/";
        public const string EVENTS    = ROOT + "Events/";
        public const string INPUT     = ROOT + "Input/";
        public const string INVENTORY = ROOT + "Inventory/";
        public const string STATS     = ROOT + "Stats/";
        public const string SAVE      = ROOT + "Save/";
        public const string SCENE     = ROOT + "Scene/";
        public const string CAMERA    = ROOT + "Camera/";
        public const string TWEEN     = ROOT + "Tween/";
        public const string FSM       = ROOT + "FSM/";
        public const string POOL      = ROOT + "Pool/";
        public const string TIME      = ROOT + "Time/";
        public const string TAGS      = ROOT + "Tags/";
        public const string ACHIEVEMENTS = ROOT + "Achievements/";
        public const string RARITY    = ROOT + "Rarity/";
        public const string INSPECTOR = ROOT + "Inspector/";
        public const string DOCS      = ROOT + "Docs/";
    }
}
#endif
