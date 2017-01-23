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

        public int PageNumber { get; }

        public int PageSize { get; }

        public Page GetCurrentPage(int totalNumberOfItems)
        {
            var lastItemIndex = totalNumberOfItems - 1;
            var lastPageNumber = (lastItemIndex / this.PageSize) + 1;

            if (this.PageNumber > lastPageNumber)
            {
                return new Page(this.PageNumber, this.PageSize, false, -1, -1);
            }

            var firstPageItemIndex = Math.Min((this.PageNumber - 1) * this.PageSize, lastItemIndex);
            var lastPageItemIndex = Math.Min((this.PageNumber * this.PageSize) - 1, lastItemIndex);
            return new Page(this.PageNumber, this.PageSize, this.PageNumber == lastPageNumber, firstPageItemIndex, lastPageItemIndex);
        }
    }
}