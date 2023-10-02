using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SubjectService.Migrations;

namespace SubjectService.Models
{
    [PrimaryKey(nameof(UserId), nameof(ClassId))]
    public class StudentClass
    {
        public string UserId { get; set; }
        public string ClassId{ get; set; }
    }
}
