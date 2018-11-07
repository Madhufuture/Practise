namespace ProductCatalogGateway.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ProductsModel
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, ErrorMessage = "Name can't be longer than 50 characters")]
        public string ProductName { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Please enter a valid integer value")]
        public int ProductPrice { get; set; }
        public DateTime LastUpdated { get; set; }
        public byte[] Image { get; set; }
    }
}