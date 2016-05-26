using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethodsOptimisation
{
    class Penalty_Method
    {
        /// <summary>
        /// Метод штрафных функций
        /// </summary>
        /// <param name="x">Начальное приближение</param>
        /// <param name="r">Коэффициент штрафа</param>
        /// <param name="C">Коэффициент штрафа</param>
        /// <param name="eps">Точность</param>
        public void _Penalty_Method(double[] x, double r, double C, double eps = 1e-7)
        {
            const int MAXITER = 10000;
            Nelder_Mid n = new Nelder_Mid();
            Newton_Raffson q = new Newton_Raffson();
            int k = 0;
            while (true)
            {
                n._Nelder_Mid(x, r);
                double tmp = Fx.Func(Fx.x, r);
                if (k > MAXITER) break;
                if (Fx.P <= eps)
                {
                    x = (double[])Fx.x.Clone();
                    break;
                }
                else
                {
                    r = C * r;
                    x = (double[])Fx.x.Clone();
                    k++;
                }
            }
            Fx.x = x;
            Fx.f = Fx.Func(x);
        }
    }
}
