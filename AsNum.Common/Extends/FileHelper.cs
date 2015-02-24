using System;
using System.IO;

namespace AsNum.Common.Extends {
    public static class FileHelper {

        public static string GetMIMEType(this string file) {
            string mimeType = "application/unknown";
            string ext = System.IO.Path.GetExtension(file).ToLower();
            Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
            if(regKey != null && regKey.GetValue("Content Type") != null)
                mimeType = regKey.GetValue("Content Type").ToString();
            return mimeType;
        }

        /// <summary>
        /// <remarks>
        /// File.ReadAllBytes 在多线程同时读取同一文件的时候，会报 IOException
        /// </remarks>
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static byte[] ReadAllBytes(this string path) {

            if(!File.Exists(path))
                throw new FileNotFoundException(path);

            byte[] bytes;
            using(FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)) {
                int index = 0;
                long fileLength = fs.Length;
                if(fileLength > Int32.MaxValue)
                    throw new IOException("File too long");
                int count = (int)fileLength;
                bytes = new byte[count];
                while(count > 0) {
                    int n = fs.Read(bytes, index, count);
                    if(n == 0)
                        throw new InvalidOperationException("End of file reached before expected");
                    index += n;
                    count -= n;
                }
            }
            return bytes;
        }
    }
}
