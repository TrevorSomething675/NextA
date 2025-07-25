﻿using Nexta.Domain.Models.DataModels;

namespace Nexta.Application.Common
{
    public class BasePagedResponse<T>
	{
		public BasePagedResponse(PagedData<T> data)
		{
			Data = data;
		}

		public PagedData<T> Data { get; init; }
	}
}