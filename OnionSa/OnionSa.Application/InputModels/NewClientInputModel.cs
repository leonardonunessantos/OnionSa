namespace OnionSa.Application.InputModels;

public class NewClientInputModel
{
    public NewClientInputModel(string name, string document, string cep)
    {
        Name = name;
        Document = document;
        Cep = cep;
    }

    public string Name { get; set; }
    public string Document { get; set; }
    public string Cep { get; set; }
}
