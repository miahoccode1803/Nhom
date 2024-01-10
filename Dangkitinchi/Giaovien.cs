using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dangkitinchi
{
    public partial class Giaovien : Form
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;

        private string mgv;
        public Giaovien(string mgv)
        {
            InitializeComponent();
            this.mgv = mgv;
        }



        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void Giaovien_Load(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(mgv))
            {
                this.Text = "Thêm mới giáo viên";

            }
            else
            {
                this.Text = "Cập nhật thông tin giáo viên";
                var r = new Database_store().Select("SelectgV '" + mgv + "'");
                txtMagv.Text = r["Mã GV"].ToString();
                txtHoten.Text = r["Họ tên"].ToString();
                mtxtNgaysinh.Text = r["Ngày sinh"].ToString();
                bool gioiTinh = Convert.ToBoolean(r["Giới tính"]);
                if (gioiTinh)
                {
                    rdbNam.Checked = true;
                }
                else
                {
                    rbdNu.Checked = true;
                }
                txtDiachi.Text = r["Địa chỉ"].ToString();
                txtEmail.Text = r["email"].ToString();
                txtSodienthoai.Text = r["Số điện thoại"].ToString();

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql = "";
            string hoten = txtHoten.Text;
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
            string gt = rdbNam.Checked ? "1" : "0";

            string email = txtEmail.Text;
            string diachi = txtDiachi.Text;
            string dienthoai = txtSodienthoai.Text;
            List<CustomParamater> lstPara = new List<CustomParamater>();
            if (string.IsNullOrEmpty(mgv))
            {
                sql = "InsertGV";
                mgv = txtMagv.Text;

            }
            else
            {
                sql = "UpdateGV";
                string newmgv = txtMagv.Text;
                lstPara.Add(new CustomParamater()
                {
                    key = "@Newmagv",
                    value = newmgv
                });

            }
            lstPara.Add(new CustomParamater()
            {
                key = "@magv",
                value = mgv
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
            if (rs == 1)
            {
                if (string.IsNullOrEmpty(mgv))
                {
                    MessageBox.Show("Thêm mới giáo viên thành công!!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Cập nhật giáo viên thành công!!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                this.Dispose();
            }
            else
            {
                MessageBox.Show("Thực thi thất bại!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
    }
}
