using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace WindowsFormsApplication2
{
    interface Inform
    {
        void StatusGame();
        void Info();
        void menu();
    }

    public partial class Form1 : Form, Inform
    {
        public int Round = 0;
        public bool WhoFirst = false;
        public int NumberGamer = 0;
        public bool thereIsWinner = false;
        public int N;
        public string gamer_1_Name = "";
        public string gamer_2_Name = "";

        public TableLayoutPanel table = new TableLayoutPanel
        {
            AutoSize = true
        };
        public void menu()
        {
            gamer_1_Name = textBox2.Text;
            gamer_2_Name = textBox3.Text;
            if (gamer_1_Name != "" && gamer_2_Name != "")
            {
                textBox2.Visible = false;
                textBox3.Visible = false;
                button2.Visible = false;
                button3.Visible = false;
                button4.Visible = false;
                textBox1.Visible = true;
                label2.Visible = true;
                checkBox1.Visible = true;
                checkBox2.Visible = true;
                button1.Visible = true;
            }
            else
            {
                MessageBox.Show("Введите имена игроков!");
            }

        }
        public void StatusGame()
        {
            if (NumberGamer % 2 == 1)
            {
                label3.Text = "Ожидается ход игрока " + gamer_2_Name;
            }
            else
            {
                label3.Text = "Ожидается ход игрока " + gamer_1_Name;
            }
            NumberGamer++;
        }

        public void Info()
        {
            if (NumberGamer == 0)
            {
                label1.Text = "Игра началась";
            }
            else if (thereIsWinner == true)
            {
                if (NumberGamer % 2 == 0)
                {
                    label1.Text = "Победил игрок " + gamer_1_Name;
                    WorkWithBD(gamer_1_Name, gamer_2_Name, NumberGamer - 1, "true");
                    NumberGamer = 0;
                    thereIsWinner = false;
                    button3.Visible = true;
                    button4.Visible = true;
                }
                else
                {
                    label1.Text = "Победил игрок " + gamer_2_Name;
                    WorkWithBD(gamer_1_Name, gamer_2_Name, NumberGamer - 1, "false");
                    NumberGamer = 0;
                    thereIsWinner = false;
                    button3.Visible = true;
                    button4.Visible = true;
                }
            }
            else if (table.Enabled == false)
            {
                label1.Text = "Ничья";

                WorkWithBD(gamer_1_Name, gamer_2_Name, NumberGamer - 1, "-1");
                NumberGamer = 0;
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        public void PrintResults()
        {
            using (SqlConnection cn = new SqlConnection("Data Source=LAPTOP-8FE5V0OM\\SQLEXPRESS;Initial Catalog=PlayGame;Integrated Security=True"))
            {
                cn.Open();

                label4.Text += "\n";
                label4.Visible = true;
                button5.Visible = false;
                string query = "SELECT * FROM Game";
                SqlCommand command = new SqlCommand(query, cn);
                IDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        string p = "";
                        if (reader[i].ToString() == "true")
                        {
                            p = " | победа";
                        }
                        else if (reader[i].ToString() == "false")
                        {
                            p = " | поражение";
                        }
                        else if (reader[i].ToString()=="-1")
                        {
                            p = " | ничья";
                        }
                        else
                        {
                            p = reader[i].ToString();
                        }
                        label4.Text += " |  " + p;
                    }
                    label4.Text += "\n";
                }
            }
        }
        public void WorkWithBD(string nameFirst,
                               string nameSecond,
                               int numStepFirst,
                               string winF="-1")
        {
            Round++;
            using (SqlConnection cn = new SqlConnection("Data Source=LAPTOP-8FE5V0OM\\SQLEXPRESS;Initial Catalog=PlayGame;Integrated Security=True"))
            {

                cn.Open();
                if (winF == "true")
                {
                    string queryFirst = "INSERT INTO Game (NameGamer, Round, WinORLose, NumberStroke) Values ('" + nameFirst + "'," + Round + ",'true'," + numStepFirst + ")";
                    string querySecond = "INSERT INTO Game (NameGamer, Round, WinORLose, NumberStroke) Values ('" + nameSecond + "'," + Round + ",'false'," + (numStepFirst - 1) + ")";
                    SqlCommand first = new SqlCommand(queryFirst, cn);
                    SqlCommand second = new SqlCommand(querySecond, cn);
                    first.ExecuteNonQuery();
                    second.ExecuteNonQuery();
                }
                else if (winF == "false")
                {
                    string queryFirst = "INSERT INTO Game (NameGamer, Round, WinORLose, NumberStroke) Values ('" + nameFirst + "'," + Round + ",'false'," + (numStepFirst - 1) + ")";
                    string querySecond = "INSERT INTO Game (NameGamer, Round, WinORLose, NumberStroke) Values ('" + nameSecond + "'," + Round + ",'true'," + numStepFirst + ")";
                    SqlCommand first = new SqlCommand(queryFirst, cn);
                    SqlCommand second = new SqlCommand(querySecond, cn);
                    second.ExecuteNonQuery();
                    first.ExecuteNonQuery();
                }

                else
                {
                    string queryFirst = "INSERT INTO Game (NameGamer, Round, WinORLose, NumberStroke) Values ('" + nameFirst + "'," + Round + ",'-1'," + numStepFirst + ")";
                    string querySecond = "INSERT INTO Game (NameGamer, Round, WinORLose, NumberStroke) Values ('" + nameSecond + "'," + Round + ",'-1'," + (numStepFirst - 1) + ")";
                    SqlCommand first = new SqlCommand(queryFirst, cn);
                    SqlCommand second = new SqlCommand(querySecond, cn);
                    first.ExecuteNonQuery();
                    second.ExecuteNonQuery();
                }
            }
        }
        public void MyButtonClick(object sender, EventArgs e)
        {
            if (((Button)sender).Text == "?")
            {
                StatusGame();
                if (WhoFirst == true)
                {
                    if (NumberGamer % 2 == 1)
                    {
                        ((Button)sender).Text = "X";
                    }
                    else
                    {
                        ((Button)sender).Text = "O";
                    }
                }
                else
                {
                    if (NumberGamer % 2 == 1)
                    {
                        ((Button)sender).Text = "O";
                    }
                    else
                    {
                        ((Button)sender).Text = "X";
                    }
                }
                Winner();
                Info();
            }
            else
            {
                MessageBox.Show("Ячейка занята!");
            }
        }

        public void Winner()
        {
            int ii = 0;
            int jj = 0;
            int schet_1 = 0;
            int schet_2 = 0;
            Button[,] btn = new Button[N, N];
            foreach (Button item in table.Controls)
            {
                if (ii < N)
                {
                    btn[ii, jj] = item;
                    ii++;
                }
                else if (ii == N)
                {
                    jj++;
                    ii = 0;
                    btn[ii, jj] = item;
                    ii++;
                }
            }

            for (int i = 0; i < N; i++)
            {
                if (btn[i, i].Text != btn[1, 1].Text || btn[i, i].Text == "?")
                {
                    schet_1++;
                }

                if (btn[i, N - i - 1].Text != btn[0, N - 1].Text || btn[i, N - i - 1].Text == "?")
                {
                    schet_2++;
                }
            }

            if (schet_1 == 0 || schet_2 == 0)
            {
                MessageBox.Show("Игра окончена");
                label3.Visible = false;
                thereIsWinner = true;
                if (schet_1 == 0)
                {
                    for (int i = 0; i < N; i++)
                    {
                        table.Controls[i * N + i].BackColor = Color.FromKnownColor(KnownColor.Coral);
                    }
                }
                if (schet_2 == 0)
                {
                    for (int i = 0; i < N; i++)
                    {
                        table.Controls[i * N + N - i - 1].BackColor = Color.FromKnownColor(KnownColor.Coral);
                    }
                }
                table.Enabled = false;
            }


            for (int i = 0; i < N; i++)
            {
                schet_1 = 0;
                schet_2 = 0;

                for (int j = 0; j < N; j++)
                {
                    if (btn[i, 0].Text != btn[i, j].Text || btn[i, j].Text == "?")
                    {
                        schet_1++;
                    }

                    if (btn[0, i].Text != btn[j, i].Text || btn[j, i].Text == "?")
                    {
                        schet_2++;
                    }
                }

                if (schet_1 == 0 || schet_2 == 0)
                {
                    MessageBox.Show("Игра окончена");
                    label3.Visible = false;
                    thereIsWinner = true;
                    if (schet_1 == 0)
                    {
                        for (int j = 0; j < N; j++)
                        {
                            table.Controls[j * N + i].BackColor = Color.FromKnownColor(KnownColor.Coral);
                        }
                    }
                    if (schet_2 == 0)
                    {
                        for (int j = 0; j < N; j++)
                        {
                            table.Controls[i * N + j].BackColor = Color.FromKnownColor(KnownColor.Coral);
                        }
                    }
                    table.Enabled = false;
                }
            }
            NoWinner(btn);
        }

        public void NoWinner(Button[,] btn)
        {
            int k = 0;
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (btn[i, j].Text == "?")
                    {
                        k++;
                    }
                }
            }
            if (k == 0 && thereIsWinner == false)
            {
                table.Enabled = false;
                button3.Visible = true;
                button4.Visible = true;
                label3.Visible = false;
                MessageBox.Show("Ничья");
            }
        }
        public void InputNumber()
        {
            if (textBox1.Text != "")
            {
                try
                {
                    N = int.Parse(textBox1.Text);
                }
                catch (FormatException e_1)
                {
                    MessageBox.Show("Ошибка: " + e_1.Message);
                    textBox1.Text = "";
                }
            }
            else
            {
                MessageBox.Show("Введите числовое значение в поле ввода!");
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            InputNumber();
            table.Visible = true;
            if (checkBox1.Checked && checkBox2.Checked)
            {
                MessageBox.Show("Необходимо выбрать один вариант!");
                checkBox1.Checked = false;
                checkBox2.Checked = false;
            }

            if ((checkBox1.Checked || checkBox2.Checked) && textBox1.Text != "")
            {
                if (checkBox1.Checked)
                {
                    WhoFirst = true;
                }
                else
                {
                    WhoFirst = false;
                }
                table.Enabled = true;
                table.ColumnCount = N;
                table.RowCount = N;
                Controls.Add(table);
                table.Location = new Point(60, 60);
                Button[,] bt = new Button[N, N];
                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j < N; j++)
                    {
                        bt[i, j] = new Button();
                        bt[i, j].Text = "?";
                        bt[i, j].Click += MyButtonClick;
                        bt[i, j].Width = 30;
                        bt[i, j].Height = 30;
                        table.Controls.Add(bt[i, j]);
                    }
                }
                Info();
                label3.Visible = true;
                StatusGame();
                textBox1.Visible = false;
                button1.Visible = false;
                checkBox1.Visible = false;
                checkBox2.Visible = false;
                label2.Visible = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label1.Text = "Введите размерность игрового поля";
            menu();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            label1.Text = "Укажите имена игроков или используйте значения по умолчанию";
            label3.Visible = false;
            textBox1.Visible = false;
            label2.Visible = false;
            checkBox1.Visible = false;
            checkBox2.Visible = false;
            button3.Visible = false;
            button4.Visible = false;
            textBox2.Visible = true;
            textBox3.Visible = true;
            button2.Visible = true;
            table.Visible = false;
            for (int i = 0; i <= N; i++)
            {
                foreach (Button item in table.Controls)
                {
                    Controls.Remove(item);
                    item.Dispose();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            button4.Visible = false;
            table.Visible = false;
            button5.Visible = true;
            button5.Click += new EventHandler(button5_Click);
            Controls.Add(button5);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            button5.Visible = false;
            button6.Visible = true;
            label4.Visible = true;
            PrintResults();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection("Data Source=LAPTOP-8FE5V0OM\\SQLEXPRESS; Initial Catalog = XorO; Integrated Security = True"))
            {
                cn.Open();
                string query = "DELETE FROM Game";
                SqlCommand doIt = new SqlCommand(query, cn);
                doIt.ExecuteNonQuery();
            }
            label4.Text = "...";
        }
    }
}

