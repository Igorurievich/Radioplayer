using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using WMPLib;
using System.Collections;
using System.Xml;
using System.IO;
using System.Threading;
using Un4seen.Bass;
using Un4seen.Bass.AddOn.Tags;

namespace Check
{
    class Radioplayer
    {
        DOWNLOADPROC _downloadProc_; //Делегат
        int numberStream;
        public int NumStream
        {
            get
            {
                return numberStream;
            }
        }
        private ArrayList stationURL;
        private ArrayList containerURL;
        private ArrayList containerName;
        private ArrayList stationName;
        private int volume;
        private int index;
        bool AccesToURL;
        public bool AccesTourl
        {
            set { AccesToURL = value; }
        }
        TAG_INFO tagInfo;
        public Radioplayer()
        {
            BassNet.Registration("igorurievich94@gmail.com", "2X29153324152222");
            stationURL = new ArrayList();
            stationName = new ArrayList();
            containerURL = new ArrayList();
            containerName = new ArrayList();
            tagInfo = new TAG_INFO();
            volume = 50;
            index = 0;
            //StreamWriter sw = new StreamWriter(@"C:\URL.txt", true);
            //sw.Close();
            //StreamReader sr = new StreamReader(@"C:\URL.txt", true);
            //FileInfo fi = new FileInfo("URL.txt");

            //containerURL.Add(@"http://online-radioroks.tavrmedia.ua/RadioROKS");       // radio Rocks+
            //containerURL.Add(@"http://cast.radiogroup.com.ua:8000/europaplus");        // Europa Plus+
            //containerURL.Add(@"http://cast.radiogroup.com.ua:8000/avtoradio");         // Avtoradio+
            //containerURL.Add(@"http://online-hitfm.tavrmedia.ua/HitFM");               // Hit FM
            //containerURL.Add(@"http://online-kissfm.tavrmedia.ua/KissFM");             // Kiss Fm+
            //containerURL.Add(@"http://online-rusradio.tavrmedia.ua:8000/RusRadio");    // Pусское радио+
            //containerURL.Add(@"http://62.80.190.246:8000/PRock128");                   // Просто рок

            //containerName.Add("Радио Rocks");
            //containerName.Add("Европа Plus");
            //containerName.Add("Авторадио");
            //containerName.Add("ХитFM");
            //containerName.Add("KissFM");
            //containerName.Add("Русское радио");
            //containerName.Add("Просто Rock");

            //if(temp.Length > 0)
            //{
            //    for(int i=0; i<containerURL.Count; i++)
            //    {
            //        stationURL.Add(sr.ReadLine());
            //    }
            //}
            //else 
            //{
            //    //FileInfo fi = new FileInfo("URL.txt");
            //    //File.Open(@"C:\URL.txt", FileMode.OpenOrCreate);
            //    sw = fi.AppendText();
            //    for (int i = 0; i < containerURL.Count; i++)
            //    {
            //         sw.WriteLine(containerURL[i].ToString());
            //         sw.Close();
            //        //File.AppendAllText(@"C:\URL.txt", containerURL[i].ToString());
            //        //File.WriteAllText(@"C:\URL.txt", containerURL[i].ToString());
            //    }
            //}

            Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);
            AccesToURL = false;

            ///////////////////////////////////////////////////////////////////////////////////////////

            stationURL.Add(@"http://online-radioroks.tavrmedia.ua/RadioROKS");       // radio Rocks+
            stationURL.Add(@"http://cast.radiogroup.com.ua:8000/europaplus");        // Europa Plus+
            stationURL.Add(@"http://cast.radiogroup.com.ua:8000/avtoradio");         // Avtoradio+
            stationURL.Add(@"http://online-hitfm.tavrmedia.ua/HitFM");               // Hit FM
            stationURL.Add(@"http://online-kissfm.tavrmedia.ua/KissFM");             // Kiss Fm+
            stationURL.Add(@"http://online-rusradio.tavrmedia.ua:8000/RusRadio");    // Pусское радио+
            stationURL.Add(@"http://62.80.190.246:8000/PRock128");                   // Просто рок




            stationName.Add("Радио Rocks");
            stationName.Add("Европа Plus");
            stationName.Add("Авторадио");
            stationName.Add("ХитFM");
            stationName.Add("KissFM");
            stationName.Add("Русское радио");
            stationName.Add("Просто Rock");


            
        }
        public void Play(Uri address)
        {
            Bass.BASS_StreamFree(numberStream); //освобождаем поток.
            //if (AccesToURL == true)
            {
                numberStream = Bass.BASS_StreamCreateURL(address.OriginalString, 0, BASSFlag.BASS_STREAM_STATUS, _downloadProc_, IntPtr.Zero);
                IntPtr tagsIntPtr = Bass.BASS_ChannelGetTags(numberStream, BASSTag.BASS_TAG_MUSIC_MESSAGE);
                BassTags.BASS_TAG_GetFromURL(numberStream, tagInfo); //получить теги
                Bass.BASS_ChannelPlay(numberStream, true); //играем полученный поток начав сначала (второй параметр за это отвечает)
                Bass.BASS_ChannelSetAttribute(numberStream, BASSAttribute.BASS_ATTRIB_VOL, volume);
            }
            //else
            //Console.WriteLine("Peace of Shit!");
        }

