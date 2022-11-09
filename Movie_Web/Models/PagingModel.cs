namespace Movie_Web.Models
{
    public class PagingModel
    {
        public int TotalCount { get; set; } = 0;
        public int TotalPages { get; set; } = 1;
        public int? NextPage { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int? PreviousPage { get; set; }
    }
}
