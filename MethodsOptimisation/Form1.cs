using System;
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
            point[] smp = new point[3];
            smp[0].x = new double[2];
            smp[1].x = new double[2];
            smp[2].x = new double[2];

            ////smp[3].x = new double[5];
            ////smp[4].x = new double[5];
            ////smp[5].x = new double[5];

            smp[0].x[0] = -50;
            smp[0].x[1] = -50;

            smp[1].x[0] = 50;
            smp[1].x[1] = -50;

            smp[2].x[0] = 0;
            smp[2].x[1] = 50;

            //smp[0].x[0] = 0;
            //smp[0].x[1] = 0;
            //smp[0].x[2] = 0;

            //smp[1].x[0] = 0;
            //smp[1].x[1] = 0;
            //smp[1].x[2] = 1*10;

            //smp[2].x[0] = 0;
            //smp[2].x[1] = 1 * 10;
            //smp[2].x[2] = 0;


            //smp[3].x[0] = 0;
            //smp[3].x[1] = 1 * 10;
            //smp[3].x[2] = 1 * 10;

            //smp[2].x[1] = 0;


            //smp[0].x[0] = 50;
            //smp[0].x[1] = 1;
            //smp[0].x[2] = 0.3;
            //smp[0].x[3] = 0.3;
            //smp[0].x[4] = 0.5;

            //smp[1].x[0] = 55;
            //smp[1].x[1] = 1;
            //smp[1].x[2] = 0.3;
            //smp[1].x[3] = 0.3;
            //smp[1].x[4] = 0.5;

            //smp[2].x[0] = 55;
            //smp[2].x[1] = 2;
            //smp[2].x[2] = 0.3;
            //smp[2].x[3] = 0.3;
            //smp[2].x[4] = 0.5;


            //smp[3].x[0] = 55;
            //smp[3].x[1] = 2;
            //smp[3].x[2] = 0.4;
            //smp[3].x[3] = 0.3;
            //smp[3].x[4] = 0.5;

            //smp[4].x[0] = 55;
            //smp[4].x[1] = 2;
            //smp[4].x[2] = 0.4;
            //smp[4].x[3] = 0.45;
            //smp[4].x[4] = 0.5;

            //smp[5].x[0] = 55;
            //smp[5].x[1] = 2;
            //smp[5].x[2] = 0.4;
            //smp[5].x[3] = 0.45;
            //smp[5].x[4] = 1;
            nd._Nelder_Mid(smp);//2
            //ss.Gauss(x0);
            textBox1.Text = "" + ss._Gauss(x0);
            //textBox2.Text = "" + test.f;
        }
    }
}
