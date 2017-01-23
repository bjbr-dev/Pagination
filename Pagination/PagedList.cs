// <copyright file="PagedList.cs" company="Berkeleybross">
// Copyright (c) Berkeleybross. All rights reserved.
// </copyright>
namespace Pagination
{
    using System;
    using System.Collections.Generic;

    public class PagedList
    {
        public PagedList(int totalItemCount, int pageSize)
        {
            Ensure.GreaterThanOrEqualTo(0, totalItemCount, nameof(totalItemCount));
            Ensure.GreaterThanOrEqualTo(1, pageSize, nameof(pageSize));

            this.TotalItemCount = totalItemCount;
            this.PageSize = pageSize;

            this.PageCount = GetPageCount(totalItemCount, pageSize);
        }

        /// <summary>
        /// Gets the total number of items in the unpaged set.
        /// </summary>
        public int TotalItemCount { get; }

        /// <summary>
        /// Gets the number of pages in the entire set.
        /// </summary>
        public int PageCount { get; }

        /// <summary>
        /// Gets the maximum number of items on each page.
        /// </summary>
        public int PageSize { get; }

        /// <summary>
        /// Gets each page, starting from page 1 to the last page.
        /// </summary>
        public IEnumerable<Page> Pages
        {
            get
            {
                // Special case - first and last item index should be -1 when there are no items at all.
                if (this.TotalItemCount == 0)
                {
                    yield return new Page(1, this.PageSize, true, -1, -1);
                    yield break;
                }

                var lastItemIndex = this.TotalItemCount - 1;
                for (var i = 1; i <= this.PageCount; i++)
                {
                    var firstPageItemIndex = (i - 1) * this.PageSize;
                    var lastPageItemIndex = Math.Min(firstPageItemIndex + this.PageSize - 1, lastItemIndex);

                    yield return new Page(i, this.PageSize, i == this.PageCount, firstPageItemIndex, lastPageItemIndex);
                }
            }
        }

        private static int GetPageCount(int totalItemCount, int pageSize)
        {
            var lastItemIndex = totalItemCount - 1;
            return (lastItemIndex / pageSize) + 1;
        }
    }
}