using GestaoDeEquipamentos.WebApp.Compartilhado;

namespace GestaoDeEquipamentos.WebApp.ModuloFabricante;

public class RepositorioFabricanteEmArquivo : RepositorioBaseEmArquivo<Fabricante>, IRepositorioFabricante
{
    public RepositorioFabricanteEmArquivo(ContextoDados contexto) : base(contexto) { }

    protected override List<Fabricante> ObterRegistros()
    {
        return contexto.Fabricantes;
    }
}
