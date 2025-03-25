using Application.Interfaces;
using Application.Models.Requests;
using Domain.Entities;
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
        public CommentService(ICommentRepository commentRepository, IVehicleService vehicleService)
        {
            _commentRepository = commentRepository;
            _vehicleService = vehicleService;
        }

        public Comment? AddComment(int userId, CreateCommentDTO commentDTO)
        {
           Comment newComment = new Comment();
            newComment.UserId = userId;
            newComment.VehicleId = commentDTO.VehicleId;
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
