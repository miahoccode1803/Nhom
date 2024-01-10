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
    public partial class Nganh : Form
    {
        private string manganh;
        public Nganh( string manganh)
        {
            InitializeComponent();
            this.manganh = manganh;
            LoadDataToCboKhoa();
        }

        private void Nganh_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(manganh))
            {
                this.Text = "Thêm mới ngành";

            }
            else
            {
                this.Text = "Cập nhật thông tin ngành";
                var r = new Database_store().Select("SelectNH '" + manganh + "'");

                txtManganh.Text = r["Mã ngành"].ToString();
                txtTennganh.Text = r["Tên ngành"].ToString();
                string selectedNH = r["Tên khoa"].ToString();
                cboKhoa.SelectedItem = selectedNH;
            }
        }
        private void LoadDataToCboKhoa()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // Kiểm tra xem ID có giá trị hay không
                    if (!string.IsNullOrEmpty(manganh))
                    {
                        string sqlGetTenKhoa = "SELECT Khoa.tenkhoa FROM Nganh INNER JOIN Khoa ON Nganh.MaKhoa = Khoa.MaKhoa WHERE Nganh.manganh = @manganh";
                        SqlCommand commandGetTenKhoa = new SqlCommand(sqlGetTenKhoa, con);
                        commandGetTenKhoa.Parameters.AddWithValue("@manganh", manganh); 

                        string tenKhoa = commandGetTenKhoa.ExecuteScalar()?.ToString();

                        // Nếu tên khoa tồn tại, thêm vào ComboBox Khoa
                        if (!string.IsNullOrEmpty(tenKhoa))
                        {
                            cboKhoa.Items.Add(tenKhoa);
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
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql = "";

            List<CustomParamater> lstPara = new List<CustomParamater>();
            if (string.IsNullOrEmpty(manganh))
            {
                sql = "InsertNganh";
                manganh = txtManganh.Text;

            }
            else
            {
                sql = "UpdateNganh";
                string newmanganh = txtManganh.Text;
                lstPara.Add(new CustomParamater()
                {
                    key = "@Newmanganh",
                    value = newmanganh
                });

            }
            lstPara.Add(new CustomParamater()
            {
                key = "@manganh",
                value = manganh
            });
            lstPara.Add(new CustomParamater()
            {
                key = "@tennganh",
                value = txtTennganh.Text
            });
            lstPara.Add(new CustomParamater()
            {
                key = "@tenkhoa",
                value = cboKhoa.SelectedItem.ToString()
            }) ;

            var rs = new Database_store().Execute(sql, lstPara);
            if (rs == 1)
            {
                if (string.IsNullOrEmpty(manganh))
                {
                    MessageBox.Show("Thêm mới ngành thành công!!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Cập nhật ngành thành công!!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                this.Dispose();
            }
            else
            {
                MessageBox.Show("Thực thi thất bại!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
