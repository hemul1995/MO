﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethodsOptimisation
{
    class Gauss
    {

        private double getDerivate1(double[] x, int index)
        {
            double eps = 1e-10;
            double h = 1, p1, p2, p;
            double tmp = x[index];
            double f1, f2;

            do
            {

                x[index] = tmp + h;
                f1 = Fx(x);
                x[index] = tmp - h;
                f2 = Fx(x);


                p1 = (f1 - f2) / (2 * h);
                h /= 2;

                x[index] = tmp + h;
                f1 = Fx(x);
                x[index] = tmp - h;
                f2 = Fx(x);
                x[index] = tmp;

                p2 = (f1 - f2) / (2 * h);
            }
            while (p1 != p2 && h * h > eps * Math.Abs(x[index]));
            p = p2;
            x[index] = tmp;
            return p;
        }

        private double getDerivate2(double[] x, int index)
        {
            double eps = 1e-10;
            double h = 1, p1, p2, p;
            double tmp = x[index];
            double f1, f2, f3;
            do
            {
                
                
                x[index] = tmp + h;
                f1 = Fx(x);
                x[index] = tmp - h;
                f2 = Fx(x);
                x[index] = tmp ;
                f3 = Fx(x);

                p1 = (f1 + f2 - 2 * f3) / (h * h);
                h /= 2;



                x[index] = tmp + h;
                f1 = Fx(x);
                x[index] = tmp - h;
                f2 = Fx(x);
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

            for (derivate2 = getDerivate2(x0, index); derivate2 <= 0 && x0[index] < 100; derivate2 = getDerivate2(x0, index))//циклится
            {
                x0[index] += 1;
            }
            double x1 = x0[index];
            derivate1 = getDerivate1(x0, index);
            double x2 = x1 - derivate1 / derivate2;
            int n = 0;
            
            x0[index] = x1;
            double f1 = Fx(x0);
            x0[index] = x2;
            double f2 = Fx(x0);
            x0[index] = tmp;
            while (Math.Abs(x2 - x1) > dx && (f2 - f1) != 0 && n < 100000)
            {
                n++;
                x1 = x2;
                x0[index] = x1;
                derivate1 = getDerivate1(x0, index);
                derivate2 = getDerivate2(x0, index);
                x2 = x1 - derivate1 / derivate2;
            }
            x0[index] = tmp;
            //minArg tmp = new minArg();
            //tmp.x = x2;
            //tmp.f = Fx(x2);
            return x2;
            //if(derivate2 <=0)

        }


        double Fx(double[] x)
        {
            double y = 0;
            y = (1 - x[0]) * (1 - x[0]) + 100 * (x[1] - x[0] * x[0]) * (x[1] - x[0] * x[0]);
            return y + 1;
        }
        public double _Gauss(double[] x0)
        {
            int K = 0;
            double[] x1 = new double[x0.Length];
            for(int i = 0; i<x0.Length; i++)
            {
                x1[i] = x0[i];
            }
            do
            {
                K++;
                for (int i = 0; i < x0.Length; i++)
                    x1[i] = getMinParabol(x1, 1e-10, i);
                double norm = 0;
                for(int i = 0; i<x0.Length; i++)
                {
                    norm += (x1[i] - x0[i]) * (x1[i] - x0[i]);
                }
                if (Math.Abs(Fx(x1) - Fx(x0)) <= 1e-10 && norm <= 1e-10)
                {
                    break;
                }
                else
                {
                    for (int i = 0; i < x0.Length; i++)
                    {
                        x0[i] = x1[i];
                    }
                }
            }
            while(K < 100000);

            return Fx(x1);
        }
    }
}