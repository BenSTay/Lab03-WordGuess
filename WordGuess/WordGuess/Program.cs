using System;
using System.IO;
using System.Text;

namespace WordGuess
{
    public class Program
    {
        static readonly string path = "../../../../../words.txt";
        static readonly Random random = new Random();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        static void CreateFile()
        {
            if (!File.Exists(path))
            {
                using (StreamWriter sw = new StreamWriter(path))
                {
                    string[] content = new string[] { "CAT", "DOG", "BEAR", "SEAL", "DREAM", "WATER", "HEIGHT", "PLAYER", "FEELING", "NEAREST" };
                    foreach (string line in content)
                    {
                        sw.WriteLine(line);
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        static string[] ReadFile()
        {
            return File.ReadAllLines(path);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="line"></param>
        static void UpdateFile(string line)
        {
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(line);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        static void DeleteFile()
        {
            File.Delete(path);
        }

        /// <summary>
        /// 
        /// </summary>
        static void DisplayWords()
        {
            Console.Clear();
            Console.WriteLine("Here are the words currently in the game:");
            string[] words = ReadFile();
            foreach (string word in words)
            {
                Console.WriteLine(word);
            }
            PressAnyButton();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="word"></param>
        /// <returns></returns>
        static bool ArrayHasWord(string[] array, string word)
        {
            foreach (string element in array)
            {
                if (word == element) return true;
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        static bool WordHasSymbols(string word)
        {
            foreach (char c in word)
            {
                if (!Char.IsLetter(c)) return true;
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        static void NewWord()
        {
            Console.Write("Enter a new word: ");
            string[] allWords = ReadFile();
            string newWord = Console.ReadLine().ToUpper();
            if (newWord.Length <= 2) Console.WriteLine("This word is too short!");
            else if (newWord.Length > 16) Console.WriteLine("This word is too long!");
            else
            {
                if (WordHasSymbols(newWord))
                {
                    Console.WriteLine("Words should only contain letters!");
                }
                else if (ArrayHasWord(allWords, newWord))
                {
                    Console.WriteLine("This word is already in the game!");
                }
                else
                {
                    UpdateFile(newWord);
                    Console.WriteLine("Word added successfully!");
                }
            }
            PressAnyButton();
        }

        /// <summary>
        /// 
        /// </summary>
        static void PressAnyButton()
        {
            Console.WriteLine("Press any button to continue...");
            Console.ReadKey();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        static bool MainMenu()
        {
            Console.Clear();
            Console.WriteLine("Welcome to WordGuess!\nPlease select an option?\n");
            Console.WriteLine("1. Play Game\n2. Admin\n3. Exit\n");
            Console.Write("Your choice: ");
            switch (GetNumber(3))
            {
                case 1: Game();
                    return true;
                case 2: while (AdminMenu()) ;
                    return true;
                default: return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        static bool AdminMenu()
        {
            Console.Clear();
            Console.WriteLine("WordGuess Admin Menu\nPlease select an option.\n");
            Console.WriteLine("1. View Words\n2. Add A Word\n3. Reset Words\n4. Exit\n");
            Console.Write("Your choice: ");
            switch (GetNumber(4))
            {
                case 1: DisplayWords();
                    return true;
                case 2: NewWord();
                    return true;
                case 3: Reset();
                    return true;
                default: return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        static void Reset()
        {
            Console.WriteLine("WARNING: THIS WILL DELETE EVERY WORD YOU HAVE ADDED TO THE GAME!");
            string message = "Are you sure? (Y/N): ";
            if (YesOrNo(message))
            {
                DeleteFile();
                CreateFile();
                Console.WriteLine("Words Reset.");
                PressAnyButton();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        static bool YesOrNo(string message)
        {
            string input;
            while (true)
            {
                Console.Write(message);
                input = Console.ReadLine().ToUpper();
                switch (input)
                {
                    case "Y": return true;
                    case "N": return false;
                    default:
                        Console.WriteLine("Invalid Input.");
                        break;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="max"></param>
        /// <returns></returns>
        static int GetNumber(int max)
        {
            while (true)
            {
                try
                {
                    int input = Convert.ToInt32(Console.ReadLine());
                    if (input > max || input < 1)
                    {
                        Console.WriteLine($"Invalid input. Please choose a number between 1 and {max}.");
                    }
                    else return input;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="words"></param>
        /// <returns></returns>
        static string RandomWord(string[] words)
        {
            return words[random.Next(0, words.Length)];
        }

        static void Game()
        {

        }

        // Display word with non-guessed letters blanked

        // Get a letter

        // Check if letter is in word

        // Check if all letters have been guessed

        static void Main(string[] args)
        {
            CreateFile();
            while (MainMenu()) ;
        }
    }
}
