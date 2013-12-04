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
    public partial class FrmCrearTipoEmpleado : DevExpress.XtraEditors.XtraForm
    {
        public FrmCrearTipoEmpleado()
        {
            InitializeComponent();
        }
        public Boolean correcto = false;
        UnitOfWork uow;
        XPCollection xpcTipoEmpleado;
        private void Enlazar()
        {
            txtDescripcion.DataBindings.Add("Text", tipoempleado, "Descripcion", true, DataSourceUpdateMode.OnPropertyChanged);
        }
        public TipoEmpleado TipoEmpleado
        {
            get
            { return TipoEmpleado; }
            set
            {
                tipoempleado = value;
                Enlazar();
            }
        }
        public FrmCrearTipoEmpleado(TipoEmpleado tipoempleado, XPCollection xpcTipoEmpleado, UnitOfWork uow)
            : this()
        {
            this.TipoEmpleado = tipoempleado;
            this.uow = uow;
            this.xpcTipoEmpleado = xpcTipoEmpleado;
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