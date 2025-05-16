using AutoMapper;
using EventManagement.Application.Interface;
using EventManagement.Application.Request.Event;
using EventManagement.Application.Responce;
using EventManagement.Domain.Entity;
using EventManagement.Shared.GlobalResponce;
using MediatR;

namespace EventManagement.Application.Features.Events.Query;

public class GetAllEventsHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetAllEventsRequest, Result<List<EventReposnce>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    public async Task<Result<List<EventReposnce>>> Handle(GetAllEventsRequest eventRequest, CancellationToken cancellationToken)
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
            List<Event>? paginatedResult = [..events.Skip((pageNumber - 1) * pageSize).Take(pageSize)];
            if (paginatedResult == null || paginatedResult.Count == 0)
            {
                return Result<List<EventReposnce>>.Failure("No Bookings Found");
            }
            List<EventReposnce> k = _mapper.Map<List<EventReposnce>>(paginatedResult);
            return Result<List<EventReposnce>>.Success(k);
        }
        return Result<List<EventReposnce>>.Failure("No Bookings Found");
    }
}
