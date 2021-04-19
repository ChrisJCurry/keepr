using System.Collections.Generic;
using Models;
using Services;
using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfilesController : ControllerBase
    {
        private readonly ProfilesService _pservice;
        private readonly VaultsService _vService;
        private readonly KeepsService _kService;

        public ProfilesController(ProfilesService pservice, VaultsService vService, KeepsService kService)
        {
            _pservice = pservice;
            _vService = vService;
            _kService = kService;
        }

        [HttpGet("{id}")]
        public ActionResult<Profile> Get(string id)
        {
            try
            {
                return Ok(_pservice.GetProfileById(id));
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        // [HttpGet("{id}/blogs")]
        // public ActionResult<CreatorBlog> GetBlogs(string id)
        // {
        //     try
        //     {
        //         return Ok(_blogService.GetByCreatorId(id));
        //     }
        //     catch (System.Exception err)
        //     {
        //         return BadRequest(err.Message);
        //     }
        // }

        [HttpGet("{id}/vaults")]
        public ActionResult<Vault> GetVaults(string id)
        {
            try
            {
                return Ok(_vService.GetByCreatorId(id));
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [HttpGet("{id}/keeps")]
        public ActionResult<Vault> GetKeeps(string id)
        {
            try
            {
                return Ok(_kService.GetByCreatorId(id));
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            }
        }
    }
}