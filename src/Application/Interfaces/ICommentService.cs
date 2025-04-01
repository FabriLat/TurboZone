using Application.Models.Requests.Comments;
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
        Comment? AddComment(int userId, CreateCommentDTO commentDTO);

        Comment? GetComment(int id);

        Comment? UpdateComment(int commentId, string comment, int userId);

        bool DeleteComment(int userId, int commentId);

        List<Comment>? GetCommentsByVehicleId(int vehicleId);
    }
}
