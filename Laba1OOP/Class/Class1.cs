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

        // Конструктор з параметрами
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
    }
}