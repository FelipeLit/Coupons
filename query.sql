-- Active: 1718397538418@@belrhullbfv4pio8rcfu-mysql.services.clever-cloud.com@3306@belrhullbfv4pio8rcfu
-- Tabla CouponUsages
CREATE TABLE CouponUsages (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    CouponId INT NOT NULL,
    MarketplaceUserId INT NOT NULL,
    UseDate DATETIME NOT NULL,
    FOREIGN KEY (CouponId) REFERENCES Coupons(Id),
    FOREIGN KEY (MarketplaceUserId) REFERENCES MarketplaceUsers(Id)
);

-- Tabla Coupons
CREATE TABLE Coupons (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Name VARCHAR(255) NOT NULL,
    Description VARCHAR(1000),
    StartDate DATETIME NOT NULL,
    EndDate DATETIME NOT NULL,
    DiscountType ENUM("Percentage", "Net") NOT NULL,
    IsLimited BOOLEAN NOT NULL,
    UsageLimit INT NOT NULL,
    AmountUses INT NOT NULL,
    MinPurchaseAmount DECIMAL(18, 2) NOT NULL,
    MaxPurchaseAmount DECIMAL(18, 2) NOT NULL,
    Status ENUM("Inactive", "Active") NOT NULL,
    MarketingUserId INT NOT NULL,
    FOREIGN KEY (MarketingUserId) REFERENCES MarketingUsers(Id)
);

-- Tabla PurchaseCoupon
CREATE TABLE PurchaseCoupon (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    PurchaseId INT NOT NULL,
    CouponId INT NOT NULL,
    FOREIGN KEY (PurchaseId) REFERENCES Purchases(Id),
    FOREIGN KEY (CouponId) REFERENCES Coupons(Id)
);

-- Tabla CouponHistory
CREATE TABLE CouponHistory (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    CouponId INT NOT NULL,
    ChangeDate DATETIME NOT NULL,
    OldValue VARCHAR(1000),
    NewValue VARCHAR(1000),
    FOREIGN KEY (CouponId) REFERENCES Coupons(Id)
);

-- Tabla Purchases
CREATE TABLE Purchases (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    MarketplaceUserId INT NOT NULL,
    ProductId INT NOT NULL,
    Date DATETIME NOT NULL,
    Amount DECIMAL(18, 2) NOT NULL,
    Discount DECIMAL(18, 2) NOT NULL,
    Total DECIMAL(18, 2) NOT NULL,
    FOREIGN KEY (MarketplaceUserId) REFERENCES MarketplaceUsers(Id),
    FOREIGN KEY (ProductId) REFERENCES Products(Id)
);

-- Tabla MarketplaceUsers
CREATE TABLE MarketplaceUsers (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Username VARCHAR(255) NOT NULL,
    Password VARCHAR(255) NOT NULL,
    Email VARCHAR(255) NOT NULL
);

-- Tabla Products
CREATE TABLE Products (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Name VARCHAR(255) NOT NULL,
    Price DECIMAL(18, 2) NOT NULL
);

-- Tabla MarketingUsers
CREATE TABLE MarketingUsers (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Username VARCHAR(255) NOT NULL,
    Password VARCHAR(255) NOT NULL,
    Email VARCHAR(255) NOT NULL
);

-- Tabla UserRoles
CREATE TABLE UserRoles (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    MarketingUserId INT NOT NULL,
    RoleId INT NOT NULL,
    FOREIGN KEY (MarketingUserId) REFERENCES MarketingUsers(Id),
    FOREIGN KEY (RoleId) REFERENCES Roles(Id)
);

-- Tabla Roles
CREATE TABLE Roles (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Name VARCHAR(255) NOT NULL
);

-- Insertamos registros en la tabla MarketplaceUsers
INSERT INTO MarketplaceUsers (Username, Password, Email) VALUES
('john_doe', 'password123', 'john_doe@example.com'),
('jane_doe', 'password123', 'jane_doe@example.com'),
('bad_luck_brian', 'badluck123', 'brian@example.com'),
('harry_potter', 'magic123', 'harry@example.com'),
('darth_vader', 'darkside123', 'vader@example.com');

-- Insertamos registros en la tabla MarketingUsers
INSERT INTO MarketingUsers (Username, Password, Email) VALUES
('mr.marketer', 'marketing123', 'marketer@example.com'),
('ad_king', 'ads123', 'adking@example.com');

-- Insertamos registros en la tabla Roles
INSERT INTO Roles (Name) VALUES
('Admin'),
('User');

-- Insertamos registros en la tabla UserRoles
INSERT INTO UserRoles (MarketingUserId, RoleId) VALUES
(1, 1),
(2, 2);

-- Insertamos registros en la tabla Products
INSERT INTO Products (Name, Price) VALUES
('Toothbrush', 2.99),
('Book - How to Fail at Everything', 14.99),
('Anti-stress Ball', 5.49),
('Lifetime Warranty for Nothing', 0.99),
('Happiness Pills', 29.99);

-- Insertamos registros en la tabla Coupons
INSERT INTO Coupons (Name, Description, StartDate, EndDate, DiscountType, IsLimited, UsageLimit, AmountUses, MinPurchaseAmount, MaxPurchaseAmount, Status, MarketingUserId) VALUES
('LUCKYDAY', 'Because bad luck Brian needs it.', '2024-01-01', '2024-12-31', 'Percentage', TRUE, 100, 0, 20.00, 100.00, 'Active', 1),
('FAILSALE', 'For those who fail but keep trying.', '2024-01-01', '2024-12-31', 'Net', TRUE, 50, 0, 10.00, 50.00, 'Active', 1),
('DARKSIDE', 'Darth Vader approved discount.', '2024-01-01', '2024-12-31', 'Percentage', TRUE, 100, 0, 20.00, 100.00, 'Active', 2),
('MAGIC10', 'Harry Potter magic discount.', '2024-01-01', '2024-12-31', 'Net', TRUE, 100, 0, 10.00, 50.00, 'Active', 2);

-- Insertamos registros en la tabla Purchases
INSERT INTO Purchases (MarketplaceUserId, ProductId, Date, Amount, Discount, Total) VALUES
(1, 1, '2024-06-01', 2.99, 0.00, 2.99),
(2, 2, '2024-06-01', 14.99, 5.00, 9.99),
(3, 3, '2024-06-01', 5.49, 1.00, 4.49),
(4, 4, '2024-06-01', 0.99, 0.00, 0.99),
(5, 5, '2024-06-01', 29.99, 10.00, 19.99);

-- Insertamos registros en la tabla PurchaseCoupon
INSERT INTO PurchaseCoupon (PurchaseId, CouponId) VALUES
(2, 1),
(3, 2);

-- Insertamos registros en la tabla CouponHistory
INSERT INTO CouponHistory (CouponId, ChangeDate, OldValue, NewValue) VALUES
(1, '2024-06-01', 'Inactive', 'Active'),
(2, '2024-06-01', 'Inactive', 'Active');

-- Insertamos registros en la tabla CouponUsages
INSERT INTO CouponUsages (CouponId, MarketplaceUserId, UseDate) VALUES
(1, 3, '2024-06-01'),
(2, 4, '2024-06-01');
