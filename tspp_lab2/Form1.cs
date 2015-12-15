using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace tspp_lab2
{
    public partial class Form1 : Form
    {
        
        double[,] arrData = new double[3, 4], 
        arrRezult = new double[3, 4];
        double maxU, maxIJ;
        StreamReader reader;	
        StreamWriter writer;	
        char[] separator = { ',' };

        public Form1()
        {
            InitializeComponent();
        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel) return;
            reader = new StreamReader(openFileDialog1.FileName);
            string[] arrStrings; 
            string workString;
            textBox1.Clear();//очищення вікна відображення вихідних даних
            textBox2.Clear();//очищення вікна відображення результатів
            Array.Clear(arrData, 0, arrData.Length);//очищення масиву даних
            textBox3.Clear();
            textBox4.Clear();
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            checkBox1.Checked = false;
            checkBox2.Checked = false;

            int K = 0;      //кількість елементів у рядку файлу
            for (int i = 0; i < 3; i++)
            {
                if (reader.EndOfStream) break;
                workString = reader.ReadLine();    //ввід чергового рядка
                arrStrings = workString.Split(separator);//розбивка на масив підрядків по ","
                //якщо елементів у рядку  < 4
                
                if (arrStrings.Length < 4) 
                    K = arrStrings.Length; 
                else K = 4;

                for (int j = 0; j < K; j++)//перетворення й запис у масив чисел
                {
                    arrData[i, j] = Double.Parse(arrStrings[j], System.Globalization.CultureInfo.InvariantCulture);
                    textBox1.Text += arrStrings[j] + ", ";//вивід у вікно відображення даних
                }

                textBox1.Text += "\r\n";    //перехід на новий рядок
            }
            reader.Close();




        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //якщо натиснуто кнопку Cancel, те вихід з методу
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel) return;
            //створення нового потоку для виводу
            writer = new StreamWriter(saveFileDialog1.FileName);
            //цикл запису у файл
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    writer.Write(arrRezult[i, j]);
                    if (j != 3) writer.Write(", ");    //наприкінці рядка не повинне бути «,»
                }
                writer.WriteLine();		//запис перекладу рядка
            }
            writer.Close();

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            
            if (checkBox1.Checked)
            {
                maxU = arrData[0, 0];
                if (maxU < arrData[0, 3])
                {
                    maxU = arrData[0, 3];
                }
                else if (maxU < arrData[2, 0])
                {
                    maxU = arrData[2, 0];
                }
                else if (maxU < arrData[2, 3])
                {
                    maxU = arrData[2, 3];
                }
                textBox3.Text = maxU.ToString();
            }
            else
            {
                textBox3.Text = "";
                maxU = double.NaN;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                maxIJ = arrData[0, 0];
                for (int i = 0; i < 3; i++)
                {
                    if (arrData[i, i] > maxIJ)
                        maxIJ = arrData[i, i];

                }

                textBox4.Text = maxIJ.ToString();
            }
            else
            {
                textBox4.Text = "";
                maxIJ = double.NaN;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                textBox2.Clear();
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        arrRezult[i, j] = Math.Log10(Math.Abs(arrData[i, j]));
                        textBox2.Text += " " + arrRezult[i, j].ToString() + ",";
                    }
                    textBox2.Text += "\r\n";
                }
            }

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                
               checkBox1.Checked = true;
               checkBox2.Checked = true;

                textBox2.Clear();
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        arrRezult[i, j] = (Math.Pow(arrData[i, j], 2) + Math.Pow(maxU, 2))/(arrData[i,j] + maxIJ);
                        textBox2.Text += " " + arrRezult[i, j].ToString() + ",";
                    }
                    textBox2.Text += "\r\n";
                }
            }
        }
    }
}
