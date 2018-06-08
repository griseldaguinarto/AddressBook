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
using System.Text.RegularExpressions;

namespace AddressBook
{
    public partial class FrmTambahData : Form
    {
        bool _result = false;

        bool _addMode = false; // true: add item, false: edit item

        People ppl = new People();
        People temp = new People();

       // List<People> list = new List<People>();

        public bool Run(FrmTambahData form)
        {
            form.ShowDialog();
            return _result;
        }

        public FrmTambahData(bool addMode, People p1 = null)
        {
            InitializeComponent();
            _addMode = addMode;

            if (p1 != null)
            {
                temp = p1;
                this.txtNama.Text = p1.Nama;
                this.txtAlamat.Text = p1.Alamat;
                this.txtKota.Text = p1.Kota;
                this.txtNoHp.Text = p1.NoHP;
                this.dtpTglLahir.Value = p1.Tanggal.Date;
                this.txtEmail.Text = p1.Email;
            }
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            // validasi
            if (this.txtNama.Text.Trim() == "") // jika isian nama kosong
            {
                MessageBox.Show("Sorry, nama wajib isi...", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtNama.Focus();
            }
            else if (this.txtAlamat.Text.Trim() == "")
            {
                MessageBox.Show("Sorry, alamat wajib isi...", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtAlamat.Focus();
            }
            else if (this.txtKota.Text.Trim() == "")
            {
                MessageBox.Show("Sorry, kota wajib isi...", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtKota.Focus();
            }
            else if (this.txtNoHp.Text.Trim() == "")
            {
                MessageBox.Show("Sorry, no Hp wajib isi...", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtNoHp.Focus();
            }
            else if (this.txtEmail.Text.Trim() == "")
            {
                MessageBox.Show("Sorry, email wajib isi...", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtEmail.Focus();
            }

            else
            {
                try
                {
                    AddressController controller = new AddressController();
                    controller.SaveData(_addMode, people(ppl), temp);
                    _result = true;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnBatal_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool EmailIsValid(string emailAddr)
        {
            string emailPattern1 = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
            Regex regex = new Regex(emailPattern1);
            Match match = regex.Match(emailAddr);
            return match.Success;
        }

        private void txtEmail_Leave(object sender, EventArgs e)
        {
            if (this.txtEmail.Text.Trim() != "")
            {
                if (!EmailIsValid(this.txtEmail.Text))
                {
                    MessageBox.Show("Sorry, data email tidak valid ...", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.txtEmail.Clear();
                    this.txtEmail.Focus();
                }
            }
        }

        private void alphabetKP(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
                e.Handled = true;
        }

        private void numericKP(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private People people(People p)
        {
            p.Nama = this.txtNama.Text;
            p.Alamat = this.txtAlamat.Text;
            p.Kota = this.txtKota.Text;
            p.NoHP = this.txtNoHp.Text;
            p.Tanggal = this.dtpTglLahir.Value;
            p.Email = this.txtEmail.Text;

            return p;
        }
    }
}
