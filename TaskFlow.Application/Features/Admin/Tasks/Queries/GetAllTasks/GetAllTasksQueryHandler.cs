using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using TaskFlow.Application.Common;
using TaskFlow.Application.Features.Admin.Tasks.DTOs;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.Application.Features.Admin.Tasks.Queries.GetAllTasks;

public class GetAllTasksQueryHandler : IRequestHandler<GetAllTasksQuery, Result<PagedResult<TaskListDto>>>
{
    private readonly IRepository<TaskItem> _taskRepository;
    private readonly IMapper _mapper;

    public GetAllTasksQueryHandler(IRepository<TaskItem> taskRepository, IMapper mapper)
    {
        _taskRepository = taskRepository;
        _mapper = mapper;
    }

    public async Task<Result<PagedResult<TaskListDto>>> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
    {
        var query = _taskRepository.Query();

        if (!string.IsNullOrWhiteSpace(request.Search))
            query = query.Where(t => t.Title.Contains(request.Search));

        var totalCount = query.Count();

        var items = query
            .OrderByDescending(t => t.CreatedAt)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ProjectTo<TaskListDto>(_mapper.ConfigurationProvider)
            .ToList();

        return Result.Success(new PagedResult<TaskListDto>
        {
            Items = items,
            TotalCount = totalCount,
            Page = request.Page,
            PageSize = request.PageSize
        });
    }
}
