using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DimOS.Installer
{
    ///Вообщем здесь будет метод тип,
    ///если к примеру не будет какой то "очень важной" папки или че та типа того, то будет восстановление
    class Installer
    {
        [Obsolete("You dont have to use 'Installer' constructor")]
        public Installer() { }
        public static void RestoreUserProfileFolder(DimOS.App.User user)
        {
            Kernel.fs.CreateDirectory($@"0:\Users\{user.Name}");
            Kernel.fs.CreateDirectory($@"0:\Users\{user.Name}\Documents");
            Kernel.fs.CreateDirectory($@"0:\Users\{user.Name}\Music");
            Kernel.fs.CreateDirectory($@"0:\Users\{user.Name}\Favorites");
            Kernel.fs.CreateDirectory($@"0:\Users\{user.Name}\Videos");
            Kernel.fs.CreateDirectory($@"0:\Users\{user.Name}\UserData");
            Kernel.fs.CreateDirectory($@"0:\Users\{user.Name}\Pictures");
        }
    }
}
