using System;

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
            ManageGame();
        }

        // Main Game
        public static void ManageGame()
        {
            int[] highScores = new int[3];
            string[] highScoreUsers = new string[3];
            for (int i = 0; i <= 2; i++)
            {
                highScores[i] = -1;
                highScoreUsers[i] = "";
            }
            int startValue = PreGameMsg();
            while (startValue == 2) 
            {
                Console.WriteLine();
                Console.WriteLine("No Scores Yet!");
                Console.WriteLine();
                startValue = PreGameMsg();
            }
            bool gameOngoing = true;
            while (startValue == 1)
            { 
                Random rnd = new Random();
                string[] celebs = new string[GetFamousCelebrities().Length];
                celebs = GetFamousCelebrities();
                string toGuess = celebs[rnd.Next(0, celebs.Length - 1)];
                char[] guessArr = new char[toGuess.Length];
                gameOngoing = true;
                for (int i = 0; i < guessArr.Length; i++)
                    guessArr[i] = toGuess[i];
                int failRemain = 5, points = 0;
                string guessed = "";

                /*// Debug Code
                for (int i = 0; i < guessArr.Length; i++)
                    Console.Write($"|{guessArr[i]}|");
                Console.WriteLine();
                //*/

                Console.WriteLine();
                Console.Write("Enter UserName: ");
                string userName = Console.ReadLine();
                Console.Clear();

                while (gameOngoing && failRemain > 0 && !LetterGuessWin(guessArr, points))
                {
                    int pickedOpt = Round(failRemain, points, guessArr, guessed, userName);
                    if (pickedOpt == 1)
                    {
                        bool charPreGuessed = false;
                        Console.Write("\nEnter Char: ");
                        char guessChar = char.Parse(Console.ReadLine());
                        for (int i = 0; i < guessed.Length; i++)
                            if (guessed[i] == guessChar)
                                charPreGuessed = true;
                        while (charPreGuessed)
                        {
                            int whileCounterFlag = 0;
                            Console.WriteLine("Letter Was Already Guessed, Enter New Letter:");
                            guessChar = char.Parse(Console.ReadLine());
                            for (int i = 0; i < guessed.Length; i++)
                            {
                                if (guessed[i] == guessChar)
                                    whileCounterFlag++;
                            }
                            if (whileCounterFlag <= 0)
                                charPreGuessed = false;
                        }
                        if (GuessChar(guessChar, guessArr))
                        {
                            guessed += $"{guessChar}, ";
                            points += Scoring(guessChar, guessArr);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"Correct! The Letter '{guessChar}' Was Found {Scoring(guessChar, guessArr) / 50} Times, You Get {Scoring(guessChar, guessArr)} Points");
                            Console.ResetColor();
                            if (LetterGuessWin(guessArr, points))
                            {
                                WinMsg(points);
                                Console.Write("To Continue Enter 1: ");
                                if (Console.ReadLine() == "1")
                                    Console.Clear();
                                else
                                    System.Environment.Exit(1);
                                gameOngoing = false;
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("WRONG :'(");
                            Console.WriteLine($"Remaining Fails: {--failRemain}");
                            guessed += $"{guessChar}, ";
                            Console.ResetColor();
                        }
                    }
                    else if (pickedOpt == 2)
                    {
                        Console.Write("Enter Guess: ");
                        if (Console.ReadLine() != toGuess)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("WRONG :'(");
                            Console.WriteLine($"Remaining Fails: {--failRemain}");

                            Console.ResetColor();
                        }
                        else
                        {
                            points += 100;
                            WinMsg(points);
                            Console.Write("To Continue Enter 1: ");
                            if (Console.ReadLine() == "1")
                                Console.Clear();
                            else
                                System.Environment.Exit(1);
                            gameOngoing = false;
                        }
                    }
                    else
                    {
                        if (points < 150)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Not Enough Poins!");
                            Console.WriteLine($"You Need {150 - points} More Point/s To Use!");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"The Most Frequent Letter {MostCommonChar(guessArr, guessed)} Has Been Revealed!");
                            guessed += $"{MostCommonChar(guessArr, guessed)}, ";
                            points -= 150;
                        }
                    }
                }
                if (failRemain <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Failed Too Many Times, Game Lost :(");
                    Console.Write($"Answer was:");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($" {toGuess}");
                    Console.ResetColor();
                    Console.Write("To Continue Enter 1: ");
                    if (Console.ReadLine() == "1")
                        Console.Clear();
                    else
                        System.Environment.Exit(1);
                    Console.ResetColor();
                }

                if (points >= highScores[0])
                {
                    highScores[1] = highScores[0];
                    highScoreUsers[1] = highScoreUsers[0];
                    highScores[0] = points;
                    highScoreUsers[0] = userName;
                }
                else if (points >= highScores[1])
                {
                    highScores[2] = highScores[1];
                    highScoreUsers[2] = highScoreUsers[1];
                    highScores[1] = points;
                    highScoreUsers[1] = userName;
                }
                else if (points >= highScores[2])
                {
                    highScores[2] = points;
                    highScoreUsers[2] = userName;
                }
                
                startValue = PreGameMsg();

                if (startValue == 2)
                {
                    HighScore(highScoreUsers, highScores);
                    Console.Write("Enter 1 To Continue | 2 To Close Game: ");
                    if (Console.ReadLine() == "1")
                    {
                        Console.Clear();
                        startValue = PreGameMsg();
                    }
                }
            }
            System.Environment.Exit(1);
        }

        /// <summary>
        /// Main Func Methods
        /// </summary>

        // Count Letter Found
        public static int Scoring(char c, char[] arr)
        {
            int score = 0;
            for (int i = 0; i < arr.Length; i++)
                if (arr[i] == c)
                    score++;
            return score * 50;
        }

        // Dictate Win By Char Guess
        public static bool LetterGuessWin(char[] arr, int score)
        {
            int spaceCount = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == ' ')
                    spaceCount++;
            }
            if (arr.Length - spaceCount == score / 50)
                return true;
            return false;
        }

        // Guess Char
        public static bool GuessChar(char c, char[] guessArr)
        {
            for (int i = 0; i < guessArr.Length; i++)
                if (guessArr[i] == c)
                    return true;
            return false;
        }

        // Guess Conversion Str
        public static string GuessString(char[] guessArr, string guessed)
        {
            string toGuess = "";
            if (guessed.Length <= 0)
            {
                for (int i = 0; i < guessArr.Length; i++)
                {
                    if (guessArr[i] != ' ')
                        toGuess += "_ ";
                    else
                        toGuess += " ";
                }
                return toGuess;
            }
            for (int i = 0; i < guessArr.Length; i++)
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

        public static char MostCommonChar(char[] arr, string guessed)
        {
            char mostFreq = ' ';
            int maxCount = 0;
            for (int i = 0; i < arr.Length;i++)
            {
                char current = arr[i];
                bool isGuessed = false;
                for (int j = 0; j < guessed.Length; j++)
                {
                    char guessChar = guessed[j];
                    if (guessChar != ',' && guessChar != ' ' && current == guessChar)
                        isGuessed = true;

                }

                if (!isGuessed)
                {
                    int count = 0;
                    for (int k  = 0; k < arr.Length; k++)
                        if(arr[k] == current) 
                            count++;
                    if (count > maxCount)
                    {
                        maxCount = count;
                        mostFreq = current;
                    }
                }
            }
            return mostFreq;
        }

        /// <summary>
        /// Messages:
        /// </summary>
        
        // Start Msg
        public static int PreGameMsg()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("=================================================");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("                   GUESS WHO");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("=================================================");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine();
            Console.WriteLine("Enter 1 To Start | 2 To See High Score | 3 To Close Game");
            string entry = Console.ReadLine();
            if (entry == "1")
            {
                Console.ResetColor();
                return 1;
            }
            if (entry == "2")
            {
                Console.ResetColor();
                return 2;
            }
            return 3;
        }

        // Round Msg
        public static int Round(int failRemain, int points, char[] guessArr, string guessed, string user)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"Guess: {GuessString(guessArr, guessed)}\n");
            Console.WriteLine();
            Console.WriteLine($"| Score: {points} | Remaining Fails: {failRemain} | Guessed: {guessed} | UserName: {user} |");
            Console.ResetColor();
            Console.Write("To Guess A Character Enter 1, To Guess Name Enter 2, At A Cost of 150 Score Reveal Most Common Letter Enter  3: ");
            string entered = Console.ReadLine();
            if (entered == "1")
                return 1;
            if (entered == "2")
                return 2;
            if (entered == "3")
                return 3;
            else
                System.Environment.Exit(1);
            return -1;
        }

        // Win Msg
        public static void WinMsg(int score)
        {
            Console.WriteLine("\n");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("===============================\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("YOU WIN!!");
            Console.WriteLine($"Your Final Score is: {score}\n");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("===============================\n\n");
        }

        // HighScore Msg
        public static void HighScore(string[] userArr, int[] scoreArr)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Clear();
            Console.WriteLine("================ HIGH SCORES ================");
            Console.WriteLine();
            for (int i = 0; i < userArr.Length; i++)
            {
                if (scoreArr[i] > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write($"| {userArr[i]} : ");
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine($"{scoreArr[i]} |");
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
            }
            Console.WriteLine("=============================================");
        }
    }
}
