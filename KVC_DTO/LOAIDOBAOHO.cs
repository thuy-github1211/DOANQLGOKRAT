//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KVC_DTO
{
    using System;
    using System.Collections.Generic;
    
    public partial class LOAIDOBAOHO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LOAIDOBAOHO()
        {
            this.CTHDDOBAOHOes = new HashSet<CTHDDOBAOHO>();
            this.DOBAOHOes = new HashSet<DOBAOHO>();
        }
    
        public string MALOAIDBH { get; set; }
        public string TENLOAIDBH { get; set; }
        public Nullable<double> DONGIA { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTHDDOBAOHO> CTHDDOBAOHOes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DOBAOHO> DOBAOHOes { get; set; }
    }
}
