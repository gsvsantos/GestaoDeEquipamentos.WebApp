using System.Diagnostics.CodeAnalysis;
using GestaoDeEquipamentos.WebApp.Compartilhado;
using GestaoDeEquipamentos.WebApp.ModuloEquipamento;

namespace GestaoDeEquipamentos.WebApp.ModuloChamado;

public class Chamado : EntidadeBase<Chamado>
{
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public Equipamento Equipamento { get; set; }
    public DateTime DataAbertura { get; set; }
    public int TempoDecorrido
    {
        get
        {
            TimeSpan diferencaTempo = DateTime.Now.Subtract(DataAbertura);

            return diferencaTempo.Days;
        }
    }

    [ExcludeFromCodeCoverage]
    public Chamado() { }

    public Chamado(string titulo, string descricao, Equipamento equipamento) : this()
    {
        Titulo = titulo;
        Descricao = descricao;
        Equipamento = equipamento;
        DataAbertura = DateTime.Now;
    }

    public override void AtualizarRegistro(Chamado chamadoAtualizado)
    {
        Titulo = chamadoAtualizado.Titulo;
        Descricao = chamadoAtualizado.Descricao;
        Equipamento = chamadoAtualizado.Equipamento;
    }
}
