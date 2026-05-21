using AutoMapper;
using MediatR;
using TaskFlow.Application.Common;
using TaskFlow.Application.Features.Admin.Projects.DTOs;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.Application.Features.Admin.Projects.Commands.UpdateProject;

public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, Result<ProjectDetailDto>>
{
    private readonly IRepository<Project> _projectRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateProjectCommandHandler(IRepository<Project> projectRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _projectRepository = projectRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<ProjectDetailDto>> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdAsync(request.Id);
        if (project is null)
            return Result.Failure<ProjectDetailDto>("Project not found", 404);

        project.Name = request.Name;
        project.Description = request.Description;

        _projectRepository.Update(project);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var taskCount = _projectRepository.Query()
            .Where(p => p.Id == project.Id)
            .Select(p => p.Tasks.Count)
            .FirstOrDefault();

        var dto = _mapper.Map<ProjectDetailDto>(project);
        dto.TaskCount = taskCount;

        return Result.Success(dto);
    }
}
