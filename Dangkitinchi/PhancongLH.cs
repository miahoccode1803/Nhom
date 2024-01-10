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
    public partial class PhancongLH : Form
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;

        private string ID;
        public PhancongLH(string ID)
        {
            InitializeComponent();
                this.ID = ID;

                // Load dữ liệu giáo viên, khoa, ngành vào combobox
                LoadDataToCboGiaovien();
                LoadDataToCboNganh();
                LoadDataToCboTenMH();

        }

        private void PhancongLH_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ID))
            {
                this.Text = "Thêm mới lớp học";
                txtID.Visible = false;
            }
            else
            {
                this.Text = "Cập nhật thông tin lớp học";
                LoadLopHocDataByID(ID);
            }
        }

        private void LoadLopHocDataByID(string lopHocID)
        {
            var r = new Database_store().Select("SelectLH '" + lopHocID + "'");
            string selectedGV = r["Giáo viên phụ trách"].ToString();
            cboGiaovien.SelectedItem = selectedGV;

            string selectedMH = r["Tên học phần"].ToString();
            cbotenHP.SelectedItem = selectedMH;

            string selectedNganh = r["Ngành"].ToString();
            MessageBox.Show(r["Ngành"].ToString());
            cboNganh.SelectedItem = selectedNganh;

            txtID.Text = r["ID"].ToString();
            txtThoigian.Text = r["thời gian"].ToString();
            txtPhonghoc.Text = r["Phòng học"].ToString();
            txtHocki.Text = r["Học kì"].ToString();

            if (r["Trạng thái"] != DBNull.Value)
            {
                bool trangthai = Convert.ToBoolean(r["Trạng thái"]);
                radioButton1.Checked = trangthai;
                radioButton2.Checked = !trangthai;
            }
        }





        private void LoadDataToCboNganh()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    cboNganh.Items.Clear(); // Clear items before adding new ones

                    // Load all nganh
                    string sqlGetAllNganh = "SELECT tennganh FROM Nganh";
                    SqlCommand commandGetAllNganh = new SqlCommand(sqlGetAllNganh, con);
                    SqlDataReader readerAllNganh = commandGetAllNganh.ExecuteReader();

                    while (readerAllNganh.Read())
                    {
                        cboNganh.Items.Add(readerAllNganh["tennganh"].ToString());
                    }

                    readerAllNganh.Close();

                    if (!string.IsNullOrEmpty(ID))
                    {
                        // Load nganh based on selected ID
                        string sqlGetNganhByID = "SELECT Nganh.tennganh FROM Lop INNER JOIN Nganh ON Lop.MaNganh = Nganh.MaNganh WHERE Lop.ID = @ID";
                        SqlCommand commandGetNganhByID = new SqlCommand(sqlGetNganhByID, con);
                        commandGetNganhByID.Parameters.AddWithValue("@ID", ID);

                        string selectedNganh = commandGetNganhByID.ExecuteScalar()?.ToString();

                        if (!string.IsNullOrEmpty(selectedNganh))
                        {
                            cboNganh.SelectedItem = selectedNganh;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load dữ liệu Ngành: " + ex.Message);
            }
        }



        private void LoadDataToCboTenMH()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // Kiểm tra xem ID có giá trị hay không
                    if (!string.IsNullOrEmpty(ID))
                    {
                        // Nếu có giá trị, thực hiện truy vấn để lấy mã môn học từ bảng Lop dựa trên ID lớp
                        string sqlGetMaMH = "SELECT MaMH FROM Lop WHERE ID = @ID";
                        SqlCommand commandGetMaMH = new SqlCommand(sqlGetMaMH, con);
                        commandGetMaMH.Parameters.AddWithValue("@ID", ID);

                        string maMH = commandGetMaMH.ExecuteScalar()?.ToString();

                        // Nếu mã môn học tồn tại, thực hiện truy vấn tên môn học từ bảng MonHoc
                        if (!string.IsNullOrEmpty(maMH))
                        {
                            string sqlGetTenMH = "SELECT tenmh FROM MonHoc WHERE MaMH = @MaMH";
                            SqlCommand commandGetTenMH = new SqlCommand(sqlGetTenMH, con);
                            commandGetTenMH.Parameters.AddWithValue("@MaMH", maMH);

                            string tenMH = commandGetTenMH.ExecuteScalar()?.ToString();

                            // Thêm tên môn học vào ComboBox Môn học
                            if (!string.IsNullOrEmpty(tenMH))
                            {
                                cbotenHP.Items.Add(tenMH);
                            }
                        }
                    }

                    // Thêm các môn học khác vào ComboBox Môn học
                    string sqlGetOtherMH = "SELECT tenmh FROM MonHoc";
                    SqlCommand commandGetOtherMH = new SqlCommand(sqlGetOtherMH, con);
                    SqlDataReader readerOtherMH = commandGetOtherMH.ExecuteReader();

                    while (readerOtherMH.Read())
                    {
                        cbotenHP.Items.Add(readerOtherMH["tenmh"].ToString());
                    }

                    readerOtherMH.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load dữ liệu Môn học: " + ex.Message);
            }

        }
        private void LoadDataToCboGiaovien()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // Kiểm tra xem ID có giá trị hay không
                    if (!string.IsNullOrEmpty(ID))
                    {
                        // Nếu có giá trị, thực hiện truy vấn để lấy mã giáo viên từ bảng Lop dựa trên ID lớp
                        string sqlGetMaGV = "SELECT Distinct MaGV FROM Lop WHERE ID = @ID";
                        SqlCommand commandGetMaGV = new SqlCommand(sqlGetMaGV, con);
                        commandGetMaGV.Parameters.AddWithValue("@ID", ID);

                        string maGV = commandGetMaGV.ExecuteScalar()?.ToString();

                        // Nếu mã giáo viên tồn tại, thực hiện truy vấn tên giáo viên từ bảng GiaoVien
                        if (!string.IsNullOrEmpty(maGV))
                        {
                            string sqlGetTenGV = "SELECT tengv FROM GiaoVien WHERE MaGV = @MaGV";
                            SqlCommand commandGetTenGV = new SqlCommand(sqlGetTenGV, con);
                            commandGetTenGV.Parameters.AddWithValue("@MaGV", maGV);

                            string tenGV = commandGetTenGV.ExecuteScalar()?.ToString();

                            // Thêm tên giáo viên vào ComboBox Giáo viên
                            if (!string.IsNullOrEmpty(tenGV))
                            {
                                cboGiaovien.Items.Add(tenGV);
                            }
                        }
                    }

                    // Thêm các giáo viên khác vào ComboBox Giáo viên
                    string sqlGetOtherGV = "SELECT tengv FROM GiaoVien";
                    SqlCommand commandGetOtherGV = new SqlCommand(sqlGetOtherGV, con);
                    SqlDataReader readerOtherGV = commandGetOtherGV.ExecuteReader();

                    while (readerOtherGV.Read())
                    {
                        cboGiaovien.Items.Add(readerOtherGV["tengv"].ToString());
                    }

                    readerOtherGV.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load dữ liệu Giáo viên: " + ex.Message);
            }

        }




        private void cbotenHP_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string phonghoc = txtPhonghoc.Text;
            string thoigian = txtThoigian.Text;
            string trangthai = radioButton1.Checked ? "1" : "0";
            string giaovien = cboGiaovien.SelectedItem.ToString();
            string tenhp = cbotenHP.SelectedItem.ToString();
            string nganh = cboNganh.SelectedItem.ToString();

            List<CustomParamater> lstPara = new List<CustomParamater>();
            string sql = string.Empty;

            if (string.IsNullOrEmpty(ID))
            {
                sql = "InsertLH";
            }
            else
            {
                sql = "UpdateLH";
                lstPara.Add(new CustomParamater()
                {
                    key = "@ID",
                    value = txtID.Text
                });
            }

            lstPara.Add(new CustomParamater()
            {
                key = "@Tenmh",
                value = tenhp.ToString()
            });
            lstPara.Add(new CustomParamater()
            {
                key = "@tengv",
                value = giaovien.ToString()
            });
            lstPara.Add(new CustomParamater()
            {
                key = "@trangthai",
                value = trangthai
            });
            lstPara.Add(new CustomParamater()
            {
                key = "@thoigian",
                value = thoigian
            });
            lstPara.Add(new CustomParamater()
            {
                key = "@Phonghoc",
                value = phonghoc
            });
            lstPara.Add(new CustomParamater()
            {
                key = "@tennganh",
                value = nganh.ToString()
            });
            lstPara.Add(new CustomParamater()
            {
                key = "@Hocki",
                value = txtHocki.Text
            });

            var rs = new Database_store().Execute(sql, lstPara);
            if (rs == 1)
            {
                MessageBox.Show(string.IsNullOrEmpty(ID) ? "Thêm mới lớp học thành công!!" : "Cập nhật lớp học thành công!!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Dispose();
            }
            else
            {
                MessageBox.Show("Thực thi thất bại!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }





        private void cboNganh_SelectedIndexChanged(object sender, EventArgs e)
        {

            /// Xử lý sự kiện khi người dùng chọn ngành
            
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
