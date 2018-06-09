using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace AddressBook
{
    public partial class FrmAddressBook : Form
    {
        People p = new People();
        People temp = new People();
        int rowIndex;

        public FrmAddressBook()
        {
            InitializeComponent();
        }

        private void FrmAddressBook_Load(object sender, EventArgs e)
        {
            AddressController controller = new AddressController();
            controller.loadData(dgvData, lblBanyakRecordData);
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            FrmTambahData ftm = new FrmTambahData(true);
            ftm.Run(ftm);
            FrmAddressBook_Load(null, null);    
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            AddressController controller = new AddressController();
            FrmTambahData ftm = new FrmTambahData(false, people(temp));
            ftm.Run(ftm);
            FrmAddressBook_Load(null, null);
        }

        private void btnHapus_Click(object sender, EventArgs e)
        { 
            AddressController controller = new AddressController();
            controller.HapusData(p, rowIndex, dgvData);
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            AddressController controller = new AddressController();
            string[] data = { txtNama.Text, txtAlamat.Text, txtKota.Text, txtNoHp.Text, txtTglLahir.Text, txtEmail.Text };
            if (txtNama.Text != "" || txtAlamat.Text != "" || txtKota.Text != "" || txtNoHp.Text != "" || txtTglLahir.Text != "" || txtEmail.Text != "")
            {
                controller.FilterData(dgvData, lblBanyakRecordData, data);
            }
            else
            {
                FrmAddressBook_Load(null, null);
            }
        }

        private People people(People p)
        {
            p.Nama = dgvData.CurrentRow.Cells[0].Value.ToString();
            p.Alamat = dgvData.CurrentRow.Cells[1].Value.ToString();
            p.Kota = dgvData.CurrentRow.Cells[2].Value.ToString();
            p.NoHP = dgvData.CurrentRow.Cells[3].Value.ToString();
            p.Tanggal = Convert.ToDateTime(dgvData.CurrentRow.Cells[4].Value).Date;
            p.Email = dgvData.CurrentRow.Cells[5].Value.ToString();

            return p;
        }

    }
}





