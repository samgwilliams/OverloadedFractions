using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fractions
{
    class Fraction
    {
        private long numerator;
        private long denominator;

        private static Fraction ZERO = new Fraction(0);
        private static Fraction ONE = new Fraction(1);

        public Fraction(long numerator){
            this.denominator = 1;
            this.numerator = numerator;
        }

        public Fraction(long numerator, long denominator)
        {
            
            long gcd = GetGCD(numerator, denominator);
            this.numerator = numerator / gcd;
            this.denominator = denominator / gcd;
        }

        public long GetNumerator()
        {
            return this.numerator;
        }

        public long GetDenominator()
        {
            return this.denominator;
        }

        public double ToDouble()
        {
            return numerator / denominator;
        }

        public Fraction Add(Fraction otherFraction)
        {
            if(this.denominator != otherFraction.GetDenominator()){
                long lcm = GetLCM(this.denominator, otherFraction.denominator);
                return new Fraction(((this.numerator * lcm) + (otherFraction.numerator * lcm)), 
                    ((this.denominator * lcm) + (otherFraction.denominator * lcm)));
            }
            else
            {
                return new Fraction((this.numerator + otherFraction.numerator), this.denominator);
            }
            
        }

        public static long GetLCM(long num1, long num2)
        {
            long numOne, numTwo;
            if (num1 > num2)
            {
                numOne = num1; numTwo = num2;
            }
            else
            {
                numOne = num2; numTwo = num1;
            }

            for (int i = 1; i <= numTwo; i++)
            {
                long lcm = numOne * i;
                if ((lcm) % num2 == 0)
                {
                    return lcm;
                }
            }
            return numTwo;
        }

        public static long GetGCD(long num1, long num2)
        {
            long numOne = Math.Abs(num1);
            long numTwo = Math.Abs(num2);
            //Console.WriteLine("Getting GCD...");

            while (numTwo != 0)
            {
                long temp = numOne;
                //Console.WriteLine("Temp: " + temp);
                numOne = numTwo;
                //Console.WriteLine("numOne: " + numOne);
                numTwo = temp % numTwo;
                //Console.WriteLine("numTwo: " + numTwo);
            }
            //Console.WriteLine("Return numOne: " + numOne);
            return numOne;
        }
    }
}
