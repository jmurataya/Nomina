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
    public partial class FrmCuentaContable : DevExpress.XtraEditors.XtraForm
    {
        public FrmCuentaContable()
        {
            InitializeComponent();
        }
        UnitOfWork uow;
        XPCollection xpcCuentaContable;
        private void RecargarColecciones()
        {
            xpcCuentaContable.Reload();
        }
        public FrmCuentaContable(XPCollection xpcCuentaContable, UnitOfWork uow)
        {
            InitializeComponent();
            this.uow = uow;
            this.xpcCuentaContable = xpcCuentaContable;
            this.xpcCuentaContable.Session = uow;
            RecargarColecciones();
            gridCuentaContable.DataSource = this.xpcCuentaContable;
            UpdateMenu();
            RefreshGrid();
        }
        private void UpdateMenu()
        {
            btnEditar.Enabled = xpcCuentaContable.Count > 0;
            btnEliminar.Enabled = xpcCuentaContable.Count > 0;
        }
        private void RefreshGrid()
        {
            xpcCuentaContable.Reload();
            gridCuentaContable.Refresh();
        }

        private void EditCuentaContable(CuentaContable cuentacontable)
        {
            FrmCrearCuentaContable crearCuentaContable = new FrmCrearCuentaContable(cuentacontable, xpcCuentaContable, uow);
            crearCuentaContable.ShowDialog();
            if (crearCuentaContable.correcto)
            {
                cuentacontable.Save();
                uow.CommitChanges();
                AuditTrailService.Instance.SaveAuditData(uow);
                uow.CommitChanges();
                RefreshGrid();
            }
            else
            {
                cuentacontable.Reload();
                RefreshGrid();
                RecargarColecciones();
            }
        }

        private void BtnSalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void BtnAniadir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            EditCuentaContable(new CuentaContable(uow));
            UpdateMenu();
        }

        private void BtnEditar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grdCuentaContable.DataRowCount > 0)
            {
                EditCuentaContable((CuentaContable)grdCuentaContable.GetFocusedRow());
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
            CuentaContable cuentacontable = ((CuentaContable)xpcCuentaContable[grdCuentaContable.FocusedRowHandle]);
            cuentacontable.Delete();
            cuentacontable.Save();
            xpcCuentaContable.Remove(cuentacontable);
            uow.CommitChanges();
            UpdateMenu();
            RefreshGrid();
        }

    }
}