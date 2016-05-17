using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
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

            //double[] x0 = new double[2];
            //Fx.CC = 0;
            //x0[0] = 5;
            //x0[1] = 5;
            //Newton_Raffson r = new Newton_Raffson();
            //r._Newton_Raffson(x0);

            //double[] x0 = new double[2];
            //x0[0] = 5;
            //x0[1] = 5;
            //Fletcher_Rivz r = new Fletcher_Rivz();
            //r._Fletcher_Rivz(x0);


            //double[] x0 = new double[2];
            //x0[0] = 5;
            //x0[1] = 5;
            //Gauss g = new Gauss();
            //g._Gauss(x0);

            //double[] x0 = new double[2];
            //x0[0] = 5;
            //x0[1] = 5;
            //Nelder_Mid n = new Nelder_Mid();
            //n._Nelder_Mid(x0);

            //double[] x0 = new double[5];
            //x0[0] = 50;
            //x0[1] = 1;
            //x0[2] = 0.3;
            //x0[3] = 0.3;
            //x0[4] = 0.5;
            //Nelder_Mid n = new Nelder_Mid();
            //n._Nelder_Mid(x0, 5);
            //Newton_Raffson r = new Newton_Raffson();
            //r._Newton_Raffson(x0);

            double[] x0 = new double[2];
            x0[0] = 2;
            x0[1] = 2;
            Penalty_Method q = new Penalty_Method();

            Fx.CC = 0;
            //q._Barrier_Method(x0, 10, 1e+3);

            //textBox1.Text += "Значение функции:\r\n" + Fx.f + "\r\n";
            //textBox1.Text += "В точке:\r\n";
            //foreach (double c in Fx.x)
            //{
            //    textBox1.Text += c + "\r\n";
            //}
            //textBox1.Text += "Кол-во итераций функции:\r\n" + Fx.CC + "\r\n\r\n";

            for (double i = 1; i < 1e+20; i *= 10)
            {
                for (double j = 4; j < 10; j++)
                {
                    Fx.CC = 0;
                    x0[0] = 2;
                    x0[1] = 2;
                    q._Penalty_Method(x0, i, j);

                    textBox1.Text += "Значение функции:\r\n" + Fx.f + " " + i + " " + j + "\r\n";
                    textBox1.Text += "В точке:\r\n";
                    foreach (double c in Fx.x)
                    {
                        textBox1.Text += c + "\r\n";
                    }
                    textBox1.Text += "Кол-во итераций функции:\r\n" + Fx.CC + "\r\n\r\n";
                }
            }

            //double[] x0 = new double[2];
            //x0[0] = 5;
            //x0[1] = 5;
            //Barrier_Method q = new Barrier_Method();
            //q._Barrier_Method(x0, 1, 10);

            //textBox1.Text = "Значение функции:\r\n" + Fx.f + "\r\n";
            //textBox1.Text += "В точке:\r\n";
            //foreach(double c in Fx.x)
            //{
            //    textBox1.Text += c + "\r\n";
            //}
            //textBox1.Text += "Кол-во итераций функции:\r\n" + Fx.CC;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            
        }
    }
}
