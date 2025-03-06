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
        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public bool AddComment(int userId, CreateCommentDTO commentDTO)
        {
           Comment newComment = new Comment();
            newComment.UserId = userId;
            newComment.VehicleId = commentDTO.VehicleId;
            newComment.Text = commentDTO.Text;  
            _commentRepository.Add(newComment);
            return true;
        }

    }
}
