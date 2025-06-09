using System.Diagnostics.CodeAnalysis;
using GestaoDeEquipamentos.WebApp.Compartilhado;
using GestaoDeEquipamentos.WebApp.ModuloEquipamento;

namespace GestaoDeEquipamentos.WebApp.ModuloFabricante;

public class Fabricante : EntidadeBase<Fabricante>
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public List<Equipamento> Equipamentos { get; set; } = [];
    public int QuantidadeEquipamentos
    {
        get
        {
            return Equipamentos.Count;
        }
    }

    [ExcludeFromCodeCoverage]
    public Fabricante() { }

    public Fabricante(string nome, string email, string telefone)
    {
        Nome = nome;
        Email = email;
        Telefone = telefone;
    }

    public override void AtualizarRegistro(Fabricante registroEditado)
    {
        Nome = registroEditado.Nome;
        Email = registroEditado.Email;
        Telefone = registroEditado.Telefone;
    }

    public void AdicionarEquipamento(Equipamento equipamento)
    {
        if (!Equipamentos.Contains(equipamento))
            Equipamentos.Add(equipamento);
    }

    public void RemoverEquipamento(Equipamento equipamento)
    {
        if (Equipamentos.Contains(equipamento))
            Equipamentos.Remove(equipamento);
    }
}
