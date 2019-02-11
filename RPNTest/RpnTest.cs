﻿using System.Security.Policy;
using NUnit.Framework;
using RPN;

namespace RPNTest
{
    
    public class RpnTest
    {
        [Test]
        public void Given_a_simple_value_should_return_the_value()
        {
            string resultExpected="1";
            Assert.AreEqual(resultExpected,Rpn.Process("1"));
        }

        [Test]
        public void Given_a_empty_value_should_return_the_value()
        {
            string resultExpected = string.Empty;
            Assert.AreEqual(resultExpected, Rpn.Process(string.Empty));
        }

        [Test]
        [TestCase("Toto")]
        [TestCase("1 + 2")]
        [TestCase("toto + tata")]
        [TestCase("toto tata +")]
        public void Given_an_expression_with_no_operator_should_return_the_same(string input)
        {
            Assert.AreEqual(input, Rpn.Process(input));
        }

        [Test]

        public void Given_an_expression_with_plus_operator_should_return_the_sum()
        {
            string resultExpected="3";
            Assert.AreEqual(resultExpected,Rpn.Process("1 2 +"));
        }

        [Test]
        public void Given_an_expression_with_minus_operator_should_return_the_substraction()
        {
            string resultExpected="1";
            Assert.AreEqual(resultExpected,Rpn.Process("2 1 -"));
        }

        [Test]
        public void Given_an_random_expression_with_plus_operator_should_return_the_sum([Random(10)]int x, [Random(10)]int y)
        {
            string resultExpected = $"{x+y}";
            Assert.AreEqual(resultExpected, Rpn.Process($"{x} {y} +"));
        }

        [Test]
        public void CommutativityTestOfPlusOperator([Random(10)]int x, [Random(10)]int y)
        {
            string resultExpected = Rpn.Process($"{y} {x} +");
            Assert.AreEqual(resultExpected, Rpn.Process($"{x} {y} +"));
        }

        [Test]
        public void ElementZeroRightTestOfPlusOperator([Random(10)]int x)
        {
            string resultExpected = $"{x}";
            Assert.AreEqual(resultExpected, Rpn.Process($"{x} 0 +"));
        }

        [Test]
        public void ElementZeroLeftTestOfPlusOperator([Random(10)]int x)
        {
            string resultExpected = $"{x}";
            Assert.AreEqual(resultExpected, Rpn.Process($"0 {x} +"));
        }

        [Test]
        public void Given_an_random_expression_with_minus_operator_should_return_the_substraction([Range(-10,10)]int x, [Random(10)]int y)
        {
            string resultExpected = $"{x - y}";
            Assert.AreEqual(resultExpected, Rpn.Process($"{x} {y} -"));
        }

        [Test]
        public void ElementZeroRightTestOfMinusOperator([Range(-10, 10)]int x)
        {
            string resultExpected = $"{x}";
            Assert.AreEqual(resultExpected, Rpn.Process($"{x} 0 -"));
        }

        [Test]
        public void ElementZeroLeftTestOfMinusOperator([Range(-10, 10)]int x)
        {
            string resultExpected = $"{-x}";
            Assert.AreEqual(resultExpected, Rpn.Process($"0 {x} -"));
        }

        [Test]
        public void Given_an_expression_with_divide_operator_should_return_the_division_with_no_rest()
        {
            string resultExpected = "2";
            Assert.AreEqual(resultExpected, Rpn.Process("4 2 /"));
        }

        [Test]
        public void Given_an_expression_with_divide_operator_should_return_the_division_with_rest()
        {
            string resultExpected = "1";
            Assert.AreEqual(resultExpected, Rpn.Process("4 3 /"));
        }

        [Test]
        public void Given_an_expression_with_divide_operator_and_neutral_element_should_return_the_same([Random(10)]int x)
        {
            string resultExpected = $"{x}";
            Assert.AreEqual(resultExpected, Rpn.Process($"{x} 1 /"));
        }

        [Test]
        public void Given_an_expression_with_divide_operator_and_zero_should_return_input([Random(10)]int x)
        {
            string resultExpected = $"{x} 0 /";
            Assert.AreEqual(resultExpected, Rpn.Process($"{x} 0 /"));
        }

        [Test]
        public void Given_an_expression_with_two_operators_should_return_the_result_of_two_operations()
        {
            string resultExpected="105";
            Assert.AreEqual(resultExpected, Rpn.Process("120 10 5 + -"));
        }
    }
}
