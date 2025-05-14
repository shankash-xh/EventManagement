using EventManagement.Shared.GlobalResponce;
using MediatR;

namespace EventManagement.Application.Request.Event;

public class DeleteEventRequest : IRequest<Result<string>>
{
    public int Id { get; set; }
}
