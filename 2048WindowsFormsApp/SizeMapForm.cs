using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2048WindowsFormsApp
{
    public partial class SizeMapForm : Form
    {
        public int MapSize = 4;
        public SizeMapForm()
        {
            InitializeComponent();            
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            MapSize = 4;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MapSize = 5;
            Close();
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            MapSize = 6;
            Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MapSize = 7;
            Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MapSize = 8;
            Close();
        }
    }
}
