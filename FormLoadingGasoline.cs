using System;
using System.IO;
using System.Windows.Forms;

namespace Gas_Station
{
    public partial class FormLoadingGasoline : Form
    {
        Form PrevForm;
        public FormLoadingGasoline(Form parent)
        {
            InitializeComponent();
            PrevForm = parent;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.PerformStep();
            label2.Text = "Осталось залить: " + --GasStation.AmountOfGasoline + " литров.";
            label3.Text = "Залито: " + ++GasStation.LoadedGasoline + " литров.";
            if (progressBar1.Value == progressBar1.Maximum)
            {
                timer1.Stop();
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = true;
                return;
            }
        }

        private void FormLoadingGasoline_Load(object sender, EventArgs e)
        {
            // изменение текста лейбла
            label1.Text += " " + GasStation.SelectedGasType + ".";
            label2.Text += " " + GasStation.AmountOfGasoline + " литров.";
            // настройка прогрессбара
            progressBar1.Maximum = (int)GasStation.AmountOfGasoline;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            button1.Enabled = true;
            button2.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            button1.Enabled = false;
            button2.Enabled = true;
            button3.Enabled = true;
        }


        private void button3_Click(object sender, EventArgs e)
        {
            if (GasStation.AmountOfGasoline > 0)
            {
                DialogResult dialogResult = MessageBox.Show("У вас осталось незалитым " + GasStation.AmountOfGasoline.ToString() + " литров бензина.\nПри продолжении они будут утеряны!\nПродолжить?", "Подверждение действия", MessageBoxButtons.OKCancel);
                if (dialogResult == DialogResult.Cancel)
                    return;
            }
            Close();
        }

        // функция для отправки денег на благотворительность
        private void MoneyShare() => MessageBox.Show("СПАСИБО!");

        private void FormLoadingGasoline_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (GasStation.AmountOfMoney > 0)
            {
                DialogResult dialogResult = MessageBox.Show("Вы имеете возможность отправить Ваш остаток денег на благотворительность.\nЖелаете сделать добро?", "Добро людям", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                    MoneyShare();
            }

            using (StreamWriter OpLogSW = new StreamWriter($"..\\..\\Чеки\\Чек.txt", false, System.Text.Encoding.Default))
            {
                OpLogSW.Write(
                    $"Чек по операции от {DateTime.UtcNow.ToLongDateString()}\n" +
                    $"Вид бензина: {GasStation.SelectedGasType}\n" +
                    $"Объем залитого бензина: {GasStation.LoadedGasoline}\n" +
                    $"Цена всего объема бензина: {GasStation.RequiredMoney}\n" +
                    $"Предоставлено к оплате: {GasStation.AmountOfMoney}\n" +
                    $"Остаток по операции: {GasStation.AmountOfMoney-GasStation.RequiredMoney}\n" +
                    $"Спасибо за покупку!"
                    );
            }

            //очищааем данные терминала о работе
            GasStation.AmountOfGasoline = 0;
            GasStation.AmountOfMoney = 0;
            GasStation.RequiredMoney = 0;
            GasStation.LoadedGasoline = 0;
            GasStation.SelectedGasType = "";
            for (int i = Application.OpenForms.Count - 2; i > 1; --i)
                Application.OpenForms[i].Close();
            Application.OpenForms[0].Show();
        }
    }
}
