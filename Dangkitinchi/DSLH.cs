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
    public partial class DSLH : Form
    {
        public DSLH()
        {
            InitializeComponent();
        }
        private string connectionString = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
        private void btnXuat_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application xcelApp = new Microsoft.Office.Interop.Excel.Application();
            xcelApp.Application.Workbooks.Add(Type.Missing);

            // Ghi tiêu đề cột
            for (int i = 1; i < dgvDSLH.Columns.Count + 1; i++)
            {
                xcelApp.Cells[1, i] = dgvDSLH.Columns[i - 1].HeaderText;
            }

            // Ghi dữ liệu từ DataGridView vào Excel
            for (int i = 0; i < dgvDSLH.Rows.Count; i++)
            {
                for (int j = 0; j < dgvDSLH.Columns.Count; j++)
                {
                    // Lấy giá trị trực tiếp từ ô DataGridView và ghi vào ô Excel
                    string cellValue = dgvDSLH.Rows[i].Cells[j].Value != null
                        ? dgvDSLH.Rows[i].Cells[j].Value.ToString()
                        : string.Empty;

                    xcelApp.Cells[i + 2, j + 1] = cellValue;
                }
            }

            // Tự động điều chỉnh kích thước cột
            xcelApp.Columns.AutoFit();
            xcelApp.Visible = true;

        }

        private void DSLH_Load(object sender, EventArgs e)
        {
            LoadDataToComboBoxes();
            LoadDataToCboHocki();
            LoadDSLH();
       
        }

        private string tukhoa = "";
        private string hocki = "";
        private string nganh ="";
        private string mahp = "";

        private void LoadDSLH()
        {
            tukhoa = txtTukhoa.Text;
            hocki = cboHocki.SelectedItem?.ToString() ?? "";
            nganh = cboNganh.SelectedItem?.ToString() ?? "";
            mahp = cboMamh.SelectedItem?.ToString() ?? "";

            List<CustomParamater> lstPara = new List<CustomParamater>();
            lstPara.Add(new CustomParamater()
            {
                key = "@tukhoa",
                value = tukhoa
            });
            lstPara.Add(new CustomParamater()
            {
                key = "@tennganh",
                value = nganh
            }); ;
            lstPara.Add(new CustomParamater()
            {
                key = "@hocki",
                value = hocki
            });
            lstPara.Add(new CustomParamater()
            {
                key = "@mahocphan",
                value = mahp
            });

            var db = new Database_store();
            dgvDSLH.DataSource = db.SelectData("SelectAllLH", lstPara);
        }

        private void dgvDSLH_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Lấy giá trị ID của hàng được double click
                DataGridViewRow selectedRow = dgvDSLH.Rows[e.RowIndex];
                string ID = selectedRow.Cells["ID"].Value?.ToString();
                MessageBox.Show("ID truyen vao"+ID);
                // Mở form PhancongLH với ID
                new PhancongLH(ID).ShowDialog();

                // Sau khi đóng form PhancongLH, làm mới danh sách lớp học
                LoadDSLH();
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            new PhancongLH(null).ShowDialog();
            LoadDataToCboHocki();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra xem có đúng một dòng được chọn không
                if (dgvDSLH.SelectedRows.Count == 1) 
                {
                    // Lấy ID từ cột "ID" của dòng được chọn
                    string ID = dgvDSLH.SelectedRows[0].Cells["ID"].Value.ToString();

                    // Hiển thị MessageBox hỏi người dùng có chắc chắn muốn xóa không
                    DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa lớp học này không?", "Xác nhận xóa", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                    if (result == DialogResult.OK)
                    {
                        // Tạo danh sách tham số để truyền vào phương thức xóa
                        List<CustomParamater> lstPara = new List<CustomParamater>();
                        lstPara.Add(new CustomParamater()
                        {
                            key = "@ID",
                            value = ID
                        });

                        var db = new Database_store();

                        // Thực hiện phương thức xóa với tham số ID
                        int rowsAffected = db.DeleteData("DeleteLH", lstPara);

                        // Kiểm tra số dòng bị ảnh hưởng, nếu lớn hơn 0 có nghĩa là xóa thành công
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Xóa lớp học thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            // Sau khi xóa thành công, làm mới danh sách lớp học
                            LoadDSLH();
                            LoadDataToCboHocki();
                        }
                        else
                        {
                            MessageBox.Show("lớp học không tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một lớp học để xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xóa lớp học: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }
        private bool comboBoxesLoaded = false;
        private void LoadDataToComboBoxes()
        {
            comboBoxesLoaded = true;

            // Đổ dữ liệu vào ComboBox Nganh
            List<string> nganhList = GetDistinctNganh();
            nganhList.Insert(0, ""); // Thêm phần tử rỗng vào đầu danh sách
            cboNganh.DataSource = nganhList;

            // Đổ dữ liệu vào ComboBox Tenhocphan
            List<string> tenhocphanList = GetDistincthocphan();
            tenhocphanList.Insert(0, ""); // Thêm phần tử rỗng vào đầu danh sách
            cboMamh.DataSource = tenhocphanList;

          
           
        }


        private List<string> GetDistinctNganh()
        {
            var db = new Database_store();
            DataTable dataTable = db.SelectData_SQL("SELECT DISTINCT tennganh FROM nganh");
            return dataTable.AsEnumerable().Select(row => row.Field<string>("tennganh")).ToList();
        }

        private List<string> GetDistincthocphan()
        {
            var db = new Database_store();
            DataTable dataTable = db.SelectData_SQL("SELECT  mamh FROM monhoc");
            return dataTable.AsEnumerable().Select(row => row.Field<string>("mamh")).ToList();
        }

        private void LoadDataToCboHocki()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    cboHocki.Items.Clear(); // Clear items before adding new ones

                    // Load all distinct hocki values from the Lop table
                    string sqlGetAllHocki = "SELECT DISTINCT Hocki FROM Lop";
                    SqlCommand commandGetAllHocki = new SqlCommand(sqlGetAllHocki, con);
                    SqlDataReader readerAllHocki = commandGetAllHocki.ExecuteReader();

                    while (readerAllHocki.Read())
                    {
                        cboHocki.Items.Add(readerAllHocki["Hocki"].ToString());
                    }

                    readerAllHocki.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load dữ liệu Học kì: " + ex.Message);
            }
        }


        private void cboMamh_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cboNganh_SelectedIndexChanged(object sender, EventArgs e)
        {
        
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            if (comboBoxesLoaded)
            {
                tukhoa = txtTukhoa.Text;
                hocki = cboHocki.SelectedItem?.ToString();
                nganh = cboNganh.SelectedItem?.ToString();
                mahp = cboMamh.SelectedItem?.ToString();
                LoadDSLH();
            }

        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            tukhoa = txtTukhoa.Text;
            LoadDSLH();
        }

        private void txtTukhoa_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTukhoa.Text))
            {
                LoadDSLH();
            }
        }

        private void dgvDSLH_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
   
        private void dgvDSLH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           

        }

       

    }

}
