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
    public partial class FrmImpuesto : DevExpress.XtraEditors.XtraForm
    {
        UnitOfWork uow;
        XPCollection xpcImpuesto;
        private void RecargarColecciones()
        {
            xpcImpuesto.Reload();
        }
        public FrmImpuesto()
        {
        }
        public FrmImpuesto(XPCollection xpcImpuesto, UnitOfWork uow)
        {
            InitializeComponent();
            this.uow = uow;
            this.xpcImpuesto = xpcImpuesto;
            this.xpcImpuesto.Session = uow;
            RecargarColecciones();
            grdImpuesto.DataSource = this.xpcImpuesto;
            UpdateMenu();
            RefreshGrid();
        }
        private void UpdateMenu() {
            BtnEditar.Enabled = xpcImpuesto.Count > 0;
            BtnEliminar.Enabled = xpcImpuesto.Count > 0;
        }
        private void RefreshGrid() {
            xpcImpuesto.Reload();
            grdImpuesto.Refresh();
        }

        private void EditImpuesto(Impuesto impuesto) 
        {
            FrmCrearImpuesto crearImpuesto = new FrmCrearImpuesto(impuesto,xpcImpuesto,uow);
            crearImpuesto.ShowDialog();
            if(crearImpuesto.correcto) 
            {
                impuesto.Save();
                AuditTrailService.Instance.SaveAuditData(uow);
                uow.CommitChanges();
                RefreshGrid();
            }
            else 
            {
                impuesto.Reload();
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
            EditImpuesto(new Impuesto(uow));
            UpdateMenu();
        }

        private void BtnEditar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridImpuesto.DataRowCount > 0)
            {
                EditImpuesto((Impuesto)gridImpuesto.GetFocusedRow());
            }
        }
    
        private void grdDeudores_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter) 
            {
                BtnEditar_ItemClick(null, null);        
            }
        }

        private void BtnEliminar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Impuesto impuesto = ((Impuesto)xpcImpuesto[gridImpuesto.FocusedRowHandle]);
            impuesto.Delete();
            impuesto.Save();
            xpcImpuesto.Remove(impuesto);
            uow.CommitChanges();
            UpdateMenu();
            RefreshGrid();
        }
    }
}