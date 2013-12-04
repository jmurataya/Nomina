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
    public partial class FrmCrearPercepcion : DevExpress.XtraEditors.XtraForm
    {
        public FrmCrearPercepcion()
        {
            InitializeComponent();
        }
        public Boolean correcto = false;
        UnitOfWork uow;
        XPCollection xpcPercepcion;
        XPCollection xpcConcepto;
        private void Enlazar()
        {
            txtImporte.DataBindings.Add("Text", percepcion, "Importe", true, DataSourceUpdateMode.OnPropertyChanged);
            txtReferencia.DataBindings.Add("Text", percepcion, "Referencia", true, DataSourceUpdateMode.OnPropertyChanged);
            txtReferencia1.DataBindings.Add("Text", percepcion, "Referencia1", true, DataSourceUpdateMode.OnPropertyChanged);
            lkpConcepto.DataBindings.Add("EditValue", percepcion, "Concepto!", true, DataSourceUpdateMode.OnPropertyChanged);
        }
        public Percepcion Percepcion
        {
            get
            { return Percepcion; }
            set
            {
                percepcion = value;
                Enlazar();
            }
        }
        public FrmCrearPercepcion(Percepcion percepcion, XPCollection xpcPercepcion, UnitOfWork uow, XPCollection xpcConcepto)
            : this()
        {
            xpcConcepto.Filter = new BinaryOperator("Tipo", ConceptoTipo.Percepcion, BinaryOperatorType.Equal);
            this.Percepcion = percepcion;
            this.uow = uow;
            this.xpcConcepto = xpcConcepto;
            this.xpcConcepto.Session = uow;
            this.xpcPercepcion = xpcPercepcion;
            this.xpcPercepcion.Session = uow;
            lkpConcepto.Properties.DataSource = xpcConcepto;
            lkpConcepto.Properties.ValueMember = "This";
            lkpConcepto.Properties.DisplayMember = "Descripcion";
        }

        private void btnAceptar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            correcto = true;
            xpcConcepto.Filter = null;
            Close();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            xpcConcepto.Filter = null;
            Close();
        }

    }
}