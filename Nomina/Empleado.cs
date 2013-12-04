using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using System.Collections;
using System.Text.RegularExpressions;
using System.Drawing;
using DevExpress.Persistent.BaseImpl;

namespace Nomina
{
    public enum EmpleadoEstado { Activo, Baja, Permiso, Incapacitado }
    public enum EmpleadoServicioMedico { Ninguno, Isssteson, Imss, RedBenefit, Otro }
    public enum EmpleadoSexo { Masculino, Femenino }
    public enum EmpleadoEstadoCivil { Soltero, Casado, UnionLibre, Divorciado, Otro }
    public class Empleado : XPObject
    {
        public Empleado(Session session)
            : base(session)
        { }
        private string _ApellidoPaterno;
        public string ApellidoPaterno
        {
            get { return _ApellidoPaterno; }
            set
            {
                _ApellidoPaterno = value;
            }
        }
        private string _ApellidoMaterno;
        public string ApellidoMaterno
        {
            get { return _ApellidoMaterno; }
            set
            {
                _ApellidoMaterno = value;
            }
        }
        private string _Nombre;
        public string Nombre
        {
            get { return _Nombre; }
            set
            {
                _Nombre = value;
            }
        }
        private string _NombreCompleto;
        [NonPersistent]
        public string NombreCompleto
        {
            get 
            { return ApellidoPaterno+" "+ApellidoMaterno+" "+Nombre; }
            set
            {
                _NombreCompleto = value;
            }
        }
        
        private EmpleadoEstado _Estado;
        public EmpleadoEstado Estado
        {
            get { return _Estado; }
            set
            {
                _Estado = value;
            }
        }
        private EmpleadoServicioMedico _ServicioMedico;
        public EmpleadoServicioMedico ServicioMedico
        {
            get { return _ServicioMedico; }
            set
            {
                _ServicioMedico = value;
            }
        }
        private string _NumeroServicioMedico;
        public string NumeroServicioMedico
        {
            get { return _NumeroServicioMedico; }
            set
            {
                _NumeroServicioMedico = value;
            }
        }
        private string _Rfc;
        public string Rfc
        {
            get { return _Rfc; }
            set
            {
                _Rfc = value;
            }
        }
        private string _Curp;
        public string Curp
        {
            get { return _Curp; }
            set
            {
                _Curp = value;
            }
        }
        private EmpleadoSexo _Sexo;
        public EmpleadoSexo Sexo
        {
            get { return _Sexo; }
            set
            {
                _Sexo = value;
            }
        }
        private DateTime _FechaDeNacimiento;
        public DateTime FechaDeNacimiento
        {
            get { return _FechaDeNacimiento; }
            set
            {
                _FechaDeNacimiento = value;
            }
        }
        private EmpleadoEstadoCivil _EstadoCivil;
        public EmpleadoEstadoCivil EstadoCivil
        {
            get { return _EstadoCivil; }
            set
            {
                _EstadoCivil = value;
            }
        }
        private string _Direccion;
        public string Direccion
        {
            get { return _Direccion; }
            set
            {
                _Direccion = value;
            }
        }
        private DateTime _FechaIngreso;
        public DateTime FechaIngreso
        {
            get { return _FechaIngreso; }
            set
            {
                _FechaIngreso = value;
            }
        }
        private string _TarjetaBancaria;
        public string TarjetaBancaria
        {
            get { return _TarjetaBancaria; }
            set
            {
                _TarjetaBancaria = value;
            }
        }
        private XPCollection<AuditDataItemPersistent> auditoria;
        public XPCollection<AuditDataItemPersistent> Auditoria
        {
            get
            {
                if (auditoria == null)
                {
                    auditoria = AuditedObjectWeakReference.GetAuditTrail(Session, this);
                }
                return auditoria;
            }
        }
        private Departamento _Departamento;
        [Association("Departamento-Empleados")]
        public Departamento Departamento
        {
            get { return _Departamento; }
            set { SetPropertyValue("Departamento", ref _Departamento, value); }
        }
        private TipoNomina _TipoNomina;
        [Association("TipoNomina-Empleados")]
        public TipoNomina TipoNomina
        {
            get { return _TipoNomina; }
            set { SetPropertyValue("TipoNomina", ref _TipoNomina, value); }
        }
        private Puesto _Puesto;
        [Association("Puesto-Empleados")]
        public Puesto Puesto
        {
            get { return _Puesto; }
            set { SetPropertyValue("Puesto", ref _Puesto, value); }
        }
        private TipoEmpleado _TipoEmpleado;
        [Association("TipoEmpleado-Empleados")]
        public TipoEmpleado TipoEmpleado
        {
            get { return _TipoEmpleado; }
            set { SetPropertyValue("TipoEmpleado", ref _TipoEmpleado, value); }
        }
        private GrupoEmpleado _GrupoEmpleado;
        [Association("GrupoEmpleado-Empleados")]
        public GrupoEmpleado GrupoEmpleado
        {
            get { return _GrupoEmpleado; }
            set { SetPropertyValue("GrupoEmpleado", ref _GrupoEmpleado, value); }
        }
        [Association("Empleado-Percepcions")]
        public XPCollection<Percepcion> Percepcions { get { return GetCollection<Percepcion>("Percepcions"); } }

        [Association("Empleado-Deduccions")]
        public XPCollection<Deduccion> Deduccions { get { return GetCollection<Deduccion>("Deduccions"); } }
    }
}
