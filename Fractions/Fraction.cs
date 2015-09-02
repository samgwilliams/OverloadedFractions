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
            double fractionDouble = ((double)this.numerator / (double)this.denominator);
            return fractionDouble;
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

        public Fraction Subtract(Fraction otherFraction)
        {
            if (this.denominator != otherFraction.GetDenominator())
            {
                long lcm = GetLCM(this.denominator, otherFraction.denominator);
                return new Fraction(((this.numerator * lcm) - (otherFraction.numerator * lcm)),
                    ((this.denominator * lcm) + (otherFraction.denominator * lcm)));
            }
            else
            {
                return new Fraction((this.numerator - otherFraction.numerator), this.denominator);
            }

        }

        public Fraction Multiply(Fraction otherFraction)
        {
            return new Fraction((this.numerator * otherFraction.numerator), (this.denominator * otherFraction.denominator));
        }

        public Fraction Divide(Fraction otherFraction)
        {
            return new Fraction((this.numerator * otherFraction.denominator), (this.denominator * otherFraction.numerator));
        }

        public Fraction Increment()
        {
            return new Fraction((this.numerator + this.denominator), this.denominator);
        }

        public Fraction Negate()
        {
            return new Fraction((this.numerator * -1), this.denominator);
        }

        public int Compare(Fraction otherFraction)
        {
            if (otherFraction.ToDouble() > this.ToDouble())
            {
            

                return 1;
            }
            else if (otherFraction.ToDouble() < this.ToDouble())
            {

                return -1;
            }
            else
            {

                return 0;
            }
        }

        public bool Equals(Fraction otherFraction)
        {
            return (this.ToDouble() == otherFraction.ToDouble());
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

        public override string ToString()
        {
            return (this.numerator + "/" + this.denominator);
        }
    }
}
