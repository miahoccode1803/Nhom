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
    public partial class Dangnhap : Form
    {
        public Dangnhap()
        {
            InitializeComponent();
        }
        public UserInfo LoggedInUser { get; private set; }

        private void txtMatkhau_TextChanged(object sender, EventArgs e)
        {
            if (txtMatkhau.PasswordChar == '\0')
            {
                checkMatkhau.BringToFront();
                txtMatkhau.PasswordChar = '•';
            }
        }

        private void checkMatkhau_CheckedChanged(object sender, EventArgs e)
        {
            if (txtMatkhau.PasswordChar == '\0')
            {
                checkMatkhau.BringToFront();
                txtMatkhau.PasswordChar = '•';
            }
            else
            {
                checkMatkhau.BringToFront();
                txtMatkhau.PasswordChar = '\0';
            }
        }

        public string tendangnhap = "";
        public string matkhau = "";
        public string loaitk = "";


        private void Dangnhap_Load(object sender, EventArgs e)
        {
           

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void btnDangnhap_Click(object sender, EventArgs e)
        {
            if (cboVaitro.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn nhóm tài khoản", "Tài khoản không được để trống");
                return;
            }

            if (string.IsNullOrEmpty(txtTendangnhap.Text))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập", "Tên đăng nhập không được để trống");
                txtTendangnhap.Select();
                return;
            }

            if (string.IsNullOrEmpty(txtMatkhau.Text))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu", "Mật khẩu không được để trống");
                txtMatkhau.Select();
                return;
            }

            tendangnhap = txtTendangnhap.Text;

            switch (cboVaitro.SelectedItem.ToString())
            {
                case "Admin":
                    loaitk = "ad";
                    break;
                case "Sinh viên":
                    loaitk = "sv";
                    break;
                default:
                    loaitk = string.Empty;
                    break;
            }

            if (string.IsNullOrEmpty(loaitk))
            {
                MessageBox.Show("Vai trò không hợp lệ", "Lỗi");
                return;
            }

            List<CustomParamater> lst = new List<CustomParamater>()
            {
                new CustomParamater()
                {
                    key = "@vaitro",
                    value = loaitk
                },

                new CustomParamater()
                {
                    key = "@tentk",
                    value = txtTendangnhap.Text
                },
                new CustomParamater()
                {
                    key = "@matkhau",
                    value = txtMatkhau.Text
                }
            };

            var rs = new Database_store().SelectData("dangnhap", lst);

            if (rs.Rows.Count > 0)
            {
                MessageBox.Show("Đăng nhập thành công");

                LoggedInUser = new UserInfo
                {
                    UserName = txtTendangnhap.Text,
                    UserType = loaitk
                    // Gán các thuộc tính khác nếu cần thiết
                };

                if (loaitk == "sv")
                {
                    List<CustomParamater> svParam = new List<CustomParamater>
                    {
                        new CustomParamater
                        {
                            key = "@masv",
                            value = rs.Rows[0]["MaSV"].ToString()
                        }
                    };

                    var svResult = new Database_store().SelectData("SelectSV", svParam);

                    if (svResult.Rows.Count > 0)
                    {
                        LoggedInUser.MaSV = svResult.Rows[0]["Mã SV"].ToString();
                        LoggedInUser.TenSV = svResult.Rows[0]["Họ tên"].ToString();
                        LoggedInUser.Lop = svResult.Rows[0]["Lớp"].ToString();
                        LoggedInUser.Gioitinh = svResult.Rows[0]["Giới tính"].ToString();
                        LoggedInUser.Nganh = svResult.Rows[0]["Ngành"].ToString();
                        LoggedInUser.khoa = svResult.Rows[0]["Khoa"].ToString();
                        LoggedInUser.Email = svResult.Rows[0]["Email"].ToString();
                        LoggedInUser.DiaChi = svResult.Rows[0]["Địa chỉ"].ToString();
                        LoggedInUser.SoDienThoai = svResult.Rows[0]["Số điện thoại"].ToString();
                        LoggedInUser.QueQuan = svResult.Rows[0]["Quê quán"].ToString();
                        LoggedInUser.ngaysinh = svResult.Rows[0]["Ngày sinh"].ToString() ;
                    }
                }

                this.Hide();
            }

            else
            {
                MessageBox.Show("Vui lòng kiểm tra lại tài khoản hoặc mật khẩu", "Tài khoản hoặc mật khẩu không hợp lệ");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public class UserInfo
        {
            public string UserName { get; set; }
            public string UserType { get; set; }
            public string Gioitinh { get; set; }
            public string MaSV { get; set; }
            public string ngaysinh { get; set; }
            public string TenSV { get; set; }
            public string Lop { get; set; }
            public string Nganh { get; set; }
            public string khoa { get; set; }
            public string Email { get; set; }
            public string DiaChi { get; set; }
            public string SoDienThoai { get; set; }
            public string QueQuan { get; set; }
            // Thêm các thuộc tính khác nếu cần thiết
        }
    }
}
