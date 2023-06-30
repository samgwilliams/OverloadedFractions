namespace OverloadedFractions.Tests
{
	public class Tests
	{
		[Fact]
		public void Fraction_cannot_be_instantiated_with_zero_denominator()
		{
			Should.Throw<ArgumentException>(() => { var fraction = new Fraction(1, 0); });
		}

		[Theory]
		[InlineData(15, 20, 3, 4)]
		[InlineData(-10, 20, -1, 2)]
		public void Fraction_simplified_by_default(long numerator, long denominator, long expectedNumerator, long expectedDenominator)
		{
			var fraction = new Fraction(numerator, denominator);
			fraction.Numerator.ShouldBe(expectedNumerator);
			fraction.Denominator.ShouldBe(expectedDenominator);
		}

		[Theory]
		[InlineData(15, 20)]
		[InlineData(-10, 20)]
		public void Fraction_not_simplified_if_specified(long numerator, long denominator)
		{
			var fraction = new Fraction(numerator, denominator, false);
			fraction.Numerator.ShouldBe(numerator);
			fraction.Denominator.ShouldBe(denominator);
		}

		[Theory]
		[InlineData(15, 20, 0.75)]
		[InlineData(-10, 20, -0.5)]
		[InlineData(1, 3, 0.33333)]
		public void Fraction_DecimalValue_as_expected(long numerator, long denominator, double expectedDecimal)
		{
			const double withinTolerance = 1E-5;

			var asDecimal = new Fraction(numerator, denominator).DecimalValue;
			asDecimal.ShouldBe(expectedDecimal, withinTolerance);
		}

		[Theory]
		[InlineData(15, 20, 5, 20, 1, 1)]
		[InlineData(15, 20, -5, 20, 1, 2)]
		[InlineData(5, 20, -15, 20, -1, 2)]
		public void Fraction_Add_as_expected(long firstNumerator, long firstDenominator, long secondNumerator, long secondDenominator, long expectedNumerator, long expectedDenominator)
		{
			var first = new Fraction(firstNumerator, firstDenominator);
			var second = new Fraction(secondNumerator, secondDenominator);
			var result = first.Add(second);
			result.Numerator.ShouldBe(expectedNumerator);
			result.Denominator.ShouldBe(expectedDenominator);

			var operatorResult = first + second;
			operatorResult.Numerator.ShouldBe(expectedNumerator);
			operatorResult.Denominator.ShouldBe(expectedDenominator);
		}

		[Theory]
		[InlineData(15, 20, 5, 20, 1, 2)]
		[InlineData(-5, 20, -15, 20, 1, 2)]
		[InlineData(5, 20, 15, 20, -1, 2)]
		public void Fraction_Subtract_as_expected(long firstNumerator, long firstDenominator, long secondNumerator, long secondDenominator, long expectedNumerator, long expectedDenominator)
		{
			var first = new Fraction(firstNumerator, firstDenominator);
			var second = new Fraction(secondNumerator, secondDenominator);
			var result = first.Subtract(second);
			result.Numerator.ShouldBe(expectedNumerator);
			result.Denominator.ShouldBe(expectedDenominator);

			var operatorResult = first - second;
			operatorResult.Numerator.ShouldBe(expectedNumerator);
			operatorResult.Denominator.ShouldBe(expectedDenominator);
		}

		[Theory]
		[InlineData(10, 20, 1, 2, 1, 4)]
		[InlineData(1, 3, -2, 3, -2, 9)]
		public void Fraction_Multiply_as_expected(long firstNumerator, long firstDenominator, long secondNumerator, long secondDenominator, long expectedNumerator, long expectedDenominator)
		{
			var first = new Fraction(firstNumerator, firstDenominator);
			var second = new Fraction(secondNumerator, secondDenominator);
			var result = first.Multiply(second);
			result.Numerator.ShouldBe(expectedNumerator);
			result.Denominator.ShouldBe(expectedDenominator);

			var operatorResult = first * second;
			operatorResult.Numerator.ShouldBe(expectedNumerator);
			operatorResult.Denominator.ShouldBe(expectedDenominator);
		}

		[Theory]
		[InlineData(1, 2, 1, 4, 2, 1)]
		[InlineData(3, 2, -1, 1, -3, 2)]
		public void Fraction_Divide_as_expected(long firstNumerator, long firstDenominator, long secondNumerator, long secondDenominator, long expectedNumerator, long expectedDenominator)
		{
			var first = new Fraction(firstNumerator, firstDenominator);
			var second = new Fraction(secondNumerator, secondDenominator);
			var result = first.Divide(second);
			result.Numerator.ShouldBe(expectedNumerator);
			result.Denominator.ShouldBe(expectedDenominator);

			var operatorResult = first / second;
			operatorResult.Numerator.ShouldBe(expectedNumerator);
			operatorResult.Denominator.ShouldBe(expectedDenominator);
		}

		[Fact]
		public void Fraction_cannot_be_divided_by_zero()
		{
			Should.Throw<DivideByZeroException>(() => 
			{ 
				var fraction = new Fraction(1, 1);
				fraction.Divide(new Fraction(0, 1));
			});

			Should.Throw<DivideByZeroException>(() => 
			{ 
				var fraction = new Fraction(1, 1);
				var result = fraction / Fraction.Zero;
			});
		}

		[Theory]
		[InlineData(1, 2, 3, 2)]
		[InlineData(-1, 2, 1, 2)]
		[InlineData(-1, 1, 0, 1)]
		public void Fraction_Increment_as_expected(long numerator, long denominator, long expectedNumerator, long expectedDenominator)
		{
			var fraction = new Fraction(numerator, denominator);
			var incremented = fraction.Incremented();
			incremented.Numerator.ShouldBe(expectedNumerator);
			incremented.Denominator.ShouldBe(expectedDenominator);
		}
		
		[Theory]
		[InlineData(1, 2)]
		[InlineData(-1, 3)]
		public void Fraction_Increment_equivalent_to_adding_one_as_fraction(long numerator, long denominator)
		{
			var fraction = new Fraction(numerator, denominator);
			var addResult = fraction + Fraction.One;
			var incrementResult = fraction.Incremented();

			incrementResult.Equals(addResult).ShouldBeTrue();
		}
		
		[Theory]
		[InlineData(3, 2, 1, 2)]
		[InlineData(1, 2, -1, 2)]
		[InlineData(1, 1, 0, 1)]
		public void Fraction_Decrement_as_expected(long numerator, long denominator, long expectedNumerator, long expectedDenominator)
		{
			var fraction = new Fraction(numerator, denominator);
			var decremented = fraction.Decremented();
			decremented.Numerator.ShouldBe(expectedNumerator);
			decremented.Denominator.ShouldBe(expectedDenominator);
		}

		[Theory]
		[InlineData(1, 2)]
		[InlineData(-1, 3)]
		public void Fraction_Decrement_equivalent_to_subtracting_one_as_fraction(long numerator, long denominator)
		{
			var fraction = new Fraction(numerator, denominator);
			var addResult = fraction + Fraction.One;
			var incrementResult = fraction.Incremented();

			incrementResult.Equals(addResult).ShouldBeTrue();
		}

		[Theory]
		[InlineData(1, 2)]
		[InlineData(-1, 3)]
		public void Fraction_Zero_multiplication_results_in_zero(long numerator, long denominator)
		{
			var fraction = new Fraction(numerator, denominator);
			var multiplicationResult = fraction * Fraction.Zero;

			multiplicationResult.Equals(Fraction.Zero).ShouldBeTrue();
		}

		[Theory]
		[InlineData(1, 2)]
		[InlineData(-1, 3)]
		public void Fraction_Zero_addition_subtraction_does_not_change_result(long numerator, long denominator)
		{
			var fraction = new Fraction(numerator, denominator);
			var addResult = fraction + Fraction.Zero;
			var subtractResult = fraction + Fraction.Zero;

			subtractResult.Equals(addResult).ShouldBeTrue();
		}

		[Theory]
		[InlineData(1, 2, -1, 2)]
		[InlineData(3, -2, 3, 2)]
		[InlineData(-3, -4, -3, 4)]
		public void Fraction_Negate_as_expected(long numerator, long denominator, long expectedNumerator, long expectedDenominator)
		{
			var fraction = new Fraction(numerator, denominator);
			var negated = fraction.Negated();
			negated.Numerator.ShouldBe(expectedNumerator);
			negated.Denominator.ShouldBe(expectedDenominator);

			var operatorFraction = new Fraction(numerator, denominator);
			var operatorResult = -operatorFraction;
			operatorResult.Numerator.ShouldBe(expectedNumerator);
			operatorResult.Denominator.ShouldBe(expectedDenominator);
		}

		[Theory]
		[InlineData(3, 2, -1, 2, 1)]
		[InlineData(4, 2, 2, 1, 0)]
		[InlineData(1, -2, 1, 2, -1)]
		public void Fraction_comparisons_as_expected(long numerator, long denominator, long otherNominator, long otherDenominator, int expectedResult)
		{
			var fraction = new Fraction(numerator, denominator);
			var other = new Fraction(otherNominator, otherDenominator);
			var result = fraction.CompareTo(other);
			result.ShouldBe(expectedResult);
		}

		[Theory]
		[InlineData(3, 2, "3/2")]
		[InlineData(-10, 20, "-1/2")]
		public void Fraction_ToString_as_expected(long numerator, long denominator, string expectedResult)
		{
			var fraction = new Fraction(numerator, denominator);
			var result = fraction.ToString();
			result.ShouldBe(expectedResult);
		}

		[Theory]
		[InlineData(3, 2, 3, 2, true)]
		public void Fraction_Equals_as_expected(long numerator, long denominator, long otherNominator, long otherDenominator, bool expectedResult)
		{
			var fraction = new Fraction(numerator, denominator);
			var other = new Fraction(otherNominator, otherDenominator);
			var result = fraction.Equals(other);
			result.ShouldBe(expectedResult);

			result = fraction == other;
			result.ShouldBe(expectedResult);

			result = Equals(fraction, other);
			result.ShouldBe(expectedResult);
		}

		[Theory]
		[InlineData(3, 2, 1.5, true)]
		[InlineData(3, -4, -0.75, true)]
		public void Fraction_Equals_decimal_value_as_expected(long numerator, long denominator, double otherDecimalValue, bool expectedResult)
		{
			var fraction = new Fraction(numerator, denominator);
			var result = fraction == otherDecimalValue;
			result.ShouldBe(expectedResult);

			var negativeResult = fraction != otherDecimalValue;
			negativeResult.ShouldBe(!expectedResult);
		}

		[Theory]
		[InlineData(1, 2, 2)]
		[InlineData(6, 2, 6)]
		[InlineData(4, 3, 12)]
		[InlineData(-3, 2, 6)]
		public void Fraction_LowestCommonMultiple_as_expected(long first, long second, long expectedResult)
		{
			var result = Fraction.LowestCommonMultiple(first, second);
			result.ShouldBe(expectedResult);
		}

		[Theory]
		[InlineData(1, 2, 1)]
		[InlineData(6, 2, 2)]
		[InlineData(12, 3, 3)]
		[InlineData(-6, 2, 2)]
		public void Fraction_GreatestCommonDenominator_as_expected(long first, long second, long expectedResult)
		{
			var result = Fraction.GreatestCommonDenominator(first, second);
			result.ShouldBe(expectedResult);
		}

		[Theory]
		[InlineData("2÷7", "2/7")]
		[InlineData("2/7", "2/7")]
		[InlineData("-2/7", "-2/7")]
		[InlineData("3/-9", "-1/3")]
		[InlineData("4/2", "2/1")]
		[InlineData("1 1/2", "3/2")]
		[InlineData("4 -1/2", "7/2")]
		[InlineData("2 -1/2", "3/2")]
		[InlineData("-2 -1/2", "-3/2")]
		[InlineData("2 -1/-2", "5/2")]
		[InlineData("-2 1/2", "-5/2")]
		public void Fraction_FromString_as_expected(string input, string expectedResult)
		{
			var result = Fraction.FromString(input);
			result.ToString().ShouldBe(expectedResult);
		}

		[Fact]
		public void Fraction_FromString_throws_with_bad_input()
		{
			Should.Throw<ArgumentException>(() =>
			{
				var test = Fraction.FromString("xy/z");
			});

			Should.Throw<ArgumentException>(() =>
			{
				var test = Fraction.FromString("2-1/2");
			});
			
			Should.Throw<ArgumentException>(() =>
			{
				var test = Fraction.FromString("2+1/2");
			});
		}

		// examples taken from https://stackoverflow.com/a/32903747
		[Theory]
		[InlineData(1.00E-05, 1E-4, "1/99999", "0/1")]
		[InlineData(1.00E-05, 1E-5, "1/100000", "1/50000")]
		[InlineData(0.333, 1E-4, "333/1000", "257/772")]
		[InlineData(0.7777, 1E-4, "1109/1426", "7/9")]
		[InlineData(3.14159 /*pi*/, 1E-4, "333/106", "333/106")]
		[InlineData(2.71828 /*e*/, 1E-4, "193/71", "193/71")]
		[InlineData(0.745454545 /*(41/55)*/, 1E-4, "41/55", "41/55")]
		[InlineData(0.6152 /*(307/499)*/, 1E-4, "251/408", "171/278")]
		[InlineData(0.04832 /*(33/683)*/, 1E-4, "23/476", "3/62")]
		[InlineData(0.061 /*(33/541)*/, 1E-4, "28/459", "5/82")]
		[InlineData(0.2632 /*(5/19)*/, 1E-4, "329/1250", "5/19")]
		[InlineData(0.609981 /*(37/61)*/, 1E-4, "61/100", "61/100")]
		public void Fraction_FromDecimal_variations_as_expected(double decimalValue, double accuracy, string expectedClosest, string expectedSimplest)
		{
			var accurateResult = Fraction.ClosestFromDecimal(decimalValue, accuracy);
			accurateResult.ToString().ShouldBe(expectedClosest, $"input: {decimalValue}, accuracy: {accuracy}");

			var lowestResult = Fraction.SimplestFromDecimal(decimalValue, accuracy);
			lowestResult.ToString().ShouldBe(expectedSimplest, $"input: {decimalValue}, accuracy: {accuracy}");
		}
	}
}
