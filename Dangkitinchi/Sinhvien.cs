using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dangkitinchi
{
    public partial class Sinhvien : Form
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;

        private string msv;
        public Sinhvien(string msv)
        {
            this.msv = msv;
            InitializeComponent();
            LoadDataToCboKhoa();
            LoadDataToCboNganh();

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql = "";
            string hoten = txtHoten.Text;
            string lop = txtLop.Text;
            DateTime  ngaysinh;
            try
            {
                ngaysinh = DateTime.ParseExact(mtxtNgaysinh.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                MessageBox.Show("Ngày sinh không hợp lệ!!!");
                mtxtNgaysinh.Select();
                return;
            }
            string gt = rdbNam.Checked ? "1" : "0";

            string nganh = cboNganh.SelectedItem.ToString();
            string khoa = cboKhoa.SelectedItem.ToString();
            string quequan = txtQuequan.Text;
            string email = txtEmail.Text;
            string diachi = txtDiachi.Text;
            string dienthoai = txtSodienthoai.Text;
            List<CustomParamater> lstPara = new List<CustomParamater>();
            if (string.IsNullOrEmpty(msv))
            {
                sql = "InsertSV";
                msv = txtMasv.Text;

            }
            else
            {
                sql = "UpdateSV";
                string newmsv = txtMasv.Text;
                lstPara.Add(new CustomParamater()
                {
                    key = "@Newmasv",
                    value = newmsv
                });

            }
            lstPara.Add(new CustomParamater() 
            { 
                key = "@masv",
                value = msv
            });
            lstPara.Add(new CustomParamater()
            {
                key = "@Lop",
                value = lop
            });
            lstPara.Add(new CustomParamater()
            {
                key = "@hoten",
                value = hoten
            });
            lstPara.Add(new CustomParamater()
            {
                key = "@gioitinh",
                value = gt
            });
            lstPara.Add(new CustomParamater()
            {
                key = "@ngaysinh",
                value = ngaysinh.ToString("yyyy-MM-dd")
            });
            lstPara.Add(new CustomParamater()
            {
                key = "@tennganh",
                value = nganh.ToString()
            });
            lstPara.Add(new CustomParamater()
            {
                key = "@tenkhoa",
                value = khoa.ToString()
            }) ;
            lstPara.Add(new CustomParamater()
            {
                key = "@quequan",
                value = quequan
            });
            lstPara.Add(new CustomParamater()
            {
                key = "@diachi",
                value = diachi
            });
            lstPara.Add(new CustomParamater()
            {
                key = "@email",
                value = email
            });
            lstPara.Add(new CustomParamater()
            {
                key = "@sdt",
                value = dienthoai
            });

            var rs = new Database_store().Execute(sql, lstPara);
            if(rs == 1)
            {
                if(string.IsNullOrEmpty(msv))
                {
                    MessageBox.Show("Thêm mới sinh viên thành công!!", "Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Cập nhật sinh viên thành công!!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                this.Dispose();
            }
            else
            {
                MessageBox.Show("Thực thi thất bại!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void LoadDataToCboNganh()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // Thực hiện truy vấn để lấy mã ngành từ bảng SinhVien dựa trên mã sinh viên
                    string sqlGetMaNganh = "SELECT MaNganh FROM SinhVien WHERE MaSV = @MaSV";
                    SqlCommand commandGetMaNganh = new SqlCommand(sqlGetMaNganh, con);
                    commandGetMaNganh.Parameters.AddWithValue("@MaSV", msv);

                    string maNganh = commandGetMaNganh.ExecuteScalar()?.ToString();

                    // Nếu mã ngành tồn tại, thực hiện truy vấn tên ngành từ bảng Nganh
                    if (!string.IsNullOrEmpty(maNganh))
                    {
                        string sqlGetTenNganh = "SELECT tennganh FROM Nganh WHERE MaNganh = @MaNganh";
                        SqlCommand commandGetTenNganh = new SqlCommand(sqlGetTenNganh, con);
                        commandGetTenNganh.Parameters.AddWithValue("@MaNganh", maNganh);

                        string tenNganh = commandGetTenNganh.ExecuteScalar()?.ToString();

                    }

                    // Thêm các ngành khác vào ComboBox Ngành
                    string sqlGetOtherNganh = "SELECT tennganh FROM Nganh";
                    SqlCommand commandGetOtherNganh = new SqlCommand(sqlGetOtherNganh, con);
                    SqlDataReader readerOtherNganh = commandGetOtherNganh.ExecuteReader();

                    while (readerOtherNganh.Read())
                    {
                        cboNganh.Items.Add(readerOtherNganh["tennganh"].ToString());
                    }

                    readerOtherNganh.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load dữ liệu Ngành: " + ex.Message);
            }
        }

        private void LoadDataToCboKhoa()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // Truy vấn mã ngành của sinh viên từ bảng SinhVien
                    string sqlGetMaNganh = "SELECT MaNganh FROM SinhVien WHERE MaSV = @MaSV";
                    SqlCommand commandGetMaNganh = new SqlCommand(sqlGetMaNganh, con);
                    commandGetMaNganh.Parameters.AddWithValue("@MaSV", msv);

                    string maNganh = commandGetMaNganh.ExecuteScalar()?.ToString();

                    // Nếu mã ngành tồn tại, thực hiện truy vấn tên khoa từ bảng Nganh
                    if (!string.IsNullOrEmpty(maNganh))
                    {
                        string sqlGetMaKhoa = "SELECT MaKhoa FROM Nganh WHERE MaNganh = @MaNganh";
                        SqlCommand commandGetMaKhoa = new SqlCommand(sqlGetMaKhoa, con);
                        commandGetMaKhoa.Parameters.AddWithValue("@MaNganh", maNganh);

                        string maKhoa = commandGetMaKhoa.ExecuteScalar()?.ToString();

                        // Nếu mã khoa tồn tại, thực hiện truy vấn tên khoa từ bảng Khoa
                        if (!string.IsNullOrEmpty(maKhoa))
                        {
                            string sqlGetTenKhoa = "SELECT tenkhoa FROM Khoa WHERE MaKhoa = @MaKhoa";
                            SqlCommand commandGetTenKhoa = new SqlCommand(sqlGetTenKhoa, con);
                            commandGetTenKhoa.Parameters.AddWithValue("@MaKhoa", maKhoa);

                            string tenKhoa = commandGetTenKhoa.ExecuteScalar()?.ToString();

                            // Thêm thông tin khoa của sinh viên vào ComboBox Khoa
                            
                        }
                    }

                    // Thêm các khoa khác vào ComboBox Khoa
                    string sqlGetOtherKhoa = "SELECT tenkhoa FROM Khoa";
                    SqlCommand commandGetOtherKhoa = new SqlCommand(sqlGetOtherKhoa, con);
                    SqlDataReader readerOtherKhoa = commandGetOtherKhoa.ExecuteReader();

                    while (readerOtherKhoa.Read())
                    {
                        cboKhoa.Items.Add(readerOtherKhoa["tenkhoa"].ToString());
                    }

                    readerOtherKhoa.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load dữ liệu Khoa: " + ex.Message);
            }
        }




        private void Sinhvien_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(msv))
            {
                this.Text = "Thêm mới sinh viên";

            }
            else
            {
                this.Text = "Cập nhật thông tin sinh viên";
                var r = new Database_store().Select("SelectSV '"+msv+"'");
                string selectedNganh = r["Ngành"].ToString();
                if (cboNganh.Items.Contains(selectedNganh))
                {
                    cboNganh.SelectedItem = selectedNganh;
                }

                // Chọn giá trị trong ComboBox Khoa
                string selectedKhoa = r["Khoa"].ToString();
                if (cboKhoa.Items.Contains(selectedKhoa))
                {
                    cboKhoa.SelectedItem = selectedKhoa;
                }
                txtMasv.Text = r["Mã SV"].ToString(); 
                txtHoten.Text = r["Họ tên"].ToString();
                mtxtNgaysinh.Text = r["Ngày sinh"].ToString() ;
                bool gioiTinh = Convert.ToBoolean(r["Giới tính"]);
                if (gioiTinh)
                {
                    rdbNam.Checked = true;
                }
                else
                {
                    rbdNu.Checked = true;
                }
                txtLop.Text = r["Lớp"].ToString();
                txtQuequan.Text = r["Quê quán"].ToString();
                txtDiachi.Text = r["Địa chỉ"].ToString();
                txtEmail.Text = r["email"].ToString();
                txtSodienthoai.Text = r["Số điện thoại"].ToString();

            }
        }

        private void txtSodienthoai_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSodienthoai_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Kiểm tra xem ký tự được nhập có phải là số không
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // Nếu không phải là số, không cho phép nhập
                e.Handled = true;
            }

            // Kiểm tra xem số điện thoại đã đạt đến độ dài 10 chưa
            if (txtSodienthoai.Text.Length >= 10 && !char.IsControl(e.KeyChar))
            {
                // Nếu đã đạt đến độ dài 10 và không phải là ký tự control, không cho phép nhập
                e.Handled = true;
            }
        }

        private void txtSodienthoai_Validated(object sender, EventArgs e)
        {

        }

        private void txtSodienthoai_Validating(object sender, CancelEventArgs e)
        {
            if (txtSodienthoai.Text.Length != 10)
            {
                // Số điện thoại không hợp lệ, hiển thị MessageBox và yêu cầu nhập lại
                MessageBox.Show("Số điện thoại không hợp lệ. Vui lòng nhập đúng 10 chữ số.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Hủy sự kiện Validating và tập trung lại vào ô nhập liệu
                e.Cancel = true;
                txtSodienthoai.Focus();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void cboKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cboNganh_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Xử lý sự kiện khi người dùng chọn ngành
           /* try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // Lấy tên ngành từ ComboBox Ngành
                    string selectedNganh = cboNganh.SelectedItem?.ToString();

                    if (!string.IsNullOrEmpty(selectedNganh))
                    {
                        // Thực hiện truy vấn để lấy danh sách các khoa tương ứng với ngành đã chọn
                        string sqlGetKhoaByNganh = "SELECT DISTINCT Khoa.tenkhoa FROM Lop INNER JOIN Nganh ON Lop.MaNganh = Nganh.MaNganh INNER JOIN Khoa ON Nganh.MaKhoa = Khoa.MaKhoa WHERE Nganh.tennganh = @TenNganh";
                        SqlCommand commandGetKhoaByNganh = new SqlCommand(sqlGetKhoaByNganh, con);
                        commandGetKhoaByNganh.Parameters.AddWithValue("@TenNganh", selectedNganh);

                        // Thực hiện truy vấn
                        SqlDataReader readerKhoa = commandGetKhoaByNganh.ExecuteReader();

                        // Xóa tất cả các mục hiện tại trong ComboBox Khoa
                        cboKhoa.Items.Clear();

                        // Thêm các khoa tương ứng vào ComboBox Khoa
                        while (readerKhoa.Read())
                        {
                            cboKhoa.Items.Add(readerKhoa["tenkhoa"].ToString());
                        }

                        readerKhoa.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật ComboBox Khoa: " + ex.Message);
            }*/

        }
    }
}
