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
    public partial class StatisticForm : Form
    {
        int bestScore;
        public StatisticForm(int bestScore)
        {
            InitializeComponent();
            this.bestScore = bestScore;
        }

        private void StatisticForm_Load(object sender, EventArgs e)
        {
            var users = UserResultsStorage.GetAll();
            foreach (var user in users)
            {
                if (user.Score == bestScore)
                {
                    bestNameLabel.Text = user.Name;
                    bestScoreLabel.Text = "Счет: " + user.Score.ToString();
                }
                statisticDataGridView.Rows.Add(user.Name, user.Score);
            }
        }
    }
}
