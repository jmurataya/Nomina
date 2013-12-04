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
    public class Impuesto : XPObject
    {
        public Impuesto(Session session)
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
        private double _CuotaFija;
        public double CuotaFija
        {
            get { return _CuotaFija; }
            set
            {
                _CuotaFija = value;
            }
        }
        private double _Porcentaje;
        public double Porcentaje
        {
            get { return _Porcentaje; }
            set
            {
                _Porcentaje = value;
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
