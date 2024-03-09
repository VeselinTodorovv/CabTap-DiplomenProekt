namespace CabTap.Shared.Pagination;

public class PaginationViewModel
{
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public string ActionName { get; set; } = null!;
}