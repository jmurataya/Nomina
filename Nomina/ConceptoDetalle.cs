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
    public class ConceptoDetalle : XPObject
    {
        public ConceptoDetalle(Session session)
            : base(session)
        { }
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
        private Concepto _concepto;
        [Association("Concepto-ConceptoDetalles")]
        public Concepto Concepto
        {
            get { return _concepto; }
            set { SetPropertyValue("Concepto", ref _concepto, value); }
        }
        private PercepcionBase _PercepcionBase;
        [Association("PercepcionBase-ConceptoDetalles")]
        public PercepcionBase PercepcionBase
        {
            get { return _PercepcionBase; }
            set { SetPropertyValue("PercepcionBase", ref _PercepcionBase, value); }
        }


    }
}
