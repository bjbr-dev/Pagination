// <copyright file="Page.cs" company="Berkeleybross">
// Copyright (c) Berkeleybross. All rights reserved.
// </copyright>
namespace Pagination
{
    using System;

    public class Page
    {
        public Page(int pageNumber, int maximumPageSize, bool isLast, int firstItemIndex, int lastItemIndex)
        {
            Ensure.GreaterThanOrEqualTo(1, pageNumber, nameof(pageNumber));
            Ensure.GreaterThanOrEqualTo(-1, firstItemIndex, nameof(firstItemIndex));
            Ensure.GreaterThanOrEqualTo(-1, lastItemIndex, nameof(lastItemIndex));
            Ensure.GreaterThanOrEqualTo(firstItemIndex, lastItemIndex, nameof(lastItemIndex));

            int pageSize;
            if (firstItemIndex == -1)
            {
                if (firstItemIndex != lastItemIndex)
                {
                    throw new ArgumentException("When firstItemIndex or lastItemIndex is -1, both must be");
                }

                pageSize = 0;
            }
            else
            {
                pageSize = (lastItemIndex - firstItemIndex) + 1;
            }

            if (this.PageSize > maximumPageSize)
            {
                throw new ArgumentException("Maximum page size is less than the number of items on this page");
            }

            this.PageNumber = pageNumber;
            this.MaximumPageSize = maximumPageSize;
            this.PageSize = pageSize;
            this.IsFirst = pageNumber == 1;
            this.IsLast = isLast;
            this.FirstItemIndex = firstItemIndex;
            this.LastItemIndex = lastItemIndex;
        }

        protected Page(Page other)
        {
            this.PageNumber = other.PageNumber;
            this.MaximumPageSize = other.MaximumPageSize;
            this.PageSize = other.PageSize;
            this.IsFirst = other.IsFirst;
            this.IsLast = other.IsLast;
            this.FirstItemIndex = other.FirstItemIndex;
            this.LastItemIndex = other.LastItemIndex;
        }

        /// <summary>
        /// Gets the 1-based number of this page in the list.
        /// </summary>
        public int PageNumber { get; }

        /// <summary>
        /// Gets the maximum number of items allowed on the page.
        /// </summary>
        public int MaximumPageSize { get; }

        /// <summary>
        /// <para>Gets the actual number of items on this page.</para>
        /// <para>This is usually the same as <see cref="MaximumPageSize"/>, except on the last page</para>
        /// </summary>
        public int PageSize { get; }

        /// <summary>
        /// Gets a value indicating whether this page is the first in the collection.
        /// </summary>
        public bool IsFirst { get; }

        /// <summary>
        /// Gets a value indicating whether this page is the last in the collection.
        /// </summary>
        public bool IsLast { get; }

        /// <summary>
        /// Gets the 0-based index of the first item on this page, in respect to the entire collection.
        /// (Or -1 if there are no items in this page)
        /// </summary>
        public int FirstItemIndex { get; }

        /// <summary>
        /// Gets the 0-based index of the last item on this page, in respect to the entire collection
        /// (Or -1 if there are no items in this page)
        /// </summary>
        public int LastItemIndex { get; }
    }
}