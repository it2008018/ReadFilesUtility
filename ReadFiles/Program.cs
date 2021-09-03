using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace ReadFiles
{
    class Program
    {
        static void Main(string[] args)
        {
            //WriteFile();
            //CheckDLLExists();
            CheckForDestPath();
        }

        private static void CheckForDestPath()
        {
            string pathfile = @"C:\Users\tbhushan\Desktop\Practice_story_test\PracticeDlls.txt";
            string path = @"C:\Users\tbhushan\Desktop\Practice_story_test\DLLs.txt";

            XElement xmlDoc = XElement.Load(@"C:\Users\tbhushan\Desktop\Practice_story_test\ServerBuild.xml");

            


            string[] lines = System.IO.File.ReadAllLines(pathfile);
            foreach (string line in lines)
            {
                var query = from item in xmlDoc.Descendants("file")
                            where (string)item.Attribute("name") == line
                            select item.Elements("destination").Attributes("folder");

                
                Console.WriteLine("-------------------");
                Console.WriteLine(line);
                foreach (var file in query)
                {
                    foreach(string i in file)
                    {
                        Console.WriteLine(i);
                    }
                }
            }
            
            
        }

        private static void CheckDLLExists()
        {
            string folderPath = @"C:\nugetize_ZachClone\Source\target\Debug\AnyCPU\bin";
            string folderPath472 = @"C:\nugetize_ZachClone\Source\target\Debug\AnyCPU\bin\net472";
            string path = @"C:\Users\tbhushan\Desktop\Practice_story_test\DLLs.txt";

            string[] lines = System.IO.File.ReadAllLines(path);
            foreach (string line in lines)
            {
                if(line.EndsWith(".dll"))
                {
                    string fullPath = string.Format("{0}\\{1}", folderPath, line);
                    string fullPath472= string.Format("{0}\\{1}", folderPath472, line);
                    var b = System.IO.File.Exists(fullPath) || System.IO.File.Exists(fullPath472);
                    if(!b)
                    {
                        Console.WriteLine(line);
                    }
                }
                
            }
        }

        private static void WriteFile()
        {
            string path = @"C:\Users\tbhushan\Desktop\Practice_story_test\DLLs.txt";


            XElement xmlDoc = XElement.Load(@"C:\Users\tbhushan\Desktop\Practice_story_test\ServerBuild.xml");

            var query = from item in xmlDoc.Descendants("file")
                            .Where(p => p.Elements("destination")
                                         .Any(c => (string)c.Attribute("folder") == "practice\\practicewebapi\\bin")
                            )
                        select (string)item.Attribute("sourcepath");

            foreach (string file in query)
            {
                int len = file.LastIndexOf("\\") + 1;
                string fileName = file.Substring(len, file.Length - len);
                if (!File.Exists(path))
                {
                    // Create a file to write to.
                    using (StreamWriter sw = File.CreateText(path))
                    {
                        sw.WriteLine(fileName);
                    }
                }
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine(fileName);
                }
            }
        }
    }
}
