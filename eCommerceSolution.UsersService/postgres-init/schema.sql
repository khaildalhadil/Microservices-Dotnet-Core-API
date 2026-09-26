-- Users table for the Users microservice (Postgres).
-- Runs automatically on first Postgres container start (docker-entrypoint-initdb.d).
-- Columns are lowercase; Dapper maps them to the PascalCase entity
-- properties case-insensitively (userid -> UserID, personname -> PersonName).
CREATE TABLE IF NOT EXISTS users (
    userid     UUID PRIMARY KEY,
    email      VARCHAR(255),
    password   VARCHAR(255),
    personname VARCHAR(255),
    gender     VARCHAR(50)
);
