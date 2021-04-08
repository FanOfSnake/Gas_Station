using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gas_Station
{
    public partial class FormAmountOfGasoline : Form
    {
        Form PrevForm;
        public FormAmountOfGasoline(Form parent)
        {
            InitializeComponent();
            PrevForm = parent;
        }

        private void FormAmountOfGasoline_Load(object sender, EventArgs e)
        {


            numericUpDown1.Maximum = 700;
            labelCost.Text = "Цена: " + (int)numericUpDown1.Value * GasStation.GasTypes[GasStation.SelectedGasType] + "₽.";
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            labelCost.Text = "Цена: " + (int)numericUpDown1.Value * GasStation.GasTypes[GasStation.SelectedGasType] + "₽.";
        }

        private void FormAmountOfGasoline_FormClosing(object sender, FormClosingEventArgs e)
        {
            PrevForm.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // запоминаем выбор пользователя
            GasStation.AmountOfGasoline = (double)numericUpDown1.Value;

            if(numericUpDown1.Value > 40)
            {
                DialogResult dialogResult = MessageBox.Show("Внимание!\nВыбранное количество бензина может быть слишком большое!\nХотите продолжить?", "Подверждение действия", MessageBoxButtons.OKCancel);
                if (dialogResult == DialogResult.Cancel)
                    return;
            }
            this.Visible = false;
            FormPayment frm = new FormPayment(this);
            frm.Show();
        }
    }
}
