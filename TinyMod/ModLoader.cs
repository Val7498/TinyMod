using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System.Reflection;
using System.IO;
using System.Reflection.Emit;
using TinyMod.Attributes;
using Falcon.UniversalAircraft;
using Falcon.Game2;
namespace TinyMod
{

   // string path = System.IO.Path.Combine(Application.dataPath, "Managed", "TinyMod.dll");
   // GameObject ml = new GameObject("TinyMod");
   // System.Reflection.Assembly asm = System.Reflection.Assembly.LoadFrom(path);
   // System.Type component = asm.GetType("TinyMod.ModLoader");
   // ml.AddComponent(component);
   //DontDestroyOnLoad(ml);
    public class ModLoader : MonoBehaviour
    {
        Dictionary<string, Assembly> assemblies = new Dictionary<string, Assembly>();
        public Action<List<Mod>> OnLoadFinish;
        public Action<string> Log, LogError, LogInfo, logDebug;
        List<Mod> loadedMods = new List<Mod>();
        string ModFolder = "";
        public GAPI gAPI = null;
        static bool Debug = true;
        public static ModLoader Instance { get; private set; }
        public static void Initialize()
        {
            GameObject ml = new GameObject("TinyMod");
            ml.AddComponent<ModLoader>();
            DontDestroyOnLoad(ml);
        }
        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
            ModFolder = Path.Combine(Application.dataPath, "..", "mods");

            Directory.CreateDirectory(ModFolder);
            gAPI = new GAPI();
            Log = gAPI.Log;
            LogError = gAPI.LogError;
            LogInfo = gAPI.LogInfo;
            if (Debug) logDebug += (string info) =>
            {
                LogInfo("[DEBUG] " + info);
            };
            Log("Initialized GAPI");
            ModEntry atr = Assembly.GetExecutingAssembly().GetCustomAttribute<ModEntry>();
            gAPI.addModEntry(atr.Name, atr.Description, new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f)));
            if (!Directory.Exists(ModFolder))
            {
                LogError("Mod folder does not exist, the folder will be created, aborting load sequence");
                Directory.CreateDirectory(ModFolder);
                logDebug("Creating mod folder");
                return;
            }
            loadAssemblies();
        }

        void loadAssemblies()
        {
            logDebug("Begin loading mods");
            foreach (string modDir in Directory.GetFiles(ModFolder, "*.dll", SearchOption.AllDirectories))
            {
                string dllname = Path.GetFileName(modDir);
                //logDebug("possible mod at " + modDir);
                if (!ValidateDll(modDir)) continue;
                logDebug("attempting to load " + dllname);
                Assembly asm = Assembly.LoadFile(modDir);
                logDebug("loaded " + dllname);
                ModEntry modinfo = asm.GetCustomAttribute<ModEntry>();
                if (modinfo == null) continue;
                if (!InitializeMod(asm)) continue; 
                logDebug("adding mod to entry list..");
                //logDebug("Mod Name: " + modinfo.Name);
                //logDebug("Mod Desc: " + modinfo.Description);
                loadedMods.Add(new Mod(modinfo.Name, modinfo.Major, modinfo.Minor, modinfo.Rev));
                gAPI.addModEntry(modinfo.Name, modinfo.Description, Color.black);
                logDebug("added.");
            }
            //IState state = new State();
            //state.loadFinish();
            OnLoadFinish(loadedMods);
        }

        bool InitializeMod(Assembly asm)
        {
            object[] attributes = asm.GetCustomAttributes(false);
            try
            {
                foreach (object attribute in attributes)
                {
                    if (attribute.GetType() == typeof(Script))
                    {
                        logDebug("Script detected");
                        Script m = (Script)attribute;
                        logDebug("cast attribute to mod attribute");

                        Type modclass = asm.GetType(m.FQN, true);
                        logDebug("getting mod class from attribute");

                        if (modclass != null)
                        {
                            logDebug("Script " + m.FQN + " found, instantiating...");

                            GameObject ScriptObject = new GameObject("Mod");
                            logDebug("creating gameobject of mod");

                            DontDestroyOnLoad(ScriptObject);
                            logDebug("DND flag set for mod object");

                            logDebug("Pre-mod Instantation");
                            ScriptObject.AddComponent(modclass);
                            logDebug("post-mod Instantation");

                            logDebug("Script Instantiated!");

                        }
                        else throw new Exception(String.Format("Could not find {0} in {1}", m.FQN, asm.GetName().Name));
                    }
                }
            }
            catch (Exception e)
            {
                LogError(e.Message);
                return false;
            }
            return true;
        }

        bool ValidateDll(string dllpath)
        {
            logDebug("Checking if " + dllpath + " could be loaded..");
            try
            {
                AssemblyName assemblyName = AssemblyName.GetAssemblyName(dllpath);
                Log(String.Format("{0} could be loaded!", assemblyName.Name));
            }
            catch (FileLoadException)
            {
                LogError("Found a duplicate dll, please check the Mod directory and remove any duplicate files!");
                return false;
            }
            catch (BadImageFormatException)
            {
                Log("Unsupported dll found (non-Assembly)");
                return false;
            }
            return true;
        }

        public struct Mod
        {
            public string Name;
            public int Major;
            public int Minor;
            public int Revision;

            public Mod(string name, int major, int minor, int rev)
            {
                Name = name;
                Major = major;
                Minor = minor;
                Revision = rev;
            }
        }
    }
}
