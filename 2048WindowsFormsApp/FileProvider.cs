using System.IO;
using System.Text;

namespace _2048WindowsFormsApp
{
    class FileProvider
    {        
        public static void Append(string path, string value)
        {
            using (var writer = new StreamWriter(path, true, Encoding.UTF8))
            {
                writer.WriteLine(value);
            }
        }
        public static string Get(string path)
        {
            var reader = new StreamReader(path, Encoding.UTF8);
            var fileData = reader.ReadToEnd();
            reader.Close();
            return fileData;
        }
        public static void Replase(string path, string value)
        {
            using (var writer = new StreamWriter(path, false, Encoding.UTF8))
            {
                writer.WriteLine(value);
            }
        }
    }
}
