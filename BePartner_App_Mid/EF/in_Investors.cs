//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BePartner_App_Mid.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class in_Investors
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public in_Investors()
        {
            this.Ideas = new HashSet<Idea>();
            this.Reports = new HashSet<Report>();
        }
    
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public System.DateTime Dob { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Nid { get; set; }
        public string OrgName { get; set; }
        public System.DateTime OrgEstablished { get; set; }
        public string OrgLocation { get; set; }
        public string OrgEmail { get; set; }
        public string OrgPhone { get; set; }
        public string OrgLicense { get; set; }
        public string Tin { get; set; }
        public string OrgSite { get; set; }
        public string Password { get; set; }
        public string Status { get; set; }
        public string EmailValidation { get; set; }
        public string In_Email { get; set; }
        public string Gender { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Idea> Ideas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Report> Reports { get; set; }
    }
}
