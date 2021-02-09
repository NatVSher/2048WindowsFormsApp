using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;


namespace _2048WindowsFormsApp
{
    public partial class MainForm : Form
    {
        private string bestScorePath = "bestScore.txt";
        public int mapSize = 4;
        private Label[,] labelsMap;
        private static Random random = new Random();
        public User user;
        private int bestScore;
        private bool flagSaveUser = false;


        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            user = new User("Инкогнито");
            var enterNameForm = new EnterNameForm(user);
            var result = enterNameForm.ShowDialog(this);
            if (result == DialogResult.None)
                return;
            if (result != DialogResult.OK)
            {
                Close();
            }
            else
            {
                bestScore = GetBestScore();
                InitMap();
                GenerateNumber();
                ShowScores();
            }
        }

        private int GetBestScore()
        {
            if (File.Exists(bestScorePath))
            {
                return Convert.ToInt32(FileProvider.Get(bestScorePath));
            }
            else
            {
                return 0;
            }
        }


        private void ShowScores()
        {
            scoreLabel.Text = user.Score.ToString();
            bestScoreLabel.Text = bestScore.ToString();
        }

        private void GenerateNumber()
        {
            while (true)
            {
                var randomNumberLabel = random.Next(mapSize * mapSize);
                var randomDigit = random.Next(1, 5);
                if (randomDigit < 4)
                {
                    randomDigit = 2;
                }
                var indexRow = randomNumberLabel / mapSize;
                var indexColumn = randomNumberLabel % mapSize;
                if (labelsMap[indexRow, indexColumn].Text == String.Empty)
                {
                    labelsMap[indexRow, indexColumn].Text = randomDigit.ToString();
                    break;
                }
            }
        }

