using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace kolko_i_krzyzyk
{
    public partial class kolko_i_krzyzyk : Form
    {
        private Button[,] cellArray; // Tablica do kółko i krzyżyk
        private Model model; // Logika kółko i krzyżyk - Model
        private int clickCounter; // Monitorowanie ilości ruchów
        private bool gameEnded = false;
        private Random random;

        public kolko_i_krzyzyk()
        {
            InitializeComponent();
            this.model = new Model();
            this.random = new Random();

            // Dodanie przycisków ( cell ) do tablicy
            cellArray = new Button[3, 3]
            {
                { A1, A2, A3 },
                { B1, B2, B3 },
                { C1, C2, C3 }
            };
            enableAllCells(false);
        }

        // Event Listener dla przycisków (cells)
        private void Cell_Click(object sender, EventArgs e)
        {
            Button cell = (Button)sender;

            if (choiceBtnLeft.Text == Choice.CZŁOWIEK.ToString() && choiceBtnRight.Text == Choice.CZŁOWIEK.ToString())
            {
                if (clickCounter % 2 == 0)
                {
                    cell.Text = Mark.X.ToString();
                    ocupationMsg(cell, Mark.X.ToString());
                }
                else
                {
                    cell.Text = Mark.O.ToString();
                    ocupationMsg(cell, Mark.O.ToString());
                }
                cell.Enabled = false;
                clickCounter += 1;

                predictWins();
                checkWins();
            }
            else if (choiceBtnLeft.Text == Choice.CZŁOWIEK.ToString() && choiceBtnRight.Text == Choice.KOMPUTER.ToString())
            {
                cell.Text = Mark.X.ToString();
                cell.Enabled = false;
                clickCounter += 1;

                checkWins();

                if (!this.gameEnded)
                {
                    insertIntoRandomNotOccupiedCell(Mark.O.ToString());
                    ocupationMsg(cell, Mark.O.ToString());
                    predictWins();
                    clickCounter += 1;
                }
            }
            else if (choiceBtnLeft.Text == Choice.KOMPUTER.ToString() && choiceBtnRight.Text == Choice.CZŁOWIEK.ToString())
            {
                cell.Text = Mark.O.ToString();
                cell.Enabled = false;
                clickCounter += 1;
                
                checkWins();

                if (!this.gameEnded)
                {
                    insertIntoRandomNotOccupiedCell(Mark.X.ToString());
                    ocupationMsg(cell, Mark.X.ToString());
                    predictWins();
                    clickCounter += 1;
                }
            }
        }

        private void predictWins()
        {
            if (model.predictRowColWin(cellArray) == "X"
                    || model.predictForCrossWins(cellArray) == "X")
            {
                messageLbl.Text = "Gracz X wygra\n w następnym ruchu!";
            }
            else if (model.predictRowColWin(cellArray) == "O"
                || model.predictForCrossWins(cellArray) == "O")
            {
                messageLbl.Text = "Gracz O wygra\n w następnym ruchu!";
            }
        }

        private void checkWins()
        {
            if (model.isDraw(clickCounter))
            {
                enableAllCells(false);
                messageLbl.Text = "Remis!";
                messageLbl.Visible = true;
                this.gameEnded = true;
            }

            // Sprawdzenie czy ktoś wygrał
            if (model.checkRowColWin(cellArray) == Mark.X.ToString()
               || model.checkForCrossWins(cellArray) == Mark.X.ToString())
            {
                enableAllCells(false);

                messageLbl.Visible = true;
                messageLbl.Text = "Gracz X wygrał!";
                this.gameEnded = true;
            }
            else if (model.checkRowColWin(cellArray) == Mark.O.ToString()
                || model.checkForCrossWins(cellArray) == Mark.O.ToString())
            {
                enableAllCells(false);

                messageLbl.Visible = true;
                messageLbl.Text = "Gracz O wygrał!";
                this.gameEnded = true;
            }
        }

        // Informacja o zajęziu komórki ( cell )
        private void ocupationMsg(Button btn, String whoseTurn)
        {
            switch (btn.Name)
            {
                case "A1":
                    messageLbl.Text = $"Gracz {whoseTurn} \nzająl górny lewy róg!";
                    break;
                case "A2":
                    messageLbl.Text = $"Gracz {whoseTurn} \nzająl górny środek!";
                    break;
                case "A3":
                    messageLbl.Text = $"Gracz {whoseTurn} \nzająl górny prawy róg!";
                    break;
                case "B1":
                    messageLbl.Text = $"Gracz {whoseTurn} \nzająl lewy środek!";
                    break;
                case "B2":
                    messageLbl.Text = $"Gracz {whoseTurn} \nzająl środek!";
                    break;
                case "B3":
                    messageLbl.Text = $"Gracz {whoseTurn} \nzająl prawy środek!";
                    break;
                case "C1":
                    messageLbl.Text = $"Gracz {whoseTurn} \nzająl lewy róg!";
                    break;
                case "C2":
                    messageLbl.Text = $"Gracz {whoseTurn} \nzająl dolny środek!";
                    break;
                case "C3":
                    messageLbl.Text = $"Gracz {whoseTurn} \nzająl prawy róg!";
                    break;
            }
        }

        // Event Listener dla przycisków wyboru
        private void ChoiceBtns_Click(object sender, EventArgs e)
        {
            Button choiceBtn = (Button)sender;
            if (choiceBtn.Text == Choice.CZŁOWIEK.ToString())
            {
                choiceBtn.Text = Choice.KOMPUTER.ToString();
            }
            else
            {
                choiceBtn.Text = Choice.CZŁOWIEK.ToString();
            }
        }

        // Event Listener dla przycisku START / RESET
        private void startResetBtn_Click(object sender, EventArgs e)
        {
            if (startResetBtn.Text == "WYCZYŚĆ")
            {
                gameEnded = false;
                startResetBtn.Text = "START";
                messageLbl.Text = "Komunikat";
                getCellList().ForEach(c => c.Text = " ");
                enableAllCells(false);
                choiceBtnLeft.Enabled = true;
                choiceBtnRight.Enabled = true;
                messageLbl.Visible = false;
            }
            else if (startResetBtn.Text == "START")
            {
                startResetBtn.Text = "WYCZYŚĆ";
                enableAllCells(true);
                choiceBtnLeft.Enabled = false;
                choiceBtnRight.Enabled = false;
                messageLbl.Visible = true;
                clickCounter = 0;

                if (choiceBtnLeft.Text == Choice.KOMPUTER.ToString() && choiceBtnRight.Text == Choice.CZŁOWIEK.ToString())
                {
                    insertIntoRandomNotOccupiedCell("X");
                    clickCounter += 1;
                }
            }
        }

        // Wstaw kółko lub krzyżyk ( whoseTurn ) w losowe puste pole
        public void insertIntoRandomNotOccupiedCell(String whoseTurn)
        {
            int randomIndex = random.Next(0, 9);
            Button randomBtn = getCellList()[randomIndex];

            while (getCellList()[randomIndex].Text != " ")
            {
                randomIndex = random.Next(0, 9);
                randomBtn = getCellList()[randomIndex];
            }
            //randomBtn.Text = whoseTurn;
            randomBtn.Enabled = false;
            getCellList()[getCellList().IndexOf(randomBtn)].Text = whoseTurn;
        }

        private void enableAllCells(bool enable)
        {
            getCellList().ForEach(c => c.Enabled = enable ? true : false);
        }

        // Pobierz tablice pól jako liste
        private List<Button> getCellList()
        {
            return cellArray.Cast<Button>().ToList();
        }
    }
}
