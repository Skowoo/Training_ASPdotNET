namespace AplikacjaLaby.Classes
{
    public class PagingList<T>
    {
        public IEnumerable<T> Data { get; }
        public int TotalItems { get; }
        public int TotalPages { get; }
        public int Page { get; }
        public int Size { get; }
        public bool IsFirst { get; }
        public bool IsLast { get; }
        public bool IsNext { get; }
        public bool IsPrevious { get; }

        private PagingList(Func<int, int, IEnumerable<T>> dataGenerator, int totalItems, int page, int size)
        {
            TotalItems = totalItems;
            Page = page;
            Size = size;
            TotalPages = CalcTotalPages(Page, TotalItems, Size);
            IsFirst = Page <= 1;
            IsLast = Page >= TotalPages;
            IsNext = !IsLast;
            IsPrevious = !IsFirst;
            Data = dataGenerator.Invoke(Page, size);
        }

        public static PagingList<T> Create(Func<int, int, IEnumerable<T>> data, int totalItems, int number, int size) 
            => new(data, totalItems, ClipPage(number, totalItems, size), Math.Abs(size));

        private static int CalcTotalPages(int page, int totalItems, int size) 
            => totalItems / size + (totalItems % size > 0 ? 1 : 0);
        
        private static int ClipPage(int page, int totalItems, int size)
        {
            int totalPages = CalcTotalPages(page, totalItems, size);
            if (page > totalPages)
                return totalPages;

            if (page < 1)
                return 1;

            return page;
        }
    }
}
