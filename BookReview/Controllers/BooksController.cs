 using BookReview.Data.DTO.BooksDTOs;
using BookReview.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookReview.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class BooksController(IBookService bookService):ControllerBase
    {
     

        [RedisCache(120)]
        [HttpGet] 
        public async Task<IActionResult> GetAll([FromQuery]BookParametersSpecification parametersSpecification)
        {
            var result=await bookService.GetAll(parametersSpecification);
            if (!result.IsSuccess)
            {
              return  BadRequest(new { message = result.Message });
            }

            return Ok(result);
        }

        [RedisCache(120)] 
        [HttpGet("id")]
        public async Task<IActionResult> GetByBookId(Guid id)
        {
            var result = await bookService.GetById(id);
            if (!result.IsSuccess)
            {
                return BadRequest(new { Message = result.Message });
            }

            return Ok(result);
        }

         

        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] BookCreateDTO Book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result =await bookService.Add(Book);
            if (!result.IsSuccess) return BadRequest(result);
            return Ok(result);
        }


        [HttpPut]
        public async Task<IActionResult> UpdateBook(BookUpdateDTO updatedBook)
        {
            if (!ModelState.IsValid) {
                return BadRequest();
            }

          await bookService.Update(updatedBook);
            return Ok();
        }



        [HttpDelete]
        public async Task<IActionResult> DeleteBook(Guid id) {
            if (!ModelState.IsValid) { return BadRequest(); }
            await bookService.Delete(id);
            return Ok();
        
        }
    }
}
