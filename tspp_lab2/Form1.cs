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

        }
    }
}
