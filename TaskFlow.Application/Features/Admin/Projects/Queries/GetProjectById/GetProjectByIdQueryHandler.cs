using AutoMapper;
using MediatR;
using TaskFlow.Application.Common;
using TaskFlow.Application.Features.Admin.Projects.DTOs;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.Application.Features.Admin.Projects.Queries.GetProjectById;

public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, Result<ProjectDetailDto>>
{
    private readonly IRepository<Project> _projectRepository;
    private readonly IMapper _mapper;

    public GetProjectByIdQueryHandler(IRepository<Project> projectRepository, IMapper mapper)
    {
        _projectRepository = projectRepository;
        _mapper = mapper;
    }

    public async Task<Result<ProjectDetailDto>> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdAsync(request.Id);
        if (project is null)
            return Result.Failure<ProjectDetailDto>("Project not found", 404);

        var taskCount = _projectRepository.Query()
            .Where(p => p.Id == project.Id)
            .Select(p => p.Tasks.Count)
            .FirstOrDefault();

        var dto = _mapper.Map<ProjectDetailDto>(project);
        dto.TaskCount = taskCount;

        return Result.Success(dto);
    }
}
