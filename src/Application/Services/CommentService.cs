using Application.Interfaces;
using Application.Models.Requests.Comments;
using Application.Models.Responses;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IVehicleService _vehicleService;
        private readonly IUserService _userService;
        public CommentService(ICommentRepository commentRepository, IVehicleService vehicleService, IUserService userService)
        {
            _commentRepository = commentRepository;
            _vehicleService = vehicleService;
            _userService = userService;
        }

        public Comment? AddComment(int userId,int vehicleId, CreateCommentDTO commentDTO)
        {

            VehicleDTO? vehicle = _vehicleService.GetVehicleById(vehicleId);

            if (vehicle == null || vehicle.State != VehicleState.Active)
            {
                return null;
            }


            Comment newComment = new Comment();
            newComment.UserId = userId;
            newComment.VehicleId = vehicleId;
            newComment.Text = commentDTO.Text;  
            _commentRepository.Add(newComment);
            return newComment;
        }

        public List<Comment>? GetCommentsByVehicleId(int vehicleId)
        {
            var comments = _commentRepository.GetCommentsByVehicleId(vehicleId);
            return comments.ToList();
        }

        public Comment? GetComment(int id)
        {
           Comment? comment = _commentRepository.GetById(id);
            return comment;
        }

        public Comment? UpdateComment(int commentId, string comment, int userId)
        {
            Comment? commentToUpdate = _commentRepository.GetById(commentId);
            if (commentToUpdate == null) return null;
            VehicleDTO? vehicle = _vehicleService.GetVehicleById(commentToUpdate.VehicleId);
            var user = _userService.GetUserById(userId);
            if (user == null) return null;
            if (commentToUpdate == null || vehicle == null || user == null)
            {
                return null;
            }

            if(user.Id == commentToUpdate.UserId || user.Rol == UserRol.SysAdmin || user.Rol == UserRol.Moderator)
            {
                commentToUpdate.Text = comment;
                _commentRepository.Update(commentToUpdate);
                return commentToUpdate;
            }
            return null;
        }

        public bool DeleteComment(int userId, int commentId)
        {
            var comment = _commentRepository.GetById(commentId);
            if (comment == null) 
                return false;
            var vehicle = _vehicleService.GetVehicleById(comment.VehicleId);
            if((comment != null && comment.UserId == userId) || vehicle != null && userId == vehicle.SellerId)
            {
                _commentRepository.Delete(comment);
                return true;
            }
            return false;
        }

    }
}
