using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CodeWorks.Auth0Provider;
using Services;
using Models;
using System.Collections.Generic;

namespace Controllers
{
    [ApiController]
    [Route("[controller]")]

    // REVIEW this tag enforces the user must be logged in
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly ProfilesService _pservice;
        private readonly KeepsService _kService;

        public AccountController(ProfilesService pservice, KeepsService kService)
        {
            _pservice = pservice;
            _kService = kService;
        }

        [HttpGet]
        // REVIEW async calls must return a System.Threading.Tasks, this is equivalent to a promise in JS
        public async Task<ActionResult<Profile>> GetAsync()
        {
            try
            {
                // REVIEW how to get the user info from the request token
                // same as to req.userInfo
                //MAKE SURE TO BRING IN codeworks.auth0provider
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                return Ok(_pservice.GetOrCreateProfile(userInfo));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("keeps")]

        public async Task<ActionResult<IEnumerable<Keep>>> GetKeepsByAccountId()
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                return Ok(_kService.GetKeepsByAccountId(userInfo.Id));
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}