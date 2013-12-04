using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using System.Collections;
using System.Text.RegularExpressions;
using System.Drawing;
using DevExpress.Persistent.AuditTrail;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo.DB;

namespace Nomina
{
    public enum tipocuenta { Departamento, Percepcion, Deduccion, Prestamo, Pension };
    public class CuentaContable : XPObject
    {
        public CuentaContable(Session session)
            : base(session)
        { }
        private tipocuenta _TipoCuenta;
        public tipocuenta TipoCuenta
        {            get { return _TipoCuenta; }
            set
            {
                _TipoCuenta = value;
            }
        }
        [Indexed("Cuenta")]
        private string _Cuenta;
        public string Cuenta
        {
            get { return _Cuenta; }
            set
            {
                _Cuenta = value;
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
        [Association("CuentaContable-Departamentos")]
        public XPCollection<Departamento> Departamentos { get { return GetCollection<Departamento>("Departamentos"); } }
        [Association("CuentaContable-Conceptos")]
        public XPCollection<Concepto> Conceptos { get { return GetCollection<Concepto>("Conceptos"); } }
        [Association("CuentaContable-PercepcionBases")]
        public XPCollection<PercepcionBase> PercepcionBases { get { return GetCollection<PercepcionBase>("PercepcionBases"); } }
        [Association("CuentaContable-Deduccions")]
        public XPCollection<Deduccion> Deduccions { get { return GetCollection<Deduccion>("Deduccions"); } }
         
    }
}
