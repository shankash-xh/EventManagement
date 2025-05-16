using EventManagement.Application.Interface;
using EventManagement.Application.Request.Event;
using EventManagement.Domain.Entity;
using EventManagement.Shared.GlobalResponce;
using MediatR;

namespace EventManagement.Application.Features.Events.Command;
public class DeleteEventComandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteEventRequest, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<string>> Handle(DeleteEventRequest request, CancellationToken cancellationToken)
    {
        Event? eventEntity = await _unitOfWork.Events.GetByIdAsync(request.Id);
        if (eventEntity != null)
        {
            await _unitOfWork.Events.DeleteAsync(eventEntity);
            await _unitOfWork.SaveAsync();
            return Result<string>.Success("Event Deleted Successfully");
        }
        return Result<string>.Failure("No Bookings Found");
    }
}
