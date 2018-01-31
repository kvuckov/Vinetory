using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    static class Kalkulator
    {
        public static double Izracun(double a, double b)
        {
            return a * b;
        }
        public static double IzracunCokotaPoHa(double a, double b)
        {
            return (a / b);
        }
        public static double IzracunPupovaPoCokotu(float a, float b, float c)
        {
            double v = b * c;
            return (a / v);
        }
        public static double IzracunPupovaPoCokotu10(float a)
        {
           double d = 10;
           double w = 100;
           double v = d/w;          
           return (v * a);
        }
        public static double ProsjecnoTrajanjeZastite(float x,int y)
        {
            return x / y;
        }
        public static float ProsjecnaBerba(float x,int y)
        {
            float a = x / y;
            return  a/1000;
        }
}

