using Medicoz.Enums;
using Medicoz.Models;
using System.ComponentModel.DataAnnotations;

namespace Medicoz.ViewModels
{
    public class AppointmentVm
    {
        
        public int DoctorId { get; set; }
        [StringLength(maximumLength: 30)]

        public string FullName { get; set; }
        [StringLength(maximumLength: 30)]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        [DataType(DataType.EmailAddress)]
        [StringLength(maximumLength: 50)]
        public string Email { get; set; }
        [StringLength(maximumLength: 250)]
        public string Message { get; set; }
        public Status Status { get; set; }
        public DateTime AppointmentTime { get; set; }=DateTime.Now;
        //[DataType(DataType.Time)]
        public string StartTime { get; set; }
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        public Doctor? Doctor { get; set; }
        public string? AppointmentCheck { get; set; }
    }
}
