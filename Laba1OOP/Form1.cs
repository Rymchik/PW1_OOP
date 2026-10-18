using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Laba1OOP.Class;

namespace Laba1OOP
{
    public partial class Form1 : Form
    {
        private MonitorManager _manager = new MonitorManager();
        private List<Monitor> _currentView = new List<Monitor>();

        public Form1()
        {
            InitializeComponent();
            _manager.SetupDataGridView(dataGridView1);
            _manager.LoadInitialData();
            ShowMonitors(_manager.GetAll());
        }

        private void ShowMonitors(List<Monitor> list)
        {
            _currentView = list;

            dataGridView1.DataSource = list.Select(m => new
            {
                Model = m.GetModel(),
                Diagonal = m.GetDiagonal(),
                RefreshRate = m.GetRefreshRate(),
                Price = m.GetPrice(),
                HasHdr = m.GetHasHdr(),
                GamingIndex = m.GetGamingIndex(),
                PricePerInch = m.GetPricePerInch()
            }).ToList();
        }

        // Кнопка "Додати"
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Monitor monitor = new Monitor(
                    textBox1.Text,
                    (double)numericUpDown1.Value,
                    (int)numericUpDown2.Value,
                    numericUpDown3.Value,
                    checkBox1.Checked);

                _manager.Add(monitor);
                ShowMonitors(_manager.GetAll());

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
            ShowMonitors(_manager.GetAll());
        }

        // Кнопка "Пошук 144 Гц"
        private void button3_Click(object sender, EventArgs e)
        {
            ShowMonitors(_manager.FindByRefreshRate(144));
        }

        // Кнопка "Найдешевший HDR"
        private void button4_Click(object sender, EventArgs e)
        {
            Monitor cheapest = _manager.FindCheapestHdr();

            if (cheapest != null)
            {
                ShowMonitors(new List<Monitor> { cheapest });
                MessageBox.Show($"Найдешевший HDR-монітор: {cheapest.GetModel()} за {cheapest.GetPrice():N2} грн.",
                                "Результат пошуку", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Монітори з підтримкою HDR не знайдено.",
                                "Результат пошуку", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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
            if (index < 0 || index >= _currentView.Count) return;

            Monitor selected = _currentView[index];

            DialogResult result = MessageBox.Show($"Видалити монітор {selected.GetModel()}?",
                                                  "Підтвердження",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                _manager.Remove(selected);
                ShowMonitors(_manager.GetAll());
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
            _manager.SortByPrice();
            ShowMonitors(_manager.GetAll());
        }
    }
}