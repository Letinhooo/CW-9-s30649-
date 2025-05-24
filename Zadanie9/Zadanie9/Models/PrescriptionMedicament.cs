using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Zadanie9.Models;
[Table("Prescription_Medicament")]
[PrimaryKey(nameof(IdPrescription),nameof(IdMedicament))]
public class PrescriptionMedicament
{
    public int IdMedicament { get; set; }
    public int IdPrescription { get; set; }
    public int?  Dose { get; set; }
    [MaxLength(100)]
    public string Details { get; set; }= null!;
    public virtual Prescription Prescription { get; set; } = null!;
    public virtual Medicament Medicament { get; set; } = null!;
}