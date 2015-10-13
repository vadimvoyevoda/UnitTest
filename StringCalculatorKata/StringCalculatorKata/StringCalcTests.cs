using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using FluentAssertions;

namespace StringCalculatorKata
{
    [TestFixture]
    public class StringCalcTests
    {
        [Test]
        public void should_return_zero_for_empty_string()
        {
            //arrange
            var calc = new StringCalcClass();

            //act
            var result = calc.Add("");

            //assert
            result.ShouldBeEquivalentTo(0);
        }

        [Test]
        public void should_return_one_for_one()
        {
            //arrange
            var calc = new StringCalcClass();

            //act
            var result = calc.Add("1");

            //assert
            result.ShouldBeEquivalentTo(1);
        }

        [Test]
        public void should_return_sum_for_two_digits()
        {
            //arrange
            var calc = new StringCalcClass();

            //act
            var result = calc.Add("1,2");

            //assert
            result.ShouldBeEquivalentTo(3);
        }

        [Test]
        public void should_return_number_for_simple_number_string()
        {
            //arrange
            var calc = new StringCalcClass();
            int number = new Random().Next(10);

            //act
            var result = calc.Add(number.ToString());

            //assert
            result.ShouldBeEquivalentTo(number);
        }

        [Test]
        public void should_handle_two_digits()
        {
            //arrange
            var calc = new StringCalcClass();
            var rand = new Random(); 
            int one = rand.Next(10);
            int two = rand.Next(10);

            //act
            var result = calc.Add(one + "," + two);
            
            //assert
            result.ShouldBeEquivalentTo(one + two);
        }

        [Test]
        public void should_handle_unknown_amount_of_numbers()
        {
            //arrange
            var calc = new StringCalcClass();
            var rand = new Random();
            var digitsNumber = rand.Next(10);

            var digits = new List<int>();
            for (int i = 0; i < digitsNumber; i++)
            {
                digits.Add(rand.Next(10));
            }

            //act
            var result = calc.Add(string.Join(",",digits));

            //assert
            result.ShouldBeEquivalentTo(digits.Sum());
        }

        [Test]
        public void should_handle_new_lines_between_numbers()
        {
            //arrange
            var calc = new StringCalcClass();

            //act
            var result = calc.Add("1\n2,3");

            //assert
            result.ShouldBeEquivalentTo(6);
        }

        [Test]
        public void should_handle_change_delimiter()
        {
            //arrange
            var calc = new StringCalcClass();

            //act
            var result = calc.Add("//;\n1;2");

            //assert
            result.ShouldBeEquivalentTo(3);
        }

        [Test]
        public void should_return_exception_for_negative_number()
        {
            //arrange
            var calc = new StringCalcClass();

            //act && assert
            Assert.Throws<ArgumentException>(() => calc.Add("-1"));
        }

        [Test]
        public void should_return_exception_for_all_negative_numbers()
        {
            //arrange
            var calc = new StringCalcClass();

            //act && assert
            var exc = Assert.Throws<ArgumentException>(() => calc.Add("-1,0,-4,4,-2"));
            StringAssert.Contains("-1,-4,-2", exc.Message);                
        }

        [Test]
        public void should_ignore_numbers_more_thousand()
        {
            //arrange
            var calc = new StringCalcClass();

            //act
            var res = calc.Add("2,1001");

            //assert
            res.ShouldBeEquivalentTo(2);
        }

        [Test]
        public void should_handle_for_any_length_delimiter()
        {
            //arrange
            var calc = new StringCalcClass();

            //act
            var result = calc.Add("//[***]\n1***2***3");

            //assert
            result.ShouldBeEquivalentTo(6);
        }

        [Test]
        public void should_handle_for_multiple_delimiter()
        {
            //arrange
            var calc = new StringCalcClass();

            //act
            var result = calc.Add("//[*][%]\n1*2%3");

            //assert
            result.ShouldBeEquivalentTo(6);
        }

        [Test]
        public void should_handle_for_multiple_any_length_delimiter()
        {
            //arrange
            var calc = new StringCalcClass();

            //act
            var result = calc.Add("//[**][%][---]\n1**2%3---1");

            //assert
            result.ShouldBeEquivalentTo(7);
        }
    }
}
