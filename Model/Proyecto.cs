//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Proyecto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Proyecto()
        {
            this.ProyectoEmpleado = new HashSet<ProyectoEmpleado>();
        }
    
        public int IdProyecto { get; set; }
        public int IdCliente { get; set; }
        public string Descripcion { get; set; }
        public System.DateTime FechaInicio { get; set; }
        public Nullable<System.DateTime> FechaFinal { get; set; }
        public Nullable<short> HorasAsignada { get; set; }
        public bool Vigente { get; set; }
        public short Estado { get; set; }
    
        public virtual Cliente Cliente { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProyectoEmpleado> ProyectoEmpleado { get; set; }
    }
}
