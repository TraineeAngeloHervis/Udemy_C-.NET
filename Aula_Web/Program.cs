using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Aprendendo a usar API");
app.MapPost(
    "/usuario",
    () =>
        new
        {
            nome = "Ângelo Hervis",
            Idade = 18,
            CEP = 83810000,
            Email = "Angelo.hervis@gmail.com"
        }
);
app.MapGet(
    "/addheader",
    (HttpResponse response) =>
    {
        response.Headers.Add("Testando...", "API funciona mesmo.");
        return "Funcionou";
    });

app.MapPost("/salvarprodutorepositorio", (Produto produto) =>
{
    RepositorioProduto.Add(produto);
});

app.MapPost("/salvarproduto", (Produto produto) =>
{
    return produto.codigo + " - " + produto.nome;
});

app.MapGet("/getproduto", ([FromQuery] string dataInicio, [FromQuery] string dataFim) =>
{
    return dataInicio + " - " + dataFim;
});

app.MapGet("/getproduto/{codigo}", ([FromRoute] string codigo) =>
{
    return codigo;
});

app.MapGet("/getprodutodoheader", (HttpRequest request) =>
{
    return request.Headers["codigo-produto"].ToString();
});

app.MapGet("/getprodutocodigo/{codigo}", ([FromRoute] string codigo) => {
    var produto = RepositorioProduto.GetBy(codigo);
    return produto;
});

app.Run();

public static class RepositorioProduto
{
    public static List<Produto> produtos { get; set; }

    public static void Add(Produto produto)
    {
        if (produtos == null)
        {
            produtos = new List<Produto>();

            produtos.Add(produto);
        }
    }
    public static Produto GetBy(string codigo) {
        return produtos.FirstOrDefault(p => p.codigo == codigo);
    }
}

public class Produto
{
    public string codigo { get; set; }
    public string nome { get; set; }
}
