// <copyright file="PagedListTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace Pagination.Tests
{
    using System;
    using FluentAssertions;
    using Xunit;

    public class PagedListTests
    {
        public class Constructor
            : PagedListTests
        {
            [Fact]
            public void Throws_exception_when_total_item_count_is_less_than_0()
            {
                // Act
                Action act = () => new PagedList(-1, new Page(1, 10, false, -1, -1));

                // Assert
                act.ShouldThrow<ArgumentOutOfRangeException>();
            }
        }

        public class TotalItemCount
            : PagedListTests
        {
            [Fact]
            public void Returns_constructor_argument()
            {
                // Arrange
                var sut = new PagedList(42, new Page(1, 10, false, -1, -1));

                // Act
                var result = sut.TotalItemCount;

                // Assert
                result.Should().Be(42);
            }
        }

        public class PageCount
            : PagedListTests
        {
            [Theory]
            [InlineData(0, 10,  1)]
            [InlineData(5, 10,  1)]
            [InlineData(10, 10,  1)]
            [InlineData(20, 5,  4)]
            [InlineData(16, 5,  4)]
            [InlineData(18, 5,  4)]
            public void Returns_appropriate_value(int totalItemCount, int pageSize, int expected)
            {
                // Arrange
                var sut = new PagedList(totalItemCount, new Page(1, pageSize, false, -1, -1));

                // Act
                sut.PageCount.Should().Be(expected);
            }
        }

        public class Pages
            : PagedListTests
        {
            [Fact]
            public void Returns_a_single_page_when_there_are_no_items()
            {
                // Arrange
                var sut = new PagedList(0, new Page(1, 10, false, -1, -1));

                // Act
                var pages = sut.Pages;

                // Assert
                pages.ShouldBeEquivalentTo(new[]
                    {
                        new Page(1, 10, true, -1, -1)
                    });
            }

            [Fact]
            public void Returns_a_single_page_when_number_of_items_is_less_than_page_size()
            {
                // Arrange
                var sut = new PagedList(5, new Page(1, 10, false, -1, -1));

                // Act
                var pages = sut.Pages;

                // Assert
                pages.ShouldBeEquivalentTo(new[]
                    {
                        new Page(1, 10, true, 0, 4)
                    });
            }

            [Fact]
            public void Returns_a_single_page_when_number_of_items_is_equal_to_page_size()
            {
                // Arrange
                var sut = new PagedList(10, new Page(1, 10, false, -1, -1));

                // Act
                var pages = sut.Pages;

                // Assert
                pages.ShouldBeEquivalentTo(new[]
                    {
                        new Page(1, 10, true, 0, 9)
                    });
            }

            [Fact]
            public void Returns_multiple_pages_when_there_are_more_items_than_page_size()
            {
                // Arrange
                var sut = new PagedList(20, new Page(1, 5, false, -1, -1));

                // Act
                var pages = sut.Pages;

                // Assert
                pages.ShouldBeEquivalentTo(new[]
                    {
                        new Page(1, 5, false, 0, 4),
                        new Page(2, 5, false, 5, 9),
                        new Page(3, 5, false, 10, 14),
                        new Page(4, 5, true, 15, 19)
                    });
            }

            [Fact]
            public void Returns_multiple_pages_when_there_is_only_one_item_on_the_last_page()
            {
                // Arrange
                var sut = new PagedList(16, new Page(1, 5, false, -1, -1));

                // Act
                var pages = sut.Pages;

                // Assert
                pages.ShouldBeEquivalentTo(new[]
                    {
                        new Page(1, 5, false, 0, 4),
                        new Page(2, 5, false, 5, 9),
                        new Page(3, 5, false, 10, 14),
                        new Page(4, 5, true, 15, 15)
                    });
            }

            [Fact]
            public void Returns_multiple_pages_when_the_last_page_is_partially_filled()
            {
                // Arrange
                var sut = new PagedList(18, new Page(1, 5, false, -1, -1));

                // Act
                var pages = sut.Pages;

                // Assert
                pages.ShouldBeEquivalentTo(new[]
                    {
                        new Page(1, 5, false, 0, 4),
                        new Page(2, 5, false, 5, 9),
                        new Page(3, 5, false, 10, 14),
                        new Page(4, 5, true, 15, 17)
                    });
            }
        }
    }
}