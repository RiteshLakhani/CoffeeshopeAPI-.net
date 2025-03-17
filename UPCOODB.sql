--Name:- Lakhani Ritesh
--EnrollmentNo:- 22010101099
--Sem:- 6   Batch:- A1

--User Table--
CREATE TABLE Users 
(
    UserID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    UserName VARCHAR(100) NOT NULL,
    Email VARCHAR(100) NOT NULL,
    Password VARCHAR(100) NOT NULL,
    MobileNo VARCHAR(15) NOT NULL,
    Address VARCHAR(100) NOT NULL,
    IsActive BIT NOT NULL
);


--Product Table--
CREATE TABLE Products (
    ProductID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    ProductName VARCHAR(100) NOT NULL,
    ProductPrice DECIMAL(10,2) NOT NULL,
    ProductCode VARCHAR(100) NOT NULL,
    Description VARCHAR(100) NOT NULL,
    UserID INT NOT NULL,
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

select * from Products


--Customer Table--
CREATE TABLE Customers (
    CustomerID INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
    CustomerName VARCHAR(100) NOT NULL,
    HomeAddress VARCHAR(100) NOT NULL,
    Email VARCHAR(100) NOT NULL,
    MobileNo VARCHAR(15) NOT NULL,
    GST_NO VARCHAR(15) NOT NULL,
    CityName VARCHAR(100) NOT NULL,
    PinCode VARCHAR(15) NOT NULL,
    NetAmount DECIMAL(10,2) NOT NULL,
    UserID INT NOT NULL,
    FOREIGN KEY (UserID) REFERENCES Users(UserID) 
);

--Order Table--
Create TABLE Orders (
    OrderID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    OrderDate DATETIME NOT NULL,
    CustomerID INT NOT NULL,
    PaymentMode VARCHAR(100),
    TotalAmount DECIMAL(10,2),
    ShippingAddress VARCHAR(100) NOT NULL,
    UserID INT NOT NULL,
    FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID), 
    FOREIGN KEY (UserID) REFERENCES Users(UserID)  
);

Alter Table Orders
Add OrderCode varchar(100)


---OrderDetail Table--
CREATE TABLE OrderDetail (
    OrderDetailID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,  
    OrderID INT NOT NULL,
    ProductID INT NOT NULL,
    Quantity INT NOT NULL,
    Amount DECIMAL(10,2) NOT NULL,
    TotalAmount DECIMAL(10,2) NOT NULL,
    UserID INT NOT NULL,
    FOREIGN KEY (OrderID) REFERENCES Orders(OrderID),     
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID), 
    FOREIGN KEY (UserID) REFERENCES Users(UserID)         
);


--Bill Table--
CREATE TABLE Bills (
    BillID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,  
    BillNumber VARCHAR(100) NOT NULL,
    BillDate DATETIME NOT NULL,
    OrderID INT NOT NULL,
    TotalAmount DECIMAL(10,2) NOT NULL,
    Discount DECIMAL(10,2),
    NetAmount DECIMAL(10,2) NOT NULL,
    UserID INT NOT NULL,
    FOREIGN KEY (OrderID) REFERENCES Orders(OrderID), 
    FOREIGN KEY (UserID) REFERENCES Users(UserID)  
);


---Procedure---

-----User-------

Select * from Users
--Add-----

Alter PROCEDURE [dbo].[PR_User_Insert]
    @UserName VARCHAR(100),
    @Email VARCHAR(100),
    @Password VARCHAR(100),
    @MobileNo VARCHAR(15),
    @Address VARCHAR(100),
    @IsActive BIT
AS
BEGIN
    INSERT INTO [dbo].[Users]
    (
        [dbo].[Users].[UserName],
        [dbo].[Users].[Email],
        [dbo].[Users].[Password],
        [dbo].[Users].[MobileNo],
        [dbo].[Users].[Address],
        [dbo].[Users].[IsActive]
    )
    VALUES
    (
        @UserName,
        @Email,
        @Password,
        @MobileNo,
        @Address,
        @IsActive
    )
END



---Update---
Alter PROCEDURE [dbo].[PR_User_UpdateByPK]
    @UserID INT,
    @UserName VARCHAR(100),
    @Email VARCHAR(100),
    @Password VARCHAR(100),
    @MobileNo VARCHAR(15),
    @Address VARCHAR(100),
    @IsActive BIT
AS
BEGIN
    UPDATE [dbo].[Users]
    SET
		[dbo].[Users].[UserName] = @UserName,
        [dbo].[Users].[Email] = @Email,
        [dbo].[Users].[Password] = @Password,
        [dbo].[Users].[MobileNo] = @MobileNo,
        [dbo].[Users].[Address] = @Address,
        [dbo].[Users].[IsActive] = @IsActive
    WHERE
        [dbo].[Users].[UserID] = @UserID;
END



--Delete--
CREATE PROCEDURE [dbo].[PR_User_DeleteByPK]
    @UserID INT
AS
BEGIN
    DELETE FROM [dbo].[Users]
    WHERE [dbo].[Users].[UserID] = @UserID;
END


--Select By All--
Alter PROCEDURE [dbo].[PR_User_SelectAll]
AS
BEGIN
    SELECT 
        [UserID],
        [UserName],
        [Email],
        [Password],
        [MobileNo],
        [Address],
        [IsActive]
    FROM 
        [dbo].[Users];
END

Select * from Users

--Select By 
Alter PROCEDURE [dbo].[PR_User_SelectByPK]
    @UserID INT
