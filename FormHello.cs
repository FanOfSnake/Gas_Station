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
    public partial class FormHello : Form
    {
        public FormHello()
        {
            InitializeComponent();
        }

        private void FormHello_Load(object sender, EventArgs e)
        {
            //  Заполнение комбобокса наименованиями бензина
            foreach (KeyValuePair<string, double> keyValue in GasStation.GasTypes)
                comboBox1.Items.Add(keyValue.Key.ToString());

            comboBox1.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GasStation.SelectedGasType = comboBox1.SelectedItem.ToString();
            labelCost.Text = "Цена: " + GasStation.GasTypes[GasStation.SelectedGasType] + "₽.";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // запоминаем выбор пользователя
            GasStation.SelectedGasType = comboBox1.SelectedItem.ToString();
            this.Visible = false;
            FormAmountOfGasoline frm = new FormAmountOfGasoline(this);
            frm.Show();
        }
    }
}
