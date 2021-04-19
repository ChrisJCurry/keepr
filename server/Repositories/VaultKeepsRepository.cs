using System;
using System.Data;
using Dapper;
using Models;

namespace Repositories
{
    public class VaultKeepsRepository
    {

        private readonly IDbConnection _db;

        public VaultKeepsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal VaultKeep Create(VaultKeep newVK)
        {
            string sql = @"
            INSERT INTO vaultkeeps
            (keepId, vaultId, creatorId)
            VALUES
            (@KeepId, @VaultId, @CreatorId);
            SELECT LAST_INSERT_ID();";
            int id = _db.ExecuteScalar<int>(sql, newVK);
            newVK.Id = id;
            return newVK;
        }

        internal void Delete(int vaultKeepId)
        {
            string sql = "DELETE FROM vaultkeeps WHERE id = @vaultKeepId LIMIT 1;";
            _db.Execute(sql, new { vaultKeepId });
            return;
        }
    }
}