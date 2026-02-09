using System.ComponentModel.DataAnnotations;

namespace TestRestAPI.Models.DTOs
{
    public class StudentCreateDTO
    {
        [MaxLength(50)]
        public string Name { get; set; }

        public int Age { get; set; }
    }
}
