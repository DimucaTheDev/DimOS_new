using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DimOS.App
{
    class User
    {
        [Obsolete("По какой то причине не видет папки :\\")]
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
        public string Name { get; set; }
        public string ShowName { get; set; }
        public string GetFolderPath() => $@"0:\Users\{Name}\";
    }
}
