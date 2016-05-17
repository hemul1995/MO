using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethodsOptimisation
{
    static class singleSearch
    {
        
        public static double f(double x)
        {
            return (x - 1) * (x - 1);
        }

        public static void Fibonachi(double x, double a = -100, double b = 100, double l = 1e-7, double eps = 1e-7)
        {
            List<double> FN = new List<double>();
            double y, z;
            double _f1 = 1, _f2 = 1, F = 1;
            for (int i = 3; ; i++)
            {
                F = _f1 + _f2;
                _f1 = _f2;
                _f2 = F;
                if (F >= Math.Abs(b - a) / l)
                {
                    FN.Add(F);
                    break;
                }
                else FN.Add(F);

            }
            int N = FN.Count - 1;

            int k = 0;

            y = a + FN[N - 2] / FN[N] * (b - a);
            z = a + FN[N - 1] / FN[N] * (b - a);
            double f1, f2;
            while (true)
            {
                //
                f1 = f(y);
                f2 = f(z);

                if (f1 <= f2)
                {
                    b = z;
                    z = y;
                    y = a + FN[N - k - 3] / FN[N - k - 1] * (b - a);
                }
                else
                {
                    a = y;
                    y = z;
                    z = a + FN[N - k - 2] / FN[N - k - 1] * (b - a);
                }
                if (k != N - 3) k++;
                else
                {
                    y = z = (a + b) / 2;
                    y = y = z;//даже не спрашивайте
                    z = y + eps;
                    if (f(y) <= f(z))
                    {
                        a = a;//и это не спрашивайте
                        b = z;
                    }
                    else
                    {
                        a = y;
                        b = b;//просто забейте, алгоритм такой :)
                    }
                    break;
                }
            }
        }

        #region

        //public minArg getMinPowell(double a, double b, double eps)//плохой метод
        //{
        //    double x1, x2, x3, dx, f1, f2, f3, a1, a2, x_a, fx_a, fmin, xmin = 0, s1, s2, s3;
        //    double[] x = new double[4];
        //    double[] f = new double[4];
        //    dx = 0.5;
        //    x[0] = a;
        //    x[1] = x[0] + dx;
        //    f[0] = Fx(x[0]);
        //    f[1] = Fx(x[1]);
        //    if (f[0] > f[1])
        //        x[2] = x[0] + 2 * dx;
        //    else
        //        x[2] = x[0] - dx;
        //    f[2] = Fx(x[2]);
        //    int i = 0;
        //    while (i < 100000)
        //    {
        //        i++;

        //        fmin = Math.Min(Math.Min(f[0], f[1]), Math.Min(f[1], f[2]));

        //        if (fmin.Equals(f[0])) xmin = x[0];
        //        if (fmin.Equals(f[1])) xmin = x[1];
        //        if (fmin.Equals(f[2])) xmin = x[2];

        //        a1 = (f[1] - f[0]) / (x[1] - x[0]);
        //        a2 = 1 / (x[2] - x[1]) * ((f[2] - f[0]) / (x[2] - x[0]) - (f[1] - f[0]) / (x[1] - x[0]));
        //        x[3] = (x[1] + x[0]) / 2 - a1 / (2 * a2);
        //        f[3] = Fx(x[3]);



        //        //if (Math.Abs(x[0] - x[1]) < eps && Math.Abs(f[0] - f[1]) < eps)
        //        //{
        //        //    break;
        //        //}
        //        if (Math.Abs(fmin - f[3]) < eps && Math.Abs(xmin - x[3]) < eps)
        //        {
        //            break;
        //        }

        //        double xt, ft;
        //        for (int j = 0; j < 4; j++)
        //            for (int k = 0; k < 4; k++)
        //                if (x[j] < x[k])
        //                {
        //                    xt = x[j];
        //                    x[j] = x[k];
        //                    x[k] = xt;

        //                    ft = f[j];
        //                    f[j] = f[k];
        //                    f[k] = ft;
        //                }
        //        //dx = dx / 2;

        //        //x[1] = x[3];
        //        //f[1] = f[3];
        //        //dx = dx / 2;
        //        //s1 = Math.Sign(x[1] - x[0]);
        //        //s2 = Math.Sign(x[2] - x[0]);
        //        //s3 = Math.Sign(x[3] - x[0]);
        //        //if(s1==s2 && s1 == -s3)
        //        //{
        //        //    x[2] = x[3];
        //        //    f[2] = f[3];
        //        //}
        //        //a1 = (f[1] - f[0]) / (x[1] - x[0]);
        //        //a2 = 1 / (x[2] - x[1]) * ((f[2] - f[0]) / (x[2] - x[0]) - (f[1] - f[0]) / (x[1] - x[0]));
        //        //x[3] = (x[1] + x[0]) / 2 - a1 / (2 * a2);
        //        //f[3] = Fx(x[3]);
        //    }
        //    //a = x[1];
        //    minArg tmp = new minArg();

        //    tmp.x = x[3];
        //    tmp.f = f[3];
        //   // b = x[3];
        //    return tmp;


            


        //}

        //public minArg getMinDih(double a, double b, double eps)
        //{
        //    double x;
        //    double f1, f2;

        //    do
        //    {
        //        x = (a + b) / 2;
        //        f1 = Fx(x - eps);
        //        f2 = Fx(x + eps);

        //        if (f1 < f2)
        //            b = x;
        //        else 
        //            a = x;
        //    }
        //    while (Math.Abs(b - a) > eps);
            
        //    x = (b + a) / 2;

        //    minArg tmp = new minArg();
        //    tmp.x = x;
        //    tmp.f = Fx(x);
        //    return tmp;
        //}

        //private double getDerivate1(double x)
        //{
        //    double eps = 1e-10;
        //    double h = 1, p1, p2, p;
        //    do
        //    {
        //        p1 = (Fx(x + h) - Fx(x - h)) / (2 * h);
        //        h /= 2;
        //        p2 = (Fx(x + h) - Fx(x - h)) / (2 * h);
        //    }
        //    while(p1 != p2 && h*h>eps*Math.Abs(x));
        //    p = p2;
        //    return p;
        //}

        //private double getDerivate2(double x)
        //{
        //    double eps = 1e-10;
        //    double h = 1, p1, p2, p;
        //    do
        //    {
        //        p1 = (Fx(x + h) + Fx(x - h) - 2 * Fx(x)) / (h * h);
        //        h /= 2;
        //        p2 = (Fx(x + h) + Fx(x - h) - 2 * Fx(x)) / (h * h);
        //    }
        //    while (p1 != p2 && h * h > eps * Math.Abs(x));
        //    p = p2;
        //    return p;
        //}

        //public minArg getMinParabol(double x0, double dx)
        //{
        //    double derivate1, derivate2;

        //    //derivate2 = getDerivate2(x0);

        //    for (derivate2 = getDerivate2(x0); derivate2 <= 0 && x0 < 100; derivate2 = getDerivate2(x0))//циклится
        //    {
        //        x0 += 1;
        //    }
        //    double x1 = x0;
        //    derivate1 = getDerivate1(x0);
        //    double x2 = x1 - derivate1 / derivate2;
        //    int n = 0;
        //    while(Math.Abs(x2 - x1) > dx && (Fx(x2) - Fx(x1)) != 0 && n < 100000)
        //    {
        //        n++;
        //        x1 = x2;
        //        derivate1 = getDerivate1(x1);
        //        derivate2 = getDerivate2(x1);
        //        x2 = x1 - derivate1 / derivate2;
        //    }

        //    minArg tmp = new minArg();
        //    tmp.x = x2;
        //    tmp.f = Fx(x2);
        //    return tmp;
        //    //if(derivate2 <=0)

        //}
        #endregion
    }
}
