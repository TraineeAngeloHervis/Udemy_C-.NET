using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
var configuration = app.Configuration;
RepositorioProduto.Init(configuration);

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

//EndPoints Refatorados no padrão REST
app.MapPost("/produtos", (Produto produto) =>
{
    RepositorioProduto.Add(produto);
    return Results.Created($"/produtos/{produto.codigo}", produto.codigo);
});

app.MapGet("/produtos/{codigo}", ([FromRoute] string codigo) =>
{
    var produto = RepositorioProduto.GetBy(codigo);
    if (produto != null)
        return Results.Ok(produto);
    return Results.NotFound();
});

app.MapPut("/produtos", (Produto produto) =>
{
    var produtoSalvo = RepositorioProduto.GetBy(produto.codigo);
    produtoSalvo.nome = produto.nome;
    return Results.Ok();
});

app.MapDelete("/produtos/{codigo}", ([FromRoute] string codigo) =>
{
    var produtoSalvo = RepositorioProduto.GetBy(codigo);
    RepositorioProduto.Remove(produtoSalvo);
    return Results.Ok();
});

if(app.Environment.IsStaging()){
    app.MapGet("/configuracoes/database", (IConfiguration configuration) => {
    return Results.Ok($"{configuration["database:connection"]}/{configuration["database:port"]}" );
});
}

app.Run();

public static class RepositorioProduto
{
    public static List<Produto> produtos { get; set; } = produtos = new List<Produto>();

    public static void Init(IConfiguration configuration)
    {
        var produtosInit = configuration.GetSection("Produtos").Get<List<Produto>>();
        produtos = produtosInit;
    }

    public static void Add(Produto produto)
    {
            produtos.Add(produto);
    }
    public static Produto GetBy(string codigo)
    {
        return produtos.FirstOrDefault(p => p.codigo == codigo);
    }

    public static void Remove(Produto produto)
    {
        produtos.Remove(produto);
    }
}

public class Produto
{
    public string codigo { get; set; }
    public string nome { get; set; }
}
