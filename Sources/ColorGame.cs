using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayerApp.Sources
{
    public enum ColorValue
    {
        R, 
        G, 
        B
    }

    internal class ColorGame
    {
        public readonly static int MINPINKVALUE = unchecked((int)0xFF9A7F7E);
        public readonly static int MAXPINKVALUE = unchecked((int)0xffffc8c9);
        public readonly static int PLAYER_YELLOW_COLOR = unchecked((int)0xffffd74c);
        public readonly static int PLAYER_PINK_COLOR = unchecked((int)0xffee36df);

        public readonly static int CHAT_WHITE_COLOR = unchecked((int)0xffffffff);
        public readonly static int CHAT_PINK_COLOR = unchecked((int)0xffffc8c8);

        public readonly static int MINIMAP_DARKGREEN = unchecked((int)0xff519a3e);
        public readonly static int MINIMAP_GREEN = unchecked((int)0xff7ae35d);
        public readonly static int MINIMAP_BALIKCI_GREEN = unchecked((int)0xff7ae75d);
        public readonly static int MAP_BALIKCI_GREEN = unchecked((int)0xff7ae75d);
        public readonly static int MAP_CAMP_FIRE_GREEN = unchecked((int)0xff7ae75d);
        public readonly static int MAP_NPC_RED_TITLE = unchecked((int)0xFFEB1609);
        public readonly static int MAP_PLAYER_LEVEL_GREEN = unchecked((int)0xFF98FF33);

        public readonly static int YELLOW_PICKUP_PLAYER_NAME = unchecked((int)0xFFFFFF00);

        public readonly static int MINIMAP_NPC_DARKRED = unchecked((int)0xff9d0f06);
        public readonly static int MINIMAP_NPC_OPENRED = unchecked((int)0xffeb1609);

        public readonly static int MINIMAP_PLAYER_DARKYELLOW = unchecked((int)0xffaa8f33);
        public readonly static int MINIMAP_PLAYER_OPENYELLOW = unchecked((int)0xffffd44c);

        public readonly static int MINIMAP_PLAYER_DARKPINK = unchecked((int)0xff9f2395);
        public readonly static int MINIMAP_PLAYER_OPENPINK = unchecked((int)0xffee36df);

        public readonly static int MIN_WOOD_VALUE = unchecked((int)0xff191108);
        public readonly static int MAX_WOOD_VALUE = unchecked((int)0xffa58b75);

        public readonly static int MİN_WORM_VALUE = unchecked((int)0xff303027);
        public readonly static int MAX_WORM_VALUE = unchecked((int)0xfff6c1ae);
    }
}
