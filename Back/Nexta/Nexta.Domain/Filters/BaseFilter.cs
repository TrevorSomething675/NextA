﻿namespace Nexta.Domain.Filters
{
    public class BaseFilter
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 8;
    }
}