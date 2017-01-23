// <copyright file="PageTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace Pagination.Tests
{
    using System;
    using FluentAssertions;
    using NUnit.Framework;

    [TestFixture]
    public class PageTests
    {
        private class Constructor
            : PageTests
        {
            [Test]
            public void Throws_exception_when_page_numer_is_less_than_1()
            {
                // Arrange
                // Act
                Action act = () => new Page(0, 10, true, 0, 9);

                // Assert
                act.ShouldThrow<ArgumentException>()
                   .WithMessage("pageNumber must be greater than or equal to 1.\r\nParameter name: pageNumber\r\nActual value was 0.");
            }

            [Test]
            public void Throws_exception_when_first_item_index_is_less_than_negative_1()
            {
                // Arrange
                // Act
                Action act = () => new Page(1, 10, true, -2, 9);

                // Assert
                act.ShouldThrow<ArgumentException>()
                   .WithMessage("firstItemIndex must be greater than or equal to -1.\r\nParameter name: firstItemIndex\r\nActual value was -2.");
            }

            [Test]
            public void Throws_exception_when_last_item_index_is_less_than_negative_1()
            {
                // Arrange
                // Act
                Action act = () => new Page(1, 10, true, 0, -2);

                // Assert
                act.ShouldThrow<ArgumentException>()
                   .WithMessage("lastItemIndex must be greater than or equal to -1.\r\nParameter name: lastItemIndex\r\nActual value was -2.");
            }

            [Test]
            public void Throws_exception_when_first_item_is_greater_than_last_item()
            {
                // Arrange
                // Act
                Action act = () => new Page(1, 10, true, 20, 10);

                // Assert
                act.ShouldThrow<ArgumentException>()
                   .WithMessage("lastItemIndex must be greater than or equal to 20.\r\nParameter name: lastItemIndex\r\nActual value was 10.");
            }

            [Test]
            public void Throws_exception_when_one_index_is_negative_1_but_other_is_not()
            {
                // Arrange
                // Act
                Action act = () => new Page(1, 10, true, -1, 10);

                // Assert
                act.ShouldThrow<ArgumentException>()
                   .WithMessage("When firstItemIndex or lastItemIndex is -1, both must be");
            }
        }

        private class PageSize
            : PageTests
        {
            [TestCase(-1, -1, ExpectedResult = 0, Description = "Returns 0 when there are no items")]
            [TestCase(0, 0, ExpectedResult = 1, Description = "Returns 1 when there is only 1 item")]
            [TestCase(0, 4, ExpectedResult = 5, Description = "On first page")]
            [TestCase(5, 9, ExpectedResult = 5, Description = "Not on first page")]
            [TestCase(5, 5, ExpectedResult = 1, Description = "Only one item but not on first page")]
            public int Returns_appropriate_value(int firstPageIndex, int lastPageIndex)
            {
                var sut = new Page(1, 10, true, firstPageIndex, lastPageIndex);
                return sut.PageSize;
            }
        }
    }
}