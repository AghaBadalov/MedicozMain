using Medicoz.Models;

namespace Medicoz.Areas.manage.ViewModels
{
    public class DashboardVM
    {
        public List<AdminMessage> Messages { get; set; }
        public List<Appointment> Appointments { get; set; }
    }
}
