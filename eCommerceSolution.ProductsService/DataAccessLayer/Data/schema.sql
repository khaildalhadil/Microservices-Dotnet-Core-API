-- Products microservice — MySQL (DB-first).
-- Run this in MySQL Workbench to create the database, table, and seed data.
-- UnitPrice is DECIMAL(10,3) to hold Omani Rial (3 decimal places / baisa).

CREATE DATABASE IF NOT EXISTS ecommerceproductsdatabase;
USE ecommerceproductsdatabase;

CREATE TABLE IF NOT EXISTS products (
    ProductID       CHAR(36) PRIMARY KEY,
    ProductName     VARCHAR(255),
    Category        VARCHAR(100),
    UnitPrice       DECIMAL(10,3),
    QuantityInStock INT
);

-- Seed data (same rows as the tutorial; every UnitPrice set to 100.000 OMR).
INSERT INTO products (ProductID, ProductName, Category, UnitPrice, QuantityInStock) VALUES
('10d7b110-ecdb-4921-85a4-58a5d1b32bf4', 'PlayStation 5',                  'Electronics',    100.000, 40),
('11f2e86a-9d5d-42f9-b3c2-3e4d652e3df8', 'Executive Office Desk',          'Furniture',      100.000, 18),
('12b369b7-9101-41b1-a653-6c6c9a4fe1e4', 'Breville Smart Blender',         'HomeAppliances', 100.000, 50),
('1a9df78b-3f46-4c3d-9f2a-1b9f69292a77', 'Apple iPhone 15 Pro Max',        'Electronics',    100.000, 50),
('2c8e8e7c-97a3-4b11-9a1b-4dbe681cfe17', 'Samsung Foldable Smart Phone 2', 'Electronics',    100.000, 100),
('3f3e8b3a-4a50-4cd0-8d8e-1e178ae2cfc1', 'Ergonomic Office Chair',         'Furniture',      100.000, 25),
('4c9b6f71-6c5d-485f-8db2-58011a236b63', 'Coffee Table with Storage',      'Furniture',      100.000, 30),
('5d7e36bf-65c3-4a71-bf97-740d561d8b65', 'Samsung QLED 75 inch',           'Electronics',    100.000, 20),
('6a14f510-72c1-42c8-9a5a-8ef8f3f45a0d', 'Running Shoes',                  'Furniture',      100.000, 75),
('7b39ef14-932b-4c84-9187-55b748d2b28f', 'Anti-Theft Laptop Backpack',     'Accessories',    100.000, 60),
('8c5f6e73-68fc-49d9-99b4-aecc3706a4f4', 'LG OLED 65 inch',                'Electronics',    100.000, 15),
('9e7e7085-6f4e-4921-8f15-c59f084080f9', 'Modern Dining Table',            'Furniture',      100.000, 10);
