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
    public partial class FrmCrearSubsidio : DevExpress.XtraEditors.XtraForm
    {
        public FrmCrearSubsidio()
        {
            InitializeComponent();
        }
        public Boolean correcto = false;
        UnitOfWork uow;
        XPCollection xpcSubsidio;
        private void Enlazar()
        {
            txtLimiteInferior.DataBindings.Add("Text", subsidio, "LimiteInferior",true,DataSourceUpdateMode.OnPropertyChanged);
            txtLimiteSuperior.DataBindings.Add("Text", subsidio, "LimiteSuperior",true,DataSourceUpdateMode.OnPropertyChanged);
            txtCredito.DataBindings.Add("Text", subsidio, "Credito",true,DataSourceUpdateMode.OnPropertyChanged);
        }
        public Subsidio Subsidio
        {
            get
            { return Subsidio; }
            set
            {
                subsidio = value;
                Enlazar();
            }
        }
        public FrmCrearSubsidio(Subsidio subsidio, XPCollection xpcSubsidio, UnitOfWork uow)
        : this()
        {
            this.Subsidio = subsidio;
            this.uow = uow;
            this.xpcSubsidio = xpcSubsidio;
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