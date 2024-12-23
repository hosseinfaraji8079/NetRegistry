using System.ComponentModel.DataAnnotations;

namespace Registry.API.ViewModel;

public class AddRegistryDto
{
    /// <summary>
    /// The first IMEI number which must be 16 characters long.
    /// </summary>
    /// <example>"1234567890123456"</example>
    [Required(ErrorMessage = "لطفاً شماره IMEI اول را وارد کنید.")]
    [MinLength(16, ErrorMessage = "شماره IMEI اول باید حداقل 16 کاراکتر باشد.")]
    [MaxLength(16, ErrorMessage = "شماره IMEI اول نمی‌تواند بیشتر از 16 کاراکتر باشد.")]
    public string? ImeI_1 { get; set; }

    /// <summary>
    /// The second IMEI number which must be 16 characters long.
    /// </summary>
    /// <example>"9876543210987654"</example>
    [MinLength(16, ErrorMessage = "شماره IMEI دوم باید حداقل 16 کاراکتر باشد.")]
    [MaxLength(16, ErrorMessage = "شماره IMEI دوم نمی‌تواند بیشتر از 16 کاراکتر باشد.")]
    public string? ImeI_2 { get; set; }

    /// <summary>
    /// A field to confirm that the user accepts the rules.
    /// </summary>
    [Required(ErrorMessage = "لطفاً قوانین را تایید کنید.")]
    public bool AcceptTheRules { get; set; } = true;

    /// <summary>
    /// A summary or description that can be up to 500 characters long.
    /// </summary>
    [MaxLength(500, ErrorMessage = "خلاصه نباید بیشتر از 500 کاراکتر باشد.")]
    public string? Summery { get; set; } = "";

    /// <summary>
    /// The target audience or recipient of the information, up to 50 characters.
    /// </summary>
    [MaxLength(50, ErrorMessage = "این فیلد نمی‌تواند بیشتر از 50 کاراکتر باشد.")]
    public string? ForWho { get; set; }

    /// <summary>
    /// The phone number, which must be up to 11 characters long.
    /// </summary>
    [Required(ErrorMessage = "لطفاً شماره تلفن را وارد کنید.")]
    [MaxLength(11, ErrorMessage = "شماره تلفن باید حداکثر 11 کاراکتر باشد.")]
    public string Phone { get; set; }
}