AS
BEGIN
    SELECT 
        [dbo].[Users].[UserID],
        [dbo].[Users].[UserName],
        [dbo].[Users].[Email],
        [dbo].[Users].[Password],
        [dbo].[Users].[MobileNo],
        [dbo].[Users].[Address],
        [dbo].[Users].[IsActive]
    FROM 
        [dbo].[Users]
    WHERE 
        [dbo].[Users].[UserID] = @UserID;
END

--DropDown---
Create Procedure [dbo].[PR_User_DropDown]
As
Begin
	Select [dbo].[Users].[UserID],
		[dbo].[Users].[UserName]
		from [dbo].[Users]
End


-----------Product------------------

--Add--
Alter PROCEDURE [dbo].[PR_Product_Insert]
    @ProductName VARCHAR(100),
    @ProductPrice DECIMAL(10,2),
    @ProductCode VARCHAR(100),
    @Description VARCHAR(100),
    @UserID INT
AS
BEGIN
    INSERT INTO [dbo].[Products] (
        [ProductName],
        [ProductPrice],
        [ProductCode],
        [Description],
        [UserID]
    )
    VALUES (
        @ProductName,
        @ProductPrice,
        @ProductCode,
        @Description,
        @UserID
    );
END



---Update----
Alter PROCEDURE [dbo].[PR_Product_UpdateByPK]
    @ProductID INT,
    @ProductName VARCHAR(100),
    @ProductPrice DECIMAL(10,2),
    @ProductCode VARCHAR(100),
    @Description VARCHAR(100),
    @UserID INT
AS
BEGIN
    UPDATE [dbo].[Products]
    SET
        [dbo].[Products].[ProductName] = @ProductName,
        [dbo].[Products].[ProductPrice] = @ProductPrice,
        [dbo].[Products].[ProductCode] = @ProductCode,
        [dbo].[Products].[Description] = @Description,
        [dbo].[Products].[UserID] = @UserID
    WHERE
        [dbo].[Products].[ProductID] = @ProductID;
END



--Delete----
CREATE PROCEDURE [dbo].[PR_Product_DeleteByPK]
    @ProductID INT
AS
BEGIN
    DELETE FROM [dbo].[Products]
    WHERE [ProductID] = @ProductID;
END

---Select By All---
Alter PROCEDURE [dbo].[PR_Product_SelectAll]
AS
BEGIN
    SELECT 
        [dbo].[Products].[ProductID],
        [dbo].[Products].[ProductName],
        [dbo].[Products].[ProductPrice],
        [dbo].[Products].[ProductCode],
        [dbo].[Products].[Description],
        [dbo].[Products].[UserID],
        [dbo].[Users].[UserName]
    FROM 
        [dbo].[Products]
    INNER JOIN 
        [dbo].[Users]
    ON 
        [dbo].[Products].[UserID] = [dbo].[Users].[UserID];
END


--Select By Pk--
Alter PROCEDURE [dbo].[PR_Product_SelectByPK]
    @ProductID INT
AS
BEGIN
    SELECT 
        [dbo].[Products].[ProductID],
        [dbo].[Products].[ProductName],
        [dbo].[Products].[ProductPrice],
        [dbo].[Products].[ProductCode],
        [dbo].[Products].[Description],
        [dbo].[Products].[UserID]
        --[dbo].[Users].[UserName]
    FROM 
        [dbo].[Products]
    --INNER JOIN 
      --  [dbo].[Users]
    --ON 
      -- [dbo].[Products].[UserID] = [dbo].[Users].[UserID]
    WHERE 
        [dbo].[Products].[ProductID] = @ProductID;
END


--Product DropDown--
Alter Procedure [dbo].[PR_Product_DropDown]
As
Begin
	Select [dbo].[Products].[ProductID],
		[dbo].[Products].[ProductName]
		from [dbo].[Products]
End


-----Customer Table--------

--Add--
CREATE PROCEDURE [dbo].[PR_Customer_Insert]
    @CustomerName VARCHAR(100),
    @HomeAddress VARCHAR(100),
    @Email VARCHAR(100),
    @MobileNo VARCHAR(15),
    @GST_NO VARCHAR(15),
    @CityName VARCHAR(100),
    @PinCode VARCHAR(15),
    @NetAmount DECIMAL(10,2),
    @UserID INT
AS
BEGIN
    INSERT INTO [dbo].[Customers] (
        [CustomerName],
        [HomeAddress],
        [Email],
        [MobileNo],
        [GST_NO],
        [CityName],
        [PinCode],
        [NetAmount],
        [UserID]
    )
    VALUES (
        @CustomerName,
        @HomeAddress,
        @Email,
        @MobileNo,
        @GST_NO,
        @CityName,
        @PinCode,
        @NetAmount,
        @UserID
    );
END

--Update--
CREATE PROCEDURE [dbo].[PR_Customer_UpdateByPK]
    @CustomerID INT,
    @CustomerName VARCHAR(100),
    @HomeAddress VARCHAR(100),
    @Email VARCHAR(100),
    @MobileNo VARCHAR(15),
    @GST_NO VARCHAR(15),
    @CityName VARCHAR(100),
    @PinCode VARCHAR(15),
    @NetAmount DECIMAL(10,2),
    @UserID INT
