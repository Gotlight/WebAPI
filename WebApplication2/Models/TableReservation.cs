//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TableReservation
    {
        public System.Guid ID { get; set; }
        public System.Guid OrganizationID { get; set; }
        public int PersonsCount { get; set; }
        public string AspNetUserID { get; set; }
        public System.DateTime ReservationDateTime { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual Organization Organization { get; set; }
    }
}
