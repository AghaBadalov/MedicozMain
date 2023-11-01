using Medicoz.DAL;
using Medicoz.Models;
using Medicoz.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.Generic;

namespace Medicoz.Controllers
{
    public class DoctorController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public DoctorController(AppDbContext context,UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Detail(int id)
        {
            Doctor doctor = _context.Doctors.Include(x => x.Department).FirstOrDefault(x => x.Id == id);
            if (doctor == null) return NotFound();
            AppUser member = null;
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                member = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            }
            AppointmentVm appointment = new AppointmentVm
            {
                Doctor = doctor,
                DoctorId = doctor.Id,
                Email=member?.Email,
                FullName=member?.FullName,
                Phone=member?.PhoneNumber,


            };
            DateTime start = appointment.Doctor.WorkStartTime;
            DateTime end = appointment.Doctor.WorkEndTime;
            List<int> list = new List<int>();
            List<int> list2 = new List<int>();
            for (int i = 0; i < 24; i++)
            {
                list2.Add(i);
            }
            if (end.Hour > start.Hour)
            {
                List<string> need = new List<string>();
                string itemStr = string.Empty;
                for (int i = start.Hour; i < end.Hour; i++)
                {
                    list.Add(i);
                }
                foreach (var item in list)
                {
                    itemStr = (item.ToString() + ":00") + " - " + ((item + 1).ToString() + ":00");
                    need.Add(itemStr);
                }
                ViewBag.List = need;

            }
            else
            {
                List<string> need = new List<string>();
                string itemStr = string.Empty;
                for (int i = start.Hour - 1; i >= end.Hour; i--)
                {
                    list2.Remove(i);
                }
                foreach (var item in list2)
                {
                    itemStr = (item.ToString() + ":00") + " - " + ((item + 1).ToString() + ":00");
                    need.Add(itemStr);


                }
                ViewBag.List = need;
            }


            

            return View(appointment);
        }
        [HttpPost]
        public IActionResult Detail(int id, AppointmentVm appointmentVm)
        {

            //List<string> strings=ViewBag.List;

            Doctor doctor = _context.Doctors.Include(x => x.Department).FirstOrDefault(x => x.Id == id);
            if (doctor == null) return NotFound();
            appointmentVm.Doctor = doctor;
            appointmentVm.DoctorId = doctor.Id;
            appointmentVm.AppointmentCheck = appointmentVm.StartTime;

            DateTime start = appointmentVm.Doctor.WorkStartTime;
            DateTime end = appointmentVm.Doctor.WorkEndTime;
            List<int> list = new List<int>();
            List<int> list2 = new List<int>();
            for (int i = 0; i < 24; i++)
            {
                list2.Add(i);
            }
            if (end.Hour > start.Hour)
            {
                List<string> need = new List<string>();
                string itemStr = string.Empty;
                for (int i = start.Hour; i < end.Hour; i++)
                {
                    list.Add(i);
                }
                foreach (var item in list)
                {
                    itemStr = (item.ToString() + ":00") + " - " + ((item + 1).ToString() + ":00");
                    need.Add(itemStr);
                }
                ViewBag.List = need;

            }
            else
            {
                List<string> need = new List<string>();
                string itemStr = string.Empty;
                for (int i = start.Hour - 1; i >= end.Hour; i--)
                {
                    list2.Remove(i);
                }
                foreach (var item in list2)
                {
                    itemStr = (item.ToString() + ":00") + " - " + ((item + 1).ToString() + ":00");
                    need.Add(itemStr);


                }
                ViewBag.List = need;
            }
            if (!ModelState.IsValid) return View(appointmentVm);

            var query = _context.Appointments.Where(x => x.DoctorId == doctor.Id).ToList();
            if (appointmentVm.StartDate < DateTime.Now || appointmentVm.StartDate>DateTime.Now.AddDays(7))
            {
                ModelState.AddModelError("StartDate", "Choice correct date");
                return View(appointmentVm);
            }
            foreach (var item in query)
            {
                if (appointmentVm.StartDate == item.StartDate)
                {
                    if (item.StartTime == appointmentVm.StartTime)
                    {
                        ModelState.AddModelError("StartTime", "Already reserved");
                        return View(appointmentVm);
                    }

                }
            }
            Appointment appointment = new Appointment
            {
                Doctor = appointmentVm.Doctor,
                DoctorId = appointmentVm.DoctorId,
                StartDate = appointmentVm.StartDate,
                StartTime = appointmentVm.StartTime,
                Status = appointmentVm.Status,
                Email = appointmentVm.Email,
                Message = appointmentVm.Message,
                FullName = appointmentVm.FullName,
                Phone = appointmentVm.Phone,
                AppointmentTime = appointmentVm.AppointmentTime,
            };
            _context.Appointments.Add(appointment);
            _context.SaveChanges();
            return RedirectToAction("index");

        }
    }
}
