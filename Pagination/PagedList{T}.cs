// <copyright file="PagedList{T}.cs" company="Berkeleybross">
// Copyright (c) Berkeleybross. All rights reserved.
// </copyright>
namespace Pagination
{
    using System.Collections.Immutable;

    public class PagedList<T>
        : PagedList
    {
        public PagedList(int totalItemCount, int pageSize, Page<T> currentPage)
            : base(totalItemCount, pageSize)
        {
            this.CurrentPage = currentPage;
        }

        public Page<T> CurrentPage { get; }

        public ImmutableArray<T> Items => this.CurrentPage.Items;
    }
}