using GestaoDeEquipamentos.WebApp.Extensions;
using GestaoDeEquipamentos.WebApp.ModuloChamado;
using GestaoDeEquipamentos.WebApp.ModuloEquipamento;

namespace GestaoDeEquipamentos.WebApp.Models;

public class FormularioChamadoViewModel
{
    public Guid Id { get; set; }
    public string? Titulo { get; set; }
    public string? Descricao { get; set; }
    public DateTime DataAbertura { get; set; }
    public int TempoDecorrido { get; set; }
    public string? NomeEquipamento { get; set; }
    public Guid EquipamentoId { get; set; }
    public List<SelecionarEquipamentoViewModel> EquipamentosDisponiveis { get; set; } = [];
}

public class SelecionarEquipamentoViewModel
{
    public Guid Id { get; set; }
    public string? Nome { get; set; }

    public SelecionarEquipamentoViewModel(Guid id, string nome)
    {
        Id = id;
        Nome = nome;
    }
}

public class CadastrarChamadoViewModel : FormularioChamadoViewModel
{
    public CadastrarChamadoViewModel() { }
    public CadastrarChamadoViewModel(List<Equipamento> equipamentos) : this()
    {
        foreach (Equipamento e in equipamentos)
        {
            SelecionarEquipamentoViewModel selecionarVM = new(e.Id, e.Nome);

            EquipamentosDisponiveis.Add(selecionarVM);
        }
    }
}

public class VisualizarChamadosViewModel
{
    public List<DataChamadoViewModel> Registros { get; set; } = [];

    public VisualizarChamadosViewModel(List<Chamado> chamados)
    {
        foreach (Chamado c in chamados)
        {
            DataChamadoViewModel dataVM = c.ParaDetalhesVM();

            Registros.Add(dataVM);
        }
    }
}

public class EditarChamadoViewModel : FormularioChamadoViewModel
{
    public EditarChamadoViewModel() { }
    public EditarChamadoViewModel(Guid id, string titulo, string descricao, Guid equipamentoId, List<Equipamento> equipamentos)
    {
        Id = id;
        Titulo = titulo;
        Descricao = descricao;
        EquipamentoId = equipamentoId;

        foreach (Equipamento e in equipamentos)
        {
            SelecionarEquipamentoViewModel selecionarVM = new(e.Id, e.Nome);

            EquipamentosDisponiveis.Add(selecionarVM);
        }
    }
}

public class ExcluirChamadoViewModel : FormularioChamadoViewModel
{
    public ExcluirChamadoViewModel() { }
    public ExcluirChamadoViewModel(Guid id, string titulo)
    {
        Id = id;
        Titulo = titulo;
    }
}

public class DataChamadoViewModel : FormularioChamadoViewModel
{
    public DataChamadoViewModel(Guid id, string titulo, string descricao, DateTime dataAbertura, int tempoDecorrido, string nomeEquipamento)
    {
        Id = id;
        Titulo = titulo;
        Descricao = descricao;
        DataAbertura = dataAbertura;
        TempoDecorrido = tempoDecorrido;
        NomeEquipamento = nomeEquipamento;
    }
    public override string ToString()
    {
        return $"ID: {Id}, Equipamento: {NomeEquipamento},  Título: {Titulo}, Descrição: {Descricao}, Data de Abertura: {DataAbertura:d}, Dias em Aberto: {TempoDecorrido} dias";
    }
}