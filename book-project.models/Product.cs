using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace book_project.models;

public class Product
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(30)]
    [DisplayName("Book Name")]
    public string? Title { get; set; }

    [Required]
    public string Author { get; set; }

    public string Description { get; set; }
    
    [Required] 
    public string ISBN { get; set; }

    [Display(Name = "List Price")]
    [Range(0,10000)]
    public double ListPrice { get; set; }
    
    [Display(Name = "Price for 1-50")]
    [Range(0,10000)]
    public double Price { get; set; }
    
    [Display(Name = "Price for 50+")]
    [Range(0,10000)]
    public double Price50 { get; set; }
    
    [Display(Name = "Price for 100+")]
    [Range(0,10000)]
    public double Price100 { get; set; }

    public int CategoryId { get; set; }
    
    [ForeignKey("CategoryId")]
    [ValidateNever]
    public Category Category { get; set; }

    [ValidateNever]
    [Display(Name = "Image Url")]
    public string? ImageUrl { get; set; }
}