// <copyright file="Ensure.cs" company="Berkeleybross">
// Copyright (c) Berkeleybross. All rights reserved.
// </copyright>
namespace Pagination
{
    using System;

    internal static class Ensure
    {
        public static void GreaterThanOrEqualTo(int expected, int actual, string paramName)
        {
            if (!(actual >= expected))
            {
                throw new ArgumentOutOfRangeException(paramName, actual, $"{paramName} must be greater than or equal to {expected}.");
            }
        }

        public static void LessThan(int expected, int actual, string paramName)
        {
            if (!(actual < expected))
            {
                throw new ArgumentOutOfRangeException(paramName, actual, $"{paramName} must be less than {expected}.");
            }
        }

        public static void NotNull<T>(T value, string paramName)
            where T : class
        {
            if (value == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }
    }
}