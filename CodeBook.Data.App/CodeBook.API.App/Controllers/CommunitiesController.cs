using CodeBook.API.App.Controllers;
using CodeBook.Business.App.DTOs;
using CodeBook.Business.App.Interfaces;
using CodeBook.Models.App;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

namespace CodeBook.Business.App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class CommunitiesController : ControllerBase
    {
        private readonly ICommunityService _communityService;
        private readonly CurrentUserInfo _currentUserInfo = new CurrentUserInfo();


        public CommunitiesController(ICommunityService communityService)
        {
            _communityService = communityService;
        }
        [HttpPost]
        public IActionResult CreateCommunity([FromBody] CreateCommunityDto dto) {
            try
            {
                var userId = _currentUserInfo.GetCurrentUserId();
                if (string.IsNullOrEmpty(dto.Name))
                    return BadRequest("Community name cannot be empty.");

                _communityService.CreateCommunity(dto,userId);
                return Ok("Community Created Successfully");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex) { 
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPut("{id}")]
        public IActionResult UpdateCommunity(int id, [FromBody] UpdateCommunityDto dto)
        {
            try
            {
                if (string.IsNullOrEmpty(dto.Name))
                    return BadRequest("Community name cannot be empty.");
                _communityService.UpdateCommunity(id, dto);
                return Ok("Community Updated Successfully");
            }
            catch (KeyNotFoundException ex) {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex) {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteCommunity(int id)
        {
            try {
                _communityService.DeleteCommunity(id);
                return Ok("Community Deleted Successfully");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost("{id}/join")]
        public IActionResult JoinCommunity(int id, [FromBody] JoinCommunityDto dto) {
            try {
                var member = new CommunityMember
                {
                    CommunityId = id,
                    UserId = _currentUserInfo.GetCurrentUserId(),
                    Role = dto.Role,
                    JoinedAt = DateTime.UtcNow
                };
                _communityService.JoinCommunity(id, member);
                return Ok("Joined Community Successfully");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }
        [HttpPost("{id}/role")]
        public IActionResult AssignRole(int id, [FromBody] AssignRoleDto dto)
        {
            try {
                _communityService.AssignRole(id, dto);
                return Ok("Role Assigned Successfully");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetCommunity(int id)
        {
            try
            {
                Community community = _communityService.GetCommunity(id);
                return Ok(community);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpDelete("{id}/unjoin")]
        public IActionResult UnjoinCommunity(int id)
        {
            try
            {
                var userId = _currentUserInfo.GetCurrentUserId();
                _communityService.UnjoinCommunity(id, userId);
                return Ok("Unjoined Community Successfully");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
