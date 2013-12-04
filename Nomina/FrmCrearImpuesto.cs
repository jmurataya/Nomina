using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.Xpo;

namespace Nomina
{
    public partial class FrmCrearImpuesto : DevExpress.XtraEditors.XtraForm
    {
        public Boolean correcto = false;
        UnitOfWork uow;
        XPCollection xpcImpuesto;
        public FrmCrearImpuesto()
        {
        }
            private void Enlazar()
        {
            txtLimiteInferior.DataBindings.Add("Text", impuesto, "LimiteInferior",true,DataSourceUpdateMode.OnPropertyChanged);
            txtLimiteSuperior.DataBindings.Add("Text", impuesto, "LimiteSuperior",true,DataSourceUpdateMode.OnPropertyChanged);
            txtCuotaFija.DataBindings.Add("Text", impuesto, "CuotaFija",true,DataSourceUpdateMode.OnPropertyChanged);
            txtPorcentaje.DataBindings.Add("Text", impuesto, "Porcentaje",true,DataSourceUpdateMode.OnPropertyChanged);
        }
        public Impuesto Impuesto
        {
            get
            { return Impuesto; }
            set
            {
                impuesto = value;
                Enlazar();
            }
        }
        public FrmCrearImpuesto(Impuesto impuesto, XPCollection xpcImpuesto, UnitOfWork uow)
        : this()
        {
            InitializeComponent();
            this.Impuesto = impuesto;
            this.uow = uow;
            this.xpcImpuesto = xpcImpuesto;
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
}
}