AS
BEGIN
    UPDATE [dbo].[Customers]
    SET
        [CustomerName] = @CustomerName,
        [HomeAddress] = @HomeAddress,
        [Email] = @Email,
        [MobileNo] = @MobileNo,
        [GST_NO] = @GST_NO,
        [CityName] = @CityName,
        [PinCode] = @PinCode,
        [NetAmount] = @NetAmount,
        [UserID] = @UserID
    WHERE
        [CustomerID] = @CustomerID;
END


--Delete--
CREATE PROCEDURE [dbo].[PR_Customer_DeleteByPK]
    @CustomerID INT
AS
BEGIN
    DELETE FROM [dbo].[Customers]
    WHERE [CustomerID] = @CustomerID;
END

--Select By ALL--
ALTER PROCEDURE [dbo].[PR_Customer_SelectAll]
AS
BEGIN
    SELECT 
        [dbo].[Customers].[CustomerID],
        [dbo].[Customers].[CustomerName],
        [dbo].[Customers].[HomeAddress],
        [dbo].[Customers].[Email],
        [dbo].[Customers].[MobileNo],
        [dbo].[Customers].[GST_NO],
        [dbo].[Customers].[CityName],
        [dbo].[Customers].[PinCode],
        [dbo].[Customers].[NetAmount],
		[dbo].[Customers].[UserID],
        [dbo].[Users].[UserName]
    FROM 
        [dbo].[Customers]
    INNER JOIN 
        [dbo].[Users]
    ON 
        [dbo].[Customers].[UserID] = [dbo].[Users].[UserID];
END


--Select By ID--
Alter PROCEDURE [dbo].[PR_Customer_SelectByPK]
    @CustomerID INT
AS
BEGIN
    SELECT 
        [dbo].[Customers].[CustomerID],
        [dbo].[Customers].[CustomerName],
        [dbo].[Customers].[HomeAddress],
        [dbo].[Customers].[Email],
        [dbo].[Customers].[MobileNo],
        [dbo].[Customers].[GST_NO],
        [dbo].[Customers].[CityName],
        [dbo].[Customers].[PinCode],
        [dbo].[Customers].[NetAmount],
		[dbo].[Customers].[UserID]
        --[dbo].[Users].[UserName] AS [AddedByUser],
        --[dbo].[Users].[Email] AS [UserEmail],
        --[dbo].[Users].[MobileNo] AS [UserMobileNo]
    FROM 
        [dbo].[Customers]
    --INNER JOIN 
      --  [dbo].[Users]
    --ON 
      --  [dbo].[Customers].[UserID] = [dbo].[Users].[UserID]
    WHERE 
        [dbo].[Customers].[CustomerID] = @CustomerID;
END


--DropDown---
Create Procedure [dbo].[PR_Customer_DropDown]
As
Begin
	Select [dbo].[Customers].[CustomerID],
		[dbo].[Customers].[CustomerName]
		from [dbo].[Customers]
End




-------Order Table-------
Select * from Orders
--Add-
Alter PROCEDURE [dbo].[PR_Order_Insert]
	@OrderCode Varchar(50),
    @OrderDate DATETIME,
    @CustomerID INT,
    @PaymentMode VARCHAR(100) = NULL,
    @TotalAmount DECIMAL(10, 2) = NULL,
    @ShippingAddress VARCHAR(100),
    @UserID INT
AS
BEGIN
    INSERT INTO [dbo].[Orders]
    (
		[OrderCode],
        [OrderDate],
        [CustomerID],
        [PaymentMode],
        [TotalAmount],
        [ShippingAddress],
        [UserID]
    )
    VALUES
    (
		@OrderCode,
        @OrderDate,
        @CustomerID,
        @PaymentMode,
        @TotalAmount,
        @ShippingAddress,
        @UserID
    );
END


--Update--
Alter PROCEDURE [dbo].[PR_Order_UpdateByPK]
    @OrderID INT,
	@OrderCode Varchar(50),
    @OrderDate DATETIME,
    @CustomerID INT,
    @PaymentMode VARCHAR(100) = NULL,
    @TotalAmount DECIMAL(10, 2) = NULL,
    @ShippingAddress VARCHAR(100),
    @UserID INT
AS
BEGIN
    UPDATE [dbo].[Orders]
    SET
		[OrderCode] = @OrderCode,
        [OrderDate] = @OrderDate,
        [CustomerID] = @CustomerID,
        [PaymentMode] = @PaymentMode,
        [TotalAmount] = @TotalAmount,
        [ShippingAddress] = @ShippingAddress,
        [UserID] = @UserID
    WHERE [OrderID] = @OrderID;
END  

--Delete--
CREATE PROCEDURE [dbo].[PR_Order_DeleteByPK]
    @OrderID INT
AS
BEGIN
    DELETE FROM [dbo].[Orders]
    WHERE [dbo].[Orders].[OrderID] = @OrderID;
END

--Select By All--
Alter PROCEDURE [dbo].[PR_Order_SelectAll]
AS
BEGIN
    SELECT 
        [dbo].[Orders].[OrderID],
		[dbo].[Orders].[OrderCode],
        [dbo].[Orders].[OrderDate],
		[dbo].[Customers].[CustomerID],
		[dbo].[Customers].[CustomerName],
        [dbo].[Orders].[PaymentMode],
        [dbo].[Orders].[TotalAmount],
        [dbo].[Orders].[ShippingAddress],
        [dbo].[Users].[UserID],
        --[dbo].[Customers].[Email],
        [dbo].[Users].[UserName]
        --[dbo].[Users].[Email]
    FROM [dbo].[Orders]
    INNER JOIN [dbo].[Customers]
       ON [dbo].[Customers].[CustomerID] = [dbo].[Orders].[CustomerID]
    INNER JOIN [dbo].[Users]
        ON [dbo].[Users].[UserID] = [dbo].[Orders].[UserID];
