using System;

namespace Laba1OOP.Class
{
    public class Monitor
    {
        // Закриті поля
        private string _model;
        private double _diagonal;
        private int _refreshRate;
        private decimal _price;
        private bool _hasHdr;

        // Конструктор за замовчуванням
        public Monitor()
        {
            _model = "Невідомо";
            _diagonal = 0.0;
            _refreshRate = 0;
            _price = 0m;
            _hasHdr = false;
        }
        public Monitor(string model, double diagonal, int refreshRate, decimal price, bool hasHdr)
        {
            SetModel(model);
            SetDiagonal(diagonal);
            SetRefreshRate(refreshRate);
            SetPrice(price);
            SetHasHdr(hasHdr);
        }

        // Гетери
        public string GetModel()
        {
            return _model;
        }

        public double GetDiagonal()
        {
            return _diagonal;
        }

        public int GetRefreshRate()
        {
            return _refreshRate;
        }

        public decimal GetPrice()
        {
            return _price;
        }

        public bool GetHasHdr()
        {
            return _hasHdr;
        }

        // Сетери з валідацією
        public void SetModel(string model)
        {
            if (string.IsNullOrWhiteSpace(model))
                throw new ArgumentException("Назва моделі не може бути порожньою.");
            _model = model.Trim();
        }

        public void SetDiagonal(double diagonal)
        {
            if (diagonal <= 0)
                throw new ArgumentException("Діагональ має бути більшою за 0.");
            _diagonal = diagonal;
        }

        public void SetRefreshRate(int refreshRate)
        {
            if (refreshRate <= 0)
                throw new ArgumentException("Частота оновлення має бути більшою за 0.");
            _refreshRate = refreshRate;
        }

        public void SetPrice(decimal price)
        {
            if (price <= 0)
                throw new ArgumentException("Ціна має бути більшою за 0.");
            _price = price;
        }

        public void SetHasHdr(bool hasHdr)
        {
            _hasHdr = hasHdr;
        }

        // Розрахункові методи
        public double GetGamingIndex()
        {
            return Math.Round((_refreshRate / 60.0) * (_hasHdr ? 1.25 : 1.0), 2);
        }

        public decimal GetPricePerInch()
        {
            if (_diagonal <= 0)
                return 0m;
            return Math.Round(_price / (decimal)_diagonal, 2);
        }

        public bool IsHighRefreshRate()
        {
            return _refreshRate == 144;
        }
        public string GetDescription()
        {
            return $"Модель: {_model}; діагональ: {_diagonal:F1}\"; частота: {_refreshRate} Гц; " +
                   $"ціна: {_price:N2} грн; HDR: {(_hasHdr ? "так" : "ні")}; " +
                   $"ігровий індекс: {GetGamingIndex():F2}; ціна за дюйм: {GetPricePerInch():N2} грн";
        }
        public override string ToString()
        {
            return $"{_model,-26} | {_diagonal,5:F1}\" | {_refreshRate,3} Гц | {_price,10:N2} грн | HDR: {(_hasHdr ? "так" : "ні")}";
        }
    }
}