using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ADONET_TELEPHONES
{
    public partial class AddPhone : Form
    {
        Phone_rec rec;

        public AddPhone(Phone_rec phrec, List<string> ind_id)
        {
            InitializeComponent();

            rec = phrec;

            comboBox1.Items.AddRange(ind_id.ToArray());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.TextLength == 0 || textBox2.TextLength == 0 || 
                comboBox1.SelectedItem == null || textBox4.TextLength == 0 || 
                textBox5.TextLength == 0)
            {
                MessageBox.Show("Not all fields are filled", "Hey you", MessageBoxButtons.OK);
            }
            else
            {
                rec.Id = Convert.ToInt32(textBox1.Text);
                rec.Model = textBox2.Text;
                rec.Ind_id = comboBox1.SelectedItem.ToString();
                rec.Year = Convert.ToInt32(textBox4.Text);
                rec.Price = Convert.ToDouble(textBox5.Text);

                DialogResult = DialogResult.OK;

                this.Close();
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 43 && number != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 43 && number != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 43 && number != 46 && number != 8)
            {
                e.Handled = true;
            }
        }
    }
}
