using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fractions
{
    class Program
    {
        static void Main(string[] args)
        {
            FractionTest fractionTest = new FractionTest();
            Console.WriteLine("NORMALIZATION TEST PASS?: " + fractionTest.TestNormalization());
            Console.WriteLine("TODOUBLE TEST PASS?: " + fractionTest.TestToDouble());
            Console.WriteLine("ADD TEST PASS?: " + fractionTest.TestAdd());
            Console.WriteLine("SUBTRACT TEST PASS?: " + fractionTest.TestSubtract());
            Console.WriteLine("MULTIPLY TEST PASS?: " + fractionTest.TestMultiply());
            Console.WriteLine("DIVIDE TEST PASS?: " + fractionTest.TestDivide());
            Console.WriteLine("INCREMENT TEST PASS?: " + fractionTest.TestIncrement());
            Console.WriteLine("NEGATE TEST PASS?: " + fractionTest.TestNegate());
            Console.WriteLine("COMPARE TEST PASS?: " + fractionTest.TestCompare());


            Console.ReadLine();
        }

        
    }

    public class FractionTest
    {
        public bool TestNormalization()
        {
            Fraction test1 = new Fraction(15, 20);
            if (test1.GetNumerator() == 3 && test1.GetDenominator() == 4)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool TestToDouble()
        {
            Fraction test1 = new Fraction(15, 20);
            return (test1.ToDouble() == .75);
        }

        public bool TestAdd()
        {
            Fraction test1 = new Fraction(15, 20);
            Fraction test2 = new Fraction(5, 20);
            Fraction test3 = new Fraction(1, 1);
            test1 = test1.Add(test2);
            if (test1.Equals(test3))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool TestSubtract()
        {
            Fraction test1 = new Fraction(15, 20);
            Fraction test2 = new Fraction(5, 20);
            Fraction test3 = new Fraction(1, 2);
            test1 = test1.Subtract(test2);
            if (test1.Equals(test3))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool TestMultiply()
        {
            Fraction test1 = new Fraction(1, 2);
            Fraction test2 = new Fraction(1, 2);
            Fraction test3 = new Fraction(1, 4);
            test1 = test1.Multiply(test2);
            if (test1.Equals(test3))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool TestDivide()
        {
            Fraction test1 = new Fraction(1, 2);
            Fraction test2 = new Fraction(1, 4);
            Fraction test3 = new Fraction(2, 1);
            test1 = test1.Divide(test2);
            if (test1.Equals(test3))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool TestIncrement()
        {
            Fraction test1 = new Fraction(1, 2);
            Fraction test2 = new Fraction(3, 2);
            test1 = test1.Increment();
            if (test1.Equals(test2))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool TestNegate()
        {
            Fraction test1 = new Fraction(1, 2);
            Fraction test2 = new Fraction(-1, 2);
            test1 = test1.Negate();
            if (test1.Equals(test2))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool TestCompare()
        {
            Fraction test1 = new Fraction(1, 1);
            Fraction test2 = new Fraction(-1, 2);
            Fraction test3 = new Fraction(2, 1);
            Fraction test4 = new Fraction(1, 1);

            if (test1.Compare(test2) == -1 && test1.Compare(test3) == 1 && test1.Compare(test4) == 0)
            {
                return true;
            }
            else
            {
                return false;
            }


        }



    }
}
