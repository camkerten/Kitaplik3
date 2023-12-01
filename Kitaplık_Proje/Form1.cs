using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Kitaplık_Proje
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\MSI\Desktop\Kitaplık.mdb


        OleDbConnection baglanti = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\MSI\\Desktop\\Kitaplık.mdb");
        void listele()
        {
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter("select * from Kitaplar",baglanti);
            da.Fill(dt);
            dataGridView1.DataSource = dt;



        }
        private void Form1_Load(object sender, EventArgs e)
        {
            listele();



        }

        private void btnlistele_Click(object sender, EventArgs e)
        {
            listele();
        }
        string durum = "";
        private void btnkaydet_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand komut1 = new OleDbCommand("insert into Kitaplar (Kitapadı,Yazar,Tur,Sayfa,Durum) values (@p1,@p2,@p3,@p4,@p5)", baglanti);
            komut1.Parameters.AddWithValue("@p1", txtkitapad.Text);
            komut1.Parameters.AddWithValue("@p2", txtyazar.Text);
            komut1.Parameters.AddWithValue("@p3",combotur.Text);
            komut1.Parameters.AddWithValue("@p4", txtsayfa.Text);
            komut1.Parameters.AddWithValue("@p5", durum);
            komut1.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kitap listeye kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();




        }

        private void radiokullanılmıs_CheckedChanged(object sender, EventArgs e)
        {
            durum = "0";


        }

        private void radiosıfır_CheckedChanged(object sender, EventArgs e)
        {
            durum = "1";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtkitapid.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtkitapad.Text= dataGridView1.Rows[secilen].Cells[1].Value.ToString(); 
            txtyazar.Text= dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            combotur.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            txtsayfa.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();  
            if (dataGridView1.Rows[secilen].Cells[5].Value.ToString() == "True")
            {
                radiosıfır.Checked = true;
            }
            else
            {
                radiokullanılmıs.Checked = true;
            }
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("Delete from Kitaplar where Kitapid = @p1",baglanti);
            komut.Parameters.AddWithValue("@p1",txtkitapid.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kitap Silinmistir.","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
            listele();




        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("update kitaplar set Kitapadı=@p1,Yazar=@p2,Tur=@p3,Sayfa=@p4,Durum=@p5 where Kitapid=@p6", baglanti);
            komut.Parameters.AddWithValue("@p1",txtkitapad.Text);
            komut.Parameters.AddWithValue("@p2",txtyazar.Text);
            komut.Parameters.AddWithValue("@p3",combotur.Text);
            komut.Parameters.AddWithValue("@p4",txtsayfa.Text);
            if(radiokullanılmıs.Checked==true)
            {
                komut.Parameters.AddWithValue("@p5", durum);
            }
            if(radiosıfır.Checked==true)
            {
                komut.Parameters.AddWithValue("@p5", durum);
            }
                    

            komut.Parameters.AddWithValue("@p6", txtkitapid.Text);
            komut.ExecuteNonQuery() ;
            baglanti.Close() ;
            MessageBox.Show("Kitap Bilgileriniz Guncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele() ;
            baglanti.Close();



        }

        private void btnkitapbul_Click(object sender, EventArgs e)
        {
            

            OleDbCommand komut = new OleDbCommand("Select * from kitaplar where kitapadı=@p1", baglanti);
            komut.Parameters.AddWithValue("@p1",txtkitapbul.Text);
           
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(komut);
            da.Fill(dt);
           dataGridView1.DataSource = dt ;
            


            
               

            



        }

        private void btnAra_Click(object sender, EventArgs e)
        {

            OleDbCommand komut = new OleDbCommand("Select * from kitaplar where kitapadı like '%" +txtkitapbul.Text +"%'", baglanti);
            

            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(komut);
            da.Fill(dt);
            dataGridView1.DataSource = dt;



        }
    }
}
