using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kolko_i_krzyzyk
{
    public partial class kolko_i_krzyzyk : Form
    {
        private Button[,] cellArray;
        private int clickCounter;

        public kolko_i_krzyzyk()
        {
            InitializeComponent();

            cellArray = new Button[3, 3] 
            {
                { A1, A2, A3 },
                { B1, B2, B3 },
                { C1, C2, C3 }
            };

            enableAllCells(false);
        }

        private void Cell_Click(object sender, EventArgs e)
        {
            Button cell = (Button)sender;

            if (clickCounter % 2 == 0)
            {
                cell.Text = Mark.X.ToString();
            }
            else
            {
                cell.Text = Mark.O.ToString();
            }
            cell.Enabled = false;
            clickCounter += 1;
        }

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

        private void startResetBtn_Click(object sender, EventArgs e)
        {
            if (startResetBtn.Text == "WYCZYŚĆ")
            {
                startResetBtn.Text = "START";
                enableAllCells(false);

                getCellList().ForEach(c => c.Text = " ");
            }
            else
            {
                startResetBtn.Text = "WYCZYŚĆ";
                enableAllCells(true);
                clickCounter = 0;
            }
        }

        private void enableAllCells(bool enable)
        {
            getCellList().ForEach(c => c.Enabled = enable ? true : false);
        }

        private List<Button> getCellList()
        {
            return cellArray.Cast<Button>().ToList();
        }

        enum Mark
        {
            X,
            O
        }

        enum Choice
        {
            CZŁOWIEK,
            KOMPUTER
        }

        
    }
}
