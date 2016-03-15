using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethodsOptimisation
{
    static class Fx
    {
        public static int CC;

        public static double Func(double[] arg)
        {

            CC++;
            double func;
            //double y = 0;
            func = (1 - arg[0]) * (1 - arg[0]) + 100 * (arg[1] - arg[0] * arg[0]) * (arg[1] - arg[0] * arg[0]);
            //func = -Math.Cos(arg[0]) * Math.Cos(arg[1]) * Math.Exp(-((arg[0] - Math.PI) * (arg[0] - Math.PI) + (arg[1] - Math.PI) * (arg[1] - Math.PI)));
            //func = Math.Sin(arg[0]) + Math.Sin(arg[1]) + Math.Sin(arg[2]);
            //func = (arg[0] + 2 * arg[1] - 7) * (arg[0] + 2 * arg[1] - 7) + (arg[1] + 2 * arg[0] - 5) * (arg[1] + 2 * arg[0] - 5);
            //func = 5 * (arg[0] - arg[1]) * (arg[0] - arg[1]) + (1 - arg[0]) * (1 - arg[0]);


            //////double x = arg[0], y = arg[1], z = arg[2], u = arg[3], v = arg[4];
            //////double p, s, t, q;
            //////p = v * z / (1 - z) + u * (1 - v) / (1 - u);
            //////s = -x * y * v / (1 - z) - (1 - v) / (1 - u);
            //////t = x * v / (1 - z * z) + (1 - v) / (1 - u * u);
            //////q = x * z * v / (1 - z * z) + u * (1 - v) / (1 - u * u);
            //////func = 2 * p * s / (t + q) + v * y * (1 + z) / (1 - z) + (1 - v) * (1 + u) / (1 - u);
            //////func = -s / (t + q);

            return func;
        }
            
    }
}
