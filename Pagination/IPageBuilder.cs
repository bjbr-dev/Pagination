// <copyright file="IPageBuilder.cs" company="Berkeleybross">
// Copyright (c) Berkeleybross. All rights reserved.
// </copyright>
namespace Pagination
{
    public interface IPageBuilder
    {
        Page GetCurrentPage(int totalNumberOfItems);
    }
}