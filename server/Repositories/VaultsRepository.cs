using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Models;

namespace Repositories
{
    public class VaultsRepository
    {
        private readonly IDbConnection _db;

        public VaultsRepository(IDbConnection db)
        {
            _db = db;
        }

        public IEnumerable<Vault> Get()
        {
            string sql = @"
            SELECT
            vault.*,
            profile.*
            FROM vaults vault
            JOIN profiles profile ON vault.creatorId = profile.id;";
            return _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
            {
                vault.Creator = profile;
                return vault;
            }, splitOn: "id");
        }

        public Vault Get(int id)
        {
            string sql = @"
            SELECT
            vault.*,
            profile.*
            FROM vaults vault
            JOIN profiles profile ON vault.creatorId = profile.id
            WHERE vault.id = @id;";
            return _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
            {
                vault.Creator = profile;
                return vault;
            }, new { id }, splitOn: "id").FirstOrDefault();
        }

        public int Create(Vault newVault)
        {
            string sql = @"
            INSERT INTO vaults
            (creatorId, name, description, isPublic)
            VALUES
            (@CreatorId, @Name, @Description, @IsPublic);
            SELECT LAST_INSERT_ID();";
            return (_db.ExecuteScalar<int>(sql, newVault));
        }
        internal void Delete(int id)
        {
            string sql = "DELETE FROM vaults WHERE id = @id LIMIT 1;";
            _db.Execute(sql, new { id });
            return;
        }

        public Vault Edit(Vault update)
        {
            string sql = @"
            UPDATE vaults
            SET
                name = @Name,
                description = @Description,
                isPublic = @IsPublic
            WHERE id = @Id;";
            _db.Execute(sql, update);
            return update;
        }
    }
}