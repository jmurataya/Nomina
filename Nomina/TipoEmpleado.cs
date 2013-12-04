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
    public class TipoEmpleado : XPObject
    {
        public TipoEmpleado(Session session)
            : base(session)
        { }
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
        [Association("TipoEmpleado-Empleados")]
        public XPCollection<Empleado> Empleados { get { return GetCollection<Empleado>("Empleados"); } }       
    }
}