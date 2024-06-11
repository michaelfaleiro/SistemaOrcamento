namespace SistemaOrcamento.Core.Request;

public class PagedRequest : Request
{
    public int PageSize = Configuration.DefaultPageSize;
    public int PageNumber = Configuration.DefaultPageNumber;
}