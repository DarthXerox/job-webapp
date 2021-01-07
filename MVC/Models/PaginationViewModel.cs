namespace MVC.Models
{
    public class PaginationViewModel
    {
        public int LeftBound { get; private set; }
        public int RightBound { get; private set; }
        public int Current { get; private set; }
        public int Last => (listCount + pageSize - 1) / pageSize;

        private readonly int listCount;
        private readonly int maxNumberOfPages;
        private readonly int pageSize;

        public PaginationViewModel(int currentPage, int listCount, int pageSize)
        {
            Current = currentPage;
            this.listCount = listCount;
            maxNumberOfPages = 5;
            this.pageSize = pageSize;
            CalculateBounds();
        }

        private void CalculateBounds()
        {
            if (listCount / pageSize > maxNumberOfPages)
            {
                LeftBound = Current - 2;
                RightBound = Current + 2;
            }

            LeftBound = 1;
            RightBound = (listCount + pageSize - 1) / pageSize;
        }
    }
}
