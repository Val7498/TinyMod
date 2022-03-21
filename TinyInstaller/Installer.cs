using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;
using Mono.Cecil.Cil;
using TinyMod;
namespace TinyMod
{
    public class Installer
    {
        readonly static string version = "0.1.0";
        readonly static string github = "https://github.com/Val7498/TinyMod";

        static string dir = "";
        public static void Main(string[] args)
        {
            //checkInst();
            //Console.Out.WriteLine("Done.");
            //Console.ReadKey(true);
            //return;
            printWelcome();
            while (true)
            {
                Console.Clear();
                printMenu();
                ConsoleKeyInfo Choice = Console.ReadKey(true);
                Console.Out.WriteLine("---------------------------------------");
                switch (Choice.Key)
                {
                    case ConsoleKey.D1:
                        {
                            Console.Out.WriteLine("Is Tiny Combat Arena installed at the default steam directory? (y/n)");
                            if (yesno()) dir = @"C:\Program Files (x86)\Steam\steamapps\common\TinyCombatArena";
                            else
                            {
                                while (true)
                                {
                                    Console.Out.WriteLine("Specify your Tiny Combat Arena install directory. (ex. C:\\games\\Tiny Combat Arena)");
                                    dir = Console.In.ReadLine();
                                    if (File.Exists(Path.Combine(dir, "Arena_Data", "Managed", "Assembly-CSharp.dll"))) break;
                                    Console.Out.WriteLine("Could not locate Tiny Combat Arena in that folder...");
                                }
                            }
                            Console.Out.WriteLine("Are you sure you want to install TinyMod? (y/n)");
                            if (!yesno()) break;
                            Console.Out.WriteLine("---------------------------------------");
                            Console.Out.WriteLine("Installing...");
                            Install();
                            Console.Out.WriteLine("TinyMod has been succesfully installed!");
                            Console.Out.WriteLine("Press any key to return to menu...");
                            Console.ReadKey(true);
                            break;
                        }
                    case ConsoleKey.D2:
                        {
                            Console.Out.WriteLine("Is Tiny Combat Arena installed at the default steam directory? (y/n)");
                            if (yesno()) dir = @"C:\Program Files (x86)\Steam\steamapps\common\TinyCombatArena";
                            else
                            {
                                while (true)
                                {
                                    Console.Out.WriteLine("Specify your Tiny Combat Arena install directory. (ex. C:\\games\\Tiny Combat Arena)");
                                    dir = Console.In.ReadLine();
                                    if (File.Exists(Path.Combine(dir, "Arena_Data", "Managed", "Assembly-CSharp.dll"))) break;
                                    Console.Out.WriteLine("Could not locate Tiny Combat Arena in that folder...");
                                }
                            }
                            Console.Out.WriteLine("Are you sure you want to Uninstall TinyMod? (y/n)");
                            if (!yesno()) break;
                            Console.Out.WriteLine("---------------------------------------");
                            Console.Out.WriteLine("Uninstalling...");
                            altUninstall();
                            Console.Out.WriteLine("To complete removal, verify game files in steam to replace modified game executable or else the game will probably hang on start up.");
                            Console.Out.WriteLine("Press any key to return to menu...");
                            Console.ReadKey(true);
                            break;
                        }
                    case ConsoleKey.D3:
                        {
                            System.Diagnostics.Process.Start(github);
                            break;
                        }
                    case ConsoleKey.D4:
                        {
                            return;
                        }
                }
            }

        }
        static void printWelcome()
        {
            Console.Out.WriteLine(@"

  _______ _             __  __           _ 
 |__   __(_)           |  \/  |         | |
    | |   _ _ __  _   _| \  / | ___   __| |
    | |  | | '_ \| | | | |\/| |/ _ \ / _` |
    | |  | | | | | |_| | |  | | (_) | (_| |
    |_|  |_|_| |_|\__, |_|  |_|\___/ \__,_|
                   __/ |                   
                  |___/                    
");
            Console.Out.WriteLine("TinyMod ~ Developed by Val7498 (Vander#1969) \n");
            Console.Out.WriteLine("This is a installer for TinyMod, a small program that injects into the games executable and allows mods to be loaded externally from a folder");
            Console.Out.WriteLine("TinyMod is a work in progress so expect tons of errors or bugs, or crashes!");
            Console.Out.WriteLine("\n Press any key to continue...");
            Console.ReadKey(true);
        }
        static void printMenu()
        {
            Console.Out.WriteLine("Tiny Mod : v{0}", version);
            Console.Out.WriteLine(" Options (press the corresponding key) ");
            Console.Out.WriteLine("-> 1 Install TinyMod                   ");
            Console.Out.WriteLine("-> 2 Uninstall TinyMod                 ");
            Console.Out.WriteLine("-> 3 Open link to github repo          ");
            Console.Out.WriteLine("-> 4 Exit Installer                    ");
            //Console.Out.WriteLine("---------------------------------------");
        }
        static bool yesno()
        {
            ConsoleKey input;
            while (true)
            {
                input = Console.ReadKey(true).Key;
                if (input == ConsoleKey.Y || input == ConsoleKey.N) break;
            }
            if (input == ConsoleKey.Y) return true;
            else return false;
        }
        static void checkInst()
        {
            dir = @"C:\Program Files (x86)\Steam\steamapps\common\TinyCombatArena";
            string dllpath = Path.Combine(dir, "Arena_Data", "Managed", "Assembly-CSharp.dll");
            string dllpatah = Path.Combine(dir, "Arena_Data", "Managed", "Assembly-CSharp-fix.dll");


            AssemblyDefinition assembly = AssemblyDefinition.ReadAssembly(dllpath);
            ModuleDefinition module = assembly.MainModule;
            foreach (AssemblyNameReference asmref in module.AssemblyReferences)
            {
                if (asmref.Name == "TinyMod")
                {
                    Console.Out.WriteLine(asmref);
                    Console.Out.WriteLine("Deleted TinyMod");
                    module.AssemblyReferences.Remove(asmref);
                    break;
                }
            }
            foreach (AssemblyNameReference asmref in module.AssemblyReferences)
            {
                if (asmref.Name == "TinyMod")
                {
                Console.Out.WriteLine(asmref);
                    Console.Out.WriteLine("Nope");
                    //module.AssemblyReferences.Remove(asmref);
                    break;
                }
            }
            //assembly.Write(dllpatah);
            //assembly.Dispose();
            //if (module.AssemblyReferences.Where(x => x.Name == "TinyMod").Count() > 0)
            //{
            //    module.AssemblyReferences.Remove(new AssemblyNameReference("TinyMod.dll", new Version("0.0.0.0")));
            //    //TypeDefinition moduleType = module.GetType("Falcon.Game2.GameLogic");

            //    //foreach (var method in moduleType.Methods)
            //    //{
            //    //    if (method.Name == "Start")
            //    //    {
            //    //        //MethodReference initMethod = module.ImportReference(typeof(ModLoader).GetMethod("Initialize"));
            //    //        //Insert instruction before the current method returns to call the initialization method
            //    //        //method.Body.Instructions.RemoveAt(method.Body.Instructions.Count - 1);
            //    //        //Console.Out.WriteLine("Done.");
            //    //        foreach (Instruction instruction in method.Body.Instructions)
            //    //        {
            //    //            Console.Out.WriteLine(instruction);
            //    //        }
            //    //        //Console.Out.WriteLine("Second to last " + method.Body.Instructions[method.Body.Instructions.Count - 2]);

            //    //        Console.Out.WriteLine("Last Instruction " + method.Body.Instructions.Last());
            //    //    }
            //    //}
            //}
        }
        static void Install()
        {
            string dllpath = Path.Combine(dir, "Arena_Data", "Managed", "Assembly-CSharp.dll");
            string outputpath = Path.Combine(dir, "Arena_Data", "Managed", "Assembly-CSharp-patched.dll");
            File.Copy(dllpath, outputpath, true);
            Console.Out.WriteLine("Attempting to read Assembly...");
            AssemblyDefinition assembly = AssemblyDefinition.ReadAssembly(outputpath);
            ModuleDefinition module = assembly.MainModule;
            Console.Out.WriteLine("Done.");
            if (module.AssemblyReferences.Where(x => x.Name == "TinyMod").Count() > 0)
            {
                Console.Out.WriteLine("Previous install detected, upgrading files instead...");
                assembly.Dispose();
                File.Delete(outputpath);
            }
            else
            {
                Console.Out.WriteLine("Adding class reference...");
                module.AssemblyReferences.Add(new AssemblyNameReference("TinyMod",typeof(TinyMod.ModLoader).Assembly.GetName().Version));
                Console.Out.WriteLine("Done.");
                //Get Type of class
                TypeDefinition moduleType = module.GetType("Falcon.Game2.GameLogic");
                Console.Out.WriteLine("Attempting to obtain entry method...");
                foreach (var method in moduleType.Methods)
                {
                    if (method.Name == "Start")
                    {
                        Console.Out.WriteLine("Found it!");

                        Console.Out.WriteLine("Attempting to add new instructions to entry method...");

                        MethodReference initMethod = module.ImportReference(typeof(ModLoader).GetMethod("Initialize"));
                        Instruction modInstruction = Instruction.Create(OpCodes.Call, initMethod);

                        //Insert instruction before the current method returns to call the initialization method
                        method.Body.Instructions.Insert(method.Body.Instructions.Count - 1, modInstruction);
                        Console.Out.WriteLine("Done.");
                    }
                }
                Console.Out.WriteLine("Saving changes to disk...");
                try
                {
                    assembly.Write(dllpath);
                    assembly.Dispose();
                    File.Delete(outputpath);
                    Console.Out.WriteLine("Done.");
                }
                catch
                {
                    Console.Out.WriteLine("Error: Could not write to disk (Is the game currently running?), Installation failed..., Installation is untouched...");
                    return;
                }

            }

            Console.Out.WriteLine("Generating folders and configurations...");
            Directory.CreateDirectory(Path.Combine(dir, "mods"));
            //Directory.CreateDirectory(Path.Combine(dir, "config"));
            File.Copy("TinyMod.dll", Path.Combine(dir, "Arena_Data", "Managed", "TinyMod.dll"), true);
            Console.Out.WriteLine("Done!\n");
        }
        static void Uninstall()
        {
            string dllpath = Path.Combine(dir, "Arena_Data", "Managed", "Assembly-CSharp.dll");
            string outputpath = Path.Combine(dir, "Arena_Data", "Managed", "Assembly-CSharp-clean.dll");
            File.Copy(dllpath, outputpath, true);
            Console.Out.WriteLine("Attempting to read Assembly...");
            AssemblyDefinition assembly = AssemblyDefinition.ReadAssembly(outputpath);
            ModuleDefinition module = assembly.MainModule;
            Console.Out.WriteLine("Done.");
            //Get Type of class
            TypeDefinition moduleType = module.GetType("Falcon.Game2.GameLogic");
            Console.Out.WriteLine("Attempting to obtain entry method...");
            foreach (var method in moduleType.Methods)
            {
                if (method.Name == "Start")
                {
                    Console.Out.WriteLine("Found it!");
                    Console.Out.WriteLine("Deleting Modloader instructions...");
                    MethodReference initMethod = module.ImportReference(typeof(ModLoader).GetMethod("Initialize"));
                    //Insert instruction before the current method returns to call the initialization method
                    method.Body.Instructions.RemoveAt(method.Body.Instructions.Count - 2); // Makes no sense but the instruction is offset by 2 
                    Console.Out.WriteLine("Done.");
                }
            }
            //ModuleReference modref = new ModuleReference("TinyMod.dll");
            if (module.AssemblyReferences.Where(x => x.Name == "TinyMod").Count() > 0)
            {
                Console.Out.WriteLine("Previous install detected, Proceeding with uninstall...");
                Console.Out.WriteLine("Removing class reference...");
                foreach (AssemblyNameReference asmref in module.AssemblyReferences)
                {
                    if (asmref.Name == "TinyMod")
                    {
                        assembly.MainModule.AssemblyReferences.Remove(asmref);
                        break;
                    }
                }
                Console.Out.WriteLine("Done.");
                Console.Out.WriteLine("Saving changes to disk...");
                try
                {
                    assembly.Write(dllpath);
                    Console.Out.WriteLine("Done.");
                }
                catch
                {
                    Console.Out.WriteLine("Error: Could not write to disk (Is the game currently running?), Installation failed..., Installation is untouched...");
                    return;
                }
            } else
            {
                Console.Out.WriteLine("Cleaning TCA install...");
            }
            assembly.Dispose();
            File.Delete(outputpath);
            string moddll = Path.Combine(dir, "Arena_Data", "Managed", "TinyMod.dll");
            if (File.Exists(moddll)) File.Delete(moddll);
            Console.Out.WriteLine("Done.");
        }

        //Whats the point of scripting a uninstall if the user is going to have to verify files in steam after?
        static void altUninstall()
        {
            string moddll = Path.Combine(dir, "Arena_Data", "Managed", "TinyMod.dll");
            string tmconffile = Path.Combine(dir, "mods", "TinyMod", "TinyMod.json");

            if (File.Exists(moddll)) File.Delete(moddll);
            Console.Out.WriteLine("Done.");

            //If config folder exists in the future just do some logic here to delete it
            Console.Out.WriteLine("Deleting TinyMod configuration file");
            if (File.Exists(tmconffile)) File.Delete(tmconffile);

        }
    }
}
