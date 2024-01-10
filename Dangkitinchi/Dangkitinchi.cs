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
    public partial class Dangkitinchi : Form
    {
        private Dangnhap.UserInfo loggedInUser;
        public Dangkitinchi(Dangnhap.UserInfo loggedInUser)
        {

            InitializeComponent();
            this.loggedInUser = loggedInUser;
            LoadDataToComboBoxes();
            loadDSdangkihoc();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Dangkitinchi_Load(object sender, EventArgs e)
        {
            loadDSdangkihoc();
        }

        private string tukhoa = "";
        private string hocki = "";
        private string tenhp = "";
        private string trangthai = "";
        private void loadDSdangkihoc()
        {


            tukhoa = txtTukhoa.Text;
            hocki = cboHocki.SelectedItem?.ToString() ?? "";
            tenhp = cboTenmh.SelectedItem?.ToString() ?? "";
            trangthai = cboTrangthai.SelectedItem?.ToString() ?? "";



            List<CustomParamater> lstPara = new List<CustomParamater>
            {
                new CustomParamater() { key = "@tukhoa", value = tukhoa },
                new CustomParamater() { key = "@tennganh", value = loggedInUser.Nganh.ToString() },
                new CustomParamater() { key = "@hocki", value = hocki },
                new CustomParamater() { key = "@tenhocphan", value = tenhp },
                new CustomParamater() { key = "@trangthai", value = trangthai }
            };

            var db = new Database_store();
            dgvDSLH.DataSource = db.SelectData("SelectAllDSDangki", lstPara);
        }

        private bool comboBoxesLoaded = false;
        private void LoadDataToComboBoxes()
        {
            comboBoxesLoaded = true;
            // Đổ dữ liệu vào ComboBox Hocki
            List<string> hockiList = GetDistinctHocki();
            hockiList.Insert(0, ""); // Thêm phần tử rỗng vào đầu danh sách
            cboHocki.DataSource = hockiList;

            // Đổ dữ liệu vào ComboBox Tenhocphan
            List<string> tenhocphanList = GetDistincthocphan();
            tenhocphanList.Insert(0, ""); // Thêm phần tử rỗng vào đầu danh sách
            cboTenmh.DataSource = tenhocphanList;


        }

        private List<string> GetDistinctHocki()
        {
            var db = new Database_store();
            DataTable dataTable = db.SelectData_SQL("SELECT DISTINCT hocki FROM lop");
            return dataTable.AsEnumerable().Select(row => row.Field<string>("hocki")).ToList();
        }

        private List<string> GetDistincthocphan()
        {
            var db = new Database_store();
            DataTable dataTable = db.SelectData_SQL("SELECT DISTINCT m.tenmh " +
                                                     "FROM monhoc m " +
                                                     "INNER JOIN lop l ON l.mamh = m.mamh " +
                                                     "INNER JOIN nganh n ON l.manganh = n.manganh " +
                                                     "WHERE n.tennganh = N'" + loggedInUser.Nganh.ToString() + "'");
            return dataTable.AsEnumerable().Select(row => row.Field<string>("tenmh")).ToList();
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            if (comboBoxesLoaded)
            {
                tukhoa = txtTukhoa.Text;
                hocki = cboHocki.SelectedItem?.ToString();
                trangthai = cboTrangthai.SelectedItem?.ToString();
                tenhp = cboTenmh.SelectedItem?.ToString();
                loadDSdangkihoc();
            }
        }

        private void txtTukhoa_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTukhoa.Text))
            {
                loadDSdangkihoc();
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            tukhoa = txtTukhoa.Text;
            loadDSdangkihoc();
        }

        private void btnDangki_Click(object sender, EventArgs e)
        {
            // Tạo danh sách các dòng cần ẩn
            // Kiểm tra xem đã chọn dòng nào chưa
            if (string.IsNullOrEmpty(maMH))
            {
                MessageBox.Show("Vui lòng chọn một lớp học để đăng ký.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy mã môn học từ sự kiện CellContentClick
            string maMonHoc = maMH;

            MessageBox.Show("Input:" + maMH + " " + id);
            // Thực hiện đăng ký thông tin
            int result = ThucHienDangki(loggedInUser.MaSV, id, maMH);

            // Xử lý kết quả đăng ký
            XuLyKetQuaDangKy(result);

            // Reset giá trị mã môn học sau khi đã sử dụng
            maMH = "";
        }

        // ...



        private int ThucHienDangki(string masv, int idLopHoc, string maMonHoc)
        {
            List<CustomParamater> lstPara = new List<CustomParamater>
            {
                new CustomParamater() { key = "@masv", value = masv },
                new CustomParamater() { key = "ID", value = idLopHoc.ToString() },
                new CustomParamater() { key = "@maMH", value = maMonHoc },
            };

            var db = new Database_store();
            return db.Execute("InsertDangki", lstPara);
        }

        private void XuLyKetQuaDangKy(int result)
            
        {
            MessageBox.Show(result.ToString());
            switch (result)
            {
                case 1:
                    MessageBox.Show("Đăng ký thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case 2:
                    MessageBox.Show("Sinh viên đã đăng ký cho lớp học này trước đó.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                case 3:
                    MessageBox.Show("Sinh viên đã đăng ký cho lớp học có cùng mã môn học trước đó.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                case 4:
                    MessageBox.Show("Lớp học đã đủ 60 sinh viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                case 5:
                    MessageBox.Show("Lớp học không tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                default:
                    MessageBox.Show("Đăng ký thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }
        private string maMH;
        private int id;
        private void dgvDSLH_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dgvDSLH.Columns["Dangki"].Index)
            {
                DataGridViewRow selectedRow = dgvDSLH.Rows[e.RowIndex];

                // Lấy dữ liệu từ các ô khác trong dòng này
                maMH = selectedRow.Cells["Mã học phần"].Value.ToString();
                 id = Convert.ToInt32(selectedRow.Cells["ID"].Value);
            }
        }
    }
}
