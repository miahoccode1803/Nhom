using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dangkitinchi
{
    public partial class DSSV : Form
    {
        public DSSV()
        {
            InitializeComponent();
        }

        private void dgvDSSV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DSSV_Load(object sender, EventArgs e)
        {
            LoadDSSV();
        }

        private void dgvDSSV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string msv = dgvDSSV.Rows[e.RowIndex].Cells["Mã SV"].Value.ToString();
            new Sinhvien(msv).ShowDialog();
            LoadDSSV();
        }
        private string tukhoa = "";
        private void LoadDSSV()
        {
            tukhoa = txtTukhoa.Text;
            List <CustomParamater> lstPara = new List <CustomParamater>();
            lstPara.Add(new CustomParamater()
            {
                key = "@tukhoa",
                value = tukhoa
            });
            var db = new Database_store();
            dgvDSSV.DataSource = db.SelectData("SelectAllSV", lstPara);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Sinhvien(null).ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra xem có đúng một dòng được chọn không
                if (dgvDSSV.SelectedRows.Count == 1)
                {
                    // Lấy mã sinh viên từ cột "Mã SV" của dòng được chọn
                    string msv = dgvDSSV.SelectedRows[0].Cells["Mã SV"].Value.ToString();

                    // Hiển thị MessageBox hỏi người dùng có chắc chắn muốn xóa không
                    DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa sinh viên này không?", "Xác nhận xóa", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                    if (result == DialogResult.OK)
                    {
                        // Tạo danh sách tham số để truyền vào phương thức xóa
                        List<CustomParamater> lstPara = new List<CustomParamater>();
                        lstPara.Add(new CustomParamater()
                        {
                            key = "@Masv",
                            value = msv
                        });

                        var db = new Database_store();

                        // Thực hiện phương thức xóa với tham số mã sinh viên
                        int rowsAffected = db.DeleteData("DeleteSinhVien", lstPara);

                        // Kiểm tra số dòng bị ảnh hưởng, nếu lớn hơn 0 có nghĩa là xóa thành công
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Xóa sinh viên thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            // Sau khi xóa thành công, làm mới danh sách sinh viên
                            LoadDSSV();
                        }
                        else
                        {
                            MessageBox.Show("Sinh viên không tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một sinh viên để xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xóa sinh viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtTukhoa_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTukhoa.Text))
            {
                LoadDSSV();
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTukhoa.Text))
            {
                // Nếu có từ khóa, thực hiện tìm kiếm
                LoadDSSV();
            }
            else
            {
                // Nếu từ khóa là rỗng, load lại toàn bộ danh sách sinh viên
                txtTukhoa.Text = ""; // Đặt lại giá trị của txtTukhoa thành rỗng
                LoadDSSV();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application xcelApp = new Microsoft.Office.Interop.Excel.Application();
            xcelApp.Application.Workbooks.Add(Type.Missing);
            for (int i = 1; i < dgvDSSV.Columns.Count + 1; i++)
            {
                xcelApp.Cells[1, i] = dgvDSSV.Columns[i - 1].HeaderText;
            }

            for (int i = 0; i < dgvDSSV.Rows.Count; i++)
            {
                for (int j = 0; j < dgvDSSV.Columns.Count; j++)
                {
                    string cellValue = dgvDSSV.Rows[i].Cells[j].Value != null
                        ? FormatCellValue(dgvDSSV.Columns[j].Name, dgvDSSV.Rows[i].Cells[j].Value.ToString())
                        : string.Empty;

                    xcelApp.Cells[i + 2, j + 1] = cellValue;
                }
            }
            xcelApp.Columns.AutoFit();
            xcelApp.Visible = true;
        }
        private string FormatCellValue(string columnName, string cellValue)
        {
            // Format phone numbers if the column is 'dienthoai'
            if (columnName.Equals("dienthoai", StringComparison.OrdinalIgnoreCase))
            {
                return FormatPhoneNumber(cellValue);
            }

            // Add additional formatting logic for other columns if needed

            // Return the value as is for other columns
            return cellValue;
        }

        private string FormatPhoneNumber(string phoneNumber)
        {
            // Remove non-numeric characters
            string numericPhoneNumber = new string(phoneNumber.Where(char.IsDigit).ToArray());

            // Add leading '0' if the length is less than 10
            if (numericPhoneNumber.Length < 10)
            {
                numericPhoneNumber = "0" + numericPhoneNumber;
            }

            // Format the phone number
            if (numericPhoneNumber.Length == 10)
            {
                return string.Format("{0:#### ### ###}", double.Parse(numericPhoneNumber));
            }

            return numericPhoneNumber; // Return as is if it doesn't match the expected length
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
