using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SubjectService.Models
{
    [PrimaryKey(nameof(ClassId),nameof(SubjectId))]
    public class SubjectClass
    {
        [Required]
        public string ClassId { get; set; }
        [Required]
        public string SubjectId { get; set; }
    }
}
