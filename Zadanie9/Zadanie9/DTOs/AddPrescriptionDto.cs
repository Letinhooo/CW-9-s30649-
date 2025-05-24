namespace Zadanie9.DTOs;

public class AddPrescriptionDto
{
    public PatientDto Patient { get; set; }
    public ICollection<MedicamentPrescriptionDto> Medicaments { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
}