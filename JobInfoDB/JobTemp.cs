//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace JobInfoDB
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    
    public partial class JobTemp
    {
        public int JobTempID { get; set; }
        public string IDJobUrl { get; set; }
        public string JobName { get; set; }
        public string Salary { get; set; }
        public string Benefit { get; set; }
        public string JobLanguage { get; set; }
    
        public Nullable<System.DateTime> DueDate { get; set; }
       
        public Nullable<System.DateTime> PostDate { get; set; }
        public string JobDescription { get; set; }
        public string Requirement { get; set; }
        public string IDCompany { get; set; }
        public string Location { get; set; }
        public string IDProcessState { get; set; }
    
        public virtual Company Company { get; set; }
        public virtual JobUrl JobUrl { get; set; }
        public virtual ProcessState ProcessState { get; set; }
    }
}
