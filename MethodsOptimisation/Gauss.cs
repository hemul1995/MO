using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethodsOptimisation
{
    class Gauss
    {
        public double goldenSection(double[] x, double a, double b, int index, double eps)
        {
            double tmp = x[index];
            double T1, T2;
            T1 = 0.381966;
            T2 = 1 - T1;

            
            double x1, x2;
            x1 = a + (b - a) * T1;
            x2 = a + (b - a) * T2;
            x[index] = (x1 + x2) / 2;
            double F1, F2;
            x[index] -= eps;
            F1 = Fx.Func(x);
            x[index] += 2 * eps;
            F2 = Fx.Func(x);
            do
            {
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

        /// <summary>
        /// Метод Гаусса
        /// Вычисление минимума многомерной функции
        /// </summary>
        /// <param name="x0">Начальное приближение</param>
        public void _Gauss(double[] x0)
        {
            int K = 0;
            const int MAXITER = 100000;
            double[] x1 = new double[x0.Length];
            for(int i = 0; i<x0.Length; i++)
            {
                x1[i] = x0[i];
            }
            while(true)
            {
                if (K > MAXITER) break;
                K++;
                for (int i = 0; i < x0.Length; i++)
                    x1[i] = goldenSection(x1, -100, 100, i, 1e-8);
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
            Fx.f = Fx.Func(x1);
            Fx.x = x1;           
        }
    }
}
