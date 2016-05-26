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
        /// <summary>
        /// Вычисление 1 производной функции
        /// </summary>
        /// <param name="x">В точке</param>
        /// <param name="index">По какой переменной</param>
        /// <returns></returns>
        private double getDerivate(double[] x, int index)
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
        /// <summary>
        /// Метод Золотого сечения
        /// Вычисление минимума одномерной функции f(x + tk * dk) относительно tk
        /// </summary>
        /// <param name="x"></param>
        /// <param name="dk"></param>
        /// <param name="a">Левая граница</param>
        /// <param name="b">Правая граница</param>
        /// <param name="eps">Точность</param>
        /// <returns>tk</returns>
        public double goldenSection(double[] x, double[] dk, double a, double b, double eps)
        {
            double T1;
            T1 = 0.381966;
            double[] tmpMass = new double[x.Length];
            double x1, x2;
            x1 = a + (b - a) * T1;
            x2 = b - (b - a) * T1;
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
            return res;
        }
        /// <summary>
        /// Вычисление 2 производной
        /// </summary>
        /// <param name="x">В точке</param>
        /// <param name="index1">По какой переменной</param>
        /// <param name="index2">По какой переменной</param>
        /// <param name="hx">Точность</param>
        /// <param name="hy">Точность</param>
        /// <returns></returns>
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
        /// <summary>
        /// Построение матрицы 2 производных
        /// </summary>
        /// <param name="Gesse">Её строим</param>
        /// <param name="x"></param>
        /// <param name="LENGTH"></param>
        void buildGesse(Matrix<double> Gesse, double[] x, double LENGTH)
        {
            for (int i = 0; i < LENGTH; i++)
                for (int j = i; j < LENGTH; j++)
                {
                    Gesse[i, j] = Gesse[j, i] = SecondDerivative(x, i, j);
                }
        }

        /// <summary>
        /// Проверка на положительную определенность матрицы
        /// </summary>
        /// <param name="Gesse"></param>
        /// <returns></returns>
        bool criterionSilvestr(Matrix<double> Gesse)
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

        /// <summary>
        /// Метод Фибоначчи
        /// Вычисление минимума одномерной функции f(x + tk * dk) относительно tk
        /// </summary>
        /// <param name="x"></param>
        /// <param name="dk"></param>
        /// <param name="a">Левая граница</param>
        /// <param name="b">Правая граница</param>
        /// <param name="l">Длина конечного интервала</param>
        /// <param name="eps">Точность</param>
        /// <returns>tk</returns>
        public double Fibonachi(double[] x, double[] dk, double a = -100, double b = 100, double l = 1e-7, double eps = 1e-7)
        {
            double[] tmpMass = new double[x.Length];
            List<double> FN = new List<double>();
            double y, z;
            double _f1 = 1, _f2 = 1, F = 1;
            while(true)
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
                tmpMass.ToList().Select((q, i) => (tmpMass[i] = x[i] + y * dk[i])).ToArray();
                f1 = Fx.Func(tmpMass, R);
                tmpMass.ToList().Select((q, i) => (tmpMass[i] = x[i] + z * dk[i])).ToArray();
                f2 = Fx.Func(tmpMass, R);

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
                    y = y = z;
                    z = y + eps;
                    double tmp1, tmp2;
                    tmpMass.ToList().Select((q, i) => (tmpMass[i] = x[i] + y * dk[i])).ToArray();
                    tmp1 = Fx.Func(tmpMass, R);
                    tmpMass.ToList().Select((q, i) => (tmpMass[i] = x[i] + z * dk[i])).ToArray();
                    tmp2 = Fx.Func(tmpMass, R);
                    if (tmp1 <= tmp2)
                    {
                        b = z;
                    }
                    else
                    {
                        a = y;
                    }
                    break;
                }
            }
            double res = (a + b) / 2;
            return res;
        }

        /// <summary>
        /// Метод Ньютона - Раффсона
        /// Вычисление минимума многомерной функции
        /// </summary>
        /// <param name="x">Начальное приближение</param>
        /// <param name="isGold">Если true, то используется метод Золотого сечения, иначе метод Фибоначчи</param>
        /// <param name="r">Коэфициент штрафа(по умолчанию равен 0)</param>
        /// <param name="eps1">Точность градиента</param>
        /// <param name="eps2">Точность точки и значения относительно истинных</param>
        public void _Newton_Raffson(double[] x, bool isGold = true, double r = 0, double eps1 = 1e-7, double eps2 = 1e-7)
        {
            R = r;
            const int MAXITER = 10000;
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
                    grad[i] = getDerivate(x, i);
                }

                //4
                if (Math.Sqrt(grad.Sum(a => a * a)) <= eps1) break;

                //5
                if (k >= MAXITER) break;

                //6
                buildGesse(Gesse, x, LENGTH);

                //7
                Gesse = Gesse.Inverse();

                //8
                if (criterionSilvestr(Gesse) == true)
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
                if(isGold)
                    tk = goldenSection(x, dk, -100, 100, 1e-7);
                else
                tk = Fibonachi(x, dk, -100, 100, 1e-7, 1e-7);
                //11
                for (int i = 0; i < LENGTH; i++)
                {
                    x1[i] = x[i] + tk * dk[i];
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
