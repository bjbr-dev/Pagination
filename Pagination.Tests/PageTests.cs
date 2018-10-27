// <copyright file="PageTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace Pagination.Tests
{
    using System;
    using FluentAssertions;
    using Xunit;

    public class PageTests
    {
        public class Constructor
            : PageTests
        {
            [Fact]
            public void Throws_exception_when_page_numer_is_less_than_1()
            {
                // Arrange
                // Act
                Action act = () => new Page(0, 10, true, 0, 9);

                // Assert
                act.Should().Throw<ArgumentException>()
                   .WithMessage("pageNumber must be greater than or equal to 1.\r\nParameter name: pageNumber\r\nActual value was 0.");
            }

            [Fact]
            public void Throws_exception_when_first_item_index_is_less_than_negative_1()
            {
                // Arrange
                // Act
                Action act = () => new Page(1, 10, true, -2, 9);

                // Assert
                act.Should().Throw<ArgumentException>()
                   .WithMessage("firstItemIndex must be greater than or equal to -1.\r\nParameter name: firstItemIndex\r\nActual value was -2.");
            }

            [Fact]
            public void Throws_exception_when_last_item_index_is_less_than_negative_1()
            {
                // Arrange
                // Act
                Action act = () => new Page(1, 10, true, 0, -2);

                // Assert
                act.Should().Throw<ArgumentException>()
                   .WithMessage("lastItemIndex must be greater than or equal to -1.\r\nParameter name: lastItemIndex\r\nActual value was -2.");
            }

            [Fact]
            public void Throws_exception_when_first_item_is_greater_than_last_item()
            {
                // Arrange
                // Act
                Action act = () => new Page(1, 10, true, 20, 10);

                // Assert
                act.Should().Throw<ArgumentException>()
                   .WithMessage("lastItemIndex must be greater than or equal to 20.\r\nParameter name: lastItemIndex\r\nActual value was 10.");
            }

            [Fact]
            public void Throws_exception_when_one_index_is_negative_1_but_other_is_not()
            {
                // Arrange
                // Act
                Action act = () => new Page(1, 10, true, -1, 10);

                // Assert
                act.Should().Throw<ArgumentException>()
                   .WithMessage("When firstItemIndex or lastItemIndex is -1, both must be");
            }
        }

        public class PageSize
            : PageTests
        {
            [Theory]
            [InlineData(-1, -1, 0)]
            [InlineData(0, 0, 1)]
            [InlineData(0, 4, 5)]
            [InlineData(5, 9, 5)]
            [InlineData(5, 5, 1)]
            public void Returns_appropriate_value(int firstPageIndex, int lastPageIndex, int expected)
            {
                var sut = new Page(1, 10, true, firstPageIndex, lastPageIndex);
                sut.PageSize.Should().Be(expected);
            }
        }
    }
}