using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace MemberApi.Models.Services
{
    public class ProfileOptionsService
    {
        public List<State> ListStates()
        {
            return new List<State>() {
                new State("Alabama", "AL"),
                new State("Alaska", "AK"),
                new State("Arkansas", "AR"),
                new State("Arizona", "AZ"),
                new State("California", "CA"),
                new State("Colorado", "CO"),
                new State("Connecticut", "CT"),
                new State("D.C.", "DC"),
                new State("Delaware", "DE"),
                new State("Florida", "FL"),
                new State("Georgia", "GA"),
                new State("Hawaii", "HI"),
                new State("Iowa", "IA"),
                new State("Idaho", "ID"),
                new State("Illinois", "IL"),
                new State("Indiana", "IN"),
                new State("Kansas", "KS"),
                new State("Kentucky", "KY"),
                new State("Louisiana", "LA"),
                new State("Massachusetts", "MA"),
                new State("Maryland", "MD"),
                new State("Maine", "ME"),
                new State("Michigan", "MI"),
                new State("Minnesota", "MN"),
                new State("Missouri", "MO"),
                new State("Mississippi", "MS"),
                new State("Montana", "MT"),
                new State("North Carolina", "NC"),
                new State("North Dakota", "ND"),
                new State("Nebraska", "NE"),
                new State("New Hampshire", "NH"),
                new State("New Jersey", "NJ"),
                new State("New Mexico", "NM"),
                new State("Nevada", "NV"),
                new State("New York", "NY"),
                new State("Oklahoma", "OK"),
                new State("Ohio", "OH"),
                new State("Oregon", "OR"),
                new State("Pennsylvania", "PA"),
                new State("Rhode Island", "RI"),
                new State("South Carolina", "SC"),
                new State("South Dakota", "SD"),
                new State("Tennessee", "TN"),
                new State("Texas", "TX"),
                new State("Utah", "UT"),
                new State("Virginia", "VA"),
                new State("Vermont", "VT"),
                new State("Washington", "WA"),
                new State("Wisconsin", "WI"),
                new State("Wisconsin", "WI"),
                new State("West Virginia", "WV"),
                new State("Wyoming", "WY")};
         }

        public List<Membership> ListRoles()
        {
            return new List<Membership> {
                new Membership("Gold Member", "GoldMember"),
                new Membership("Silver Member", "SilverMember"),
                new Membership("Bronze Member", "BronzeMember")
            };
        }
    }

    public class State
    {
        public State(string _name, string _code)
        {
            Name = _name;
            Code = _code;
        }
        public string Name { get; set; }
        public string Code { get; set; }
    }

    public class Membership
    {
        public Membership(string _name, string _code)
        {
            Name = _name;
            Code = _code;
        }
        public string Name { get; set; }
        public string Code { get; set; }
    }



    public enum ContactStatus
    {
        Registered,
        Approved,
        Lockout
    }

    public enum CountryNames
    {
        UK,
        USA,
        France,
        China
    }

    public enum approved
    {
        Submitted,
        Approved,
        Rejected
    }

}

