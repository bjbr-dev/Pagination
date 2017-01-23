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
    }
}