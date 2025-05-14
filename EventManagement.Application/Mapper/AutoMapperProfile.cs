using AutoMapper;
using EventManagement.Application.Request.Booking;
using EventManagement.Application.Request.Event;
using EventManagement.Application.Responce;
using EventManagement.Domain.Entity;

namespace EventManagement.Application.Mapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<User, UserResponce>().ReverseMap();
        CreateMap<Event,EventReposnce>().ReverseMap();
        CreateMap<Booking, BookingResponce>().ReverseMap();
        CreateMap<Booking, AddBookingRequest>().ReverseMap();
        CreateMap<AddEventRequest, Event>().ReverseMap();
    }
}
