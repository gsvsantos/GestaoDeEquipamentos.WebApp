using GestaoDeEquipamentos.WebApp.Compartilhado;
using GestaoDeEquipamentos.WebApp.Extensions;
using GestaoDeEquipamentos.WebApp.Models;
using GestaoDeEquipamentos.WebApp.ModuloFabricante;
using Microsoft.AspNetCore.Mvc;

namespace GestaoDeEquipamentos.WebApp.Controllers;

[Route("fabricantes")]
public class FabricanteController : Controller
{
    private readonly ContextoDados contextoDados;
    private readonly IRepositorioFabricante repositorioFabricante;

    public FabricanteController()
    {
        contextoDados = new ContextoDados(true);
        repositorioFabricante = new RepositorioFabricanteEmArquivo(contextoDados);
    }

    [HttpGet("cadastrar")]
    public IActionResult Cadastrar()
    {
        CadastrarFabricanteViewModel cadastrarVM = new();

        return View(cadastrarVM);
    }

    [HttpPost("cadastrar")]
    public IActionResult Cadastrar(CadastrarFabricanteViewModel cadastrarVM)
    {
        Fabricante novoFabricante = cadastrarVM.ParaEntidade();

        repositorioFabricante.CadastrarRegistro(novoFabricante);

        NotificacaoViewModel notificacaoVM = new(
            "Menu Fabricantes",
            "fabricantes",
            $"O registro \"{novoFabricante.Nome}\" foi cadastrado com sucesso!");

        return View("Notificacao", notificacaoVM);
    }

    [HttpGet("visualizar")]
    public IActionResult Visualizar()
    {
        List<Fabricante> fabricantes = repositorioFabricante.SelecionarRegistros();

        VisualizarFabricantesViewModel visualizarVM = new(fabricantes);

        return View(visualizarVM);
    }

    [HttpGet("editar/{id:Guid}")]
    public IActionResult Editar(Guid id)
    {
        Fabricante fabricanteSelecionado = repositorioFabricante.SelecionarRegistroPorId(id);

        EditarFabricanteViewModel editarVM = new(
            fabricanteSelecionado.Id, fabricanteSelecionado.Nome,
            fabricanteSelecionado.Email, fabricanteSelecionado.Telefone);

        return View(editarVM);
    }

    [HttpPost("editar/{id:Guid}")]
    public IActionResult Editar(Guid id, EditarFabricanteViewModel editarVM)
    {
        Fabricante fabricanteAtualizado = editarVM.ParaEntidade();

        repositorioFabricante.EditarRegistro(id, fabricanteAtualizado);

        NotificacaoViewModel notificacaoVM = new(
            "Menu Fabricantes",
            "fabricantes",
            $"O registro \"{fabricanteAtualizado.Nome}\" foi editado com sucesso!");

        return View("Notificacao", notificacaoVM);
    }

    [HttpGet("excluir/{id:Guid}")]
    public IActionResult Excluir(Guid id)
    {
        Fabricante fabricanteSelecionado = repositorioFabricante.SelecionarRegistroPorId(id);

        ExcluirFabricanteViewModel excluirVM = new(id, fabricanteSelecionado.Nome);

        return View(excluirVM);
    }

    [HttpPost("excluir/{id:Guid}")]
    public IActionResult ExcluirConfirmado(Guid id)
    {
        repositorioFabricante.ExcluirRegistro(id);

        NotificacaoViewModel notificacaoVM = new(
            "Menu Fabricantes",
            "fabricantes",
            $"Registro excluído com sucesso!");

        return View("Notificacao", notificacaoVM);
    }
}
