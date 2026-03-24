using AutoMapper;
using FilmesApi.Data;
using FilmesApi.Data.Dtos;
using FilmesApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FilmesApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CinemaController : ControllerBase
{
    private FilmeContext _context;
    private IMapper _mapper;

    public CinemaController(FilmeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Adiciona um cinema ao banco de dados
    /// </summary>
    /// <param name="cinemaDto">Objeto com os campos necessários para criação de um cinema</param>
    /// <returns>IActionResult</returns>
    /// <response code="201"> Caso inserção seja feita com sucesso</response>
    /// <response code="400">Caso os dados enviados sejam inválidos</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult AdicionaCinema([FromBody] CreateCinemaDto cinemaDto)
    {
        Cinema cinema = _mapper.Map<Cinema>(cinemaDto);
        _context.Cinemas.Add(cinema);
        _context.SaveChanges();
        return CreatedAtAction(nameof(RecuperaCinemasPorId), new { Id = cinema.Id}, cinemaDto);
    }

    /// <summary>
    /// Recupera uma lista de cinemas do banco de dados, opcionalmente filtrados por enderecoId
    /// </summary>
    /// <param name="enderecoId">Identificador do endereço para filtrar cinemas (opcional)</param>
    /// <returns>Lista de cinemas cadastrados</returns>
    /// <response code="200">Retorna a lista de cinemas</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IEnumerable<ReadCinemaDto> RecuperaCinemas([FromQuery] int? enderecoId = null)
    {
        if (enderecoId == null)
            return _mapper.Map<List<ReadCinemaDto>>(_context.Cinemas.ToList());
        return _mapper.Map<List<ReadCinemaDto>>
            (_context.Cinemas.FromSqlRaw($"SELECT Id, Nome, EnderecoId FROM cinemas WHERE cinemas.EnderecoId = {enderecoId}").ToList());
        
    }

    /// <summary>
    /// Recupera um cinema específico pelo Id
    /// </summary>
    /// <param name="id">Identificador único do cinema</param>
    /// <returns>Cinema encontrado</returns>
    /// <response code="200">Retorna o cinema encontrado</response>
    /// <response code="404">Caso o cinema não seja encontrado</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult RecuperaCinemasPorId(int id)
    {
        Cinema cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
        if (cinema != null)
        {
            ReadCinemaDto cinemaDto = _mapper.Map<ReadCinemaDto>(cinema);
            return Ok(cinemaDto);
        }
        return NotFound();
    }

    /// <summary>
    /// Atualiza completamente os dados de um cinema
    /// </summary>
    /// <param name="id">Identificador único do cinema</param>
    /// <param name="cinemaDto">Objeto com os novos dados do cinema</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Caso a atualização seja realizada com sucesso</response>
    /// <response code="404">Caso o cinema não seja encontrado</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult AtualizaCinema(int id, [FromBody] UpdateCinemaDto cinemaDto)
    {
        Cinema cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
        if (cinema == null)
        {
            return NotFound();
        }
        _mapper.Map(cinemaDto, cinema);
        _context.SaveChanges();
        return NoContent();
    }

    /// <summary>
    /// Remove um cinema do banco de dados
    /// </summary>
    /// <param name="id">Identificador único do cinema</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Caso o cinema seja removido com sucesso</response>
    /// <response code="404">Caso o cinema não seja encontrado</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DeletaCinema(int id)
    {
        Cinema cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
        if (cinema == null)
        {
            return NotFound();
        }
        _context.Remove(cinema);
        _context.SaveChanges();
        return NoContent();
    }
    
}
