using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class CommentRepository : RepositoryBase<Comment>, ICommentRepository
    {
        public CommentRepository(ApplicationContext context) : base(context)
        {
        }

        public List<Comment>? GetCommentsByVehicleId(int vehicleId)
        {
            var appDbContext = (ApplicationContext)_dbContext;

            List<Comment> comments = appDbContext.Comments.Where(c => c.VehicleId == vehicleId).ToList();
            return comments;
        }
    }
}
