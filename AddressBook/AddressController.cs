using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace AddressBook
{
   public class AddressController
    {
        // property
        public List<People> ListData { get; set; } = null;

        // constructor
        public AddressController()
        {
            ListData = new List<People>();
            try
            {
                if (File.Exists(Properties.Settings.Default.NamaFile))
                {
                    string[] fileContent = File.ReadAllLines(Properties.Settings.Default.NamaFile);
                    foreach (string item in fileContent)
                    {
                        string[] arrItem = item.Split(';');
                        ListData.Add(new People
                        {
                            Nama = arrItem[0].Trim(),
                            Alamat = arrItem[1].Trim(),
                            Kota = arrItem[2].Trim(),
                            NoHP = arrItem[3].Trim(),
                            Tanggal = Convert.ToDateTime(arrItem[4].Trim()),
                            Email = arrItem[5].Trim(),
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void loadData(DataGridView dgvData, Label lbl)
        {
            try
            {
                dgvData.Rows.Clear();
                AddressController controller = new AddressController();
                ListData = controller.ListData;
                foreach (People p in ListData)
                {
                    dgvData.Rows.Add(new string[] { p.Nama, p.Alamat, p.Kota, p.NoHP, p.Tanggal.ToShortDateString(), p.Email });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Address", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                lbl.Text = $"{dgvData.Rows.Count.ToString("n0")} Record data.";
            }
        }

        public void HapusData(People p, int rowIndex, DataGridView dgvData)
        {
            p.Nama = dgvData.CurrentRow.Cells[0].Value.ToString();
            p.Alamat = dgvData.CurrentRow.Cells[1].Value.ToString();
            p.Kota = dgvData.CurrentRow.Cells[2].Value.ToString();
            p.NoHP = dgvData.CurrentRow.Cells[3].Value.ToString();
            p.Tanggal = Convert.ToDateTime(dgvData.CurrentRow.Cells[4].Value).Date;
            p.Email = dgvData.CurrentRow.Cells[5].Value.ToString();

            rowIndex = dgvData.CurrentCell.RowIndex;
            if (MessageBox.Show("Are you sure you want to delete this data?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                dgvData.Rows.RemoveAt(rowIndex);
                MessageBox.Show("Data is deleted.");
            }

            string[] line = File.ReadAllLines("addressbook.csv");

            using (FileStream fs = new FileStream("addressbook.csv", FileMode.Create, FileAccess.ReadWrite))
            {
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    for (int i = 0; i < line.Length; i++)
                    {
                        if (line[i] != $"{p.Nama};{p.Alamat};{p.Kota};{p.NoHP};{p.Tanggal.ToShortDateString()};{p.Email}")
                        {
                            writer.WriteLine(line[i]);
                        }
                    }
                }
            }
        }

        public void SaveData(bool addMode, People p, People temp)
        {
            if (addMode) // add new item 
            {
                using (FileStream fs = new FileStream("addressbook.csv", FileMode.Append, FileAccess.Write))
                {
                    using (StreamWriter writer = new StreamWriter(fs))
                    {
                        writer.WriteLine($"{p.Nama};{p.Alamat};{p.Kota};{p.NoHP};{p.Tanggal.ToShortDateString()};{p.Email}");
                    }
                }
            }
            else // edit data
            {
                string[] line = File.ReadAllLines("addressbook.csv");

                using (FileStream fs = new FileStream("addressbook.csv", FileMode.Create, FileAccess.ReadWrite))
                {
                    using (StreamWriter writer = new StreamWriter(fs))
                    {
                        for (int i = 0; i < line.Length; i++)
                        {
                            if (line[i] == $"{temp.Nama};{temp.Alamat};{temp.Kota};{temp.NoHP};{temp.Tanggal.ToShortDateString()};{temp.Email}")
                            {
                                writer.WriteLine($"{p.Nama};{p.Alamat};{p.Kota};{p.NoHP};{p.Tanggal.ToShortDateString()};{p.Email}");
                            }
                            else
                            {
                                writer.WriteLine(line[i]);
                            }
                        }
                    }
                }
            }
        }

        public void FilterData()
        {

        }

    }
}

