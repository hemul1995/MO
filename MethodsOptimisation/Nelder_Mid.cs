using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethodsOptimisation
{
    struct point
    {
        public double f;
        public double[] x;
    }
    class Nelder_Mid
    {
        int CC = 0;
        

        double Fx(double[] arg)
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
        public double _Nelder_Mid(point[] smp, int X_LENGTH = 2)
        {
            int SMP_LENGTH = X_LENGTH + 1;

            double[] xh = new double[X_LENGTH],
                     xg = new double[X_LENGTH],
                     xl = new double[X_LENGTH],
                     xr = new double[X_LENGTH],
                     xe = new double[X_LENGTH],
                     xs = new double[X_LENGTH],
                     xc = new double[X_LENGTH]; 
            double fh, fg, fl = 0, fr, fe, fs;
            double alpha = 1, beta = 0.5, gamma = 2;
            bool flag;
            for (int i = 0; i < SMP_LENGTH; i++)
                smp[i].f = Fx(smp[i].x);

            int K = 0;


            do
            {
                K++;
                Array.Sort(smp, new Comparison<point>((a, b) => a.f.CompareTo(b.f)));
                for (int i = 0; i < X_LENGTH; i++)
                {
                    xh[i] = smp[SMP_LENGTH - 1].x[i];
                    xg[i] = smp[SMP_LENGTH - 2].x[i];
                    xl[i] = smp[0].x[i];
                }

                fh = smp[SMP_LENGTH - 1].f;
                fg = smp[SMP_LENGTH - 2].f;
                fl = smp[0].f;
                //2
                for (int i = 0; i < X_LENGTH; i++)
                    xc[i] = 0;
                for (int i = 0; i < SMP_LENGTH - 1; i++)
                {
                    for (int j = 0; j < X_LENGTH; j++)
                        xc[j] += smp[i].x[j];

                }
                for (int i = 0; i < X_LENGTH; i++)
                    xc[i] /= (SMP_LENGTH - 1);

                //3
                for (int i = 0; i < X_LENGTH; i++)
                {
                    xr[i] = xc[i] * (1 + alpha) - xh[i] * alpha;
                }
                fr = Fx(xr);

                //4
                if (fr <= fl)
                {
                    //4a
                    for (int i = 0; i < X_LENGTH; i++)
                    {
                        xe[i] = xc[i] * (1 - gamma) + xr[i] * gamma;

                    }
                    fe = Fx(xe);
                    if (fe < fr)
                    {
                        for (int i = 0; i < X_LENGTH; i++)
                        {
                            smp[SMP_LENGTH - 1].x[i] = xe[i];

                        }
                        smp[SMP_LENGTH - 1].f = fe;
                    }
                    else
                    {
                        for (int i = 0; i < X_LENGTH; i++)
                        {
                            smp[SMP_LENGTH - 1].x[i] = xr[i];

                        }
                        smp[SMP_LENGTH - 1].f = fr;
                    }

                }
                if (fl < fr && fr <= fg)
                {
                    //4b
                    for (int i = 0; i < X_LENGTH; i++)
                    {
                        smp[SMP_LENGTH - 1].x[i] = xr[i];

                    }
                    smp[SMP_LENGTH - 1].f = fr;
                }
                flag = false;
                if (fh >= fr && fr > fg)
                {
                    //4c
                    flag = true;
                    //tmpD = fh;
                    for (int i = 0; i < X_LENGTH; i++)
                    {
                        //tmpV[i] = xh[i];
                        smp[SMP_LENGTH - 1].x[i] = xr[i];

                        //xr[i] = tmpV[i];//
                        xr[i] = xh[i];//
                    }


                    smp[SMP_LENGTH - 1].f = fr;

                    //fr = tmpD;
                    fr = fh;
                }
                //4d
                if (fr > fh) flag = true;

                if (flag)
                {
                    //5
                    for (int i = 0; i < X_LENGTH; i++)
                    {
                        xs[i] = xh[i] * beta + xc[i] * (1 - beta);
                    }
                    fs = Fx(xs);
                    if (fs < fh)
                    {
                        //6
                        //tmpD = fh;
                        for (int i = 0; i < X_LENGTH; i++)
                        {
                            //tmpV[i] = xh[i];
                            smp[SMP_LENGTH - 1].x[i] = xs[i];//
                            //xs[i] = tmpV[i];//
                            xs[i] = xh[i];

                        }
                        smp[SMP_LENGTH - 1].f = fs;
                        fs = fh;
                        //fs = tmpD;

                    }
                    else
                    {
                        //7
                        for (int i = 0; i < SMP_LENGTH; i++)
                            for (int j = 0; j < X_LENGTH; j++)
                                smp[i].x[j] = xl[j] + (smp[i].x[j] - xl[j]) / 2;
                                //smp[i].x[j] = xl[j] = xl[j] + (smp[i].x[j] - xl[j]) / 2;
                    }
                }

                
                
            }
            while (K < 1000 /*&& Math.Abs(fl) >= 1e-30*/);
            fl = Fx(xl);
            return fl;
        }
    }
}
