-- Products table for the Products microservice (MySQL).
CREATE TABLE IF NOT EXISTS products (
    ProductID       CHAR(36) PRIMARY KEY,
    ProductName     VARCHAR(255),
    Category        VARCHAR(100),
    UnitPrice       DOUBLE,
    QuantityInStock INT
);
