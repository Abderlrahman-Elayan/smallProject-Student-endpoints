using System.ComponentModel.DataAnnotations;

namespace TestRestAPI.Models.DTOs
{
    public class StudentUpdateDTO
    {
        [Required]
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }
    }
}
