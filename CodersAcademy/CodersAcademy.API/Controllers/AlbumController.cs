using AutoMapper;
using CodersAcademy.API.Model;
using CodersAcademy.API.Repository;
using CodersAcademy.API.ViewModel.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodersAcademy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {

        public AlbumRepository Repository { get; set; }

        public IMapper Mapper { get; set; }

        public AlbumController(AlbumRepository repository, IMapper mapper)
        {
            Repository = repository;
            Mapper = mapper;
        }

        [HttpGet]      
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAlbuns()
        {
            return Ok(await this.Repository.GetAllAsync());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAlbum(Guid id)
        {
            var result = await this.Repository.GetAlbumByIdAsync(id);

            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);

            // Somente C#9 -> .Net 5
            //return result is not null ? Ok(result) : NotFound();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> SaveAlbuns([FromBody] AlbumRequest request)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            //Album album = new Album()
            //{
            //    Backdrop = request.Backdrop,
            //    Band = request.Band,
            //    Description = request.Description,
            //    Name = request.Name
            //};

            Album album = this.Mapper.Map<Album>(request);
            await this.Repository.CreateAsync(album);

            return Created($"/{album.Id}", album);


        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteAlbum(Guid id)
        {
            var result = await this.Repository.GetAlbumByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            await this.Repository.DeleteAsync(result);

            return NoContent();
        }
    }
}
