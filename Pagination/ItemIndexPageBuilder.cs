// <copyright file="ItemIndexPageBuilder.cs" company="Berkeleybross">
// Copyright (c) Berkeleybross. All rights reserved.
// </copyright>
namespace Pagination
{
    using System;

    public class ItemIndexPageBuilder
        : IPageBuilder
    {
        public ItemIndexPageBuilder(int skip, int? take, bool capSkipAtLastPage = false)
        {
            Ensure.GreaterThanOrEqualTo(0, skip, nameof(skip));
            if (take != null)
            {
                Ensure.GreaterThanOrEqualTo(1, take.Value, nameof(take));
                if (skip % take.Value != 0)
                {
                    throw new ArgumentException("Skip must be a multiple of take");
                }
            }

            this.Skip = skip;
            this.Take = take;
            this.CapSkipAtLastPage = capSkipAtLastPage;
        }

        public int Skip { get; }

        public int? Take { get; }

        public bool CapSkipAtLastPage { get; }

        public Page GetCurrentPage(int totalNumberOfItems)
        {
            return this.Take != null
                ? this.GetCurrentPage(totalNumberOfItems, this.Skip, this.Take.Value)
                : GetCurrentPage(totalNumberOfItems, this.Skip);
        }

        public Page GetPageContainingItem(int itemIndex, int totalNumberOfItems)
        {
            Ensure.GreaterThanOrEqualTo(1, totalNumberOfItems, nameof(totalNumberOfItems));
            Ensure.LessThan(totalNumberOfItems, itemIndex, nameof(itemIndex));

            return this.Take != null
                ? this.GetCurrentPage(totalNumberOfItems, itemIndex, this.Take.Value)
                : GetCurrentPage(totalNumberOfItems, itemIndex);
        }

        private static Page GetCurrentPage(int totalNumberOfItems, int skip)
        {
            var pageNumber = skip == 0
                ? 1
                : 2;

            var firstPageItemIndex = totalNumberOfItems == 0
                ? -1
                : skip;
            var lastPageItemIndex = totalNumberOfItems - 1;
            return new Page(pageNumber, int.MaxValue, true, firstPageItemIndex, lastPageItemIndex);
        }

        private Page GetCurrentPage(int totalNumberOfItems, int skip, int take)
        {
            var pageNumber = (skip / take) + 1;
            var lastItemIndex = totalNumberOfItems - 1;
            var lastPageNumber = (lastItemIndex / take) + 1;

            // Special case if page out of bounds. It's a bit nonsensical,
            // but some use cases like to see an empty page instead of being "bounced" back to last page (e.g. API calls)
            if (pageNumber > lastPageNumber)
            {
                return this.CapSkipAtLastPage
                    ? new Page(lastPageNumber, take, true, take * (lastPageNumber - 1), lastItemIndex)
                    : new Page(pageNumber, take, false, -1, -1);
            }

            var firstPageItemIndex = Math.Min(skip, lastItemIndex);
            var lastPageItemIndex = Math.Min(checked(skip + (take - 1)), lastItemIndex);
            return new Page(pageNumber, take, pageNumber == lastPageNumber, firstPageItemIndex, lastPageItemIndex);
        }
    }
}