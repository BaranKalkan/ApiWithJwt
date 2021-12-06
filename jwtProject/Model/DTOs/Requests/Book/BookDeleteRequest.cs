using System.ComponentModel.DataAnnotations;

namespace jwtProject.Model.DTOs.Requests
{
    public class BookDeleteRequest
    {
        [Required]
        public int Id { get; set; }
    }
}
