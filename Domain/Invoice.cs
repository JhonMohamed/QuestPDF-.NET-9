
namespace Domain;

public class Invoice
{
    public string Title { get; set; }
    public string Content { get; set; }
}

public interface IPdfGenerator
{
    byte[] GenerateInvoice(string title, string content);
}
