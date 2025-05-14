using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Responce;

public class UserResponce
{
    public string Token { get; set; }
    public string RefeshToken { get; set; }
}

