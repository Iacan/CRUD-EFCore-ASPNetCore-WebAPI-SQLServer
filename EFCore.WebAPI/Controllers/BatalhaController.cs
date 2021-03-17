using EFCore.Dominio.Models;
using EFCore.Repositorio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EFCore.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BatalhaController : ControllerBase
    {
        private readonly IEFCoreRepository _repository;

        public BatalhaController(IEFCoreRepository repository)
        {
            _repository = repository;
        }

        // GET: api/<BatalhaController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var herois = await _repository.GetAllBatalhas(true);
                return Ok(herois);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }

        // GET: api/<BatalhaController>
        [HttpGet("{id}", Name = "GetBatalha")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var herois = await _repository.GetBatalhaById(id, true);
                return Ok(herois);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }

        // POST api/<BatalhaController>
        [HttpPost]
        public async Task<IActionResult> Post(Batalha model)
        {
            try
            {
                _repository.Add(model);

                if (await _repository.SaveChangeAsync())
                {
                    return Ok("BAZINGA");
                }
                else
                {
                    return Ok("Não Salvou");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }

        // PUT api/<BatalhaController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id,Batalha model)
        {
            try
            {
                var heroi = await _repository.GetBatalhaById(id);

                if (heroi != null)
                {
                    _repository.Update(heroi);
                    if (await _repository.SaveChangeAsync())
                    {
                        return Ok("BAZINGA");
                    }
                }
                return Ok("Não Encontrado!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }

        // DELETE api/<BatalhaController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var batalha = await _repository.GetBatalhaById(id);

                if (batalha != null)
                {
                    _repository.Delete(batalha);
                    if(await _repository.SaveChangeAsync())
                    {
                        return Ok("BAZINGA");
                    }
                }
                return Ok("Não Encontrado!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }
    }
}
