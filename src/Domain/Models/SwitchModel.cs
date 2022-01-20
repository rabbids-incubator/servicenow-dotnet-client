﻿using System;
using System.Collections.Generic;

namespace RabbidsIncubator.ServiceNowClient.Domain.Models
{
    public class SwitchModel
    {
        public string? Id { get; set; }

        public string? Name { get; set; }

        public string? IpAddress { get; set; }

        public bool? IsStack { get; set; }

        public string? SerialNumber { get; set; }
    }
}
