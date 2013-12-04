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
    public partial class FrmCrearGrupoEmpleado : DevExpress.XtraEditors.XtraForm
    {
        public FrmCrearGrupoEmpleado()
        {
            InitializeComponent();
        }
        public Boolean correcto = false;
        UnitOfWork uow;
        XPCollection xpcGrupoEmpleado;
        private void Enlazar()
        {
            txtDescripcion.DataBindings.Add("Text", grupoempleado, "Descripcion", true, DataSourceUpdateMode.OnPropertyChanged);
        }
        public GrupoEmpleado GrupoEmpleado
        {
            get
            { return GrupoEmpleado; }
            set
            {
                grupoempleado = value;
                Enlazar();
            }
        }
        public FrmCrearGrupoEmpleado(GrupoEmpleado grupoempleado, XPCollection xpcGrupoEmpleado, UnitOfWork uow)
            : this()
        {
            this.GrupoEmpleado = grupoempleado;
            this.uow = uow;
            this.xpcGrupoEmpleado = xpcGrupoEmpleado;
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