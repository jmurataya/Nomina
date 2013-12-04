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
    public enum AplicarPrestamo{Concepto,Percepciones,Neto};
    public class Deduccion : XPObject
    {
        public Deduccion(Session session)
            : base(session)
        { }
        private Empleado _Empleado;
        [Association("Empleado-Deduccions")]
        public Empleado Empleado
        {
            get { return _Empleado; }
            set { SetPropertyValue("Empleado", ref _Empleado, value); }
        }
        private Concepto _Concepto;
        [Association("Concepto-Deduccions")]
        public Concepto Concepto
        {
            get { return _Concepto; }
            set { SetPropertyValue("Concepto", ref _Concepto, value); }
        }
        private DateTime _Fecha;
        public  DateTime Fecha
        {
            get { return _Fecha; }
            set
            {
                _Fecha = value;
            }
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
        
        private decimal _Descuento;
        public decimal Descuento
        {
            get { return _Descuento; }
            set
            {
                _Descuento = value;
            }
        }
        private double _Adeudo;
        public double Adeudo
        {
            get { return _Adeudo; }
            set
            {
                _Adeudo = value;
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
        private AplicarPrestamo _AplicarSobre;
        public AplicarPrestamo AplicarSobre
        {
            get { return _AplicarSobre; }
            set
            {
                _AplicarSobre = value;
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
        
        private CuentaContable _CuentaContable;
        [Association("CuentaContable-Deduccions")]
        public CuentaContable CuentaContable
        {
            get { return _CuentaContable; }
            set { SetPropertyValue("CuentaContable", ref _CuentaContable, value); }
        }
    }
}
