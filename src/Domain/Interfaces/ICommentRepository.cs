﻿using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICommentRepository : IRepositoryBase<Comment>
    {
        List<Comment>? GetCommentsByVehicleId(int vehicleId);
    }
}
