using System.Drawing;
using System.Windows.Forms;

namespace Tarzo_Week05_Work04
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblBarcode = new Label(); txtBarcode = new TextBox(); lblBarcodeStatus = new Label();
            lblName = new Label(); txtName = new TextBox();
            lblDescription = new Label(); txtDescription = new TextBox();
            lblCategory = new Label(); cboCategory = new ComboBox();
            lblOrigin = new Label(); grpOrigin = new GroupBox(); rbLocal = new RadioButton(); rbAbroad = new RadioButton();
            lblStatus = new Label(); chkActive = new CheckBox();
            lblCost = new Label(); txtCost = new TextBox();
            lblPrice = new Label(); txtPrice = new TextBox();
            lblMarkup = new Label(); txtMarkup = new TextBox();
            btnCancel = new Button(); btnClear = new Button(); btnSave = new Button();
            grpOrigin.SuspendLayout();
            SuspendLayout();

           
            lblBarcode.Text = "Barcode"; lblBarcode.Location = new Point(15, 20); lblBarcode.AutoSize = true;
            lblName.Text = "Name"; lblName.Location = new Point(15, 78); lblName.AutoSize = true;
            lblDescription.Text = "Description"; lblDescription.Location = new Point(15, 108); lblDescription.AutoSize = true;
            lblCategory.Text = "Category"; lblCategory.Location = new Point(15, 197); lblCategory.AutoSize = true;
            lblOrigin.Text = "Origin"; lblOrigin.Location = new Point(15, 232); lblOrigin.AutoSize = true;
            lblStatus.Text = "Status"; lblStatus.Location = new Point(15, 295); lblStatus.AutoSize = true;
            lblCost.Text = "Cost"; lblCost.Location = new Point(15, 335); lblCost.AutoSize = true;
            lblPrice.Text = "Price"; lblPrice.Location = new Point(125, 335); lblPrice.AutoSize = true;
            lblMarkup.Text = "Markup (%)"; lblMarkup.Location = new Point(285, 335); lblMarkup.AutoSize = true;

           
            txtBarcode.Location = new Point(110, 17); txtBarcode.Size = new Size(285, 23); txtBarcode.MaxLength = 13;
            txtBarcode.TextChanged += txtBarcode_TextChanged;
            txtBarcode.KeyPress += DigitsOnly_KeyPress;
            lblBarcodeStatus.Location = new Point(110, 43); lblBarcodeStatus.AutoSize = true;
            lblBarcodeStatus.Font = new Font("Segoe UI", 8F, FontStyle.Italic);

           
            txtName.Location = new Point(110, 75); txtName.Size = new Size(285, 23); txtName.MaxLength = 100;
            txtName.Leave += txtName_Leave;
            txtDescription.Location = new Point(110, 105); txtDescription.Size = new Size(285, 80);
            txtDescription.Multiline = true; txtDescription.ScrollBars = ScrollBars.Vertical; txtDescription.MaxLength = 250;

            
            cboCategory.Location = new Point(110, 194); cboCategory.Size = new Size(285, 23);
            cboCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCategory.Items.AddRange(new object[] { "Beverages", "Electronics", "Food", "Furniture", "Clothing", "Personal Care", "Others" });

            
            grpOrigin.Text = "Manufactured"; grpOrigin.Location = new Point(110, 225); grpOrigin.Size = new Size(285, 55);
            grpOrigin.Controls.Add(rbLocal); grpOrigin.Controls.Add(rbAbroad);
            rbLocal.Text = "Local"; rbLocal.Location = new Point(20, 22); rbLocal.AutoSize = true; rbLocal.Checked = true;
            rbAbroad.Text = "Abroad"; rbAbroad.Location = new Point(130, 22); rbAbroad.AutoSize = true;

            
            chkActive.Text = "Active"; chkActive.Location = new Point(110, 293); chkActive.AutoSize = true;
            chkActive.CheckedChanged += chkActive_CheckedChanged;

            
            txtCost.Location = new Point(15, 355); txtCost.Size = new Size(95, 23);
            txtPrice.Location = new Point(125, 355); txtPrice.Size = new Size(140, 23);
            txtMarkup.Location = new Point(285, 355); txtMarkup.Size = new Size(110, 23);
            txtCost.TextChanged += CostOrMarkup_TextChanged;
            txtMarkup.TextChanged += CostOrMarkup_TextChanged;
            txtPrice.TextChanged += txtPrice_TextChanged;
            txtCost.KeyPress += Decimal_KeyPress; txtPrice.KeyPress += Decimal_KeyPress; txtMarkup.KeyPress += Decimal_KeyPress;
            txtCost.Leave += FormatMoney_Leave; txtPrice.Leave += FormatMoney_Leave;

            
            btnCancel.Text = "Cancel"; btnCancel.Location = new Point(15, 410); btnCancel.Size = new Size(85, 30);
            btnClear.Text = "Clear"; btnClear.Location = new Point(125, 410); btnClear.Size = new Size(85, 30);
            btnSave.Text = "Save"; btnSave.Location = new Point(285, 410); btnSave.Size = new Size(110, 30);
            btnSave.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnCancel.Click += btnCancel_Click; btnClear.Click += btnClear_Click; btnSave.Click += btnSave_Click;

          
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(415, 460);
            Text = "Product Form";
            FormBorderStyle = FormBorderStyle.FixedSingle; MaximizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            AcceptButton = btnSave; CancelButton = btnCancel;
            Controls.AddRange(new Control[] {
                lblBarcode, txtBarcode, lblBarcodeStatus, lblName, txtName, lblDescription, txtDescription,
                lblCategory, cboCategory, lblOrigin, grpOrigin, lblStatus, chkActive,
                lblCost, txtCost, lblPrice, txtPrice, lblMarkup, txtMarkup, btnCancel, btnClear, btnSave });
            grpOrigin.ResumeLayout(false); grpOrigin.PerformLayout();
            ResumeLayout(false); PerformLayout();
        }

        private Label lblBarcode, lblBarcodeStatus, lblName, lblDescription, lblCategory, lblOrigin, lblStatus, lblCost, lblPrice, lblMarkup;
        private TextBox txtBarcode, txtName, txtDescription, txtCost, txtPrice, txtMarkup;
        private ComboBox cboCategory;
        private GroupBox grpOrigin;
        private RadioButton rbLocal, rbAbroad;
        private CheckBox chkActive;
        private Button btnCancel, btnClear, btnSave;
    }
}