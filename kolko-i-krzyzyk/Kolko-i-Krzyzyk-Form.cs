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
        }

        private void Cell_Click(object sender, EventArgs e)
        {
            Button cell = (Button)sender;

            if (clickCounter % 2 == 0)
            {
                cell.Text = "X";
            }
            else
            {
                cell.Text = "O";
            }
            cell.Enabled = false;
            clickCounter += 1;
        }
    }
}
