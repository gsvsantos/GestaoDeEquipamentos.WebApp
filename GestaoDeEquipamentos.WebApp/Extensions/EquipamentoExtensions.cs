using GestaoDeEquipamentos.WebApp.Models;
using GestaoDeEquipamentos.WebApp.ModuloEquipamento;
using GestaoDeEquipamentos.WebApp.ModuloFabricante;

namespace GestaoDeEquipamentos.WebApp.Extensions;

public static class EquipamentoExtensions
{
    public static Equipamento ParaEntidade(this FormularioEquipamentoViewModel formularioVM, List<Fabricante> fabricantes)
    {
        Fabricante fabricanteSelecionado = null!;

        foreach (Fabricante f in fabricantes)
        {
            if (f.Id == formularioVM.FabricanteId)
                fabricanteSelecionado = f;
        }

        return new(
            formularioVM.Nome!,
            formularioVM.PrecoAquisicao,
            formularioVM.DataFabricacao,
            fabricanteSelecionado);
    }

    public static DataEquipamentoViewModel ParaDetalhesVM(this Equipamento equipamento)
    {
        return new(
            equipamento.Id,
            equipamento.Nome,
            equipamento.Fabricante.Nome,
            equipamento.PrecoAquisicao,
            equipamento.DataFabricacao);
    }
}
