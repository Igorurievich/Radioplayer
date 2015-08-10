using WMPLib;
using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Windows;
using System.IO;
using Un4seen.Bass;
using Un4seen.Bass.AddOn.Tags;


namespace Check
{
    class Program
    {
        static void Main(string[] args)
        {
            Check.Radioplayer r = new Check.Radioplayer();
            Check.CheckWeb cw = new CheckWeb();
            Check.Directories dir = new Directories();

            //TimerCallback timeCB = new TimerCallback(r.PrintTime);
            
            //Timer time = new Timer(timeCB, null, 0, 1000);
            //Console.WriteLine("Нажми чтоб выйти");
            //Console.ReadLine();

            bool player = true;
            int temp = 10;  
            ConsoleKeyInfo keypress;
            while (true)
            {
                Console.Clear();
                //Console.ResetColor();
                dir.IfRoot();
                //r.AccesTourl = cw.CheckURL(r.GetCurrentURL());


                
                //r.GetTagsFromCurrentURLStream();
                r.GraphicsPlayer();
                dir.graph();
                

                
                keypress = Console.ReadKey(true);
                
                switch (keypress.Key)

                {
                    case ConsoleKey.Enter:
                        {
                            if (player == true)
                            {
                                r.Play(new Uri(r.GetCurrentURL()));
                            }
                            else if (player == false)
                            {
                                player =  dir.TryOpen();
                                if (player == true)
                                {
                                    r.addStation(dir.Open());
                                    Console.BackgroundColor = ConsoleColor.Black;
                                }
                            }
                            break;
                        }
                    case ConsoleKey.Spacebar:
                        {
                            if (Bass.BASS_ChannelIsActive(r.NumStream) == BASSActive.BASS_ACTIVE_PLAYING)
                            {
                                Bass.BASS_ChannelPause(r.NumStream);
                            }
                            else
                            {
                                Bass.BASS_ChannelPlay(r.NumStream, false);
                            }
                            break;
                        }
                    case ConsoleKey.UpArrow: //up
                        {
                            if (player == true)
                            {
                                
                            }
                            else if (player == false)
                            {
                                dir.FocusUp();
                            }
                            break;
                        }
                    case ConsoleKey.DownArrow: //down
                        {
                            if (player == true)
                            {
                                
                            }
                            else if (player == false)
                            {
                                dir.FocusDown();
                            }
                            break;
                        }
                    case ConsoleKey.LeftArrow: //left
                        {
                            //r.GetTagsFromCurrentURLStream();
                            r.prevStation();
                            break;
                        }
                    case ConsoleKey.RightArrow://right
                        {
                            //r.GetTagsFromCurrentURLStream();
                            r.nextStation();
                            break;
                        }
                    case ConsoleKey.OemMinus:
                            {
                                r.setVolume(temp -= 1);
                                break;
                            }
                    case ConsoleKey.OemPlus:
                            {
                                r.setVolume(temp += 1);
                                break;
                            }
                    case ConsoleKey.Tab:
                        {
                            player = player ? false : true;
                            break;
                        }
                    case ConsoleKey.Escape:
                        {
                            if (player == false)
                            {
                                dir.GoBack();
                            }
                            break;
                        }
                }
            }
        }
    }
}
