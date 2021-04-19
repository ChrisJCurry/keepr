using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Models;

namespace Repositories
{
    public class KeepsRepository
    {
        private readonly IDbConnection _db;

        public KeepsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal IEnumerable<Keep> Get()
        {
            string sql = @"
            SELECT
            keep.creatorId, keep.img, keep.name, keep.id,
            profile.*
            FROM keeps keep
            JOIN profiles profile ON keep.creatorId = profile.id;";
            return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
            {
                keep.Creator = profile;
                return keep;
            }, splitOn: "id");
        }

        internal Keep Get(int id)
        {
            string sql = @"
            UPDATE keeps
            SET
                views = views + 1
            WHERE id = @id;
            SELECT
            keep.*,
            profile.*
            FROM keeps keep
            JOIN profiles profile ON keep.creatorId = profile.id
            WHERE keep.id = @id;";
            return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
            {
                keep.Creator = profile;
                return keep;
            }, new { id }, splitOn: "id").FirstOrDefault();
        }

        internal int Create(Keep newKeep)
        {
            string sql = @"
            INSERT INTO keeps
            (creatorId, name, description, img)
            VALUES
            (@CreatorId, @Name, @Description, @Img);
            SELECT LAST_INSERT_ID();";
            return (_db.ExecuteScalar<int>(sql, newKeep));
        }

        internal void Delete(int id)
        {
            string sql = "DELETE FROM keeps WHERE id = @id LIMIT 1;";
            _db.Execute(sql, new { id });
            return;
        }

        internal IEnumerable<Keep> GetByCreatorId(string id)
        {
            string sql = @"
            SELECT
            keep.*,
            profile.*
            FROM keeps keep
            JOIN profiles profile ON keep.creatorId = profile.id
            WHERE keep.creatorId = @id;";
            return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
            {
                keep.Creator = profile;
                return keep;
            }, new { id }, splitOn: "id");
        }

        internal Keep Edit(Keep update)
        {
            string sql = @"
            UPDATE keeps
            SET
                name = @Name,
                description = @Description
            WHERE id = @Id;";
            _db.Execute(sql, update);
            return update;
        }

        internal IEnumerable<VaultKeepModel> GetByVault(int id)
        {
            string sql = @"
            SELECT
            k.*,
            vk.id AS VaultKeepId
            FROM VaultKeeps vk
            JOIN keeps k ON k.id = vk.keepId
            WHERE vaultId = @id;";
            return _db.Query<VaultKeepModel>(sql, new { id });
        }
    }
}