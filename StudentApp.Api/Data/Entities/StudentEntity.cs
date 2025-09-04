using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentApp.Api.Data.Entities
{
    public class StudentEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, StringLength(50, MinimumLength = 1)]
        public string studentName { get; set; } = string.Empty;

        [Required, StringLength(50, MinimumLength = 1)]
        public string studentSurname { get; set; } = string.Empty;
        public string studentNo { get; set; } = string.Empty;
        public string studentClass { get; set; } = string.Empty;

    }
}
