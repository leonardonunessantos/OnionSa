﻿namespace OnionSa.Application.ViewModels
{
    public class OrderViewModel
    {
        public OrderViewModel(int orderId, string clientName, string clientCep, string productName, decimal productValue, decimal totalCostFrete, int totalDaysFrete, DateTime createdAt)
        {
            OrderId = orderId;
            ClientName = clientName;
            ClientCep = clientCep;
            ProductName = productName;
            ProductValue = productValue;
            TotalCostFrete = totalCostFrete;
            TotalDaysFrete = totalDaysFrete;
            CreatedAt = createdAt;
        }

        public int OrderId { get; set; }
        public string ClientName { get; set; }
        public string ClientCep { get; set; }
        public string ProductName { get; set; }
        public decimal ProductValue { get; set; }
        public decimal TotalCostFrete { get; set; }
        public int TotalDaysFrete { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
