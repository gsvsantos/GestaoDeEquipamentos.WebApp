using GestaoDeEquipamentos.WebApp.Extensions;
using GestaoDeEquipamentos.WebApp.ModuloEquipamento;
using GestaoDeEquipamentos.WebApp.ModuloFabricante;

namespace GestaoDeEquipamentos.WebApp.Models;

public abstract class FormularioEquipamentoViewModel
{
    public Guid Id { get; set; }
    public string? Nome { get; set; }
    public string? NomeFabricante { get; set; }
    public Guid FabricanteId { get; set; }
    public decimal PrecoAquisicao { get; set; }
    public DateTime DataFabricacao { get; set; }
    public List<SelecionarFabricanteViewModel> FabricantesDisponiveis { get; set; } = [];
}

public class SelecionarFabricanteViewModel
{
    public Guid Id { get; set; }
    public string? Nome { get; set; }

    public SelecionarFabricanteViewModel(Guid id, string nome)
    {
        Id = id;
        Nome = nome;
    }
}

public class CadastrarEquipamentoViewModel : FormularioEquipamentoViewModel
{
    public CadastrarEquipamentoViewModel() { }
    public CadastrarEquipamentoViewModel(List<Fabricante> fabricantes) : this()
    {
        foreach (Fabricante f in fabricantes)
        {
            SelecionarFabricanteViewModel selecionarVM = new(f.Id, f.Nome);

            FabricantesDisponiveis.Add(selecionarVM);
        }
    }
}

public class VisualizarEquipamentosViewModel
{
    public List<DataEquipamentoViewModel> Registros { get; set; } = [];

    public VisualizarEquipamentosViewModel(List<Equipamento> equipamentos)
    {
        foreach (Equipamento e in equipamentos)
        {
            DataEquipamentoViewModel dataVM = e.ParaDetalhesVM();

            Registros.Add(dataVM);
        }
    }
}

public class EditarEquipamentoViewModel : FormularioEquipamentoViewModel
{
    public EditarEquipamentoViewModel() { }
    public EditarEquipamentoViewModel(Guid id, string nome, decimal precoAquisicao, DateTime dataFabricacao, Guid fabricanteId, List<Fabricante> fabricantes)
    {
        Id = id;
        Nome = nome;
        PrecoAquisicao = precoAquisicao;
        DataFabricacao = dataFabricacao;
        FabricanteId = fabricanteId;

        foreach (Fabricante f in fabricantes)
        {
            SelecionarFabricanteViewModel selecionarVM = new(f.Id, f.Nome);

            FabricantesDisponiveis.Add(selecionarVM);
        }
    }
}

public class ExcluirEquipamentoViewModel : FormularioEquipamentoViewModel
{
    public ExcluirEquipamentoViewModel() { }
    public ExcluirEquipamentoViewModel(Guid id, string nome)
    {
        Id = id;
        Nome = nome;
    }
}

public class DataEquipamentoViewModel : FormularioEquipamentoViewModel
{
    public DataEquipamentoViewModel(Guid id, string nome, string nomeFabricante, decimal precoAquisicao, DateTime dataFabricacao)
    {
        Id = id;
        Nome = nome;
        NomeFabricante = nomeFabricante;
        PrecoAquisicao = precoAquisicao;
        DataFabricacao = dataFabricacao;
    }
    public override string ToString()
    {
        return $"ID: {Id}, Nome: {Nome}, Fabricante: {NomeFabricante}, Preço de Aquisição: R${PrecoAquisicao:F2}, Data de Fabricação: {DataFabricacao:d}";
    }
}
