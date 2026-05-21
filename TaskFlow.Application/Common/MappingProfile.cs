using AutoMapper;
using TaskFlow.Application.Features.Admin.Projects.DTOs;
using TaskFlow.Application.Features.Admin.Tasks.DTOs;
using TaskFlow.Application.Features.Shared.Auth.DTOs;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Application.Common;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Project
        CreateMap<Project, ProjectListDto>()
            .ForMember(d => d.TaskCount, opt => opt.MapFrom(s => s.Tasks.Count));

        CreateMap<Project, ProjectDetailDto>()
            .ForMember(d => d.TaskCount, opt => opt.MapFrom(s => s.Tasks.Count));

        // Task
        CreateMap<TaskItem, TaskListDto>()
            .ForMember(d => d.Status, opt => opt.MapFrom(s => s.Status.ToString()))
            .ForMember(d => d.Priority, opt => opt.MapFrom(s => s.Priority.ToString()))
            .ForMember(d => d.ProjectName, opt => opt.MapFrom(s => s.Project.Name));

        CreateMap<TaskItem, TaskDetailDto>()
            .ForMember(d => d.Status, opt => opt.MapFrom(s => s.Status.ToString()))
            .ForMember(d => d.Priority, opt => opt.MapFrom(s => s.Priority.ToString()))
            .ForMember(d => d.ProjectName, opt => opt.MapFrom(s => s.Project.Name));

        // User
        CreateMap<User, UserDto>()
            .ForMember(d => d.Roles, opt => opt.Ignore());
    }
}
