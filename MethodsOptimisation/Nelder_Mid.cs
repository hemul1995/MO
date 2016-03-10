using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethodsOptimisation
{
    class Nelder_Mid
    {
        
        struct point
        {
            public double f;
            public double[] x;
        }

        double Fx(double[] x)
        {
            double y = 0;
            y = (1 - x[0]) * (1 - x[0]) + 100 * (x[1] - x[0] * x[0]) * (x[1] - x[0] * x[0]);
            //y = Math.Sin(x[0]) + Math.Sin(x[1]) + Math.Sin(x[2]);

            //// double x = x0[0], y = x0[1], z = x0[2], u = x0[3], v = x0[4];

            //// double p, s, t, q;
            //// p = v * z / (1 - z) + u * (1 - v) / (1 - u);
            //// s = -x * y * v / (1 - z) - (1 - v) / (1 - u);
            //// t = x * v / (1 - z * z) + (1 - v) / (1 - u * u);
            //// q = x * z * v / (1 - z * z) + u * (1 - v) / (1 - u * u);

            //// //double func;
            //// //func = 2 * p * s / (t + q) + v * y * (1 + z) / (1 - z) + (1 - v) * (1 + u) / (1 - u);

            ////// func = -s / (t + q);
            //y = 5 * (x0[0] - x0[1]) * (x0[0] - x0[1]) + (1 - x0[0]) * (1 - x0[0]);
            return y;
        }
        public double _Nelder_Mid(/*point[] smp*/)
        {
            point[] smp = new point[3];
            smp[0].x = new double[2];
            smp[1].x = new double[2];
            smp[2].x = new double[2];

            smp[0].x[0] = 10;
            smp[0].x[1] = 9;

            smp[1].x[0] = 10;
            smp[1].x[1] = -2;

            smp[2].x[0] = 21;
            smp[2].x[1] = 1;
            double[] xh = new double[2], xg = new double[2], xl = new double[2], xr = new double[2], xe = new double[2], xs = new double[2], xc = new double[2], tmpV = new double[2];
            double fh, fg, fl = 0, fr, fe, fs, tmpD;
            double alpha = 1, beta = 0.5, gamma = 2;
            bool flag;
            for (int i = 0; i < smp.Length; i++)
                smp[i].f = Fx(smp[i].x);

            int K = 0;


                while (K < 1000)
                {
                    K++;
                    Array.Sort(smp, new Comparison<point>((a, b) => a.f.CompareTo(b.f)));
                    for (int i = 0; i < xc.Length; i++)
                        xh[i] = smp[smp.Length - 1].x[i];
                    for (int i = 0; i < xc.Length; i++)
                        xg[i] = smp[smp.Length - 2].x[i];
                    for (int i = 0; i < xc.Length; i++)
                        xl[i] = smp[0].x[i];


                    fh = smp[smp.Length - 1].f;
                    fg = smp[smp.Length - 2].f;
                    fl = smp[0].f;
                    //2
                    for (int i = 0; i < xc.Length; i++)
                        xc[i] = 0;
                    for (int i = 0; i < smp.Length - 1; i++)
                    {
                        for (int j = 0; j < xc.Length; j++)
                            xc[j] += smp[i].x[j];

                    }
                    for (int i = 0; i < xc.Length; i++)
                        xc[i] /= (smp.Length - 1);
                    //3
                    for (int i = 0; i < xc.Length; i++)
                    {
                        xr[i] = xc[i] * (1 + alpha) - xh[i] * alpha;
                    }
                    fr = Fx(xr);
                    //4
                    if (fr <= fl)
                    {
                        //4a
                        for (int i = 0; i < xc.Length; i++)
                        {
                            xe[i] = xc[i] * (1 - gamma) + xr[i] * gamma;

                        }
                        fe = Fx(xe);
                        if (fe < fl)
                        {
                            for (int i = 0; i < xc.Length; i++)
                            {
                                smp[smp.Length - 1].x[i] = xe[i];

                            }
                            smp[smp.Length - 1].f = fe;
                        }
                        else
                        {
                            for (int i = 0; i < xc.Length; i++)
                            {
                                smp[smp.Length - 1].x[i] = xr[i];

                            }
                            smp[smp.Length - 1].f = fr;
                        }

                    }
                    if (fl < fr && fr <= fg)
                    {
                        //4b
                        for (int i = 0; i < xc.Length; i++)
                        {
                            smp[smp.Length - 1].x[i] = xr[i];

                        }
                        smp[smp.Length - 1].f = fr;
                    }
                    flag = false;
                    if (fh >= fr && fr > fg)
                    {
                        //4c
                        flag = true;
                        tmpD = fh;
                        for (int i = 0; i < xc.Length; i++)
                        {
                            tmpV[i] = xh[i];

                        }

                        for (int i = 0; i < xc.Length; i++)
                        {
                            smp[smp.Length - 1].x[i] = xr[i];

                        }
                        smp[smp.Length - 1].f = fr;
                        for (int i = 0; i < xc.Length; i++)
                        {
                            xr[i] = tmpV[i];

                        }
                        fr = tmpD;
                    }
                    //4d
                    if (fr > fh) flag = true;

                    if (flag)
                    {
                        //5
                        for (int i = 0; i < xc.Length; i++)
                        {
                            xs[i] = xh[i] * beta + xc[i] * (1 - beta);
                        }
                        fs = Fx(xs);
                        if (fs < fh)
                        {
                            //6
                            tmpD = fh;
                            for (int i = 0; i < xc.Length; i++)
                            {
                                tmpV[i] = xh[i];

                            }
                            for (int i = 0; i < xc.Length; i++)
                            {
                                smp[smp.Length - 1].x[i] = xs[i];

                            }
                            smp[smp.Length - 1].f = fs;
                            for (int i = 0; i < xc.Length; i++)
                            {
                                xs[i] = tmpV[i];

                            }
                            fs = tmpD;

                        }
                        else
                        {
                            //7
                            for (int i = 0; i < smp.Length; i++)
                                for (int j = 0; j < xc.Length; j++)
                                    smp[i].x[j] = xl[j] + (smp[i].x[j] - xl[j]) / 2;
                        }
                    }
                }
            return fl;
        }
    }
}
