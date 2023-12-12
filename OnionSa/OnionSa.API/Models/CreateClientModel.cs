namespace OnionSa.API.Models;

// Futuramente criar padrão de entrada de dados, não a leitura direta do excel
public class CreateClientModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Document { get; set; }
    public string Cep { get; set; }
}
