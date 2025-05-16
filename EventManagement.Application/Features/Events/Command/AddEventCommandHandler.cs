using AutoMapper;
using EventManagement.Application.Interface;
using EventManagement.Application.Request.Event;
using EventManagement.Application.Responce;
using EventManagement.Domain.Entity;
using EventManagement.Shared.GlobalResponce;
using MediatR;

namespace EventManagement.Application.Features.Events.Command;

public class AddEventCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<AddEventRequest, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<string>> Handle(AddEventRequest eventRequest, CancellationToken cancellationToken)
    {
        Event eventEntity = _mapper.Map<Event>(eventRequest);
        await _unitOfWork.Events.AddAsync(eventEntity);
        await _unitOfWork.SaveAsync();
        return Result<string>.Success("Event Added Successfully");
    }
}
