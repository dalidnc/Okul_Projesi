using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proje_Ogrenci_Akademisyen.Formlar
{
    public partial class FrmGiris : Form
    {
        public FrmGiris()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=LAPTOP-8LKDD961\\SQLEXPRESS;Initial Catalog=OgrenciSinav;Integrated Security=True;Encrypt=False");
        private void BtnGirisYap_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * from TblOgrenci Where OgrNumara=@p1 and OgrSifre=@p2",baglanti);
            komut.Parameters.AddWithValue("@p1",MskNumara.Text);
            komut.Parameters.AddWithValue("@p2",TxtSifre.Text);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                FrmOgrenciPanel frm = new FrmOgrenciPanel();
                frm.numara = MskNumara.Text;
                frm.ShowDialog();
                this.Hide();

            }
            else
            {
                MessageBox.Show("Numaranız veya şifreniz hataldır");
            }
            baglanti.Close();
        }

        private void MskNumara_TextChanged(object sender, EventArgs e)
        {
            if(MskNumara.Text=="00000" && TxtSifre.Text=="000")
            {
                //Yeni Form
                FrmHarita frm = new FrmHarita();
                frm.Show();
                this.Hide();
            }
        }

        private void TxtSifre_TextChanged(object sender, EventArgs e)
        {
            if(TxtSifre.Text=="000" && MskNumara.Text=="00000")
            {
                //Yeni Form
            }
        }
    }
}
