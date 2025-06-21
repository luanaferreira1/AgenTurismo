using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;

namespace AgenTurismo.Pages
{
    public class ViewNotesModel : PageModel
    {
        private readonly IWebHostEnvironment _environment;

        public ViewNotesModel(IWebHostEnvironment environment)
        {
            _environment = environment;
            NotesDirectory = Path.Combine(_environment.WebRootPath, "files");
            if (!Directory.Exists(NotesDirectory))
            {
                Directory.CreateDirectory(NotesDirectory);
            }
        }

        public string NotesDirectory { get; }

        [BindProperty]
        public string NoteContent { get; set; } = string.Empty;

        [BindProperty]
        public string FileName { get; set; } = string.Empty;

        public List<string> Files { get; set; } = new List<string>();

        public string CurrentFileContent { get; set; } = string.Empty;

        public void OnGet(string? file = null)
        {
            Files = Directory.GetFiles(NotesDirectory, "*.txt")
                            .Select(Path.GetFileName)
                            .ToList()!;

            if (!string.IsNullOrEmpty(file))
            {
                var filePath = Path.Combine(NotesDirectory, file);
                if (System.IO.File.Exists(filePath))
                {
                    CurrentFileContent = System.IO.File.ReadAllText(filePath);
                }
            }
        }

        public IActionResult OnPostCreate()
        {
            if (!string.IsNullOrWhiteSpace(FileName) && !string.IsNullOrWhiteSpace(NoteContent))
            {
                var fileName = Path.GetFileNameWithoutExtension(FileName) + ".txt";
                var filePath = Path.Combine(NotesDirectory, fileName);
                System.IO.File.WriteAllText(filePath, NoteContent, Encoding.UTF8);
            }
            return RedirectToPage();
        }

        public IActionResult OnPostView(string file)
        {
            return RedirectToPage(new { file });
        }
    }
}