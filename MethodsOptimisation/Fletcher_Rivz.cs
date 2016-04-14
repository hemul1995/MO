using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethodsOptimisation
{
    class Fletcher_Rivz
    {

        private double getDerivate1(double[] x, int index)
        {
            double h = 1e-7, p1, p;
            double tmp = x[index];
            double f1, f2;

                x[index] -= h;
                f1 = Fx.Func(x);
                x[index] += 2 * h;
                f2 = Fx.Func(x);
                p1 = (f1 - f2) / (2 * h);
                x[index] = tmp;
            p = p1;
            return p;
        }

        public double goldenSection(double[] x, double a, double b, int index, double eps)
        {
            double tmp = x[index];
            double T1, T2;
            T1 = 0.381966;
            T2 = 1 - T1;

            do
            {
                double x1, x2;
                x1 = a + (b - a) * T1;
                x2 = a + (b - a) * T2;
                x[index] = (x1 + x2) / 2;
                double F1, F2;
                x[index] -= eps;
                F1 = Fx.Func(x);
                x[index] += 2 * eps;
                F2 = Fx.Func(x);

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

        
        public void _Fletcher_Rivz(double[] x, double eps)
        {
            const int MAXITER = 1000;
            double[] S = new double[x.Length];
            double[] xj = new double[x.Length];
            double[] xj1 = new double[x.Length];
            double[] λ = new double[x.Length];
            double[] tmp = new double[x.Length];
            double[] grad = new double[x.Length];
            int j;
            int k = 0;
        TWO: ;
            j = 0;
            for (int i = 0; i < x.Length; i++ )
            {
                S[i] = -getDerivate1(x, i);
            }

            for (int i = 0; i < x.Length; i++ )
            {
                xj[i] = x[i];
            }

        THREE: ;
            for(int i = 0; i < x.Length; i++)
            {
                λ[i] = goldenSection(x, -2, 5, i, 1e-8);
                λ[i] = (λ[i] - xj[i]) / S[i];
                xj1[i] = xj[i] + λ[i] * S[i];
                grad[i] = -getDerivate1(xj1, i);
            }

            for (int i = 0; i < x.Length; i++)
            {
                tmp[i] = xj1[i] - xj[i];
            }
            double ω = 0;
            //ω = grad.Sum((double a) => a * a) / x.Sum((double b) => b * b);
            ω = Math.Sqrt(grad.Sum((double a) => a * a)) / Math.Sqrt(x.Sum((double b) => b * b));

            //double foo = 0;
            //for (int i = 0; i < x.Length; i++)
            //{
            //    foo += grad[i] * tmp[i];
            //}
            //foo /= x.Sum((double b) => b * b);
            //ω = Math.Max(0, foo);
            
            for (int i = 0; i < x.Length; i++)
            {
                S[i] = -getDerivate1(xj1, i) + ω * S[i];
            }
            double norm = Math.Sqrt(S.Sum((double a) => a * a));

            

            double norm1 = Math.Sqrt(tmp.Sum((double a) => a * a));

            if(k > MAXITER) goto EXIT;
            if(norm < eps || norm1 < eps)
            {
                goto EXIT;
            }
            else
            {
                j++;
                if (j < x.Length) goto THREE;
                else
                {
                    k++;
                    for(int i = 0; i<x.Length; i++)
                    {
                        x[i] = xj1[i];
                    }
                    goto TWO;
                }
            }






        EXIT: ;
            Fx.x = x;
            Fx.f = Fx.Func(x);
            //cout << "Fletcher-Rieves Method: " << endl; 
            //PrintSolution(x, F1(x), numIter);
        }
    }
}
