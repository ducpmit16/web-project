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
    
    public partial class Event
    {
        public int id_event { get; set; }
        public string name_event { get; set; }
        public Nullable<System.DateTime> date_start { get; set; }
        public Nullable<System.DateTime> date_end { get; set; }
        public Nullable<System.TimeSpan> time_event { get; set; }
        public string location { get; set; }
        public string image_event { get; set; }
    }
}
