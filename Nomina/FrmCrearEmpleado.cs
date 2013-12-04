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
    public partial class FrmCrearEmpleado : DevExpress.XtraEditors.XtraForm
    {
        public FrmCrearEmpleado()
        {
            InitializeComponent();
        }
        public Boolean correcto = false;
        UnitOfWork uow;
        NestedUnitOfWork uowAnidada;
        XPCollection xpcConcepto;
        XPCollection xpcPercepcion;
        XPCollection xpcDeduccion;
        XPCollection xpcPercepcionAnidada;
        XPCollection xpcDeduccionAnidada;
        XPCollection xpcConceptoAnidada;
        XPCollection xpcEmpleado;
        XPCollection xpcDepartamento;
        XPCollection xpcTipoNomina;
        XPCollection xpcPuesto;
        XPCollection xpcTipoEmpleado;
        XPCollection xpcGrupoEmpleado;
        XPCollection xpcCuentaContable;
        XPCollection xpcCuentaContableAnidada;
        private void Enlazar()
        {
            txtCodigoEmpleado.DataBindings.Add("Text", empleado, "Oid", true, DataSourceUpdateMode.OnPropertyChanged);
            txtApellidoPaterno.DataBindings.Add("Text", empleado, "ApellidoPaterno", true, DataSourceUpdateMode.OnPropertyChanged);
            txtApellidoMaterno.DataBindings.Add("Text", empleado, "ApellidoMaterno", true, DataSourceUpdateMode.OnPropertyChanged);
            txtNombre.DataBindings.Add("Text", empleado, "Nombre", true, DataSourceUpdateMode.OnPropertyChanged);
            cmbEstado.Properties.Items.AddRange(Enum.GetValues(typeof(EmpleadoEstado)));
            cmbEstado.DataBindings.Add("EditValue", empleado, "Estado", true, DataSourceUpdateMode.OnPropertyChanged);
            cmbServicioMedico.Properties.Items.AddRange(Enum.GetValues(typeof(EmpleadoServicioMedico)));
            cmbServicioMedico.DataBindings.Add("EditValue", empleado, "ServicioMedico", true, DataSourceUpdateMode.OnPropertyChanged);
            cmbSexo.Properties.Items.AddRange(Enum.GetValues(typeof(EmpleadoSexo)));
            cmbSexo.DataBindings.Add("EditValue", empleado, "Sexo", true, DataSourceUpdateMode.OnPropertyChanged);
            cmbEstadoCivil.Properties.Items.AddRange(Enum.GetValues(typeof(EmpleadoEstadoCivil)));
            cmbEstadoCivil.DataBindings.Add("EditValue", empleado, "EstadoCivil", true, DataSourceUpdateMode.OnPropertyChanged);
            txtServicioMedico.DataBindings.Add("Text", empleado, "NumeroServicioMedico", true, DataSourceUpdateMode.OnPropertyChanged);
            txtFechaIngreso.DataBindings.Add("Text", empleado, "FechaIngreso", true, DataSourceUpdateMode.OnPropertyChanged);
            txtTarjetaBancaria.DataBindings.Add("Text", empleado, "TarjetaBancaria", true, DataSourceUpdateMode.OnPropertyChanged);
            txtFechaDeNacimiento.DataBindings.Add("Text", empleado, "FechaDeNacimiento", true, DataSourceUpdateMode.OnPropertyChanged);
            txtRfc.DataBindings.Add("Text",empleado,"Rfc",true,DataSourceUpdateMode.OnPropertyChanged);
            txtCurp.DataBindings.Add("Text",empleado,"Curp",true,DataSourceUpdateMode.OnPropertyChanged);
            txtDireccion.DataBindings.Add("Text",empleado,"Direccion",true,DataSourceUpdateMode.OnPropertyChanged);
            pppInformacionPersonal.DataBindings.Add("EditValue", empleado, "Curp", true, DataSourceUpdateMode.OnPropertyChanged);
            pppNombre.DataBindings.Add("EditValue", empleado, "NombreCompleto", true, DataSourceUpdateMode.OnPropertyChanged);
            lkpDepartamento.DataBindings.Add("EditValue", empleado, "Departamento!", true, DataSourceUpdateMode.OnPropertyChanged);
            lkpTipoNomina.DataBindings.Add("EditValue", empleado, "TipoNomina!", true, DataSourceUpdateMode.OnPropertyChanged);
            lkpPuesto.DataBindings.Add("EditValue", empleado, "Puesto!", true, DataSourceUpdateMode.OnPropertyChanged);
            lkpTipoEmpleado.DataBindings.Add("EditValue", empleado, "TipoEmpleado!", true, DataSourceUpdateMode.OnPropertyChanged);
            lkpGrupo.DataBindings.Add("EditValue", empleado, "GrupoEmpleado!", true, DataSourceUpdateMode.OnPropertyChanged);
        }
        public Empleado Empleado
        {
            get
            { return Empleado; }
            set
            {
                empleado = value;
                Enlazar();
            }
        }
        public FrmCrearEmpleado(Empleado empleado, XPCollection xpcEmpleado, XPCollection xpcDepartamento, XPCollection xpcTipoNomina, XPCollection xpcPuesto, XPCollection xpcTipoEmpleado, XPCollection xpcGrupoEmpleado, XPCollection xpcPercepcion, XPCollection xpcDeduccion, XPCollection xpcConcepto,XPCollection xpcCuentaContable, UnitOfWork uow)
            : this()
        {
            this.Empleado = empleado;
            this.uow = uow;
            this.xpcEmpleado = xpcEmpleado;
            this.xpcDepartamento = xpcDepartamento;
            this.xpcTipoNomina = xpcTipoNomina;
            this.xpcPuesto = xpcPuesto;
            this.xpcTipoEmpleado = xpcTipoEmpleado;
            this.xpcGrupoEmpleado = xpcGrupoEmpleado;
            this.xpcPercepcion = xpcPercepcion;
            this.xpcDeduccion = xpcDeduccion;
            this.xpcDepartamento.Session = uow;
            this.xpcTipoNomina.Session = uow;
            this.xpcPuesto.Session = uow;
            this.xpcTipoEmpleado.Session = uow;
            this.xpcGrupoEmpleado.Session = uow;
            this.xpcPercepcion.Session = uow;
            this.xpcDeduccion.Session = uow;
            this.xpcConcepto = xpcConcepto;
            this.xpcConcepto.Session = uow;
            this.xpcCuentaContable = xpcCuentaContable;
            this.xpcCuentaContable.Session = uow;
            this.uowAnidada = uow.BeginNestedUnitOfWork();
            this.xpcConceptoAnidada = new XPCollection(uowAnidada, xpcConcepto);
            this.xpcPercepcionAnidada = new XPCollection(uowAnidada, xpcPercepcion, new GroupOperator(GroupOperatorType.And, new BinaryOperator("Empleado", empleado, BinaryOperatorType.Equal)));
            xpcPercepcionAnidada.DisplayableProperties = "This;Concepto.Descripcion;Importe;Referencia;Referencia1";
            this.xpcCuentaContableAnidada = new XPCollection(uowAnidada, xpcCuentaContable);
            ///xpcCuentaContableAnidada.DisplayableProperties = "This;Concepto.Descripcion;Importe;Referencia;Referencia1";
            gridPercepcion.DataSource = xpcPercepcionAnidada;
            grdPercepcion.Columns.Clear();
            grdPercepcion.Columns.AddVisible("Concepto.Descripcion", "Concepto");
            grdPercepcion.Columns.AddVisible("Importe", "Importe");
            grdPercepcion.Columns.AddVisible("Referencia", "Referencia");
            grdPercepcion.Columns.AddVisible("Referencia1", "Referencia1");
            grdPercepcion.BestFitColumns();
            this.xpcDeduccionAnidada = new XPCollection(uowAnidada, xpcDeduccion, new GroupOperator(GroupOperatorType.And, new BinaryOperator("Empleado", empleado, BinaryOperatorType.Equal)));
            xpcDeduccionAnidada.DisplayableProperties = "This;Concepto.Descripcion;Fecha;Importe;Descuento;Adeudo;Referencia;Referencia1;CuentaContable.Descripcion";
            gridDeduccion.DataSource = xpcDeduccionAnidada;
            grdDeduccion.Columns.Clear();
            grdDeduccion.Columns.AddVisible("Concepto.Descripcion","Concepto");
            grdDeduccion.Columns.AddVisible("Fecha", "Fecha");
            grdDeduccion.Columns.AddVisible("Importe", "Importe");
            grdDeduccion.Columns.AddVisible("Descuento", "Descuento");
            grdDeduccion.Columns.AddVisible("Adeudo", "Adeudo");
            grdDeduccion.Columns.AddVisible("Referencia", "Referencia");
            grdDeduccion.BestFitColumns();
            RellenarCombos();
        }
        private void RellenarCombos()
        {
            lkpDepartamento.Properties.DataSource = xpcDepartamento;
            lkpDepartamento.Properties.ValueMember = "This";
            lkpDepartamento.Properties.Columns.Clear();
            lkpDepartamento.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descripcion", 100, "Descripción"));
            lkpDepartamento.Properties.DisplayMember = "Descripcion";
            lkpTipoNomina.Properties.DataSource = xpcTipoNomina;
            lkpTipoNomina.Properties.ValueMember = "This";
            lkpTipoNomina.Properties.Columns.Clear();
            lkpTipoNomina.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descripcion", 100, "Descripción"));
            lkpTipoNomina.Properties.DisplayMember = "Descripcion";
            lkpPuesto.Properties.DataSource = xpcPuesto;
            lkpPuesto.Properties.ValueMember = "This";
            lkpPuesto.Properties.Columns.Clear();
            lkpPuesto.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descripcion", 100, "Descripción"));
            lkpPuesto.Properties.DisplayMember = "Descripcion";
            lkpTipoEmpleado.Properties.DataSource = xpcTipoEmpleado;
            lkpTipoEmpleado.Properties.ValueMember = "This";
            lkpTipoEmpleado.Properties.Columns.Clear();
            lkpTipoEmpleado.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descripcion", 100, "Descripción"));
            lkpTipoEmpleado.Properties.DisplayMember = "Descripcion";
            lkpGrupo.Properties.DataSource = xpcGrupoEmpleado;
            lkpGrupo.Properties.ValueMember = "This";
            lkpGrupo.Properties.Columns.Clear();
            lkpGrupo.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descripcion", 100, "Descripción"));
            lkpGrupo.Properties.DisplayMember = "Descripcion";
        }
        private void btnAceptar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            correcto = true;
            uowAnidada.CommitChanges();
            Close();
        }

        private void btnCancelar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void pppNombre_QueryDisplayText(object sender, DevExpress.XtraEditors.Controls.QueryDisplayTextEventArgs e)
        {
            if (empleado!=null)
            pppNombre.Text = empleado.NombreCompleto;
        }

        
        
        /// <summary>
        /// Realiza las Operaciones con Percepciones
        /// </summary>
        private void UpdateMenu()
        {
            btnEditarPercepcion.Enabled = xpcPercepcionAnidada.Count > 0;
            btnEliminarPercepcion.Enabled = xpcPercepcionAnidada.Count > 0;
            btnEditarDeduccion.Enabled = xpcDeduccionAnidada.Count > 0;
            btnEliminarDeduccion.Enabled = xpcDeduccionAnidada.Count > 0;
        }
        private void RefreshGrid()
        {
//            xpcPercepcionAnidada.Reload();
            gridPercepcion.Refresh();
            //            xpcDeduccionAnidada.Reload();
            gridDeduccion.Refresh();
        }
        private void EditPercepcion(Percepcion percepcion)
        {
            FrmCrearPercepcion crearPercepcion = new FrmCrearPercepcion(percepcion, xpcPercepcionAnidada, uowAnidada, xpcConceptoAnidada);
            crearPercepcion.ShowDialog();
            if (crearPercepcion.correcto)
            {
                percepcion.Empleado = uowAnidada.GetNestedObject(empleado);
                percepcion.Save();
                xpcPercepcionAnidada.Add(percepcion);
                RefreshGrid();
            }
            else
            {
                if (!uowAnidada.IsNewObject(percepcion))
                {
                    percepcion.Reload();
                    RefreshGrid();
                }
            }
        }
        private void btnAniadirPercepcion_Click(object sender, EventArgs e)
        {
            EditPercepcion(new Percepcion(uowAnidada));
            UpdateMenu();
        }

        private void btnEditarPercepcion_Click(object sender, EventArgs e)
        {
            if (grdPercepcion.DataRowCount > 0)
            {
                EditPercepcion((Percepcion)grdPercepcion.GetFocusedRow());
            }
        }

        private void btnEliminarPercepcion_Click(object sender, EventArgs e)
        {
            Percepcion percepcion = ((Percepcion)xpcPercepcionAnidada[grdPercepcion.FocusedRowHandle]);
            percepcion.Delete();
            percepcion.Save();
            xpcPercepcionAnidada.Remove(percepcion);
            UpdateMenu();
            RefreshGrid();
        }

        ///Operaciones con Deducciones
        private void EditDeduccion(Deduccion deduccion)
        {
            FrmCrearDeduccion crearDeduccion = new FrmCrearDeduccion(deduccion, xpcDeduccionAnidada, uowAnidada, xpcConceptoAnidada,xpcCuentaContableAnidada);
            crearDeduccion.ShowDialog();
            if (crearDeduccion.correcto)
            {
                deduccion.Empleado = uowAnidada.GetNestedObject(empleado);
                deduccion.Save();
                xpcDeduccionAnidada.Add(deduccion);
                RefreshGrid();
            }
            else
            {
                if (!uowAnidada.IsNewObject(deduccion))
                {
                    deduccion.Reload();
                    RefreshGrid();
                }
            }
        }
        private void btnAniadirDeduccion_Click(object sender, EventArgs e)
        {
            EditDeduccion(new Deduccion(uowAnidada));
            UpdateMenu();
        }

        private void btnEditarDeduccion_Click(object sender, EventArgs e)
        {
            if (grdDeduccion.DataRowCount > 0)
            {
                EditDeduccion((Deduccion)grdDeduccion.GetFocusedRow());
            }
        }

        private void btnEliminarDeduccion_Click(object sender, EventArgs e)
        {
            Deduccion deduccion = ((Deduccion)xpcDeduccionAnidada[grdDeduccion.FocusedRowHandle]);
            deduccion.Delete();
            deduccion.Save();
            xpcDeduccionAnidada.Remove(deduccion);
            UpdateMenu();
            RefreshGrid();
        }



    }
}