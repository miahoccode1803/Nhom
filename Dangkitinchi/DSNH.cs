using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dangkitinchi
{
    public partial class DSNH : Form
    {
        public DSNH()
        {
            InitializeComponent();
        }

        private void btnXuat_Click(object sender, EventArgs e)
        {

            Microsoft.Office.Interop.Excel.Application xcelApp = new Microsoft.Office.Interop.Excel.Application();
            xcelApp.Application.Workbooks.Add(Type.Missing);

            // Ghi tiêu đề cột
            for (int i = 1; i < dgvDSNH.Columns.Count + 1; i++)
            {
                xcelApp.Cells[1, i] = dgvDSNH.Columns[i - 1].HeaderText;
            }

            // Ghi dữ liệu từ DataGridView vào Excel
            for (int i = 0; i < dgvDSNH.Rows.Count; i++)
            {
                for (int j = 0; j < dgvDSNH.Columns.Count; j++)
                {
                    // Lấy giá trị trực tiếp từ ô DataGridView và ghi vào ô Excel
                    string cellValue = dgvDSNH.Rows[i].Cells[j].Value != null
                        ? dgvDSNH.Rows[i].Cells[j].Value.ToString()
                        : string.Empty;

                    xcelApp.Cells[i + 2, j + 1] = cellValue;
                }
            }

            // Tự động điều chỉnh kích thước cột
            xcelApp.Columns.AutoFit();
            xcelApp.Visible = true;
        }
        private string manganh;
        private void btnThem_Click(object sender, EventArgs e)
        {
           Nganh nganh = new Nganh(manganh);
            nganh.ShowDialog();

        }

        private void dgvDSNH_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvDSNH_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string manganh = dgvDSNH.Rows[e.RowIndex].Cells["Mã ngành"].Value.ToString();
            new Nganh(manganh).ShowDialog();
            LoadDSNH();
        }


        private string tukhoa = "";
        private string khoa = "";
        private void LoadDSNH()
        {
            tukhoa = txtTukhoa.Text;
            khoa = cboKhoa.SelectedItem?.ToString() ?? "";

            List<CustomParamater> lstPara = new List<CustomParamater>();
            lstPara.Add(new CustomParamater()
            {
                key = "@tukhoa",
                value = tukhoa
            });
            lstPara.Add(new CustomParamater()
            {
                key = "@tenkhoa",
                value = khoa
            }); ;
           

            var db = new Database_store();
            dgvDSNH.DataSource = db.SelectData("SelectAllNH", lstPara);
        }

        private bool comboBoxesLoaded = false;
        private void LoadDataToComboBoxes()
        {
            comboBoxesLoaded = true;

            // Đổ dữ liệu vào ComboBox Nganh
            List<string> khoaList = GetDistinctKhoa();
            khoaList.Insert(0, ""); // Thêm phần tử rỗng vào đầu danh sách
            cboKhoa.DataSource = khoaList;

        }
        private List<string> GetDistinctKhoa()
        {
            var db = new Database_store();
            DataTable dataTable = db.SelectData_SQL("SELECT distinct tenkhoa FROM Khoa");
            return dataTable.AsEnumerable().Select(row => row.Field<string>("tenkhoa")).ToList();
        }

        private void DSNH_Load(object sender, EventArgs e)
        {
            LoadDataToComboBoxes();
            LoadDSNH();
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            if (comboBoxesLoaded)
            {
                tukhoa = txtTukhoa.Text;
                khoa = cboKhoa.SelectedItem?.ToString();
                
                LoadDSNH();
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            tukhoa = txtTukhoa.Text;
            LoadDSNH();
        }
    }
}
