using Microsoft.AspNetCore.Mvc;
using PoCGastos.Models;
using PoCGastos.Data;
using System.Globalization;
using System.Linq;

namespace PoCGastos.Controllers
{
    public class TabelasController : Controller
    {
        private readonly AppDbContext _banco;

        public TabelasController(AppDbContext banco)
        {
            _banco = banco;
        }

        public IActionResult Index()
        {
            var contextos = _banco.Contextos.ToList();
            return View(contextos);
        }

        [HttpPost]
        public IActionResult Criar(string nome, string? descricao, string? limiteMensal, bool ativo)
        {
            if (!string.IsNullOrEmpty(nome))
            {
                var novoContexto = new ContextoGasto
                {
                    Nome = nome,
                    Descricao = string.IsNullOrEmpty(descricao) ? "Sem descrição" : descricao,
                    LimiteMensal = LerValor(limiteMensal),
                    Ativo = ativo,
                    TotalGasto = 0,
                    QuantidadeLancamentos = 0
                };

                _banco.Contextos.Add(novoContexto); 
                _banco.SaveChanges(); 
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Editar(int id, string nome, string? descricao, string? limiteMensal, bool ativo)
        {
            var contexto = _banco.Contextos.Find(id); 
            if (contexto != null && !string.IsNullOrEmpty(nome))
            {
                contexto.Nome = nome;
                contexto.Descricao = string.IsNullOrEmpty(descricao) ? "Sem descrição" : descricao;
                contexto.LimiteMensal = LerValor(limiteMensal);
                contexto.Ativo = ativo;

                _banco.SaveChanges(); 
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Deletar(int id)
        {
            var contexto = _banco.Contextos.Find(id); 
            if (contexto != null)
            {
                _banco.Contextos.Remove(contexto);
                _banco.SaveChanges(); 
            }
            return RedirectToAction("Index");
        }

        private static decimal? LerValor(string? valor)
        {
            return decimal.TryParse(valor, NumberStyles.Number, CultureInfo.InvariantCulture, out var resultado)
                ? resultado
                : null;
        }
    }
}