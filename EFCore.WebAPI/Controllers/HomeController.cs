using EFCore.Dominio.Models;
using EFCore.Repositorio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        public readonly HeroiContext _context;

        public HomeController(HeroiContext context)
        {
            _context = context;
        }

        [HttpGet("Atualizar/{nameHero}")]
        public ActionResult Get(string nameHero)
        {
            var heroi = _context.Herois
                            .Where(h => h.Id == 3)
                            .FirstOrDefault();

            heroi.Nome = "Homem-Aranha";

            _context.Herois.Update(heroi);
            _context.SaveChanges();

            return Ok();
        }

        [HttpGet("Delete/{id}")]
        public ActionResult Delete(int id)
        {
            var heroi = _context.Herois
                            .Where(h => h.Id == id)
                            .Single();

            _context.Herois.Remove(heroi);
            _context.SaveChanges();

            return Ok();
        }

        [HttpGet("AddRange")]
        public ActionResult GetAddRange()
        {
            _context.AddRange(
                new Heroi { Nome = "Capitão América" },
                new Heroi { Nome = "Doutor Estranho" },
                new Heroi { Nome = "Pantera Negra" },
                new Heroi { Nome = "Viúva Negra" },
                new Heroi { Nome = "Hulk" },
                new Heroi { Nome = "Gavião Arqueiro" },
                new Heroi { Nome = "Capitã Marvel" }
            );

            _context.SaveChanges();

            return Ok();
        }

        [HttpGet("Seleciona/{nome}")]
        public ActionResult GetFiltro(string nome)
        {
            var listHeroi = _context.Herois
                .Where(h => EF.Functions.Like(h.Nome, $"%{nome}%"))
                .OrderBy(h => h.Id)
                .LastOrDefault();

            //var listHeroi = (
            //                  from heroi in _context.Herois
            //                  where heroi.Nome.Contains(nome)
            //                  select heroi
            //                ).ToList();
            return Ok(listHeroi);
        }
    }
}
