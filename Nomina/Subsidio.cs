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
    public class Subsidio : XPObject
    {
        public Subsidio(Session session)
            : base(session)
        { }
        private double _LimiteInferior;
        public double LimiteInferior
        {
            get { return _LimiteInferior; }
            set
            {
                _LimiteInferior = value;
            }
        }
        private double _LimiteSuperior;
        public double LimiteSuperior
        {
            get { return _LimiteSuperior; }
            set
            {
                _LimiteSuperior = value;
            }
        }
        private double _Credito;
        public double Credito
        {
            get { return _Credito; }
            set
            {
                _Credito = value;
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
