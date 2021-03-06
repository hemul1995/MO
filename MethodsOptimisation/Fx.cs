﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethodsOptimisation
{
    static class Fx
    {
        public static double f;
        public static double[] x;
        public static int iter;
        public static double P = 0;
        /// <summary>
        /// Вычисление значения функции
        /// </summary>
        /// <param name="arg">Точка</param>
        /// <param name="r">Коэфициент штрафа(по умолчанию равен 0)</param>
        /// <returns></returns>
        public static double Func(double[] arg, double r = 0)
        {
            P = 0;
            double func;
            iter++;

            /**
             * Global-min function
             * 
             * 
             * 
             * 
            **/
            func = 20 + arg[0] * arg[0] + arg[1] * arg[1] - 10 * (Math.Cos(2 * Math.PI * arg[0]) + Math.Cos(2 * Math.PI * arg[0]));

            /**
             * Himmelblau function
             * (3, 2) -> 0
             * (-3,779310 -3,283186) -> 0
             * (-2,805118 3,131312) -> 0
             * (3,584428 -1,848126) -> 0
            **/
            //func = (arg[0] * arg[0] + arg[1] - 11) * (arg[0] * arg[0] + arg[1] - 11) + (arg[0] + arg[1] * arg[1] - 7) * (arg[0] + arg[1] * arg[1] - 7);

            /**
             * Quadratish function
             * (1, 1) -> 0
            **/
            //func = 5 * (arg[0] - arg[1]) * (arg[0] - arg[1]) + (1 - arg[0]) * (1 - arg[0]);

            /**
             * Rosenbrock function
             * (1, 1) -> 0
            **/
            //func = (1 - arg[0]) * (1 - arg[0]) + 100 * (arg[1] - arg[0] * arg[0]) * (arg[1] - arg[0] * arg[0]);

            /**
             * Booth's function
             * (1, 3) -> 0
            **/
            //func = (arg[0] + 2 * arg[1] - 7) * (arg[0] + 2 * arg[1] - 7) + (2 * arg[0] + arg[1] - 5) * (2 * arg[0] + arg[1] - 5);

            /**
             * Three-hump camel function:
             * (0, 0) -> 0
            **/
            //func = 2 * arg[0] * arg[0] - 1.05 * Math.Pow(arg[0], 4) + Math.Pow(arg[0], 6) / 6 + arg[0] * arg[1] + arg[1] * arg[1];

            /**
             * Beale's function
             * (3, 0.5) -> 0
            **/
            //func = (1.5 - arg[0] + arg[0] * arg[1]) * (1.5 - arg[0] + arg[0] * arg[1]) + (2.25 - arg[0] + arg[0] * arg[1] * arg[1]) * (2.25 - arg[0] + arg[0] * arg[1] * arg[1]) + (2.625 - arg[0] + arg[0] * arg[1] * arg[1] * arg[1]) * (2.625 - arg[0] + arg[0] * arg[1] * arg[1] * arg[1]);

            //double x = arg[0], y = arg[1], z = arg[2], u = arg[3], v = arg[4];
            //double p, s, t, q;
            //p = v * z / (1 - z) + u * (1 - v) / (1 - u);
            //s = -x * y * v / (1 - z) - (1 - v) / (1 - u);
            //t = x * v / (1 - z * z) + (1 - v) / (1 - u * u);
            //q = x * z * v / (1 - z * z) + u * (1 - v) / (1 - u * u);
            //func = 2 * p * s / (t + q) + v * y * (1 + z) / (1 - z) + (1 - v) * (1 + u) / (1 - u);//-0.25
            //func = -s / (t + q);



            //Штрафные ограничения вида P(x, r) <- r/2 * [Σg(x) + ΣMax(0, _g(x))];
            //(-0.5 -0.5 ) -> 0.5
   //         if (r != 0)

            //(1.2 1.44 ) -> 0.04
 //               P = r / 2 * (Math.Pow(Math.Max(0, 2 - arg[0]), 2) + Math.Pow(Math.Max(0, 0.1 - arg[1]), 2) + Math.Pow(Math.Max(0, 0.1 - arg[2]), 2) + Math.Pow(Math.Max(0, 0.1 - arg[3]), 2) + Math.Pow(Math.Max(0, 0 - arg[4]), 2) + Math.Pow(Math.Max(0, arg[0] - 100), 2) + Math.Pow(Math.Max(0, arg[1] - 2), 2) + Math.Pow(Math.Max(0, arg[2] - 0.45), 2) + Math.Pow(Math.Max(0, arg[3] - 0.45), 2) + Math.Pow(Math.Max(0, arg[4] - 1), 2));
                //P = r / 2 * ((Math.Max(0, 1.2 - arg[0]) * Math.Max(0, 1.2 - arg[0])) + (Math.Max(0, 1.2 - arg[1]) * Math.Max(0, 1.2 - arg[1])) + (Math.Max(0, arg[0] - 2) * Math.Max(0, arg[0] - 2)) + (Math.Max(0, arg[1] - 2) * Math.Max(0, arg[1] - 2)));
            //(0.9 0.81 ) -> 0.001    
                //P = r / 2 * ((Math.Max(0, -arg[0] - 1) * Math.Max(0, -arg[0] -1)) + (Math.Max(0, -arg[1] -1) * Math.Max(0, -arg[1] - 1)) + (Math.Max(0, arg[0] - 0.9) * Math.Max(0, arg[0] - 0.9)) + (Math.Max(0, arg[1] - 0.9) * Math.Max(0, arg[1] - 0.9)));
                //P = r / 2 * ((Math.Max(0, -arg[0]) * Math.Max(0, -arg[0])) + (Math.Max(0, -arg[1]) * Math.Max(0, -arg[1])) + (Math.Max(0, arg[0] - 0.1) * Math.Max(0, arg[0] - 0.1)) + (Math.Max(0, arg[1] - 0.1) * Math.Max(0, arg[1] - 0.1)));
                //P = r / 2 * (Math.Max(0, arg[0] + arg[1] + 1) * Math.Max(0, arg[0] + arg[1] + 1));

            //Барьерные ограничения вида P(x, r) <- -r * Σ1/_g(x);
            //(-0.5 -0.5 ) -> 0.5
            //if(r != 0)
            //P = -r * (1 / (1.2 - arg[0]) + 1 / (1.2 - arg[1]) + 1 / (arg[0] - 2) + 1 / (arg[1] - 2));

            //Барьерные ограничения вида P(x, r) <- -r * Σln(-_g(x));
            //(-0.5 -0.5 ) -> 0.5
            //if (r != 0)
            //P = -r * Math.Log(arg[0] + arg[1] + 1);
            func += P;

            
            if (func == Double.PositiveInfinity) func = Double.MaxValue;
            if (func == Double.NegativeInfinity) func = -Double.MaxValue;
            return func;
        }

    }
}
