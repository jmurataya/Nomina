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
    public partial class FrmPuesto : DevExpress.XtraEditors.XtraForm
    {
        public FrmPuesto()
        {
            InitializeComponent();
        }
        UnitOfWork uow;
        XPCollection xpcPuesto;
        private void RecargarColecciones()
        {
            xpcPuesto.Reload();
        }
        public FrmPuesto(XPCollection xpcPuesto, UnitOfWork uow)
        {
            InitializeComponent();
            this.uow = uow;
            this.xpcPuesto = xpcPuesto;
            this.xpcPuesto.Session = uow;
            RecargarColecciones();
            gridPuesto.DataSource = this.xpcPuesto;
            UpdateMenu();
            RefreshGrid();
        }
        private void UpdateMenu()
        {
            btnEditar.Enabled = xpcPuesto.Count > 0;
            btnEliminar.Enabled = xpcPuesto.Count > 0;
        }
        private void RefreshGrid()
        {
            xpcPuesto.Reload();
            gridPuesto.Refresh();
        }

        private void EditPuesto(Puesto puesto)
        {
            FrmCrearPuesto crearPuesto = new FrmCrearPuesto(puesto, xpcPuesto, uow);
            crearPuesto.ShowDialog();
            if (crearPuesto.correcto)
            {
                puesto.Save();
                AuditTrailService.Instance.SaveAuditData(uow);
                uow.CommitChanges();
                RefreshGrid();
            }
            else
            {
                puesto.Reload();
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
            EditPuesto(new Puesto(uow));
            UpdateMenu();
        }

        private void BtnEditar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grdPuesto.DataRowCount > 0)
            {
                EditPuesto((Puesto)grdPuesto.GetFocusedRow());
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
            Puesto puesto = ((Puesto)xpcPuesto[grdPuesto.FocusedRowHandle]);
            puesto.Delete();
            puesto.Save();
            xpcPuesto.Remove(puesto);
            uow.CommitChanges();
            UpdateMenu();
            RefreshGrid();
        }

    }
}