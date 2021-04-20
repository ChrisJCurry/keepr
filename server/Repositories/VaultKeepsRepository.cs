using System;
using System.Collections.Generic;
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
            UPDATE keeps
            SET
                keeps = keeps + 1
            WHERE id = @KeepId;
            SELECT LAST_INSERT_ID();";
            int id = _db.ExecuteScalar<int>(sql, newVK);
            newVK.Id = id;
            return newVK;
        }

        internal VaultKeep Get(int id)
        {
            string sql = "SELECT * FROM vaultkeeps WHERE id = @id;";
            return _db.QueryFirstOrDefault<VaultKeep>(sql, new { id });
        }

        internal IEnumerable<VaultKeep> GetByVaultId(int id)
        {
            string sql = @"
            SELECT
            vk.*,
            profile.*
            FROM vaultkeeps vk 
            JOIN profiles profile ON vk.creatorId = profile.id
            WHERE vk.vaultId = @id;";
            return _db.Query<VaultKeep, Profile, VaultKeep>(sql, (vk, profile) =>
            {
                return vk;
            }, new { id }, splitOn: "id");
        }

        internal void Delete(int vaultKeepId, int keepId)
        {
            string sql = @"
            DELETE FROM 
            vaultkeeps 
            WHERE id = @vaultKeepId LIMIT 1;
            UPDATE keeps
            SET
                keeps = keeps - 1
            WHERE id = @keepId;";
            _db.Execute(sql, new { vaultKeepId, keepId });
            return;
        }
    }
}