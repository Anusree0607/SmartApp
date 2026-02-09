using System.Net.Http.Json;
using SmartBusinessApp.Domain;
namespace SmartBusinessApp.UI
#nullable disable
{

    public partial class MainBillingForm : Form
    {
        public MainBillingForm()
        {
            InitializeComponent();

            // Read-only columns
            dgvBillItems.Columns["colRate"].ReadOnly = true;
            dgvBillItems.Columns["colGross"].ReadOnly = true;
            dgvBillItems.Columns["colTaxPercent"].ReadOnly = true;
            dgvBillItems.Columns["colTaxAmount"].ReadOnly = true;
            dgvBillItems.Columns["colNetAmount"].ReadOnly = true;

            // No automatic new row
            dgvBillItems.AllowUserToAddRows = false;

            // Start with one empty row
            if (dgvBillItems.Rows.Count == 0)
            {
                dgvBillItems.Rows.Add();
            }

            // Set S.No 1 for initial row
            if (dgvBillItems.Rows.Count > 0)
            {
                dgvBillItems.Rows[0].Cells["colSNo"].Value = "1";
            }

            // Qty alignment & numeric check
            dgvBillItems.Columns["colQty"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvBillItems.EditingControlShowing += dgvBillItems_EditingControlShowing;

            // Events
            dgvBillItems.RowsAdded += dgvBillItems_RowsAdded;
            dgvBillItems.KeyDown += dgvBillItems_KeyDown;
            dgvBillItems.CellValueChanged += dgvBillItems_CellValueChanged;
            dgvBillItems.CurrentCellDirtyStateChanged += dgvBillItems_CurrentCellDirtyStateChanged;

            // Load data on start
            this.Load += MainBillingForm_Load;

            btnSave.Visible = true;
            btnSave.Enabled = true;
        }

        private void dgvBillItems_EditingControlShowing(object? sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvBillItems.CurrentCell?.ColumnIndex == dgvBillItems.Columns["colQty"].Index)
            {
                if (e.Control is TextBox tb)
                {
                    tb.KeyPress -= TextBoxNumeric_KeyPress;
                    tb.KeyPress += TextBoxNumeric_KeyPress;
                }
            }
        }

        private void TextBoxNumeric_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void dgvBillItems_RowsAdded(object? sender, DataGridViewRowsAddedEventArgs e)
        {
            for (int i = 0; i < e.RowCount; i++)
            {
                int idx = e.RowIndex + i;
                if (idx >= 0 && idx < dgvBillItems.Rows.Count)
                {
                    dgvBillItems.Rows[idx].Cells["colSNo"].Value = (idx + 1).ToString();
                }
            }
        }

        private void dgvBillItems_CurrentCellDirtyStateChanged(object? sender, EventArgs e)
        {
            if (dgvBillItems.IsCurrentCellDirty)
            {
                dgvBillItems.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dgvBillItems_CellValueChanged(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            string col = dgvBillItems.Columns[e.ColumnIndex].Name;

            if (col == "colProduct")
            {
                if (dgvBillItems.Rows[e.RowIndex].Cells["colProduct"].Value is int pid)
                {
                    var p = GetProductById(pid);
                    if (p != null)
                    {
                        dgvBillItems.Rows[e.RowIndex].Cells["colRate"].Value = p.Rate.ToString("N2");
                        dgvBillItems.Rows[e.RowIndex].Cells["colTaxPercent"].Value = p.TaxPercent.ToString("N2");
                        RecalculateRow(e.RowIndex);
                    }
                }
            }
            else if (col == "colQty")
            {
                RecalculateRow(e.RowIndex);
            }
        }

        private void dgvBillItems_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                var row = dgvBillItems.CurrentRow;
                if (row == null || row.Index != dgvBillItems.Rows.Count - 1)
                    return;

                bool hasData = row.Cells["colProduct"].Value != null ||
                               (row.Cells["colQty"].Value != null &&
                                int.TryParse(row.Cells["colQty"].Value.ToString(), out int q) && q > 0);

                if (hasData)
                {
                    int newIdx = dgvBillItems.Rows.Add();
                    dgvBillItems.CurrentCell = dgvBillItems.Rows[newIdx].Cells["colProduct"];
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                }
            }
        }

        private ProductItem GetProductById(int id)
        {
            var list = (List<ProductItem>)((DataGridViewComboBoxColumn)dgvBillItems.Columns["colProduct"]).DataSource;
            return list?.FirstOrDefault(p => p.Id == id);
        }

        private void RecalculateRow(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= dgvBillItems.Rows.Count) return;

            var row = dgvBillItems.Rows[rowIndex];

            if (decimal.TryParse(row.Cells["colQty"].Value?.ToString(), out decimal qty) &&
                decimal.TryParse(row.Cells["colRate"].Value?.ToString(), out decimal rate) &&
                decimal.TryParse(row.Cells["colTaxPercent"].Value?.ToString(), out decimal taxPct))
            {
                decimal g = qty * rate;
                decimal t = g * (taxPct / 100);
                decimal n = g + t;

                row.Cells["colGross"].Value = g.ToString("N2");
                row.Cells["colTaxAmount"].Value = t.ToString("N2");
                row.Cells["colNetAmount"].Value = n.ToString("N2");
            }
        }

        private async void MainBillingForm_Load(object? sender, EventArgs e)
        {
            await Task.WhenAll(
                LoadProductsFromApiAsync(),
                LoadBranchesAsync(),
                LoadCustomersAsync()
            );
        }

        private async Task LoadProductsFromApiAsync()
        {
            try
            {
                using var client = new HttpClient { BaseAddress = new Uri("https://localhost:7177/") };
                var list = await client.GetFromJsonAsync<List<ProductItem>>("api/product");
                if (list?.Count > 0)
                {
                    var col = (DataGridViewComboBoxColumn)dgvBillItems.Columns["colProduct"];
                    col.DataSource = list;
                    col.DisplayMember = "Name";
                    col.ValueMember = "Id";
                    col.FlatStyle = FlatStyle.Flat;
                }
                else
                {
                    MessageBox.Show("No products from API.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Products load failed:\n{ex.Message}");
            }
        }

        private async Task LoadBranchesAsync()
        {
            try
            {
                using var client = new HttpClient { BaseAddress = new Uri("https://localhost:7177/") };
                var list = await client.GetFromJsonAsync<List<BranchItem>>("api/branch");
                if (list?.Count > 0)
                {
                    cmbBranch.DataSource = list;
                    cmbBranch.DisplayMember = "Name";
                    cmbBranch.ValueMember = "Id";
                    cmbBranch.SelectedIndex = -1;
                }
                else
                {
                    MessageBox.Show("No branches from API.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Branches load failed:\n{ex.Message}");
            }
        }

        private async Task LoadCustomersAsync()
        {
            try
            {
                using var client = new HttpClient { BaseAddress = new Uri("https://localhost:7177/") };
                var list = await client.GetFromJsonAsync<List<CustomerItem>>("api/customer");
                if (list?.Count > 0)
                {
                    cmbCustomer.DataSource = list;
                    cmbCustomer.DisplayMember = "Name";
                    cmbCustomer.ValueMember = "Id";
                    cmbCustomer.SelectedIndex = -1;
                }
                else
                {
                    MessageBox.Show("No customers from API.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Customers load failed:\n{ex.Message}");
            }
        }

        private async void label1_Click(object? sender, EventArgs e)
        {
            // Immediate feedback that the click was registered
            MessageBox.Show("Save button clicked! Starting process...", "Debug Info");

            var items = new List<BillItemDto>();

            foreach (DataGridViewRow row in dgvBillItems.Rows)
            {
                // Skip completely empty rows
                if (row.Cells["colProduct"].Value == null && row.Cells["colQty"].Value == null)
                    continue;

                // Parse product ID
                if (!int.TryParse(row.Cells["colProduct"].Value?.ToString(), out int pid))
                    continue;

                // Parse quantity and check > 0
                if (!int.TryParse(row.Cells["colQty"].Value?.ToString(), out int qty) || qty <= 0)
                    continue;

                // Parse calculated fields (should already be filled by RecalculateRow)
                if (!decimal.TryParse(row.Cells["colRate"].Value?.ToString(), out decimal rate) ||
                    !decimal.TryParse(row.Cells["colTaxPercent"].Value?.ToString(), out decimal tp) ||
                    !decimal.TryParse(row.Cells["colGross"].Value?.ToString(), out decimal g) ||
                    !decimal.TryParse(row.Cells["colTaxAmount"].Value?.ToString(), out decimal ta) ||
                    !decimal.TryParse(row.Cells["colNetAmount"].Value?.ToString(), out decimal n))
                {
                    continue;
                }

                items.Add(new BillItemDto
                {
                    ProductId = pid,
                    Quantity = qty,
                    Rate = rate,
                    TaxPercent = tp,
                    Gross = g,
                    TaxAmount = ta,
                    NetAmount = n
                });
            }

            // Show how many items were actually collected
            MessageBox.Show($"Found {items.Count} valid bill items.", "Debug Info");

            if (items.Count == 0)
            {
                MessageBox.Show("No valid items found in the grid!\nPlease add at least one product with quantity > 0.",
                                "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbBranch.SelectedValue == null || cmbCustomer.SelectedValue == null)
            {
                MessageBox.Show("Please select both Branch and Customer before saving.",
                                "Required Fields", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var bill = new BillDto
            {
                BillDate = dtpBillDate.Value,
                BranchId = Convert.ToInt32(cmbBranch.SelectedValue),
                CustomerId = Convert.ToInt32(cmbCustomer.SelectedValue),
                Items = items
            };

            MessageBox.Show("Bill data prepared. Attempting to save to API...", "Debug Info");

            try
            {
                using var client = new HttpClient { BaseAddress = new Uri("https://localhost:7177/") };

                // Optional: add timeout to prevent hanging forever
                client.Timeout = TimeSpan.FromSeconds(30);

                var resp = await client.PostAsJsonAsync("api/bill", bill);

                if (resp.IsSuccessStatusCode)
                {
                    var res = await resp.Content.ReadFromJsonAsync<Dictionary<string, object>>();
                    string bid = res?.ContainsKey("billId") == true
                        ? res["billId"]?.ToString() ?? "unknown"
                        : "unknown";

                    MessageBox.Show($"Bill saved successfully!\nBill ID: {bid}",
                                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    string errorContent = await resp.Content.ReadAsStringAsync();
                    MessageBox.Show($"Save failed!\nStatus: {resp.StatusCode}\nDetails: {errorContent}",
                                    "API Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (HttpRequestException httpEx) when (httpEx.InnerException is System.Net.Sockets.SocketException)
            {
                MessageBox.Show("Connection failed.\nMake sure the API is running on https://localhost:7177\n\n" +
                                httpEx.Message, "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (TaskCanceledException)
            {
                MessageBox.Show("Request timed out. The API might be slow or not responding.",
                                "Timeout", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected error while saving:\n{ex.Message}\n\n{ex.StackTrace}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label4_Click(object? sender, EventArgs e) { }
        private void txtGross(object? sender, EventArgs e) { }
        private void label5_Click(object? sender, EventArgs e) { }
        private void dataGridView1_AutoSizeColumnsModeChanged(object? sender, DataGridViewAutoSizeColumnsModeEventArgs e) { }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
