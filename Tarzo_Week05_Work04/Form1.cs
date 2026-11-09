using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace Tarzo_Week05_Work04
{
    public partial class Form1 : Form
    {
        private readonly List<Product> _products = new List<Product>();
        private bool _updating; // prevents Price <-> Markup events from re-triggering each other
        private static readonly CultureInfo Ci = CultureInfo.InvariantCulture;

        public Form1()
        {
            InitializeComponent();
            ResetForm();
        }

        // ---------- Barcode ----------
        private void txtBarcode_TextChanged(object sender, EventArgs e)
        {
            string code = txtBarcode.Text;
            if (code.Length == 0)
            {
                lblBarcodeStatus.Text = "Enter 8 to 13 digits.";
                lblBarcodeStatus.ForeColor = Color.Gray;
            }
            else if (code.Length < 8)
            {
                lblBarcodeStatus.Text = "Barcode must be at least 8 digits.";
                lblBarcodeStatus.ForeColor = Color.Firebrick;
            }
            else if (_products.Any(p => p.Barcode == code))
            {
                lblBarcodeStatus.Text = "Barcode already exists.";
                lblBarcodeStatus.ForeColor = Color.Firebrick;
            }
            else
            {
                lblBarcodeStatus.Text = "Barcode is available.";
                lblBarcodeStatus.ForeColor = Color.SeaGreen;
            }
        }

        private void DigitsOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) e.Handled = true;
        }

        private void txtName_Leave(object sender, EventArgs e)
        {
            txtName.Text = txtName.Text.Trim();
        }

        // ---------- Numeric input ----------
        private void Decimal_KeyPress(object sender, KeyPressEventArgs e)
        {
            var tb = (TextBox)sender;
            if (char.IsControl(e.KeyChar) || char.IsDigit(e.KeyChar)) return;
            if (e.KeyChar == '.' && !tb.Text.Contains(".")) return;
            e.Handled = true;
        }

        private void FormatMoney_Leave(object sender, EventArgs e)
        {
            var tb = (TextBox)sender;
            if (TryParse(tb.Text, out decimal v)) tb.Text = v.ToString("0.00", Ci);
        }

        private static bool TryParse(string s, out decimal v) =>
            decimal.TryParse(s, NumberStyles.Number, Ci, out v);

        // ---------- Calculations ----------
        // Price = Cost + (Cost * Markup / 100)
        private void CostOrMarkup_TextChanged(object sender, EventArgs e)
        {
            if (_updating) return;
            if (TryParse(txtCost.Text, out decimal cost) && TryParse(txtMarkup.Text, out decimal markup))
            {
                _updating = true;
                txtPrice.Text = (cost + cost * markup / 100m).ToString("0.00", Ci);
                _updating = false;
            }
        }

        // Editing Price directly recalculates Markup = (Price - Cost) / Cost * 100
        private void txtPrice_TextChanged(object sender, EventArgs e)
        {
            if (_updating) return;
            if (TryParse(txtCost.Text, out decimal cost) && cost > 0 && TryParse(txtPrice.Text, out decimal price))
            {
                _updating = true;
                txtMarkup.Text = ((price - cost) / cost * 100m).ToString("0.##", Ci);
                _updating = false;
            }
        }

        private void chkActive_CheckedChanged(object sender, EventArgs e)
        {
            chkActive.Text = chkActive.Checked ? "Active" : "Inactive";
        }

        // ---------- Buttons ----------
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs(out string error, out Control bad))
            {
                MessageBox.Show(error, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                bad?.Focus();
                return;
            }

            var p = new Product
            {
                Barcode = txtBarcode.Text,
                Name = txtName.Text.Trim(),
                Description = txtDescription.Text.Trim(),
                Category = cboCategory.SelectedItem.ToString(),
                Origin = rbLocal.Checked ? "Local" : "Abroad",
                IsActive = chkActive.Checked,
                Cost = decimal.Parse(txtCost.Text, Ci),
                Price = decimal.Parse(txtPrice.Text, Ci),
                Markup = decimal.Parse(txtMarkup.Text, Ci)
            };
            _products.Add(p);

            MessageBox.Show(
                $"Product saved!\n\nBarcode: {p.Barcode}\nName: {p.Name}\nCategory: {p.Category}\n" +
                $"Origin: {p.Origin}\nStatus: {(p.IsActive ? "Active" : "Inactive")}\n" +
                $"Cost: {p.Cost:N2}\nPrice: {p.Price:N2}\nMarkup: {p.Markup}%\n\nTotal products: {_products.Count}",
                "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ResetForm();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Clear all fields?", "Clear", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                ResetForm();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Close the form? Unsaved data will be lost.", "Cancel",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                Close();
        }

        // ---------- Helpers ----------
        private bool ValidateInputs(out string error, out Control bad)
        {
            error = ""; bad = null;

            if (txtBarcode.Text.Length < 8)
            { error = "Barcode must be 8 to 13 digits."; bad = txtBarcode; return false; }
            if (_products.Any(p => p.Barcode == txtBarcode.Text))
            { error = "Barcode already exists."; bad = txtBarcode; return false; }
            if (string.IsNullOrWhiteSpace(txtName.Text))
            { error = "Name is required."; bad = txtName; return false; }
            if (string.IsNullOrWhiteSpace(txtDescription.Text))
            { error = "Description is required."; bad = txtDescription; return false; }
            if (cboCategory.SelectedIndex < 0)
            { error = "Please select a category."; bad = cboCategory; return false; }
            if (!TryParse(txtCost.Text, out decimal cost) || cost <= 0)
            { error = "Cost must be a number greater than 0."; bad = txtCost; return false; }
            if (!TryParse(txtMarkup.Text, out decimal markup) || markup < 0)
            { error = "Markup must be 0 or higher."; bad = txtMarkup; return false; }
            if (!TryParse(txtPrice.Text, out decimal price) || price < cost)
            { error = "Price must be a number and not lower than Cost."; bad = txtPrice; return false; }

            return true;
        }

        private void ResetForm()
        {
            _updating = true;
            txtBarcode.Clear(); txtName.Clear(); txtDescription.Clear();
            txtCost.Clear(); txtPrice.Clear(); txtMarkup.Clear();
            cboCategory.SelectedIndex = -1;
            rbLocal.Checked = true;
            chkActive.Checked = true; chkActive.Text = "Active";
            _updating = false;
            txtBarcode_TextChanged(null, EventArgs.Empty);
            txtBarcode.Focus();
        }
    }

    public class Product
    {
        public string Barcode { get; set; } = "";
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public string Category { get; set; } = "";
        public string Origin { get; set; } = "";
        public bool IsActive { get; set; }
        public decimal Cost { get; set; }
        public decimal Price { get; set; }
        public decimal Markup { get; set; }
    }
}