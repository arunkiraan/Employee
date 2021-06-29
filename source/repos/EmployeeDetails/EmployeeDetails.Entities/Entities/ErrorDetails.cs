﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace EmployeeDetails.Entities.Entities
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
