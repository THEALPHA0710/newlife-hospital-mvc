using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewLifeHospital.Models
{
    public class PatientInfoDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RegistrationID { get; set; }

        [Required(ErrorMessage = "PatientName is required")]
        [StringLength(25, ErrorMessage = "PatientName cannot exceed 25 characters")]
        [Column(TypeName = "varchar(25)")]
        public string PatientName { get; set; }

        [Required(ErrorMessage = "Age is required")]
        [Range(1, 120, ErrorMessage = "Age must be between 1 and 120")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        [StringLength(10, ErrorMessage = "Gender cannot exceed 10 characters")]
        [Column(TypeName = "varchar(10)")]
        public string Gender { get; set; }

        [StringLength(4, ErrorMessage = "BloodGroup cannot exceed 4 characters")]
        [Column(TypeName = "varchar(4)")]
        public string BloodGroup { get; set; }

        [Required(ErrorMessage = "ContactNumber is required")]
        [StringLength(10, ErrorMessage = "ContactNumber cannot exceed 10 characters")]
        [RegularExpression(@"^[6-9]\d{9}$", ErrorMessage = "ContactNumber must be a valid 10 digit number")]
        [Column(TypeName = "varchar(10)")]
        public string ContactNumber { get; set; }

        [Required(ErrorMessage = "EmailID is required")]
        [StringLength(30, ErrorMessage = "EmailID cannot exceed 30 characters")]
        [EmailAddress(ErrorMessage = "EmailID must be a valid email address")]
        [Column(TypeName = "varchar(30)")]
        public string EmailID { get; set; }
    }
}
