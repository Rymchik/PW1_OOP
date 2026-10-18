using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Laba1OOP.Class
{
    public class MonitorManager
    {
        private List<Monitor> _monitors = new List<Monitor>();
        // Налаштування стовпців таблиці
        public void SetupDataGridView(DataGridView grid)
        {
            grid.AutoGenerateColumns = false;
            grid.Columns.Clear();
            grid.ReadOnly = true;
            grid.AllowUserToAddRows = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.MultiSelect = false;

            grid.Columns.Add(new DataGridViewTextBoxColumn
            { DataPropertyName = "Model", HeaderText = "Модель", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            grid.Columns.Add(new DataGridViewTextBoxColumn
            { DataPropertyName = "Diagonal", HeaderText = "Діагональ (\")" });
            grid.Columns.Add(new DataGridViewTextBoxColumn
            { DataPropertyName = "RefreshRate", HeaderText = "Частота (Гц)" });
            grid.Columns.Add(new DataGridViewTextBoxColumn
            { DataPropertyName = "Price", HeaderText = "Ціна (грн)" });
            grid.Columns.Add(new DataGridViewCheckBoxColumn
            { DataPropertyName = "HasHdr", HeaderText = "HDR" });
            grid.Columns.Add(new DataGridViewTextBoxColumn
            { DataPropertyName = "GamingIndex", HeaderText = "Ігровий індекс" });
            grid.Columns.Add(new DataGridViewTextBoxColumn
            { DataPropertyName = "PricePerInch", HeaderText = "Ціна за дюйм" });
        }
        // Завантаження початкових даних
        public void LoadInitialData()
        {
            Add(new Monitor("Dell SE2422H", 23.8, 75, 4200m, false));
            Add(new Monitor("ASUS TUF Gaming VG249Q", 23.8, 144, 6800m, false));
            Add(new Monitor("LG UltraGear 27GP850-B", 27.0, 165, 13500m, true));
            Add(new Monitor("Samsung Odyssey G5", 32.0, 144, 11000m, true));
            Add(new Monitor("Gigabyte G27F", 27.0, 144, 7500m, true));
            Add(new Monitor("AOC 24G2U", 23.8, 144, 6200m, false));
            Add(new Monitor("Acer Nitro VG271U", 27.0, 170, 9800m, true));
            Add(new Monitor("MSI Optix G242", 23.8, 60, 3900m, false));
            Add(new Monitor("BenQ MOBIUZ EX2710Q", 27.0, 165, 14200m, true));
            Add(new Monitor("Philips 242E1GSJ", 23.8, 144, 5400m, false));
            Add(new Monitor("Samsung Odyssey G7", 32.0, 240, 21500m, true));
            Add(new Monitor("LG 24MP60G", 23.8, 75, 4800m, false));
            Add(new Monitor("ASUS ROG Swift PG279QM", 27.0, 240, 25000m, true));
            Add(new Monitor("HP X27", 27.0, 165, 8700m, false));
            Add(new Monitor("Dell Alienware AW3423DW", 34.0, 175, 39000m, true));
        }
        public void Add(Monitor monitor)
        {
            if (monitor == null)
                throw new ArgumentNullException(nameof(monitor));
            _monitors.Add(monitor);
        }

        public bool Remove(Monitor monitor)
        {
            return _monitors.Remove(monitor);
        }
        public void RemoveAt(int index)
        {
            if (index < 0 || index >= _monitors.Count)
                throw new ArgumentOutOfRangeException(nameof(index), "Некоректний індекс.");
            _monitors.RemoveAt(index);
        }
        public int GetCount()
        {
            return _monitors.Count;
        }
        public List<Monitor> GetAll()
        {
            return new List<Monitor>(_monitors);
        }
        public List<string> GetAllDescriptions()
        {
            return _monitors.Select(m => m.GetDescription()).ToList();
        }

        public List<Monitor> FindByRefreshRate(int rate)
        {
            return _monitors.Where(m => m.GetRefreshRate() == rate).ToList();
        }
        public Monitor FindCheapestHdr()
        {
            return _monitors
                .Where(m => m.GetHasHdr())
                .OrderBy(m => m.GetPrice())
                .FirstOrDefault();
        }
        public decimal GetTotalPrice()
        {
            return _monitors.Sum(m => m.GetPrice());
        }
        public decimal GetAveragePrice()
        {
            if (_monitors.Count == 0)
                return 0m;
            return Math.Round(GetTotalPrice() / _monitors.Count, 2);
        }
        public Monitor GetMostExpensive()
        {
            return _monitors.OrderByDescending(m => m.GetPrice()).FirstOrDefault();
        }
        public string GetStatistics()
        {
            if (_monitors.Count == 0)
                return "Список моніторів порожній.";
            Monitor expensive = GetMostExpensive();
            int hdrCount = _monitors.Count(m => m.GetHasHdr());
            int highRateCount = _monitors.Count(m => m.IsHighRefreshRate());
            return $"Кількість моніторів: {_monitors.Count}\n" +
                   $"Загальна вартість: {GetTotalPrice():N2} грн\n" +
                   $"Середня ціна: {GetAveragePrice():N2} грн\n" +
                   $"Найдорожчий: {expensive.GetModel()} ({expensive.GetPrice():N2} грн)\n" +
                   $"З підтримкою HDR: {hdrCount}\n" +
                   $"З частотою ≥ 144 Гц: {highRateCount}";
        }
        // Сортування
        public void SortByPrice()
        {
            _monitors = _monitors.OrderBy(m => m.GetPrice()).ToList();
        }
        public void SortByRefreshRate()
        {
            _monitors = _monitors.OrderByDescending(m => m.GetRefreshRate()).ToList();
        }
        public void SortByModel()
        {
            _monitors = _monitors.OrderBy(m => m.GetModel()).ToList();
        }
    }
}