// <copyright file="PagedList{T}.cs" company="Berkeleybross">
// Copyright (c) Berkeleybross. All rights reserved.
// </copyright>
namespace Pagination
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Linq;

    public class PagedList<T>
        : PagedList
    {
        public PagedList(int totalItemCount, Page<T> currentPage)
            : base(totalItemCount, currentPage.MaximumPageSize)
        {
            this.CurrentPage = currentPage;
        }

        public Page<T> CurrentPage { get; }

        public ImmutableArray<T> Items => this.CurrentPage.Items;

        public static PagedList<T> Empty(int totalItemCount, Page page)
        {
            return Create(totalItemCount, page, ImmutableArray<T>.Empty);
        }

        public static PagedList<T> Create(int totalItemCount, Page page, IEnumerable<T> items)
        {
            return Create(totalItemCount, page, items.ToImmutableArray());
        }

        public static PagedList<T> Create(int totalItemCount, Page page, ImmutableArray<T> items)
        {
            return new PagedList<T>(totalItemCount, new Page<T>(page, items));
        }

        public PagedList<TOther> Transform<TOther>(Func<T, TOther> selector)
        {
            var otherPage = new Page<TOther>(this.CurrentPage, this.CurrentPage.Items.Select(selector).ToImmutableArray());
            return new PagedList<TOther>(this.TotalItemCount, otherPage);
        }
    }
}