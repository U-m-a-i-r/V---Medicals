using System;
using System.Collections.Generic;

using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using V___Medicals.Data;
using V___Medicals.Models;
using V___Medicals.ValidationModels;

namespace V___Medicals.Pages.Invoices
{
    public class CreateModel : PageModel
    {
        private readonly V___Medicals.Data.ApplicationDbContext _context;

        public CreateModel(V___Medicals.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        

        public IActionResult OnGet()
        {
            ViewData["PatientId"] = new SelectList(_context.Patients.Where(p => p.IsDeleted == false), "PatientId", "FullName");
            return Page();
        }
        [BindProperty]
        public int patientId { get; set; } = default!;
        public async Task<IActionResult> OnGetAppointment(int id)
        {
            patientId = id;
            ViewData["AppointmentId"] = new SelectList(_context.Appointments.Where(Apt=>Apt.Patient.PatientId==id).Include(Apt=>Apt.Doctor).Include(Apt => Apt.Patient), "AppointmentId", "Name");
            var appointments = await _context.Appointments.Where(Apt => Apt.Patient.PatientId == id).Include(Apt => Apt.Doctor).Include(Apt => Apt.Patient).ToListAsync();
            return new JsonResult(appointments);
        }
        [BindProperty]
        public int appointmentId { get; set; } = default!;
        public async Task<IActionResult> OnGetDetails(int id)
        {
            List<Dictionary<string, dynamic>> response = new List<Dictionary<string, dynamic>>();
            appointmentId = id;

            var invoice = _context.Invoices.Where(invoice => invoice.AppointmentId == id).FirstOrDefault();
            if (invoice != null)
            {
                response.Add(new Dictionary<string, dynamic>() { { "error", "Invoice is already created for this appointment!" } });
                return new JsonResult(response);
            }
            var appointment = _context.Appointments.Where(apt => apt.AppointmentId == id).Include(apt => apt.Doctor).FirstOrDefault();
            double doctor_charges = 0;
            if (appointment!.AppointmentType == ClinicTypes.Online)
            {
                doctor_charges = double.Parse(appointment!.Doctor.VideoConsultancyCharges!);

            }
            if(appointment.AppointmentType == ClinicTypes.Face_To_Face){
                doctor_charges = double.Parse(appointment!.Doctor.PhysicalConsultancyCharges!);
            }
            double total_amount = doctor_charges + (doctor_charges / 100 * 20);

            response.Add(new Dictionary<string, dynamic>() {
                { "doctorName", appointment!.Doctor.FullName },
                { "doctorCharges", doctor_charges },
                 { "totalCharges", total_amount },
                { "appointmentStatus", appointment.Status.ToString()},
                { "appointmentType", appointment.AppointmentType.ToString()!},
                { "speciality", appointment.SpecialityName!},
            });
            return new JsonResult(response);
        }
        [BindProperty]
        public InvoiceViewModel InputModel { get; set; }
        Invoice invoice;
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }
            ClaimsPrincipal _user = HttpContext?.User!;
            var userName = _user.Identity.Name;
            var latestInvoiceNumber = _context.Invoices
        .OrderByDescending(p => p.InvoiceNumber)
        .FirstOrDefault()?.InvoiceNumber;
            if (string.IsNullOrEmpty(latestInvoiceNumber))
            {
                latestInvoiceNumber = "Invoice-1";
            }
            var latestMRNumberWithoutPrefix = latestInvoiceNumber.Substring(8);
            var newMRNumber = int.Parse(latestMRNumberWithoutPrefix) + 1;
            var newMRNumberString = "Invoice-" + newMRNumber.ToString();
            var appointment = _context.Appointments.Where(apt => apt.AppointmentId == InputModel.AppointmentId).Include(p=>p.Patient).Include(p => p.Doctor).FirstOrDefault();
            if (appointment != null)
            {
                 invoice = new Invoice();
                invoice.InvoiceNumber = newMRNumberString;
                invoice.Appointment = appointment;
                invoice.AppointmentId = InputModel.AppointmentId;
                invoice.CreatedBy = userName;
                invoice.CreatedOn = DateTime.UtcNow;
                invoice.InvoiceDate = InputModel.InvoiceDate;
                invoice.isPaidToDoctor = false;
                invoice.PaymentDate = InputModel.PaymentDate;
                
                invoice.PaidAmount = InputModel.PaidAmount;
               
                invoice.Status = InputModel.Status;
                invoice.TotalAmount = InputModel.TotalAmount;
                
                _context.Invoices.Add(invoice);
                await _context.SaveChangesAsync();

               return CreateDocument(invoice);
               // return RedirectToPage("./Index");
            }
            else
            {
                return Page();
            }
            
            
        }
        public IActionResult CreateDocument(Invoice invoice)
        {
            PdfDocument doc = new PdfDocument();
            PdfPage page = doc.Pages.Add();
            PdfGrid pdfGrid = new PdfGrid();
            List<object> data = new List<object>();
            Object row1 = new { ID = "Invoice No", Name = invoice.InvoiceNumber };
            Object row2 = new { ID = "Patient Name", Name = invoice.Appointment.Patient.FullName };
            Object row3 = new { ID = "Appointment Date", Name = invoice.Appointment.ClinicDate };
            Object row4 = new { ID = "Doctor Name", Name = invoice.Appointment.Doctor.FullName };
            Object row5 = new { ID = "Total Amount", Name = invoice.TotalAmount.ToString() };
            Object row6 = new { ID = "Invoice Date", Name = invoice.InvoiceDate };
            Object row7 = new { ID = "Status", Name = invoice.Status.ToString() };
            data.Add(row1);
            data.Add(row2);
            data.Add(row3);
            data.Add(row4);
            data.Add(row5);
            data.Add(row6);
            data.Add(row7);
            //Add list to IEnumerable.
            IEnumerable<object> dataTable = data;
            //Assign data source.
            pdfGrid.DataSource = dataTable;
            //Draw grid to the page of PDF document.
            pdfGrid.Draw(page, new Syncfusion.Drawing.PointF(10, 10));
            //Write the PDF document to stream.
            MemoryStream stream = new MemoryStream();
            doc.Save(stream);
            //If the position is not set to '0' then the PDF will be empty.
            stream.Position = 0;
            //Close the document.
            doc.Close(true);
            //Defining the ContentType for pdf file.
            string contentType = "application/pdf";
            //Define the file name.
            string fileName = invoice.InvoiceNumber+".pdf";
            //Creates a FileContentResult object by using the file contents, content type, and file name.
            return File(stream, contentType, fileName);
        }
    }
}
