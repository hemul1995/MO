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


            //qwertyasdf
            //minArg test = s.getMinPowell(0, 2, 1e-10);
            minArg test = s.getMinDih(-1, 1, 1e-10);
            //minArg test = s.getMinParabol(0, 1e-10);
            //textBox1.Text = "" + s.getMinPowell(-0.2, 2, 1e-5);
            //textBox1.Text = "" + s.getMinDih(-1, 1, 1e-15);
            textBox1.Text = "" + test.x;
            textBox2.Text = "" + test.f;
        }
    }
}