using System.ComponentModel.DataAnnotations.Schema;

namespace Laboratory.Models
{
    public class Request
    {
        public  int Id { get; set; }
        public int NationalId { get; set; }
        public int UniversityNo { get; set; }
        public string StudentStatus { get; set; }
        public string College { get; set; }
        public string FirstNameEn { get; set; }
        public string FatherNameEn { get; set; }
        public string GrandfatherNameEn { get; set; }
        public string FamilyNameEn { get; set; }
        public string FirstNameAr { get; set; }
        public string FatherNameAr { get; set; }
        public string GrandfatherNameAr { get; set; }
        public string FamilyNameAr { get; set; }
        public string Email { get; set; }
        public int PhoneNo { get; set; }
        public DateTime BirthDate { get; set; }
        public int MidecalfileNo { get; set; }

    }
}
