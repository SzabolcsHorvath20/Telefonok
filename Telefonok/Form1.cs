using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Telefonok
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dGV_Telefonok.ColumnCount = 4;
            dGV_Telefonok.Columns[0].Name = "id";
            dGV_Telefonok.Columns[1].Name = "marka";
            dGV_Telefonok.Columns[1].HeaderText = "Márka";
            dGV_Telefonok.Columns[2].Name = "tipus";
            dGV_Telefonok.Columns[2].HeaderText = "Típus";
            dGV_Telefonok.Columns[3].Name = "ar";
            dGV_Telefonok.Columns[3].HeaderText = "Ár";
            TelefonokSelect();
        }
        private void TelefonokSelect()
        {
            dGV_Telefonok.Rows.Clear();
            Program.sql.CommandText = "SELECT `id`, `marka`, `tipus`, `ar` FROM `telefon_2`";
            try
            {
                using (MySqlDataReader dataReader = Program.sql.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        int sor_index = dGV_Telefonok.Rows.Add();
                        dGV_Telefonok.Rows[sor_index].Cells["id"].Value = dataReader.GetInt32("id");
                        dGV_Telefonok.Rows[sor_index].Cells["marka"].Value = dataReader.GetString("marka");
                        dGV_Telefonok.Rows[sor_index].Cells["tipus"].Value = dataReader.GetString("tipus");
                        dGV_Telefonok.Rows[sor_index].Cells["ar"].Value = dataReader.GetInt32("ar");
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            Program.sql.CommandText = "INSERT INTO telefon_2(marka, tipus, ar) VALUES (@marka,@tipus,@ar)";
            Program.sql.Parameters.Clear();
            Program.sql.Parameters.AddWithValue("@marka", textBox1.Text);
            Program.sql.Parameters.AddWithValue("@tipus", textBox2.Text);
            Program.sql.Parameters.AddWithValue("@ar", (int)numericUpDown1.Value);
            try
            {
                Program.sql.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            TelefonokSelect();
        }
    }
}
