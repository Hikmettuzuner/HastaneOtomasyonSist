using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace Hastane_Projesi_2018
{
    public partial class FrmBrans : Form
    {
        public FrmBrans()
        {
            InitializeComponent();
        }
        sqlBaglantisi bgl = new sqlBaglantisi();
        private void FrmBrans_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From TBL_Branslar", bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;


        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            SqlCommand komut5 = new SqlCommand("insert into Tbl_Branslar (BransAd) values(@s1)", bgl.baglanti());
            komut5.Parameters.AddWithValue("@s1", TxtAd.Text);
            komut5.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Kayıt Başarılı..");
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut4 = new SqlCommand("Delete  From Tbl_Branslar Where Bransid=@p1", bgl.baglanti());
            komut4.Parameters.AddWithValue("@p1", TxtId.Text);
            komut4.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Silme Başarılı..");
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            TxtId.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            TxtAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut4 = new SqlCommand("Update Tbl_Branslar Set BransAd=@m1 Where Bransid=@m2", bgl.baglanti());
            komut4.Parameters.AddWithValue("@m1", TxtAd.Text);
            komut4.Parameters.AddWithValue("@m2", TxtId.Text);
            komut4.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Branşlar Güncellendi , Başarılı");
        }
    }
}
