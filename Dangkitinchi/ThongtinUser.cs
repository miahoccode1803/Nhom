using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Dangkitinchi.Dangnhap;

namespace Dangkitinchi
{
    public partial class ThongtinUser : Form
    {
        private string msv;
        private Dangnhap.UserInfo loggedInUser;
        public ThongtinUser(Dangnhap.UserInfo loggedInUser)
        {
            InitializeComponent();
            this.msv = loggedInUser.MaSV;
            this.loggedInUser = loggedInUser;
        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void ThongtinUser_Load(object sender, EventArgs e)
        {
            LoadUser();
        }

        private void LoadUser()
        {
            lblMasv.Text = msv.ToString();
            lblHoten.Text = loggedInUser.TenSV.ToString();
            lblLop.Text = loggedInUser.Lop.ToString();
            lblNganh.Text = loggedInUser.Nganh.ToString();
            lblKhoa.Text = loggedInUser.khoa.ToString();
            bool gioiTinh = Convert.ToBoolean(loggedInUser.Gioitinh.ToString());
            if (gioiTinh)
            {
                rdbNam.Checked = true;
            }
            else
            {
                rbdNu.Checked = true;
            }
            txtDiachi.Text = loggedInUser.DiaChi.ToString();
            txtSodienthoai.Text = loggedInUser.SoDienThoai.ToString();
            txtQuequan.Text = loggedInUser.QueQuan.ToString();
            mtxtNgaysinh.Text = loggedInUser.ngaysinh.ToString();
            txtEmail.Text = loggedInUser.Email.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string hoten = lblHoten.Text;
            string lop = lblLop.Text;
            DateTime ngaysinh;
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
            string gioitinh = rdbNam.Checked ? "1" : "0";
            string nganh = lblNganh.Text;
            string khoa = lblKhoa.Text;
            string quequan = txtQuequan.Text;
            string email = txtEmail.Text;
            string diachi = txtDiachi.Text;
            string dienthoai = txtSodienthoai.Text;
            List<CustomParamater> lstPara = new List<CustomParamater>()
            {
                new CustomParamater() { key = "@masv", value = loggedInUser.MaSV },
                new CustomParamater() { key = "@Newmasv", value = loggedInUser.MaSV },
                new CustomParamater() { key = "@Lop", value = lop },
                new CustomParamater() { key = "@hoten", value = hoten },
                new CustomParamater() { key = "@gioitinh", value = gioitinh },
                new CustomParamater() { key = "@ngaysinh", value = ngaysinh.ToString("yyyy-MM-dd") },
                new CustomParamater() { key = "@tennganh", value = nganh },
                new CustomParamater() { key = "@tenkhoa", value = khoa },
                new CustomParamater() { key = "@quequan", value = quequan },
                new CustomParamater() { key = "@diachi", value = diachi },
                new CustomParamater() { key = "@email", value = email },
                new CustomParamater() { key = "@sdt", value = dienthoai }
            };

            var rs = new Database_store().Execute("UpdateSV", lstPara);
            if (rs == 1)
            {
                if (string.IsNullOrEmpty(msv))
                {
                    MessageBox.Show("Thực thi thất bại!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Cập nhật thành công!!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            GetUserDetailsFromDatabase();
            LoadUser();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void GetUserDetailsFromDatabase()
        {
            List<CustomParamater> lstPara = new List<CustomParamater>
            {
                new CustomParamater() { key = "@masv", value = loggedInUser.MaSV }
            };

            var db = new Database_store();
            var userData = db.SelectData("SelectSV", lstPara);

            if (userData.Rows.Count > 0)
            {
                var row = userData.Rows[0];
                loggedInUser.TenSV = row["Họ tên"].ToString();
                loggedInUser.Lop = row["Lớp"].ToString();
                loggedInUser.Nganh = row["Ngành"].ToString();
                loggedInUser.khoa = row["Khoa"].ToString();
                loggedInUser.Gioitinh  = row["Giới tính"].ToString();
                loggedInUser.DiaChi = row["Địa chỉ"].ToString();
                loggedInUser.SoDienThoai = row["Số điện thoại"].ToString();
                loggedInUser.QueQuan = row["Quê quán"].ToString();
                loggedInUser.ngaysinh = row["Ngày sinh"].ToString();
                loggedInUser.Email = row["Email"].ToString();
            }

        }
    }
}
