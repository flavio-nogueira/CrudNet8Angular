using AutoMapper;
using BlackEnd.Application.Commands;
using BlackEnd.Application.DTOs;
using BlackEnd.Domain.Entities;
using BlackEnd.Domain.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace BlackEnd.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IValidator<CreateClienteCommand> _createValidator;
        private readonly IValidator<UpdateClienteCommand> _updateValidator;

        public ClienteController(
            IClienteRepository clienteRepository,
            IMapper mapper,
            IMediator mediator,
            IValidator<CreateClienteCommand> createValidator,
            IValidator<UpdateClienteCommand> updateValidator)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
            _mediator = mediator;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }
    
        [HttpPost]
        [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreateClienteCommand command)
        {
            if (command == null)
                return BadRequest(new ProblemDetails { Title = "O comando não pode ser nulo." });

            try
            {
                var result = await _createValidator.ValidateAsync(command);
                if (!result.IsValid)
                {
                    return BadRequest(new ProblemDetails
                    {
                        Title = "Erro de Validação",
                        Status = (int)HttpStatusCode.BadRequest,
                        Detail = "Ocorreram erros de validação.",
                        Extensions = { ["errors"] = result.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }) }
                    });
                }

                var clienteId = await _mediator.Send(command);
                return CreatedAtAction(nameof(GetById), new { id = clienteId }, clienteId);
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: (int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ClienteDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateClienteCommand command)
        {
            if (command == null)
                return BadRequest(new ProblemDetails { Title = "O comando não pode ser nulo." });

            if (command.Id != id)
                return BadRequest(new ProblemDetails { Title = "O ID do cliente não corresponde." });

            try
            {
                var result = await _updateValidator.ValidateAsync(command);
                if (!result.IsValid)
                {
                    return BadRequest(new ProblemDetails
                    {
                        Title = "Erro de Validação",
                        Status = (int)HttpStatusCode.BadRequest,
                        Detail = "Ocorreram erros de validação.",
                        Extensions = { ["errors"] = result.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }) }
                    });
                }

                var atualizado = await _mediator.Send(command);
                if (!atualizado)
                    return NotFound(new ProblemDetails { Title = "Cliente não encontrado." });

                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: (int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var command = new DeleteClienteCommand { Id = id };
                var resultado = await _mediator.Send(command);

                if (!resultado)
                    return NotFound(new ProblemDetails { Title = "Cliente não encontrado." });

                return NoContent();
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: (int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ClienteDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var cliente = await _clienteRepository.ObterPorIdAsync(id);
                if (cliente == null)
                    return NotFound(new ProblemDetails { Title = "Cliente não encontrado." });

                return Ok(_mapper.Map<ClienteDto>(cliente));
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: (int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<ClienteDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var clientes = await _clienteRepository.ObterTodosAsync();
                var clientesDto = _mapper.Map<List<ClienteDto>>(clientes);
                return Ok(clientesDto);
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: (int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
