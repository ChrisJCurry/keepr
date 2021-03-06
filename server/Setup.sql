-- CREATE TABLE profiles
-- (
--     id VARCHAR(255) NOT NULL,
--     email VARCHAR(255) NOT NULL,
--     name VARCHAR(255) NOT NULL,
--     nickName VARCHAR(255) NOT NULL,
--     picture VARCHAR(255) NOT NULL,

--     PRIMARY KEY (id)
-- );
-- SET FOREIGN_KEY_CHECKS=1;
-- SET FOREIGN_KEY_CHECKS=0; doesnt care, clears all tables even if theres a foreign key check, turn to 1 to enable
-- DROP TABLE keeps;
-- CREATE TABLE keeps
-- (
--     id INT NOT NULL AUTO_INCREMENT,
--     creatorId VARCHAR(255) NOT NULL,
--     name VARCHAR(255) NOT NULL,
--     description VARCHAR(255) NOT NULL,
--     img VARCHAR(255) NOT NULL,
--     views INT NOT NULL DEFAULT 0,
--     shares INT NOT NULL DEFAULT 0,
--     keeps INT NOT NULL DEFAULT 0,

--     PRIMARY KEY (id),

--     FOREIGN KEY (creatorId) 
--         REFERENCES profiles (id)
--         ON DELETE CASCADE

-- );
-- SET FOREIGN_KEY_CHECKS=0;
-- DROP TABLE vaults;
-- CREATE TABLE vaults
-- (
--     id INT NOT NULL AUTO_INCREMENT,
--     creatorId VARCHAR(255) NOT NULL,
--     name VARCHAR(255) NOT NULL,
--     description VARCHAR(255) NOT NULL,
--     isPrivate TINYINT(1) NOT NULL,

--     PRIMARY KEY (id),

--     FOREIGN KEY (creatorId) 
--         REFERENCES profiles (id)
--         ON DELETE CASCADE

-- );
-- CREATE TABLE vaultkeeps
-- (
--     id INT NOT NULL AUTO_INCREMENT,
--     keepId INT,
--     vaultId INT,
--     creatorId VARCHAR(255) NOT NULL,

--     PRIMARY KEY (id),

--     FOREIGN KEY (keepId)
--         REFERENCES keeps (id)
--         ON DELETE CASCADE,

--     FOREIGN KEY (vaultId)
--         REFERENCES vaults (id)
--         ON DELETE CASCADE,

--     FOREIGN KEY (creatorId)
--         REFERENCES profiles (id)
--         ON DELETE CASCADE
-- );

SET FOREIGN_KEY_CHECKS=0;
TRUNCATE vaultkeeps;
TRUNCATE keeps;
TRUNCATE vaults;