using System;
using System.Windows.Forms;

namespace Gas_Station
{
    public partial class FormPayment : Form
    {
        // переменная для сохранения адреса предыдущей формы
        Form PrevForm;

        public FormPayment(Form parent)
        {
            InitializeComponent();
            PrevForm = parent;
        }

        private void FormPayment_FormClosing(object sender, FormClosingEventArgs e)
        {
            PrevForm.Visible = true;
        }

        private void FormPayment_Load(object sender, EventArgs e)
        {
            // высчитывание цены, которую надо заплатить клиенту
            GasStation.RequiredMoney = GasStation.GasTypes[GasStation.SelectedGasType] * GasStation.AmountOfGasoline;
            labelPrice.Text = "К оплате: " + GasStation.RequiredMoney.ToString() + "₽.";

            InsertedMoneyAmountUpdate();
        }

        #region Обновление лейбла с данными о введенных деньгах
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            InsertedMoneyAmountUpdate();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            InsertedMoneyAmountUpdate();
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            InsertedMoneyAmountUpdate();
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            InsertedMoneyAmountUpdate();
        }
        #endregion

        //метод для обновления лэйбла с данными о введенных деньгах
        private void InsertedMoneyAmountUpdate()
        {
            GasStation.AmountOfMoney = (long)(numericUpDown1.Value * 1000 + numericUpDown2.Value * 500 + numericUpDown3.Value * 50 + numericUpDown4.Value * 100);
            labelInsertedMoney.Text = "К оплате предоставлено: " + GasStation.AmountOfMoney + "₽.";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (GasStation.AmountOfMoney < GasStation.RequiredMoney)
            {
                MessageBox.Show("Введенной суммы не хватает для оплаты услуги!");
                return;
            }
            if (GasStation.AmountOfMoney > GasStation.RequiredMoney)
            {
                DialogResult dialogResult = MessageBox.Show("Количество введенных денег избытычно!\nАвтомат не предоставляет сдачи!\nХотите продолжить?", "Подверждение действия", MessageBoxButtons.OKCancel);
                if (dialogResult == DialogResult.Cancel)
                    return;
            }
            this.Visible = false;
            FormLoadingGasoline frm = new FormLoadingGasoline(this);
            frm.Show();
        }

        public void SetAmountOfGasolin(int AmountOfGasoline)
        {
            GasStation.AmountOfGasoline = AmountOfGasoline;
        }

    }
}
