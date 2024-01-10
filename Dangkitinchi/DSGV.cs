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
    public partial class DSGV : Form
    {
        public DSGV()
        {
            InitializeComponent();
        }

        private void DSGV_Load(object sender, EventArgs e)
        {
            LoadDSGV();
        }
        private string tukhoa = "";
        private void LoadDSGV()
        {
            tukhoa = txtTukhoa.Text;
            List<CustomParamater> lstPara = new List<CustomParamater>();
            lstPara.Add(new CustomParamater()
            {
                key = "@tukhoa",
                value = tukhoa
            });
            var db = new Database_store();
            dgvDSGV.DataSource = db.SelectData("SelectAllGV", lstPara);
        }

        private void dgvDSGV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string mgv = dgvDSGV.Rows[e.RowIndex].Cells["Mã GV"].Value.ToString();
            new Giaovien(mgv).ShowDialog();
            LoadDSGV();

        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTukhoa.Text))
            {
                // Nếu có từ khóa, thực hiện tìm kiếm
                LoadDSGV();
            }
            else
            {
                // Nếu từ khóa là rỗng, load lại toàn bộ danh sách giáo viên
                txtTukhoa.Text = ""; // Đặt lại giá trị của txtTukhoa thành rỗng
                LoadDSGV();
            }
        }

        private void txtTukhoa_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTukhoa.Text))
            {
                LoadDSGV();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra xem có đúng một dòng được chọn không
                if (dgvDSGV.SelectedRows.Count == 1)
                {
                    // Lấy mã giáo viên từ cột "Mã SV" của dòng được chọn
                    string mgv = dgvDSGV.SelectedRows[0].Cells["Mã GV"].Value.ToString();

                    // Hiển thị MessageBox hỏi người dùng có chắc chắn muốn xóa không
                    DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa giáo viên này không?", "Xác nhận xóa", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                    if (result == DialogResult.OK)
                    {
                        // Tạo danh sách tham số để truyền vào phương thức xóa
                        List<CustomParamater> lstPara = new List<CustomParamater>();
                        lstPara.Add(new CustomParamater()
                        {
                            key = "@Magv",
                            value = mgv
                        });

                        var db = new Database_store();

                        // Thực hiện phương thức xóa với tham số mã giáo viên
                        int rowsAffected = db.DeleteData("DeleteGV", lstPara);

                        // Kiểm tra số dòng bị ảnh hưởng, nếu lớn hơn 0 có nghĩa là xóa thành công
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Xóa giáo viên thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            // Sau khi xóa thành công, làm mới danh sách giáo viên
                            LoadDSGV();
                        }
                        else
                        {
                            MessageBox.Show("giáo viên không tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một giáo viên để xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xóa giáo viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXuat_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application xcelApp = new Microsoft.Office.Interop.Excel.Application();
            xcelApp.Application.Workbooks.Add(Type.Missing);
            for (int i = 1; i < dgvDSGV.Columns.Count + 1; i++)
            {
                xcelApp.Cells[1, i] = dgvDSGV.Columns[i - 1].HeaderText;
            }

            for (int i = 0; i < dgvDSGV.Rows.Count; i++)
            {
                for (int j = 0; j < dgvDSGV.Columns.Count; j++)
                {
                    string cellValue = dgvDSGV.Rows[i].Cells[j].Value != null
                        ? FormatCellValue(dgvDSGV.Columns[j].Name, dgvDSGV.Rows[i].Cells[j].Value.ToString())
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

        private void btnThem_Click(object sender, EventArgs e)
        {
            new Giaovien(null).ShowDialog();
        }
    }
}
