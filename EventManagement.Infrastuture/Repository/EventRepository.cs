using EventManagement.Application.Interface;
using EventManagement.Domain.Entity;
using EventManagement.Infrastuture.DataBase;

namespace EventManagement.Infrastuture.Repository;

public class EventRepository(AppDbContext context) : GenricRepositrory<Event>(context), IEventRepository
{

}
