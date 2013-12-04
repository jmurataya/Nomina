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
    public partial class FrmEmpleado : DevExpress.XtraEditors.XtraForm
    {
        public FrmEmpleado()
        {
            InitializeComponent();
        }
        UnitOfWork uow;
        XPCollection xpcEmpleado;
        XPCollection xpcDepartamento;
        XPCollection xpcTipoNomina;
        XPCollection xpcPuesto;
        XPCollection xpcTipoEmpleado;
        XPCollection xpcGrupoEmpleado;
        XPCollection xpcPercepcion;
        XPCollection xpcDeduccion;
        XPCollection xpcConcepto;
        XPCollection xpcCuentaContable;
        private void RecargarColecciones()
        {
            xpcEmpleado.Reload();
            xpcDepartamento.Reload();
            xpcTipoNomina.Reload();
            xpcPuesto.Reload();
            xpcTipoEmpleado.Reload();
            xpcGrupoEmpleado.Reload();
            xpcPercepcion.Reload();
            xpcDeduccion.Reload();
            xpcConcepto.Reload();
            xpcCuentaContable.Reload();
        }
        public FrmEmpleado(XPCollection xpcEmpleado, XPCollection xpcDepartamento, XPCollection xpcTipoNomina,XPCollection xpcPuesto,XPCollection xpcTipoEmpleado,XPCollection xpcGrupoEmpleado, XPCollection xpcPercepcion, XPCollection xpcDeduccion,XPCollection xpcConcepto,XPCollection xpcCuentaContable, UnitOfWork uow)
        {
            InitializeComponent();
            this.uow = uow;
            this.xpcEmpleado = xpcEmpleado;
            this.xpcEmpleado.Session = uow;
            this.xpcDepartamento=xpcDepartamento;
            this.xpcTipoNomina=xpcTipoNomina;
            this.xpcPuesto=xpcPuesto;
            this.xpcTipoEmpleado=xpcTipoEmpleado;
            this.xpcGrupoEmpleado = xpcGrupoEmpleado;
            this.xpcDepartamento.Session = uow;
            this.xpcTipoNomina.Session = uow;
            this.xpcPuesto.Session = uow;
            this.xpcTipoEmpleado.Session = uow;
            this.xpcGrupoEmpleado.Session = uow;
            this.xpcPercepcion = xpcPercepcion;
            this.xpcPercepcion.Session = uow;
            this.xpcDeduccion = xpcDeduccion;
            this.xpcDeduccion.Session = uow;
            this.xpcConcepto = xpcConcepto;
            this.xpcConcepto.Session = uow;
            this.xpcCuentaContable = xpcCuentaContable;
            this.xpcCuentaContable.Session = uow;
            RecargarColecciones();
            gridEmpleado.DataSource = this.xpcEmpleado;
            UpdateMenu();
            grdEmpleado.Columns.Clear();
            grdEmpleado.Columns.AddVisible("Oid");
            grdEmpleado.Columns.AddVisible("ApellidoPaterno");
            grdEmpleado.Columns.AddVisible("ApellidoMaterno");
            grdEmpleado.Columns.AddVisible("Nombre");
            grdEmpleado.BestFitColumns();
            RefreshGrid();
        }
        private void UpdateMenu()
        {
            btnEditar.Enabled = xpcEmpleado.Count > 0;
            btnEliminar.Enabled = xpcEmpleado.Count > 0;
        }
        private void RefreshGrid()
        {
            xpcEmpleado.Reload();
            gridEmpleado.Refresh();
        }

        private void EditEmpleado(Empleado empleado)
        {
            FrmCrearEmpleado crearEmpleado = new FrmCrearEmpleado(empleado, xpcEmpleado,xpcDepartamento,xpcTipoNomina,xpcPuesto,xpcTipoEmpleado,xpcGrupoEmpleado, xpcPercepcion, xpcDeduccion, xpcConcepto,xpcCuentaContable, uow);
            crearEmpleado.ShowDialog();
            if (crearEmpleado.correcto)
            {
                empleado.Save();
                AuditTrailService.Instance.SaveAuditData(uow);
                uow.CommitChanges();
                RefreshGrid();
            }
            else
            {
                empleado.Reload();
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
            EditEmpleado(new Empleado(uow));
            UpdateMenu();
        }

        private void BtnEditar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grdEmpleado.DataRowCount > 0)
            {
                EditEmpleado((Empleado)grdEmpleado.GetFocusedRow());
            }
        }

        private void grdEmpleado_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnEditar_ItemClick(null, null);
            }
        }

        private void BtnEliminar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Empleado empleado = ((Empleado)xpcEmpleado[grdEmpleado.FocusedRowHandle]);
            empleado.Delete();
            empleado.Save();
            xpcEmpleado.Remove(empleado);
            uow.CommitChanges();
            UpdateMenu();
            RefreshGrid();
        }

    }
}