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
        IEnumerable<Event>? events = await _unitOfWork.Events.GetAllAsync();
        string? filterOn = eventRequest.FilterOn;
        string? filterQuery = eventRequest.FilterQuery;
        string? sortOn = eventRequest.SortOn;
        bool isAscending = eventRequest.IsAscending;
        int pageNumber = eventRequest.PageNumber;
        int pageSize = eventRequest.PageSize;
        if (events != null)
        {
            // Filtering
            if (!string.IsNullOrEmpty(filterOn) && !string.IsNullOrEmpty(filterQuery))
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    events = events.Where(x => x.Name.ToString().Equals(filterQuery));
                }
                if (filterOn.Equals("OrganizerId", StringComparison.OrdinalIgnoreCase))
                {
                    events = events.Where(x => x.OrganizerId.ToString().Equals(filterQuery));
                }
                if (filterOn.Equals("Location", StringComparison.OrdinalIgnoreCase))
                {
                    events = events.Where(x => x.Location.ToString().Equals(filterQuery));
                }
                if (filterOn.Equals("Time", StringComparison.OrdinalIgnoreCase))
                {
                    events = events.Where(x => x.Time.ToString().Equals(filterQuery));
                }
                if (filterOn.Equals("Capacity", StringComparison.OrdinalIgnoreCase))
                {
                    events = events.Where(x => x.Capacity.ToString().Equals(filterQuery));
                }
                if (filterOn.Equals("IsPrivate", StringComparison.OrdinalIgnoreCase))
                {
                    events = events.Where(x => x.IsPrivate.ToString().Equals(filterQuery));
                }
            }
            // Sorting
            if (!string.IsNullOrEmpty(sortOn))
            {
                if (sortOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    events = (isAscending) ? events.OrderBy(x => x.Name) : events.OrderByDescending(x => x.Name);
                }
                if (sortOn.Equals("OrganizerId", StringComparison.OrdinalIgnoreCase))
                {
                    events = (isAscending) ? events.OrderBy(x => x.OrganizerId) : events.OrderByDescending(x => x.OrganizerId);
                }
                if (sortOn.Equals("Location", StringComparison.OrdinalIgnoreCase))
                {
                    events = (isAscending) ? events.OrderBy(x => x.Location) : events.OrderByDescending(x => x.Location);
                }
                if (sortOn.Equals("Time", StringComparison.OrdinalIgnoreCase))
                {
                    events = (isAscending) ? events.OrderBy(x => x.Time) : events.OrderByDescending(x => x.Time);
                }
                if (sortOn.Equals("Capacity", StringComparison.OrdinalIgnoreCase))
                {
                    events = (isAscending) ? events.OrderBy(x => x.Capacity) : events.OrderByDescending(x => x.Capacity);
                }
                if (sortOn.Equals("IsPrivate", StringComparison.OrdinalIgnoreCase))
                {
                    events = (isAscending) ? events.OrderBy(x => x.IsPrivate) : events.OrderByDescending(x => x.IsPrivate);
                }
            }
            // Pagination
            var paginatedResult = events.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            if (paginatedResult == null || paginatedResult.Count == 0)
            {
                return Result<List<EventReposnce>>.Failure("No Bookings Found");
            }
            List<EventReposnce> k = _mapper.Map<List<EventReposnce>>(paginatedResult);
            return Result<List<EventReposnce>>.Success(k);
        }
        return Result<List<EventReposnce>>.Failure("No Bookings Found");
    }

    [HttpGet("get-event-by-id/{id}")]
    [Authorize(Roles = "Organizers,User")]
    public async Task<Result<EventReposnce>> GetEventById(int id)
    {
        Event? eventEntity = await _unitOfWork.Events.GetByIdAsync(id);
        if (eventEntity != null)
        {
            EventReposnce k = _mapper.Map<EventReposnce>(eventEntity);
            return Result<EventReposnce>.Success(k);
        }
        return Result<EventReposnce>.Failure("No Bookings Found");
    }

    [HttpPost("add-event")]
    [Authorize(Roles = "Organizers")]
    public async Task<Result<string>> AddEvent([FromBody] AddEventRequest eventRequest)
    {
        Event eventEntity = _mapper.Map<Event>(eventRequest);
        await _unitOfWork.Events.AddAsync(eventEntity);
        await _unitOfWork.SaveAsync();
        return Result<string>.Success("Event Added Successfully");
    }
    [HttpPut("update-event/{id}")]
    [Authorize(Roles = "Organizers")]
    public async Task<Result<string>> UpdateEvent(int id, [FromBody] UpdateEventRequest eventRequest)
    {

        await _unitOfWork.Events.UpdateAsync(_mapper.Map<Event>(eventRequest));
        await _unitOfWork.SaveAsync();
        return Result<string>.Success("Event Updated Successfully");

    }
    [HttpDelete("delete-event/{id}")]
    [Authorize(Roles = "Organizers")]
    public async Task<Result<string>> DeleteEvent(int id)
    {
        Event? eventEntity = await _unitOfWork.Events.GetByIdAsync(id);
        if (eventEntity != null)
        {
            await _unitOfWork.Events.DeleteAsync(eventEntity);
            await _unitOfWork.SaveAsync();
            return Result<string>.Success("Event Deleted Successfully");
        }
        return Result<string>.Failure("No Bookings Found");
    }
}
