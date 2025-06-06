﻿using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement.API.Controllers;


public abstract class BaseController : ControllerBase
{
    private IMediator? _mediator;
    protected IMediator Mediator => _mediator = HttpContext.RequestServices.GetService<IMediator>()!;
}
