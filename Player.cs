using A26_Ex02_OriHendler_211676119_MayBelo_322758954;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A26_Ex02_OriHendler_211676119_MayBelo_322758954
{
    class Player
    {
        private int m_playerNum = 0;
        private int m_playerPoints = 0;
        public Player(int i_playerNum,int i_playerPoints)
        {
            m_playerNum=i_playerNum;
            m_playerPoints = i_playerPoints;
        }

        public static void WinPlayer(Player i_player)
        {
            i_player.m_playerPoints++;
        }
        public static int GetPlayerPoints(Player i_player)
        { 
            return i_player.m_playerPoints;
        }
    }
}
