
namespace DigitalLibrary.Application.Common.Interfaces.Utilities
{
    public interface IFileManagement
    {
        public string[] GetAllFiles(string path);
        public (string, string) GetFileById(string id, string path);
        public void AddFile(string fileName, MemoryStream ms);
        public bool DeleteFile(string id, string[] files);
        public int GetMaxId(string path);
    }
}
