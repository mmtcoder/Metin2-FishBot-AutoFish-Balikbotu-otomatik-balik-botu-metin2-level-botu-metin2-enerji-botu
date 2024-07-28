using MusicPlayerApp.Debugs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static MusicPlayerApp.Sources.ScreenShotWinAPI;

namespace MusicPlayerApp.Sources
{
    public enum PathWayStruct
    {
        PATH_STANDART,
        PATH_DESTKOP,
        PATH_IMAGE,
        PATH_FISHES,
        PATH_CHAT_ALPHABETS,
        PATH_TESTIMAGES,
        PATH_CHAT_Q_A,
        PATH_SCREENSHOTS

    }

    public enum ReturOrFind
    {
        RETURN_PATH,
        FIND_PATH,
    }
    internal class FileHandler
    {
        
        

        public static void SaveImageAsPng(Bitmap bitmap, string fileName, PathWayStruct pathWay)
        {
            if (bitmap == null || bitmap.Width > 0)
            {
                string path = ReturnOrFindPathWay(fileName, pathWay,ReturOrFind.RETURN_PATH);

                bitmap.Save(path, System.Drawing.Imaging.ImageFormat.Png);
            }
            else
            {
                DebugPfCnsl.println(" bitmap null or widht is less than zero in saveImage FUNCT !!!");
            }
        }

        public static string PathWantedWayFromBase(string fileName,string []folderNames)
        {
            //string currentDirect = Environment.CurrentDirectory;
            string currentDirect = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string[] stringArray = currentDirect.Split('\\');

            string temp = "";
            for (int i = 0; i < stringArray.Length - 2; i++)
            {
                if (i == (stringArray.Length -3))
                {
                    temp += stringArray[i];
                    break;
                }
                temp += stringArray[i] + "\\";

            }

            try
            {

                for (int k=0; k < folderNames.Length; k++) 
                 {
                     temp = Path.Combine(temp, folderNames[k]);
                 }

                temp = Path.Combine(temp, fileName);

            }
            catch (ArgumentNullException ex1)
            {
                DebugPfCnsl.println(ex1.Message);
            }
            catch (ArgumentException ex2) { DebugPfCnsl.println(ex2.Message); }

           

            //DebugPfCnsl.println(temp);
            return temp;
            
        }

        public static string ReturnOrFindPathWay(string fileName,PathWayStruct folderWay, ReturOrFind state) 
        {
            string path = "";
            //string currentDirect = Environment.CurrentDirectory;
            //string[] stringArray = currentDirect.Split('\\');

            if (folderWay == PathWayStruct.PATH_STANDART)
            {
                path = fileName;
            }
            else if (folderWay == PathWayStruct.PATH_DESTKOP)
            {
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                path = Path.Combine(desktopPath, fileName);

            }
            else if(folderWay == PathWayStruct.PATH_IMAGE)
            {
                path = PathWantedWayFromBase(fileName, new string[] { "Images" });

            }
            else if(folderWay == PathWayStruct.PATH_CHAT_ALPHABETS)
            {
                path = PathWantedWayFromBase(fileName, new string[] { "ChatResources", "GameAlphabets" });
            }
            else if(folderWay == PathWayStruct.PATH_TESTIMAGES)
            {
                path = PathWantedWayFromBase(fileName, new string[] { "TestImages" });
            }
            else if(folderWay == PathWayStruct.PATH_CHAT_Q_A)
            {
                path = PathWantedWayFromBase(fileName, new string[] { "ChatResources", "ChatQuestionAnswer" });
            }
            else if(folderWay == PathWayStruct.PATH_SCREENSHOTS)
            {
                path = PathWantedWayFromBase(fileName, new string[] { "ScreenShot" });
            }
            else
            {
                path = PathWantedWayFromBase(fileName, new string[] { "Fishes" });
            }
                if(state == ReturOrFind.FIND_PATH)
            {
                if (File.Exists(path))
                {
                    return path;
                }
                else
                { throw new FileNotFoundException("file name couldn't be found"); }

            }
            else { return path; }

        }
        /// <summary>
        /// Find wanted folder name from base that project start from exe build place
        /// </summary>
        /// <param name="folderName"> wanted folder name </param>
        /// <returns>folder name that we wanted if it could't
        /// find, returns NULL.</returns>
        public static string FindFolderNameFromBase(String folderName)
        {
            string currentDirect = Environment.CurrentDirectory;
            string[] stringArray = currentDirect.Split('\\');

            string parsedWay = "";
            for (int i = 0; i < stringArray.Length - 2; i++)
            {
                if (i == (stringArray.Length - 3))
                {
                    parsedWay += stringArray[i];
                    break;
                }
                parsedWay += stringArray[i] + "\\";

            }
           // DebugPfCnsl.println("parsedWay = " + parsedWay);

            string[] foldarPaths = Directory.GetDirectories(parsedWay,folderName,SearchOption.AllDirectories);

            if(foldarPaths.Length > 0)
            {
                foreach (string foldarPath in foldarPaths)
                {
                   // Console.WriteLine("Bulunan klasor: " + foldarPath);
                    if (foldarPath.Contains(folderName))
                    {
                        return foldarPath;
                    }
            }
            }else
            {
                throw new FileNotFoundException("Wanted folder is not found !!");
            }
           
          return null;
        }

        public static Bitmap ReadPngFileGetBitmap(String fileName,PathWayStruct pathWay)
        {
            string filePath = ReturnOrFindPathWay(fileName, pathWay, ReturOrFind.FIND_PATH);
            if (!filePath.Contains("png")) throw new Exception("This file Path is not a png format ");
            try
            {
                Bitmap bitmap = (Bitmap)Image.FromFile(filePath);
                return bitmap;
            }
            catch (Exception ex) 
            { throw new Exception(ex.Message); }
          
            
        }
    }
}
