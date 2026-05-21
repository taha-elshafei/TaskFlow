using MediatR;
using TaskFlow.Application.Common;

namespace TaskFlow.Application.Features.Admin.Tasks.Commands.DeleteTask;

public class DeleteTaskCommand : IRequest<Result>
{
    public Guid Id { get; set; }
}
