using Nexta.Domain.Abstractions.Repositories;
using Nexta.Application.DTO;
using Nexta.Domain.Models;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Commands.Admin.UpdateDetailCommand
{
	public class UpdateAdminDetailCommandHandler : IRequestHandler<UpdateAdminDetailCommandRequest, UpdateAdminDetailCommandResponse>
	{
		private readonly IDetailRepository _detailRepository;
		private readonly IMapper _mapper;

		public UpdateAdminDetailCommandHandler(IDetailRepository detailRepository, IMapper mapper)
		{
			_detailRepository = detailRepository;
			_mapper = mapper;
		}

		public async Task<UpdateAdminDetailCommandResponse> Handle(UpdateAdminDetailCommandRequest request, CancellationToken ct = default)
		{
			var detailToUpdate = _mapper.Map<Detail>(request);

			var updatedDetail = await _detailRepository.UpdateAsync(detailToUpdate, ct);
			var detailResponse = _mapper.Map<DetailResponse>(updatedDetail);

			return new UpdateAdminDetailCommandResponse(detailResponse);
		}
	}
}