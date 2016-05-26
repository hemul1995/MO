using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethodsOptimisation
{
    class Barrier_Method
    {
        /// <summary>
        /// Метод барьерных функций
        /// </summary>
        /// <param name="x">Начальное приближение</param>
        /// <param name="r">Коэффициент штрафа</param>
        /// <param name="C">Коэффициент штрафа</param>
        /// <param name="eps">Точность</param>
        public void _Barrier_Method(double[] x, double r, double C, double eps = 1e-7)
        {
            const int MAXITER = 10000;
            Newton_Raffson n = new Newton_Raffson();
            int k = 0;
            while (true)
            {
                n._Newton_Raffson(x, true, r);
                double tmp = Fx.Func(Fx.x, r);
                if (k > MAXITER) break;
                if (Math.Abs(Fx.P) <= eps)
                {
                    x = (double[])Fx.x.Clone();
                    break;
                }
                else
                {
                    r = r / C;
                    x = (double[])Fx.x.Clone();
                    k++;
                }
            }
            Fx.x = x;
            Fx.f = Fx.Func(x);
        }
    }
}
