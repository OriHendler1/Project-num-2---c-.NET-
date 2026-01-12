using System;

namespace A26_Ex02_OriHendler_211676119_MayBelo_322758954
{
    class UserInterface
    {
        public enum columnLetters
        {
            A = 0,
            B,
            C,
            D,
            E,
            F,
            G,
            H
        }
        private static bool CheckRowAndColumnNum(string i_num)
        {
            bool v_isValid = true;
            int i_numInt = int.Parse(i_num);
            if (i_numInt >= 4 && i_numInt <= 8)
            {
                return v_isValid;
            }
            return !v_isValid;
        }
        private static bool CheckPlayersNumber(int i_num)
        {
            bool v_isValid = true;
            if (i_num >= 1 && i_num <= 2)
            {
                return v_isValid;
            }
            return !v_isValid;
        }
        public static string[,] CreateNewGameBoard(string rows, string columns)
        {
            int rowsInt = int.Parse(rows);
            int columnsInt = int.Parse(columns);
            string[,] gameBoard = new string[rowsInt, columnsInt];
            UserInterface.PrintGameBoard(ref gameBoard);
            return gameBoard;
        }
        public static void StartNewGame()
        {
            Player player1 = new Player(0);
            Player player2 = new Player(0);
            Console.WriteLine("Welcom to 4 in a Row Game!\nPlease enter number of Rows (number must be between 4 and 8)");
            string numOfRows = System.Console.ReadLine();
            while (UserInterface.CheckRowAndColumnNum(numOfRows) == false)
            {
                Console.WriteLine("Please enter number of Rows (number must be between 4 and 8)");
                numOfRows = System.Console.ReadLine();
            }
            Console.WriteLine("Please enter number of columns (number must be between 4 and 8)");
            string numOfColumns = System.Console.ReadLine();
            while (UserInterface.CheckRowAndColumnNum(numOfColumns) == false)
            {
                Console.WriteLine("Please enter number of columns (number must be between 4 and 8)");
                numOfColumns = System.Console.ReadLine();
            }
            Console.WriteLine("Please enter number of players (1 or 2)");
            int numOfPlayers = int.Parse(System.Console.ReadLine());
            while (UserInterface.CheckPlayersNumber(numOfPlayers) == false)
            {
                Console.WriteLine("Please enter number of players (1 or 2)");
                numOfPlayers = int.Parse(System.Console.ReadLine());
            }
            string [,] gameBoard = UserInterface.CreateNewGameBoard(numOfRows, numOfColumns);
            if (numOfPlayers == 1)
            {
                GameLogic.OnePlayerGame(ref gameBoard, numOfColumns, numOfPlayers);
            }
            else 
            {
                GameLogic.TwoPlayersGame(ref gameBoard, numOfColumns, numOfPlayers);
            }
        }
        public static void PrintGameBoard(ref string[,] io_GameBoard)
        {
            Console.Clear();
            int rows = io_GameBoard.GetLength(0);
            int columns = io_GameBoard.GetLength(1);
            Console.Write("  ");
            for (int j = 0; j < columns; j++)
            {
                Console.Write($"  {(char)('A' + j)}   ");
            }
            Console.WriteLine();
            for (int i = 0; i < rows; i++)
            {
                Console.Write(" ");
                for (int j = 0; j < columns; j++)
                {
                    //print cell content or space if empty
                    string cellContent = string.IsNullOrEmpty(io_GameBoard[i, j]) ? " " : io_GameBoard[i, j];
                    Console.Write($"|  {cellContent}  ");
                }
                Console.WriteLine("|");
                Console.Write(" ");
                for (int j = 0; j < columns; j++)
                {
                    Console.Write("======");
                }
                Console.WriteLine("=");
            }
        }
        public static columnLetters GetColumnFromPlayer(int i_numOfColumns)
        {
            //get letter from user
            Console.WriteLine("Please enter the letter of the column:");
            string colLetter = Console.ReadLine().ToUpper();
            //check letter
            bool v_colLetterValid = char.TryParse(colLetter, out char o_resultChar);
            if (o_resultChar == 'Q')
            { 
                
            }
            while (!v_colLetterValid || ((o_resultChar - 'A') >= i_numOfColumns))
            { 
                Console.WriteLine("Letter is Invalid. \nPlease enter the letter of the column:");
                colLetter = Console.ReadLine().ToUpper();
                v_colLetterValid = char.TryParse(colLetter, out o_resultChar);
            }
            UserInterface.columnLetters Letter = (columnLetters)(o_resultChar - 'A');
            return Letter;
        }
    
    }
}
