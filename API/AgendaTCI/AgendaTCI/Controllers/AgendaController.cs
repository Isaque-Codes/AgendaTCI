using AgendaTCI.Applications.Services;
using AgendaTCI.DTOs.AgendaDto;
using AgendaTCI.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace AgendaTCI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgendaController : ControllerBase
    {
        private readonly AgendaService _service;

        public AgendaController(AgendaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<LerAgendaDto>>> Listar()
        {
            List<LerAgendaDto> agendas = await _service.ListarAsync();
            return Ok(agendas);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<LerAgendaDto>> BuscarPorId(Guid id)
        {
            try
            {
                LerAgendaDto agenda = await _service.BuscarPorIdAsync(id);
                return Ok(agenda);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("buscar/nome")]
        public async Task<ActionResult<LerAgendaDto>> BuscarPorNome(string nome)
        {
            try
            {
                LerAgendaDto agenda = await _service.BuscarPorNomeAsync(nome);
                return Ok(agenda);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("buscar/email")]
        public async Task<ActionResult<LerAgendaDto>> BuscarPorEmail(string email)
        {
            try
            {
                LerAgendaDto agenda = await _service.BuscarPorEmailAsync(email);
                return Ok(agenda);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("buscar/cpf")]
        public async Task<ActionResult<LerAgendaDto>> BuscarPorCPF(string cpf)
        {
            try
            {
                LerAgendaDto agenda = await _service.BuscarPorCPFAsync(cpf);
                return Ok(agenda);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("buscar/telefone")]
        public async Task<ActionResult<LerAgendaDto>> BuscarPorTelefone(string telefone)
        {
            try
            {
                LerAgendaDto agenda = await _service.BuscarPorTelefone(telefone);
                return Ok(agenda);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(CadastrarAgendaDto dto)
        {
            try
            {
                await _service.CadastrarAsync(dto);
                return StatusCode(201);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Atualizar(Guid id, AtualizarAgendaDto dto)
        {
            try
            {
                await _service.AtualizarAsync(id, dto);
                return NoContent();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Deletar(Guid id)
        {
            try
            {
                await _service.DeletarAsync(id);
                return NoContent();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}