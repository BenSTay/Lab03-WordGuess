using System;
using System.IO;
using System.Text;

namespace WordGuess
{
    public class Program
    {
        static void CreateFile(string path, string[] content)
        {
            if (!File.Exists(path))
            {
                using (StreamWriter sw = new StreamWriter(path))
                {
                    foreach (string line in content)
                    {
                        sw.WriteLine(line);
                    }
                }
            }
        }

        static string[] ReadFile(string path)
        {
            return File.ReadAllLines(path);
        }

        static void UpdateFile(string path, string line)
        {
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(line.ToUpper());
            }
        }

        static void DeleteFile(string path)
        {
            File.Delete(path);
        }

        static void DisplayWords(string path)
        {
            string[] words = ReadFile(path);
            foreach (string word in words)
            {
                Console.WriteLine(word);
            }
        }

        static void CRUDTest(string path, string[] defaultWords)
        {
            CreateFile(path, defaultWords);
            DisplayWords(path);
            UpdateFile(path, Console.ReadLine());
            DisplayWords(path);
            Console.ReadKey();
            DeleteFile(path);
        }

        static void Main(string[] args)
        {
            string path = "../../../../../words.txt";
            string[] defaultWords = new string[] { "DECLARED", "PROPERLY", "COEFFICIENT", "ENIGMATIC", "QUESTIONING" };
            CRUDTest(path, defaultWords);
        }
    }
}
