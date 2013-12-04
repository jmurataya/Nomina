using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.Xpo;
using DevExpress.Persistent.AuditTrail;

namespace Nomina
{
    public partial class FrmConcepto : DevExpress.XtraEditors.XtraForm
    {
        public FrmConcepto()
        {
            InitializeComponent();
        }
        UnitOfWork uow;
        XPCollection xpcConcepto;
        XPCollection xpcCuentaContable;
        XPCollection xpcPercepcionBase;
        XPCollection xpcConceptoDetalle;
        public FrmConcepto(XPCollection xpcConcepto, UnitOfWork uow,XPCollection xpcCuentaContable,XPCollection xpcPercepcionBase, XPCollection xpcConceptoDetalle)
        {
            InitializeComponent();
            this.uow = uow;
            this.xpcCuentaContable = xpcCuentaContable;
            this.xpcCuentaContable.Session = uow;
            this.xpcConcepto = xpcConcepto;
            this.xpcConcepto.Session = uow;
            this.xpcPercepcionBase = xpcPercepcionBase;
            this.xpcPercepcionBase.Session = uow;
            this.xpcConceptoDetalle = xpcConceptoDetalle;
            this.xpcConceptoDetalle.Session = uow;
            RecargarColecciones();
            gridConcepto.DataSource = this.xpcConcepto;
            UpdateMenu();
            RefreshGrid();
        }
        private void UpdateMenu()
        {
            btnEditar.Enabled = xpcConcepto.Count > 0;
            btnEliminar.Enabled = xpcConcepto.Count > 0;
        }
        private void RefreshGrid()
        {
            xpcConcepto.Reload();
            gridConcepto.Refresh();
        }

        private void EditConcepto(Concepto concepto)
        {
            FrmCrearConcepto crearConcepto = new FrmCrearConcepto(concepto, xpcConcepto, uow,xpcCuentaContable,xpcPercepcionBase,xpcConceptoDetalle);
            crearConcepto.ShowDialog();
            if (crearConcepto.correcto)
            {
                concepto.Save();
                AuditTrailService.Instance.SaveAuditData(uow);
                uow.CommitChanges();
                RefreshGrid();
            }
            else
            {
                concepto.Reload();
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
            EditConcepto(new Concepto(uow));
            UpdateMenu();
        }

        private void BtnEditar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grdConcepto.DataRowCount > 0)
            {
                EditConcepto((Concepto)grdConcepto.GetFocusedRow());
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
            Concepto concepto = ((Concepto)xpcConcepto[grdConcepto.FocusedRowHandle]);
            concepto.Delete();
            concepto.Save();
            xpcConcepto.Remove(concepto);
            uow.CommitChanges();
            UpdateMenu();
            RefreshGrid();
        }
        private void RecargarColecciones()
        {
            xpcConcepto.Reload();
            xpcConceptoDetalle.Reload();
            xpcCuentaContable.Reload();
        }

        private void FrmConcepto_FormClosed(object sender, FormClosedEventArgs e)
        {
        }
    }
}