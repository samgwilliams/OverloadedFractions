namespace OverloadedFractions
{
	/// <summary>
	/// Fraction struct representing fractions of the form "Numerator/Denominator". Implements IEquatable, IComparable and includes overloaded operators (==, !=, ++, --, +, -, *, /) for ease of use.
	/// </summary>
	public readonly struct Fraction : IEquatable<Fraction>, IComparable<Fraction>
	{
		#region constructors

		/// <summary>
		/// Creates a Fraction from a whole number.
		/// </summary>
		/// <param name="numerator">Value of the numerator</param>
		public Fraction(long numerator)
		{
			(Numerator, Denominator, DecimalValue) = CheckValues(numerator, 1, false);
		}
		
		/// <summary>
		/// Creates a Fraction from a decimal number.
		/// </summary>
		/// <param name="decimalValue">The target decimal value of the Fraction to be created</param>
		/// <param name="desiredAccuracy">The accuracy with which the created Fraction's decimal value should match the target. If not provided, Fraction.DefaultAccuracy is used</param>
		/// <param name="simple">If true, equivalent to using the SimplestFromDecimal method, otherwise equivalent to using the ClosestFromDecimal method</param>
		public Fraction(double decimalValue, double? desiredAccuracy, bool simple = true)
		{
			(long numerator, long denominator) = simple ? SimplestFromDecimalInternal(decimalValue, desiredAccuracy) : ClosestFromDecimalInternal(decimalValue, desiredAccuracy);
			(Numerator, Denominator, DecimalValue) = CheckValues(numerator, denominator, false);
		}

		/// <summary>
		/// Creates a Fraction from given numerator and denominator values.
		/// </summary>
		/// <param name="numerator">Value of the numerator</param>
		/// <param name="denominator">Value of the denominator</param>
		/// <param name="simplify">Whether to automatically simplify the Fraction (i.e. if the numerator and denominator share a common factor)</param>
		public Fraction(long numerator, long denominator, bool simplify = true)
		{
			if (denominator == 0)
			{
				throw new ArgumentException("Denominator cannot be zero.", nameof(denominator));
			}

			(Numerator, Denominator, DecimalValue) = CheckValues(numerator, denominator, simplify);
		}

		private static (long numerator, long denominator, double decimalValue) CheckValues(long numerator, long denominator, bool simplify = true)
		{
			if (simplify)
			{
				long commonDenominator = GreatestCommonDenominator(numerator, denominator);
				numerator /= commonDenominator;
				denominator /= commonDenominator;
			}
			
			if (denominator < 0)
			{
				denominator = -denominator;
				numerator = -numerator;
			}
			
			var decimalValue = ((double)numerator / denominator);

			return (numerator, denominator, decimalValue);
		}

		#endregion constructors

		#region properties

		/// <summary>
		/// Creates a Fraction with a numerator of zero.
		/// </summary>
		public static Fraction Zero => new Fraction(0);

		/// <summary>
		/// Creates a Fraction with a decimal value of one.
		/// </summary>
		public static Fraction One => new Fraction(1);

		/// <summary>
		/// Default accuracy used in creating Fractions from decimal values when optional parameter is not provided.
		/// </summary>
		public static double DefaultAccuracy { get; } = 1E-3;

		/// <summary>
		/// Value of the Fraction's numerator.
		/// </summary>
		public long Numerator { get; }

		/// <summary>
		/// Value of the Fraction's denominator.
		/// </summary>
		public long Denominator { get; }

		/// <summary>
		/// Value of the Fraction expressed as a decimal.
		/// </summary>
		public double DecimalValue { get; }

		#endregion properties

		#region methods

		#region operators
		
		// equals
		public static bool operator ==(Fraction a, Fraction b)
		{
			return a.Equals(b);
		}

		public static bool operator ==(Fraction a, double b)
		{
			return a.DecimalValue == b;
		}

		// not equals
		public static bool operator !=(Fraction a, Fraction b)
		{
			return !(a == b);
		}

		public static bool operator !=(Fraction a, double b)
		{
			return a.DecimalValue != b;
		}

		// affirm
		public static Fraction operator +(Fraction a) => a;

		// negate
		public static Fraction operator -(Fraction a) => new Fraction(-a.Numerator, a.Denominator);

		// add
		public static Fraction operator +(Fraction a, Fraction b) => new Fraction((a.Numerator * b.Denominator) + (b.Numerator * a.Denominator), a.Denominator * b.Denominator);

		// subtract
		public static Fraction operator -(Fraction a, Fraction b) => a + (-b);

		// multiply
		public static Fraction operator *(Fraction a, Fraction b) => new Fraction(a.Numerator * b.Numerator, a.Denominator * b.Denominator);

		// divide
		public static Fraction operator /(Fraction a, Fraction b)
		{
			if (b.Numerator == 0)
			{
				throw new DivideByZeroException();
			}

			return new Fraction(a.Numerator * b.Denominator, a.Denominator * b.Numerator);
		}


		#endregion

		/// <summary>
		/// Find the lowest common (positive) multiple between two numbers.
		/// </summary>
		/// <param name="first">Value of the first number</param>
		/// <param name="second">Value of the second number</param>
		/// <returns>Lowest (positive) multiple of the two numbers</returns>
		public static long LowestCommonMultiple(long first, long second)
		{
			long firstNumber = Math.Abs(first);
			long secondNumber = Math.Abs(second);

			long largerNumber, smallerNumber;
			if (firstNumber > secondNumber)
			{
				largerNumber = firstNumber; smallerNumber = secondNumber;
			}
			else
			{
				largerNumber = secondNumber; smallerNumber = firstNumber;
			}

			for (int i = 1; i <= smallerNumber; i++)
			{
				long lcm = largerNumber * i;
				if ((lcm) % secondNumber == 0)
				{
					return lcm;
				}
			}

			return smallerNumber;
		}

		/// <summary>
		/// Find the greatest common (positive) denominator of two numbers
		/// </summary>
		/// <param name="first">Value of the first number</param>
		/// <param name="second">Value of the second number</param>
		/// <returns>Greatest common (positive) denominator of the two numbers</returns>
		public static long GreatestCommonDenominator(long first, long second)
		{
			long firstNumber = Math.Abs(first);
			long secondNumber = Math.Abs(second);

			while (secondNumber != 0)
			{
				long temp = firstNumber;
				firstNumber = secondNumber;
				secondNumber = temp % secondNumber;
			}

			return firstNumber;
		}

		/// <summary>
		/// Creates a Fraction from a string representation
		/// </summary>
		/// <param name="fractionString">String representing the fraction to create (e.g. 2 4/3, 2÷3)</param>
		/// <param name="simplify">Whether to automatically simplify the Fraction (i.e. if the numerator and denominator share a common factor)</param>
		/// <returns>Fraction parsed from the input string</returns>
		public static Fraction FromString(string fractionString, bool simplify = true)
		{
			var integerParts = fractionString.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
			if (integerParts.Count == 2 && long.TryParse(integerParts[0], out var integer))
			{
				var integerPart = new Fraction(integer);
				var fractionalPart = FromStringInternal(integerParts[1]);
				return (integerPart.DecimalValue < 0) ? integerPart - fractionalPart : integerPart + fractionalPart;
			}

			return FromStringInternal(fractionString, simplify);
		}

		/// <summary>
		/// Creates a Fraction with the desired accuracy from a given decimal value.
		/// This function uses an implementation (https://bit.ly/2Q2ZfYT) of the Richards' method which tends to produce more "complex" fractions that are closer to the desired decimal value.
		/// </summary>
		/// <param name="decimalValue">The target decimal value of the Fraction to be created</param>
		/// <param name="desiredAccuracy">The accuracy with which the created Fraction's decimal value should match the target. If not provided, Fraction.DefaultAccuracy is used</param>
		/// <returns>Fraction whose decimal value should match the input within the desired accuracy</returns>
		public static Fraction ClosestFromDecimal(double decimalValue, double? desiredAccuracy = null)
		{
			(long numerator, long denominator) = ClosestFromDecimalInternal(decimalValue, desiredAccuracy);
			return new Fraction(numerator, denominator, false);
		}
		
		private static (long numerator, long denominator) ClosestFromDecimalInternal(double decimalValue, double? desiredAccuracy = null)
		{
			if (double.IsNaN(decimalValue) || double.IsInfinity(decimalValue))
			{
				throw new ArgumentOutOfRangeException(nameof(decimalValue), "Value must be well-defined.");
			}

			var accuracy = desiredAccuracy ?? DefaultAccuracy;
			if (accuracy <= 0.0 || accuracy >= 1.0)
			{
				throw new ArgumentOutOfRangeException(nameof(desiredAccuracy), "Value must be between 0 and 1.");
			}

			int sign = Math.Sign(decimalValue);

			if (sign == -1)
			{
				decimalValue = Math.Abs(decimalValue);
			}

			// accuracy is the maximum relative error; convert to absolute maxError
			double maxError = sign == 0 ? accuracy : decimalValue * accuracy;

			int n = (int)Math.Floor(decimalValue);
			decimalValue -= n;

			if (decimalValue < maxError)
			{
				return new (sign * n, 1);
			}

			if (1 - maxError < decimalValue)
			{
				return new (sign * (n + 1), 1);
			}

			double z = decimalValue;
			int previousDenominator = 0;
			int denominator = 1;
			int numeratorPart;

			do
			{
				z = 1.0 / (z - (int)z);
				int temp = denominator;
				denominator = denominator * (int)z + previousDenominator;
				previousDenominator = temp;
				numeratorPart = Convert.ToInt32(decimalValue * denominator);
			}
			while (Math.Abs(decimalValue - (double)numeratorPart / denominator) > maxError && z != (int)z);

			long numerator = (n * denominator + numeratorPart) * sign;

			return new (numerator, denominator);
		}

		/// <summary>
		/// Creates a Fraction with the desired accuracy from a given decimal value.
		/// This function uses a Farey pair/continued fraction implementation (https://bit.ly/2WN8A8p) similar to Richards' algorithm that tends to produce more "simple" fractions that consist of lower numbers.
		/// </summary>
		/// <param name="decimalValue">The target decimal value of the Fraction to be created</param>
		/// <param name="desiredAccuracy">The accuracy with which the created Fraction's decimal value should match the target. If not provided, Fraction.DefaultAccuracy is used</param>
		/// <returns>Fraction whose decimal value should match the target within the desired accuracy</returns>
		public static Fraction SimplestFromDecimal(double decimalValue, double? desiredAccuracy = null)
		{
			(long numerator, long denominator) = SimplestFromDecimalInternal(decimalValue, desiredAccuracy);
			return new Fraction(numerator, denominator);
		}
		
		private static (long numerator, long denominator) SimplestFromDecimalInternal(double decimalValue, double? desiredAccuracy = null)
		{
			if (double.IsNaN(decimalValue) || double.IsInfinity(decimalValue))
			{
				throw new ArgumentOutOfRangeException(nameof(decimalValue), "Value must be well-defined.");
			}

			var accuracy = desiredAccuracy ?? DefaultAccuracy;
			if (accuracy <= 0.0 || accuracy >= 1.0)
			{
				throw new ArgumentOutOfRangeException(nameof(desiredAccuracy), "Value must be between 0 and 1.");
			}

			int sign = decimalValue < 0 ? -1 : 1;
			decimalValue = decimalValue < 0 ? -decimalValue : decimalValue;
			int integerpart = (int)decimalValue;
			decimalValue -= integerpart;
			double minimalvalue = decimalValue - accuracy;

			if (minimalvalue < 0.0) return (sign * integerpart, 1);
			double maximumvalue = decimalValue + accuracy;
			if (maximumvalue > 1.0) return (sign * (integerpart + 1), 1);

			int b = 1;
			int d = (int)(1 / maximumvalue);
			double left_n = minimalvalue;
			double left_d = 1.0 - d * minimalvalue;
			double right_n = 1.0 - d * maximumvalue;
			double right_d = maximumvalue;

			while (true)
			{
				if (left_n < left_d) break;
				int n = (int)(left_n / left_d);
				b += n * d;
				left_n -= n * left_d;
				right_d -= n * right_n;
				if (right_n < right_d) break;
				n = (int)(right_n / right_d);
				d += n * b;
				left_d -= n * left_n;
				right_n -= n * right_d;
			}

			int denominator = b + d;
			int numeratorPart = (int)(decimalValue * denominator + 0.5);
			long numerator = sign * (integerpart * denominator + numeratorPart);

			return (numerator, denominator);
		}

		public bool Equals(Fraction other)
		{
			return DecimalValue.Equals(other.DecimalValue);
		}

		public override bool Equals(object other)
		{
			if (other is Fraction otherFraction) return Equals(otherFraction);
			if (other is double otherDecimalValue) return otherDecimalValue == DecimalValue;
			return false;
		}
		
		public override int GetHashCode()
		{
			return DecimalValue.GetHashCode();
		}

		/// <summary>
		/// Adds another Fraction's value onto this object and simplifies the result.
		/// </summary>
		/// <param name="otherFraction">Fraction to add</param>
		/// <returns>The addition result as a new simplified Fraction</returns>
		public Fraction Add(Fraction otherFraction)
		{
			return this + otherFraction;
		}

		/// <summary>
		/// Subtracts another Fraction's value from this object and simplifies the result.
		/// </summary>
		/// <param name="otherFraction">Fraction to subtract</param>
		/// <returns>The subtraction result as a new simplified Fraction</returns>
		public Fraction Subtract(Fraction otherFraction)
		{
			return this - otherFraction;
		}

		/// <summary>
		/// Multiplies another Fraction's value with this object and simplifies the result.
		/// </summary>
		/// <param name="otherFraction">Fraction to multiply</param>
		/// <returns>The multiplication result as a new simplified Fraction</returns>
		public Fraction Multiply(Fraction otherFraction)
		{
			return this * otherFraction;
		}

		/// <summary>
		/// Divides another Fraction's value with this object and simplifies the result.
		/// </summary>
		/// <param name="otherFraction">Fraction to divide</param>
		/// <returns>The division result as a new simplified Fraction</returns>
		public Fraction Divide(Fraction otherFraction)
		{
			return this / otherFraction;
		}

		/// <summary>
		/// Increments the Fraction's value by one.
		/// </summary>
		/// <returns>The incremented result as a new simplified Fraction</returns>
		public Fraction Incremented()
		{
			return new Fraction(Numerator + Denominator, Denominator);
		}

		/// <summary>
		/// Decrements this Fraction's value by one.
		/// </summary>
		/// <returns>The decremented result as a new simplified Fraction</returns>
		public Fraction Decremented()
		{
			return new Fraction(Numerator - Denominator, Denominator);
		}

		/// <summary>
		/// Negates this Fraction's value (i.e. equivalent to multiplication by -1)
		/// </summary>
		/// <returns>The negated result as a new simplified Fraction</returns>
		public Fraction Negated()
		{
			return new Fraction(Numerator * -1, Denominator, false);
		}

		public override string ToString()
		{
			return (Numerator + "/" + Denominator);
		}

		public int CompareTo(Fraction other)
		{
			return DecimalValue.CompareTo(other.DecimalValue);
		}

		private static Fraction FromStringInternal(string fractionString, bool simplify = true)
		{
			var parts = fractionString.Split(new[] { '/', '÷' }, StringSplitOptions.RemoveEmptyEntries).ToList();
			if (parts.Count == 2 && long.TryParse(parts[0], out var numerator) && long.TryParse(parts[1], out var denominator))
			{
				return new Fraction(numerator, denominator, simplify);
			}

			throw new ArgumentException($"Could not parse Fraction from input: {fractionString}");
		}

		#endregion methods
	}
}