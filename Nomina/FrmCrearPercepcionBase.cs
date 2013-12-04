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

namespace Nomina
{
    public partial class FrmCrearPercepcionBase : DevExpress.XtraEditors.XtraForm
    {
        public FrmCrearPercepcionBase()
        {
            InitializeComponent();
        }
        public Boolean correcto = false;
        UnitOfWork uow;
        XPCollection xpcCuentaContable;
        XPCollection xpcPercepcionBase;
        private void Enlazar()
        {
            txtDescripcion.DataBindings.Add("Text", percepcionbase, "Descripcion", true, DataSourceUpdateMode.OnPropertyChanged);
            cmbTipo.Properties.Items.AddRange(Enum.GetValues(typeof(TipoBase)));
            cmbTipo.DataBindings.Add("EditValue", percepcionbase, "TipoPercepcionBase", true, DataSourceUpdateMode.OnPropertyChanged);
            lkpCuentaContable.DataBindings.Add("EditValue",percepcionbase,"CuentaContable!",true,DataSourceUpdateMode.OnPropertyChanged);
        }
        public PercepcionBase PercepcionBase
        {
            get
            { return PercepcionBase; }
            set
            {
                percepcionbase = value;
                Enlazar();
            }
        }
        public FrmCrearPercepcionBase(PercepcionBase percepcionbase, XPCollection xpcPercepcionBase, UnitOfWork uow, XPCollection xpcCuentaContable)
            : this()
        {
            xpcCuentaContable.Filter = new BinaryOperator("TipoCuenta", tipocuenta.Percepcion, BinaryOperatorType.Equal);
            this.PercepcionBase = percepcionbase;
            this.uow = uow;
            this.xpcCuentaContable = xpcCuentaContable;
            this.xpcCuentaContable.Session = uow;
            this.xpcPercepcionBase = xpcPercepcionBase;
            this.xpcPercepcionBase.Session = uow;
            lkpCuentaContable.Properties.DataSource = xpcCuentaContable;
            lkpCuentaContable.Properties.ValueMember = "This";
            lkpCuentaContable.Properties.DisplayMember = "Cuenta";
        }

        private void btnAceptar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            correcto = true;
            xpcCuentaContable.Filter = null;
            Close();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            xpcCuentaContable.Filter = null;
            Close();
        }
    }
}