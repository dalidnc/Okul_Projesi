using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Proje_Ogrenci_Akademisyen.Entity;

namespace Proje_Ogrenci_Akademisyen.Formlar
{
    public partial class FrmNotlar : Form
    {
        public FrmNotlar()
        {
            InitializeComponent();
        }
        OgrenciSinavEntities db = new OgrenciSinavEntities();
        private void FrmNotlar_Load(object sender, EventArgs e)
        {
            comboBox1.DisplayMember = "DersAd";
            comboBox1.ValueMember = "DersID";
            comboBox1.DataSource = db.TblDersler.ToList();
            CmbAra.DisplayMember = "DersAd";
            CmbAra.ValueMember = "DersID";
            CmbAra.DataSource = db.TblDersler.ToList();
        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            TblNotlar t = new TblNotlar();
            t.Sinav1=byte.Parse(TxtSinav1.Text);
            t.Sinav2=byte.Parse(TxtSinav2.Text);
            t.Sinav3=byte.Parse(TxtSinav3.Text);
            t.Proje=byte.Parse(TxtProje.Text);
            t.Quiz1=byte.Parse(TxtQuiz1.Text);
            t.Quiz2=byte.Parse(TxtQuiz2.Text);
            t.Ders=int.Parse(comboBox1.SelectedValue.ToString());
            t.Ogrenci=int.Parse(TxtOgrenci.Text);
            db.TblNotlar.Add(t);
            db.SaveChanges();
            MessageBox.Show("Öğrenci not bilgisi için sisteme kaydedildi");

        }

        private void BtnHesapla_Click(object sender, EventArgs e)
        {
            int s1, s2, s3, q1, q2, proje;
            double ortalama;
            s1=Convert.ToSByte(TxtSinav1.Text);
            s2 = Convert.ToSByte(TxtSinav2.Text);
            s3 = Convert.ToSByte(TxtSinav3.Text);
            q1 = Convert.ToSByte(TxtQuiz1.Text);
            q2 = Convert.ToSByte(TxtQuiz2.Text);
            proje = Convert.ToSByte(TxtProje.Text);
            ortalama = (s1 + s2 + s3 + q1 + q2 +proje)/6;
            TxtOrtalama.Text =ortalama.ToString();
        }

        private void BtnListele_Click(object sender, EventArgs e)
        {
            //dataGridView1.DataSource=db.View_1.ToList();
            dataGridView1.DataSource = db.Notlar2();

        }

        private void CmbAra_SelectedIndexChanged(object sender, EventArgs e)
        {
            var degerler = from x in db.TblNotlar
                           select new
                           {
                               x.NotID,
                               x.TblDersler.DersAd,
                               Ogrenci_Adı=x.TblOgrenci.OgrAd+x.TblOgrenci.OgrSoyad,
                               x.Sinav1,
                               x.Sinav2,
                               x.Sinav3,
                               x.Quiz1,
                               x.Quiz2,
                               x.Proje,
                               x.Ortalama,
                               x.Ders
                             
                           };
          
            int d = int.Parse(CmbAra.SelectedValue.ToString());
            dataGridView1.DataSource = degerler.Where(y => y.Ders ==d).ToList();
            dataGridView1.Columns["Ders"].Visible = false;
        }

        private void BtnAra_Click(object sender, EventArgs e)
        {
            var degerler = from x in db.TblNotlar
                           select new
                           {
                               x.NotID,
                               x.TblDersler.DersAd,
                               Ogrenci_Adı = x.TblOgrenci.OgrAd + x.TblOgrenci.OgrSoyad,
                               x.Sinav1,
                               x.Sinav2,
                               x.Sinav3,
                               x.Quiz1,
                               x.Quiz2,
                               x.Proje,
                               x.Ortalama,
                               x.Ders,
                               x.Ogrenci

                           };
            int i = int.Parse(MskNumara.Text);
            dataGridView1.DataSource = degerler.Where(y => y.Ogrenci == i).ToList();
            dataGridView1.Columns["Ogrenci"].Visible = false;
        }

        private void BtnAra2_Click(object sender, EventArgs e)
        {
            string no = MskNumara.Text;
            var deger = db.TblOgrenci.Where(x => x.OgrNumara == no).Select(y=>y.OgrID).FirstOrDefault();
           // TxtID.Text = deger.ToString();//ilgili değerler arasındaki ilk değeri getirir.
            var notlar = from x in db.TblNotlar
                       select new
                       {
                           x.NotID,
                           x.TblDersler.DersAd,
                           Ogrenci_Adı = x.TblOgrenci.OgrAd + x.TblOgrenci.OgrSoyad,
                           x.Sinav1,
                           x.Sinav2,
                           x.Sinav3,
                           x.Quiz1,
                           x.Quiz2,
                           x.Proje,
                           x.Ortalama,
                           x.Ders,
                           x.Ogrenci
                       };

            dataGridView1.DataSource = notlar.Where(z=>z.Ogrenci==deger).ToList();
            dataGridView1.Columns["Ogrenci"].Visible = false;
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            int id = int.Parse(TxtID.Text);
            var x = db.TblNotlar.Find(id);
            x.Sinav1=int.Parse(TxtSinav1.Text);
            x.Sinav2=int.Parse(TxtSinav2.Text);
            x.Sinav3=int.Parse(TxtSinav3.Text);
            x.Quiz1 = int.Parse(TxtQuiz1.Text);
            x.Quiz2=int.Parse(TxtQuiz2.Text);
            x.Proje = int.Parse(TxtProje.Text);
            x.Ortalama=decimal.Parse(TxtOrtalama.Text);
            db.SaveChanges();
            MessageBox.Show("Öğrenci notları başarılı bir şekilde güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
          
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            TxtID.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            TxtSinav1.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            TxtSinav2.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            TxtSinav3.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            TxtQuiz1.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            TxtQuiz2.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
            TxtProje.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
            TxtOrtalama.Text=dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
            //comboBox1.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
        }
    }
}