END


--Select By Pk----
Alter PROCEDURE [dbo].[PR_Order_SelectByPK]
    @OrderID INT
AS
BEGIN
    SELECT 
        [dbo].[Orders].[OrderID],
		[dbo].[Orders].[OrderCode],
        [dbo].[Orders].[OrderDate],
        [dbo].[Orders].[PaymentMode],
        [dbo].[Orders].[TotalAmount],
        [dbo].[Orders].[ShippingAddress],
        [dbo].[Orders].[CustomerID],
        [dbo].[Orders].[UserID]
    FROM [dbo].[Orders]
    WHERE [dbo].[Orders].[OrderID] = @OrderID;
END


--DropDown--
Create Procedure [dbo].[PR_Orders_DropDown]
As
Begin
	Select [dbo].[Orders].[OrderID],
		[dbo].[Orders].[OrderCode]
		from [dbo].[Orders]
End



----Order Details-----

--Add---
CREATE PROCEDURE [dbo].[PR_OrderDetail_Insert]
    @OrderID INT,
    @ProductID INT,
    @Quantity INT,
    @Amount DECIMAL(10,2),
    @TotalAmount DECIMAL(10,2),
    @UserID INT
AS
BEGIN
    INSERT INTO [dbo].[OrderDetail]
    (
        [OrderID],
        [ProductID],
        [Quantity],
        [Amount],
        [TotalAmount],
        [UserID]
    )
    VALUES
    (
        @OrderID,
        @ProductID,
        @Quantity,
        @Amount,
        @TotalAmount,
        @UserID
    );
END

--Update--
CREATE PROCEDURE [dbo].[PR_OrderDetail_UpdateByPK]
    @OrderDetailID INT,
    @OrderID INT,
    @ProductID INT,
    @Quantity INT,
    @Amount DECIMAL(10,2),
    @TotalAmount DECIMAL(10,2),
    @UserID INT
AS
BEGIN
    UPDATE [dbo].[OrderDetail]
    SET
        [OrderID] = @OrderID,
        [ProductID] = @ProductID,
        [Quantity] = @Quantity,
        [Amount] = @Amount,
        [TotalAmount] = @TotalAmount,
        [UserID] = @UserID
    WHERE [OrderDetailID] = @OrderDetailID;
END

Select * from OrderDetail
--Delete---
CREATE PROCEDURE [dbo].[PR_OrderDetail_DeleteByPK]
    @OrderDetailID INT
AS
BEGIN
    DELETE FROM [dbo].[OrderDetail]
    WHERE [OrderDetailID] = @OrderDetailID;
END


--Select By All--
Alter PROCEDURE [dbo].[PR_OrderDetail_SelectAll]
AS
BEGIN
    SELECT 
        [dbo].[OrderDetail].[OrderDetailID],
		[dbo].[OrderDetail].[OrderID],
		[dbo].[OrderDetail].[ProductID],
		[dbo].[OrderDetail].[Quantity],
		[dbo].[OrderDetail].[Amount],
		[dbo].[OrderDetail].[TotalAmount],
		[dbo].[Orders].[OrderID],
		[dbo].[Orders].[OrderCode],
		[dbo].[Products].[ProductID],  
		[dbo].[Products].[ProductName],
		[dbo].[Users].[UserID],
		[dbo].[Users].[UserName]      
    FROM [dbo].[OrderDetail]
    INNER JOIN [dbo].[Orders]
        ON [dbo].[Orders].[OrderID] = [dbo].[OrderDetail].[OrderID]
    INNER JOIN [dbo].[Products]
        ON [dbo].[Products].[ProductID] = [dbo].[OrderDetail].[ProductID]
    INNER JOIN [dbo].[Users]
       ON [dbo].[Users].[UserID] = [dbo].[OrderDetail].[UserID];
END


--Select By PK--
Alter PROCEDURE [dbo].[PR_OrderDetail_SelectByPK]
    @OrderDetailID INT
AS
BEGIN
    SELECT 
        [dbo].[OrderDetail].[OrderDetailID],
        [dbo].[OrderDetail].[OrderID],
        [dbo].[OrderDetail].[ProductID],
        [dbo].[OrderDetail].[Quantity],
        [dbo].[OrderDetail].[Amount],
        [dbo].[OrderDetail].[TotalAmount],
        [dbo].[OrderDetail].[UserID]
        --[dbo].[Orders].[OrderDate],
        --[dbo].[Products].[ProductName]
    FROM [dbo].[OrderDetail]
    --INNER JOIN [dbo].[Users]
      --  ON [dbo].[Users].[UserID] = [dbo].[OrderDetail].[UserID]
    --INNER JOIN [dbo].[Orders]
      --  ON [dbo].[Orders].[OrderID] = [dbo].[OrderDetail].[OrderID]
    --INNER JOIN [dbo].[Products]
      --  ON [dbo].[Products].[ProductID] = [dbo].[OrderDetail].[ProductID]
    WHERE [dbo].[OrderDetail].[OrderDetailID] = @OrderDetailID;
END

