using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.Data;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.XtraReports;
using DevExpress.XtraReports.UI;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.AuditTrail;
using System.Security.Principal;

namespace Nomina
{
    public partial class Principal : DevExpress.XtraEditors.XtraForm
    {
        public Principal()
        {
            InitializeComponent();
        }

        private void BtnImpuesto_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmImpuesto frmImpuesto = new FrmImpuesto(xpcImpuesto, uow);
            frmImpuesto.ShowDialog();
        }

        private void BtnSubsidio_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmSubsidio frmSubsidio = new FrmSubsidio(xpcSubsidio, uow);
            frmSubsidio.ShowDialog();
        }

        private void BtnSalir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void BtnCuentaContable_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmCuentaContable frmcuentacontable = new FrmCuentaContable(xpcCuentaContable, uow);
            frmcuentacontable.ShowDialog();
        }

        private void BtnDepartamentos_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmDepartamento frmdepartamento = new FrmDepartamento(xpcDepartamento, uow, xpcCuentaContable);
            frmdepartamento.ShowDialog();
        }

        private void BtnPuestos_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmPuesto frmpuesto = new FrmPuesto(xpcPuesto, uow);
            frmpuesto.ShowDialog();
        }

        private void BtnConceptos_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmConcepto frmconcepto = new FrmConcepto(xpcConcepto, uow,xpcCuentaContable,xpcPercepcionBase,xpcConceptoDetalle);
            frmconcepto.ShowDialog();
        }

        private void btnPercepcionBase_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmPercepcionBase frmpercepcionbase = new FrmPercepcionBase(xpcPercepcionBase, uow, xpcCuentaContable,xpcConceptoDetalle);
            frmpercepcionbase.btnAsignar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            frmpercepcionbase.ShowDialog();
        }

        private void BtnEmpleados_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmEmpleado frmempleado = new FrmEmpleado(xpcEmpleado, xpcDepartamento, xpcTipoNomina, xpcPuesto, xpcTipoEmpleado, xpcGrupoEmpleado,xpcPercepcion,xpcDeduccion, xpcConcepto,xpcCuentaContable, uow);
            frmempleado.ShowDialog();
        }

        private void btnTipoNomina_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmTipoNomina frmtiponomina = new FrmTipoNomina(xpcTipoNomina, uow);
            frmtiponomina.ShowDialog();
        }

        private void btnTipoEmpleado_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmTipoEmpleado frmtipoempleado = new FrmTipoEmpleado(xpcTipoEmpleado, uow);
            frmtipoempleado.ShowDialog();
        }

        private void btnGrupoEmpleado_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmGrupoEmpleado frmgrupoempleado = new FrmGrupoEmpleado(xpcGrupoEmpleado,uow);
            frmgrupoempleado.ShowDialog();
        }

        private void Principal_Load(object sender, EventArgs e)
        {
            AuditTrailService.Instance.QueryCurrentUserName += new QueryCurrentUserNameEventHandler(Instance_QueryCurrentUserName);
            uow.Dictionary.GetDataStoreSchema(typeof(Concepto).Assembly, typeof(CuentaContable).Assembly, typeof(Deduccion).Assembly,
                typeof(Departamento).Assembly, typeof(Empleado).Assembly, typeof(GrupoEmpleado).Assembly, typeof(Impuesto).Assembly,
                typeof(Percepcion).Assembly, typeof(PercepcionBase).Assembly, typeof(Puesto).Assembly, typeof(Subsidio).Assembly,
                typeof(TipoEmpleado).Assembly, typeof(TipoNomina).Assembly);
            AuditTrailService.Instance.SetXPDictionary(uow.Dictionary);
            AuditTrailService.Instance.AuditDataStore = new AuditDataStore<AuditDataItemPersistent, AuditedObjectWeakReference>();
            AuditTrailService.Instance.BeginSessionAudit(uow, AuditTrailStrategy.OnObjectChanged, ObjectAuditingMode.Full);
            uow.UpdateSchema();
        }
        void Instance_QueryCurrentUserName(object sender, QueryCurrentUserNameEventArgs e)
        {
            e.CurrentUserName = WindowsIdentity.GetCurrent().Name;
        }

        private void Principal_FormClosed(object sender, FormClosedEventArgs e)
        {
            AuditTrailService.Instance.SaveAuditData(uow);
            uow.CommitChanges();
        }
    }
}