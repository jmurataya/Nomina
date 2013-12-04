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
using DevExpress.Persistent.BaseImpl;
using System.Security.Principal;
using DevExpress.ExpressApp;

namespace Nomina
{
    public partial class FrmCrearCuentaContable : DevExpress.XtraEditors.XtraForm
    {
        public FrmCrearCuentaContable()
        {
            InitializeComponent();
        }
        public Boolean correcto = false;
        UnitOfWork uow;
        XPCollection xpcCuentaContable;
        private void Enlazar()
        {
            txtCuenta.DataBindings.Add("Text", cuentacontable, "Cuenta", true, DataSourceUpdateMode.OnPropertyChanged);
            txtDescripcion.DataBindings.Add("Text", cuentacontable, "Descripcion", true, DataSourceUpdateMode.OnPropertyChanged);
            cmbTipo.Properties.Items.AddRange(Enum.GetValues(typeof(tipocuenta)));
            cmbTipo.DataBindings.Add("EditValue", cuentacontable, "TipoCuenta", true, DataSourceUpdateMode.OnPropertyChanged);
        }
        public CuentaContable CuentaContable
        {
            get
            { return CuentaContable; }
            set
            {
                cuentacontable = value;
                Enlazar();
            }
        }
        public FrmCrearCuentaContable(CuentaContable cuentacontable, XPCollection xpcCuentaContable, UnitOfWork uow)
            : this()
        {
            this.CuentaContable = cuentacontable;
            this.uow = uow;
            this.xpcCuentaContable = xpcCuentaContable;
        }

        private void btnAceptar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            correcto = true;
            Close();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void FrmCrearCuentaContable_Load(object sender, EventArgs e)
        {

        }

        private void btnAuditoria_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmAuditoria frmAuditoria = new FrmAuditoria(cuentacontable.Auditoria);
            frmAuditoria.ShowDialog();
        }

    }
}