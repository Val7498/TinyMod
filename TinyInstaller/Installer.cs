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
        readonly static string github = "https://github.com/Val7498";

        static string dir = @"C:\Program Files (x86)\Steam\steamapps\common\TinyCombatArena";
        public static void Main(string[] args)
        {
            //checkInst();
            //Console.Out.WriteLine("Done.");
            //Console.In.ReadLine();
            //return;
            //Verify();
            //Console.Out.WriteLine("Done.");
            //Console.In.ReadLine();
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
                            if (yesno())
                            {
                                dir = @"C:\Program Files (x86)\Steam\steamapps\common\TinyCombatArena";
                                //if (Install())
                                //{
                                //    Console.WriteLine("TinyMod successfully installed! \nTo update, download and replace TinyMod.dll in Arena_Data/Managed");
                                //}
                                //else Console.WriteLine("Failed to install... (Is TCA running?) ");
                            }
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
                            Console.Out.WriteLine("Press any key to return to menu...");
                            Console.ReadKey(true);
                            break;
                        }
                    case ConsoleKey.D2:
                        {
                            //checkInst();
                            //Console.ReadLine();
                            //break;
                            Console.Out.WriteLine("Is Tiny Combat Arena installed at the default steam directory? (y/n)");
                            if (yesno())
                            {
                                dir = @"C:\Program Files (x86)\Steam\steamapps\common\TinyCombatArena";
                                //if (Install())
                                //{
                                //    Console.WriteLine("TinyMod successfully installed! \nTo update, download and replace TinyMod.dll in Arena_Data/Managed");
                                //}
                                //else Console.WriteLine("Failed to install... (Is TCA running?) ");
                            }
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
                            Console.Out.WriteLine("Removing...");
                            Uninstall();
                            Console.Out.WriteLine("Press any key to return to menu...");
                            Console.ReadKey(true);
                            //Console.Out.WriteLine("Open Steam and in the Manage Tab of TCA select Local Files and then press Verify integrity of game files, this should remove the preloader from the game and prevent mods from being loaded");
                            //Console.Out.WriteLine("Press any key to return to menu...");
                            //Console.ReadKey(true);
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
            Console.Out.WriteLine("-> 2 Open link to github repo          ");
            Console.Out.WriteLine("-> 3 Uninstall TinyMod                 ");
            Console.Out.WriteLine("-> 4 Exit Installer                    ");
            //Console.Out.WriteLine("---------------------------------------");
        }
        static bool yesno()
        {
            ConsoleKey input = Console.ReadKey(true).Key;
            while (true)
            {
                if (input == ConsoleKey.Y || input == ConsoleKey.N) break;
            }
            if (input == ConsoleKey.Y) return true;
            else return false;
        }
        static void checkInst()
        {
            string dllpath = Path.Combine(dir, "Arena_Data", "Managed", "Assembly-CSharp.dll");

            AssemblyDefinition assembly = AssemblyDefinition.ReadAssembly(dllpath);
            ModuleDefinition module = assembly.MainModule;
            if (module.AssemblyReferences.Where(x => x.Name == "TinyMod").Count() > 0)
            {
                TypeDefinition moduleType = module.GetType("Falcon.Game2.GameLogic");

                foreach (var method in moduleType.Methods)
                {
                    if (method.Name == "Start")
                    {
                        //MethodReference initMethod = module.ImportReference(typeof(ModLoader).GetMethod("Initialize"));
                        //Insert instruction before the current method returns to call the initialization method
                        //method.Body.Instructions.RemoveAt(method.Body.Instructions.Count - 1);
                        //Console.Out.WriteLine("Done.");
                        foreach (Instruction instruction in method.Body.Instructions)
                        {
                            Console.Out.WriteLine(instruction);
                        }
                        //Console.Out.WriteLine("Second to last " + method.Body.Instructions[method.Body.Instructions.Count - 2]);

                        Console.Out.WriteLine("Last Instruction " + method.Body.Instructions.Last());
                    }
                }
            }
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
                module.ModuleReferences.Add(new ModuleReference("TinyMod.dll"));
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
                } catch
                {
                    Console.Out.WriteLine("Error: Could not write to disk (Is the game currently running?), Installation failed..., Installation is untouched...");
                    return;
                }
                
            }

            Console.Out.WriteLine("Generating folders and configurations...");
            Directory.CreateDirectory(Path.Combine(dir, "mods"));
            Directory.CreateDirectory(Path.Combine(dir, "config"));
            File.Copy("TinyMod.dll", Path.Combine(dir, "Arena_Data", "Managed", "TinyMod.dll"), true);
            Console.Out.WriteLine("Done!\n");
            Console.Out.WriteLine("TinyMod has been succesfully installed!");


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
            //ModuleReference modref = new ModuleReference("TinyMod.dll");
            if (module.AssemblyReferences.Where(x => x.Name == "TinyMod").Count() > 0)
            {
                Console.Out.WriteLine("Previous install detected, Proceeding with uninstall...");
                Console.Out.WriteLine("Removing class reference...");
                //module.AssemblyReferences.Remove(module.AssemblyReferences.Where(x => x.Name == "TinyMod").First());
                Console.Out.WriteLine("Done.");

                //Get Type of class
                TypeDefinition moduleType = module.GetType("Falcon.Game2.GameLogic");
                foreach (var method in moduleType.Methods)
                {
                    Console.Out.WriteLine("Attempting to obtain entry method...");
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

            string moddll = Path.Combine(dir, "Arena_Data", "Managed", "TinyMod.dll");
            if (File.Exists(moddll)) File.Delete(moddll);
            Console.Out.WriteLine("TinyMod has been removed from the Install, it is recommended that you verify game integrity in Steam to avoid any issues.");
        }
    }
}