        //public void GetTagsFromCurrentURLStream()
        //{
        //    TAG_INFO tagInfo = new TAG_INFO();
        //    IntPtr tagsIntPtr = Bass.BASS_ChannelGetTags(numberStream, BASSTag.BASS_TAG_MUSIC_MESSAGE);
        //    BassTags.BASS_TAG_GetFromURL(numberStream, tagInfo);
        //}
        public void addStation(string station)
        {
            stationURL.Add(station);
            stationName.Add(" ");

        }
        public void SaveStations()
        {
            
        }
        public bool CheckAcces(bool fl)
        {
            return AccesToURL = fl;
        }
        public void setVolume(int vol)
         {
             volume = vol;
             Bass.BASS_SetVolume((float)volume / 100);
         }
        public string GetCurrentURL()
         {
             return stationURL[index].ToString();
         }
        public void nextStation()
         {
            if(index < stationURL.Count-1 && index > -1)
            {
                index = index+1;
            }
            Play(new Uri(stationURL[index].ToString()));
         }
        public void prevStation()
        {
            if (index < stationURL.Count && index > 0)
            {
                index = index - 1;
            }
            Play(new Uri(stationURL[index].ToString()));
        }
        public void PrintTime(object state)
        {
            Console.Clear();
            Console.WriteLine("Текущее время:  " +
            DateTime.Now.ToLongTimeString());
        }
        public void GraphicsPlayer()
        {
            
            Console.Title = "Radioplayer";
            Console.CursorVisible = false;
            Console.Clear();
            Console.SetWindowSize(150, 54);

            int top = 1;
            int bottom = 25;
            int right = 75;
            int left = 3;

            //Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.White;
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
            /////


            Console.SetCursorPosition(left+4, top+5); // Отрисовка левой стрелки
            Console.Write((char)8592);

            Console.SetCursorPosition(left+16, top+5); // Отрисовка правой стрелки
            Console.Write((char)8594);

            
            Console.SetCursorPosition(left+4, top+8); // Отрисовка громкости
            Console.WriteLine("Volume: {0}%", volume);

            Console.SetCursorPosition(left+4, top+2);
            Console.WriteLine((index + 1) + ". " + (stationName[index])); // Показ названия станции


            if (Bass.BASS_ChannelIsActive(numberStream) == BASSActive.BASS_ACTIVE_PLAYING)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(left + 10, top + 5); // Отрисовка треугольника "Play"
                Console.Write((char)16);
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(left + 10, top + 5); // Отрисовка треугольника "Play"
                Console.Write((char)9632 +""+ (char)9632);
                Console.ResetColor();
            }
            //GetTagsFromCurrentURLStream();
            BassTags.BASS_TAG_GetFromURL(numberStream, tagInfo);
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(left+4, top+9);
            Console.WriteLine("Сейчас играет: "+tagInfo.artist+" - "+tagInfo.title);
            Console.SetCursorPosition(left + 4, top + 11);
            Console.WriteLine("Качество вещания: "+tagInfo.bitrate+" кб/с");
            Console.SetCursorPosition(left + 4, top + 12);
            Console.WriteLine("Жанр: "+tagInfo.genre);
            
            tagInfo.title = " ";
            tagInfo.artist = " ";
            tagInfo.bitrate = 0;
            tagInfo.genre = " ";

            Console.SetCursorPosition(left + +4, top + 7);
            //if(AccesToURL == true)
            {
                //Console.WriteLine("Состояние потока: "+"Доступен");
            }
            //else
            {
                //Console.WriteLine("Состояние потока: " + "Недоступен");
            }
          

        }
        ////public void GraphicsFileManager()
        //{

        //}
    }
}

//    class Radioplayer
//    {

//        private WindowsMediaPlayer WMP;

//        private string artist;
//        public string Artist
//        {
//            set
//            {
//                artist = value;
//            }
//            get
//            {
//                return artist;
//            }
//        }
//        private string title;
//        public string Title
//        {
//            get
//            {
//                return title;
//            }
//            set
//            {
//                title = value;
//            }
//        }

        
//        public Radioplayer()
//        {
//            stationURL = new ArrayList();
//            stationName = new ArrayList();
//            WMP = new WindowsMediaPlayer();
//            volume = 0;

