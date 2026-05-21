using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using TaskFlow.Application.Common;
using TaskFlow.Application.Features.Admin.Projects.DTOs;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.Application.Features.Admin.Projects.Queries.GetAllProjects;

public class GetAllProjectsQueryHandler : IRequestHandler<GetAllProjectsQuery, Result<PagedResult<ProjectListDto>>>
{
    private readonly IRepository<Project> _projectRepository;
    private readonly IMapper _mapper;

    public GetAllProjectsQueryHandler(IRepository<Project> projectRepository, IMapper mapper)
    {
        _projectRepository = projectRepository;
        _mapper = mapper;
    }

    public async Task<Result<PagedResult<ProjectListDto>>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
    {
        var query = _projectRepository.Query();

        if (!string.IsNullOrWhiteSpace(request.Search))
            query = query.Where(p => p.Name.Contains(request.Search) || p.Description.Contains(request.Search));

        var totalCount = await Task.Run(() => query.Count(), cancellationToken);

        var items = query
            .OrderByDescending(p => p.CreatedAt)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ProjectTo<ProjectListDto>(_mapper.ConfigurationProvider)
            .ToList();

        return Result.Success(new PagedResult<ProjectListDto>
        {
            Items = items,
            TotalCount = totalCount,
            Page = request.Page,
            PageSize = request.PageSize
        });
    }
}
