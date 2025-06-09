using GestaoDeEquipamentos.WebApp.Models;
using GestaoDeEquipamentos.WebApp.ModuloFabricante;

namespace GestaoDeEquipamentos.WebApp.Extensions;

public static class FabricanteExtensions
{
    public static Fabricante ParaEntidade(this FormularioFabricanteViewModel formularioVM)
    {
        return new(
            formularioVM.Nome!,
            formularioVM.Email!,
            formularioVM.Telefone!);
    }

    public static DataFabricanteViewModel ParaDetalhesVM(this Fabricante fabricante)
    {
        return new(
            fabricante.Id,
            fabricante.Nome,
            fabricante.Email,
            fabricante.Telefone);
    }
}
