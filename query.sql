-- Active: 1718397538418@@belrhullbfv4pio8rcfu-mysql.services.clever-cloud.com@3306@belrhullbfv4pio8rcfu
-- Tabla CouponUsages
CREATE TABLE CouponUsages (
    Id INT PRIMARY KEY IDENTITY,
    CouponId INT NOT NULL,
    MarketplaceUserId INT NOT NULL,
    UseDate DATETIME NOT NULL,
    FOREIGN KEY (CouponId) REFERENCES Coupons(Id),
    FOREIGN KEY (MarketplaceUserId) REFERENCES MarketplaceUsers(Id)
);

-- Tabla Coupons
CREATE TABLE Coupons (
    Id INT PRIMARY KEY IDENTITY,
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
    Id INT PRIMARY KEY IDENTITY,
    PurchaseId INT NOT NULL,
    CouponId INT NOT NULL,
    FOREIGN KEY (PurchaseId) REFERENCES Purchases(Id),
    FOREIGN KEY (CouponId) REFERENCES Coupons(Id)
);

-- Tabla CouponHistory
CREATE TABLE CouponHistory (
    Id INT PRIMARY KEY IDENTITY,
    CouponId INT NOT NULL,
    ChangeDate DATETIME NOT NULL,
    OldValue VARCHAR(1000),
    NewValue VARCHAR(1000),
    FOREIGN KEY (CouponId) REFERENCES Coupons(Id)
);

-- Tabla Purchases
CREATE TABLE Purchases (
    Id INT PRIMARY KEY IDENTITY,
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
    Id INT PRIMARY KEY IDENTITY,
    Username VARCHAR(255) NOT NULL,
    Password VARCHAR(255) NOT NULL,
    Email VARCHAR(255) NOT NULL
);

-- Tabla Products
CREATE TABLE Products (
    Id INT PRIMARY KEY IDENTITY,
    Name VARCHAR(255) NOT NULL,
    Price DECIMAL(18, 2) NOT NULL
);

-- Tabla MarketingUsers
CREATE TABLE MarketingUsers (
    Id INT PRIMARY KEY IDENTITY,
    Username VARCHAR(255) NOT NULL,
    Password VARCHAR(255) NOT NULL,
    Email VARCHAR(255) NOT NULL
);

-- Tabla UserRoles
CREATE TABLE UserRoles (
    Id INT PRIMARY KEY IDENTITY,
    MarketingUserId INT NOT NULL,
    RoleId INT NOT NULL,
    FOREIGN KEY (MarketingUserId) REFERENCES MarketingUsers(Id),
    FOREIGN KEY (RoleId) REFERENCES Roles(Id)
);

-- Tabla Roles
CREATE TABLE Roles (
    Id INT PRIMARY KEY IDENTITY,
    Name VARCHAR(255) NOT NULL
);