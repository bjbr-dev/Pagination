// <copyright file="PaginationHelpers.cs" company="Berkeleybross">
// Copyright (c) Berkeleybross. All rights reserved.
// </copyright>
namespace Pagination
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class PaginationHelpers
    {
        /// <summary>
        /// <para>Selects a page from <paramref name="source"/>.</para>
        /// <para>This is equivalent to calling source.Skip(skip).Take(take) but also returns how many pages there are in the collection.</para>
        /// <para>This method enumerates the source twice.</para>
        /// </summary>
        public static PagedList<T> Page<T>(this IEnumerable<T> source, int skip, int? take)
        {
            return source.Page(new ItemIndexPageBuilder(skip, take));
        }

        /// <summary>
        /// <para>Selects a page from <paramref name="source"/>.</para>
        /// <para>This is equivalent to calling source.Skip(skip).Take(take) but also returns how many pages there are in the collection.</para>
        /// <para>This method enumerates the source twice.</para>
        /// </summary>
        public static PagedList<T> Page<T>(this IEnumerable<T> source, IPageBuilder pageBuilder)
        {
            var totalItemCount = source.Count();
            var page = pageBuilder.GetCurrentPage(totalItemCount);

            if (page.IsEmpty)
            {
                return PagedList<T>.Empty(totalItemCount, page);
            }

            var result = source.Skip(page.FirstItemIndex).Take(page.PageSize);
            return PagedList<T>.Create(totalItemCount, page, result);
        }

        /// <summary>
        /// <para>Selects a page from <paramref name="source"/>.</para>
        /// <para>This is equivalent to calling source.Skip(skip).Take(take) but also returns how many pages there are in the collection.</para>
        /// <para>This method queries the source twice.</para>
        /// </summary>
        public static PagedList<T> Page<T>(this IQueryable<T> source, int skip, int? take)
        {
            return source.Page(new ItemIndexPageBuilder(skip, take));
        }

        /// <summary>
        /// <para>Selects a page from <paramref name="source"/>.</para>
        /// <para>This is equivalent to calling source.Skip(skip).Take(take) but also returns how many pages there are in the collection.</para>
        /// <para>This method queries the source twice.</para>
        /// </summary>
        public static PagedList<T> Page<T>(this IQueryable<T> source, IPageBuilder pageBuilder)
        {
            var totalItemCount = source.Count();
            var page = pageBuilder.GetCurrentPage(totalItemCount);
            if (page.IsEmpty)
            {
                return PagedList<T>.Empty(totalItemCount, page);
            }

            var result = source.Skip(Math.Max(page.FirstItemIndex, 0)).Take(page.PageSize);
            return PagedList<T>.Create(totalItemCount, page, result);
        }
    }
}