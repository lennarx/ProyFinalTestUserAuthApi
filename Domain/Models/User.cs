using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class User : Entity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public byte[] Password { get; set; }
        public bool IsActive { get; set; }
    }
}
