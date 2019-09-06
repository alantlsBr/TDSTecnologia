using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TDSTecnologia.Site.Core.Entities;
using TDSTecnologia.Site.Infrastructure.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TDSTecnologia.Site.Web.Controllers
{
    public class PermissaoController : Controller
    {
        private readonly PermissaoService _permissaoService;

        public PermissaoController(PermissaoService permissaoService)
        {
            _permissaoService = permissaoService;
        }

        public IActionResult Index()
        {
            List<Permissao> permissoes = _permissaoService.ListarTodos();
            return View("Index", permissoes);
        }

        [HttpGet]
        public IActionResult Novo()
        {
            return View("Novo");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Novo([Bind("Descricao,Name")] Permissao permissao)
        {
            if (ModelState.IsValid)
            {
                bool existePermissao = await _permissaoService.ExistePermissao(permissao.Name);

                if (!existePermissao)
                {
                    permissao.NormalizedName = permissao.Name.ToUpper();
                    await _permissaoService.Salvar(permissao);
                    return RedirectToAction("Index", "Permissao");
                }
            }
            return View(permissao);
        }
    }
}
