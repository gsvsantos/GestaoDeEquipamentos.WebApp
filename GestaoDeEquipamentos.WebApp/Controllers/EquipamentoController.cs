using GestaoDeEquipamentos.WebApp.Compartilhado;
using GestaoDeEquipamentos.WebApp.Extensions;
using GestaoDeEquipamentos.WebApp.Models;
using GestaoDeEquipamentos.WebApp.ModuloEquipamento;
using GestaoDeEquipamentos.WebApp.ModuloFabricante;
using Microsoft.AspNetCore.Mvc;

namespace GestaoDeEquipamentos.WebApp.Controllers;

[Route("equipamentos")]
public class EquipamentoController : Controller
{
    private readonly ContextoDados contextoDados;
    private readonly IRepositorioEquipamento repositorioEquipamento;
    private readonly IRepositorioFabricante repositorioFabricante;

    public EquipamentoController()
    {
        contextoDados = new ContextoDados(true);
        repositorioEquipamento = new RepositorioEquipamentoEmArquivo(contextoDados);
        repositorioFabricante = new RepositorioFabricanteEmArquivo(contextoDados);
    }

    [HttpGet("cadastrar")]
    public IActionResult Cadastrar()
    {
        var fabricantes = repositorioFabricante.SelecionarRegistros();

        var cadastrarVM = new CadastrarEquipamentoViewModel(fabricantes);

        return View(cadastrarVM);
    }

    [HttpPost("cadastrar")]
    public IActionResult Cadastrar(CadastrarEquipamentoViewModel cadastrarVM)
    {
        var fabricantes = repositorioFabricante.SelecionarRegistros();

        var novoEquipamento = cadastrarVM.ParaEntidade(fabricantes);

        repositorioEquipamento.CadastrarRegistro(novoEquipamento);

        var notificacaoVM = new NotificacaoViewModel(
            "Menu Equipamentos",
            "equipamentos",
            $"O registro {novoEquipamento.Nome} foi cadastrado com sucesso!");

        return View("Notificacao", notificacaoVM);
    }

    [HttpGet("visualizar")]
    public IActionResult Visualizar()
    {
        var equipamentos = repositorioEquipamento.SelecionarRegistros();

        var visualizarVM = new VisualizarEquipamentosViewModel(equipamentos);

        return View(visualizarVM);
    }

    [HttpGet("editar/{id:Guid}")]
    public IActionResult Editar(Guid id)
    {
        var equipamentoSelecionado = repositorioEquipamento.SelecionarRegistroPorId(id);

        var fabricantes = repositorioFabricante.SelecionarRegistros();

        var editarVM = new EditarEquipamentoViewModel(
            id,
            equipamentoSelecionado.Nome,
            equipamentoSelecionado.PrecoAquisicao,
            equipamentoSelecionado.DataFabricacao,
            equipamentoSelecionado.Fabricante.Id,
            fabricantes);

        return View(editarVM);
    }

    [HttpPost("editar/{id:Guid}")]
    public IActionResult Editar(Guid id, EditarEquipamentoViewModel editarVM)
    {
        var fabricantes = repositorioFabricante.SelecionarRegistros();

        var equipamentoOriginal = repositorioEquipamento.SelecionarRegistroPorId(id); ;
        var fabricanteOriginal = equipamentoOriginal.Fabricante;

        var equipamentoEditado = editarVM.ParaEntidade(fabricantes);
        var fabricanteEditado = equipamentoEditado.Fabricante;

        if (fabricanteOriginal != fabricanteEditado)
        {
            fabricanteOriginal.RemoverEquipamento(equipamentoOriginal);
            fabricanteEditado.AdicionarEquipamento(equipamentoEditado);
        }

        repositorioEquipamento.EditarRegistro(id, equipamentoEditado);

        var notificacaoVM = new NotificacaoViewModel(
            "Menu Equipamentos",
            "equipamentos",
            $"O registro {equipamentoEditado.Nome} foi editado com sucesso!");

        return View("Notificacao", notificacaoVM);
    }

    [HttpGet("excluir/{id:Guid}")]
    public IActionResult Excluir(Guid id)
    {
        var equipamentoSelecionado = repositorioEquipamento.SelecionarRegistroPorId(id);

        var excluirVM = new ExcluirEquipamentoViewModel(id, equipamentoSelecionado.Nome);

        return View(excluirVM);
    }

    [HttpPost("excluir/{id:Guid}")]
    public IActionResult ExcluirConfirmado(Guid id)
    {
        var equipamentoSelecionado = repositorioEquipamento.SelecionarRegistroPorId(id);

        repositorioEquipamento.ExcluirRegistro(id);

        var notificacaoVM = new NotificacaoViewModel(
            "Menu Equipamentos",
            "equipamentos",
            $"Registro excluído com sucesso!");

        return View("Notificacao", notificacaoVM);
    }
}
