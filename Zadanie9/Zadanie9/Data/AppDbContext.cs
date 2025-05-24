using Microsoft.EntityFrameworkCore;
using Zadanie9.Models;

namespace Zadanie9.Data;

public class AppDbContext : DbContext
{
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }
    
    public AppDbContext(DbContextOptions options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        var patient = new Patient()
        {
            IdPatient = 1,
            FirstName = "John",
            LastName = "Doe",
            BirthDate = new DateTime(2000, 5, 24)
        };
        var doctor = new Doctor()
        {
            IdDoctor = 1,
            FirstName = "Andrew",
            LastName = "Copventyr",
            Email = "johndoe@gmail.com",
        };
        var prescription = new Prescription()
        {
            IdPrescription = 1,
            Date = new DateTime(2000, 5, 24),
            DueDate = new DateTime(2025, 5, 24),
            IdPatient = 1,
            IdDoctor = 1
        };
        var medicament = new Medicament()
        {
            IdMedicament = 1,
            Name = "Trembolon",
            Description = "Mocne",
            Type = "Steryd"
        };
        var prescriptionmedicament = new PrescriptionMedicament()
        {
            IdMedicament = 1,
            IdPrescription = 1,
            Dose = 3,
            Details = "bierz to"
        };
       modelBuilder.Entity<Medicament>().HasData(medicament);
       modelBuilder.Entity<Doctor>().HasData(doctor);
       modelBuilder.Entity<Patient>().HasData(patient);
       modelBuilder.Entity<Prescription>().HasData(prescription);
       modelBuilder.Entity<PrescriptionMedicament>().HasData(prescriptionmedicament);
    }
}