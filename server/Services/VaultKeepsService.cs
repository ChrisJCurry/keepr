using System;
using System.Threading.Tasks;
using Models;
using Repositories;

namespace Services
{
    public class VaultKeepsService
    {

        private readonly VaultKeepsRepository _vkRepo;

        public VaultKeepsService(VaultKeepsRepository vkRepo)
        {
            _vkRepo = vkRepo;
        }

        internal VaultKeep CreateAsync(VaultKeep newVK, string userId)
        {

            return _vkRepo.Create(newVK);
        }
    }
}