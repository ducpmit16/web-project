//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebSiteSushi.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Reservation
    {
        public int BookID { get; set; }
        public Nullable<System.DateTime> BookDate { get; set; }
        public Nullable<System.TimeSpan> BookTime { get; set; }
        public Nullable<int> NumberOfPeople { get; set; }
        public string NameBookedPeople { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string MoreRequest { get; set; }
        public Nullable<bool> Status { get; set; }
    }
}
