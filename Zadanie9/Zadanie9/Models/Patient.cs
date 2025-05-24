using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zadanie9.Models;
[Table("Patient")]
public class Patient
{
    [Key]
    public int IdPatient { get; set; }
    [MaxLength(100)] 
    public string FirstName { get; set; }= null!; 
    [MaxLength(100)] 
    public string LastName { get; set; } = null!;
    [Column(TypeName = "date")]
    public DateTime BirthDate { get; set; }
    public virtual ICollection<Prescription> Prescriptions { get; set; }
}