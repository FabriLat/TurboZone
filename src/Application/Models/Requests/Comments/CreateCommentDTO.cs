﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Requests.Comments
{
    public class CreateCommentDTO
    {
        public int VehicleId { get; set; }

        public string Text { get; set; }
    }
}
