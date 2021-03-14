using System;

namespace Generator.Processor.Utilities
{
    public static class Asserter
    {
        public static void AssertNotNull(object argument, string argumentName)
        {
            if (argument == null)
                throw new ArgumentNullException(argumentName, $"<{argumentName}> is null.");
        }

        public static void AssertIsNotZero(double argument, string argumentName)
        {
            if (argument == 0)
                throw new ArgumentOutOfRangeException(argumentName, $"<{argumentName}> must not be equal to zero.");
        }

        public static void AssertIsTrue(bool argument, string argumentName, string message)
        {
            if (!argument)
                throw new ArgumentOutOfRangeException(argumentName, message);

        }

        public static void AssertStringIsNotNullOrEmpty(string argument, string argumentName)
        {
            AssertStringIsNotNullOrEmpty(argument, argumentName, $"<{argumentName}> cannot be null, empty or just white space");
        }

        public static void AssertStringIsNotNullOrEmpty(string argument, string argumentName, string message)
        {
            if (String.IsNullOrEmpty(argument) || String.IsNullOrWhiteSpace(argument))
                throw new ArgumentNullException(argumentName, message);
        }
    }
}
