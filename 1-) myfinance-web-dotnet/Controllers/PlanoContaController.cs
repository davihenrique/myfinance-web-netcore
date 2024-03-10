using Microsoft.AspNetCore.Mvc;
using myfinance_web_dotnet.Models;
using myfinance_web_dotnet_domain.Entities;
using nyfinance_web_dotnet_service.Interfaces;

namespace myfinance_web_dotnet.Controllers;

[Route("[Controller]")]
public class PlanoContaController : Controller
{
    private readonly ILogger<PlanoContaController> _logger;
    private readonly IPlanoContaService _planoContaService;

    public PlanoContaController(ILogger<PlanoContaController> logger, IPlanoContaService planoContaService)
    {
        _logger = logger;
        _planoContaService = planoContaService;
    }

    [HttpGet]
    [Route("Index")]
    public IActionResult Index()
    {

        var listaPlanoContas = _planoContaService.ListarRegistros();
        List<PlanoContaModel> listaPlanoContaModel = new List<PlanoContaModel>();

        foreach(var item in listaPlanoContas){
            var itemPlanoConta = new PlanoContaModel()
            {
                Id = item.Id,
                Descricao = item.Descricao,
                Tipo = item.Tipo
            };

            listaPlanoContaModel.Add(itemPlanoConta);
        }
        ViewBag.ListaPlanoConta = listaPlanoContaModel;
        return View();
    }

    [HttpGet]
    [Route("Cadastrar")]
    [Route("Cadastrar/{Id}")]
    public IActionResult Cadastrar(int? Id)
    {
        if(Id is null)
          return View();

        var planoConta = _planoContaService.RetornarRegistro((int)Id);
        var planoContaModel = new PlanoContaModel{
            Id = planoConta.Id,
            Descricao = planoConta.Descricao,
            Tipo = planoConta.Tipo
        };

        return View(planoContaModel);
    }

    [HttpPost]
    [Route("Cadastrar")]
    [Route("Cadastrar/{Id}")]
    public IActionResult Cadastrar(PlanoContaModel model)
    {
       var planoconta = new PlanoConta
        {
            Id = model.Id,
            Descricao = model.Descricao,
            Tipo = model.Tipo
        };

        _planoContaService.Cadastrar(planoconta);

        return RedirectToAction("Index");
    }

    [HttpGet]
    [Route("Excluir/{Id}")]
    public IActionResult Exlcuir(int? Id)
    {
        _planoContaService.Excluir((int)Id);

        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location =ResponseCacheLocation.None, NoStore =true)]
    public IActionResult Error()
    {
        return View("Error!");
    }
    
}
