using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using DimOS.util;
using DimOS.App.Games;

namespace DimOS.App
{
    class OsApps
    {
        public static string _value;
        public static string[] value;
        
        
        public static void ExecuteCommand(string[] command)
        {
            value = command;
            #region Included
            if (value[0] == "calc")
                OsApps.calc();
            else if (value[0] == "game")
            {
                if (value[1] == "-list")
                    Console.WriteLine(String.Join(Environment.NewLine, "gn             | Guess number  | Guess random number!"));
                if (value[1] == "gn")
                    DimOS.App.Games.GuessNumber.Start();
                if (value[1] == "-help")
                    Console.WriteLine(
                        $"Game launcher help page:\n" +
                        $"\n" +
                        $"<parametor> - important argument\n" +
                        $"[parametor] - not important argument\n" +
                        $"\n" +
                        $"game <short game name> - starts a game, see 'game -list' to show all games");
            }
            else if (value[0] == "settings")
            {
                if (value[1] == "add")
                    Settings.ChangeSetting(value[2], true);
                else if (value[1] == "remove")
                    Settings.ChangeSetting(value[2], false);
            }
            #endregion
            #region Util
            else if (value[0] == "read")
            {
                Console.WriteLine(File.ReadAllText(Kernel.workingDirectory + "\\" + value[1]));
            }
            else if (value[0] == "write")
            {
                File.WriteAllText($"{Kernel.workingDirectory}\\{_value.Split(' ')[1]}", _value.Remove(0, _value.Remove(0, 7).Length));
            }
            else if (value[0] == "echo")
            {
                Console.WriteLine(_value.Substring(5));
            }

            else if (value[0] == "exit" || value[0] == "leave" || value[0] == "shutdown")
            {
                Logger.log("Shutting down...");
                Cosmos.System.Power.Shutdown();
            }
            else if (value[0] == "reboot" || value[0] == "restart")
            {
                Logger.log("Rebooting...");
                Cosmos.System.Power.Reboot();
            }
            else if (value[0] == "dir")
            {
                Console.WriteLine();
                try
                {
                    Console.WriteLine("Files:");
                    foreach (var item in Directory.GetFiles(Kernel.workingDirectory))
                        Console.WriteLine("\t" + item);
                    Console.WriteLine("Directories:");
                    foreach (var item in Directory.GetDirectories(Kernel.workingDirectory))
                        Console.WriteLine("\t" + item);
                    Console.WriteLine();
                    Console.WriteLine(
                        $"Total files: {Directory.GetFiles(Kernel.workingDirectory).Length}, {Environment.NewLine}" +
                        $"Total folders: {Directory.GetDirectories(Kernel.workingDirectory).Length}, {Environment.NewLine}" +
                        $"Size: {Kernel.fs.GetDirectory(Kernel.workingDirectory).mSize / 1024} KB");
                }
                catch (Exception e) { Logger.log(e.Message); }
                Console.WriteLine();
            }
            else if (value[0] == "mkfile")
            {
                Kernel.fs.CreateFile(Kernel.workingDirectory + "\\" + _value.Substring(7));
                Console.WriteLine("File was succesfully created" + Environment.NewLine);
            }
            else if (value[0] == "mkdir")
            {
                Kernel.fs.CreateDirectory(Kernel.workingDirectory + "\\" + _value.Substring(6));
                Console.WriteLine("Directory was succesfully created" + Environment.NewLine);
            }
            else if (value[0] == "cls")
            {
                Console.Clear();
                Console.WriteLine();
            }

            //fixme: СПРАВКА HELP
            else if (value[0] == "help")
            {
                Console.WriteLine
                (Environment.NewLine + String.Join(
                    Environment.NewLine,
                    "exit/leave/shutdown       | Shutting down the system",
                    "reboot/restart            | Restarting the system",
                    "calc/calculate/calculator | Calculating two numbers",
                    "mkfile                    | Creates a file",
                    "dir                       | Shows all directories and files",
                    "                            in current directory",
                    "cd                        | Navigates to directory",
                    "help                      | shows all commands and their description",
                    "set                       | Creates a local variable with value",
                    "get                       | Gets variable's value",
                    "game                      | Command that starts games, use 'game -help'",
                    "                            for help page",
                    "systeminfo/sysinfo        | Shows System information"
                    )
                );
            }
            else if (value[0] == "cd")
            {
                if (value.Length > 1)
                {
                    if (Directory.Exists(Kernel.workingDirectory + "\\" + value[1]))
                        Kernel.workingDirectory = Kernel.workingDirectory + "\\" + value[1];
                    else
                    {
                        if (value[1] == "..")
                            Kernel.workingDirectory = Kernel.fs.GetDirectory("..").mFullPath;
                        else
                            Console.WriteLine($"Directory \"{value[1]}\" does not exists");
                    }
                }
            }
            else if (value[0] == "set")
            {
                Logger.SetVar(value[1], value[2]);
                Console.WriteLine($"Variable {value[1]} set with value {value[2]}\n");
            }
            else if (value[0] == "get")
            {
                Console.WriteLine(Logger.GetVar(value[1]) + Environment.NewLine);
            }
            else if (value[0] == "FORMAT")
            {
                Console.WriteLine("Formating will delete ALL data on this disc. Do you want to continue? [no/yes]: ");
                if (Console.ReadLine() == "yes") Kernel.ResetAndFormat(true);
            }
            else if (value[0] == "systeminfo" || value[0] == "sysinfo") systeminfo();
            #endregion

            else if (File.Exists($"{Kernel.workingDirectory}\\{value[0]}"))
            {
                if (value[0].EndsWith(".dsh"))
                {
                    foreach (var item in File.ReadAllLines($"{Kernel.workingDirectory}\\{value[0]}"))
                    {
                        value = item.Split(' ');
                        _value = item;
                        ExecuteCommand(value);
                    }
                    
                }
            }
            else if (value[0].Replace(" ", "") != "")
                Console.WriteLine($@"The ""{value[0]}"" command does not exist"); 
        }
        public static void calc()
        {
            int latest = 0;
            switch (value[2])
            {
                case "+":
                    Console.WriteLine((int.Parse(value[1]) + int.Parse(value[3])));
                    latest = int.Parse(value[1]) + int.Parse(value[3]);
                    break;
                case "-":
                    Console.WriteLine((int.Parse(value[1]) - int.Parse(value[3])));
                    latest = int.Parse(value[1]) - int.Parse(value[3]);
                    break;
                case "*":
                    Console.WriteLine((int.Parse(value[1]) * int.Parse(value[3])));
                    latest = int.Parse(value[1]) * int.Parse(value[3]);
                    break;
                case "/":
                    Console.WriteLine((int.Parse(value[1]) / int.Parse(value[3])));
                    latest = int.Parse(value[1]) / int.Parse(value[3]);
                    break;

                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error: '" + value[1] + "' is not an operator");
                    Console.ResetColor();
                    break;
            }
/*            File.Create("latest.txt").Close();
            File.WriteAllText("latest.txt", latest.ToString());*/
        }
        public static void systeminfo()
        {
            Console.WriteLine($"OS name and version:    {Kernel.OSname} {Kernel.OSversion}");
            Console.WriteLine($"Total RAM:              {Cosmos.Core.CPU.GetAmountOfRAM()} MB");
            Console.WriteLine($"Curent date:            {Cosmos.HAL.RTC.DayOfTheMonth}.{Cosmos.HAL.RTC.Month}.{Cosmos.HAL.RTC.Year}, " +
                $"{Cosmos.HAL.RTC.Second}:{Cosmos.HAL.RTC.Minute}:{Cosmos.HAL.RTC.Hour} ");
            Console.WriteLine($"CPU cycle speed:        {Cosmos.Core.CPU.GetCPUCycleSpeed()}");
            Console.WriteLine($"CPU cycles:             {Cosmos.Core.CPU.GetCPUUptime()}");
            Console.WriteLine($"CPU vendor name:        {Cosmos.Core.CPU.GetCPUVendorName()}");

        }
    }
}
