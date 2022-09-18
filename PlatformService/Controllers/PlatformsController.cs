using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlatformsController : ControllerBase
{
    private readonly IPlatformRepo _repository;
    private readonly IMapper _mapper;

    public PlatformsController(IPlatformRepo repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<IEnumerable<PlatformReadDto>> GetAllPlatforms()
    {
        Console.Write("--> Getting Platforms...");

        var platforms = _repository.GetAllPlatforms();
        var platformDtos = _mapper.Map<IEnumerable<PlatformReadDto>>(platforms);

        return Ok(platformDtos);
    }

    [HttpGet("{id}", Name = "GetPlatformById")]
    public ActionResult<IEnumerable<PlatformReadDto>> GetPlatformById(int id)
    {
        Console.Write("--> Getting Platforms...");

        var platform = _repository.GetPlatformById(id);
        
        if(platform != null)
        {
            var platformDto = _mapper.Map<PlatformReadDto>(platform);

            return Ok(platformDto);
        }

        return NotFound();
    }

    [HttpPost]
    public ActionResult<PlatformReadDto> CreatePlatform(PlatformCreateDto platformCreateDto)
    {
        Console.Write("--> Getting Platforms...");

        var platformModel = _mapper.Map<Platform>(platformCreateDto);

        _repository.CreatePlatform(platformModel);
        _repository.SaveChanges();

        var platformReadDto = _mapper.Map<PlatformReadDto>(platformModel);

        return CreatedAtRoute(nameof(GetPlatformById), new { Id = platformReadDto.Id }, platformReadDto);
    }
}
