using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Dangkitinchi.Dangnhap;

namespace Dangkitinchi
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();

        }
        private UserInfo loggedInUser;

        private string tk;
        private string loaitk;
        private bool isLoggedIn = false;

        private void Form1_Load(object sender, EventArgs e)
        {
            var fn = new Dangnhap();
            fn.ShowDialog();
            loggedInUser = fn.LoggedInUser;
            tk = fn.tendangnhap;
            loaitk = fn.loaitk;
            if (loaitk.Equals("ad"))
            {
                dangkitinToolStripMenuItem.Visible = false;
                lblTenuser.Text = "Quản trị viên";
                btnEdit.Enabled = false;
            }
            else
            {
                qlysinhvienToolStripMenuItem.Visible = false;
                giaovientoolStripMenuItem.Visible = false;
                qlylophoctoolStripMenuItem.Visible = false;
                monhoctoolStripMenuItem.Visible = false;
                toolStripMenuItem1.Visible = false;
                lblTenuser.Text = "Sinh viên " + loggedInUser.TenSV.ToString();
            }

            // Kiểm tra trạng thái đăng nhập để hiển thị hoặc ẩn btnLogout và btnLogin
            isLoggedIn = !string.IsNullOrEmpty(tk) && !string.IsNullOrEmpty(loaitk);
            btnLogout.Visible = isLoggedIn;
            btnLogin.Visible = !isLoggedIn;
            

        }

        private void Addform(Form f)
        {
            this.panel1.Controls.Clear();
            f.TopLevel = false;
            f.AutoScroll = true;
            f.FormBorderStyle = FormBorderStyle.None;
            f.Dock = DockStyle.Fill;
            this.panel1.Controls.Add(f);
            f.Show();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var fn = new Dangnhap();
            fn.ShowDialog();

            // Cập nhật trạng thái đăng nhập khi đăng nhập thành công
            isLoggedIn = !string.IsNullOrEmpty(fn.tendangnhap) && !string.IsNullOrEmpty(fn.loaitk);
            btnLogin.Visible = !isLoggedIn;
            btnLogout.Visible = isLoggedIn;


        }

        private void qlysinhvienToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DSSV f = new DSSV();
            Addform(f);
        }

        private void giaovientoolStripMenuItem_Click(object sender, EventArgs e)
        {
            DSGV f = new DSGV();
            Addform(f);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void qlylophoctoolStripMenuItem_Click(object sender, EventArgs e)
        {
            DSLH f = new DSLH();
            Addform(f);
        }

        private void homeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.panel1.Controls.Clear();
        }

        private void dangkitinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dangkitinchi f = new Dangkitinchi(loggedInUser);
            MessageBox.Show("Mã SV:" + loggedInUser.MaSV.ToString());
            Addform(f);
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {

                // Hiển thị MessageBox xác nhận đăng xuất
                DialogResult result = MessageBox.Show("Bạn có muốn đăng xuất? Nếu bạn đăng xuất thì phải đăng nhập lại mới sử dụng được app", "Xác nhận đăng xuất", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                // Đặt trạng thái đăng nhập về false và ẩn btnLogout
                isLoggedIn = false;
                btnLogout.Visible = false;
                btnLogin.Visible = true;
                lblTenuser.Visible = false;
                loggedInUser = null;

                // Ẩn tất cả các menu strip
                    dangkitinToolStripMenuItem.Visible = false;
                    qlysinhvienToolStripMenuItem.Visible = false;
                    giaovientoolStripMenuItem.Visible = false;
                    qlylophoctoolStripMenuItem.Visible = false;
                    
                    monhoctoolStripMenuItem.Visible = false;
                    toolStripMenuItem1.Visible = false;
                    this.panel1.Controls.Clear();
                // Hiển thị lại trang đăng nhập
                var loginForm = new Dangnhap();
                    loginForm.ShowDialog();

                    // Kiểm tra trạng thái đăng nhập sau khi đăng xuất
                    if (!string.IsNullOrEmpty(loginForm.tendangnhap) && !string.IsNullOrEmpty(loginForm.loaitk))
                    {
                        // Nếu đăng nhập thành công, cập nhật thông tin đăng nhập và hiển thị menu strip
                        tk = loginForm.tendangnhap;
                        loaitk = loginForm.loaitk;
                        isLoggedIn = true;
                        btnLogout.Visible = true;
                        btnLogin.Visible = false;
                        lblTenuser.Visible = true;

                    // Hiển thị các menu strip phù hợp với loại tài khoản
                    if (loaitk.Equals("ad"))
                    {
                        qlysinhvienToolStripMenuItem.Visible = true;
                        giaovientoolStripMenuItem.Visible =true ;
                        qlylophoctoolStripMenuItem.Visible = true;
                         
                        monhoctoolStripMenuItem.Visible = true;
                        toolStripMenuItem1.Visible = true;
                        dangkitinToolStripMenuItem.Visible = false;
                        btnEdit.Enabled = false;
                        
                    }
                    else
                    {
                        dangkitinToolStripMenuItem.Visible = true;
                        toolStripMenuItem1.Visible = false;
                        qlysinhvienToolStripMenuItem.Visible = false;
                        giaovientoolStripMenuItem.Visible = false;
                        qlylophoctoolStripMenuItem.Visible = false;
                        monhoctoolStripMenuItem.Visible = false;
                        btnEdit.Enabled = true;
                        
                    }
                    loggedInUser = loginForm.LoggedInUser;
                    lblTenuser.Text = loaitk.Equals("ad") ? "Quản trị viên" : "Sinh viên " + loggedInUser.TenSV.ToString();
                }
                }
            }


        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void tracuuToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void ketqua_Click(object sender, EventArgs e)
        {
            Dangkitinchi f = new Dangkitinchi(loggedInUser);
            Addform(f);
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            ThongtinUser user = new ThongtinUser(loggedInUser);
            Addform(user);
        }

        private void monhoctoolStripMenuItem_Click(object sender, EventArgs e)
        {
            DSMH f = new DSMH();
            Addform(f);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DSNH f = new DSNH();
            Addform(f);
        }

        private void hotroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hotro f = new Hotro();
            Addform(f);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