//            //URL = new Uri("http://online-rusradio.tavrmedia.ua:8000/RusRadio");


//            stationURL.Add("http://online-radioroks.tavrmedia.ua/RadioROKS");       // radio Rocks+
//            stationURL.Add("http://cast.radiogroup.com.ua:8000/europaplus");        // Europa Plus+
//            stationURL.Add("http://cast.radiogroup.com.ua:8000/avtoradio");         // Avtoradio+
//            stationURL.Add("http://online-hitfm.tavrmedia.ua/HitFM");               // Hit FM
//            stationURL.Add("http://online-kissfm.tavrmedia.ua/KissFM_digital");     // Kiss Fm+
//            stationURL.Add("http://online-rusradio.tavrmedia.ua:8000/RusRadio");    // Pусское радио+
//            stationURL.Add("http://62.80.190.246:8000/PRock128");                   // Просто рок


//            stationName.Add("Радио Rocks");
//            stationName.Add("Европа Plus");
//            stationName.Add("Авторадио");
//            stationName.Add("ХитFM");
//            stationName.Add("KissFM");
//            stationName.Add("Русское радио");
//            stationName.Add("Просто Rock");
//        }
//        public void setStationURL(int StTemp)
//        {
//            if (StTemp < 0 || StTemp > stationURL.Count)
//            {
//                return;
//            }
//            index = StTemp;
//            WMP.URL = stationURL[index].ToString();
//            stationURL[index].ToString();
//        }

//        public void playRadio()
//        {
//            WMP.controls.play();
//            //Graphics();
//        }
//        public void setVolume(int vol)
//        {
//            //Graphics();
//            volume = vol;
//            WMP.settings.volume = volume;
//        }
//        public void pauseRadio()
//        {
//            WMP.controls.pause();
//        }
//        public int GettLength()
//        {
//            return stationURL.Count;
//        }
//        public string GetCurrentURL()
//        {
//            return stationURL[index].ToString();
//        }
//        public void Graphics(string artist, string title)
//        {
//           // Bass.BASS_StreamFree(numberStream);
//            //Bass.BASS_Free();
//            Console.Title = "Radioplayer";
//            Console.CursorVisible = false;
//            Console.Clear();
//            Console.SetWindowSize(150, 54);

//            int top = 1;
//            int bottom = 25;
//            int right = 75;
//            int left = 3;


//            Console.SetCursorPosition(left, top);
//            for (int i = left; i < right; i++) // отрисовка верхней границы
//            {
//                Console.Write((char)9472);
//            }
//            for (int i = top, cursorY = top; i < bottom; i++, cursorY++) //Отрисовка  левой границы
//            {
//                Console.SetCursorPosition(left, cursorY);
//                Console.Write((char)166);
//            }
//            for (int i = top, cursorY = top; i < bottom; i++, cursorY++) // Отрисовка правой границы
//            {
//                Console.SetCursorPosition(right, cursorY);
//                Console.WriteLine((char)166);
//            }
//            Console.SetCursorPosition(left, bottom);
//            for (int i = left; i < right; i++) // Отрисовка нижней границы
//            {
//                Console.Write((char)9472);
//            }

//            /////
//            Console.SetCursorPosition(left, top); // Верхний левый уголок
//            Console.Write((char)9484);

//            Console.SetCursorPosition(right, top); // Верхний правый уголок
//            Console.Write((char)9488);

//            Console.SetCursorPosition(left, bottom); // Нижний левый уголок
//            Console.Write((char)9492);

//            Console.SetCursorPosition(right, bottom); // Нижний правый уголок
//            Console.Write((char)9496);
//            /////

//            Console.SetCursorPosition(40, 10); // Отрисовка треугольника "Play"
//            Console.Write((char)16);

//            Console.SetCursorPosition(29, 10); // Отрисовка левой стрелки
//            Console.Write((char)8592);

//            Console.SetCursorPosition(50, 10); // Отрисовка правой стрелки
//            Console.Write((char)8594);

//            Console.SetCursorPosition(28, 13); // Отрисовка громкости
//            Console.WriteLine("Volume: {0}%", volume);

//            Console.SetCursorPosition(28, 7);
//            Console.WriteLine((index + 1) + ". " + (stationName[index])); // Показ названия станции


//            //TAG_INFO tagInfo = new TAG_INFO();
//            //IntPtr tagsIntPtr = Bass.BASS_ChannelGetTags(numberStream, BASSTag.BASS_TAG_MUSIC_MESSAGE);
//            //BassTags.BASS_TAG_GetFromURL(numberStream, tagInfo);
//            Console.SetCursorPosition(28, 15);
//            Console.WriteLine(artist);
//            Console.SetCursorPosition(28, 16);
//            Console.WriteLine(title);



//        }
//    }
//}