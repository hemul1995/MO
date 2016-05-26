using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethodsOptimisation
{
    class Fletcher_Rivz
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
            f1 = Fx.Func(x);
            x[index] += 2 * h;
            f2 = Fx.Func(x);
            p1 = (f1 - f2) / (2 * h);
            x[index] = tmp;

            p = p1;
            return p;
        }
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
        
        double R;
        /// <summary>
        /// Метод Флетчера - Ривза
        /// Вычисление минимума многомерной функции
        /// </summary>
        /// <param name="x">Начальное приближение</param>
        /// <param name="isBeta">Если true, то используется для квадрат. функций, иначе для всех</param>
        /// <param name="isGold">Если true, то используется метод Золотого сечения, иначе метод Фибоначчи</param>
        /// <param name="r">Коэфициент штрафа(по умолчанию равен 0)</param>
        /// <param name="eps1">Точность градиента</param>
        /// <param name="eps2">Точность точки и значения относительно истинных</param>
        public void _Fletcher_Rivz(double[] x, bool isBeta, bool isGold, double r = 0, double eps1 = 1e-7, double eps2 = 1e-7)
        {
            R = r;
            const int MAXITER = 10000;
            int LENGTH = x.Length;
            double tk;
            double[] dk = new double[LENGTH];
            double[] grad = new double[LENGTH];
            double[] grad0 = new double[LENGTH];
            double[] x1 = new double[LENGTH];
            double[] x0 = new double[LENGTH];
            double beta;
            int k = 0;
            while (true)
            {
                //3
                for(int i = 0; i < LENGTH; i++)
                {
                    grad[i] = getDerivate(x, i);
                }

                //4
                if (Math.Sqrt(grad.Sum(a => a * a)) <= eps1)
                {
                    break;
                }

                //5
                if (k >= MAXITER) break;

                //6
                if (k == 0)
                {
                    for (int i = 0; i < LENGTH; i++)
                    {
                        dk[i] = -grad[i];
                    }
                }
                else
                {
                    beta = 0;
                    if(isBeta)
                        beta = grad.Sum(a => a * a) / grad0.Sum(b => b * b);
                    else
                        if ((k + 1) % 2 != 0)
                        {
                            for (int i = 0; i < LENGTH; i++)
                            {
                                beta += grad[i] * (grad[i] - grad0[i]);
                            }
                            beta /= grad0.Sum(b => b * b);
                        }

                    //8
                    for(int i = 0; i < LENGTH; i++)
                    {
                        dk[i] = -grad[i] + beta * dk[i];
                    }
                }

                //9
                if (isGold)
                    tk = goldenSection(x, dk, -100, 100, 1e-7);
                else
                    tk = Fibonachi(x, dk, -100, 100, 1e-7, 1e-7);

                //10
                for (int i = 0; i < LENGTH; i++)
                {
                    x1[i] = x[i] + tk * dk[i];
                }

                //11
                double norm = 0;
                for (int i = 0; i < LENGTH; i++)
                {
                    norm += (x1[i] - x[i]) * (x1[i] - x[i]);
                }
                if (Math.Abs(Fx.Func(x1, R) - Fx.Func(x, R)) < eps2 && Math.Sqrt(norm) < eps2 && k > 1) break;
                else
                {
                    for (int i = 0; i < LENGTH; i++)
                    {
                        x0[i] = x[i];
                        grad0[i] = grad[i];
                    }
                    for (int i = 0; i < LENGTH; i++)
                    {
                        x[i] = x1[i];
                    }
                    k++;
                }
            }
            Fx.x = x;
            Fx.f = Fx.Func(x);
        }
    }
}
