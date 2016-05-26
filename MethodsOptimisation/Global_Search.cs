using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethodsOptimisation
{
    static class RandomExtension
    {
        public static double NextDouble(this Random r, double min, double max)
        {
            return min + r.NextDouble() * (max - min);
        }

        public static double NextDouble(this Random r, double max)
        {
            return r.NextDouble() * max;
        }
    }
    class Global_Search
    {
        /// <summary>
        /// Генерация точки в N-мерном прямоугольнике
        /// </summary>
        /// <returns></returns>
        public double[] RndInArea()
        {
            Random rnd = new Random();
            double[] x = new double[2];
            x[0] = rnd.NextDouble(-50, 50);
            x[1] = rnd.NextDouble(-50, 50);
            return x;
        }
        /// <summary>
        /// Метод глобального поиска
        /// </summary>
        /// <param name="x"></param>
        public void _Global_Search(double[] x)
        {
            double[] xmin = new double[x.Length];
            double[] xk = new double[x.Length];
            double fk = Double.MaxValue;
            double k = 0, K = 100;

            while(true)
            {
                if (k > K) break;
                x = (double[])RndInArea().Clone();
                //Newton_Raffson n = new Newton_Raffson();
                //n._Newton_Raffson(x);
                Nelder_Mid q = new Nelder_Mid();
                q._Nelder_Mid(x);
                xk = (double[])Fx.x.Clone();
                if (Fx.f < fk)
                {
                    k = 0;
                    xmin = (double[])xk.Clone();
                    fk = Fx.f;
                }
                else k++;
            }
            Fx.x = (double[])xmin.Clone();
            Fx.f = Fx.Func(xmin);
        }
    }
}
