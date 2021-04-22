using System;
using System.Collections.Generic;
using Models;
using Repositories;

namespace Services
{
    public class KeepsService
    {
        private readonly KeepsRepository _kRepo;

        private readonly VaultKeepsRepository _vkRepo;
        private readonly VaultsService _vService;

        public KeepsService(KeepsRepository kRepo, VaultKeepsRepository vkRepo, VaultsService vService)
        {
            _kRepo = kRepo;
            _vkRepo = vkRepo;
            _vService = vService;
        }

        internal IEnumerable<Keep> Get()
        {
            IEnumerable<Keep> keeps = _kRepo.Get();
            return keeps;
        }

        internal Keep Get(int id)
        {
            Keep curKeep = _kRepo.Get(id);
            if (curKeep == null)
            {
                throw new Exception("This doesn't exist.");
            }


            return (curKeep);
        }

        internal Keep Create(Keep keep)
        {
            keep.Id = _kRepo.Create(keep);
            return keep;
        }

        internal Keep Edit(Keep keep, string userId)
        {
            Keep original = Get(keep.Id);
            if (userId != original.CreatorId)
            {
                throw new Exception("You are not the creator, you can't edit this.");
            }
            keep.Name = keep.Name == null ? original.Name : keep.Name;
            keep.Description = keep.Description == null ? original.Description : keep.Description;
            keep.Img = keep.Img == null ? original.Img : keep.Img;
            return (_kRepo.Edit(keep));
        }

        internal string Delete(int id, string userId)
        {
            Keep original = Get(id);
            if (userId != original.CreatorId)
            {
                throw new Exception("You are not the creator, you can't delete this");
            }
            if (original == null)
            {
                throw new Exception("You can't delete something that's already deleted.");
            }
            _kRepo.Delete(id);
            return "deleted";
        }

        internal IEnumerable<VaultKeepModel> GetByVault(int id, string userId)
        {
            Vault curVault = _vService.Get(id, userId);
            if (curVault == null)
            {
                throw new Exception("Could not find this vault.");
            }
            if (curVault.IsPrivate == true && userId != curVault.CreatorId)
            {
                throw new Exception("You don't have access to this.");
            }
            return _vkRepo.GetByVaultId(id);
        }

        internal IEnumerable<Keep> GetKeepsByAccountId(string id)
        {
            return _kRepo.GetByCreatorId(id);
        }
        internal IEnumerable<Keep> GetByCreatorId(string id)
        {
            IEnumerable<Keep> keeps = _kRepo.GetByCreatorId(id);
            return keeps;
        }
    }
}