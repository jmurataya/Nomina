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
    public enum ConceptoTipo {Percepcion, Deduccion, Prestamo, Pension }
    public enum ConceptoPeriodo { Unico, Fijo }
    public enum ConceptoPago { Importe, Diario, Horas, Complemento, Porcentaje }
    public class Concepto : XPObject
    {
        public Concepto(Session session)
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
        private ConceptoTipo _Tipo;
        public ConceptoTipo Tipo
        {
            get { return _Tipo; }
            set
            {
                _Tipo = value;
            }
        }
        private ConceptoPeriodo _Periodo;
        public ConceptoPeriodo Periodo
        {
            get { return _Periodo; }
            set
            {
                _Periodo = value;
            }
        }
        private ConceptoPago _Pago;
        public ConceptoPago pago
        {
            get { return _Pago; }
            set
            {
                _Pago = value;
            }
        }
        private decimal _Porcentaje;
        public decimal Porcentaje
        {
            get { return _Porcentaje; }
            set
            {
                _Porcentaje = value;
            }
        }
        private decimal _Maximo;
        public decimal Maximo
        {
            get { return _Maximo; }
            set
            {
                _Maximo = value;
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
        [Association("CuentaContable-Conceptos")]
        public CuentaContable CuentaContable
        {
            get { return _cuentacontable; }
            set { SetPropertyValue("CuentaContable", ref _cuentacontable, value); }
        }
        [Association("Concepto-ConceptoDetalles")]
        public XPCollection<ConceptoDetalle> ConceptoDetalles { get { return GetCollection<ConceptoDetalle>("ConceptoDetalles"); } }
        [Association("Concepto-Percepcions")]
        public XPCollection<Percepcion> Percepcions { get { return GetCollection<Percepcion>("Percepcions"); } }
        [Association("Concepto-Deduccions")]
        public XPCollection<Deduccion> Deduccions { get { return GetCollection<Deduccion>("Deduccions"); } }
        
    }
}
