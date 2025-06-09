using GestaoDeEquipamentos.WebApp.Extensions;
using GestaoDeEquipamentos.WebApp.ModuloFabricante;

namespace GestaoDeEquipamentos.WebApp.Models;

public abstract class FormularioFabricanteViewModel
{
    public Guid Id { get; set; }
    public string? Nome { get; set; }
    public string? Email { get; set; }
    public string? Telefone { get; set; }
}

public class CadastrarFabricanteViewModel : FormularioFabricanteViewModel
{
    public CadastrarFabricanteViewModel() { }
    public CadastrarFabricanteViewModel(string nome, string email, string telefone) : this()
    {
        Nome = nome;
        Email = email;
        Telefone = telefone;
    }
}

public class VisualizarFabricantesViewModel
{
    public List<DataFabricanteViewModel> Registros { get; set; } = [];

    public VisualizarFabricantesViewModel(List<Fabricante> fabricantes)
    {
        foreach (Fabricante f in fabricantes)
        {
            DataFabricanteViewModel dataVM = f.ParaDetalhesVM();

            Registros.Add(dataVM);
        }
    }
}

public class EditarFabricanteViewModel : FormularioFabricanteViewModel
{
    public EditarFabricanteViewModel() { }
    public EditarFabricanteViewModel(Guid id, string nome, string email, string telefone)
    {
        Id = id;
        Nome = nome;
        Email = email;
        Telefone = telefone;
    }
}

public class ExcluirFabricanteViewModel : FormularioFabricanteViewModel
{
    public ExcluirFabricanteViewModel() { }
    public ExcluirFabricanteViewModel(Guid id, string nome)
    {
        Id = id;
        Nome = nome;
    }
}

public class DataFabricanteViewModel : FormularioFabricanteViewModel
{
    public DataFabricanteViewModel(Guid id, string nome, string email, string telefone)
    {
        Id = id;
        Nome = nome;
        Email = email;
        Telefone = telefone;
    }

    public override string ToString()
    {
        return $"Id: {Id}, Nome: {Nome}, E-Mail: {Email}, Telefone: {Telefone}";
    }
}
