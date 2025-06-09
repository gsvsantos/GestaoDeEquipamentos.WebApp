using System.Diagnostics.CodeAnalysis;
using GestaoDeEquipamentos.WebApp.Compartilhado;
using GestaoDeEquipamentos.WebApp.ModuloChamado;
using GestaoDeEquipamentos.WebApp.ModuloFabricante;

namespace GestaoDeEquipamentos.WebApp.ModuloEquipamento;

public class Equipamento : EntidadeBase<Equipamento>
{
    public string Nome { get; set; }
    public Fabricante Fabricante { get; set; }
    public decimal PrecoAquisicao { get; set; }
    public DateTime DataFabricacao { get; set; }

    public List<Chamado> Chamados { get; set; } = [];

    public string NumeroSerie
    {
        get
        {
            string tresPrimeirosCaracteres = Nome.Substring(0, 3).ToUpper();

            return $"{tresPrimeirosCaracteres}-{Id}";
        }
    }

    [ExcludeFromCodeCoverage]
    public Equipamento() { }

    public Equipamento(string nome, decimal precoAquisicao, DateTime dataFabricacao, Fabricante fabricante) : this()
    {
        Nome = nome;
        PrecoAquisicao = precoAquisicao;
        DataFabricacao = dataFabricacao;
        Fabricante = fabricante;
    }

    public void AdicionarChamado(Chamado chamado)
    {
        if (!Chamados.Contains(chamado))
            Chamados.Add(chamado);
    }

    public void RemoverChamado(Chamado chamado)
    {
        if (Chamados.Contains(chamado))
            Chamados.Remove(chamado);
    }

    public override void AtualizarRegistro(Equipamento equipamentoAtualizado)
    {
        Nome = equipamentoAtualizado.Nome;
        DataFabricacao = equipamentoAtualizado.DataFabricacao;
        PrecoAquisicao = equipamentoAtualizado.PrecoAquisicao;
    }
}
