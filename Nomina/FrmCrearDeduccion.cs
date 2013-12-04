using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;

namespace Nomina
{
    public partial class FrmCrearDeduccion : DevExpress.XtraEditors.XtraForm
    {
        public FrmCrearDeduccion()
        {
            InitializeComponent();
        }
        public Boolean correcto = false;
        UnitOfWork uow;
        XPCollection xpcDeduccion;
        XPCollection xpcConcepto;
        XPCollection xpcCuentaContable;
        private void Enlazar()
        {
            lkpConcepto.DataBindings.Add("EditValue", deduccion, "Concepto!", true, DataSourceUpdateMode.OnPropertyChanged);
            txtFecha.DataBindings.Add("Text", deduccion, "Fecha",true,DataSourceUpdateMode.OnPropertyChanged);
            txtImporte.DataBindings.Add("Text", deduccion, "Importe", true, DataSourceUpdateMode.OnPropertyChanged);
            txtDescuento.DataBindings.Add("Text",deduccion,"Descuento",true,DataSourceUpdateMode.OnPropertyChanged);
            txtAdeudo.DataBindings.Add("Text", deduccion, "Adeudo", true, DataSourceUpdateMode.OnPropertyChanged);
            txtReferencia.DataBindings.Add("Text", deduccion, "Referencia", true, DataSourceUpdateMode.OnPropertyChanged);
            txtReferencia1.DataBindings.Add("Text", deduccion, "Referencia1", true, DataSourceUpdateMode.OnPropertyChanged);
            cmbAplicarSobre.Properties.Items.AddRange(Enum.GetValues(typeof(AplicarPrestamo)));
            cmbAplicarSobre.DataBindings.Add("EditValue", deduccion, "AplicarSobre", true, DataSourceUpdateMode.OnPropertyChanged);
            lkpCuentaContable.DataBindings.Add("EditValue", deduccion, "CuentaContable!", true, DataSourceUpdateMode.OnPropertyChanged);
        }
        public Deduccion Deduccion
        {
            get
            { return Deduccion; }
            set
            {
                deduccion = value;
                Enlazar();
            }
        }
        public FrmCrearDeduccion(Deduccion deduccion, XPCollection xpcDeduccion, UnitOfWork uow, XPCollection xpcConcepto,XPCollection xpcCuentaContable)
            : this()
        {
            xpcConcepto.Filter = new BinaryOperator("Tipo", ConceptoTipo.Deduccion, BinaryOperatorType.Equal);
            this.Deduccion = deduccion;
            this.uow = uow;
            this.xpcConcepto = xpcConcepto;
            this.xpcCuentaContable = xpcCuentaContable;
            this.xpcDeduccion = xpcDeduccion;
            lkpConcepto.Properties.DataSource = this.xpcConcepto;
            lkpConcepto.Properties.ValueMember = "This";
            lkpConcepto.Properties.DisplayMember = "Descripcion";
            lkpCuentaContable.Properties.DataSource = this.xpcCuentaContable;
            lkpCuentaContable.Properties.ValueMember = "This";
            lkpCuentaContable.Properties.DisplayMember = "Descripcion";
        }

        private void btnAceptar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            correcto = true;
            xpcConcepto.Filter = null;
            Close();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            xpcConcepto.Filter = null;
            Close();
        }
    }
}