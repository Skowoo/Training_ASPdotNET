using Egzamin2023.Models;

namespace Egzamin2023.Services
{
    public class NoteService
    {
        private readonly IDateProvider _dateTimeProvider;

        public NoteService(IDateProvider dateProvider)
        {
            _dateTimeProvider = dateProvider;
        }

        private static List<Note> Notes = new();

        // Avoid duplicates and wrong dealines!
        public void Add(Note input) 
        {
            // If Note is not valid (deadline too soon) - return (no action)
            if (input.Deadline < _dateTimeProvider.CurrentDate.AddHours(1)) 
                return;

            // If there is already Note with same ID - return (no action)
            if (Notes.Where(x => x.Title.Equals(input.Title)).Any()) 
                return;

            Notes.Add(input); // Else add Note to internal List
        }

        // Return only valid Notes (judge by Deadline) - wtf task -,-
        public List<Note> GetAll() 
            => Notes.Where(x => x.Deadline > _dateTimeProvider.CurrentDate).ToList();

        // Return nullable property - if item not found return null and handle this case in controller
        public Note? GetById(string id) => Notes.SingleOrDefault(x => x.Title.Equals(id)); 
    }
}
