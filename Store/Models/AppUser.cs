using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Models
{
    public class AppUser:IdentityUser
    {

        public string FullName { get; set; }
        public string FaceBook { get; set; }
        public string Telegram { get; set; }
    }
}
