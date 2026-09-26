// Seed data for OrdersDatabase.orders — prices in Omani Rial (OMR, 3 decimals)
// Runs automatically on first MongoDB container start (docker-entrypoint-initdb.d).
db = db.getSiblingDB("OrdersDatabase");

var orders = [
  {
    _id: "a1b2c3d4-0001-4a1b-9c1d-000000000001",
    OrderID: "a1b2c3d4-0001-4a1b-9c1d-000000000001",
    UserID: "e3b0c442-98fc-4c14-9afb-f4c8996fb924",
    OrderDate: new Date("2026-09-01T10:15:00Z"),
    TotalBill: 546.700,
    OrderItems: [
      { _id: "b1000000-0001-4000-8000-000000000001", ProductID: "1a9df78b-3f46-4c3d-9f2a-1b9f69292a77", UnitPrice: 500.500, Quantity: 1, TotalPrice: 500.500 }, // iPhone 15 Pro Max
      { _id: "b1000000-0001-4000-8000-000000000002", ProductID: "7b39ef14-932b-4c84-9187-55b748d2b28f", UnitPrice: 23.100, Quantity: 2, TotalPrice: 46.200 }    // Laptop Backpack
    ]
  },
  {
    _id: "a1b2c3d4-0002-4a1b-9c1d-000000000002",
    OrderID: "a1b2c3d4-0002-4a1b-9c1d-000000000002",
    UserID: "5f2d7c1a-3b8e-4d6f-a9c2-1e4b7d9f0a36",
    OrderDate: new Date("2026-09-05T14:30:00Z"),
    TotalBill: 770.000,
    OrderItems: [
      { _id: "b1000000-0002-4000-8000-000000000001", ProductID: "8c5f6e73-68fc-49d9-99b4-aecc3706a4f4", UnitPrice: 577.500, Quantity: 1, TotalPrice: 577.500 }, // LG OLED 65
      { _id: "b1000000-0002-4000-8000-000000000002", ProductID: "10d7b110-ecdb-4921-85a4-58a5d1b32bf4", UnitPrice: 192.500, Quantity: 1, TotalPrice: 192.500 }  // PlayStation 5
    ]
  },
  {
    _id: "a1b2c3d4-0003-4a1b-9c1d-000000000003",
    OrderID: "a1b2c3d4-0003-4a1b-9c1d-000000000003",
    UserID: "e3b0c442-98fc-4c14-9afb-f4c8996fb924",
    OrderDate: new Date("2026-09-10T09:00:00Z"),
    TotalBill: 308.000,
    OrderItems: [
      { _id: "b1000000-0003-4000-8000-000000000001", ProductID: "3f3e8b3a-4a50-4cd0-8d8e-1e178ae2cfc1", UnitPrice: 96.250, Quantity: 2, TotalPrice: 192.500 },  // Office Chair
      { _id: "b1000000-0003-4000-8000-000000000002", ProductID: "11f2e86a-9d5d-42f9-b3c2-3e4d652e3df8", UnitPrice: 115.500, Quantity: 1, TotalPrice: 115.500 }  // Office Desk
    ]
  },
  {
    _id: "a1b2c3d4-0004-4a1b-9c1d-000000000004",
    OrderID: "a1b2c3d4-0004-4a1b-9c1d-000000000004",
    UserID: "9c4e2a7b-6d1f-4e8a-b3c5-7f0d2e9a4b18",
    OrderDate: new Date("2026-09-15T18:45:00Z"),
    TotalBill: 57.750,
    OrderItems: [
      { _id: "b1000000-0004-4000-8000-000000000001", ProductID: "6a14f510-72c1-42c8-9a5a-8ef8f3f45a0d", UnitPrice: 19.250, Quantity: 3, TotalPrice: 57.750 }    // Running Shoes
    ]
  },
  {
    _id: "a1b2c3d4-0005-4a1b-9c1d-000000000005",
    OrderID: "a1b2c3d4-0005-4a1b-9c1d-000000000005",
    UserID: "5f2d7c1a-3b8e-4d6f-a9c2-1e4b7d9f0a36",
    OrderDate: new Date("2026-09-20T12:00:00Z"),
    TotalBill: 415.800,
    OrderItems: [
      { _id: "b1000000-0005-4000-8000-000000000001", ProductID: "9e7e7085-6f4e-4921-8f15-c59f084080f9", UnitPrice: 269.500, Quantity: 1, TotalPrice: 269.500 }, // Dining Table
      { _id: "b1000000-0005-4000-8000-000000000002", ProductID: "4c9b6f71-6c5d-485f-8db2-58011a236b63", UnitPrice: 69.300, Quantity: 1, TotalPrice: 69.300 },   // Coffee Table
      { _id: "b1000000-0005-4000-8000-000000000003", ProductID: "12b369b7-9101-41b1-a653-6c6c9a4fe1e4", UnitPrice: 38.500, Quantity: 2, TotalPrice: 77.000 }    // Smart Blender
    ]
  }
];

db.orders.insertMany(orders);
