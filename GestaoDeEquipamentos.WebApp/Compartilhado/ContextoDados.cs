using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using GestaoDeEquipamentos.WebApp.ModuloChamado;
using GestaoDeEquipamentos.WebApp.ModuloEquipamento;
using GestaoDeEquipamentos.WebApp.ModuloFabricante;

namespace GestaoDeEquipamentos.WebApp.Compartilhado;

public class ContextoDados
{
    public List<Fabricante> Fabricantes { get; set; }
    public List<Equipamento> Equipamentos { get; set; }
    public List<Chamado> Chamados { get; set; }

    private string pastaArmazenamento = string.Empty;
    private string arquivoArmazenamento = "dados-gestao-equipamentos.json";

    public ContextoDados()
    {
        Fabricantes = new List<Fabricante>();
        Equipamentos = new List<Equipamento>();
        Chamados = new List<Chamado>();

    }

    public void VerificarSistemaOperacional()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            pastaArmazenamento = @"C:\temp";
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            pastaArmazenamento = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "temp");
        }
    }

    public ContextoDados(bool carregarDados) : this()
    {
        if (carregarDados)
            Carregar();
    }

    public void Salvar()
    {
        VerificarSistemaOperacional();
        string caminhoCompleto = Path.Combine(pastaArmazenamento, arquivoArmazenamento);

        JsonSerializerOptions jsonOptions = new JsonSerializerOptions();
        jsonOptions.WriteIndented = true;
        jsonOptions.ReferenceHandler = ReferenceHandler.Preserve;

        string json = JsonSerializer.Serialize(this, jsonOptions);

        if (!Directory.Exists(pastaArmazenamento))
            Directory.CreateDirectory(pastaArmazenamento);

        File.WriteAllText(caminhoCompleto, json);
    }

    public void Carregar()
    {
        VerificarSistemaOperacional();
        string caminhoCompleto = Path.Combine(pastaArmazenamento, arquivoArmazenamento);

        if (!File.Exists(caminhoCompleto)) return;

        string json = File.ReadAllText(caminhoCompleto);

        if (string.IsNullOrWhiteSpace(json)) return;

        JsonSerializerOptions jsonOptions = new JsonSerializerOptions();
        jsonOptions.ReferenceHandler = ReferenceHandler.Preserve;

        ContextoDados contextoArmazenado = JsonSerializer.Deserialize<ContextoDados>(json, jsonOptions)!;

        if (contextoArmazenado == null) return;

        Fabricantes = contextoArmazenado.Fabricantes;
        Equipamentos = contextoArmazenado.Equipamentos;
        Chamados = contextoArmazenado.Chamados;
    }
}
