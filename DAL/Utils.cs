using System.IO; 

namespace DAL
{
    class Utils
    {
        public static string ReadFile(string path)
        {
            using FileStream fileStream = new FileStream(path, FileMode.Open);
            using StreamReader streamReader = new StreamReader(fileStream);

            return streamReader.ReadToEnd();
        }
    }
}
