
using DigitalLibrary.Application.Common.Interfaces.Utilities;
using DigitalLibrary.Domain;
using System.Reflection;
using System.Text;

namespace DigitalLibrary.Infrastructure.Utilities
{
    public class FileManagement : IFileManagement
    {
        public string[] GetAllFiles(string path)
        {
            return Directory.GetFiles(path);
        }

        public (string, string) GetFileById(string id, string path)
        {
            bool fileFound = false;
            string bookName = string.Empty;
            string filePath = string.Empty;
            string[] files = Directory.GetFiles(path);

            //Search by id
            foreach (string file in files)
            {
                string fileName = Path.GetFileNameWithoutExtension(file);
                string[] tokenizedFileName = fileName.Split('-');
                if (id.Equals(tokenizedFileName[0].Trim()))
                {
                    fileFound = true;
                    bookName = tokenizedFileName[1].Trim();
                    filePath = file;
                }
            }

            if (!fileFound)
                throw new KeyNotFoundException($"No book with id {id} exists");

            //Read UTF8 txt files
            string readContents;
            using (StreamReader streamReader = new StreamReader(filePath, Encoding.UTF8))
            {
                readContents = streamReader.ReadToEnd();
            }
            return (bookName, readContents);
        }


        public void AddFile(string fileName, MemoryStream ms)
        {
            string path = fileName + ".txt";
            string utf8Encoded = Encoding.UTF8.GetString(ms.ToArray());
            File.WriteAllText(path, utf8Encoded, Encoding.UTF8);
        }

        public bool DeleteFile(string id, string[] files)
        {
            bool successfulDeletion = false;
            foreach (string file in files)
            {
                string fileName = Path.GetFileName(file);
                string[] tokenizedFileName = fileName.Split('-');
                if(id.Equals(tokenizedFileName[0].Trim()))
                {
                    File.Delete(file);
                    successfulDeletion = true;
                    break;
                }
            }
            return successfulDeletion;
        }

        public int GetMaxId (string path)
        {
            int maxId = 0;
            string[] files = GetAllFiles(path);
            foreach (string file in files)
            {

                string fileName = Path.GetFileNameWithoutExtension(file);
                string[] tokenizedFileName = fileName.Split('-');
                int currentBookId = int.Parse(tokenizedFileName[0].Trim());
                if (maxId < currentBookId)
                    maxId = currentBookId;
            }

            return maxId;
        }
    }
}