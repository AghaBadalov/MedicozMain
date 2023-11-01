using Medicoz.DAL;
using Medicoz.Helpers;
using Medicoz.Models;
using Medicoz.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Medicoz.Areas.manage.Controllers
{
    [Area("manage")]
    [Authorize(Roles = ("SuperAdmin,Admin"))]

    public class AppointmentController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IEmailService _emailService;

        public AppointmentController(AppDbContext context,IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }
        public IActionResult Index(int page=1)
        {
            var query = _context.Appointments.Include(x=>x.Doctor).Where(x => x.Status == Enums.Status.Pending).AsQueryable();
            PaginatedList<Appointment> appointments = PaginatedList<Appointment>.Create(query, 5, page);
            return View(appointments);
        }
        public IActionResult Accepted(int page = 1)
        {
            var query = _context.Appointments.Include(x => x.Doctor).Where(x=>x.Status==Enums.Status.Accepted).AsQueryable();
            PaginatedList<Appointment> appointments = PaginatedList<Appointment>.Create(query, 5, page);
            return View(appointments);
        }
        public IActionResult Reject(int id)
        {
            Appointment appointment = _context.Appointments.FirstOrDefault(x => x.Id == id);
            if (appointment == null) return NotFound();
            appointment.Status = Enums.Status.Declined;
            _emailService.Send(appointment.Email, "Your Appointment cancelled", "Sorry ");

            _context.SaveChanges();
            return RedirectToAction("index");

         }
        public IActionResult Accept(int id)
        {
            Appointment appointment = _context.Appointments.FirstOrDefault(x => x.Id == id);
            if (appointment == null) return NotFound();
            appointment.Status = Enums.Status.Accepted;
            _emailService.Send(appointment.Email, "Your Appointment accepted", "Thank you ");
            _context.SaveChanges();
            return RedirectToAction("index");

        }
        public IActionResult Detail(int id)
        {
            Appointment appointment = _context.Appointments.Include(x=>x.Doctor).FirstOrDefault(x => x.Id == id);
            if (appointment == null) return NotFound();
            
            
            return View(appointment);

        }

    }
}
