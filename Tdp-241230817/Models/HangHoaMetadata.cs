using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Tdp_241230817.Models;


[ModelMetadataType(typeof(HangHoaMetadata))]
public partial class HangHoa { }

public class HangHoaMetadata
{
    [Display(Name = "Loại hàng")]
    public int MaLoai { get; set; }

    [Display(Name = "Tên hàng")]
    [Required(ErrorMessage = "Tên hàng không được để trống.")]
    [StringLength(50, ErrorMessage = "Tên hàng tối đa 50 ký tự.")]
    public string TenHang { get; set; } = null!;

    [Display(Name = "Giá")]
    [Required(ErrorMessage = "Giá không được để trống.")]
    [Range(100.0, 5000.0, ErrorMessage = "Giá phải nằm trong khoảng từ 100 đến 5000.")]
    public decimal? Gia { get; set; }

    [Display(Name = "Ảnh")]
    [StringLength(100, ErrorMessage = "Tên ảnh tối đa 100 ký tự.")]
    [RegularExpression(@"^[^\\/:*?""<>|]+\.(?i:jpg|png|gif|tiff)$",
        ErrorMessage = "Tên file ảnh phải có đuôi .jpg, .png, .gif hoặc .tiff.")]
    public string? Anh { get; set; }
}
