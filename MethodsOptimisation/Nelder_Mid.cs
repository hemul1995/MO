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
        public void _Nelder_Mid(double[] x, double r = 0)
        {
            int X_LENGTH = x.Length;
            int SMP_LENGTH = X_LENGTH + 1;
            point[] smp = new point[SMP_LENGTH];
            for (int i = 0; i < SMP_LENGTH; i++)
            {
                smp[i].x = new double[X_LENGTH];
            }
            double r1, r2;
            r1 = (Math.Sqrt(SMP_LENGTH) + X_LENGTH - 1) / (X_LENGTH * Math.Sqrt(2));
            r2 = (Math.Sqrt(SMP_LENGTH) - 1) / (X_LENGTH * Math.Sqrt(2));
            for (int i = 0; i < X_LENGTH; i++)
            {
                smp[0].x[i] = x[i];
                for (int j = 1; j < SMP_LENGTH; j++)
                {
                    if (j - 1 == i)
                    {
                        smp[j].x[i] = x[i] + r1;
                    }
                    else
                    {
                        smp[j].x[i] = x[i] + r2;
                    }
                }
            }
            //Fx.CC = 0;
            double[] xh = new double[X_LENGTH],
                     xg = new double[X_LENGTH],
                     xl = new double[X_LENGTH],
                     xr = new double[X_LENGTH],
                     xe = new double[X_LENGTH],
                     xs = new double[X_LENGTH],
                     xc = new double[X_LENGTH];
            double fh, fg, fl = 0, fr, fe, fs;
            double alpha = 1, beta = 0.5, gamma = 2;
            //double alpha = 2, beta = 0.25, gamma = 2.5;
            //double alpha = 1, beta = 0.5, gamma = 2.9;
            bool flag;
            for (int i = 0; i < SMP_LENGTH; i++)
                smp[i].f = Fx.Func(smp[i].x, r);

            int K = 0;
            double sf = 0;

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
                fr = Fx.Func(xr, r);

                //4
                if (fr <= fl)
                {
                    //4a
                    for (int i = 0; i < X_LENGTH; i++)
                    {
                        xe[i] = xc[i] * (1 - gamma) + xr[i] * gamma;

                    }
                    fe = Fx.Func(xe, r);
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
                    fs = Fx.Func(xs, r);
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

                sf = 0;
                double sr = 0;
                for (int i = 0; i < SMP_LENGTH; i++)
                    sr += smp[i].f;
                sr /= SMP_LENGTH;
                for (int i = 0; i < SMP_LENGTH; i++)
                    sf += (smp[i].f * smp[i].f - sr) * (smp[i].f * smp[i].f - sr);
                sf /= SMP_LENGTH;
                sr = 0;



            }
            while (K < 1000 && sf >= 1e-30);

            fl = Fx.Func(xl, r);

            Fx.f = fl;
            Fx.x = xl;
            //return fl;
        }
    }
}
