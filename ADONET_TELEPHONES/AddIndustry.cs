using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADONET_TELEPHONES
{
    public partial class AddIndustry : Form
    {
        Industry_rec rec;

        public AddIndustry(Industry_rec inrec)
        {
            InitializeComponent();

            rec = inrec;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length != 4)
            {
                MessageBox.Show("ind_id must be 4 symbols length", "did you know that", MessageBoxButtons.OK);
            }
            else
            if (textBox1.TextLength == 0 || textBox2.TextLength == 0 || 
                textBox3.TextLength == 0 || textBox4.TextLength == 0 || 
                textBox5.TextLength == 0)
            {
                MessageBox.Show("Not all fields are filled", "Hey you", MessageBoxButtons.OK);
            }
            else
            {
                rec.Id = textBox1.Text;
                rec.Name = textBox2.Text;
                rec.Country = textBox3.Text;
                rec.Type = textBox4.Text;
                rec.Website = textBox5.Text;

                DialogResult = DialogResult.OK;

                this.Close();
            }
        }
    }
}
