//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication1.Model.DBContext
{
    using System;
    using System.Collections.Generic;
    
    public partial class users
    {
        public string role { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string address { get; set; }
        public string contact { get; set; }
        public Nullable<int> stateId { get; set; }
        public Nullable<int> cityId { get; set; }
        public int userId { get; set; }
    
        public virtual cities cities { get; set; }
        public virtual states states { get; set; }
    }
}
