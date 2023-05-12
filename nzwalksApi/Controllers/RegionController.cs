using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NZWalksApi.DTOs;
using NZWalksApi.Model.Domain;
using NZWalksApi.Repository;
using System.Linq.Expressions;

namespace NZWalksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly IRepository _repository;
        public RegionController(IRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // get data from DataBase - Domain Model
            var regionList = await _repository.GetAllAsync();

            // Map Domain Model to DtoRegion Data
            var regionsDtos = new List<DtoRegion>();
            foreach (var region in regionList)
            {
                regionsDtos.Add(new DtoRegion
                {
                    Id = region.Id,
                    Code = region.Code,
                    Name = region.Name,
                    RegionImageUrl = region.RegionImageUrl,
                });
            }
            // Return DTOs
            return Ok(regionsDtos);

        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {

            // Get Region Domain Model Object by passing ID
            var regionsData = await _repository.GetByIdAsync(id);

            // Cheak weather data exist OR Not 
            if(regionsData == null) { 
                return NotFound();
            
            }

            // Creting New DTORegino Object and Map Domain Model into DTORegion
            DtoRegion dtoRegion = new DtoRegion {
                Id = regionsData.Id,
                Code = regionsData.Code,
                Name = regionsData.Name,
                RegionImageUrl = regionsData.RegionImageUrl,
            
            };

            // Return DTORegion Object

            return Ok(dtoRegion);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRequestRegionDto dto) 
        {
            // Create Region Object and Convert CreateRequestRegionDto Object to Domain Model
            var regionDomainModel = new Region
            {
                Code = dto.Code,
                Name = dto.Name,
                RegionImageUrl = dto.imageUrl
            };

            // Add regionDomainModel to the Database By calling repository method called CreateRegionAsync that take Region object

            regionDomainModel  = await _repository.CreateRegionAsync(regionDomainModel);


            //Convert newlly added region Object into DtoRegion object So that we can return that object to the client


            var regionDto = new DtoRegion
            {

                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl,

            };

            // Return DtoRegion Object with CreateAtAction Action

            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRequestRegionDto dto)
        {
            //Create New Region Object and Convert UpdateRequestRegionDto object into Region Object 
            var Region = new Region
            {
                Code = dto.Code,
                Name = dto.Name,
                RegionImageUrl = dto.ImageUrl
            };
            

            //We are Calling Repository UpdateRegionAsync Method for Updating data by Passing Id and Region Object

            Region = await _repository.UpdateRegionAsync(id, Region);

            //Cheak Region Object Exist OR Not 
            if (Region == null)
            {
                return NotFound();
            }

            //Now we are going to Convert Region Object to DtoRgion Object  So that we Can Pass to client 

            var dtoRegion = new DtoRegion
            {
                Id = Region.Id,
                Code = Region.Code,
                Name = Region.Name,
                RegionImageUrl = Region.RegionImageUrl,
            };

            //Now we will Return DtoRegion Object to the Client with Successfull Ok Or 200 Message

            return Ok(dtoRegion);

              
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            // Creation RegionData and assign into  it region Object that DeleteregionAsync Method Return 
            // DeleteRegionAsync Take Id as a Parameter OF type Guid
            var regionData = await _repository.DeleteRegionAsync(id);

            // Cheaking that DeleteReginoAsync Method return Null or Object of type Region
            if (regionData == null)
            {
                return NotFound();
            }

            //Convetin Domain Model region Object into DtoRegion Object 
            // Here we regionData variable of Type Region

            var regionDto = new DtoRegion
            {
                Id = regionData.Id,
                Code = regionData.Code,
                Name = regionData.Name,
                RegionImageUrl = regionData.RegionImageUrl,
            };

            // Return regionDto Object with Ok massage

            return Ok(regionDto);

        }
    }
}
