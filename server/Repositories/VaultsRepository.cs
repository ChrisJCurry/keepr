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

        internal IEnumerable<Vault> GetByCreatorId(string id)
        {
            string sql = @"
            SELECT
            vault.*,
            profile.*
            FROM vaults vault
            JOIN profiles profile ON vault.creatorId = profile.id
            WHERE vault.creatorId = @id;";
            return _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
            {
                vault.Creator = profile;
                return vault;
            }, new { id }, splitOn: "id");
        }

        public int Create(Vault newVault)
        {
            string sql = @"
            INSERT INTO vaults
            (creatorId, name, description, isPrivate)
            VALUES
            (@CreatorId, @Name, @Description, @IsPrivate);
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
                isPrivate = @IsPrivate
            WHERE id = @Id;";
            _db.Execute(sql, update);
            return update;
        }
    }
}