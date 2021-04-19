using System;
using System.Collections.Generic;
using Models;
using Repositories;

namespace Services
{
    public class VaultsService
    {
        private readonly VaultsRepository _vRepo;

        public VaultsService(VaultsRepository vRepo)
        {
            _vRepo = vRepo;
        }

        internal IEnumerable<Vault> Get()
        {
            IEnumerable<Vault> vaults = _vRepo.Get();
            return vaults;
        }

        internal Vault Get(int id)
        {
            return (_vRepo.Get(id));
        }

        internal Vault Create(Vault vault)
        {
            vault.Id = _vRepo.Create(vault);
            return vault;
        }

        internal Vault Edit(Vault vault, string userId)
        {
            Vault original = Get(vault.Id);
            if (userId != original.CreatorId)
            {
                throw new Exception("You are not the creator, you can't edit this.");
            }
            vault.Name = vault.Name == null ? original.Name : vault.Name;
            vault.Description = vault.Description == null ? original.Description : vault.Description;
            vault.IsPublic = vault.IsPublic != original.IsPublic ? vault.IsPublic : original.IsPublic;
            return (_vRepo.Edit(vault));
        }

        internal string Delete(int id, Profile userInfo)
        {
            Vault original = Get(id);
            if (userInfo.Id != original.CreatorId)
            {
                throw new Exception("You are not the creator, you can't delete this");
            }
            _vRepo.Delete(id);
            return ("Deleted");
        }

        internal IEnumerable<Vault> GetVaultsByAccountId(string id)
        {
            return _vRepo.GetByCreatorId(id);
        }
    }
}