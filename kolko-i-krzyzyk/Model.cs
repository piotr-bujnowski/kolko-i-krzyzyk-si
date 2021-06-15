using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kolko_i_krzyzyk
{
    class Model
    {
        public string checkRowColWin(Button[,] cellArray)
        {
            string whoWins = "";

            for (int i = 0; i < cellArray.GetLength(0); i++)
            {
                int horizontal = 0;
                int vertical = 0;

                for (int j = 0; j < cellArray.GetLength(1); j++)
                {
                    horizontal += cellArray[i, j].Text[0];
                    vertical += cellArray[j, i].Text[0];

                    whoWins = checkWin(horizontal, vertical, whoWins);
                }
            }
            return whoWins;
        }

        public string checkForCrossWins(Button[,] cellArray)
        {
            string whoWins = "";

            int leftCross = cellArray[0, 0].Text[0]
                          + cellArray[1, 1].Text[0]
                          + cellArray[2, 2].Text[0];

            int rightCross = cellArray[0, 2].Text[0]
                           + cellArray[1, 1].Text[0]
                           + cellArray[2, 0].Text[0];

            return checkWin(leftCross, rightCross, whoWins);
        }
        

        public string checkWin(int horizontal, int vertical, string whoWins)
        {
            if (horizontal == 264 || vertical == 264)
            {
                whoWins = Mark.X.ToString();
            }
            else if (horizontal == 237 || vertical == 237)
            {
                whoWins = Mark.O.ToString();
            }
            return whoWins;
        }

    }

    
}
