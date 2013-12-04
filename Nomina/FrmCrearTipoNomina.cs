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
    public partial class FrmCrearTipoNomina : DevExpress.XtraEditors.XtraForm
    {
        public FrmCrearTipoNomina()
        {
            InitializeComponent();
        }
        public Boolean correcto = false;
        UnitOfWork uow;
        XPCollection xpcTipoNomina;
        private void Enlazar()
        {
            txtDescripcion.DataBindings.Add("Text", tiponomina, "Descripcion", true, DataSourceUpdateMode.OnPropertyChanged);
        }
        public TipoNomina TipoNomina
        {
            get
            { return TipoNomina; }
            set
            {
                tiponomina = value;
                Enlazar();
            }
        }
        public FrmCrearTipoNomina(TipoNomina tiponomina, XPCollection xpcTipoNomina, UnitOfWork uow)
            : this()
        {
            this.TipoNomina = tiponomina;
            this.uow = uow;
            this.xpcTipoNomina = xpcTipoNomina;
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