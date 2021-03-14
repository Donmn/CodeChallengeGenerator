using Generator.Processor.Utilities;
using System;
using Xunit;

namespace Generator.Processor.Unit.Tests.Utilities
{
    public class AsserterTests
    {
        [Fact]
        public void AssertNotNull_WithNull_Throws()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => Asserter.AssertNotNull(null, "anArgument"));

            Assert.Contains("anArgument", ex.Message);
        }

        [Fact]
        public void AssertNotNull_WithNonNull_DoesNotThrow()
        {
            Asserter.AssertNotNull(new object(), "anArgument");
        }

        [Fact]
        public void AssertIsNotZero_WithZero_Throws()
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Asserter.AssertIsNotZero(0, "anArgument"));

            Assert.Contains("anArgument", ex.Message);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(-1)]
        public void AssertIsNotZero_WithNumberThatIsNotZero_DoesNotThrow(double valueToTest)
        {
            Asserter.AssertIsNotZero(valueToTest, "anArgument");
        }

        [Fact]
        public void AssertIsTrue_WithFalse_Throws()
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Asserter.AssertIsTrue(false, "anArgument", "aMessage"));

            Assert.Contains("anArgument", ex.Message);
            Assert.Contains("aMessage", ex.Message);
        }

        [Fact]
        public void AssertIsTrue_WithTrue_DoesNotThrow()
        {
            Asserter.AssertIsTrue(true, "anArgument", "aMessage");
        }


        [Fact]
        public void AssertStringIsNotNullOrEmpty_WithValidString_DoesNotThrows()
        {
            Asserter.AssertStringIsNotNullOrEmpty("aValue", "anArgument");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void AssertStringIsNotNullOrEmpty_WithInvalidString_ThrowsAndReturnsMessage(string valueToTest)
        {
            var ex = Assert.Throws<ArgumentNullException>(() => Asserter.AssertStringIsNotNullOrEmpty(valueToTest, "anArgument"));

            Assert.Contains("anArgument", ex.Message);
            Assert.Contains("null", ex.Message);
            Assert.Contains("empty", ex.Message);
            Assert.Contains("white space", ex.Message);
        }

        [Fact]
        public void AssertStringIsNotNullOrEmpty_WithValidStringAndCustomMessage_DoesNotThrows()
        {
            Asserter.AssertStringIsNotNullOrEmpty("aValue", "anArgument", "aMessage");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void AssertStringIsNotNullOrEmpty_WithInvalidString_ThrowsAndReturnsCustomMessage(string valueToTest)
        {
            var ex = Assert.Throws<ArgumentNullException>(() => Asserter.AssertStringIsNotNullOrEmpty(valueToTest, "anArgument", "aMessage"));

            Assert.Contains("anArgument", ex.Message);
            Assert.Contains("aMessage", ex.Message);
        }

    }
}
