using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ValidateModel]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDTO walkDTO)
        {
            var addedWalk = await walkRepository.AddAsync(mapper.Map<Walk>(walkDTO));
            var walkDTOResponse = mapper.Map<WalkDTO>(addedWalk);
            return CreatedAtAction(nameof(GetWalkById), new { id = walkDTOResponse.Id }, walkDTOResponse);
        }
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetWalkById([FromRoute] Guid id)
        {
            var walk = await walkRepository.GetByIdAsync(id);
            if (walk != null)
            {
                return Ok(mapper.Map<WalkDTO>(walk));
            }
            return NotFound();
        }
        [HttpGet]
        public async Task<IActionResult> GetAllWalks([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNumber=1, [FromQuery] int pageSize=1000 )
        {
            throw new Exception("test");
            var walks = await walkRepository.GetAllAsync(filterOn,filterQuery,sortBy,isAscending,pageNumber,pageSize);
            return Ok(mapper.Map<List<WalkDTO>>(walks));
        }
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWalkRequestDTO walkDTO)
        {
            var updatedWalk = await walkRepository.UpdateAsync(id, mapper.Map<Walk>(walkDTO));
            if (updatedWalk != null)
            {
                return Ok(mapper.Map<WalkDTO>(updatedWalk));
            }
            return NotFound();
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deletedWalk = await walkRepository.DeleteAsync(id);
            if (deletedWalk != null)
            {
                return Ok(mapper.Map<WalkDTO>(deletedWalk));
            }
            return NotFound();
        }
    }
}
