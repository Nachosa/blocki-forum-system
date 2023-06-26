using AutoMapper;
using ForumSystem.Api.QueryParams;
using ForumSystem.Business;
using ForumSystem.Business.AuthenticationManager;
using ForumSystem.Business.TagService;
using ForumSystem.Business.UserService;
using ForumSystem.DataAccess.Exceptions;
using ForumSystem.DataAccess.Models;
using ForumSystem.DataAccess.QueryParams;
using ForumSystemDTO.PostDTO;
using ForumSystemDTO.TagDTO;
using Microsoft.AspNetCore.Mvc;

namespace ForumSystem.Api.Controllers
{
    [ApiController]
    [Route("api/tags")]
    public class TagApiController : ControllerBase
    {
        private readonly ITagService tagService;
        private readonly IAuthManager authManager;
        private readonly IMapper mapper;

        public TagApiController(ITagService tagService, IAuthManager authManager, IMapper mapper)
        {
            this.authManager = authManager;
            this.tagService = tagService;
            this.mapper = mapper;
        }

        //Не е нужен логин, това окей ли е? Не знам.
        [HttpGet("")]
        public IActionResult GetTags([FromQuery] TagQueryParameters queryParams)
        {
            var tags = tagService.GetTags(queryParams);
            var mappedTags = tags.Select(tag => mapper.Map<TagDto>(tag)).ToList();
            return StatusCode(StatusCodes.Status200OK, mappedTags);
        }

        [HttpGet("{id}")]
        public IActionResult GetTagById(int id)
        {
            try
            {
                var tag = tagService.GetTagById(id);
                var mappedTag = mapper.Map<TagDto>(tag);
                return StatusCode(StatusCodes.Status200OK, mappedTag);
            }
            catch (EntityNotFoundException e)
            {
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
        }

        [HttpPost("")]
        public IActionResult CreateTag([FromHeader] string credentials, [FromBody] TagDto tagDto)
        {
            try
            {
                authManager.BlockedCheck(credentials);

                var tag = mapper.Map<Tag>(tagDto);
                tagService.CreateTag(tag);
                return StatusCode(StatusCodes.Status200OK, tagDto);
            }
            catch (UnauthenticatedOperationException e)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
        }

        [HttpPatch("{id}")]
        public IActionResult UpdateTagName(int id, [FromBody] TagDto tagDto, [FromHeader] string credentials)
        {
            try
            {
                authManager.BlockedCheck(credentials);
                string[] userInfo = credentials.Split(':');
                string username = userInfo[0];

                var mappedTag = mapper.Map<Tag>(tagDto);
                var updatedTag = tagService.UpdateTagName(id, mappedTag, username);
                var updatedTagDto = mapper.Map<TagDto>(updatedTag);
                return StatusCode(StatusCodes.Status200OK, updatedTagDto);
            }
            catch (UnauthenticatedOperationException e)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
            catch (EntityNotFoundException e)
            {
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
        }

        //Всеки може да трие, който и да е таг?
        //Трябва или да е само за админи или всеки таг да си има юзър.
        [HttpDelete("{id}")]
        public IActionResult DeleteTagById(int id)
        {
            try
            {
                var isDeleted = tagService.DeleteTagById(id);
                return StatusCode(StatusCodes.Status200OK, isDeleted);
            }
            catch (EntityNotFoundException e)
            {
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
        }
    }
}
