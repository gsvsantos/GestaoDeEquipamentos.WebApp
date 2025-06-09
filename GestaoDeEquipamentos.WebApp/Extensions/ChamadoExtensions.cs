using GestaoDeEquipamentos.WebApp.Models;
using GestaoDeEquipamentos.WebApp.ModuloChamado;
using GestaoDeEquipamentos.WebApp.ModuloEquipamento;

namespace GestaoDeEquipamentos.WebApp.Extensions;

public static class ChamadoExtensions
{
    public static Chamado ParaEntidade(this FormularioChamadoViewModel formularioVM, List<Equipamento> equipamentos)
    {
        Equipamento equipamentoSelecionado = null!;

        foreach (Equipamento e in equipamentos)
        {
            if (e.Id == formularioVM.EquipamentoId)
                equipamentoSelecionado = e;
        }

        return new(
            formularioVM.Titulo!,
            formularioVM.Descricao!,
            equipamentoSelecionado);
    }

    public static DataChamadoViewModel ParaDetalhesVM(this Chamado chamado)
    {
        return new(
            chamado.Id,
            chamado.Titulo,
            chamado.Descricao,
            chamado.DataAbertura,
            chamado.TempoDecorrido,
            chamado.Equipamento.Nome);
    }
}
