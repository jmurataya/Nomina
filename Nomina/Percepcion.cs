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
    public class Percepcion : XPObject
    {
        public Percepcion(Session session)
            : base(session)
        { }
        private Empleado _Empleado;
        [Association("Empleado-Percepcions")]
        public Empleado Empleado
        {
            get { return _Empleado; }
            set { SetPropertyValue("Empleado", ref _Empleado, value); }
        }
        private Concepto _Concepto;
        [Association("Concepto-Percepcions")]
        public Concepto Concepto
        {
            get { return _Concepto; }
            set { SetPropertyValue("Concepto", ref _Concepto, value); }
        }
        private decimal _Importe;
        public decimal Importe
        {
            get { return _Importe; }
            set
            {
                _Importe = value;
            }
        }
        private string _Referencia;
        public string Referencia
        {
            get { return _Referencia; }
            set
            {
                _Referencia = value;
            }
        }
        private string _Referencia1;
        public string Referencia1
        {
            get { return _Referencia1; }
            set
            {
                _Referencia1 = value;
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
    }
}
