using Microsoft.AspNetCore.Mvc;

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

app.MapPost("/SalvarProduto", (Produto produto) =>
{
    return produto.ID + " - " + produto.Nome;
});

app.MapGet("/getproduto", ([FromQuery] string dataInicio, [FromQuery] string dataFim) =>
{
    return dataInicio + " - " + dataFim;
});

app.MapGet("/getproduto/{code}", ([FromRoute] string code) =>
{
    return code;
});

app.MapGet("/getprodutodoheader", (HttpRequest request) =>
{
    return request.Headers["codigo-produto"].ToString();
});

app.Run();

public static class RepositorioProduto
{
    public static List<Produto> Produtos { get; set; }

    public static void Add(Produto produto)
    {
        if (Produtos == null)
        {
            Produtos = new List<Produto>();

            Produtos.Add(Produto);
        }
    }
    public static Produto GetBy(string code) {
        return Produtos.First(p => p.code == code);
    }
}

public class Produto
{
    public string ID { get; set; }
    public string Nome { get; set; }
}
