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
    public partial class FrmDoktorPaneli : Form
    {
        public FrmDoktorPaneli()
        {
            InitializeComponent();
        }
        sqlBaglantisi bgl = new sqlBaglantisi();

        private void FrmDoktorPaneli_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Tbl_Doktorlar", bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            SqlCommand kmt = new SqlCommand("Select BransAd From Tbl_Branslar", bgl.baglanti());
            SqlDataReader dr2 = kmt.ExecuteReader();
            while (dr2.Read())
            {
                CmbBrans.Items.Add(dr2[0].ToString());

            }
        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            SqlCommand komut2 = new SqlCommand("insert into Tbl_Doktorlar (DoktorAd,DoktorSoyad,DoktorBrans,DoktorTC,DoktorSifre) values (@r1,@r2,@r3,@r4,@r5) ", bgl.baglanti());
            komut2.Parameters.AddWithValue("@r1", TxtAd.Text);
            komut2.Parameters.AddWithValue("@r2", TxtSoyad.Text);
            komut2.Parameters.AddWithValue("@r3", CmbBrans.Text);
            komut2.Parameters.AddWithValue("@r4", MskTC.Text);
            komut2.Parameters.AddWithValue("@r5", TxtSifre.Text);
            komut2.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Kayıt Başarılı");
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            TxtAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            TxtSoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            CmbBrans.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            MskTC.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            TxtSifre.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();

        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut3 = new SqlCommand("Delete  From Tbl_Doktorlar Where DoktorTC=@f1", bgl.baglanti());
            komut3.Parameters.AddWithValue("@f1", MskTC.Text);
            komut3.ExecuteNonQuery();
            bgl.baglanti().Close();
            FrmDoktorPaneli fr = new FrmDoktorPaneli();
            fr.Show();
            this.Hide();
            MessageBox.Show("Kayıt Silindi");
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut4 = new SqlCommand("Update Tbl_Doktorlar Set DoktorAd=@m1,DoktorSoyad=@m2,DoktorBrans=@m3,DoktorSifre=@m5 Where DoktorTC=@m4", bgl.baglanti());
            komut4.Parameters.AddWithValue("@m1", TxtAd.Text);
            komut4.Parameters.AddWithValue("@m2", TxtSoyad.Text);
            komut4.Parameters.AddWithValue("@m3", CmbBrans.Text);
            komut4.Parameters.AddWithValue("@m4", MskTC.Text);
            komut4.Parameters.AddWithValue("@m5", TxtSifre.Text);
            komut4.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Doktor Güncellendi , Başarılı");
        }
    }
}
