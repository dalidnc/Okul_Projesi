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
    public partial class FrmOgrenciPanel : Form
    {
        public FrmOgrenciPanel()
        {
            InitializeComponent();
        }
        public string numara;
        public int ogrID;
        OgrenciSinavEntities db = new OgrenciSinavEntities();
        private void FrmOgrenciPanel_Load(object sender, EventArgs e)
        {
            TxtNumara.Text = numara;
            TxtAd.Text=db.TblOgrenci.Where(x=>x.OgrNumara==numara).Select(y=>y.OgrAd).FirstOrDefault();
            TxtSoyad.Text = db.TblOgrenci.Where(x => x.OgrNumara == numara).Select(y => y.OgrSoyad).FirstOrDefault();
            TxtMail.Text = db.TblOgrenci.Where(x => x.OgrNumara == numara).Select(y => y.OgrMail).FirstOrDefault();
            TxtSifre.Text = db.TblOgrenci.Where(x => x.OgrNumara == numara).Select(y => y.OgrSifre).FirstOrDefault();
            TxtBolum.Text = db.TblOgrenci.Where(x => x.OgrNumara == numara).Select(y => y.OgrDurum).FirstOrDefault().ToString();

             ogrID = db.TblOgrenci.Where(x => x.OgrNumara == numara).Select(y => y.OgrID).FirstOrDefault();

            var sinavnotlari = (from x in db.TblNotlar
                               select new
                               {
                                   x.TblDersler.DersAd,
                                   x.Sinav1,
                                   x.Sinav2,
                                   x.Sinav3,
                                   x.Quiz1,
                                   x.Quiz2,
                                   x.Proje,
                                   x.Ortalama,
                                   x.Ogrenci
                               }).Where(y=>y.Ogrenci==ogrID).ToList();
            dataGridView1.DataSource = sinavnotlari;
        }



        private void button2_Click(object sender, EventArgs e)
        {
            if (TxtYeniSifre1.Text == TxtYeniSifre2.Text)
            {
                var deger = db.TblOgrenci.Find(ogrID);
                deger.OgrSifre = TxtYeniSifre1.Text;
                db.SaveChanges();
                MessageBox.Show("Şifre Değiştirme işlemi başarılı bir şekilde gerçekleşti");
            }
            else
            {
                MessageBox.Show("Girdiğiniz yeni şifreler birbirleri ile uyuşmuyor");
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {
           
        }
    }
}
