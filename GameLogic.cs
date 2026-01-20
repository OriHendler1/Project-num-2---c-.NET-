using System;
using System.Diagnostics.Contracts;
using System.Security.AccessControl;
using static A26_Ex02_OriHendler_211676119_MayBelo_322758954.UserInterface;

namespace A26_Ex02_OriHendler_211676119_MayBelo_322758954
{
    class GameLogic
    {
        private enum players
        {
            X = 1,
            O = 2
        }
        private static void playerAction(ref string[,] io_gameBoard, string i_numOfColumns, int i_playerNumber, ref Player io_player1, ref Player io_player2, int i_numOfPlayers, ref int io_chosenRow, ref int io_chosenCol)
        {
            int winnerPlayerNum = ((i_playerNumber == 1) ? 2 : i_playerNumber);
            int numOfColumnsInt = int.Parse(i_numOfColumns);
            UserInterface.columnLetters colLetter = UserInterface.GetColumnFromPlayer(ref io_gameBoard, numOfColumnsInt, winnerPlayerNum, ref io_player1, ref io_player2, i_numOfPlayers, ref io_chosenRow, ref io_chosenCol);
            while (GameLogic.enterCoinToColumn(ref io_gameBoard, colLetter, i_playerNumber, ref io_chosenRow, ref io_chosenCol) == false)
            {
                Console.WriteLine("Column is full! Please choose another one");
                colLetter = UserInterface.GetColumnFromPlayer(ref io_gameBoard, numOfColumnsInt, winnerPlayerNum, ref io_player1, ref io_player2, i_numOfPlayers, ref io_chosenRow, ref io_chosenCol);
            }
            UserInterface.PrintGameBoard(ref io_gameBoard);
        }
        private static void computerAction(ref string[,] io_gameBoard, string i_numOfColumns, int i_playerNumber, ref int io_chosenRow, ref int io_chosenCol)
        {
            int numOfCol = int.Parse(i_numOfColumns);
            Random randomNum = new Random();
            int randomColNum = randomNum.Next(numOfCol);
            UserInterface.columnLetters enumRamdomCol = (columnLetters)randomColNum;
            GameLogic.enterCoinToColumn(ref io_gameBoard, enumRamdomCol, i_playerNumber, ref io_chosenRow, ref io_chosenCol);
            UserInterface.PrintGameBoard(ref io_gameBoard);
        }
        private static bool enterCoinToColumn(ref string[,] io_gameBoard, UserInterface.columnLetters i_letter, int i_playerNumber, ref int io_chosenRow, ref int io_chosenCol)
        {
            bool v_isCoinEntered = true;
            string playerSign = ((players)i_playerNumber).ToString();
            int colomn = (int)i_letter;
            int row = io_gameBoard.GetLength(0);
            for (int i = row - 1; i >= 0; i--)
            {
                if (string.IsNullOrEmpty(io_gameBoard[i, colomn]))
                {
                    io_gameBoard[i, colomn] = playerSign;
                    io_chosenRow = i;
                    io_chosenCol = colomn;
                    UserInterface.PrintGameBoard(ref io_gameBoard);
                    return v_isCoinEntered;
                }
            }
            return !v_isCoinEntered;
        }
        public static void OnePlayerGame(ref string[,] io_gameBoard, string i_numOfColumns, int i_playerNumber, ref Player io_player1, ref Player io_player2, ref int io_chosenRow, ref int io_chosenCol)
        {
            int numOfPlayers = 1;
            int currentPlayerNum = 1;
            playerAction(ref io_gameBoard, i_numOfColumns, i_playerNumber = currentPlayerNum, ref io_player1, ref io_player2, numOfPlayers, ref io_chosenRow, ref io_chosenCol);
            while (GameLogic.CheckWin(ref io_gameBoard, ref io_chosenRow, ref io_chosenCol, i_playerNumber) == false)
            {
                currentPlayerNum++;
                computerAction(ref io_gameBoard, i_numOfColumns, i_playerNumber = currentPlayerNum, ref io_chosenRow, ref io_chosenCol);
                if (GameLogic.CheckWin(ref io_gameBoard, ref io_chosenRow, ref io_chosenCol, i_playerNumber) == false)
                {
                    currentPlayerNum--;
                    playerAction(ref io_gameBoard, i_numOfColumns, i_playerNumber = currentPlayerNum, ref io_player1, ref io_player2, numOfPlayers, ref io_chosenRow, ref io_chosenCol);
                }
            }
            UserInterface.EndGameMessage(ref io_gameBoard, currentPlayerNum, ref io_player1, ref io_player2, numOfPlayers, ref io_chosenRow, ref io_chosenCol);
        }
        public static void TwoPlayersGame(ref string[,] io_gameBoard, string i_numOfColumns, int i_playerNumber, ref Player io_player1, ref Player io_player2, ref int io_chosenRow, ref int io_chosenCol)
        {            
            int numOfPlayers = 2;
            int currentPlayerNum = 1;
            playerAction(ref io_gameBoard, i_numOfColumns, i_playerNumber = currentPlayerNum, ref io_player1, ref io_player2, numOfPlayers, ref io_chosenRow, ref io_chosenCol);
            while (GameLogic.CheckWin(ref io_gameBoard, ref io_chosenRow, ref io_chosenCol, i_playerNumber) == false)
            {
                currentPlayerNum++;
                playerAction(ref io_gameBoard, i_numOfColumns, i_playerNumber = currentPlayerNum, ref io_player1, ref io_player2, numOfPlayers, ref io_chosenRow, ref io_chosenCol);
                if (GameLogic.CheckWin(ref io_gameBoard, ref io_chosenRow, ref io_chosenCol, i_playerNumber) == false)
                {
                    currentPlayerNum--;
                    playerAction(ref io_gameBoard, i_numOfColumns, i_playerNumber = currentPlayerNum, ref io_player1, ref io_player2, numOfPlayers, ref io_chosenRow, ref io_chosenCol);
                }
            }
            UserInterface.EndGameMessage(ref io_gameBoard, currentPlayerNum, ref io_player1, ref io_player2, numOfPlayers, ref io_chosenRow, ref io_chosenCol);
        }
        public static bool CheckWin(ref string[,] io_gameBoard, ref int io_chosenRow,ref int io_chosenCol, int i_playerNumber)
        {
            bool isValid = true;
            string playerSign = ((players)i_playerNumber).ToString();
            int[][] directions = new int[][] {
                new int[] {0,1}, 
                new int[] {1,0},
                new int[] {1,1},
                new int[] {1,-1}
            };
            foreach (var direction in directions) 
            {
                int count = 1;
                count += GameLogic.countCoinInDirection(ref io_gameBoard,ref io_chosenRow,ref io_chosenCol, direction[0], direction[1], playerSign);
                count += GameLogic.countCoinInDirection(ref io_gameBoard,ref io_chosenRow,ref io_chosenCol, -direction[0], -direction[1], playerSign);
                if (count >= 4)
                { 
                    return isValid;
                }
            }
            return !isValid;
        }
        private static int countCoinInDirection(ref string[,] io_gameBoard,ref int io_chosenRow, ref int io_chosenCol, int i_rowDirection, int i_colDirection, string i_playerSign)
        {
            int rows = (io_gameBoard.GetLength(0));
            int columns = (io_gameBoard.GetLength(1));
            int count = 0;
            int currentRow = io_chosenRow + i_rowDirection;
            int currentCol = io_chosenCol + i_colDirection;
            //check borders and player sign
            while (currentRow >= 0 && currentRow < rows && currentCol >= 0 && currentCol < columns && io_gameBoard[currentRow, currentCol] == i_playerSign) 
            {
                count++;
                currentRow += i_rowDirection;
                currentCol += i_colDirection;
            }
            return count;
        }
        public static void nextRound(ref string[,] io_gameBoard, int i_numOfPlayers, ref Player io_player1, ref Player io_player2, ref int io_chosenRow, ref int io_chosenCol)
        {
            string rows = (io_gameBoard.GetLength(0)).ToString();
            string columns = (io_gameBoard.GetLength(1)).ToString();
            string[,] gameBoard = UserInterface.CreateNewGameBoard(rows, columns);
            if (i_numOfPlayers == 1)
            {
                GameLogic.OnePlayerGame(ref gameBoard, columns, i_numOfPlayers, ref io_player1, ref io_player2, ref io_chosenRow, ref io_chosenCol);
            }
            else
            {
                GameLogic.TwoPlayersGame(ref gameBoard, columns, i_numOfPlayers, ref io_player1, ref io_player2, ref io_chosenRow, ref io_chosenCol);
            }
        }
    }
}          
