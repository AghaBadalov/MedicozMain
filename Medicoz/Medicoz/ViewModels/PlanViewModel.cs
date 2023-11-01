using Medicoz.DAL;
using Medicoz.Enums;
using Medicoz.Models;

namespace Medicoz.ViewModels
{
    public class PlanViewModel
    {
        public List<Plan> Plans{ get; set; }
        public List<Period> periods{ get; set; }
    }
}
