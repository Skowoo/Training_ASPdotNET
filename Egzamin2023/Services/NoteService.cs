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

        public void Add(Note input) // Avoid duplicates and wrong dealines!
        {
            if (input.Deadline < _dateTimeProvider.CurrentDate.AddHours(1))
                return;

            if (Notes.Where(x => x.Title.Equals(input.Title)).Count() != 0)
                return;

            Notes.Add(input);
        }

        public List<Note> GetAll() => Notes;

        public Note? GetById(string id) => Notes.SingleOrDefault(x => x.Title.Equals(id)); // Return nullable property - if item not found return null and handle this case in controller
    }
}
