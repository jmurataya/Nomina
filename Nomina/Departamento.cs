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
    public class Departamento : XPObject
    {
        public Departamento(Session session)
            : base(session)
        { }
        private string _Dependencia;
        public string Dependencia
        {
            get { return _Dependencia; }
            set
            {
                _Dependencia = value;
            }
        }
        private string _Descripcion;
        public string Descripcion
        {
            get { return _Descripcion; }
            set
            {
                _Descripcion = value;
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
        private CuentaContable _cuentacontable;
        [Association("CuentaContable-Departamentos")]
        public CuentaContable CuentaContable
        {
            get { return _cuentacontable; }
            set { SetPropertyValue("CuentaContable", ref _cuentacontable, value); }
        }
        [Association("Departamento-Empleados")]
        public XPCollection<Empleado> Empleados { get { return GetCollection<Empleado>("Empleados"); } }

    }
}
