using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using V___Medicals.Constants;
using V___Medicals.Data;
using V___Medicals.Models;
using V___Medicals.ValidationModels;

namespace V___Medicals.APIs.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class InvoiceController : ControllerBase
    {
        private readonly ApplicationDbContext _appDbContext;

        public InvoiceController(
        ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] InvoiceViewModel model)
        {
            if (ModelState.IsValid)
            {
                var invoice = _appDbContext.Invoices.Where(invoice => invoice.AppointmentId == model.AppointmentId).FirstOrDefault();
                
                if (invoice != null)
                {
                    return BadRequest(new Response { Status = "Error", Message = "Invoice is already created for this appointment!" });
                }
                ClaimsPrincipal _user = HttpContext?.User!;
                var userName = _user.Identity.Name;
                var latestInvoiceNumber = _appDbContext.Invoices
            .OrderByDescending(p => p.InvoiceNumber)
            .FirstOrDefault()?.InvoiceNumber;
                if (string.IsNullOrEmpty(latestInvoiceNumber))
                {
                    latestInvoiceNumber = "Invoice-1";
                }
                var latestMRNumberWithoutPrefix = latestInvoiceNumber.Substring(8);
                var newMRNumber = int.Parse(latestMRNumberWithoutPrefix) + 1;
                var newMRNumberString = "Invoice-" + newMRNumber.ToString();
                var appointment = _appDbContext.Appointments.Where(apt => apt.AppointmentId == model.AppointmentId).Include(p => p.Patient).Include(p => p.Doctor).FirstOrDefault();
                if (appointment != null)
                {
                    invoice = new Invoice();
                    invoice.InvoiceNumber = newMRNumberString;
                    invoice.Appointment = appointment;
                    invoice.AppointmentId = model.AppointmentId;
                    invoice.CreatedBy = userName;
                    invoice.CreatedOn = DateTime.UtcNow;
                    invoice.InvoiceDate = model.InvoiceDate;
                    invoice.isPaidToDoctor = false;
                    invoice.PaymentDate = model.PaymentDate;
                    invoice.payproInvoiceURL = model.payproInvoiceURL;
                    invoice.PaidAmount = model.PaidAmount;

                    invoice.Status = model.Status;
                    invoice.TotalAmount = model.TotalAmount;

                   var createdInvoice= _appDbContext.Invoices.Add(invoice);
                    await _appDbContext.SaveChangesAsync();

                    return StatusCode(201, new { createdInvoice.Entity });
                    // return RedirectToPage("./Index");
                }
                else
                {
                    return ValidationProblem();
                }
            }
            else
            {
                return ValidationProblem();
            }


        }
    }
}
