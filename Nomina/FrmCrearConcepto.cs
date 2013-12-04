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
    public partial class FrmCrearConcepto : DevExpress.XtraEditors.XtraForm
    {
        public FrmCrearConcepto()
        {
            InitializeComponent();
        }
        public Boolean correcto = false;
        UnitOfWork uow;
        NestedUnitOfWork uowAnidada;
        XPCollection xpcConcepto;
        XPCollection xpcCuentaContable;
        XPCollection xpcPercepcionBase;
        XPCollection xpcConceptoDetalle;
        XPCollection xpcConceptoDetalleAnidada;
        private void Enlazar()
        {
            txtDescripcion.DataBindings.Add("Text", concepto, "Descripcion", true, DataSourceUpdateMode.OnPropertyChanged);
            cmbTipoPago.Properties.Items.AddRange(Enum.GetValues(typeof(ConceptoTipo)));
            cmbTipoPago.DataBindings.Add("EditValue", concepto, "Tipo", true, DataSourceUpdateMode.OnPropertyChanged);
            cmbPeriodoPago.Properties.Items.AddRange(Enum.GetValues(typeof(ConceptoPeriodo)));
            cmbPeriodoPago.DataBindings.Add("EditValue", concepto, "Periodo", true, DataSourceUpdateMode.OnPropertyChanged);
            cmbParametroPago.Properties.Items.AddRange(Enum.GetValues(typeof(ConceptoPago)));
            cmbParametroPago.DataBindings.Add("EditValue", concepto, "Pago", true, DataSourceUpdateMode.OnPropertyChanged);
            txtPorcentaje.DataBindings.Add("Text", concepto, "Porcentaje", true, DataSourceUpdateMode.OnPropertyChanged);
            txtMaximo.DataBindings.Add("Text", concepto, "Maximo", true, DataSourceUpdateMode.OnPropertyChanged);
            lkpCuentaContable.DataBindings.Add("EditValue", concepto, "CuentaContable!", true, DataSourceUpdateMode.OnPropertyChanged);
        }
        public Concepto Concepto
        {
            get
            { return Concepto; }
            set
            {
                concepto = value;
                Enlazar();
            }
        }
        public FrmCrearConcepto(Concepto concepto, XPCollection xpcConcepto, UnitOfWork uow,XPCollection xpcCuentaContable,XPCollection xpcPercepcionBase,XPCollection xpcConceptoDetalle)
            : this()
        {
            this.uow = uow;
            this.uowAnidada = uow.BeginNestedUnitOfWork();
            this.xpcCuentaContable = xpcCuentaContable;
            this.xpcCuentaContable.Session = uow;
            this.Concepto = concepto;
            this.xpcConcepto = xpcConcepto;
            this.xpcConcepto.Session = uow;
            this.xpcPercepcionBase = xpcPercepcionBase;
            this.xpcPercepcionBase.Session = uow;
            this.xpcConceptoDetalle = xpcConceptoDetalle;
            this.xpcConceptoDetalle.Session = uow;
            xpcConceptoDetalleAnidada = new XPCollection(uowAnidada, xpcConceptoDetalle, new GroupOperator(GroupOperatorType.And, new BinaryOperator("Concepto", concepto, BinaryOperatorType.Equal)));
            xpcConceptoDetalleAnidada.DisplayableProperties="This;PercepcionBase.Descripcion";
            lkpCuentaContable.Properties.DataSource = xpcCuentaContable;
            lkpCuentaContable.Properties.ValueMember = "This";
            lkpCuentaContable.Properties.DisplayMember = "Cuenta";
            gridConceptoDetalle.DataSource = xpcConceptoDetalleAnidada;
            grdConceptoDetalle.Columns.Clear();
            grdConceptoDetalle.Columns.AddVisible("PercepcionBase.Descripcion");
        }

        private void btnAceptar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            correcto = true;
            uowAnidada.CommitChanges();
            Close();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void btnAniadir_Click(object sender, EventArgs e)
        {
            XPCollection xpcConceptoAnidada = new XPCollection(uowAnidada, xpcConcepto, new GroupOperator(GroupOperatorType.And, new BinaryOperator("This", concepto, BinaryOperatorType.Equal)));
            if (xpcConceptoAnidada.Count != 0)
            {
                Concepto concepto1 = (Concepto)(xpcConceptoAnidada[0]);
                FrmPercepcionBase frmpercepcionbase = new FrmPercepcionBase(xpcPercepcionBase, uow, xpcCuentaContable, xpcConceptoDetalle, concepto1, xpcConceptoDetalleAnidada, uowAnidada);
                frmpercepcionbase.ShowDialog();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            ConceptoDetalle conceptodetalle = ((ConceptoDetalle)xpcConceptoDetalleAnidada[grdConceptoDetalle.FocusedRowHandle]);
            conceptodetalle.Delete();
//            conceptodetalle.Save();
            xpcConceptoDetalleAnidada.Remove(conceptodetalle);
            ///uow.CommitChanges();
            gridConceptoDetalle.Refresh();

        }

        private void cmbParametroPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbParametroPago.EditValue!=DBNull.Value)
                if (Convert.ToInt16(cmbParametroPago.EditValue) == (int)ConceptoPago.Importe)
            {
                gridConceptoDetalle.Enabled = false;
                btnAniadir.Enabled = false;
                btnEliminar.Enabled = false;
            }
            else
            {
                gridConceptoDetalle.Enabled = true;
                btnAniadir.Enabled = true;
                btnEliminar.Enabled = true;
            }
        }

        private void cmbTipoPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTipoPago.EditValue!=DBNull.Value)
            {
            int seleccion=Convert.ToInt32(cmbTipoPago.EditValue);
            switch (seleccion)
            {
                case 0:
                    xpcCuentaContable.Filter = new BinaryOperator("TipoCuenta", tipocuenta.Percepcion, BinaryOperatorType.Equal);
                    break;
                case 1:
                    xpcCuentaContable.Filter = new BinaryOperator("TipoCuenta", tipocuenta.Deduccion, BinaryOperatorType.Equal);
                    break;
                case 2:
                    xpcCuentaContable.Filter = new BinaryOperator("TipoCuenta", tipocuenta.Prestamo, BinaryOperatorType.Equal);
                    break;
                case 3:
                    xpcCuentaContable.Filter = new BinaryOperator("TipoCuenta", tipocuenta.Pension, BinaryOperatorType.Equal);
                    break;
                default:
                    xpcCuentaContable.Filter = null;
                    break;
            }
            }
        }
    }
}