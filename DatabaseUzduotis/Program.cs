using System.IO;
using mFile = System.IO.File;
namespace DatabaseUzduotis
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            using var dbContext = new FilesDbContext();
            string path = Console.ReadLine();
           

            dbContext.Folders.AddRange(GetFolders(path));
            dbContext.SaveChanges();
        }
        private static List<File> GetFilesInFolders(string folderPath)
        {
            string[] filePaths = Directory.GetFiles(folderPath);

            List<File> files = new List<File>();

            foreach (string filePath in filePaths)
            {
                FileInfo fileInfo = new FileInfo(filePath);
                
                File file = new File();
                file.Name = fileInfo.Name;
                file.FullDirectory = fileInfo.FullName;
                file.Size = fileInfo.Length;

                files.Add(file);
            }
            return files;
        }
        private static List<Folder> GetFolders (string path)
        {
            Folder folder = new Folder();
            folder.Name = path;
            folder.Files = GetFilesInFolders(path);
            List<Folder> folders = new List<Folder>(); 
            folders.Add(folder);
            string[] directoryPaths = Directory.GetDirectories(path);
            
            foreach (string directoryPath in directoryPaths)
            {
                folders.AddRange(GetFolders(directoryPath));
            }
            return folders;
        }
    }
}