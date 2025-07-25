﻿using Nexta.Application.DTO.RequestModels;
using MediatR;

namespace Nexta.Application.Commands.Admin.AddNewsCommand
{
    public class AddNewsCommandRequest : IRequest<AddNewsCommandResponse>
    {
		public string? Header { get; set; }
		public string? Description { get; set; }
        public NewsImageRequest? Image { get; init; }
    }
}