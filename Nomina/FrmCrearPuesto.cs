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
    public partial class FrmCrearPuesto : DevExpress.XtraEditors.XtraForm
    {
        public FrmCrearPuesto()
        {
            InitializeComponent();
        }
        public Boolean correcto = false;
        UnitOfWork uow;
        XPCollection xpcPuesto;
        private void Enlazar()
        {
            txtDescripcion.DataBindings.Add("Text", puesto, "Descripcion", true, DataSourceUpdateMode.OnPropertyChanged);
            txtSueldo.DataBindings.Add("Text", puesto, "Sueldo", true, DataSourceUpdateMode.OnPropertyChanged);
        }
        public Puesto Puesto
        {
            get
            { return Puesto; }
            set
            {
                puesto = value;
                Enlazar();
            }
        }
        public FrmCrearPuesto(Puesto puesto, XPCollection xpcPuesto, UnitOfWork uow)
            : this()
        {
            this.Puesto = puesto;
            this.uow = uow;
            this.xpcPuesto = xpcPuesto;
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