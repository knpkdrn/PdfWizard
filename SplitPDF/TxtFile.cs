using System;
using System.IO;

namespace SplitPDF
{
    public class TxtFile
    {
        public string Path {  get; set; }
        public string ContentInSingleString { get; set; }
        public string[] ContentInArray { get; set; }

        public TxtFile(string path)
        {
            Path = path;
        }

        public TxtFile(string path, string content)
        {
            Path = path;
            ContentInSingleString = content;
        }

        public TxtFile(string path, string[] content)
        {
            Path = path;
            ContentInArray = content;
        }

        public bool CreateFile()
        {
            if (File.Exists(Path))
                return false;

            else
            {
                using (var stream = File.Create(Path)) { }
                
                if (ContentInSingleString != null)
                {
                    File.WriteAllText(Path, ContentInSingleString);
                    return true;
                }
                else if (ContentInArray != null)
                {
                    File.WriteAllLines(Path, ContentInArray);
                    return true;
                }
            }

            Console.WriteLine("Error creating the file ");
            return false;
        }
    }
}