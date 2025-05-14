using AutoMapper;
using EventManagement.Application.Interface;
using EventManagement.Application.Request.Event;
using EventManagement.Domain.Entity;
using EventManagement.Shared.GlobalResponce;
using MediatR;

namespace EventManagement.Application.Features.Events.Command;

public class UpdateEventCommandHandler : IRequestHandler<UpdateEventRequest, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public UpdateEventCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<Result<string>> Handle(UpdateEventRequest eventRequest, CancellationToken cancellationToken)
    {

        await _unitOfWork.Events.UpdateAsync(_mapper.Map<Event>(eventRequest));
        await _unitOfWork.SaveAsync();
        return Result<string>.Success("Event Updated Successfully");
    }
}
