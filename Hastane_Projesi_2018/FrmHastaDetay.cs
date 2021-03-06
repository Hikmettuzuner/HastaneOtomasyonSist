﻿using System;
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
    public partial class FrmHastaDetay : Form
    {
        public FrmHastaDetay()
        {
            InitializeComponent();
        }
        public string tc;
        sqlBaglantisi bgl = new sqlBaglantisi();

        private void BtnRandevuAl_Click(object sender, EventArgs e)
        {
            SqlCommand komut5 = new SqlCommand("Update  Tbl_Randevular Set RandevuDurum=1, HastaTC=@p2 ,HastaSikayet=@p3 Where Randevuid=@p0", bgl.baglanti());
            komut5.Parameters.AddWithValue("@p2", LblTC.Text);
            komut5.Parameters.AddWithValue("@p3", RchSikayet.Text);
            komut5.Parameters.AddWithValue("@p0", Txtid.Text);

            komut5.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Kayıt Başarılı..");
        }

        private void FrmHastaDetay_Load(object sender, EventArgs e)
        {
            LblTC.Text = tc;

            // AdSoyad Çekme
            SqlCommand komut = new SqlCommand("SELECT HastaAd,HastaSoyad FROM Tbl_Hastalar WHERE HastaTC=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", LblTC.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                LblAdSoyad.Text = dr[0] + "" + dr[1];
            }
            bgl.baglanti().Close();

            //Randevu Geçmişi
            SqlCommand komutt = new SqlCommand("SELECT * FROM Tbl_Randevular WHERE HastaTC=@p2", bgl.baglanti());
            komutt.Parameters.AddWithValue("@p2", LblTC.Text);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(komutt);
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            //Branşların Çekimi
            SqlCommand komut2 = new SqlCommand("SELECT BransAd FROM Tbl_Branslar", bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                CmbBrans.Items.Add(dr2[0]);
            }
            bgl.baglanti().Close();

        }

        private void CmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            CmbDoktor.Items.Clear();
            SqlCommand komut3 = new SqlCommand("SELECT DoktorAd,DoktorSoyad FROM Tbl_Doktorlar WHERE DoktorBrans=@p1", bgl.baglanti());
            komut3.Parameters.AddWithValue("@p1", CmbBrans.Text);
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                CmbDoktor.Items.Add(dr3[0] + " " + dr3[1]);
            }
            bgl.baglanti().Close();
        }

        private void CmbDoktor_SelectedIndexChanged(object sender, EventArgs e)
        {

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Tbl_Randevular WHERE RandevuBrans='" + CmbBrans.Text + "'" + "and RandevuDoktor='" + CmbDoktor.Text + "' and RandevuDurum=0  ", bgl.baglanti());
            da.Fill(dt);
            dataGridView2.DataSource = dt;

        }

        private void LnkBilgiDuzenle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmBilgiDuzenle fr = new FrmBilgiDuzenle();
            fr.TCNo = LblTC.Text;
            fr.Show();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView2.SelectedCells[0].RowIndex;
            Txtid.Text = dataGridView2.Rows[secilen].Cells[0].Value.ToString();
        }
    }
}
