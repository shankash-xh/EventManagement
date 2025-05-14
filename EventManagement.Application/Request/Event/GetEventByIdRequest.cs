using EventManagement.Application.Responce;
using EventManagement.Shared.GlobalResponce;
using MediatR;

namespace EventManagement.Application.Request.Event;

public class GetEventByIdRequest : IRequest<Result<EventReposnce>>
{
    public int Id { get; set; }
}
