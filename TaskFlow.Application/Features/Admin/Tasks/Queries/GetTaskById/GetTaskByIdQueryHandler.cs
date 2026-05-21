using AutoMapper;
using MediatR;
using TaskFlow.Application.Common;
using TaskFlow.Application.Features.Admin.Tasks.DTOs;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.Application.Features.Admin.Tasks.Queries.GetTaskById;

public class GetTaskByIdQueryHandler : IRequestHandler<GetTaskByIdQuery, Result<TaskDetailDto>>
{
    private readonly IRepository<TaskItem> _taskRepository;
    private readonly IRepository<Project> _projectRepository;
    private readonly IMapper _mapper;

    public GetTaskByIdQueryHandler(IRepository<TaskItem> taskRepository, IRepository<Project> projectRepository, IMapper mapper)
    {
        _taskRepository = taskRepository;
        _projectRepository = projectRepository;
        _mapper = mapper;
    }

    public async Task<Result<TaskDetailDto>> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetByIdAsync(request.Id);
        if (task is null)
            return Result.Failure<TaskDetailDto>("Task not found", 404);

        var project = await _projectRepository.GetByIdAsync(task.ProjectId);

        var dto = _mapper.Map<TaskDetailDto>(task);
        dto.ProjectName = project?.Name ?? string.Empty;

        return Result.Success(dto);
    }
}
