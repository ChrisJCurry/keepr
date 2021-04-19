using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VaultsController : ControllerBase
    {
        private readonly VaultsService _vService;
        private readonly KeepsService _kService;

        public VaultsController(VaultsService vService, KeepsService kService)
        {
            _vService = vService;
            _kService = kService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Vault>> Get()
        {
            try
            {
                return Ok(_vService.Get());
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Vault> Get(int id)
        {
            try
            {
                return Ok(_vService.Get(id));
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Vault>> CreateAsync([FromBody] Vault vault)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                vault.CreatorId = userInfo.Id;
                vault.Creator = userInfo;
                Vault created = _vService.Create(vault);
                return Ok(created);
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Vault>> EditAsync(int id, [FromBody] Vault vault)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                vault.Id = id;
                vault.CreatorId = userInfo.Id;
                vault.Creator = userInfo;
                return Ok(_vService.Edit(vault, userInfo.Id));
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Vault>> DeleteAsync(int id)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                return Ok(_vService.Delete(id, userInfo));
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [HttpGet("{id}/keeps")]
        public ActionResult<Vault> GetKeepsByVault(int id)
        {
            try
            {
                return Ok(_kService.GetByVault(id));
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            }
        }
    }
}