using System;
using System.IO;
using System.Text;

namespace WordGuess
{
    public class Program
    {
        public static readonly string path = "../../../../../words.txt";
        static readonly Random random = new Random();

        /// <summary>
        /// Creates words.txt with a set of default words if it doesn't already exist.
        /// </summary>
        public static void CreateFile()
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
        /// Gets all of the words in words.txt.
        /// </summary>
        /// <returns>An array with all of the words</returns>
        public static string[] ReadFile()
        {
            return File.ReadAllLines(path);
        }

        /// <summary>
        /// Adds a word to words.txt.
        /// </summary>
        /// <param name="line">The word being added</param>
        public static void UpdateFile(string line)
        {
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(line);
            }
        }

        /// <summary>
        /// Delestes words.txt.
        /// </summary>
        public static void DeleteFile()
        {
            File.Delete(path);
        }

        /// <summary>
        /// Displays all of the words in words.txt.
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
        /// Checks if a given string appears in a given array of strings.
        /// </summary>
        /// <param name="array">The array being searched.</param>
        /// <param name="word">The string being searched for.</param>
        /// <returns>True or false depending on if the word is in the array.</returns>
        public static bool ArrayHasWord(string[] array, string word)
        {
            foreach (string element in array)
            {
                if (word == element) return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if a word contains any non-letter characters.
        /// </summary>
        /// <param name="word">The word being checked.</param>
        /// <returns>True or false depending on if the word has symbols.</returns>
        static bool WordHasSymbols(string word)
        {
            foreach (char c in word)
            {
                if (!Char.IsLetter(c)) return true;
            }
            return false;
        }

        /// <summary>
        /// Adds a new word to the game from user input.
        /// </summary>
        static void NewWord()
        {
            Console.Write("Enter a new word (length: 3-16): ");
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
        /// Deletes a single word from the game.
        /// </summary>
        static void DeleteWord()
        {
            string[] words = ReadFile();
            Console.Write("Word to delete: ");
            string toDelete = Console.ReadLine().ToUpper();
            if (!ArrayHasWord(words, toDelete))
            {
                Console.WriteLine("This word is not in the game!");
            }
            else
            {
                DeleteFile();
                using (StreamWriter sw = new StreamWriter(path))
                {
                    foreach (string word in words)
                    {
                        if (word != toDelete) sw.WriteLine(word);
                    }
                }
                Console.WriteLine("Word removed.");
            }
            PressAnyButton();
        }

        /// <summary>
        /// Prompts the user to press a button to continue.
        /// </summary>
        static void PressAnyButton()
        {
            Console.WriteLine("Press any button to continue...");
            Console.ReadKey();
        }

        /// <summary>
        /// Handles main menu navigation.
        /// </summary>
        /// <returns>True unless the user chooses to quit the game.</returns>
        static bool MainMenu()
        {
            Console.Clear();
            Console.WriteLine("Welcome to WordGuess!\nPlease select an option.\n");
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
        /// Handles admin menu navigation.
        /// </summary>
        /// <returns>True unless the user chooses to exit the menu.</returns>
        static bool AdminMenu()
        {
            Console.Clear();
            Console.WriteLine("WordGuess Admin Menu\nPlease select an option.\n");
            Console.WriteLine("1. View Words\n2. Add A Word\n3. Delete A Word\n4. Reset Words\n5. Main Menu\n");
            Console.Write("Your choice: ");
            switch (GetNumber(5))
            {
                case 1: DisplayWords();
                    return true;
                case 2: NewWord();
                    return true;
                case 3: DeleteWord();
                    return true;
                case 4: Reset();
                    return true;
                default: return false;
            }
        }

        /// <summary>
        /// Deletes words.txt and creates it again.
        /// </summary>
        static void Reset()
        {
            Console.WriteLine("WARNING: THIS WILL RESET THE GAME TO ITS DEFAULT STATE!");
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
        /// Gets a yes or no response from the user.
        /// </summary>
        /// <param name="message">The prompt given to the user.</param>
        /// <returns>True or false depending on what the user chooses.</returns>
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
        /// Gets a number in between 1 and a given maximum from the user.
        /// </summary>
        /// <param name="max">The maximum allowed number.</param>
        /// <returns>The number entered by the user.</returns>
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
        /// Retrieves a random word from words.txt.
        /// </summary>
        /// <returns>A random word.</returns>
        static string RandomWord()
        {
            string[] words = ReadFile();
            return words[random.Next(0, words.Length)];
        }

        /// <summary>
        /// Handles the word guessing game.
        /// </summary>
        static void Game()
        {
            string word = RandomWord();
            string guessed = "";
            string blanked = BlankWord(word, guessed);
            while (blanked.Contains("_"))
            {
                DisplayGame(blanked, guessed);
                guessed += GetLetter(guessed);
                blanked = BlankWord(word, guessed);
            }
            DisplayGame(blanked, guessed);
            Console.WriteLine("You got it!");
            PressAnyButton();
        }

        /// <summary>
        /// Displays game information.
        /// </summary>
        /// <param name="blanked">The word being guessed with non-guessed letters blanked out.</param>
        /// <param name="guessed">The letters already entered by the user.</param>
        static void DisplayGame(string blanked, string guessed)
        {
            Console.Clear();
            Console.WriteLine(blanked);
            DisplayGuessed(guessed);
        }

        /// <summary>
        /// Displays previous guesses.
        /// </summary>
        /// <param name="guessed">The letters already entered by the user.</param>
        static void DisplayGuessed(string guessed)
        {
            Console.Write("Guessed: ");
            foreach (char c in guessed)
            {
                Console.Write($"{c} ");
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Formats a word so that non-guessed characters are blanked out.
        /// </summary>
        /// <param name="word">The word being formatted.</param>
        /// <param name="guessed">The letters already entered by the user.</param>
        /// <returns>The formatted word.</returns>
        public static string BlankWord(string word, string guessed)
        {
            string output = "";
            foreach (char c in word)
            {
                output += $"{(guessed.Contains(c) ? c : "_"[0])} ";
            }
            return output.Substring(0, output.Length-1);
        }

        /// <summary>
        /// Gets a single letter from the user.
        /// </summary>
        /// <param name="guessed">The letters already entered by the user.</param>
        /// <returns>A letter.</returns>
        static char GetLetter(string guessed)
        {
            string input;
            while (true)
            {
                Console.Write("Pick a letter: ");
                input = Console.ReadLine().ToUpper();
                if (input.Length > 1 || WordHasSymbols(input))
                {
                    Console.WriteLine("Invalid input. Please enter a single letter.");
                }
                else if (guessed.Contains(input[0]))
                {
                    Console.WriteLine("This letter has already been guessed!");
                }
                else return input[0];
            }
        }

        static void Main(string[] args)
        {
            CreateFile();
            while (MainMenu()) ;
        }
    }
}
