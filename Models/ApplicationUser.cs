using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MemberApi.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        /*
        public List<Dispensary> Dispensaries { get; set; }
        public List<ProducerProcessor> ProducerProcessors {get;set;}
        public List<TestingLab> TestingLabs { get; set; }
        */
    }
}
