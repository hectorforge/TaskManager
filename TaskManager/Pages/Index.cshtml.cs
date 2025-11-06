using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Models;

namespace TaskManager.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<TaskItem> PendingTasks { get; set; } = new();
        public List<TaskItem> InProgressTasks { get; set; } = new();
        public List<TaskItem> CompletedTasks { get; set; } = new();

        public async Task OnGetAsync()
        {
            var tasks = await _context.Tasks.OrderByDescending(t => t.CreatedAt).ToListAsync();

            PendingTasks = tasks.Where(t => t.Status == Models.TaskStatus.Pending).ToList();
            InProgressTasks = tasks.Where(t => t.Status == Models.TaskStatus.InProgress).ToList();
            CompletedTasks = tasks.Where(t => t.Status == Models.TaskStatus.Completed).ToList();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostChangeStatusAsync(int id, Models.TaskStatus newStatus)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task != null)
            {
                task.Status = newStatus;
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }
    }
}