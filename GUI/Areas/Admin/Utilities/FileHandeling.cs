using System;
using System.IO;

namespace GUI.Areas.Admin.Utilities
{
    public static class FileHandeling
    {
        public static bool SaveFile(string NameWithPath, byte[] arry)
        {
            BinaryWriter Writer = null;
            Writer = new BinaryWriter(File.OpenWrite(NameWithPath));
            // Writer raw data                
            Writer.Write(arry);
            Writer.Flush();
            Writer.Close();
            return true;
        }
        public static string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 10)
                      + Path.GetExtension(fileName);
        }
        public static byte[] ImageToByteArrayFromFilePath(string imagefilePath)
        {
            byte[] imageArray = File.ReadAllBytes(imagefilePath);
            return imageArray;
        }
    }
}
