using Nexta.Domain.Abstractions.Repositories;
using Nexta.Application.DTO.Admin;
using AutoMapper;
using MediatR;
using Nexta.Domain.Abstractions.Services;
using Nexta.Application.DTO;

namespace Nexta.Application.Queries.Admin.GetDetailQuery
{
	public class GetAdminDetailQueryHandler : IRequestHandler<GetAdminDetailQueryRequest, GetAdminDetailQueryResponse>
	{
		private readonly IDetailRepository _detailRepository;
		private readonly IMinioService _minioService;
		private readonly IMapper _mapper;
		public GetAdminDetailQueryHandler(IDetailRepository detailRepository, IMapper mapper, IMinioService minioService) 
		{
			_detailRepository = detailRepository;
			_minioService = minioService;
			_mapper = mapper;
		}
		public async Task<GetAdminDetailQueryResponse> Handle(GetAdminDetailQueryRequest request, CancellationToken ct = default)
		{
			var detail = _mapper.Map<AdminDetailResponse>(await _detailRepository.GetAsync(request.DetailId, ct));

			if (request.WithImage && detail.Image != null)
			{
				var minioImage = (await _minioService.GetFilesAsync("details", ct, detail.Image.Name!)).FirstOrDefault();
				var responseImage = _mapper.Map<ImageResponse>(minioImage);
				detail.Image = responseImage;
			}

			return new GetAdminDetailQueryResponse(detail);
		}
	}
}