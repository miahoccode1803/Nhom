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

namespace Dangkitinchi
{
    public partial class Monhoc : Form
    {
        private string mamh;
        public Monhoc(string mamh)
        {
            InitializeComponent();
            this.mamh = mamh;
        }

        private void Monhoc_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(mamh))
            {
                this.Text = "Thêm mới môn học";

            }
            else
            {
                this.Text = "Cập nhật thông tin môn học";
                var r = new Database_store().Select("SelectMH '" + mamh + "'");

                txtMahp.Text = r["Mã học phần"].ToString();
                txtTenhp.Text = r["Tên học phần"].ToString();
                txtSotin.Text = r["Số tín chỉ"].ToString();
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql = "";
            
            List<CustomParamater> lstPara = new List<CustomParamater>();
            if (string.IsNullOrEmpty(mamh))
            {
                sql = "InsertMH";
                mamh = txtMahp.Text;

            }
            else
            {
                sql = "UpdateMH";
                string newmhp = txtMahp.Text;
                lstPara.Add(new CustomParamater()
                {
                    key = "@Newmahp",
                    value = newmhp
                });

            }
            lstPara.Add(new CustomParamater()
            {
                key = "@mahocphan",
                value = mamh
            });
            lstPara.Add(new CustomParamater()
            {
                key = "@tenhocphan",
                value = txtTenhp.Text
            });
            lstPara.Add(new CustomParamater()
            {
                key = "@Sotinchi",
                value = txtSotin.Text
            });

            var rs = new Database_store().Execute(sql, lstPara);
            if (rs == 1)
            {
                if (string.IsNullOrEmpty(mamh))
                {
                    MessageBox.Show("Thêm mới môn học thành công!!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Cập nhật môn học thành công!!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                this.Dispose();
            }
            else
            {
                MessageBox.Show("Thực thi thất bại!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
