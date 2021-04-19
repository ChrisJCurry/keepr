using System;
using System.Collections.Generic;
using Models;
using Repositories;

namespace Services
{
    public class KeepsService
    {
        private readonly KeepsRepository _kRepo;

        public KeepsService(KeepsRepository kRepo)
        {
            _kRepo = kRepo;
        }

        internal IEnumerable<Keep> Get()
        {
            IEnumerable<Keep> keeps = _kRepo.Get();
            return keeps;
        }

        internal Keep Get(int id)
        {
            return (_kRepo.Get(id));
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

        internal string Delete(int id, Profile userInfo)
        {
            Keep original = Get(id);
            if (userInfo.Id != original.CreatorId)
            {
                throw new Exception("You are not the creator, you can't delete this");
            }
            _kRepo.Delete(id);
            return ("Deleted");
        }

        internal IEnumerable<VaultKeepModel> GetByVault(int id)
        {
            return _kRepo.GetByVault(id);
        }

        internal IEnumerable<Keep> GetKeepsByAccountId(string id)
        {
            return _kRepo.GetByCreatorId(id);
        }
    }
}