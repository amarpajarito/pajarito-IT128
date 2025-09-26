namespace ItemFormsUI
{
    partial class InventoryForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.GroupBox groupBoxDetails;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label labelCode;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Label labelBrand;
        private System.Windows.Forms.TextBox txtBrand;
        private System.Windows.Forms.Label labelUnitPrice;
        private System.Windows.Forms.TextBox txtUnitPrice;
        private System.Windows.Forms.Label labelQuantity;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.DataGridView dgvItems;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            labelTitle = new System.Windows.Forms.Label();
            groupBoxDetails = new System.Windows.Forms.GroupBox();
            labelName = new System.Windows.Forms.Label();
            txtName = new System.Windows.Forms.TextBox();
            labelCode = new System.Windows.Forms.Label();
            txtCode = new System.Windows.Forms.TextBox();
            labelBrand = new System.Windows.Forms.Label();
            txtBrand = new System.Windows.Forms.TextBox();
            labelUnitPrice = new System.Windows.Forms.Label();
            txtUnitPrice = new System.Windows.Forms.TextBox();
            labelQuantity = new System.Windows.Forms.Label();
            txtQuantity = new System.Windows.Forms.TextBox();
            btnAdd = new System.Windows.Forms.Button();
            btnUpdate = new System.Windows.Forms.Button();
            btnDelete = new System.Windows.Forms.Button();
            dgvItems = new System.Windows.Forms.DataGridView();

            ((System.ComponentModel.ISupportInitialize)dgvItems).BeginInit();
            groupBoxDetails.SuspendLayout();
            SuspendLayout();

            labelTitle.Dock = System.Windows.Forms.DockStyle.Top;
            labelTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            labelTitle.Text = "School Inventory Management System";
            labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            labelTitle.Height = 50;

            int formWidth = 800;
            int detailsWidth = 460;
            int detailsHeight = 300;  
            groupBoxDetails.Size = new System.Drawing.Size(detailsWidth, detailsHeight);
            groupBoxDetails.Location = new System.Drawing.Point((formWidth - detailsWidth) / 2, 60);
            groupBoxDetails.Text = "Item Details";

            int labelX = 20;
            int textX = 140;
            int startY = 30;
            int gapY = 55;  

            labelName.Text = "Name:";
            labelName.Location = new System.Drawing.Point(labelX, startY);
            labelName.AutoSize = true;
            txtName.Location = new System.Drawing.Point(textX, startY - 2);
            txtName.Width = 285;

            labelCode.Text = "Code:";
            labelCode.Location = new System.Drawing.Point(labelX, startY + gapY);
            labelCode.AutoSize = true;
            txtCode.Location = new System.Drawing.Point(textX, startY + gapY - 2);
            txtCode.Width = 285;

            labelBrand.Text = "Brand:";
            labelBrand.Location = new System.Drawing.Point(labelX, startY + 2 * gapY);
            labelBrand.AutoSize = true;
            txtBrand.Location = new System.Drawing.Point(textX, startY + 2 * gapY - 2);
            txtBrand.Width = 285;

            labelUnitPrice.Text = "Unit Price:";
            labelUnitPrice.Location = new System.Drawing.Point(labelX, startY + 3 * gapY);
            labelUnitPrice.AutoSize = true;
            txtUnitPrice.Location = new System.Drawing.Point(textX, startY + 3 * gapY - 2);
            txtUnitPrice.Width = 285;

            labelQuantity.Text = "Quantity:";
            labelQuantity.Location = new System.Drawing.Point(labelX, startY + 4 * gapY);
            labelQuantity.AutoSize = true;
            txtQuantity.Location = new System.Drawing.Point(textX, startY + 4 * gapY - 2);
            txtQuantity.Width = 285;

            int btnW = 100, btnH = 35, gapX = 30;
            int totalButtonsWidth = 3 * btnW + 2 * gapX;
            int btnStartX = (formWidth - totalButtonsWidth) / 2;
            int btnY = groupBoxDetails.Location.Y + groupBoxDetails.Height + 20;

            btnAdd.Text = "Add";
            btnAdd.Location = new System.Drawing.Point(btnStartX, btnY);
            btnAdd.Size = new System.Drawing.Size(btnW, btnH);
            btnAdd.Click += btnAdd_Click;

            btnUpdate.Text = "Update";
            btnUpdate.Location = new System.Drawing.Point(btnStartX + (btnW + gapX), btnY);
            btnUpdate.Size = new System.Drawing.Size(btnW, btnH);
            btnUpdate.Click += btnUpdate_Click;

            btnDelete.Text = "Delete";
            btnDelete.Location = new System.Drawing.Point(btnStartX + 2 * (btnW + gapX), btnY);
            btnDelete.Size = new System.Drawing.Size(btnW, btnH);
            btnDelete.Click += btnDelete_Click;

            int dgvY = btnY + btnH + 25;
            dgvItems.Location = new System.Drawing.Point(20, dgvY);
            dgvItems.Size = new System.Drawing.Size(formWidth - 40, 240);
            dgvItems.Anchor = System.Windows.Forms.AnchorStyles.Top |
                              System.Windows.Forms.AnchorStyles.Bottom |
                              System.Windows.Forms.AnchorStyles.Left |
                              System.Windows.Forms.AnchorStyles.Right;
            dgvItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            Controls.Add(labelTitle);
            Controls.Add(groupBoxDetails);
            Controls.Add(btnAdd);
            Controls.Add(btnUpdate);
            Controls.Add(btnDelete);
            Controls.Add(dgvItems);

            groupBoxDetails.Controls.Add(labelName);
            groupBoxDetails.Controls.Add(txtName);
            groupBoxDetails.Controls.Add(labelCode);
            groupBoxDetails.Controls.Add(txtCode);
            groupBoxDetails.Controls.Add(labelBrand);
            groupBoxDetails.Controls.Add(txtBrand);
            groupBoxDetails.Controls.Add(labelUnitPrice);
            groupBoxDetails.Controls.Add(txtUnitPrice);
            groupBoxDetails.Controls.Add(labelQuantity);
            groupBoxDetails.Controls.Add(txtQuantity);

            this.ClientSize = new System.Drawing.Size(formWidth, 700);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "School Inventory Management System";
            this.Load += InventoryForm_Load;

            ((System.ComponentModel.ISupportInitialize)dgvItems).EndInit();
            groupBoxDetails.ResumeLayout(false);
            groupBoxDetails.PerformLayout();
            ResumeLayout(false);
        }
    }
}
