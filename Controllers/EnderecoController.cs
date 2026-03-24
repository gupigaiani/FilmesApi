using AutoMapper;
using FilmesApi.Data;
using FilmesApi.Data.Dtos;
using FilmesApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FilmesApi.Controllers;

[ApiController]
[Route("[controller]")]
public class EnderecoController : ControllerBase
{
    private FilmeContext _context;
    private IMapper _mapper;

    public EnderecoController(FilmeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Adiciona um endereço ao banco de dados
    /// </summary>
    /// <param name="enderecoDto">Objeto com os campos necessários para criação de um endereço</param>
    /// <returns>IActionResult</returns>
    /// <response code="201"> Caso inserção seja feita com sucesso</response>
    /// <response code="400">Caso os dados enviados sejam inválidos</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult AdicionaEndereco([FromBody] CreateEnderecoDto enderecoDto)
    {
        Endereco endereco = _mapper.Map<Endereco>(enderecoDto);
        _context.Enderecos.Add(endereco);
        _context.SaveChanges();
        return CreatedAtAction(nameof(RecuperaCinemasPorId), new { Id = endereco.Id}, enderecoDto);
    }

    /// <summary>
    /// Recupera uma lista de endereços do banco de dados
    /// </summary>
    /// <returns>Lista de endereços cadastrados</returns>
    /// <response code="200">Retorna a lista de endereços</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IEnumerable<ReadEnderecoDto> RecuperaEnderecos()
    {
        return _mapper.Map<List<ReadEnderecoDto>>(_context.Enderecos);
    }

    /// <summary>
    /// Recupera um endereço específico pelo Id
    /// </summary>
    /// <param name="id">Identificador único do endereço</param>
    /// <returns>Endereço encontrado</returns>
    /// <response code="200">Retorna o endereço encontrado</response>
    /// <response code="404">Caso o endereço não seja encontrado</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult RecuperaCinemasPorId(int id)
    {
        Endereco endereco = _context.Enderecos.FirstOrDefault(endereco => endereco.Id == id);
        if (endereco != null)
        {
            ReadEnderecoDto enderecoDto = _mapper.Map<ReadEnderecoDto>(endereco);
            return Ok(enderecoDto);
        }
        return NotFound();
    }

    /// <summary>
    /// Atualiza completamente os dados de um endereço
    /// </summary>
    /// <param name="id">Identificador único do endereço</param>
    /// <param name="enderecoDto">Objeto com os novos dados do endereço</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Caso a atualização seja realizada com sucesso</response>
    /// <response code="404">Caso o endereço não seja encontrado</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult AtualizaEndereco(int id, [FromBody] UpdateEnderecoDto enderecoDto)
    {
        Endereco endereco = _context.Enderecos.FirstOrDefault(endereco => endereco.Id == id);
        if (endereco == null)
        {
            return NotFound();
        }
        _mapper.Map(enderecoDto, endereco);
        _context.SaveChanges();
        return NoContent();
    }

    /// <summary>
    /// Remove um endereço do banco de dados
    /// </summary>
    /// <param name="id">Identificador único do endereço</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Caso o endereço seja removido com sucesso</response>
    /// <response code="404">Caso o endereço não seja encontrado</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DeletaEndereco(int id)
    {
        Endereco endereco = _context.Enderecos.FirstOrDefault(endereco => endereco.Id == id);
        if (endereco == null)
        {
            return NotFound();
        }
        _context.Remove(endereco);
        _context.SaveChanges();
        return NoContent();
    }
    
}
