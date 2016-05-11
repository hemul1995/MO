using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
namespace MethodsOptimisation
{
    class Newton_Raffson
    {
        private double getDerivate1(double[] x, int index)
        {
            double h = 1e-7, p1, p;
            double tmp = x[index];
            double f1, f2;
            x[index] -= h;
            f1 = Fx.Func(x, R);
            x[index] += 2 * h;
            f2 = Fx.Func(x, R);
            p1 = (f1 - f2) / (2 * h);
            x[index] = tmp;

            p = p1;
            return p;
        }
        public double goldenSection(double[] x, double[] dk, double a, double b, double eps)
        {
            //double tmp = x[index];
            double T1;
            T1 = 0.381966;
            //T2 = 1 - T1;
            double[] tmpMass = new double[x.Length];
            //do
            //{
            double x1, x2;
            x1 = a + (b - a) * T1;
            x2 = b - (b - a) * T1;
            //x[index] = (x1 + x2) / 2;

            double F1, F2;
            tmpMass.ToList().Select((z, i) => (tmpMass[i] = x[i] + x1 * dk[i])).ToArray();
            F1 = Fx.Func(tmpMass, R);
            tmpMass.ToList().Select((z, i) => (tmpMass[i] = x[i] + x2 * dk[i])).ToArray();
            F2 = Fx.Func(tmpMass, R);
            do
            {
                if (F1 < F2)
                {
                    b = x2;
                    if (Math.Abs(b - a) < eps) break;
                    x2 = x1;
                    F2 = F1;
                    x1 = a + (b - a) * T1;
                    tmpMass.ToList().Select((z, i) => (tmpMass[i] = x[i] + x1 * dk[i])).ToArray();
                    F1 = Fx.Func(tmpMass, R);
                }
                else
                {
                    a = x1;
                    if (Math.Abs(b - a) < eps) break;
                    x1 = x2;
                    F1 = F2;
                    x2 = b - (b - a) * T1;
                    tmpMass.ToList().Select((z, i) => (tmpMass[i] = x[i] + x2 * dk[i])).ToArray();
                    F2 = Fx.Func(tmpMass, R);
                }
            }
            while (true);

            double res = (a + b) / 2;
            //x[index] = tmp;

            return res;

        }
        double SecondDerivative(double[] x, int index1, int index2, double hx = 1e-7, double hy = 1e-7)
        {
            double tmp1 = x[index1], tmp2 = x[index2];
            double f1, f2, f3, f4;
            x[index1] += hx;
            x[index2] += hy;
            f1 = Fx.Func(x, R);
            x[index2] -= 2 * hy;
            f2 = Fx.Func(x, R);
            x[index1] -= 2 * hx;
            f4 = Fx.Func(x, R);
            x[index2] += 2 * hy;
            f3 = Fx.Func(x, R);

            x[index1] = tmp1;
            x[index2] = tmp2;
            return (f1 - f2 - f3 + f4) / (4 * hx * hy);
        }
        void gesse(Matrix<double> Gesse, double[] x, double LENGTH)
        {
            for (int i = 0; i < LENGTH; i++)
                for (int j = i; j < LENGTH; j++)
                {
                    Gesse[i, j] = Gesse[j, i] = SecondDerivative(x, i, j);
                }

        }

        bool positivOpred(Matrix<double> Gesse)
        {
            Matrix<double> G = Gesse.Clone();
            double det;
            det = G[0, 0];
            if (det < 0) return false;
            for (int i = Gesse.RowCount - 1; i > 0; i--)
            {
                det = G.Determinant();
                if (det < 0) return false;
                G = G.RemoveRow(i);
                G = G.RemoveColumn(i);

            }
            return true;

        }

        double R;
        public void _Newton_Raffson(double[] x, double r = 0, double eps1 = 1e-7, double eps2 = 1e-7)
        {
            R = r;
            //Fx.CC = 0;
            int MAXITER = 10000;
            int LENGTH = x.Length;
            double[] grad = new double[LENGTH];
            Matrix<double> Gesse = DenseMatrix.OfArray(new double[LENGTH, LENGTH]);
            int k = 0;
            double[] dk = new double[LENGTH];
            double[] x1 = new double[LENGTH];
            double tk;
            while (true)
            {
                //3
                for (int i = 0; i < LENGTH; i++)
                {
                    grad[i] = getDerivate1(x, i);
                }

                //4
                if (Math.Sqrt(grad.Sum(a => a * a)) <= eps1 /*|| Math.Sqrt(grad.Sum(a => a * a)) >= 1 / eps1*/) break;  //2 условие - костыль

                //5
                if (k >= MAXITER) break;

                //6
                gesse(Gesse, x, LENGTH);

                //7
                Gesse = Gesse.Inverse();

                //8
                if (positivOpred(Gesse) == true)
                {
                    /*перемножить матрицу на вектор*/
                    double[] tmp = new double[LENGTH];
                    for (int i = 0; i < LENGTH; i++)
                        for (int j = 0; j < LENGTH; j++)
                        {
                            tmp[i] += Gesse[i, j] * grad[j];
                        }


                    for (int i = 0; i < LENGTH; i++)
                    {
                        dk[i] = -tmp[i];
                    }
                }
                else
                {
                    for (int i = 0; i < LENGTH; i++)
                    {
                        dk[i] = -grad[i];
                    }
                }

                //9-10
                tk = goldenSection(x, dk, -100, 100, 1e-7);

                //11
                for (int i = 0; i < LENGTH; i++)
                {
                    x1[i] = x[i] + tk * dk[i];
                    //    if (x1[i] > 2) x1[i] = 2;
                    //    if (x1[i] < 1.2) x1[i] = 1.2;
                }

                //12
                double norm = 0;
                for (int i = 0; i < LENGTH; i++)
                {
                    norm += (x1[i] - x[i]) * (x1[i] - x[i]);
                }
                if (Math.Abs(Fx.Func(x1, R) - Fx.Func(x, R)) < eps2 && Math.Sqrt(norm) < eps2) break;
                else
                {
                    for (int i = 0; i < LENGTH; i++)
                    {
                        x[i] = x1[i];
                    }
                    k++;
                }
            }

            Fx.x = x;
            Fx.f = Fx.Func(x, R);
        }


    }
}
