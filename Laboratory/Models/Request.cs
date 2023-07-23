using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Laboratory.Models
{
    public class Request
    {
        public  int Id { get; set; }
        [StringLength(10, ErrorMessage = "Must be 10 integer")]
        public int NationalId { get; set; }
        [StringLength(10)]
        public int UniversityNo { get; set; }
        public string StudentStatus { get; set; }
        public string College { get; set; }
        [StringLength(20,MinimumLength =3, ErrorMessage ="Invalid Name must be 3 or greater")]
        public string FirstNameEn { get; set; }
        [StringLength(20,MinimumLength =3)] 
        public string FatherNameEn { get; set; }
        public string GrandfatherNameEn { get; set; }
        [StringLength(20, MinimumLength = 3)]
        public string FamilyNameEn { get; set; }
        public string FirstNameAr { get; set; }
        public string FatherNameAr { get; set; }
        public string GrandfatherNameAr { get; set; }
        public string FamilyNameAr { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public int PhoneNo { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Release Date")]
        public DateTime BirthDate { get; set; }
        public int? MidecalfileNo { get; set; }
        public DateTime DateSelected { get; set; }
        
    }
}
