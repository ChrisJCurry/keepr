using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;
using Repositories;

namespace Services
{
    public class VaultKeepsService
    {

        private readonly VaultKeepsRepository _vkRepo;
        private readonly VaultsService _vService;
        private readonly KeepsService _kService;

        public VaultKeepsService(VaultKeepsRepository vkRepo, VaultsService vService, KeepsService kService)
        {
            _vkRepo = vkRepo;
            _vService = vService;
            _kService = kService;
        }

        internal VaultKeep CreateAsync(VaultKeep newVK, string userId)
        {
            List<VaultKeep> vk = _vkRepo.GetByVaultId(newVK.VaultId).ToList();
            if (vk.Any())
            {
                throw new Exception("You already added this.");
            }
            return _vkRepo.Create(newVK);
        }

        internal string DeleteAsync(int vaultKeepId, string userId)
        {
            VaultKeep originalVK = _vkRepo.Get(vaultKeepId);
            Vault original = _vService.Get(originalVK.VaultId, userId);
            Keep originalKeep = _kService.Get(originalVK.KeepId);
            Console.WriteLine("yo" + originalKeep.Id);
            if (original.IsPrivate && userId != original.CreatorId)
            {
                throw new Exception("You can't access this.");
            }
            _vkRepo.Delete(vaultKeepId, originalKeep.Id);
            return "deleted";
        }
    }
}