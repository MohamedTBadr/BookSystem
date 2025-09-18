using BookReview.Data.DTO.AuthorDTOs;
using BookReview.Services.Common;
using BookReview.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
//using System.Web.Http;

namespace BookReview.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController(IAuthorService authorService):ControllerBase
    {
        [RedisCache(120)]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result= await authorService.GetAllAsync();
            if (!result.IsSuccess)
            {
                return BadRequest();
            }
            return Ok(result);
        }
        [RedisCache(120)]
        [HttpGet("id")]
        public async Task<IActionResult> GetByIdAsync(Guid Id)
        {
            var result=await authorService.GetByIdAsync(Id);
            if (!result.IsSuccess)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        public async Task<IActionResult> AddAsync([FromBody]AuthorCreateDTO author)
        {
            var result = await authorService.CreateAuthorAsync(author);
            if (!result.IsSuccess) return BadRequest(result);
            //return Created();
            return NoContent();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateAsync([FromBody] AuthorUpdateDTO author)
        {
            if (!ModelState.IsValid) return BadRequest();

            var result = authorService.UpdateAuthorAsync(author);
            

            return NoContent();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await authorService.DeleteAuthorAsync(id);

            return NoContent();
        }
       
    }
}
