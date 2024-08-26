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
    public partial class Bolumler : Form
    {
        public Bolumler()
        {
            InitializeComponent();
        }
        OgrenciSinavEntities db = new OgrenciSinavEntities();

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            if(TxtBolumAd.Text == "")
            {
                errorProvider1.SetError(TxtBolumAd, "Bölüm Adı boş geçilemez");
            }
            else
            {
                TblBolum t = new TblBolum();
                t.BolumAd=TxtBolumAd.Text;
                db.TblBolum.Add(t);
                db.SaveChanges();
                MessageBox.Show("Bölüm ekleme işlemi başarılı bir şekilde gerçekleşti","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }
    }
}
