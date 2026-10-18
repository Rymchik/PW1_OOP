using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace Laba1OOP.Class
{
    public class MonitorManager
    {
        // Приватна колекція моніторів
        private List<Monitor> _monitors = new List<Monitor>();

        // Список, який зараз показано у формі (потрібен для видалення вибраного рядка)
        private List<Monitor> _currentView = new List<Monitor>();

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

            foreach (DataGridViewColumn column in grid.Columns)
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

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

        // Створення монітора з даних форми. Некоректні дані викликають ArgumentException із сетерів.
        // Повертає оновлену таблицю для показу у формі
        public DataTable AddMonitor(string model, double diagonal, int refreshRate,
                                    decimal price, bool hasHdr)
        {
            Add(new Monitor(model, diagonal, refreshRate, price, hasHdr));
            return GetAllTable();
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

        // Назва монітора з рядка таблиці (null, якщо такого рядка немає)
        public string GetModelAt(int viewIndex)
        {
            if (viewIndex < 0 || viewIndex >= _currentView.Count)
                return null;
            return _currentView[viewIndex].GetModel();
        }

        // Видалення монітора, який показано в рядку таблиці
        public bool DeleteFromView(int viewIndex)
        {
            if (viewIndex < 0 || viewIndex >= _currentView.Count)
                return false;
            return _monitors.Remove(_currentView[viewIndex]);
        }

        public int GetCount()
        {
            return _monitors.Count;
        }

        public List<Monitor> GetAll()
        {
            return new List<Monitor>(_monitors);
        }

        // Розрахункові методи
        public double GetGamingIndex(Monitor monitor)
        {
            return Math.Round((monitor.GetRefreshRate() / 60.0) * (monitor.GetHasHdr() ? 1.25 : 1.0), 2);
        }

        public decimal GetPricePerInch(Monitor monitor)
        {
            if (monitor.GetDiagonal() <= 0)
                return 0m;
            return Math.Round(monitor.GetPrice() / (decimal)monitor.GetDiagonal(), 2);
        }

        public bool IsHighRefreshRate(Monitor monitor)
        {
            return monitor.GetRefreshRate() == 144;
        }

        // Текстовий опис монітора
        public string GetDescription(Monitor monitor)
        {
            return $"Модель: {monitor.GetModel()}; діагональ: {monitor.GetDiagonal():F1}\"; " +
                   $"частота: {monitor.GetRefreshRate()} Гц; ціна: {monitor.GetPrice():N2} грн; " +
                   $"HDR: {(monitor.GetHasHdr() ? "так" : "ні")}; " +
                   $"ігровий індекс: {GetGamingIndex(monitor):F2}; " +
                   $"ціна за дюйм: {GetPricePerInch(monitor):N2} грн";
        }

        // Форматований вивід монітора
        public string GetFormattedLine(Monitor monitor)
        {
            return $"{monitor.GetModel(),-26} | {monitor.GetDiagonal(),5:F1}\" | " +
                   $"{monitor.GetRefreshRate(),3} Гц | {monitor.GetPrice(),10:N2} грн | " +
                   $"HDR: {(monitor.GetHasHdr() ? "так" : "ні")}";
        }

        public List<string> GetAllDescriptions()
        {
            List<string> result = new List<string>();

            foreach (Monitor m in _monitors)
            {
                result.Add(GetDescription(m));
            }

            return result;
        }

        public List<Monitor> FindByRefreshRate(int rate)
        {
            List<Monitor> result = new List<Monitor>();

            foreach (Monitor m in _monitors)
            {
                if (m.GetRefreshRate() == rate)
                {
                    result.Add(m);
                }
            }

            return result;
        }

        public Monitor FindCheapestHdr()
        {
            Monitor cheapest = null;

            foreach (Monitor m in _monitors)
            {
                if (m.GetHasHdr())
                {
                    if (cheapest == null || m.GetPrice() < cheapest.GetPrice())
                    {
                        cheapest = m;
                    }
                }
            }

            return cheapest;
        }

        public decimal GetTotalPrice()
        {
            decimal total = 0m;

            foreach (Monitor m in _monitors)
            {
                total += m.GetPrice();
            }

            return total;
        }

        public decimal GetAveragePrice()
        {
            if (_monitors.Count == 0)
                return 0m;
            return Math.Round(GetTotalPrice() / _monitors.Count, 2);
        }

        public Monitor GetMostExpensive()
        {
            Monitor mostExpensive = null;

            foreach (Monitor m in _monitors)
            {
                if (mostExpensive == null || m.GetPrice() > mostExpensive.GetPrice())
                {
                    mostExpensive = m;
                }
            }

            return mostExpensive;
        }

        public string GetStatistics()
        {
            if (_monitors.Count == 0)
                return "Список моніторів порожній.";

            Monitor expensive = GetMostExpensive();
            int hdrCount = 0;
            int highRateCount = 0;

            foreach (Monitor m in _monitors)
            {
                if (m.GetHasHdr())
                {
                    hdrCount++;
                }
                if (IsHighRefreshRate(m))
                {
                    highRateCount++;
                }
            }

            return $"Кількість моніторів: {_monitors.Count}\n" +
                   $"Загальна вартість: {GetTotalPrice():N2} грн\n" +
                   $"Середня ціна: {GetAveragePrice():N2} грн\n" +
                   $"Найдорожчий: {expensive.GetModel()} ({expensive.GetPrice():N2} грн)\n" +
                   $"З підтримкою HDR: {hdrCount}\n" +
                   $"З частотою 144 Гц: {highRateCount}";
        }

        public void SortByPrice()
        {
            for (int i = 0; i < _monitors.Count - 1; i++)
            {
                for (int j = 0; j < _monitors.Count - 1 - i; j++)
                {
                    if (_monitors[j].GetPrice() > _monitors[j + 1].GetPrice())
                    {
                        Monitor temp = _monitors[j];
                        _monitors[j] = _monitors[j + 1];
                        _monitors[j + 1] = temp;
                    }
                }
            }
        }

        public void SortByRefreshRate()
        {
            for (int i = 0; i < _monitors.Count - 1; i++)
            {
                for (int j = 0; j < _monitors.Count - 1 - i; j++)
                {
                    if (_monitors[j].GetRefreshRate() < _monitors[j + 1].GetRefreshRate())
                    {
                        Monitor temp = _monitors[j];
                        _monitors[j] = _monitors[j + 1];
                        _monitors[j + 1] = temp;
                    }
                }
            }
        }

        public void SortByModel()
        {
            for (int i = 0; i < _monitors.Count - 1; i++)
            {
                for (int j = 0; j < _monitors.Count - 1 - i; j++)
                {
                    if (string.Compare(_monitors[j].GetModel(), _monitors[j + 1].GetModel()) > 0)
                    {
                        Monitor temp = _monitors[j];
                        _monitors[j] = _monitors[j + 1];
                        _monitors[j + 1] = temp;
                    }
                }
            }
        }

        public DataTable GetAllTable()
        {
            return BuildTable(GetAll());
        }

        // Сортування за ціною і повернення оновленої таблиці
        public DataTable GetSortedByPriceTable()
        {
            SortByPrice();
            return GetAllTable();
        }

        public DataTable GetByRefreshRateTable(int rate)
        {
            return BuildTable(FindByRefreshRate(rate));
        }

        // Таблиця з найдешевшим HDR-монітором (порожня, якщо таких моніторів немає)
        public DataTable GetCheapestHdrTable()
        {
            List<Monitor> result = new List<Monitor>();
            Monitor cheapest = FindCheapestHdr();

            if (cheapest != null)
                result.Add(cheapest);

            return BuildTable(result);
        }

        // Формування таблиці з даних моніторів (дані читаються через гетери)
        private DataTable BuildTable(List<Monitor> list)
        {
            _currentView = list;

            DataTable table = new DataTable();
            table.Columns.Add("Model", typeof(string));
            table.Columns.Add("Diagonal", typeof(double));
            table.Columns.Add("RefreshRate", typeof(int));
            table.Columns.Add("Price", typeof(decimal));
            table.Columns.Add("HasHdr", typeof(bool));
            table.Columns.Add("GamingIndex", typeof(double));
            table.Columns.Add("PricePerInch", typeof(decimal));

            foreach (Monitor m in list)
            {
                table.Rows.Add(
                    m.GetModel(),
                    m.GetDiagonal(),
                    m.GetRefreshRate(),
                    m.GetPrice(),
                    m.GetHasHdr(),
                    GetGamingIndex(m),
                    GetPricePerInch(m));
            }

            return table;
        }
    }
}