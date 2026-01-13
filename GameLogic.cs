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
        private static void playerAction(ref string[,] io_gameBoard, string i_numOfColumns, int playerNumber, ref Player io_player1, ref Player io_player2, int i_numOfPlayers)
        {
            int winnerPlayerNum = ((playerNumber == 1) ? 2 : playerNumber);
            int numOfColumnsInt = int.Parse(i_numOfColumns);
            UserInterface.columnLetters colLetter = UserInterface.GetColumnFromPlayer(ref io_gameBoard, numOfColumnsInt, winnerPlayerNum, ref io_player1, ref io_player2, i_numOfPlayers);
            if (GameLogic.enterCoinToColumn(ref io_gameBoard, colLetter, playerNumber) == false)
            {
                Console.WriteLine("Column is full! Please choose another one");
                colLetter = UserInterface.GetColumnFromPlayer(ref io_gameBoard,numOfColumnsInt, winnerPlayerNum, ref io_player1, ref io_player2, i_numOfPlayers);
            }
            UserInterface.PrintGameBoard(ref io_gameBoard);
        }
        private static void computerAction(ref string[,] io_gameBoard, string i_numOfColumns, int playerNumber)
        {
            int numOfCol = int.Parse(i_numOfColumns);
            Random randomNum = new Random();
            int randomColNum = randomNum.Next(numOfCol);
            UserInterface.columnLetters enumRamdomCol = (columnLetters)randomColNum;
            GameLogic.enterCoinToColumn(ref io_gameBoard, enumRamdomCol, playerNumber);
            UserInterface.PrintGameBoard(ref io_gameBoard);
        }
        private static bool enterCoinToColumn(ref string[,] io_gameBoard, UserInterface.columnLetters i_letter, int i_playerNumber)
        {
            bool v_isCoinEntered = true;
            string playerSign = ((players)i_playerNumber).ToString();
            int colomn = (int)i_letter;
            int row = io_gameBoard.GetLength(0);
            for (int i = row-1; i >= 0; i--) 
            {
                if (string.IsNullOrEmpty(io_gameBoard[i, colomn]) )
                {
                    io_gameBoard[i, colomn] = playerSign;
                    UserInterface.PrintGameBoard(ref io_gameBoard);
                    return v_isCoinEntered;
                }
            }
            return !v_isCoinEntered;
        }
        public static void OnePlayerGame(ref string[,] io_gameBoard, string i_numOfColumns, int i_playerNumber, ref Player io_player1, ref Player io_player2)
        {
            int numOfPlayers = 1;
            int currentPlayerNum = 1;
            while (GameLogic.CheckWin(ref io_gameBoard) == false)
            {
                playerAction(ref io_gameBoard, i_numOfColumns, i_playerNumber = currentPlayerNum, ref io_player1, ref io_player2, numOfPlayers);
                currentPlayerNum++;
                if (GameLogic.CheckWin(ref io_gameBoard) == false)
                { 
                    computerAction(ref io_gameBoard, i_numOfColumns, i_playerNumber = currentPlayerNum);
                    currentPlayerNum--;
                }
            }
            UserInterface.EndGameMessage(ref io_gameBoard, currentPlayerNum, ref io_player1, ref io_player2, numOfPlayers);
        }
        public static void TwoPlayersGame(ref string[,] io_gameBoard, string i_numOfColumns, int i_playerNumber, ref Player io_player1, ref Player io_player2)
        {
            int numOfPlayers = 2;
            int currentPlayerNum = 1;
            while (GameLogic.CheckWin(ref io_gameBoard) == false)
            {
                playerAction(ref io_gameBoard, i_numOfColumns, i_playerNumber = currentPlayerNum, ref io_player1, ref io_player2, numOfPlayers);
                currentPlayerNum++;
                if (GameLogic.CheckWin(ref io_gameBoard) == false)
                {
                    playerAction(ref io_gameBoard, i_numOfColumns, i_playerNumber = currentPlayerNum, ref io_player1, ref io_player2, numOfPlayers);
                    currentPlayerNum--;
                }
            }
            UserInterface.EndGameMessage(ref io_gameBoard, currentPlayerNum,ref io_player1, ref io_player2, numOfPlayers);
        }
        public static bool CheckWin(ref string[,] io_gameBoard)
        {
            bool isValid = true;

            return !isValid;

        }
        public static void nextRound(ref string[,] io_gameBoard, int i_numOfPlayers, ref Player io_player1, ref Player io_player2)
        {
            string rows = (io_gameBoard.GetLength(0)).ToString();
            string columns = (io_gameBoard.GetLength(1)).ToString();
            string[,] gameBoard = UserInterface.CreateNewGameBoard(rows, columns);
            if (i_numOfPlayers == 1)
            {
                GameLogic.OnePlayerGame(ref io_gameBoard, columns, i_numOfPlayers, ref io_player1, ref io_player2);
            }
            else
            {
                GameLogic.TwoPlayersGame(ref io_gameBoard, columns, i_numOfPlayers, ref io_player1, ref io_player2);
            }
        }
        
    }
}   
