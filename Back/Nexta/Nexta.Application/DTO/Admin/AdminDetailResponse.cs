﻿using Nexta.Domain.Enums;

namespace Nexta.Application.DTO.Admin
{
    public class AdminDetailResponse
    {
		public Guid Id { get; init; }
		public string Name { get; init; } = null!;
		public string Article { get; init; } = null!;
		public string Description { get; init; } = null!;
		public DetailStatus Status { get; init; }

		public string OrderDate { get; init; } = string.Empty;
		public string DeliveryDate { get; init; } = string.Empty;

		public int Count { get; init; }
		public int NewPrice { get; init; }
		public int OldPrice { get; init; }
		public bool isVisible { get; init; }

		public Guid ImageId { get; init; }
		public AdminDetailImageResponse? Image { get; set; }
	}
}