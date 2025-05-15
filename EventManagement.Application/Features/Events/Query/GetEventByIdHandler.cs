using AutoMapper;
using EventManagement.Application.Interface;
using EventManagement.Application.Request.Event;
using EventManagement.Application.Responce;
using EventManagement.Domain.Entity;
using EventManagement.Shared.GlobalResponce;
using MediatR;

namespace EventManagement.Application.Features.Events.Query;

public class GetEventByIdHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetEventByIdRequest, Result<EventReposnce>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<EventReposnce>> Handle(GetEventByIdRequest request, CancellationToken cancellationToken)
    {
        Event? eventEntity = await _unitOfWork.Events.GetByIdAsync(request.Id);
        if (eventEntity != null)
        {
            EventReposnce k = _mapper.Map<EventReposnce>(eventEntity);
            return Result<EventReposnce>.Success(k);
        }
        return Result<EventReposnce>.Failure("No Bookings Found");
    }
}
