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
    public partial class FrmSubsidio : DevExpress.XtraEditors.XtraForm
    {
        public FrmSubsidio()
        {
            InitializeComponent();
        }
        UnitOfWork uow;
        XPCollection xpcSubsidio;
        private void RecargarColecciones()
        {
            xpcSubsidio.Reload();
        }
        public FrmSubsidio(XPCollection xpcSubsidio, UnitOfWork uow)
        {
            InitializeComponent();
            this.uow = uow;
            this.xpcSubsidio = xpcSubsidio;
            this.xpcSubsidio.Session = uow;
            RecargarColecciones();
            grdSubsidio.DataSource = this.xpcSubsidio;
            UpdateMenu();
            RefreshGrid();
        }
        private void UpdateMenu() {
            BtnEditar.Enabled = xpcSubsidio.Count > 0;
            BtnEliminar.Enabled = xpcSubsidio.Count > 0;
        }
        private void RefreshGrid() {
            xpcSubsidio.Reload();
            grdSubsidio.Refresh();
        }

        private void EditSubsidio(Subsidio subsidio) 
        {
            FrmCrearSubsidio crearSubsidio = new FrmCrearSubsidio(subsidio,xpcSubsidio,uow);
            crearSubsidio.ShowDialog();
            if(crearSubsidio.correcto) 
            {
                subsidio.Save();
                AuditTrailService.Instance.SaveAuditData(uow);
                uow.CommitChanges();
                RefreshGrid();
            }
            else 
            {
                subsidio.Reload();
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
            EditSubsidio(new Subsidio(uow));
            UpdateMenu();
        }

        private void BtnEditar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridSubsidio.DataRowCount > 0)
            {
                EditSubsidio((Subsidio)gridSubsidio.GetFocusedRow());
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
            Subsidio subsidio = ((Subsidio)xpcSubsidio[gridSubsidio.FocusedRowHandle]);
            subsidio.Delete();
            subsidio.Save();
            xpcSubsidio.Remove(subsidio);
            uow.CommitChanges();
            UpdateMenu();
            RefreshGrid();
        }
    }
}