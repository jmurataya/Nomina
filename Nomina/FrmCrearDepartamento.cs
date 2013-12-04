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
    public partial class FrmCrearDepartamento : DevExpress.XtraEditors.XtraForm
    {
        public FrmCrearDepartamento()
        {
            InitializeComponent();
        }
        public Boolean correcto = false;
        UnitOfWork uow;
        XPCollection xpcDepartamento;
        XPCollection xpcCuentaContable;
        private void Enlazar()
        {
            txtDependencia.DataBindings.Add("Text", departamento, "Dependencia", true, DataSourceUpdateMode.OnPropertyChanged);
            txtDescripcion.DataBindings.Add("Text", departamento, "Descripcion",true,DataSourceUpdateMode.OnPropertyChanged);
            lkpCuentaContable.DataBindings.Add("EditValue", departamento, "CuentaContable!", true, DataSourceUpdateMode.OnPropertyChanged);
        }
        public Departamento Departamento
        {
            get
            { return Departamento; }
            set
            {
                departamento = value;
                Enlazar();
            }
        }
        public FrmCrearDepartamento(Departamento departamento, XPCollection xpcDepartamento, UnitOfWork uow,XPCollection xpcCuentaContable)
        : this()
        {
            xpcCuentaContable.Filter = new BinaryOperator("TipoCuenta", tipocuenta.Departamento, BinaryOperatorType.Equal);
            this.Departamento = departamento;
            this.uow = uow;
            this.xpcCuentaContable = xpcCuentaContable;
            this.xpcCuentaContable.Session = uow;
            this.xpcDepartamento = xpcDepartamento;
            this.xpcDepartamento.Session = uow;
            lkpCuentaContable.Properties.DataSource = xpcCuentaContable;
            lkpCuentaContable.Properties.ValueMember = "This";
            lkpCuentaContable.Properties.DisplayMember = "Descripcion";
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