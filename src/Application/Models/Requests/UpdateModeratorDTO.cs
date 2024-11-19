using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Requests
{
    public class UpdateModeratorDTO
    {
        public int Id { get; set; }
        
        public string FullName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Location { get; set; }

    }
}
