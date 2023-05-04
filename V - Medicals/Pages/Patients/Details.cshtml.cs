using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IronPdf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.EntityFrameworkCore;
using Syncfusion.Pdf;
using V___Medicals.Data;
using V___Medicals.Models;

namespace V___Medicals.Pages.Patients
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly V___Medicals.Data.ApplicationDbContext _context;

        public DetailsModel(V___Medicals.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult OnPostGeneratePdf([FromBody] dynamic data)
        {
            string htmlContent = data.htmlContent;
            var renderer = new HtmlToPdf();
            var pdfDoc = renderer.RenderHtmlAsPdf(htmlContent);
            var pdfBytes = pdfDoc.BinaryData;
            return File(pdfBytes, "application/pdf", "MyPdfDocument.pdf");
        }
        public Patient Patient { get; set; }
        public IList<Appointment> Appointments { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Patients == null)
            {
                return NotFound();
            }

            var patient = await _context.Patients.FirstOrDefaultAsync(m => m.PatientId == id);
           
            if (patient == null)
            {
                return NotFound();
            }
            else 
            {
                Appointments = await _context.Appointments.Where(apt => apt.PatientId == patient.PatientId).Include(apt=>apt.Doctor).Include(apt => apt.Documents).ToListAsync();
                Patient = patient;
            }
            return Page();
        }
    }
}
