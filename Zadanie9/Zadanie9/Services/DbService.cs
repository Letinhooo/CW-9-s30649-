using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zadanie9.Data;
using Zadanie9.DTOs;
using Zadanie9.Exceptions;
using Zadanie9.Models;

namespace Zadanie9.Services;

public interface IDbService
{
   public Task<PatientDto> GetPatientByIdAsync(int id);
   public Task<IActionResult> AddPrescriptionAsync(AddPrescriptionDto prescriptionData);
}
public class DbService(AppDbContext data) : IDbService
{
   public async Task<PatientDto> GetPatientByIdAsync(int id)
   {
      var wynik = await data.Patients
         .Where(p => p.IdPatient == id)
         .Select(p => new PatientDto
         {
            IdPatient = p.IdPatient,
            FirstName = p.FirstName,
            LastName = p.LastName,
            Birthdate = p.BirthDate,
            Prescriptions = p.Prescriptions
               .OrderBy(pr => pr.DueDate)
               .Select(pr => new PrescriptionDto
               {
                  IdPrescription = pr.IdPrescription,
                  Date = pr.Date,
                  DueDate = pr.DueDate,
                  Doctor = new DoctorDto()
                  {
                     IdDoctor = pr.Doctor.IdDoctor,
                     FirstName = pr.Doctor.FirstName,
                     LastName = pr.Doctor.LastName,
                     Email = pr.Doctor.Email,
                  },
                  Medicaments = pr.PrescriptionMedicaments
                     .Select(pm => new MedicamentDto
                     {
                        IdMedicament = pm.Medicament.IdMedicament,
                        Name = pm.Medicament.Name,
                        Dose = pm.Dose,
                        Details = pm.Details
                     }).ToList()
               }).ToList()
         }).FirstOrDefaultAsync();
      if (wynik is null)
      {
         throw new NotFoundException("Nie ma pacjenta z takim id");
      }
      return wynik;
   }

   public async Task<IActionResult> AddPrescriptionAsync(AddPrescriptionDto prescriptionData)
   {
      if (prescriptionData.DueDate < prescriptionData.Date)
      {
         throw new Exception("Daty sa zle");
      }

      if (prescriptionData.Medicaments.Count > 10)
      {
         throw new Exception("Nie moze byc wiecej niz 10 lekow dodanych");
      }

      var patient = await data.Patients
         .FirstOrDefaultAsync(p => p.IdPatient == prescriptionData.Patient.IdPatient);
      if (patient is null)
      {
         patient = new Patient
         {
            FirstName = prescriptionData.Patient.FirstName,
            LastName = prescriptionData.Patient.LastName,
            BirthDate = prescriptionData.Patient.Birthdate,
         };
         data.Patients.Add(patient);
         await data.SaveChangesAsync();
      }

      var medicamentIds = prescriptionData.Medicaments.Select(m => m.MedicamentId).ToList();
      var existingMedicament = await data.Medicaments
         .Where(m=>medicamentIds.Contains(m.IdMedicament))
         .Select(m => m.IdMedicament)
         .ToListAsync();
      var brakujace = medicamentIds.Except(existingMedicament).ToList();
      if (brakujace.Any())
      {
         throw new NotFoundException("Nie ma jednego z lekow");
      }
      
      var prescription = new Prescription
      {
         Date = prescriptionData.Date,
         DueDate = prescriptionData.DueDate,
         IdPatient = patient.IdPatient,
         PrescriptionMedicaments = prescriptionData.Medicaments.Select(m => new PrescriptionMedicament
         {
            IdMedicament = m.MedicamentId,
            Dose = m.Dose,
            Details = m.Description
         }).ToList()
      };
      data.Prescriptions.Add(prescription);
      await data.SaveChangesAsync();
      return new OkObjectResult("Dodano recepte");
   }
}