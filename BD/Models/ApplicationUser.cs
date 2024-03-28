using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BD.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Lastname { get; set; }
        public ICollection<Product> Products { get; } = new List<Product>();
    }
}
