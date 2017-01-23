// <copyright file="Page{T}.cs" company="Berkeleybross">
// Copyright (c) Berkeleybross. All rights reserved.
// </copyright>
namespace Pagination
{
    using System.Collections.Immutable;

    public class Page<T>
        : Page
    {
        public Page(Page page, ImmutableArray<T> items)
            : base(page)
        {
            this.Items = items;
        }

        public ImmutableArray<T> Items { get; }
    }
}