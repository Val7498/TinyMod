using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Falcon.Game2;
using Falcon.UniversalAircraft;
//using Falcon;
namespace TinyMod
{
    /// <summary>
    /// This class contains actions that you may subscribe to use in your mods, 
    /// including methods that wrap methods in the game to use in your game
    /// </summary>
    public static class Helper

    {
        /// <summary>
        /// This action is fired when the game state is changed, subscribe here if you want to know what 
        /// </summary>
        public static Action<GameState> onStateChange;
        //public Action<>
        /// <summary>
        /// This action is fired when FlightGame is active in the current scene, 
        /// subscribe to this action if you would like to do some stuff right 
        /// when the game environment is loaded but before the player spawns.
        /// </summary>
        public static Action<FlightGame> onFMload;

        /// <summary>
        /// This action is fired when FlightGame is unloaded,
        /// subscribe to this action if your mod needs to perform cleanup.
        /// </summary>
        public static Action onFMunload;

        /// <summary>
        /// This action is fired when player aircraft is spawned. (Note your script is automatically destroyed when player is dead)
        /// </summary>
        public static Action<UniAircraft> onPlayerSpawn;

        public static FlightGame FM
        {
            get { return FM; }
            set { 
                if(value == null)
                {
                    FM = value;
                    onFMunload();
                }
                if(FM != value)
                {
                    FM = value;
                    onFMload?.Invoke(value);
                }
            }
        }
    }

    public enum GameState
    {
        MENU,
        ARENA,
        QUICKFLIGHT,
        DATABASE,

    }


}
