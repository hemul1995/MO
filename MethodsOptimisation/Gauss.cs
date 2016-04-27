using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethodsOptimisation
{
    class Gauss
    {
        double[] A = new double[5] { 2, 0.1, 0.1, 0.1, 0 };
        double[] B = new double[5] { 100, 2, 0.45, 0.45, 1 };
        public double getMinDih(double[] x0, int index, double a, double b, double eps)
        {
            double x;
            double f1, f2;
            double tmp = x0[index];

            do
            {
                x0[index] = (a + b) / 2;
                x0[index] -= eps;
                f1 = Fx.Func(x0);
                x0[index] += 2 * eps;
                f2 = Fx.Func(x0);

                if (f1 < f2)
                    b = x0[index];
                else
                    a = x0[index];
            }
            while (Math.Abs(b - a) > 1e-2);

            x0[index] = (b + a) / 2;
            x = x0[index];
            x0[index] = tmp;
            return x;
        }
        private double getDerivate1(double[] x, int index)
        {
            double eps = 1e-10;
            double h = 1e-5, p1, p2, p;
            double tmp = x[index];
            double f1, f2;



                x[index] = tmp + h;
                f1 = Fx.Func(x);
                x[index] = tmp - h;
                f2 = Fx.Func(x);


                p1 = (f1 - f2) / (2 * h);
               

            p = p1;
            x[index] = tmp;
            return p;
        }

        private double getDerivate2(double[] x, int index)
        {
            double eps = 1e-10;
            double h = 1e-5, p1, p2, p;
            double tmp = x[index];
            double f1, f2, f3;

                
                
                x[index] = tmp + h;
                f1 = Fx.Func(x);
                x[index] = tmp - h;
                f2 = Fx.Func(x);
                x[index] = tmp ;
                f3 = Fx.Func(x);

                p1 = (f1 + f2 - 2 * f3) / (h * h);
               
            p = p1;
            x[index] = tmp;
            return p;
        }

        public double getMinParabol(double[] x0, double dx, int index)
        {
            double derivate1, derivate2;
            double tmp = x0[index];
            //derivate2 = getDerivate2(x0);

            for (derivate2 = getDerivate2(x0, index); derivate2 <= 0 && x0[index] < 100; derivate2 = getDerivate2(x0, index))//циклится
            {
                x0[index] += 1;
            }
            double x1 = x0[index];
            derivate1 = getDerivate1(x0, index);
            double x2 = x1 - derivate1 / derivate2;
            int n = 0;
            
            x0[index] = x1;
            double f1 = Fx.Func(x0);
            x0[index] = x2;
            double f2 = Fx.Func(x0);
            x0[index] = tmp;
            while (Math.Abs(x2 - x1) > dx && (f2 - f1) != 0 && n < 10000)
            {
                n++;
                x1 = x2;
                x0[index] = x1;
                derivate1 = getDerivate1(x0, index);
                derivate2 = getDerivate2(x0, index);
                x2 = x1 - derivate1 / derivate2;
            }
            x0[index] = tmp;
            //minArg tmp = new minArg();
            //tmp.x = x2;
            //tmp.f = Fx(x2);
            return x2;
            //if(derivate2 <=0)

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
            x[index] = tmp;
            return res;

        }


        public void _Gauss(double[] x0)
        {
            Fx.CC = 0;
            int K = 0;
            double[] x1 = new double[x0.Length];
            for(int i = 0; i<x0.Length; i++)
            {
                x1[i] = x0[i];
            }
            do
            {
                K++;
                for (int i = 0; i < x0.Length; i++)
                    x1[i] = goldenSection(x1, -100, 100, i, 1e-8);
                    //x1[i] = getMinDih(x0, i, -20, 30, 1e-5);
                    //x1[i] = getMinParabol(x0, 1e-10, i);
                double norm = 0;
                for(int i = 0; i<x0.Length; i++)
                {
                    norm += (x1[i] - x0[i]) * (x1[i] - x0[i]);
                }
                if (Math.Abs(Fx.Func(x1) - Fx.Func(x0)) <= 1e-10 && norm <= 1e-10)
                {
                    break;
                }
                else
                {
                    for (int i = 0; i < x0.Length; i++)
                    {
                        x0[i] = x1[i];
                    }
                }
            }
            while(K < 100000);

            Fx.f = Fx.Func(x1);
            Fx.x = x1;
            
        }
    }
}
