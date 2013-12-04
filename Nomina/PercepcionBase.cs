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
    public enum TipoBase { Diario, Importe }
    public class PercepcionBase : XPObject
    {
        public PercepcionBase(Session session)
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
        private TipoBase _TipoPercepcionBase;
        public TipoBase TipoPercepcionBase
        {
            get { return _TipoPercepcionBase; }
            set
            {
                _TipoPercepcionBase = value;
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
        [Association("CuentaContable-PercepcionBases")]
        public CuentaContable CuentaContable
        {
            get { return _cuentacontable; }
            set { SetPropertyValue("CuentaContable", ref _cuentacontable, value); }
        }
        [Association("PercepcionBase-ConceptoDetalles")]
        public XPCollection<ConceptoDetalle> ConceptoDetalles { get { return GetCollection<ConceptoDetalle>("ConceptoDetalles"); } }

    }
}
