//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Conference_Organization_App.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Users
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Users()
        {
            this.Schedule = new HashSet<Schedule>();
            this.Schedule1 = new HashSet<Schedule>();
            this.Schedule2 = new HashSet<Schedule>();
            this.Schedule3 = new HashSet<Schedule>();
            this.Schedule4 = new HashSet<Schedule>();
            this.Schedule5 = new HashSet<Schedule>();
        }
    
        public int id { get; set; }
        public int Role_Id { get; set; }
        public int Gender_Id { get; set; }
        public int Country_Id { get; set; }
        public string Lastname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Email { get; set; }
        public System.DateTime Birth { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string Photo_Name { get; set; }
        public byte[] Photo_Image { get; set; }
        public Nullable<int> UserDirection_Id { get; set; }
        public Nullable<int> UserEventDirection_Id { get; set; }
    
        public virtual Countries Countries { get; set; }
        public virtual Genders Genders { get; set; }
        public virtual Roles Roles { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Schedule> Schedule { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Schedule> Schedule1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Schedule> Schedule2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Schedule> Schedule3 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Schedule> Schedule4 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Schedule> Schedule5 { get; set; }
        public virtual UserEventDirections UserEventDirections { get; set; }
        public virtual UsersDirections UsersDirections { get; set; }
    }
}
