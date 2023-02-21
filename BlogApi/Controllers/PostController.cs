using AutoMapper;
using BlogApi.Dto;
using BlogApi.Interfaces;
using BlogApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController: Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public PostController(IPostRepository postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Post>))]
        public IActionResult GetPosts()
        {
            var posts = _mapper.Map<List<PostDto>>(_postRepository.GetPosts());
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(posts);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Post))]
        [ProducesResponseType(400)]
        public IActionResult GetPost(int id)
        {
            if(!_postRepository.PostExists(id)) 
                return NotFound();

            var post = _mapper.Map<PostDto>(_postRepository.GetPost(id));

            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(post);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreatePost([FromBody] PostDto postDto)
        {
            if(postDto == null)
                return BadRequest(ModelState);

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var postMap = _mapper.Map<Post>(postDto);

            if (!_postRepository.CreatePost(postMap))
            {
                ModelState.AddModelError("", "Creating post is failed");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created!");
        }

        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdatePost(int id, [FromBody] PostDto postDto)
        {
            if(postDto ==null)
                return BadRequest(ModelState);
            if(id != postDto.Id)
                return BadRequest(ModelState);
            if (!_postRepository.PostExists(id))
                return NotFound();
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var postMap = _mapper.Map<Post>(postDto);

            if (!_postRepository.UpdatePost(postMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating.");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeletePost(int id)
        {
            if (!_postRepository.PostExists(id))
                return NotFound();

            var post = _postRepository.GetPost(id);

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_postRepository.DeletePost(post))
            {
                ModelState.AddModelError("", "Something went wrong while deleting.");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

    }
}
