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
    public partial class DSMH : Form
    {
        public DSMH()
        {
            InitializeComponent();
        }

        private void DSMH_Load(object sender, EventArgs e)
        {
            LoadDataToComboBoxes();
            LoadDSMH();
        }

        private string tukhoa = "";
        private string nganh = "";
        private string mahp = "";
        private void LoadDSMH()
        {
            tukhoa = txtTukhoa.Text;
            mahp = cboMamh.SelectedItem?.ToString() ?? "";

            List<CustomParamater> lstPara = new List<CustomParamater>();
            lstPara.Add(new CustomParamater()
            {
                key = "@tukhoa",
                value = tukhoa
            });
            lstPara.Add(new CustomParamater()
            {
                key = "@mahocphan",
                value = mahp
            });

            var db = new Database_store();
            dgvDSMH.DataSource = db.SelectData("SelectAllMH", lstPara);
        }



        private void btnXuat_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application xcelApp = new Microsoft.Office.Interop.Excel.Application();
            xcelApp.Application.Workbooks.Add(Type.Missing);

            // Ghi tiêu đề cột
            for (int i = 1; i < dgvDSMH.Columns.Count + 1; i++)
            {
                xcelApp.Cells[1, i] = dgvDSMH.Columns[i - 1].HeaderText;
            }

            // Ghi dữ liệu từ DataGridView vào Excel
            for (int i = 0; i < dgvDSMH.Rows.Count; i++)
            {
                for (int j = 0; j < dgvDSMH.Columns.Count; j++)
                {
                    // Lấy giá trị trực tiếp từ ô DataGridView và ghi vào ô Excel
                    string cellValue = dgvDSMH.Rows[i].Cells[j].Value != null
                        ? dgvDSMH.Rows[i].Cells[j].Value.ToString()
                        : string.Empty;

                    xcelApp.Cells[i + 2, j + 1] = cellValue;
                }
            }

            // Tự động điều chỉnh kích thước cột
            xcelApp.Columns.AutoFit();
            xcelApp.Visible = true;
        }

        private bool comboBoxesLoaded = false;
        private void LoadDataToComboBoxes()
        {
            comboBoxesLoaded = true;

            // Đổ dữ liệu vào ComboBox Tenhocphan
            List<string> tenhocphanList = GetDistincthocphan();
            tenhocphanList.Insert(0, ""); // Thêm phần tử rỗng vào đầu danh sách
            cboMamh.DataSource = tenhocphanList;


        }


        private List<string> GetDistincthocphan()
        {
            var db = new Database_store();
            DataTable dataTable = db.SelectData_SQL("SELECT  mamh FROM monhoc");
            return dataTable.AsEnumerable().Select(row => row.Field<string>("mamh")).ToList();
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            if (comboBoxesLoaded)
            {
                tukhoa = txtTukhoa.Text;
                mahp = cboMamh.SelectedItem?.ToString();
                LoadDSMH();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra xem có đúng một dòng được chọn không
                if (dgvDSMH.SelectedRows.Count == 1)
                {
                    // Lấy ID từ cột "ID" của dòng được chọn
                    string ID = dgvDSMH.SelectedRows[0].Cells["Mã học phần"].Value.ToString();

                    // Hiển thị MessageBox hỏi người dùng có chắc chắn muốn xóa không
                    DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa môn học này không?", "Xác nhận xóa", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                    if (result == DialogResult.OK)
                    {
                        // Tạo danh sách tham số để truyền vào phương thức xóa
                        List<CustomParamater> lstPara = new List<CustomParamater>();
                        lstPara.Add(new CustomParamater()
                        {
                            key = "@mahocphan",
                            value = ID
                        });

                        var db = new Database_store();

                        // Thực hiện phương thức xóa với tham số ID
                        int rowsAffected = db.DeleteData("DeleteMH", lstPara);

                        // Kiểm tra số dòng bị ảnh hưởng, nếu lớn hơn 0 có nghĩa là xóa thành công
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Xóa môn học thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            // Sau khi xóa thành công, làm mới danh sách môn học
                            LoadDSMH();
                        }
                        else
                        {
                            MessageBox.Show("môn học không tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một môn học để xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xóa môn học: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            new Monhoc(null).ShowDialog();
        }

        private void txtTukhoa_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTukhoa.Text))
            {
                LoadDSMH();
            }
        }

        private void dgvDSMH_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string mamh = dgvDSMH.Rows[e.RowIndex].Cells["Mã học phần"].Value.ToString();
            new Monhoc(mamh).ShowDialog();
            LoadDSMH();
        }

        private void dgvDSMH_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