------Bill Table-----
--Add--
CREATE PROCEDURE [dbo].[PR_Bills_Insert]
    @BillNumber varchar(100),
    @BillDate datetime,
    @OrderID int,
    @TotalAmount decimal(10,2),
    @Discount decimal(10,2) = NULL,
    @NetAmount decimal(10,2),
    @UserID int
AS
BEGIN
    INSERT INTO [dbo].[Bills]
    (
        [BillNumber],
        [BillDate],
        [OrderID],
        [TotalAmount],
        [Discount],
        [NetAmount],
        [UserID]
    )
    VALUES
    (
        @BillNumber,
        @BillDate,
        @OrderID,
        @TotalAmount,
        @Discount,
        @NetAmount,
        @UserID
    );
END


--Update--
CREATE PROCEDURE [dbo].[PR_Bills_UpdateByPK]
    @BillID int,
    @BillNumber varchar(100),
    @BillDate datetime,
    @OrderID int,
    @TotalAmount decimal(10,2),
    @Discount decimal(10,2) = NULL,
    @NetAmount decimal(10,2),
    @UserID int
AS
BEGIN
    UPDATE [dbo].[Bills]
    SET
        [BillNumber] = @BillNumber,
        [BillDate] = @BillDate,
        [OrderID] = @OrderID,
        [TotalAmount] = @TotalAmount,
        [Discount] = @Discount,
        [NetAmount] = @NetAmount,
        [UserID] = @UserID
    WHERE [BillID] = @BillID;
END

--Delete--
CREATE PROCEDURE [dbo].[PR_Bills_DeleteByPK]
    @BillID int
AS
BEGIN
    DELETE FROM [dbo].[Bills]
    WHERE [BillID] = @BillID;
END


--Select By ALL--
Alter PROCEDURE [dbo].[PR_Bills_SelectAll]
AS
BEGIN
    SELECT
        [dbo].[Bills].[BillID],
        [dbo].[Bills].[BillNumber],
        [dbo].[Bills].[BillDate],
		[dbo].[Bills].[OrderID],
        [dbo].[Bills].[TotalAmount],
        [dbo].[Bills].[Discount],
        [dbo].[Bills].[NetAmount],
        [dbo].[Users].[UserID],
		[dbo].[Users].[UserName],
        [dbo].[Orders].[OrderID],
		[dbo].[Orders].[OrderCode]
    FROM [dbo].[Bills]
    INNER JOIN [dbo].[Users]
        ON [dbo].[Users].[UserID] = [dbo].[Bills].[UserID]
    INNER JOIN [dbo].[Orders]
       ON [dbo].[Orders].[OrderID] = [dbo].[Bills].[OrderID];
END

Select * from Bills
--Select By PK--
Alter PROCEDURE [dbo].[PR_Bills_SelectByPK]
    @BillID int
AS
BEGIN
    SELECT
        [dbo].[Bills].[BillID],
        [dbo].[Bills].[BillNumber],
        [dbo].[Bills].[BillDate],
        [dbo].[Bills].[OrderID],
        [dbo].[Bills].[TotalAmount],
        [dbo].[Bills].[Discount],
        [dbo].[Bills].[NetAmount],
        [dbo].[Bills].[UserID]
        --[dbo].[Orders].[OrderDate],
        --[dbo].[Orders].[CustomerID]
    FROM [dbo].[Bills]
    --INNER JOIN [dbo].[Users]
      --  ON [dbo].[Users].[UserID] = [dbo].[Bills].[UserID]
    --INNER JOIN [dbo].[Orders]
      --  ON [dbo].[Orders].[OrderID] = [dbo].[Bills].[OrderID]
    WHERE [dbo].[Bills].[BillID] = @BillID;
END

-------------Insert Data into All Tables--------------------
----User---------

INSERT INTO dbo.[Users] (UserName, Email, Password, MobileNo, Address, IsActive)
VALUES 
('John Doe', 'john.doe@example.com', 'password123', '1234567890', '123 Main St', 1),
('Jane Smith', 'jane.smith@example.com', 'password456', '0987654321', '456 Elm St', 1),
('Alice Johnson', 'alice.johnson@example.com', 'password789', '1122334455', '789 Pine St', 0),
('Bob Brown', 'bob.brown@example.com', 'password321', '2233445566', '321 Oak St', 1),
('Charlie Davis', 'charlie.davis@example.com', 'password654', '3344556677', '654 Cedar St', 0),
('David Evans', 'david.evans@example.com', 'password111', '4455667788', '111 Maple St', 1),
('Eva Green', 'eva.green@example.com', 'password222', '5566778899', '222 Birch St', 0),
('Frank Harris', 'frank.harris@example.com', 'password333', '6677889900', '333 Willow St', 1),
('Grace Lee', 'grace.lee@example.com', 'password444', '7788990011', '444 Spruce St', 1),
('Henry King', 'henry.king@example.com', 'password555', '8899001122', '555 Cherry St', 0);


-- Order Table

INSERT INTO dbo.[Orders] (OrderDate, CustomerID, PaymentMode, TotalAmount, ShippingAddress, UserID)
VALUES 
('2023-07-01 10:30:00', 1, 'Credit Card', 150.75, '123 Main St', 1),
('2023-07-02 14:00:00', 2, 'PayPal', 200.00, '456 Elm St', 2),
('2023-07-03 09:15:00', 3, 'Credit Card', 120.00, '789 Pine St', 3),
('2023-07-04 11:45:00', 4, 'Cash', 99.99, '321 Oak St', 4),
('2023-07-05 16:20:00', 5, 'Debit Card', 175.50, '654 Cedar St', 5),
('2023-07-06 12:00:00', 1, 'Credit Card', 220.75, '123 Main St', 1),
('2023-07-07 08:45:00', 2, 'PayPal', 300.00, '456 Elm St', 2),
('2023-07-08 17:30:00', 3, 'Cash', 180.25, '789 Pine St', 3),
('2023-07-09 13:10:00', 4, 'Credit Card', 210.00, '321 Oak St', 4),
('2023-07-10 10:50:00', 5, 'Debit Card', 250.00, '654 Cedar St', 5);


