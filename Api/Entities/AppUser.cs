using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Entities
{
    public class AppUser
    {
        public int Id{ get; set; }
        public string UserName { get; set; }
        public byte[] HashPassword { get; set; }
        public byte[] SaltHashPassword { get; set; }
    }
}