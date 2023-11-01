using Medicoz.DAL;
using Medicoz.Models;
using Microsoft.EntityFrameworkCore;

namespace Medicoz.Services
{
    public class SettingsService
    {
        private readonly AppDbContext _context;

        public SettingsService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Settings>> GetSettings()
        {
            return await _context.Settings.ToListAsync();
        }
    }
}
