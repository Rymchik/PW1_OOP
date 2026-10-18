using System;
using System.Data;
using System.Windows.Forms;
using Laba1OOP.Class;

namespace Laba1OOP
{
    public partial class Form1 : Form
    {
        private MonitorManager _manager = new MonitorManager();

        public Form1()
        {
            InitializeComponent();
            _manager.SetupDataGridView(dataGridView1);
            _manager.LoadInitialData();
            ShowTable(_manager.GetAllTable());
        }

        // Показ таблиці, отриманої від менеджера
        private void ShowTable(DataTable table)
        {
            dataGridView1.DataSource = table;
        }

        // Кнопка "Додати"
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ShowTable(_manager.AddMonitor(
                    textBox1.Text,
                    (double)numericUpDown1.Value,
                    (int)numericUpDown2.Value,
                    numericUpDown3.Value,
                    checkBox1.Checked));

                textBox1.Clear();
                numericUpDown1.Value = numericUpDown1.Minimum;
                numericUpDown2.Value = numericUpDown2.Minimum;
                numericUpDown3.Value = numericUpDown3.Minimum;
                checkBox1.Checked = false;
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Помилка введення",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Кнопка "Показати все"
        private void button2_Click(object sender, EventArgs e)
        {
            ShowTable(_manager.GetAllTable());
        }

        // Кнопка "Пошук 144 Гц"
        private void button3_Click(object sender, EventArgs e)
        {
            ShowTable(_manager.GetByRefreshRateTable(144));
        }

        // Кнопка "Найдешевший HDR"
        private void button4_Click(object sender, EventArgs e)
        {
            ShowTable(_manager.GetCheapestHdrTable());
        }

        // Кнопка "Видалити"
        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Оберіть монітор для видалення.",
                                "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int index = dataGridView1.CurrentRow.Index;
            string model = _manager.GetModelAt(index);
            if (model == null) return;

            DialogResult result = MessageBox.Show($"Видалити монітор {model}?",
                                                  "Підтвердження",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                _manager.DeleteFromView(index);
                ShowTable(_manager.GetAllTable());
            }
        }

        // Кнопка "Статистика"
        private void button6_Click(object sender, EventArgs e)
        {
            MessageBox.Show(_manager.GetStatistics(), "Статистика",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Кнопка "Сортувати за ціною"
        private void button7_Click(object sender, EventArgs e)
        {
            ShowTable(_manager.GetSortedByPriceTable());
        }
    }
}