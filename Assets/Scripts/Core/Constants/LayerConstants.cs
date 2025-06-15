using System.Collections.Generic;

namespace Core.Constants
{
    public static class LayerConstants
    {
        public const int Ground = 0;
        public const int Path = 1;
        public const int Decoration = 2;

        public const int LayerCount = 3;

        public static readonly string[] LayerNames = {
            "Ground", "Path", "Decoration"
        };

        public static readonly HashSet<int> EditableInEditor = new()
        {
            Ground, Path, Decoration
        };
    }

}