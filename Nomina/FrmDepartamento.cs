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
    public partial class FrmDepartamento : DevExpress.XtraEditors.XtraForm
    {
        public FrmDepartamento()
        {
            InitializeComponent();
        }
        UnitOfWork uow;
        XPCollection xpcDepartamento;
        XPCollection xpcCuentaContable;
        private void RecargarColecciones()
        {
            xpcDepartamento.Reload();
            xpcCuentaContable.Reload();
        }

        public FrmDepartamento(XPCollection xpcDepartamento, UnitOfWork uow,XPCollection xpcCuentaContable)
        {
            InitializeComponent();
            this.uow = uow;
            this.xpcDepartamento = xpcDepartamento;
            this.xpcDepartamento.Session = uow;
            this.xpcCuentaContable = xpcCuentaContable;
            this.xpcCuentaContable.Session = uow;
            RecargarColecciones();
            gridDepartamento.DataSource = this.xpcDepartamento;
            UpdateMenu();
            RefreshGrid();
        }
        private void UpdateMenu() {
            btnEditar.Enabled = xpcDepartamento.Count > 0;
            btnEliminar.Enabled = xpcDepartamento.Count > 0;
        }
        private void RefreshGrid() {
            xpcDepartamento.Reload();
            gridDepartamento.Refresh();
        }

        private void EditDepartamento(Departamento departamento) 
        {
            FrmCrearDepartamento crearDepartamento = new FrmCrearDepartamento(departamento,xpcDepartamento,uow,xpcCuentaContable);
            crearDepartamento.ShowDialog();
            if (crearDepartamento.correcto)
            {
                departamento.Save();
                AuditTrailService.Instance.SaveAuditData(uow);
                uow.CommitChanges();
                RefreshGrid();
            }
            else
            {
                departamento.Reload();
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
            EditDepartamento(new Departamento(uow));
            UpdateMenu();
        }

        private void BtnEditar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grdDepartamento.DataRowCount > 0)
            {
                EditDepartamento((Departamento)grdDepartamento.GetFocusedRow());
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
            Departamento departamento = ((Departamento)xpcDepartamento[grdDepartamento.FocusedRowHandle]);
            departamento.Delete();
            departamento.Save();
            xpcDepartamento.Remove(departamento);
            uow.CommitChanges();
            UpdateMenu();
            RefreshGrid();
        }

    }
}