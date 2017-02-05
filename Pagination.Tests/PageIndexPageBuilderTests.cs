// <copyright file="PageIndexPageBuilderTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace Pagination.Tests
{
    using System;
    using FluentAssertions;
    using NUnit.Framework;

    [TestFixture]
    public class PageIndexPageBuilderTests
    {
        private class Constructor
            : PageIndexPageBuilderTests
        {
            [Test]
            public void Throws_exception_when_current_page_number_is_less_than_1()
            {
                // Act
                Action act = () => new PageIndexPageBuilder(0, 10);

                // Assert
                act.ShouldThrow<ArgumentOutOfRangeException>()
                   .WithMessage("pageNumber must be greater than or equal to 1.\r\nParameter name: pageNumber\r\nActual value was 0.");
            }

            [Test]
            public void Throws_exception_when_page_size_is_less_than_0()
            {
                // Act
                Action act = () => new PageIndexPageBuilder(1, -1);

                // Assert
                act.ShouldThrow<ArgumentOutOfRangeException>()
                   .WithMessage("pageSize must be greater than or equal to 0.\r\nParameter name: pageSize\r\nActual value was -1.");
            }
        }

        private class GetCurrentPage
            : PageIndexPageBuilderTests
        {
            [Test]
            public void Sets_appropriate_values_when_there_are_no_items()
            {
                // Arrange
                var sut = new PageIndexPageBuilder(1, 10);

                // Act
                var result = sut.GetCurrentPage(0);

                // Assert
                result.ShouldBeEquivalentTo(new Page(1, 10, true, -1, -1));
            }

            [Test]
            public void Sets_appropriate_values_when_there_are_no_items_and_not_on_first_page()
            {
                // Arrange
                var sut = new PageIndexPageBuilder(4, 10);

                // Act
                var result = sut.GetCurrentPage(0);

                // Assert
                result.ShouldBeEquivalentTo(new Page(4, 10, false, -1, -1));
            }

            [Test]
            public void Sets_appropriate_values_when_not_on_last_page()
            {
                // Arrange
                var sut = new PageIndexPageBuilder(4, 10);

                // Act
                var result = sut.GetCurrentPage(60);

                // Assert
                result.ShouldBeEquivalentTo(new Page(4, 10, false, 30, 39));
            }

            [Test]
            public void Sets_appropriate_values_when_on_last_page_with_one_value()
            {
                // Arrange
                var sut = new PageIndexPageBuilder(4, 10);

                // Act
                var result = sut.GetCurrentPage(31);

                // Assert
                result.ShouldBeEquivalentTo(new Page(4, 10, true, 30, 30));
            }

            [Test]
            public void Sets_appropriate_values_when_on_last_page_with_several_value()
            {
                // Arrange
                var sut = new PageIndexPageBuilder(4, 10);

                // Act
                var result = sut.GetCurrentPage(34);

                // Assert
                result.ShouldBeEquivalentTo(new Page(4, 10, true, 30, 33));
            }

            [Test]
            public void Sets_appropriate_values_when_on_last_full_page()
            {
                // Arrange
                var sut = new PageIndexPageBuilder(4, 10);

                // Act
                var result = sut.GetCurrentPage(40);

                // Assert
                result.ShouldBeEquivalentTo(new Page(4, 10, true, 30, 39));
            }

            [Test]
            public void Sets_first_and_last_index_to_negative_1_when_past_last_page()
            {
                // Arrange
                var sut = new PageIndexPageBuilder(4, 10);

                // Act
                var result = sut.GetCurrentPage(20);

                // Assert
                result.ShouldBeEquivalentTo(new Page(4, 10, false, -1, -1));
            }
        }

        private class GetPageContainingItem
            : PageIndexPageBuilderTests
        {
            [TestCase(0)]
            [TestCase(-1)]
            public void Throws_exception_when_total_number_of_items_is_less_than_or_equal_to_zero(int totalNumberOfItems)
            {
                // Arrange
                var sut = new PageIndexPageBuilder(1, 10);

                // Act
                Action act = () => sut.GetPageContainingItem(0, totalNumberOfItems);

                // Assert
                act.ShouldThrow<ArgumentException>()
                   .WithMessage($"totalNumberOfItems must be greater than or equal to 1.\r\nParameter name: totalNumberOfItems\r\nActual value was {totalNumberOfItems}.");
            }

            [TestCase(26)]
            [TestCase(27)]
            public void Throws_exception_when_item_index_is_greater_than_the_total_number_of_items(int itemIndex)
            {
                // Arrange
                var sut = new PageIndexPageBuilder(4, 10);

                // Act
                Action act = () => sut.GetPageContainingItem(itemIndex, 26);

                // Assert
                act.ShouldThrow<ArgumentException>()
                   .WithMessage($"itemIndex must be less than 26.\r\nParameter name: itemIndex\r\nActual value was {itemIndex}.");
            }

            [Test]
            public void Sets_appropriate_values_when_not_on_last_page()
            {
                // Arrange
                var sut = new PageIndexPageBuilder(4, 10);

                // Act
                var result = sut.GetPageContainingItem(12, 60);

                // Assert
                result.ShouldBeEquivalentTo(new Page(2, 10, false, 10, 19));
            }

            [Test]
            public void Sets_appropriate_values_when_on_last_page_with_one_value()
            {
                // Arrange
                var sut = new PageIndexPageBuilder(4, 10);

                // Act
                var result = sut.GetPageContainingItem(30, 31);

                // Assert
                result.ShouldBeEquivalentTo(new Page(4, 10, true, 30, 30));
            }

            [Test]
            public void Sets_appropriate_values_when_on_last_page_with_several_value()
            {
                // Arrange
                var sut = new PageIndexPageBuilder(4, 10);

                // Act
                var result = sut.GetPageContainingItem(30, 34);

                // Assert
                result.ShouldBeEquivalentTo(new Page(4, 10, true, 30, 33));
            }

            [Test]
            public void Sets_appropriate_values_when_on_last_full_page()
            {
                // Arrange
                var sut = new PageIndexPageBuilder(4, 10);

                // Act
                var result = sut.GetPageContainingItem(30, 40);

                // Assert
                result.ShouldBeEquivalentTo(new Page(4, 10, true, 30, 39));
            }
        }
    }
}