// <copyright file="IPageBuilder.cs" company="Berkeleybross">
// Copyright (c) Berkeleybross. All rights reserved.
// </copyright>
namespace Pagination
{
    public interface IPageBuilder
    {
        /// <summary>
        /// Gets the currently viewed page, optionally limited to the total number of items
        /// </summary>
        Page GetCurrentPage(int totalNumberOfItems);

        /// <summary>
        /// Gets the page which contains an item at <paramref name="itemIndex"/>.
        /// </summary>
        /// <param name="itemIndex">The zero based index of the item</param>
        /// <param name="totalNumberOfItems"></param>
        Page GetPageContainingItem(int itemIndex, int totalNumberOfItems);
    }
}