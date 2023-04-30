using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MemberApi.Models.Services;

namespace MemberApi.Models.Members
{

    public class Roles
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class Contact
    {       
        public int ContactId { get; set; }

        // user Role from AspNetUser table from role manager 
        //or find userManager user by email
        private string _role;
        [NotMapped]
        public string Role { get { return _role; } set { _role = value; } }

        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public ContactStatus Status { get; set; }
    }

    

   
}