-- Product

INSERT INTO dbo.[Products] (ProductName, ProductPrice, ProductCode, Description, UserID)
VALUES 
('Product A', 10.00, 'PRA100', 'Description of Product A', 1),
('Product B', 20.00, 'PRB200', 'Description of Product B', 2),
('Product C', 30.00, 'PRC300', 'Description of Product C', 1),
('Product D', 40.00, 'PRD400', 'Description of Product D', 3),
('Product E', 50.00, 'PRE500', 'Description of Product E', 2),
('Product F', 60.00, 'PRF600', 'Description of Product F', 3),
('Product G', 70.00, 'PRG700', 'Description of Product G', 1),
('Product H', 80.00, 'PRH800', 'Description of Product H', 2),
('Product I', 90.00, 'PRI900', 'Description of Product I', 3),
('Product J', 100.00, 'PRJ1000', 'Description of Product J', 1);


-- Order Detail

INSERT INTO dbo.[OrderDetail] (OrderID, ProductID, Quantity, Amount, TotalAmount, UserID)
VALUES 
(1, 1, 1, 10.00, 10.00, 1),
(1, 2, 2, 20.00, 40.00, 1),
(2, 3, 1, 30.00, 30.00, 2),
(2, 4, 2, 40.00, 80.00, 2),
(3, 5, 1, 50.00, 50.00, 3),
(3, 1, 3, 10.00, 30.00, 3),
(4, 2, 2, 20.00, 40.00, 1),
(4, 3, 1, 30.00, 30.00, 1),
(5, 4, 2, 40.00, 80.00, 2),
(5, 5, 1, 50.00, 50.00, 2);


-- Insert records into dbo.Bills table

INSERT INTO dbo.[Bills] (BillNumber, BillDate, OrderID, TotalAmount, Discount, NetAmount, UserID)
VALUES 
('BILL001', '2024-07-01', 1, 100.00, 5.00, 95.00, 1),
('BILL002', '2024-07-02', 2, 200.00, 10.00, 190.00, 2),
('BILL003', '2024-07-03', 3, 300.00, 15.00, 285.00, 3),
('BILL004', '2024-07-04', 4, 150.00, NULL, 150.00, 1),
('BILL005', '2024-07-05', 5, 250.00, 12.50, 237.50, 2),
('BILL006', '2024-07-06', 1, 120.00, 6.00, 114.00, 1),
('BILL007', '2024-07-07', 2, 220.00, 11.00, 209.00, 2),
('BILL008', '2024-07-08', 3, 320.00, 16.00, 304.00, 3),
('BILL009', '2024-07-09', 4, 180.00, 9.00, 171.00, 1),
('BILL010', '2024-07-10', 5, 270.00, 13.50, 256.50, 2);

-- Insert records into dbo.Customer table

INSERT INTO dbo.[Customers] (CustomerName, HomeAddress, Email, MobileNo, GST_NO, CityName, PinCode, NetAmount, UserID)
VALUES 
('Alice Green', '789 Pine St', 'alice.green@example.com', '1234567890', 'GST1234567890', 'Pine City', '123456', 1000.00, 1),
('Bob White', '321 Oak St', 'bob.white@example.com', '0987654321', 'GST0987654321', 'Oak Town', '654321', 2000.00, 2),
('Charlie Black', '456 Elm St', 'charlie.black@example.com', '1122334455', 'GST1122334455', 'Elm Village', '789012', 1500.00, 3),
('David Blue', '654 Cedar St', 'david.blue@example.com', '2233445566', 'GST2233445566', 'Cedar Grove', '345678', 2500.00, 4),
('Emma Yellow', '123 Main St', 'emma.yellow@example.com', '3344556677', 'GST3344556677', 'Main City', '567890', 3000.00, 5),
('Frank Orange', '789 Birch St', 'frank.orange@example.com', '4455667788', 'GST4455667788', 'Birch Town', '678901', 1750.00, 1),
('Grace Purple', '321 Willow St', 'grace.purple@example.com', '5566778899', 'GST5566778899', 'Willow Grove', '890123', 2250.00, 2),
('Henry Brown', '456 Maple St', 'henry.brown@example.com', '6677889900', 'GST6677889900', 'Maple Village', '901234', 2750.00, 3),
('Isabel Silver', '654 Spruce St', 'isabel.silver@example.com', '7788990011', 'GST7788990011', 'Spruce Town', '123789', 3250.00, 4),
('Jack Gold', '123 Cedar St', 'jack.gold@example.com', '8899001122', 'GST8899001122', 'Cedar City', '345012', 3500.00, 5);




select * from Users
select * from Products
select * from Customers
select * from Orders
select * from OrderDetail
select * from Bills


----Login Procedure-----
Create Procedure [dbo].[PR_User_Login]
	@UserName varchar(50),
	@Password varchar(50)
