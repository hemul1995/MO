using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethodsOptimisation
{
    class Fletcher_Rivz
    {

        private double getDerivate1(double[] x, int index)
        {
            double eps = 1e-10;
            double h = 1, p1, p2, p;
            double tmp = x[index];
            double f1, f2;
            do
            {
                x[index] -= h;
                f1 = Fx.Func(x);
                x[index] += 2 * h;
                f2 = Fx.Func(x);
                p1 = (f1 - f2) / (2 * h);
                x[index] = tmp;
                h /= 2;
                x[index] -= h;
                f1 = Fx.Func(x);
                x[index] += 2 * h;
                f2 = Fx.Func(x);
                p2 = (f1 - f2) / (2 * h);
                x[index] = tmp;
            }
            while (p1 != p2 && h * h > eps * Math.Abs(x[index]));
            p = p2;
            return p;
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
                x2 = a + (b - a) * T2;
                x[index] = (x1 + x2) / 2;
                double F1, F2;
                x[index] -= eps;
                F1 = Fx.Func(x);
                x[index] += 2 * eps;
                F2 = Fx.Func(x);

                if (F1 < F2)
                {
                    b = x2;
                    x2 = x1;
                    F2 = F1;
                    x1 = a + (b - a) * T1;
                    x[index] = x1;
                    F1 = Fx.Func(x);
                }
                else
                {
                    a = x1;
                    x1 = x2;
                    F1 = F2;

                    x2 = a + (b - a) * T2;
                    x[index] = x2; ;
                    F2 = Fx.Func(x);
                }
            }
            while (Math.Abs(b - a) > eps);

            double res = (a + b) / 2;
            return res;

        }

        
        public void _Fletcher_Rivz(double[] x, double eps)
        {

            Fx.x = x;
            Fx.f = Fx.Func(x);
            //cout << "Fletcher-Rieves Method: " << endl; 
            //PrintSolution(x, F1(x), numIter);
        }
    }
}
