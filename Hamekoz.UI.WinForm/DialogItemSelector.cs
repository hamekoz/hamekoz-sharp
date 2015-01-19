using System;
using System.Windows.Forms;

namespace Hamekoz.UI.WinForm
{
    public partial class DialogItemSelector : Form
    {
        public DialogItemSelector()
        {
            InitializeComponent();
        }

        public object Item
        {
            get { return combo.SelectedItem; }
        }

        public ComboBox ComboBox {
            get { return combo; }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void DialogItemSelector_Load(object sender, EventArgs e)
        {
            this.combo.Focus();
        }
    }
}