As
Begin
	Select 
	[dbo].[Users].[UserID],
	[dbo].[Users].[UserName],
	[dbo].[Users].[MobileNo],
	[dbo].[Users].[Email],
	[dbo].[Users].[Password],
	[dbo].[Users].[Address]
	from [dbo].[Users]
	Where 
	 [dbo].[Users].[UserName] = @UserName 
     AND 
	 [dbo].[Users].[Password] = @Password;
End;

select * from Users


----Register Procedure-----
CREATE PROCEDURE [dbo].[PR_User_Register]
    @UserName NVARCHAR(50),
    @Password NVARCHAR(50),
    @Email NVARCHAR(500),
    @MobileNo VARCHAR(50),
    @Address VARCHAR(50)
AS
BEGIN
    INSERT INTO [dbo].[Users]
    (
        [UserName],
        [Password],
        [Email],
        [MobileNo],
        [Address]
    )
    VALUES
    (
        @UserName,
        @Password,
        @Email,
        @MobileNo,
        @Address
    );
END


-----------COUNTRY TABLE---------------------
CREATE TABLE Country (
    CountryID INT PRIMARY KEY IDENTITY(1,1),
    CountryName NVARCHAR(100) NOT NULL,
    CountryCode NVARCHAR(10) NOT NULL,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    ModifiedDate DATETIME NULL
);

----------State TABLE--------------------------
CREATE TABLE State (
    StateID INT PRIMARY KEY IDENTITY(1,1),
    CountryID INT NOT NULL,
    StateName NVARCHAR(100) NOT NULL,
    StateCode NVARCHAR(10),
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    ModifiedDate DATETIME NULL,
    FOREIGN KEY (CountryID) REFERENCES Country(CountryID)
);


--------CITY TABLE-----------------------------
CREATE TABLE City (
    CityID INT PRIMARY KEY IDENTITY(1,1),
    StateID INT NOT NULL,
    CountryID INT NOT NULL,
    CityName NVARCHAR(100) NOT NULL,
    CityCode NVARCHAR(10),
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    ModifiedDate DATETIME NULL,
    FOREIGN KEY (StateID) REFERENCES State(StateID),
    FOREIGN KEY (CountryID) REFERENCES Country(CountryID)
);


INSERT INTO Country (CountryName, CountryCode, CreatedDate) VALUES
('United States', 'US', GETDATE()),
('India', 'IN', GETDATE()),
('Australia', 'AU', GETDATE()),
('Canada', 'CA', GETDATE()),
('United Kingdom', 'UK', GETDATE()),
('Germany', 'DE', GETDATE()),
('France', 'FR', GETDATE()),
('Japan', 'JP', GETDATE()),
('China', 'CN', GETDATE()),
('Brazil', 'BR', GETDATE());

INSERT INTO State (StateName, StateCode, CountryID, CreatedDate) VALUES
('California', 'CA', 1, GETDATE()),
('Texas', 'TX', 1, GETDATE()),
('Gujarat', 'GJ', 2, GETDATE()),
('Maharashtra', 'MH', 2, GETDATE()),
('New South Wales', 'NSW', 3, GETDATE()),
('Victoria', 'VIC', 3, GETDATE()),
('Ontario', 'ON', 4, GETDATE()),
('Quebec', 'QC', 4, GETDATE()),
('England', 'ENG', 5, GETDATE()),
('Scotland', 'SCT', 5, GETDATE());

INSERT INTO City (CityName, CityCode, StateID, CountryID, CreatedDate) VALUES
('Los Angeles', 'LA', 1, 1, GETDATE()),
('Houston', 'HOU', 2, 1, GETDATE()),
('Ahmedabad', 'AMD', 3, 2, GETDATE()),
('Mumbai', 'MUM', 4, 2, GETDATE()),
('Sydney', 'SYD', 5, 3, GETDATE()),
('Melbourne', 'MEL', 6, 3, GETDATE()),
('Toronto', 'TOR', 7, 4, GETDATE()),
('Montreal', 'MTL', 8, 4, GETDATE()),
('London', 'LDN', 9, 5, GETDATE()),
('Edinburgh', 'EDI', 10, 5, GETDATE());

---------------------City---------------------------------
--GET ALL CITY--
Alter PROCEDURE [dbo].[PR_LOC_City_SelectAll]
AS 
SELECT
		[dbo].[City].[CityID],
		[dbo].[City].[StateID],
		[dbo].[Country].[CountryID],
		[dbo].[Country].[CountryName],
		[dbo].[State].[StateName],
		[dbo].[State].[StateCode],
		[dbo].[City].[CreatedDate],
		[dbo].[City].[ModifiedDate],
		[dbo].[City].[CityName],
		[dbo].[City].[CityCode]
		
FROM [dbo].[City]
LEFT OUTER JOIN [dbo].[State]
ON [dbo].[State].[StateID] = [dbo].[City].[StateID]
LEFT OUTER JOIN [dbo].[Country]
ON [dbo].[Country].[CountryID] = [dbo].[State].[CountryID]

--GET CITY BY ID--
CREATE PROCEDURE PR_LOC_City_SelectByPK
    @CityID INT
AS
BEGIN
    SELECT CityID, CityName, StateID, CountryID, CityCode
    FROM City
    WHERE CityID = @CityID
END

--INSERT CITY--
Alter PROCEDURE PR_LOC_City_Insert
    @CityName NVARCHAR(100),
    @CityCode NVARCHAR(10),
    @StateID INT,
    @CountryID INT,
	@Created DATETIME,
	@Modified DATETIME
