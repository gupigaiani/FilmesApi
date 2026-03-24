using AutoMapper;
using FilmesApi.Data;
using FilmesApi.Data.Dtos;
using FilmesApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FilmesApi.Controllers;

[ApiController]
[Route("[controller]")]
public class SessaoController : ControllerBase
{
    private FilmeContext _context;
    private IMapper _mapper;

    public SessaoController(FilmeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Adiciona uma sessão ao banco de dados
    /// </summary>
    /// <param name="dto">Objeto com os campos necessários para criação de uma sessão</param>
    /// <returns>IActionResult</returns>
    /// <response code="201"> Caso inserção seja feita com sucesso</response>
    /// <response code="400">Caso os dados enviados sejam inválidos</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult AdicionaSessao(CreateSessaoDto dto)
    {
        Sessao sessao = _mapper.Map<Sessao>(dto);
        _context.Sessoes.Add(sessao);
        _context.SaveChanges();
        return CreatedAtAction(nameof(RecuperaSessoesPorId), new { filmeId = sessao.FilmeId, cinemaId = sessao.CinemaId }, sessao);
    }

    /// <summary>
    /// Recupera uma lista de sessões do banco de dados
    /// </summary>
    /// <returns>Lista de sessões cadastradas</returns>
    /// <response code="200">Retorna a lista de sessões</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IEnumerable<ReadSessaoDto> RecuperaSessoes()
    {
        return _mapper.Map<List<ReadSessaoDto>>(_context.Sessoes);
    }

    /// <summary>
    /// Recupera uma sessão específica pelos Ids do filme e cinema
    /// </summary>
    /// <param name="filmeId">Identificador único do filme</param>
    /// <param name="cinemaId">Identificador único do cinema</param>
    /// <returns>Sessão encontrada</returns>
    /// <response code="200">Retorna a sessão encontrada</response>
    /// <response code="404">Caso a sessão não seja encontrada</response>
    [HttpGet("{filmeId}/{cinemaId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult RecuperaSessoesPorId(int filmeId, int cinemaId)
    {
        Sessao sessao = _context.Sessoes.FirstOrDefault(sessao => 
            sessao.FilmeId == filmeId && sessao.CinemaId == cinemaId);
        if (sessao != null)
        {
            ReadSessaoDto sessaoDto = _mapper.Map<ReadSessaoDto>(sessao);
            return Ok(sessaoDto);
        }
        return NotFound();
    }
    
}
