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
    public partial class FrmTipoEmpleado : DevExpress.XtraEditors.XtraForm
    {
        public FrmTipoEmpleado()
        {
            InitializeComponent();
        }
        UnitOfWork uow;
        XPCollection xpcTipoEmpleado;
        private void RecargarColecciones()
        {
            xpcTipoEmpleado.Reload();
        }
        public FrmTipoEmpleado(XPCollection xpcTipoEmpleado, UnitOfWork uow)
        {
            InitializeComponent();
            this.uow = uow;
            this.xpcTipoEmpleado = xpcTipoEmpleado;
            this.xpcTipoEmpleado.Session = uow;
            RecargarColecciones();
            gridTipoEmpleado.DataSource = this.xpcTipoEmpleado;
            UpdateMenu();
            RefreshGrid();
        }
        private void UpdateMenu()
        {
            btnEditar.Enabled = xpcTipoEmpleado.Count > 0;
            btnEliminar.Enabled = xpcTipoEmpleado.Count > 0;
        }
        private void RefreshGrid()
        {
            xpcTipoEmpleado.Reload();
            gridTipoEmpleado.Refresh();
        }

        private void EditTipoEmpleado(TipoEmpleado TipoEmpleado)
        {
            FrmCrearTipoEmpleado crearTipoEmpleado = new FrmCrearTipoEmpleado(TipoEmpleado, xpcTipoEmpleado, uow);
            crearTipoEmpleado.ShowDialog();
            if (crearTipoEmpleado.correcto)
            {
                TipoEmpleado.Save();
                AuditTrailService.Instance.SaveAuditData(uow);
                uow.CommitChanges();
                RefreshGrid();
            }
            else
            {
                TipoEmpleado.Reload();
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
            EditTipoEmpleado(new TipoEmpleado(uow));
            UpdateMenu();
        }

        private void BtnEditar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grdTipoEmpleado.DataRowCount > 0)
            {
                EditTipoEmpleado((TipoEmpleado)grdTipoEmpleado.GetFocusedRow());
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
            TipoEmpleado TipoEmpleado = ((TipoEmpleado)xpcTipoEmpleado[grdTipoEmpleado.FocusedRowHandle]);
            TipoEmpleado.Delete();
            TipoEmpleado.Save();
            xpcTipoEmpleado.Remove(TipoEmpleado);
            uow.CommitChanges();
            UpdateMenu();
            RefreshGrid();
        }

    }
}