AS
BEGIN
    INSERT INTO City (CityName, CityCode, StateID, CountryID, CreatedDate,ModifiedDate)
    VALUES (@CityName, @CityCode, @StateID, @CountryID,ISNULL(@Created,GETDATE()),ISNULL(@Modified,GETDATE()));
END

--UPDATE CITY--
ALTER PROCEDURE PR_LOC_City_Update
    @CityID INT,
    @CityName NVARCHAR(100),
    @CityCode NVARCHAR(10),
    @StateID INT,
    @CountryID INT,
	@Modified DATETIME
AS
BEGIN
    UPDATE City
    SET CityName = @CityName,
        CityCode = @CityCode,
        StateID = @StateID,
        CountryID = @CountryID,
        ModifiedDate = ISNULL(@Modified,GETDATE())
    WHERE CityID = @CityID;
END;

--DELETE CITY--
CREATE PROCEDURE PR_LOC_City_Delete
    @CityID INT
AS
BEGIN
    DELETE FROM City
    WHERE CityID = @CityID
END

--DROP DOWN FOR GET ALL COUNTRY--
CREATE PROCEDURE [dbo].[PR_LOC_Country_SelectComboBox]
AS 
SELECT
    COUNTRYID,
    COUNTRYNAME
FROM COUNTRY
ORDER BY COUNTRYNAME

--DROP DOWN FOR GET STATE BY COUNTRYID--
CREATE PROCEDURE [dbo].[PR_LOC_State_SelectComboBoxByCountryID]
@CountryID INT
AS 
SELECT
    [dbo].[State].[StateID],
    [dbo].[State].[StateName]
FROM [dbo].[State]
WHERE [dbo].[State].[CountryID] = @CountryID


-----------Country---------------------
--GET ALL Country by ID--
ALTER PROCEDURE [dbo].[PR_LOC_Country_SelectByPK]
    @CountryID INT
AS
BEGIN
    SELECT 
        CountryID, 
        CountryName, 
        CountryCode
    FROM Country
    WHERE CountryID = @CountryID;
END;


--Insert Country---
Alter PROCEDURE [dbo].[pr_LOC_Country_Insert]
    @CountryName NVARCHAR(100),
    @CountryCode NVARCHAR(10),
	@Created DATETIME,
	@Modified DATETIME
AS
BEGIN
    INSERT INTO Country (CountryName, CountryCode, CreatedDate,ModifiedDate)
    VALUES (@CountryName, @CountryCode,ISNULL(@Created,GETDATE()),ISNULL(@Modified,GETDATE()));
END;

--Update Country---
Alter PROCEDURE [dbo].[pr_LOC_Country_Update]
    @CountryID INT,
    @CountryName NVARCHAR(100),
    @CountryCode NVARCHAR(10),
	@Modified DATETIME
AS
BEGIN
    UPDATE Country
    SET CountryName = @CountryName,
        CountryCode = @CountryCode,
        ModifiedDate = GETDATE()
    WHERE CountryID = @CountryID;
END;

---Delete---
CREATE PROCEDURE [dbo].[pr_LOC_Country_Delete]
    @CountryID INT
AS
BEGIN
    DELETE FROM Country
    WHERE CountryID = @CountryID;
END;

----Select All------
CREATE PROCEDURE [dbo].[PR_LOC_Country_SelectAll]
AS
BEGIN
    SELECT 
        CountryID,
        CountryName,
        CountryCode,
        CreatedDate,
        ModifiedDate
    FROM 
        Country
    ORDER BY 
        CountryName;
END;

-----State-------
--Get All State by SelectAll
Alter PROCEDURE [dbo].[PR_LOC_STATE_SELECTALL]
AS
BEGIN
    SELECT StateID, CountryID, StateName, StateCode, CreatedDate, ModifiedDate
    FROM State
	ORDER BY 
        StateName;;
END

--SELECT BY PK----
Alter PROCEDURE [dbo].[PR_LOC_STATE_SELECTBYPK]
    @StateID INT
AS
BEGIN
    SELECT StateID, CountryID, StateName, StateCode
    FROM State
    WHERE StateID = @StateID;
END

----INSERT-----
Alter PROCEDURE [dbo].[PR_LOC_STATE_INSERT]
    @CountryID INT,
    @StateName NVARCHAR(100),
    @StateCode NVARCHAR(10) = NULL,
	@Created DATETIME,
	@Modified DATETIME
AS
BEGIN
    INSERT INTO State (CountryID, StateName, StateCode, CreatedDate,ModifiedDate)
    VALUES (@CountryID, @StateName, @StateCode,ISNULL(@Created,GETDATE()),ISNULL(@Modified,GETDATE()));
END

----UPDATE----
ALTER PROCEDURE [dbo].[PR_LOC_STATE_UPDATE]
    @StateID INT,
    @CountryID INT,
    @StateName NVARCHAR(100),
    @StateCode NVARCHAR(10) = NULL,
	@Modified DATETIME
AS
BEGIN
    UPDATE State
    SET CountryID = @CountryID,
        StateName = @StateName,
        StateCode = @StateCode,
        ModifiedDate = GETDATE()
    WHERE StateID = @StateID;
END


----DELETE-----
CREATE PROCEDURE [dbo].[PR_LOC_STATE_DELETE]
    @StateID INT
AS
BEGIN
    DELETE FROM State WHERE StateID = @StateID;
END


Select * from City
Select * from Country
Select * from State
Select * from Bills