using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using ItemDataLibrary.Model;

namespace ItemFormsUI
{
    public partial class InventoryForm : Form
    {
        private readonly HttpClient _httpClient;
        private bool _isDataLoaded = false;

        public InventoryForm()
        {
            InitializeComponent();
            _httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:7287/") };
            StyleForm();

            dgvItems.SelectionChanged += DgvItems_SelectionChanged;
            dgvItems.CellBeginEdit += DgvItems_CellBeginEdit;
            dgvItems.DataBindingComplete += DgvItems_DataBindingComplete;
        }

        private void DgvItems_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            e.Cancel = true;
        }

        private void DgvItems_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvItems.ClearSelection();
            dgvItems.CurrentCell = null;
            _isDataLoaded = true;
        }

        private void DgvItems_SelectionChanged(object sender, EventArgs e)
        {
            if (!_isDataLoaded) return;

            if (dgvItems.SelectedRows.Count > 0)
            {
                var item = dgvItems.SelectedRows[0].DataBoundItem as ItemModel;
                if (item != null)
                {
                    txtName.Text = item.Name;
                    txtCode.Text = item.Code;
                    txtBrand.Text = item.Brand;
                    txtUnitPrice.Text = item.UnitPrice.ToString();
                    txtQuantity.Text = item.Quantity.ToString();
                }
            }
            else
            {
                ClearFields();
            }
        }

        private async void InventoryForm_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            await LoadItemsAsync();
        }

        private void SetupDataGridView()
        {
            dgvItems.AutoGenerateColumns = false;
            dgvItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvItems.MultiSelect = false;
            dgvItems.Columns.Clear();

            dgvItems.RowHeadersVisible = false;

            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Name", DataPropertyName = "Name", Width = 150 });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Code", DataPropertyName = "Code", Width = 100 });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Brand", DataPropertyName = "Brand", Width = 120 });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Unit Price", DataPropertyName = "UnitPrice", Width = 100 });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Quantity", DataPropertyName = "Quantity", Width = 80 });

            dgvItems.ReadOnly = true;
            foreach (DataGridViewColumn c in dgvItems.Columns)
                c.ReadOnly = true;
        }

        private async Task LoadItemsAsync()
        {
            var response = await _httpClient.GetAsync("api/items");
            if (response.IsSuccessStatusCode)
            {
                var items = await response.Content.ReadFromJsonAsync<List<ItemModel>>();
                dgvItems.DataSource = items;
            }
            else
            {
                MessageBox.Show("Failed to load items");
            }
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtCode.Text))
            {
                MessageBox.Show("Name and Code are required!");
                return;
            }

            decimal.TryParse(txtUnitPrice.Text, out decimal price);
            int.TryParse(txtQuantity.Text, out int qty);

            var newItem = new ItemModel
            {
                Name = txtName.Text,
                Code = txtCode.Text,
                Brand = txtBrand.Text,
                UnitPrice = price,
                Quantity = qty
            };

            var json = JsonSerializer.Serialize(newItem);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/items", content);
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Item added successfully!");
                ClearFields();
                await LoadItemsAsync();
            }
            else
            {
                MessageBox.Show("Error adding item: " + response.StatusCode);
            }
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvItems.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an item to update.");
                return;
            }

            var selectedItem = dgvItems.SelectedRows[0].DataBoundItem as ItemModel;
            if (selectedItem == null)
            {
                MessageBox.Show("Selected item is invalid.");
                return;
            }

            decimal.TryParse(txtUnitPrice.Text, out decimal price);
            int.TryParse(txtQuantity.Text, out int qty);

            selectedItem.Name = txtName.Text;
            selectedItem.Brand = txtBrand.Text;
            selectedItem.UnitPrice = price;
            selectedItem.Quantity = qty;

            string itemCode = selectedItem.Code;

            var json = JsonSerializer.Serialize(selectedItem);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            try
            {
                var response = await _httpClient.PutAsync($"api/items/{itemCode}", content);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Item updated successfully!");
                    ClearFields();
                    await LoadItemsAsync();
                }
                else
                {
                    MessageBox.Show("Error updating item: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception updating item: " + ex.Message);
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvItems.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an item to delete.");
                return;
            }

            var selectedItem = dgvItems.SelectedRows[0].DataBoundItem as ItemModel;
            if (selectedItem == null)
            {
                MessageBox.Show("Selected item is invalid.");
                return;
            }

            try
            {
                var response = await _httpClient.DeleteAsync($"api/items/{selectedItem.Code}");
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Item deleted successfully!");
                    ClearFields();
                    await LoadItemsAsync();
                }
                else
                {
                    MessageBox.Show("Error deleting item: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception deleting item: " + ex.Message);
            }
        }

        private void ClearFields()
        {
            txtName.Text = "";
            txtCode.Text = "";
            txtBrand.Text = "";
            txtUnitPrice.Text = "";
            txtQuantity.Text = "";
        }

        private void StyleForm()
        {
            this.BackColor = Color.Black;
            this.ForeColor = Color.White;
            this.Font = new Font("Segoe UI", 10, FontStyle.Regular);

            labelTitle.ForeColor = Color.White;
            labelTitle.BackColor = Color.Black;

            dgvItems.BackgroundColor = Color.FromArgb(25, 25, 25);
            dgvItems.BorderStyle = BorderStyle.None;
            dgvItems.EnableHeadersVisualStyles = false;
            dgvItems.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(45, 45, 45);
            dgvItems.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvItems.DefaultCellStyle.BackColor = Color.FromArgb(30, 30, 30);
            dgvItems.DefaultCellStyle.ForeColor = Color.White;
            dgvItems.DefaultCellStyle.SelectionBackColor = Color.DimGray;
            dgvItems.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvItems.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(28, 28, 28);

            groupBoxDetails.BackColor = Color.FromArgb(20, 20, 20);
            groupBoxDetails.ForeColor = Color.White;

            foreach (Control ctrl in groupBoxDetails.Controls)
            {
                if (ctrl is Label lbl)
                {
                    lbl.ForeColor = Color.White;
                    lbl.BackColor = Color.Transparent;
                }
            }

            TextBox[] txts = { txtName, txtCode, txtBrand, txtUnitPrice, txtQuantity };
            foreach (var t in txts)
            {
                t.BackColor = Color.FromArgb(40, 40, 40);
                t.ForeColor = Color.White;
                t.BorderStyle = BorderStyle.FixedSingle;
            }

            Button[] btns = { btnAdd, btnUpdate, btnDelete };
            foreach (var b in btns)
            {
                b.FlatStyle = FlatStyle.Flat;
                b.FlatAppearance.BorderSize = 1;
                b.FlatAppearance.BorderColor = Color.White;
                b.BackColor = Color.FromArgb(60, 60, 60);
                b.ForeColor = Color.White;
                b.Height = 35;
                b.Width = 100;
                b.MouseEnter += (s, e) => { b.BackColor = Color.FromArgb(90, 90, 90); };
                b.MouseLeave += (s, e) => { b.BackColor = Color.FromArgb(60, 60, 60); };
            }
        }
    }
}
