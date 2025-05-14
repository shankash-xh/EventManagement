using MediatR;

namespace EventManagement.Application.Request.Event;

public class DeleteEventRequest : IRequest<bool>
{
    public int Id { get; set; }
}
