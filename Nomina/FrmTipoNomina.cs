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
    public partial class FrmTipoNomina : DevExpress.XtraEditors.XtraForm
    {
        public FrmTipoNomina()
        {
            InitializeComponent();
        }
        UnitOfWork uow;
        XPCollection xpcTipoNomina;
        private void RecargarColecciones()
        {
            xpcTipoNomina.Reload();
        }
        public FrmTipoNomina(XPCollection xpcTipoNomina, UnitOfWork uow)
        {
            InitializeComponent();
            this.uow = uow;
            this.xpcTipoNomina = xpcTipoNomina;
            this.xpcTipoNomina.Session = uow;
            RecargarColecciones();
            gridTipoNomina.DataSource = this.xpcTipoNomina;
            UpdateMenu();
            RefreshGrid();
        }
        private void UpdateMenu()
        {
            btnEditar.Enabled = xpcTipoNomina.Count > 0;
            btnEliminar.Enabled = xpcTipoNomina.Count > 0;
        }
        private void RefreshGrid()
        {
            xpcTipoNomina.Reload();
            gridTipoNomina.Refresh();
        }

        private void EditTipoNomina(TipoNomina tiponomina)
        {
            FrmCrearTipoNomina crearTipoNomina = new FrmCrearTipoNomina(tiponomina, xpcTipoNomina, uow);
            crearTipoNomina.ShowDialog();
            if (crearTipoNomina.correcto)
            {
                tiponomina.Save();
                AuditTrailService.Instance.SaveAuditData(uow);
                uow.CommitChanges();
                RefreshGrid();
            }
            else
            {
                tiponomina.Reload();
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
            EditTipoNomina(new TipoNomina(uow));
            UpdateMenu();
        }

        private void BtnEditar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grdTipoNomina.DataRowCount > 0)
            {
                EditTipoNomina((TipoNomina)grdTipoNomina.GetFocusedRow());
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
            TipoNomina tiponomina = ((TipoNomina)xpcTipoNomina[grdTipoNomina.FocusedRowHandle]);
            tiponomina.Delete();
            tiponomina.Save();
            xpcTipoNomina.Remove(tiponomina);
            uow.CommitChanges();
            UpdateMenu();
            RefreshGrid();
        }

    }
}