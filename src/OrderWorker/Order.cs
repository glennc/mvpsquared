using System;
using System.ComponentModel.DataAnnotations;

namespace OrderWorker
{
    public class Order
    {
        [Required]
        public string AccountId { get; set; }

        [Range(0.01, 1000000)]
        public decimal PricePerWidget { get; set; }

        [Range(1, 1000)]
        public int NumberOfWidgets { get; set; }
    }
}