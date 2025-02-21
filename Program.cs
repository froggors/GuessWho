using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace GuessWho
{
    internal class Program
    {
        private static string[] GetFamousCelebrities()
        {
            string[] celebrities =
            {
                "noa kirel",
                "eden golan",
                "static",
                "ben el tavori",
                "omer adam",
                "neta barzilai",
                "hanan ben ari",
                "eden ben zaken",
                "eyal golan",
                "moshe peretz",
                "gal gadot",
                "natalie portman",
                "lior raz",
                "shira haas",
                "rotem sela",
                "yehuda levi",
                "guy zo-aretz",
                "deni avdija",
                "eran zahavi",
                "manor solomon",
                "bibras natcho",
                "itamar ben gvir",
                "yair lapid",
                "benjamin netanyahu",
                "naftali bennett",
                "gadi eizenkot",
                "aviv kochavi",
                "assi azar",
                "bar refaeli",
                "yael shelbia",
                "michael aloni",
                "tzachi halevy",
                "ninet tayeb",
                "shlomi shabat",
                "idan raichel",
                "amir dadon",
                "keren peles",
                "yuval dayan",
                "dana international",
                "ivri lider",
                "matti caspi",
                "david broza",
                "gal toren",
                "eliana tidhar",
                "oshri cohen",
                "adi ashkenazi",
                "yarden harel",
                "shlomi koriat",
                "guri alfi",
                "asi cohen",
                "tamir bar",
                "eli finish",
                "yonatan mergui"
            };

            return celebrities;
        }
        static void Main(string[] args)
        {
            bool gameOngoing = true;
            while (PreGame())
            {
                gameOngoing = true;
                Random rnd = new Random();
                string[] celebs = new string[GetFamousCelebrities().Length];
                celebs = GetFamousCelebrities();
                string toGuess = celebs[rnd.Next(0, celebs.Length - 1)];
                char[] guessArr = new char[toGuess.Length];
                for (int i = 0; i < guessArr.Length; i++)
                    guessArr[i] = toGuess[i];
                int failRemain = 5, points = 0;
                string guessed = "";
                // A
                for (int i = 0; i < guessArr.Length; i++)
                    Console.Write($"|{guessArr[i]}|");
                Console.WriteLine();
                // A
                while (gameOngoing && failRemain > 0) 
                {
                    if (Round(failRemain, points, guessArr, guessed) == 1)
                    {
                        bool charPreGuessed = false;
                        Console.Write("\nEnter Char: ");
                        char guessChar = char.Parse(Console.ReadLine());
                        for (int i = 0; i < guessed.Length; i++)
                            if (guessed[i] == guessChar)
                                charPreGuessed = true;
                        while (charPreGuessed)
                        {
                            Console.WriteLine("Already Guessed, Enter New Char:");
                            guessChar = char.Parse(Console.ReadLine());
                            for (int i = 0; i < guessed.Length; i++)
                                if (guessed[i] == guessChar)
                                    charPreGuessed = true;
                        }
                        if (GuessChar(guessChar, guessArr))
                        {
                            guessed += $"{guessChar}, ";
                            points += Scoring(guessChar, guessArr);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"Correct! The Letter '{guessChar}' Was Found {Scoring(guessChar, guessArr) / 50} Times, You Get {Scoring(guessChar, guessArr)} Points");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("WRONG :'(");
                            Console.ResetColor();
                            failRemain--;
                        }
                    }
                    else
                    {
                        Console.Write("Enter Guess: ");
                        if (Console.ReadLine() != toGuess)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("WRONG :'(");
                            Console.ResetColor();
                            failRemain--;
                        }
                        else
                        {
                            points += 100;
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.WriteLine("===============================");
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                            Console.WriteLine("YOU WIN!!");
                            Console.WriteLine($"Your Final Score is: {points}\n\n");
                            gameOngoing = false;
                        }
                    }
                }
                if (failRemain <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Failed To Many Times, Game Lost :(\n\n");
                    Console.ResetColor();
                }
            }
            System.Environment.Exit(1);
        }

        // Count Letter found
        public static int Scoring(char c, char[] arr)
        {
            int score = 0;
            for (int i = 0; i < arr.Length;i++)
                if (arr[i] == c)
                    score++;
            return score * 50;
        }

        public static bool LetterGuessWin(char[] arr, int score)
        {
            int spaceCount = 0;
            for 
        }

        // Round Msg
        public static int Round(int failRemain, int points, char[] guessArr, string guessed)
        {
            Console.Write($"Guess: {GuessString(guessArr, guessed)}\n");
            Console.WriteLine($"| Score: {points} | Remaining Fails: {failRemain} | Guessed: {guessed} |");
            Console.Write("To Guess A Character Enter 1, To Guess Name Enter 2: ");
            string entered = Console.ReadLine();
            if (entered == "1")
                return 1;
            else if (entered == "2")
                return 2;
            else
                System.Environment.Exit(1);
            return -1;
        }
        /// Guess Functions:
        // Guess Char
        public static bool GuessChar(char c, char[] guessArr)
        {
            for (int i = 0; i < guessArr.Length; i++)
                if (guessArr[i] == c)
                    return true; 
            return false;
        }

        // Guess Name

        // Guess Conversion Str
        public static string GuessString(char[] guessArr, string guessed)
        {
            string toGuess = "";
            if (guessed.Length <= 0)
            {
                for (int i = 0; i < guessArr.Length;i++)
                {
                    if (guessArr[i] != ' ')
                        toGuess += "_ ";
                    else
                        toGuess += " ";
                }
                return toGuess;
            }
            for (int i = 0; i < guessArr.Length;i++)
            {
                bool contains = false;
                for (int j = 0; j < guessed.Length; j++)
                    if (guessArr[i] == guessed[j])
                        contains = true;
                if (guessArr[i] == ' ')
                    toGuess += " ";
                else if (contains)
                    toGuess += guessArr[i] + " ";
                else
                    toGuess += "_ ";
            }
            return toGuess;
        }

        // Start Msg
        public static bool PreGame()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("=================================================");
            Console.WriteLine("                   GUESS WHO");
            Console.WriteLine("=================================================");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine();
            Console.WriteLine("Enter 1 To Start");
            if (Console.ReadLine() == "1")
            {
                Console.ResetColor();
                return true;
            }
            else
            {
                Console.ResetColor();
                return false;
            }
        } 
    }
}
