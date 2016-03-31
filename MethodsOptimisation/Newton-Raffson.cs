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
            double eps = 1e-10;
            double h = 1, p1, p2, p;
            double tmp = x[index];           
            double f1, f2;
            do
            {
                x[index] -= h;
                f1 = Fx.Func(x);
                x[index] += 2 * h;
                f2 = Fx.Func(x);
                p1 = (f1 - f2) / (2 * h);
                x[index] = tmp;
                h /= 2;
                x[index] -= h;
                f1 = Fx.Func(x);
                x[index] += 2 * h;
                f2 = Fx.Func(x);
                p2 = (f1 - f2) / (2 * h);
                x[index] = tmp;
            }
            while (p1 != p2 && h * h > eps * Math.Abs(x[index]));
            p = p2;
            return p;
        }

        double SecondDerivative(double[] x, int index1, int index2, double hx = 1e-5, double hy = 1e-5)
        {
            double tmp1 = x[index1], tmp2 = x[index2];
            double f1, f2, f3, f4;
            x[index1] +=hx;
            x[index2] +=hy;
            f1 = Fx.Func(x);
            x[index2] -= 2 * hy;
            f2 = Fx.Func(x);
            x[index1] -= 2 * hx;
            f4 = Fx.Func(x);
            x[index2] += 2 * hy;
            f3 = Fx.Func(x);

            x[index1] = tmp1;
            x[index2] = tmp2;
            return (f1 - f2 - f3 + f4) / (4 * hx * hy);
        }
        void gesse(Matrix<double> Gesse, double[] x, double LENGTH)
        {
            for(int i = 0; i<LENGTH; i++)
                for(int j = i; j<LENGTH; j++)
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
            for(int i = Gesse.RowCount-1; i > 0; i--)
            {
                det = G.Determinant();
                if (det < 0) return false; 
                G = G.RemoveRow(i);
                G = G.RemoveColumn(i);

            }
            return true;
            
        }


        private double getDerivate11(double[] x, int index)
        {
            double eps = 1e-10;
            double h = 1, p1, p2, p;
            double tmp = x[index];
            double f1, f2;

            do
            {

                x[index] = tmp + h;
                f1 = Fx.Func(x);
                x[index] = tmp - h;
                f2 = Fx.Func(x);


                p1 = (f1 - f2) / (2 * h);
                h /= 2;

                x[index] = tmp + h;
                f1 = Fx.Func(x);
                x[index] = tmp - h;
                f2 = Fx.Func(x);
                x[index] = tmp;

                p2 = (f1 - f2) / (2 * h);
            }
            while (p1 != p2 && h * h > eps * Math.Abs(x[index]));
            p = p2;
            x[index] = tmp;
            return p;
        }

        private double getDerivate12(double[] x, int index)
        {
            double eps = 1e-10;
            double h = 1, p1, p2, p;
            double tmp = x[index];
            double f1, f2, f3;
            do
            {


                x[index] = tmp + h;
                f1 = Fx.Func(x);
                x[index] = tmp - h;
                f2 = Fx.Func(x);
                x[index] = tmp;
                f3 = Fx.Func(x);

                p1 = (f1 + f2 - 2 * f3) / (h * h);
                h /= 2;



                x[index] = tmp + h;
                f1 = Fx.Func(x);
                x[index] = tmp - h;
                f2 = Fx.Func(x);
                x[index] = tmp;

                p2 = (f1 + f2 - 2 * f3) / (h * h);
            }
            while (p1 != p2 && h * h > eps * Math.Abs(x[index]));
            p = p2;
            x[index] = tmp;
            return p;
        }

        public double getMinParabol(double[] x0, double dx, int index)
        {
            double derivate1, derivate2;
            double tmp = x0[index];
            //derivate2 = getDerivate2(x0);

            for (derivate2 = getDerivate12(x0, index); derivate2 <= 0 && x0[index] < 100; derivate2 = getDerivate12(x0, index))//циклится
            {
                x0[index] += 1;
            }
            double x1 = x0[index];
            derivate1 = getDerivate11(x0, index);
            double x2 = x1 - derivate1 / derivate2;
            int n = 0;

            x0[index] = x1;
            double f1 = Fx.Func(x0);
            x0[index] = x2;
            double f2 = Fx.Func(x0);
            x0[index] = tmp;
            while (Math.Abs(x2 - x1) > dx && (f2 - f1) != 0 && n < 100000)
            {
                n++;
                x1 = x2;
                x0[index] = x1;
                derivate1 = getDerivate11(x0, index);
                derivate2 = getDerivate12(x0, index);
                x2 = x1 - derivate1 / derivate2;
            }
            x0[index] = tmp;
            //minArg tmp = new minArg();
            //tmp.x = x2;
            //tmp.f = Fx(x2);
            return x2;
            //if(derivate2 <=0)

        }

        double[] A = new double[5] { 2, 0.1, 0.1, 0.1, 0 };
        double[] B = new double[5] { 100, 2, 0.45, 0.45, 1 };
        public double getMinDih(double[] x0, int index, double a, double b, double eps)
        {
            double x;
            double f1, f2;
            double tmp = x0[index];

            do
            {
                x0[index] = (a + b) / 2;
                x0[index] -= eps;
                f1 = Fx.Func(x0);
                x0[index] += 2 * eps;
                f2 = Fx.Func(x0);

                if (f1 < f2)
                    b = x0[index];
                else
                    a = x0[index];
            }
            while (Math.Abs(b - a) > 1e-2);

            x0[index] = (b + a) / 2;
            x = x0[index];
            x0[index] = tmp;
            return x;
        }
        public void _Newton_Raffson(double[] x, double eps = 1e-5)
        {
            Fx.CC = 0;
            int k = 0;
            int LENGTH = x.Length;
            double det;
            double[] tk = new double[LENGTH];
            for (int i = 0; i < LENGTH; i++)
                tk[i] = 1;
            double[] x1 = new double[LENGTH];
            double norm = 0;
            double[] grad = new double[LENGTH], dk = new double[LENGTH];
            Matrix<double> Gesse = DenseMatrix.OfArray(new double[LENGTH, LENGTH]);
            while (k < 10000)
            {
                //1
                for (int i = 0; i < LENGTH; i++)
                {
                    grad[i] = getDerivate1(x, i);
                }

                //2
                for (int i = 0; i < LENGTH; i++)
                {
                    norm += grad[i] * grad[i];
                }
                if (Math.Sqrt(norm) < eps) break;

                //3
                gesse(Gesse, x, LENGTH);

                //4
                Gesse = Gesse.Inverse();

                //5
                if(positivOpred(Gesse))
                {
                    det = Gesse.Determinant();
                    for(int i = 0; i<LENGTH; i++)
                    {
                        dk[i] = -det * grad[i];
                    }
                }
                else
                {
                    for (int i = 0; i < LENGTH; i++)
                    {
                        dk[i] = -grad[i];
                    }
                }

                //6
                for(int i = 0; i<LENGTH; i++)
                {
                    x1[i] = x[i] + tk[i] * dk[i];
                }

                //7
                for(int i = 0; i<LENGTH; i++)
                {
                    //tk[i] = getMinParabol(x1, 1e-5, i);
                    tk[i] = getMinDih(x1, i, -5, 5, 1e-5);
                }
                //8
                for(int i = 0; i<LENGTH; i++)
                {
                    x[i] = x[i] + tk[i] * dk[i];
                    
                }
                k++;

            }



            Fx.f = Fx.Func(x);
            Fx.x = x;
        }
    }
}
