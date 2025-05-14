using AutoMapper;
using EventManagement.Application.Interface;
using EventManagement.Application.Request.Event;
using EventManagement.Application.Responce;
using EventManagement.Domain.Entity;
using EventManagement.Shared.GlobalResponce;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventController(IUnitOfWork unitOfWork, IMapper mapper) : BaseController
{
    public readonly IUnitOfWork _unitOfWork = unitOfWork;
    public readonly IMapper _mapper = mapper;

    [HttpGet("get-all-events")]
    [Authorize(Roles = "Organizers,User")]
    public async Task<Result<List<EventReposnce>>> GetAllEvents([FromQuery] GetAllEventsRequest eventRequest)
    {
      return await Mediator.Send(eventRequest);
    }

    [HttpGet("get-event-by-id/{id}")]
    [Authorize(Roles = "Organizers,User")]
    public async Task<Result<EventReposnce>> GetEventById(int id)
    {
       return await Mediator.Send(new GetEventByIdRequest { Id = id });
    }

    [HttpPost("add-event")]
    [Authorize(Roles = "Organizers")]
    public async Task<Result<string>> AddEvent([FromBody] AddEventRequest eventRequest)
    {
        return await Mediator.Send(eventRequest);
    }
    [HttpPut("update-event")]
    [Authorize(Roles = "Organizers")]
    public async Task<Result<string>> UpdateEvent([FromBody] UpdateEventRequest eventRequest)
    {

        return await Mediator.Send(eventRequest);
    }
    [HttpDelete("delete-event/{id}")]
    [Authorize(Roles = "Organizers")]
    public async Task<Result<string>> DeleteEvent(int id)
    {
       return await Mediator.Send(new DeleteEventRequest { Id = id });
    }
}
