using GestaoDeEquipamentos.WebApp.Compartilhado;
using GestaoDeEquipamentos.WebApp.Extensions;
using GestaoDeEquipamentos.WebApp.Models;
using GestaoDeEquipamentos.WebApp.ModuloChamado;
using GestaoDeEquipamentos.WebApp.ModuloEquipamento;
using Microsoft.AspNetCore.Mvc;

namespace GestaoDeEquipamentos.WebApp.Controllers;

[Route("chamados")]
public class ChamadoController : Controller
{
    private readonly ContextoDados contextoDados;
    private readonly IRepositorioChamado repositorioChamado;
    private readonly IRepositorioEquipamento repositorioEquipamento;

    public ChamadoController()
    {
        contextoDados = new(true);
        repositorioChamado = new RepositorioChamadoEmArquivo(contextoDados);
        repositorioEquipamento = new RepositorioEquipamentoEmArquivo(contextoDados);
    }

    [HttpGet("cadastrar")]
    public IActionResult Cadastrar()
    {
        List<Equipamento> equipamentos = repositorioEquipamento.SelecionarRegistros();

        CadastrarChamadoViewModel cadastrarVM = new(equipamentos);

        return View(cadastrarVM);
    }

    [HttpPost("cadastrar")]
    public IActionResult Cadastrar(CadastrarChamadoViewModel cadastrarVM)
    {
        List<Equipamento> equipamentos = repositorioEquipamento.SelecionarRegistros();

        Chamado novoChamado = cadastrarVM.ParaEntidade(equipamentos);

        repositorioChamado.CadastrarRegistro(novoChamado);

        var notificacaoVM = new NotificacaoViewModel(
            "Menu Chamados",
            "chamados",
            $"O registro {novoChamado.Titulo} foi cadastrado com sucesso!");

        return View("Notificacao", notificacaoVM);
    }

    [HttpGet("visualizar")]
    public IActionResult Visualizar()
    {
        List<Chamado> chamados = repositorioChamado.SelecionarRegistros();

        VisualizarChamadosViewModel visualizarVM = new(chamados);

        return View(visualizarVM);
    }

    [HttpGet("editar/{id:Guid}")]
    public IActionResult Editar(Guid id)
    {
        List<Equipamento> equipamentos = repositorioEquipamento.SelecionarRegistros();

        Chamado chamadoSelecionado = repositorioChamado.SelecionarRegistroPorId(id);

        EditarChamadoViewModel editarVM = new(
            id,
            chamadoSelecionado.Titulo,
            chamadoSelecionado.Descricao,
            chamadoSelecionado.Equipamento.Id,
            equipamentos);

        return View(editarVM);
    }

    [HttpPost("editar/{id:Guid}")]
    public IActionResult Editar(Guid id, EditarChamadoViewModel editarVM)
    {
        List<Equipamento> equipamentos = repositorioEquipamento.SelecionarRegistros();

        Chamado chamadoOriginal = repositorioChamado.SelecionarRegistroPorId(id);
        Equipamento equipamentoOriginal = chamadoOriginal.Equipamento;

        Chamado chamadoEditado = editarVM.ParaEntidade(equipamentos);
        Equipamento equipamentoEditado = chamadoOriginal.Equipamento;

        if (equipamentoOriginal.Id != equipamentoEditado.Id)
        {
            equipamentoOriginal.RemoverChamado(chamadoOriginal);
            equipamentoEditado.AdicionarChamado(chamadoEditado);
        }

        repositorioChamado.EditarRegistro(id, chamadoEditado);

        var notificacaoVM = new NotificacaoViewModel(
            "Menu Chamados",
            "chamados",
            $"O registro {chamadoEditado.Titulo} foi editado com sucesso!");

        return View("Notificacao", notificacaoVM);
    }

    [HttpGet("excluir/{id:Guid}")]
    public IActionResult Excluir(Guid id)
    {
        Chamado chamadoSelecionado = repositorioChamado.SelecionarRegistroPorId(id);

        ExcluirChamadoViewModel excluirVM = new(id, chamadoSelecionado.Titulo);

        return View(excluirVM);
    }

    [HttpPost("excluir/{id:Guid}")]
    public IActionResult ExcluirConfirmado(Guid id)
    {
        repositorioChamado.ExcluirRegistro(id);

        var notificacaoVM = new NotificacaoViewModel(
            "Menu Chamados",
            "chamados",
            $"Registro excluído com sucesso!");

        return View("Notificacao", notificacaoVM);
    }
}
