using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethodsOptimisation
{
    class Barrier_Method
    {
        public void _Barrier_Method(double[] x, double r, double C, double eps = 1e-7)
        {
            int MAXITER = 10000;
            Newton_Raffson n = new Newton_Raffson();
            Fx.CC = 0;

            /*Равенства*/


            /*Неравенства*/



            int k = 0;
            while (true)
            {
                n._Newton_Raffson(x, r);
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
