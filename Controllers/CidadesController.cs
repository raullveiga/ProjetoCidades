using Microsoft.AspNetCore.Mvc;
using ProjetoCidades.Models;
using ProjetoCidades.Repositorio;

namespace ProjetoCidades.Controllers
{
    public class CidadesController : Controller
    {
        Cidade cidade = new Cidade();
        //criar objeto do cidadeRepositorio
        CidadeRepositorio objCidadeRep = new CidadeRepositorio();
        public IActionResult Index()
        {
            //colocar o objRep e seu metodo
            //ele vai listar as infos do banco de dados
            var lista = objCidadeRep.Listar();
            return View(lista);
        }

        public IActionResult CidadesEstados()
        {
            var lista = cidade.ListarCidades();
            return View(lista);
        }

        public IActionResult TodosDados()
        {
            var lista = cidade.ListarCidades();
            return View(lista);
        }
        //usado qndo quero visualizar
        [HttpGet]

        public IActionResult Cadastrar()
        {
            return View();
        }

        //usado quando quero enviar dados
        [HttpPost]
        //Bind é usado quando recebemos informações
        public IActionResult Cadastrar([Bind]Cidade cidade)
        {
            objCidadeRep.Cadastrar(cidade);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Editar(int id){
            var dados = objCidadeRep.Listar(id);
            return View(dados);
        }
        [HttpPost]
        public IActionResult Editar([Bind]Cidade cidade)
        {
            objCidadeRep.Editar(cidade);
            return RedirectToAction("Index");
        }
        public IActionResult Excluir(int id){
            objCidadeRep.Excluir(id);
            return RedirectToAction("Index");
        }


    }
}