        private void InitMap()
        {
            int formX = 76 * mapSize + 10;
            int formY = 76 * (mapSize + 1);
            this.ClientSize = new System.Drawing.Size(formX, formY);
            labelsMap = new Label[mapSize, mapSize];
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    var newLabel = CreateLabel(i, j);
                    Controls.Add(newLabel);
                    labelsMap[i, j] = newLabel;
                }
            }

        }

        private Label CreateLabel(int indexRow, int indexColumn)
        {
            var label = new Label();
            label.BackColor = SystemColors.ActiveBorder;
            label.Font = new Font("Times New Roman", 18F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(204)));
            label.Size = new Size(70, 70);
            label.TextAlign = ContentAlignment.MiddleCenter;
            int x = 10 + indexColumn * 76;
            int y = 70 + indexRow * 76;
            label.Location = new Point(x, y);
            label.TextChanged += Label_TextChanged;
            return label;
        }

        private void Label_TextChanged(object sender, EventArgs e)
        {
            var label = (Label)sender;
            UpdateLabelBackColor(label);
        }

        private void UpdateLabelBackColor(Label label)
        {
            int exp = 0;
            if (label.Text != string.Empty)
            {
                var number = Convert.ToInt32(label.Text);
                exp = (int)Math.Log(number, 2);
            }

            Color[] colors = new Color[12];
            colors[0] = SystemColors.ActiveBorder;
            colors[1] = Color.Aqua;
            colors[2] = Color.Gray;
            colors[3] = Color.Brown;
            colors[4] = Color.BlueViolet;
            colors[5] = Color.Coral;
            colors[6] = Color.Green;
            colors[7] = Color.DimGray;
            colors[8] = Color.Pink;
            colors[9] = Color.Red;
            colors[10] = Color.White;
            colors[11] = Color.Yellow;

            label.BackColor = colors[exp];
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Right && e.KeyCode != Keys.Left && e.KeyCode != Keys.Up && e.KeyCode != Keys.Down)
            {
                return;
            }
            bool isMerged = true;
            if (e.KeyCode == Keys.Right)
            {
                for (int i = 0; i < mapSize; i++)
                {
                    for (int j = mapSize - 1; j >= 0; j--)
                    {
                        if (labelsMap[i, j].Text != string.Empty)
                        {
                            for (int k = j - 1; k >= 0; k--)
                            {
                                if (labelsMap[i, k].Text != string.Empty)
                                {
                                    if (labelsMap[i, j].Text == labelsMap[i, k].Text)
                                    {
                                        var number = int.Parse(labelsMap[i, j].Text);
                                        user.CountingScore(number);
                                        isMerged = false;
                                        labelsMap[i, j].Text = (number * 2).ToString();
                                        labelsMap[i, k].Text = string.Empty;
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }
                for (int i = 0; i < mapSize; i++)
                {
                    for (int j = mapSize - 1; j >= 0; j--)
                    {
                        if (labelsMap[i, j].Text == string.Empty)
                        {
                            for (int k = j - 1; k >= 0; k--)
                            {
                                if (labelsMap[i, k].Text != string.Empty)
                                {
                                    labelsMap[i, j].Text = labelsMap[i, k].Text;
                                    labelsMap[i, k].Text = string.Empty;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            if (e.KeyCode == Keys.Left)
            {
                for (int i = 0; i < mapSize; i++)
                {
                    for (int j = 0; j < mapSize; j++)
                    {
                        if (labelsMap[i, j].Text != string.Empty)
                        {
                            for (int k = j + 1; k < mapSize; k++)
                            {
                                if (labelsMap[i, k].Text != string.Empty)
                                {
                                    if (labelsMap[i, j].Text == labelsMap[i, k].Text)
                                    {
                                        var number = int.Parse(labelsMap[i, j].Text);
                                        user.CountingScore(number);
                                        isMerged = false;
                                        labelsMap[i, j].Text = (number * 2).ToString();
                                        labelsMap[i, k].Text = string.Empty;
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }
                for (int i = 0; i < mapSize; i++)
                {
                    for (int j = 0; j < mapSize; j++)
                    {
                        if (labelsMap[i, j].Text == string.Empty)
                        {
                            for (int k = j + 1; k < mapSize; k++)
                            {
                                if (labelsMap[i, k].Text != string.Empty)
                                {
                                    labelsMap[i, j].Text = labelsMap[i, k].Text;
                                    labelsMap[i, k].Text = string.Empty;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            if (e.KeyCode == Keys.Up)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    for (int i = 0; i < mapSize; i++)
                    {
                        if (labelsMap[i, j].Text != string.Empty)
                        {
                            for (int k = i + 1; k < mapSize; k++)
                            {
                                if (labelsMap[k, j].Text != string.Empty)
                                {
                                    if (labelsMap[i, j].Text == labelsMap[k, j].Text)
                                    {
                                        var number = int.Parse(labelsMap[i, j].Text);
                                        user.CountingScore(number);
                                        isMerged = false;
                                        labelsMap[i, j].Text = (number * 2).ToString();
                                        labelsMap[k, j].Text = string.Empty;
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }
                for (int j = 0; j < mapSize; j++)
                {
                    for (int i = 0; i < mapSize; i++)
                    {
                        if (labelsMap[i, j].Text == string.Empty)
                        {
                            for (int k = i + 1; k < mapSize; k++)
                            {
                                if (labelsMap[k, j].Text != string.Empty)
                                {
                                    labelsMap[i, j].Text = labelsMap[k, j].Text;
                                    labelsMap[k, j].Text = string.Empty;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            if (e.KeyCode == Keys.Down)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    for (int i = mapSize - 1; i >= 0; i--)
                    {
                        if (labelsMap[i, j].Text != string.Empty)
                        {
                            for (int k = i - 1; k >= 0; k--)
                            {
                                if (labelsMap[k, j].Text != string.Empty)
                                {
                                    if (labelsMap[i, j].Text == labelsMap[k, j].Text)
                                    {
                                        var number = int.Parse(labelsMap[i, j].Text);
                                        user.CountingScore(number);
                                        isMerged = false;
                                        labelsMap[i, j].Text = (number * 2).ToString();
                                        labelsMap[k, j].Text = string.Empty;
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }
                for (int j = 0; j < mapSize; j++)
                {
                    for (int i = mapSize - 1; i >= 0; i--)
                    {
                        if (labelsMap[i, j].Text == string.Empty)
                        {
                            for (int k = i - 1; k >= 0; k--)
                            {
                                if (labelsMap[k, j].Text != string.Empty)
                                {
                                    labelsMap[i, j].Text = labelsMap[k, j].Text;
                                    labelsMap[k, j].Text = string.Empty;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            TryUpdateBestScore();
            ShowScores();
            if (IsFullMap() && isMerged || Exists2048())
            {
                MessageBox.Show("Конец игры!\n Ваш счет: " + user.Score);
                UserResultsStorage.Append(user);
                flagSaveUser = true;
                DialogResult dialogResult = MessageBox.Show("Хотите начать сначала?", "Ваш выбор", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    RemoveLabels();
                    InitMap();
                    GenerateNumber();
                    user.Score = 0;
                    ShowScores();
                }
                else if (dialogResult == DialogResult.No)
                {
                    Application.Exit();
                }                
            }
            else
            {
                GenerateNumber();
            }
        }

        private void TryUpdateBestScore()
        {
            if (user.Score > bestScore)
            {
                bestScore = user.Score;
            }
        }

        private bool IsFullMap()
        {
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    if (labelsMap[i, j].Text == string.Empty)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        private bool Exists2048()
        {
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    if (labelsMap[i, j].Text == "2048")
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        
        private void рестартToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void правилаИгрыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string rules = "В начале и после каждого хода на поле появляется цифра 2 или 4.\n" +
                "Нажимая кнопочку вверх, вправо, влево или вниз, все ваши кирпичики будут смещаться в ту же сторону. \n" +
                "Но, при соприкосновении клеточек с одним и тем же номиналом, они объединяются и создают сумму вдвое больше.\n" +
                "Игра заканчивается тогда, когда все пустые ячейки заполнены, и вы больше не можете передвигать клеточки ни в одну из сторон. \n" +
                "Ну, или когда на одном из кубов, наконец, появилась заветная цифра 2048.";
            MessageBox.Show(rules);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            FileProvider.Replase(bestScorePath, bestScore.ToString());
            if (!flagSaveUser)
            {
                UserResultsStorage.Append(user);
            }
        }

        private void статистикаИгрыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var statisticForm = new StatisticForm(bestScore);
            statisticForm.Show();
        }

        private void выборРазмераПоляToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sizeMapForm = new SizeMapForm();
            sizeMapForm.FormClosing += (sender1, e1) =>
            {
                RemoveLabels();
                mapSize = sizeMapForm.MapSize;
                InitMap();
            };
            sizeMapForm.ShowDialog();
            user.Score = 0;
            GenerateNumber();
            ShowScores();

        }
        private void RemoveLabels()
        {
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    Controls.Remove(labelsMap[i, j]);
                }
            }
        }
    }
}
