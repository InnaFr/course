using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public int[] wins = new int[4];
        public int[] sum = new int[4];
        private void button1_Click(object sender, EventArgs e)
        {

            if (textBox1.Text == "")
            {
                MessageBox.Show("Заполните 1 поле!");
            }

            if (textBox2.Text == "")
            {
                MessageBox.Show("Заполните 2 поле!");
            }

            if (textBox3.Text == "")
            {
                MessageBox.Show("Заполните 3 поле!");
            }

            if (textBox4.Text == "")
            {
                MessageBox.Show("Заполните 4 поле!");
            }

            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "")
            {

                Dictionary<int, string> Names = new Dictionary<int, string>();
                Names.Add(0, textBox1.Text);
                Names.Add(1, textBox2.Text);
                Names.Add(2, textBox3.Text);
                Names.Add(3, textBox4.Text);
                string[] begimMas = new string[36];
                int k = 0;
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 6; j < 15; j++)
                    {
                        switch (i)
                        {
                            case 0:
                                {
                                    begimMas[k] = j + "a";
                                    k++;
                                    break;
                                }
                            case 1:
                                {
                                    begimMas[k] = j + "b";
                                    k++;
                                    break;
                                }
                            case 2:
                                {
                                    begimMas[k] = j + "c";
                                    k++;
                                    break;
                                }
                            case 3:
                                {
                                    begimMas[k] = j + "d";
                                    k++;
                                    break;
                                }
                        }
                    }
                }

                Queue<int> cards = new Queue<int>();
                List<int> iskl = new List<int>();
                int buf = 0;
                int el = 0;
                for (int i = 0; i < 24; i++)
                {
                    buf = 0;
                    while (buf != -1)
                    {

                        Random rand = new Random();
                        buf = 0;
                        el = rand.Next(0, 35);
                        if (iskl.Count == 0)
                        {
                            buf = -1;
                        }
                        buf = -1;
                        foreach (var p in iskl)
                        {
                            if (p == el)
                            {
                                buf = 0;
                            }
                        }

                        if (buf == -1)
                        {
                            iskl.Add(el);
                            cards.Enqueue(el);
                        }
                    }
                }
                int[] sumPart = new int[4];
                string smth = "";
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        smth = begimMas[cards.Peek()];
                        cards.Dequeue();
                        sumPart[i] += Convert.ToInt32(smth.Remove(smth.Length - 1, 1));
                        sum[i] += Convert.ToInt32(smth.Remove(smth.Length - 1, 1));
                        smth = "";
                    }
                }

                int max = 0;
                int maxNum = 0;

                for (int i = 0; i < 4; i++)
                {
                    if (max < sumPart[i])
                    {
                        max = sumPart[i];
                        maxNum = i;
                    }
                }

                label2.Text = "В данном раунде побеждает игрок " + Names[maxNum] + " со счетом " + sumPart[maxNum];

                switch (maxNum)
                {
                    case 0:
                        {
                            wins[0]++;
                            break;
                        }
                    case 1:
                        {
                            wins[1]++;
                            break;
                        }
                    case 2:
                        {
                            wins[2]++;
                            break;
                        }
                    case 3:
                        {
                            wins[3]++;
                            break;
                        }
                }

                label3.Text = "Игрок " + Names[0] + " набрал " + sum[0] + "(" + wins[0] + ")";
                label4.Text = "Игрок " + Names[1] + " набрал " + sum[1] + "(" + wins[1] + ")";
                label5.Text = "Игрок " + Names[2] + " набрал " + sum[2] + "(" + wins[2] + ")";
                label6.Text = "Игрок " + Names[3] + " набрал " + sum[3] + "(" + wins[3] + ")";

                if (wins[0] == 5 || wins[1] == 5 || wins[2]==5 || wins[3]==5)
                {
                    button1.Visible = false;
                    max = 0;
                    maxNum=0;
                    for (int i=0; i<4; i++)
                    {
                        if (sum[i]>max)
                        {
                            max = sum[i];
                            maxNum = i;
                        }
                    }
                    label2.Text = "Побеждает игрок " + Names[maxNum] + " со счетом " + max;
                   
                    MessageBox.Show("Игра окончена. Ознакомьтесь с результатами.");
                }
            }
        }
    }
}
