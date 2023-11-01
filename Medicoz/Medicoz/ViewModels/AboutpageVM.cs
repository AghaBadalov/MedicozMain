using Medicoz.Models;

namespace Medicoz.ViewModels
{
    public class AboutpageVM
    {
        public About About { get; set; }
        public List<Testimonial> Testimonials { get; set; }
        public List<Ad> Ads { get; set; }
        public List<Doctor> Doctors { get; set; }
        
        public int DepartmentCount { get; set; }
        public int DoctorCount { get; set; }
        public int UserCount { get; set; }
        public int AppointmentCount { get; set; }

    }
}
