using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Check
{
    class Directories
    {
        int Pointer = 0;
        int DirCounter = 1;
        string URL;
        string[] pathes = new string[10];
        string[] all = Directory.GetLogicalDrives();
        string[] drives = Directory.GetLogicalDrives();
        string[] readText;

        public string[] GetF(string path)
        {
            string[] dirs = Directory.GetDirectories(@path);
            string[] files = Directory.GetFiles(@path, "*m3u");
            string[] all = new string[(dirs.Length + files.Length)];

            int i = 0;
            int j = 0;
            while (j < files.Length || i < dirs.Length)
            {
                if (i < dirs.Length)
                {
                    all[i] = dirs[i];
                    i++;
                }
                else if (i >= dirs.Length)
                {
                    all[i] = files[j];
                    j++;
                    i++;
                }
            }
            return all;
        }
        public void graph()
        {
            int top = 29;
            int bottom = top+all.Length+3;
            int right = 75;
            int left = 3;

            //Console.WriteLine();


            Console.SetCursorPosition(left, top);
            for (int i = left; i < right; i++) // отрисовка верхней границы
            {
                Console.Write((char)9472);
            }
            for (int i = top, cursorY = top; i < bottom; i++, cursorY++) //Отрисовка  левой границы
            {
                Console.SetCursorPosition(left, cursorY);
                Console.Write((char)166);
            }
            for (int i = top, cursorY = top; i < bottom; i++, cursorY++) // Отрисовка правой границы
            {
                Console.SetCursorPosition(right, cursorY);
                Console.WriteLine((char)166);
            }
            Console.SetCursorPosition(left, bottom);
            for (int i = left; i < right; i++) // Отрисовка нижней границы
            {
                Console.Write((char)9472);
            }
            /////
            Console.SetCursorPosition(left, top); // Верхний левый уголок
            Console.Write((char)9484);

            Console.SetCursorPosition(right, top); // Верхний правый уголок
            Console.Write((char)9488);

            Console.SetCursorPosition(left, bottom); // Нижний левый уголок
            Console.Write((char)9492);

            Console.SetCursorPosition(right, bottom); // Нижний правый уголок
            Console.Write((char)9496);


            for (int i = 0; i < all.Length; i++)
            {
                Console.SetCursorPosition(6, 31+i);
                if (Pointer == i)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }

                Console.WriteLine(all[i]);
            }

        }
        public void IfRoot()
        {
            if (DirCounter == 0)
            {
                for (int i = 0; i < all.Length; i++)
                {
                    all[i] = " ";
                }
                drives.CopyTo(all, 0);
            }
        }
        public bool TryOpen()
        { 
            try
            {
                pathes[DirCounter] = all[Pointer];
                all = GetF(pathes[DirCounter]);
                DirCounter++;
                Pointer = 0;
                return false;
            }
            catch
            {
                return true;
            }
        }
        public string Open()
        {
            readText = File.ReadAllLines(all[Pointer]);
            for (int i = 0; i < readText.Length; i++)
            {
                if (readText[i].Contains(@"http://"))
                {
                    URL = readText[i];
                }
            }
            return URL;
        }
        public void FocusUp()
        {
            if (Pointer > 0)
            {
                Pointer--;
            }
        }
        public void FocusDown()
        {
            if (Pointer < all.Length - 1)
            {
                Pointer++;
            }
        }
        public void GoBack()
        {
            DirCounter--;
            if (DirCounter > 0)
            {
                all = GetF(pathes[DirCounter]);
            }
        }
    }
}
