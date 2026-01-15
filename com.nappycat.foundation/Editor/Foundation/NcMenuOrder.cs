/*
 * NAPPY CAT
 *
 * Copyright © 2025 NAPPY CAT Games
 * http://nappycat.net
 *
 * Author: Stan Nesi
 *
 * File: Packages/com.nappycat.foundation/Editor/Foundation/NcMenuOrder.cs
 */
#if UNITY_EDITOR
namespace NappyCat.Foundation.Editor
{
    /// <summary>
    /// Shared domain menu order constants for Nappy Cat editor menus.
    /// Packages should use these when adding NappyCat/&lt;Domain&gt;/... items
    /// so ordering stays stable across the suite.
    /// </summary>
    internal static class NcMenuOrder
    {
        // / globals (settings, docs, hub)
        public const int CORE      =  50;
        public const int APP       =  60;

        // Main domains
        public const int LOCALE    = 100;
        public const int AUDIO     = 200;
        public const int UI        = 300;
        public const int SHAPES    = 400; // 2D / NcShapes / Character kits
        public const int DEBUG     = 500;
        public const int PERF      = 510;
        public const int EVENTS    = 520;
        public const int INPUT     = 600;
        public const int INVENTORY = 650;
        public const int ACHIEVEMENTS = 660;
        public const int STATS     = 665;
        public const int SAVE      = 700;
        public const int SCENE     = 710;
        public const int CAMERA    = 720;
        public const int TWEEN     = 730; // CueFX / Tween
        public const int FSM       = 740;
        public const int POOL      = 750;
        public const int TIME      = 760;
        public const int TAGS      = 770;
        public const int RARITY    = 775;
        public const int INSPECTOR = 800;
        public const int DOCS      = 900; // Global docs/help bucket if needed
    }
}
#endif
