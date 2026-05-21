using AutoMapper;
using MediatR;
using TaskFlow.Application.Common;
using TaskFlow.Application.Features.Admin.Tasks.DTOs;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.Application.Features.Admin.Tasks.Commands.UpdateTask;

public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, Result<TaskDetailDto>>
{
    private readonly IRepository<TaskItem> _taskRepository;
    private readonly IRepository<Project> _projectRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateTaskCommandHandler(
        IRepository<TaskItem> taskRepository,
        IRepository<Project> projectRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _taskRepository = taskRepository;
        _projectRepository = projectRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<TaskDetailDto>> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetByIdAsync(request.Id);
        if (task is null)
            return Result.Failure<TaskDetailDto>("Task not found", 404);

        task.Title = request.Title;
        task.Description = request.Description;
        task.DueDate = request.DueDate;
        task.Priority = request.Priority;
        task.Status = request.Status;

        _taskRepository.Update(task);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var project = await _projectRepository.GetByIdAsync(task.ProjectId);

        var dto = _mapper.Map<TaskDetailDto>(task);
        dto.ProjectName = project?.Name ?? string.Empty;

        return Result.Success(dto);
    }
}
