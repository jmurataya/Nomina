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
using DevExpress.Persistent.AuditTrail;

namespace Nomina
{
    public partial class FrmPercepcionBase : DevExpress.XtraEditors.XtraForm
    {
        public FrmPercepcionBase()
        {
            InitializeComponent();
        }
        UnitOfWork uow;
        XPCollection xpcPercepcionBase;
        XPCollection xpcCuentaContable;
        XPCollection xpcConceptoDetalle;
        Concepto concepto = null;
        XPCollection xpcConceptoDetalleAnidada;
        NestedUnitOfWork uowAnidada;
        private void RecargarColecciones()
        {
            xpcPercepcionBase.Reload();
            xpcConceptoDetalle.Reload();
            xpcCuentaContable.Reload();
            //xpcConceptoDetalleAnidada.Reload();
        }

        public FrmPercepcionBase(XPCollection xpcPercepcionBase, UnitOfWork uow, XPCollection xpcCuentaContable,XPCollection xpcConceptoDetalle)
        {
            InitializeComponent();
            this.uow = uow;
            this.xpcPercepcionBase = xpcPercepcionBase;
            this.xpcPercepcionBase.Session = uow;
            this.xpcCuentaContable = xpcCuentaContable;
            this.xpcCuentaContable.Session = uow;
            this.xpcConceptoDetalle = xpcConceptoDetalle;
            this.xpcConceptoDetalle.Session = uow;
            RecargarColecciones();
            gridPercepcionBase.DataSource = this.xpcPercepcionBase;
            UpdateMenu();
            RefreshGrid();
        }
        public FrmPercepcionBase(XPCollection xpcPercepcionBase, UnitOfWork uow, XPCollection xpcCuentaContable, XPCollection xpcConceptoDetalle,Concepto concepto,XPCollection xpcConceptoDetalleAnidada, NestedUnitOfWork uowAnidada)
        {
            InitializeComponent();
            this.uow = uow;
            this.uowAnidada = uowAnidada;
            this.xpcPercepcionBase = xpcPercepcionBase;
            this.xpcPercepcionBase.Session = uow;
            this.xpcCuentaContable = xpcCuentaContable;
            this.xpcCuentaContable.Session = uow;
            this.xpcConceptoDetalle = xpcConceptoDetalle;
            this.xpcConceptoDetalle.Session = uow;
            this.xpcConceptoDetalleAnidada = xpcConceptoDetalleAnidada;
            this.xpcConceptoDetalleAnidada.Session = uowAnidada;
            gridPercepcionBase.DataSource = this.xpcPercepcionBase;
            this.concepto = concepto;
            UpdateMenu();
            RefreshGrid();
        }
        private void UpdateMenu()
        {
            btnEditar.Enabled = xpcPercepcionBase.Count > 0;
            btnEliminar.Enabled = xpcPercepcionBase.Count > 0;
        }
        private void RefreshGrid()
        {
            xpcPercepcionBase.Reload();
            gridPercepcionBase.Refresh();
        }

        private void EditPercepcionBase(PercepcionBase percepcionbase)
        {
            FrmCrearPercepcionBase crearPercepcionBase = new FrmCrearPercepcionBase(percepcionbase, xpcPercepcionBase, uow,xpcCuentaContable);
            crearPercepcionBase.ShowDialog();
            if (crearPercepcionBase.correcto)
            {
                percepcionbase.Save();
                AuditTrailService.Instance.SaveAuditData(uow);
                uow.CommitChanges();
                RefreshGrid();
            }
            else
            {
                percepcionbase.Reload();
                RefreshGrid();
            }
            RecargarColecciones();
        }

        private void BtnSalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void BtnAniadir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            EditPercepcionBase(new PercepcionBase(uow));
            UpdateMenu();
        }

        private void BtnEditar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grdPercepcionBase.DataRowCount > 0)
            {
                EditPercepcionBase((PercepcionBase)grdPercepcionBase.GetFocusedRow());
            }
        }

        private void grdDeudores_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnEditar_ItemClick(null, null);
            }
        }

        private void BtnEliminar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PercepcionBase percepcionbase = ((PercepcionBase)xpcPercepcionBase[grdPercepcionBase.FocusedRowHandle]);
            percepcionbase.Delete();
            percepcionbase.Save();
            xpcPercepcionBase.Remove(percepcionbase);
            uow.CommitChanges();
            UpdateMenu();
            RefreshGrid();
        }

        private void btnAsignar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PercepcionBase percepcionbase = ((PercepcionBase)xpcPercepcionBase[grdPercepcionBase.FocusedRowHandle]);
            XPCollection xpcPercepcionBaseAnidada = new XPCollection(uowAnidada, xpcPercepcionBase, new GroupOperator(GroupOperatorType.And, new BinaryOperator("This", percepcionbase, BinaryOperatorType.Equal)));
            if (xpcPercepcionBaseAnidada.Count != 0)
            {
                PercepcionBase percepcionbase1 = (PercepcionBase)(xpcPercepcionBaseAnidada[0]);
                xpcConceptoDetalleAnidada.Filter = new GroupOperator(GroupOperatorType.And, new BinaryOperator("PercepcionBase", percepcionbase1, BinaryOperatorType.Equal),
                    new BinaryOperator("Concepto", concepto, BinaryOperatorType.Equal));
                if (xpcConceptoDetalleAnidada.Count == 0)
                {
                    ConceptoDetalle conceptodetalle = new ConceptoDetalle(uowAnidada);
                    conceptodetalle.Concepto = concepto;
                    conceptodetalle.PercepcionBase = percepcionbase1;
                    //                conceptodetalle.Save();
                    xpcConceptoDetalleAnidada.Add(conceptodetalle);
                    xpcConceptoDetalleAnidada.Filter = new GroupOperator(GroupOperatorType.And, new BinaryOperator("Concepto", concepto, BinaryOperatorType.Equal));
                    Close();
                }
                else
                {
                    XtraMessageBox.Show("La Percepcion Base ya Esta Incluida en el Concepto", "Percepcion Base ya Incluida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    xpcConceptoDetalleAnidada.Filter = new GroupOperator(GroupOperatorType.And, new BinaryOperator("Concepto", concepto, BinaryOperatorType.Equal));
                    Close();
                }
            }
        }

    }
}