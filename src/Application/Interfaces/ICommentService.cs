using Application.Models.Requests;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICommentService
    {
        bool AddComment(int userId, CreateCommentDTO commentDTO);

        bool DeleteComment(int userId, int commentId);

        List<Comment>? GetCommentsByVehicleId(int vehicleId);
    }
}
