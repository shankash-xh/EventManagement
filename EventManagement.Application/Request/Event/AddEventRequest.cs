﻿using EventManagement.Application.Responce;
using EventManagement.Shared.GlobalResponce;
using MediatR;

namespace EventManagement.Application.Request.Event;

public class AddEventRequest : IRequest<Result<string>>
{
    public string? Name { get; set; }
    public string? OrganizerId { get; set; }
    public string? Location { get; set; }
    public DateTime Time { get; set; }
    public int Capacity { get; set; }
    public bool IsPrivate { get; set; }
}
