﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MethodsOptimisation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            singleSearch s = new singleSearch();

            //Добавить годный функционал
            //qwertyasdf
            //minArg test = s.getMinPowell(0, 2, 1e-10);
            //minArg test = s.getMinParabol(5, 1e-5);
            //minArg test = s.getMinParabol(0, 1e-10);
            //textBox1.Text = "" + s.getMinPowell(-0.2, 2, 1e-5);
            //textBox1.Text = "" + s.getMinDih(-1, 1, 1e-15);
            double[] x0 = new double[2];
            x0[0] = 5;
            x0[1] = 5;
            //x0[0] = 50 + 1;
            //x0[1] = 1 + 1;
            //x0[2] = 0.3 + 0.1;
            //x0[3] = 0.3 + 0.1;
            //x0[4] = 0.5 + 0.1;
            Gauss ss = new Gauss();
            Nelder_Mid nd = new Nelder_Mid();

            nd._Nelder_Mid();
            //ss.Gauss(x0);
            textBox1.Text = "" + ss._Gauss(x0);
            //textBox2.Text = "" + test.f;
        }
    }
}
