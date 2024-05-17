var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Aprendendo a usar API");
app.MapPost(
    "/user",
    () =>
        new
        {
            Name = "Ângelo Hervis",
            Age = 18,
            CEP = 83810000,
            Email = "Angelo.hervis@gmail.com"
        }
);
app.MapGet(
    "/AddHeader",
    (HttpResponse response) =>
    {
        response.Headers.Add("Testando...", "API funciona mesmo.");
        return "Funcionou";
    });

app.MapPost("/SalvarProduto", (Produto produto) => {
    return produto.ID + " - " + produto.Nome;
});

app.Run();

public class Produto
{
    public string ID { get; set; }
    public string Nome { get; set; }
}
