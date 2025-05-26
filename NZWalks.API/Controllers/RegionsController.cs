using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ValidateModel]
    //[Authorize]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        private readonly ILogger<RegionsController> logger;

        public RegionsController(IRegionRepository regionRepository,
            IMapper mapper,ILogger<RegionsController> logger)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
            this.logger = logger;
        }
        [HttpGet]
        //[RoleAccess("reader")]
        public async Task<IActionResult> GetAll()
        {
            logger.LogInformation("");
            var regions = await regionRepository.GetAllAsync();
            logger.LogInformation($"Regions {JsonSerializer.Serialize(regions)}");
            return Ok(mapper.Map<List<RegionDTO>>(regions));
        }
        [HttpGet]
        [Route("{id:Guid}")]
        [RoleAccess("reader")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var region = await regionRepository.GetByIdAsync(id);
            if (region != null)
            {
                return Ok(mapper.Map<RegionDTO>(region));
            }
            return NotFound();
        }
        [HttpPut]
        [Route("{id:Guid}")]
        [RoleAccess("writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDTO region)
        {
            var existingRegion =await regionRepository.UpdateAsync(id,mapper.Map<Region>(region));
            if (existingRegion != null)
            {
                return Ok(mapper.Map<Region>(existingRegion));
            }
            return NotFound();
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        [RoleAccess("writer")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var existingRegion =await regionRepository.DeleteAsync(id);
            if (existingRegion != null)
            {
                return Ok(mapper.Map<RegionDTO>(existingRegion));
            }
            return NotFound();
        }
        [HttpPost]
        [RoleAccess("writer")]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDTO region)
        {
            var createdRegion = await regionRepository.AddAsync(mapper.Map<Region>(region));
            var regionDTO = mapper.Map<RegionDTO>(createdRegion);

            // Ensure the Guid is valid and matches the GetByIdAsync route parameter
            return CreatedAtAction(
                nameof(GetById), // "GetByIdAsync"
                new { id = regionDTO.Id }, // This must exactly match the route parameter
                regionDTO
            );
        }
    }
}
