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
            double h = 1e-7, p1, p;
            double tmp = x[index];
            double f1, f2;
            x[index] -= h;
            f1 = Fx.Func(x);
            x[index] += 2 * h;
            f2 = Fx.Func(x);
            p1 = (f1 - f2) / (2 * h);
            x[index] = tmp;

            p = p1;
            return p;
        }

        public double goldenSection(double[] x, double[] dk, double a, double b, double eps)
        {
            //double tmp = x[index];
            double T1;
            T1 = 0.381966;
            //T2 = 1 - T1;
            double[] tmpMass = new double[x.Length];
            do
            {
                double x1, x2;
                x1 = a + (b - a) * T1;
                x2 = b - (b - a) * T1;
                //x[index] = (x1 + x2) / 2;

                double F1, F2;
                tmpMass.ToList().Select((z, i) => (tmpMass[i] = x[i] + x1 * dk[i])).ToArray();
                F1 = Fx.Func(tmpMass);
                tmpMass.ToList().Select((z, i) => (tmpMass[i] = x[i] + x2 * dk[i])).ToArray();
                F2 = Fx.Func(tmpMass);

                if (F1 < F2)
                {
                    b = x2;
                    if (Math.Abs(b - a) < eps) break;
                    x2 = x1;
                    F2 = F1;
                    x1 = a + (b - a) * T1;
                    tmpMass.ToList().Select((z, i) => (tmpMass[i] = x[i] + x1 * dk[i])).ToArray();
                    F1 = Fx.Func(tmpMass);
                }
                else
                {
                    a = x1;
                    if (Math.Abs(b - a) < eps) break;
                    x1 = x2;
                    F1 = F2;
                    x2 = b - (b - a) * T1;
                    tmpMass.ToList().Select((z, i) => (tmpMass[i] = x[i] + x2 * dk[i])).ToArray();
                    F2 = Fx.Func(tmpMass);
                }
            }
            while (true);

            double res = (a + b) / 2;
            //x[index] = tmp;

            return res;

        }

        
        public void _Fletcher_Rivz(double[] x, double eps1 = 1e-5, double eps2 = 1e-5)
        {
            Fx.CC = 0;
            const int MAXITER = 10000;
            int LENGTH = x.Length;
            double tk;
            double[] dk = new double[LENGTH];
            double[] grad = new double[LENGTH];
            double[] grad0 = new double[LENGTH];
            double[] x1 = new double[LENGTH];
            double[] x0 = new double[LENGTH];
            double beta;
            int k = 0;
            while (true)
            {
                //3
                for(int i = 0; i < LENGTH; i++)
                {
                    grad[i] = getDerivate1(x, i);
                }

                //4
                if (Math.Sqrt(grad.Sum(a => a * a)) <= eps1)
                {
                    break;
                }

                //5
                if (k >= MAXITER) break;

                //6
                if (k == 0)
                {
                    for (int i = 0; i < LENGTH; i++)
                    {
                        dk[i] = -grad[i];
                    }
                }
                else
                {
                    beta = 0;
                    beta = grad.Sum(a => a * a) / grad0.Sum(b => b * b);
                    if ((k + 1) % 2 != 0)
                    {


                        //for (int i = 0; i < LENGTH; i++)
                        //{
                        //    beta += grad[i] * (grad[i] - grad0[i]);
                        //}
                        //beta /= grad0.Sum(b => b * b);
                    }

                    //8
                    for(int i = 0; i < LENGTH; i++)
                    {
                        dk[i] = -grad[i] + beta * dk[i];
                    }
                }

                //9
                tk = goldenSection(x, dk, -100, 100, 1e-5);

                //10
                for (int i = 0; i < LENGTH; i++)
                {
                    x1[i] = x[i] + tk * dk[i];
                }

                //11
                double norm = 0;
                for (int i = 0; i < LENGTH; i++)
                {
                    norm += (x1[i] - x[i]) * (x1[i] - x[i]);
                }
                if (Math.Abs(Fx.Func(x1) - Fx.Func(x)) < eps2 && Math.Sqrt(norm) < eps2 && k > 1) break;
                else
                {
                    for (int i = 0; i < LENGTH; i++)
                    {
                        x0[i] = x[i];
                        grad0[i] = grad[i];
                    }
                    for (int i = 0; i < LENGTH; i++)
                    {
                        x[i] = x1[i];
                    }
                    k++;
                }
            }
            Fx.x = x;
            Fx.f = Fx.Func(x);
            //cout << "Fletcher-Rieves Method: " << endl; 
            //PrintSolution(x, F1(x), numIter);
        }
    }
}
