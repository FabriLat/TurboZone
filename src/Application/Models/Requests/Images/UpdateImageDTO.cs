﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Requests.Images
{
    public class UpdateImageDTO
    {
        public string NewImageName { get; set; }

        public string NewImageUrl { get; set; }
    }
}
