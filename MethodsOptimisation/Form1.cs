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

            double[] x0 = new double[2];
            x0[0] = 2;
            x0[1] = 2;
            Newton_Raffson r = new Newton_Raffson();
            r._Newton_Raffson(x0);

            //double[] x0 = new double[2];
            //x0[0] = 5;
            //x0[1] = 5;
            //Fletcher_Rivz r = new Fletcher_Rivz();
            //r._Fletcher_Rivz(x0);

            //Gauss g = new Gauss();
            //g._Gauss(x0);
            //ss.Gauss(x0);
            
            
            textBox1.Text = "Значение функции:\r\n" + Fx.f + "\r\n";
            textBox1.Text += "В точке:\r\n";// + Fx.f + "\n";
            foreach(double c in Fx.x)
            {
                textBox1.Text += c + "\r\n";// + Fx.f + "\n";
            }
            textBox1.Text += "Кол-во итераций функции:\r\n" + Fx.CC;// + Fx.f + "\n";
            //textBox2.Text = "" + test.f;
        }
        public double goldenSection(double[] x, double a, double b, int index, double eps)
        {
            double tmp = x[index];
            double T1, T2;
            T1 = 0.381966;
            T2 = 1 - T1;

            do
            {
                double x1, x2;
                x1 = a + (b - a) * T1;
                x2 = b - (b - a) * T1;
                x[index] = (x1 + x2) / 2;
                double F1, F2;
                x[index] = x1;
                F1 = Fx.Func(x);
                x[index] = x2;
                F2 = Fx.Func(x);

                if (F1 < F2)
                {
                    b = x2;
                    if (Math.Abs(b - a) < eps) break; 
                    x2 = x1;
                    F2 = F1;
                    x1 = a + (b - a) * T1;
                    x[index] = x1;
                    F1 = Fx.Func(x);
                }
                else
                {
                    a = x1;
                    if (Math.Abs(b - a) < eps) break; 
                    x1 = x2;
                    F1 = F2;

                    x2 = b - (b - a) * T1;
                    x[index] = x2; ;
                    F2 = Fx.Func(x);
                }
            }
            while (true);

            double res = (a + b) / 2;
            x[index] = tmp;

            return res;

        }
        private void button2_Click(object sender, EventArgs e)
        {
            for(int i = 0; i < 2; i++)
            {
                double t = goldenSection(new double[] { 0, 0 }, -30, 30, i, 1e-5);
            }

        }
    }
}
