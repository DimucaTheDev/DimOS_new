using System;
using Sys = Cosmos.System;
using DimOS.App;
using DimOS.util;
using System.Threading;
using System.IO;
using Cosmos.System.Graphics;
using System.Net;
using Cosmos.System.Network;

namespace DimOS
{
    public class Kernel : Sys.Kernel
    {
        public static string OSname = "DimOS";
        //TODO: МЕНЯЙ ДАТУ И ВЕРСИЮ
        public static string OSversion = "0.031_14122021";

        public static Sys.FileSystem.CosmosVFS fs = new Sys.FileSystem.CosmosVFS();
        public static string workingDirectory = @"0:";
        public static bool fast = true;
        //public static string latest = (new Uri("https://dimucathedev.github.io/DimOS/latest.txt"));
        
        protected override void BeforeRun()
        {

            Sys.FileSystem.VFS.VFSManager.RegisterVFS(fs);

            if (File.Exists($"{workingDirectory}\\format"))
            {
                Console.Clear();
                Console.WriteLine($"Fromatting drive, please wait...");
                fs.Format("0", "0", fast);
                Console.WriteLine("Formatting completed");

                for (int i = 0; i < 4000 * 100000; i++)
                {

                }
                fs.DeleteFile(fs.GetDirectory($"{workingDirectory}\\format"));

                Sys.Power.Reboot();
            }
            if (File.Exists("0:\\sys\\sdi")) //Old: SkipDiskInformation
                Console.Clear();
            else Console.WriteLine();

            Console.WriteLine($"Welcome to {OSname} [Version {OSversion}]");
            Console.WriteLine("(C) DimucaTheDev. All rights reserved" + Environment.NewLine);
            Console.WriteLine("Download the latest version of DimOS on one of these web-sites: \n" + "adfoc.us/72690482142477\n"+ "clck.ru/ZSPM3 \n" + "dimucathedev.github.io/projects/\n");
            //if (latest != OSversion) Console.WriteLine($"New version ({latest}) avaliable!\nYou can download it on http://adfoc.us/72690482142477");
        }

        protected override void Run()
        {
            Console.Write(workingDirectory+">");
                string _temp = Console.ReadLine();
                //CatOS.App.OsApps.value = _temp.Split(' ');
                try
                {
                    OsApps._value = _temp;
                    OsApps.ExecuteCommand(_temp.Split(' '));
                }
                catch (Exception ex)
                {
                if (Settings.ExistSetting("sbsod"))
                    CriticalError(ex);
                else
                    Console.WriteLine(ex.Message);
                }
        }
        public void reestartKernel() => Run();
        public static void restartKernel() => new Kernel().reestartKernel();
        public static void ResetAndFormat(bool _fast)
        {
            fs.CreateFile($"0:\\format");
            Sys.Power.Reboot();
        }
        public static void CriticalError(Exception ex)
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.Clear();
            Console.WriteLine(
                $"{OSname} {OSversion}\n"+
                $"  Critical error occured!\n" +
                $"  Message:{(ex.Message)}\n\n");
            Console.WriteLine("Press R to restart computer, S to shutdown");
            ConsoleKeyInfo k = Console.ReadKey();
            if (k.Key == ConsoleKey.R) Sys.Power.Reboot();
            else Sys.Power.Shutdown();
        }
    }
    class Settings
    {
        public static void ChangeSetting(string sys, bool value)
        {
            if (!Directory.Exists("0:\\sys")) Directory.CreateDirectory("0:\\sys");
            if (value)
                File.Create("0:\\sys\\"+sys);
            else
                File.Delete("0:\\sys\\" + sys);
        }
        public static bool ExistSetting(string sys) => File.Exists("0:\\" + sys);
    }
    class temp
    {/*
        public static void CreateUser(string username)
        {
            if (!Directory.Exists($@"0:\Users\{username}"))
            {
                Directory.CreateDirectory($@"0:\Users\{username}");
                Directory.CreateDirectory($@"0:\Users\{username}\Documents");
                Directory.CreateDirectory($@"0:\Users\{username}\Music");
                Directory.CreateDirectory($@"0:\Users\{username}\Favorites");
                Directory.CreateDirectory($@"0:\Users\{username}\Videos");
                Directory.CreateDirectory($@"0:\Users\{username}\UserData");
                Directory.CreateDirectory($@"0:\Users\{username}\Pictures");
            }
        }
                    if (FixAndRepair())
            {
                string username = Console.ReadLine();
                if (!Directory.Exists($@"0:\users\{username}"))
                {
                    Directory.CreateDirectory($@"0:\Users\{username}");
                    Logger.log("Created 'Dima' folder in Users");

                    Directory.CreateDirectory($@"0:\Users\{username}\documents");
                    Logger.log("Created 'Documents' folder");

                    Directory.CreateDirectory($@"0:\Users\{username}\music");
                    Logger.log("Created 'Music' folder");

                    Directory.CreateDirectory($@"0:\Users\{username}\favorites");
                    Logger.log("Created 'Favorites' folder");

                    Directory.CreateDirectory($@"0:\Users\{username}\videos");
                    Logger.log("Created 'Videos' folder");

                    Directory.CreateDirectory($@"0:\Users\{username}\userdata");
                    Logger.log("Created 'UserData' folder");

                    Directory.CreateDirectory($@"0:\Users\{username}\pictures");
                    Logger.log("Created 'Pictures' folder");
                }
}*/

    }
}
