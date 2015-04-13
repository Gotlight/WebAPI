using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Reservation
    {
        public string OrganizationID { get; set; }
        public int PersonsCount { get; set; }
        public string AspNetUserID { get; set; }
        public System.DateTime ReservationDateTime { get; set; }
    }
}