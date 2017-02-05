// <copyright file="PageIndexPageBuilder.cs" company="Berkeleybross">
// Copyright (c) Berkeleybross. All rights reserved.
// </copyright>
namespace Pagination
{
    using System;

    public class PageIndexPageBuilder
        : IPageBuilder
    {
        public PageIndexPageBuilder(int pageNumber, int pageSize)
        {
            Ensure.GreaterThanOrEqualTo(1, pageNumber, nameof(pageNumber));
            Ensure.GreaterThanOrEqualTo(0, pageSize, nameof(pageSize));

            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
        }

        public PageIndexPageBuilder(Page page)
        {
            Ensure.NotNull(page, nameof(page));

            this.PageNumber = page.PageNumber;
            this.PageSize = page.MaximumPageSize;
        }

        public int PageNumber { get; }

        public int PageSize { get; }

        public Page GetCurrentPage(int totalNumberOfItems)
        {
            return GetCurrentPage(totalNumberOfItems, this.PageNumber, this.PageSize);
        }

        public Page GetPageContainingItem(int itemIndex, int totalNumberOfItems)
        {
            Ensure.GreaterThanOrEqualTo(1, totalNumberOfItems, nameof(totalNumberOfItems));
            Ensure.LessThan(totalNumberOfItems, itemIndex, nameof(itemIndex));

            var page = (itemIndex / this.PageSize) + 1;
            return GetCurrentPage(totalNumberOfItems, page, this.PageSize);
        }

        private static Page GetCurrentPage(int totalNumberOfItems, int pageNumber, int pageSize)
        {
            var lastItemIndex = totalNumberOfItems - 1;
            var lastPageNumber = (lastItemIndex / pageSize) + 1;

            if (pageNumber > lastPageNumber)
            {
                return new Page(pageNumber, pageSize, false, -1, -1);
            }

            var firstPageItemIndex = Math.Min((pageNumber - 1) * pageSize, lastItemIndex);
            var lastPageItemIndex = Math.Min((pageNumber * pageSize) - 1, lastItemIndex);
            return new Page(pageNumber, pageSize, pageNumber == lastPageNumber, firstPageItemIndex, lastPageItemIndex);
        }
    }
}