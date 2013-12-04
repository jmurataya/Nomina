using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo.DB;
using DevExpress.Utils;

namespace Nomina
{
    public partial class FrmAuditoria : Form
    {
        XPCollection<AuditDataItemPersistent> auditoria;
        public FrmAuditoria()
        {
            InitializeComponent();
        }
        public FrmAuditoria(XPCollection<AuditDataItemPersistent>  auditoria)
        {
            this.auditoria = auditoria;
            InitializeComponent();
        }

        private void FrmAuditoria_Load(object sender, EventArgs e)
        {
            gridAuditoria.DataSource=auditoria;
            grdAuditoria.Columns.Clear();
            grdAuditoria.Columns.AddVisible("UserName", "Usuario");
            grdAuditoria.Columns.AddVisible("ModifiedOn", "Modificado");
            grdAuditoria.Columns.AddVisible("PropertyName", "Propiedad");
            grdAuditoria.Columns.AddVisible("NewValue", "Valor");
            grdAuditoria.ClearSorting();
            grdAuditoria.Columns["ModifiedOn"].SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
            grdAuditoria.Columns["PropertyName"].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
            grdAuditoria.Columns["ModifiedOn"].DisplayFormat.FormatType = FormatType.DateTime;
            grdAuditoria.Columns["ModifiedOn"].DisplayFormat.FormatString = "g";
            grdAuditoria.BestFitColumns();
            grdAuditoria.MoveFirst();
        }
    }
}
