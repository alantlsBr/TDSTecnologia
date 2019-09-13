using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TDSTecnologia.Site.Core.Entities;
using TDSTecnologia.Site.Core.Utilitarios;
using TDSTecnologia.Site.Infrastructure.Integrations.Email;
using TDSTecnologia.Site.Infrastructure.Services;
using TDSTecnologia.Site.Web.ViewModels;
using X.PagedList;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TDSTecnologia.Site.Web.Controllers
{
    public class HomeController : AppAbstractController
    {
        private readonly CursoService _cursoService;
        private readonly ILogger<HomeController> _logger;
        private readonly IEmail _email;

        public HomeController(CursoService cursoService, ILogger<HomeController> logger, IEmail email)
        {
            _cursoService = cursoService;
            _logger = logger;
            _email = email;
        }

        public IActionResult Index(int? pagina)
        {
            _logger.LogInformation("Listagem de cursos...");
            IPagedList<Curso> cursos = _cursoService.ListarComPaginacao(pagina);
            var viewModel = new CursoViewModel
            {
                CursosComPaginacao = cursos
            };
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Novo()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Novo([Bind("Id,Nome,Descricao,QuantidadeAula,DataInicio,Turno,Modalidade,Nivel,Vagas")] Curso curso, IFormFile arquivo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    curso.Banner = UtilImagem.ConverterParaByte(arquivo);
                    _cursoService.Salvar(curso);
                    AddMensagemSucesso("Curso Cadastrado");
                    return RedirectToAction(nameof(Novo));
                }
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
                AddMensagemErro("Falha no cadastro");
            }

            return View(curso);

        }

        public IActionResult Detalhes(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var curso = _cursoService.PesquisarPorId(id);
            if (curso == null)
            {
                return NotFound();
            }

            return View(curso);
        }

        public IActionResult Alterar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var curso = _cursoService.PesquisarPorId(id);

            if (curso == null)
            {
                return NotFound();
            }
            return View(curso);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Alterar(int id, [Bind("Id,Nome,Descricao,QuantidadeAula,DataInicio,Turno,Modalidade,Nivel,Vagas")] Curso curso)
        {
            if (id != curso.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _cursoService.Atualizar(curso);
                return RedirectToAction(nameof(Index));
            }
            return View(curso);
        }

        public IActionResult Excluir(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var curso = _cursoService.PesquisarPorId(id);
            if (curso == null)
            {
                return NotFound();
            }

            return View(curso);
        }

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmarExclusao(int id)
        {
            _cursoService.Excluir(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult PesquisarCurso(CursoViewModel pesquisa)
        {
            if (pesquisa.Texto != null && !String.IsNullOrEmpty(pesquisa.Texto))
            {
                List<Curso> cursos = _cursoService.PesquisarPorNomeDescricao(pesquisa.Texto);
                var viewModel = new CursoViewModel
                {
                    Cursos = cursos
                };
                return View("Index", viewModel);
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Email()
        {
            string assunto = "Mensagem da Aplicação TDSTecnologia";
            string mensagem = string.Format("Email enviado!!!");
            await _email.EnviarEmail("alantls.sis@gmail.com", assunto, mensagem);
            return RedirectToAction(nameof(Index));
        }
    }
}
