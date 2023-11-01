using Medicoz.Models;

namespace Medicoz.ViewModels
{
    public class HomeVM
    {
        public List<Slider> Sliders { get; set; }
        public List<Plan> Plans { get; set; }
        public List<Ad> Ads { get; set; }
        public List<Feature> Features { get; set; }
        public List<Testimonial> Testimonials { get; set; }
        public int Departmentcount { get; set; }
        public int Doctorcount { get; set; }
        public int ReservationCount { get; set; }
        public int MemberCount { get; set; }



    }
}
