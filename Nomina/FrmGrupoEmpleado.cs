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
    public partial class FrmGrupoEmpleado : DevExpress.XtraEditors.XtraForm
    {
        public FrmGrupoEmpleado()
        {
            InitializeComponent();
        }
        UnitOfWork uow;
        XPCollection xpcGrupoEmpleado;
        private void RecargarColecciones()
        {
            xpcGrupoEmpleado.Reload();
        }
        public FrmGrupoEmpleado(XPCollection xpcGrupoEmpleado, UnitOfWork uow)
        {
            InitializeComponent();
            this.uow = uow;
            this.xpcGrupoEmpleado = xpcGrupoEmpleado;
            this.xpcGrupoEmpleado.Session = uow;
            RecargarColecciones();
            gridGrupoEmpleado.DataSource = this.xpcGrupoEmpleado;
            UpdateMenu();
            RefreshGrid();
        }
        private void UpdateMenu()
        {
            btnEditar.Enabled = xpcGrupoEmpleado.Count > 0;
            btnEliminar.Enabled = xpcGrupoEmpleado.Count > 0;
        }
        private void RefreshGrid()
        {
            xpcGrupoEmpleado.Reload();
            gridGrupoEmpleado.Refresh();
        }

        private void EditGrupoEmpleado(GrupoEmpleado GrupoEmpleado)
        {
            FrmCrearGrupoEmpleado crearGrupoEmpleado = new FrmCrearGrupoEmpleado(GrupoEmpleado, xpcGrupoEmpleado, uow);
            crearGrupoEmpleado.ShowDialog();
            if (crearGrupoEmpleado.correcto)
            {
                GrupoEmpleado.Save();
                AuditTrailService.Instance.SaveAuditData(uow);
                uow.CommitChanges();
                RefreshGrid();
            }
            else
            {
                GrupoEmpleado.Reload();
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
            EditGrupoEmpleado(new GrupoEmpleado(uow));
            UpdateMenu();
        }

        private void BtnEditar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grdGrupoEmpleado.DataRowCount > 0)
            {
                EditGrupoEmpleado((GrupoEmpleado)grdGrupoEmpleado.GetFocusedRow());
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
            GrupoEmpleado GrupoEmpleado = ((GrupoEmpleado)xpcGrupoEmpleado[grdGrupoEmpleado.FocusedRowHandle]);
            GrupoEmpleado.Delete();
            GrupoEmpleado.Save();
            xpcGrupoEmpleado.Remove(GrupoEmpleado);
            uow.CommitChanges();
            UpdateMenu();
            RefreshGrid();
        }

    }
}