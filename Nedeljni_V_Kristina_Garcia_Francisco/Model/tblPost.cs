//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Nedeljni_V_Kristina_Garcia_Francisco.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblPost
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblPost()
        {
            this.tblPostLikes = new HashSet<tblPostLike>();
        }
    
        public int PostID { get; set; }
        public System.DateTime DateOfPost { get; set; }
        public string PostText { get; set; }
        public int NumberOfLikes { get; set; }
        public int UserID { get; set; }
    
        public virtual tblUser tblUser { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblPostLike> tblPostLikes { get; set; }
    }
}
