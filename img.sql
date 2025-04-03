alter table destinations
Add ImageUrl varchar(500)  
select * from destinations
UPDATE Destinations 
set ImageUrl='https://hips.hearstapps.com/hmg-prod/images/maldives-girls-holiday-1-66b5d876941be.jpg'
where DestinationID=1;
SELECT DestinationName, ImageUrl FROM Destinations;


alter PROCEDURE [dbo].[PR_Destination_SelectAll]
AS
SELECT [dbo].[Destinations].[DestinationID]
      ,[dbo].[Destinations].[DestinationName]
	  ,[dbo].[Destinations].[ImageUrl]
      ,[dbo].[Destinations].[Country]
      ,[dbo].[Destinations].[State]
      ,[dbo].[Destinations].[Description]
      ,[dbo].[Destinations].[BestTimeToVisit]
FROM [dbo].[Destinations]
ORDER BY [dbo].[Destinations].[DestinationName]


alter PROCEDURE [dbo].[PR_Destination_SelectByPK] 1
@DestinationID INT
AS
SELECT [dbo].[Destinations].[DestinationID]
      ,[dbo].[Destinations].[DestinationName]
	   ,[dbo].[Destinations].[ImageUrl]
      ,[dbo].[Destinations].[Country]
      ,[dbo].[Destinations].[State]
      ,[dbo].[Destinations].[Description]
      ,[dbo].[Destinations].[BestTimeToVisit]
FROM [dbo].[Destinations]
WHERE [dbo].[Destinations].[DestinationID] = @DestinationID

alter PROCEDURE [dbo].[PR_Destination_Insert]
@DestinationName VARCHAR(255),
@ImageUrl varchar(500),
@Country VARCHAR(100),
@State NVARCHAR(100),
@Description NVARCHAR(255),
@BestTimeToVisit VARCHAR(100)
AS
INSERT INTO [dbo].[Destinations]
([DestinationName], [ImageUrl],[Country], [State], [Description], [BestTimeToVisit])
VALUES
(@DestinationName, @ImageUrl ,@Country, @State, @Description, @BestTimeToVisit)


alter PROCEDURE [dbo].[PR_Destination_UpdateByPK]
@DestinationID INT,
@DestinationName VARCHAR(255),
@ImageUrl varchar(500),
@Country VARCHAR(100),
@State NVARCHAR(100),
@Description NVARCHAR(255),
@BestTimeToVisit VARCHAR(100)
AS
UPDATE [dbo].[Destinations]
SET [DestinationName] = @DestinationName,
	[ImageUrl]=@ImageUrl,
    [Country] = @Country,
    [State] = @State,
    [Description] = @Description,
    [BestTimeToVisit] = @BestTimeToVisit
WHERE [dbo].[Destinations].[DestinationID] = @DestinationID

alter table Packages
Add ImageUrl varchar(500) 
select * from packages
UPDATE packages 
set ImageUrl='https://hips.hearstapps.com/hmg-prod/images/maldives-girls-holiday-1-66b5d876941be.jpg'
where PackageID=1;
SELECT PackageName, ImageUrl FROM Packages;



alter PROCEDURE [dbo].[PR_Package_SelectAll]
AS
SELECT [dbo].[Packages].[PackageID]
      ,[dbo].[Packages].[PackageName]
	  ,[dbo].[Packages].[ImageUrl]
      ,[dbo].[Packages].[Description]
	  ,[dbo].[Packages].[DestinationID]
	  ,[dbo].[Destinations].[DestinationName]     
	  ,[dbo].[Packages].[Price]
      ,[dbo].[Packages].[Duration]
      ,[dbo].[Packages].[Status]
FROM [dbo].[Packages]
INNER JOIN [dbo].[Destinations]
ON [dbo].[Destinations].[DestinationID] = [dbo].[Packages].[DestinationID]
ORDER BY [dbo].[Packages].[PackageName]


alter PROCEDURE [dbo].[PR_Package_SelectByPK]
@PackageID INT
AS
SELECT [dbo].[Packages].[PackageID]
      ,[dbo].[Packages].[PackageName]
	   ,[dbo].[Packages].[ImageUrl]
      ,[dbo].[Packages].[Description]
      ,[dbo].[Packages].[DestinationID]
	  ,[dbo].[Destinations].[DestinationName]
      ,[dbo].[Packages].[Price]
      ,[dbo].[Packages].[Duration]
      ,[dbo].[Packages].[Status]
FROM [dbo].[Packages]
INNER JOIN [dbo].[Destinations]
ON [dbo].[Destinations].[DestinationID] = [dbo].[Packages].[DestinationID]
WHERE [dbo].[Packages].[PackageID] = @PackageID



alter PROCEDURE [dbo].[PR_Package_Insert]
@PackageName VARCHAR(255),
@ImageUrl varchar(500),
@Description NVARCHAR(255),
@DestinationID INT,
@Price DECIMAL(10, 2),
@Duration INT,
@Status VARCHAR(20)
AS
INSERT INTO [dbo].[Packages]
([PackageName], [ImageUrl],[Description], [DestinationID], [Price], [Duration], [Status])
VALUES
(@PackageName, @ImageUrl,@Description, @DestinationID, @Price, @Duration, @Status)
exec [PR_Package_Insert] 'Beach','Relax',1,200.2,1,'Not'

alter PROCEDURE [dbo].[PR_Package_UpdateByPK]
@PackageID INT,
@ImageUrl varchar(500),
@PackageName VARCHAR(255),
@Description NVARCHAR(255),
@DestinationID INT,
@Price DECIMAL(10, 2),
@Duration INT,
@Status VARCHAR(20)
AS
UPDATE [dbo].[Packages]
SET [PackageName] = @PackageName,
	[ImageUrl]=@ImageUrl,
    [Description] = @Description,
    [DestinationID] = @DestinationID,
    [Price] = @Price,
    [Duration] = @Duration,
    [Status] = @Status
WHERE [dbo].[Packages].[PackageID] = @PackageID
exec [PR_Package_UpdateByPK] 1,'Beach l','Relax',1,200.2,1,'Not'






alter table itinerary
Add ImageUrl varchar(500) 
select * from itinerary
UPDATE itinerary 
set ImageUrl='https://hips.hearstapps.com/hmg-prod/images/maldives-girls-holiday-1-66b5d876941be.jpg'
where ItineraryID=1;
SELECT PackageID, ImageUrl FROM itinerary;

[PR_Itinerary_SelectAll]

alter PROCEDURE [dbo].[PR_Itinerary_SelectAll] 
AS
BEGIN
    SELECT 
        [dbo].[Itinerary].ItineraryID,
        [dbo].[Packages].PackageID,
		[dbo].[Packages].[PackageName],
		[dbo].[Itinerary].[ImageUrl],
        [dbo].[Itinerary].DayNumber,
        [dbo].[Itinerary].Activity,
        [dbo].[Itinerary].Location,
        [dbo].[Itinerary].Time
    FROM 
        [dbo].[Itinerary]
    INNER JOIN 
        [dbo].[Packages] ON [dbo].[Itinerary].PackageID = [dbo].[Packages].PackageID
    ORDER BY 
        [dbo].[Itinerary].DayNumber;
END;


alter PROCEDURE [dbo].[PR_Itinerary_SelectByPK]
@ItineraryID INT
AS
BEGIN
    SELECT 
        [dbo].[Itinerary].ItineraryID,
        [dbo].[Packages].PackageID,
		[dbo].[Packages].PackageName,
		[dbo].[Itinerary].ImageUrl,
        [dbo].[Itinerary].DayNumber,
        [dbo].[Itinerary].Activity,
        [dbo].[Itinerary].Location,
        [dbo].[Itinerary].Time
    FROM 
        [dbo].[Itinerary]
    INNER JOIN 
        [dbo].[Packages] ON [dbo].[Itinerary].PackageID = [dbo].[Packages].PackageID
    WHERE 
        [dbo].[Itinerary].ItineraryID = @ItineraryID;
END;

alter PROCEDURE [dbo].[PR_Itinerary_Insert]
@PackageID INT,
@ImageUrl varchar(500),
@DayNumber INT,
@Activity NVARCHAR(255),
@Location VARCHAR(255)
AS
INSERT INTO [dbo].[Itinerary]
([PackageID], [ImageUrl],[DayNumber], [Activity], [Location], [Time])
VALUES
(@PackageID, @ImageUrl,@DayNumber, @Activity, @Location, GETDATE())

PR_Itinerary_SelectAll
alter PROCEDURE [dbo].[PR_Itinerary_UpdateByPK]
@ItineraryID INT,
@PackageID INT,
@ImageUrl varchar(500),
@DayNumber INT,
@Activity NVARCHAR(255),
@Location VARCHAR(255)
AS
UPDATE [dbo].[Itinerary]
SET [PackageID] = @PackageID,
	[ImageUrl]=@ImageUrl,
    [DayNumber] = @DayNumber,
    [Activity] = @Activity,
    [Location] = @Location,
    [Time] = GETDATE()
WHERE [dbo].[Itinerary].[ItineraryID] = @ItineraryID


[PR_Feedback_SelectAll]



alter table customers
Add ImageUrl varchar(500)  
select * from customers
UPDATE customers 
set ImageUrl='https://www.edesk.com/wp-content/uploads/2021/06/changing-face-customer-service.jpg'
where CustomerID=3;


alter PROCEDURE [dbo].[PR_Customer_SelectAll]
AS
SELECT [dbo].[Customers].[CustomerID]
      ,[dbo].[Customers].[FullName]
	   ,[dbo].[Customers].[ImageUrl]
      ,[dbo].[Customers].[Email]
      ,[dbo].[Customers].[Phone]
      ,[dbo].[Customers].[Address]
      ,[dbo].[Customers].[RegistrationDate]
	  ,[dbo].[Customers].[Password]
FROM [dbo].[Customers]
ORDER BY [dbo].[Customers].[FullName]

EXEC [dbo].[PR_Customer_SelectAll]

alter PROCEDURE [dbo].[PR_Customer_SelectByPK]
@CustomerID INT
AS
SELECT [dbo].[Customers].[CustomerID]
      ,[dbo].[Customers].[FullName]
	   ,[dbo].[Customers].[ImageUrl]
      ,[dbo].[Customers].[Email]
      ,[dbo].[Customers].[Phone]
      ,[dbo].[Customers].[Address]
      ,[dbo].[Customers].[RegistrationDate]
	  	  ,[dbo].[Customers].[Password]

FROM [dbo].[Customers]
WHERE [dbo].[Customers].[CustomerID] = @CustomerID

[dbo].[PR_Customer_SelectByPK] 1

alter PROCEDURE [dbo].[PR_Customer_Insert]
@FullName VARCHAR(255),
@ImageUrl VARCHAR(500),
@Email VARCHAR(255),
@Phone VARCHAR(20),
@Address NVARCHAR(255),
 @Password NVARCHAR(255)
AS
INSERT INTO [dbo].[Customers]
([FullName],[ImageUrl], [Email], [Phone], [Address], [Password],[RegistrationDate])
VALUES
(@FullName,@ImageUrl, @Email, @Phone, @Address,@Password, GETDATE())

EXEC PR_Customer_Insert
    @FullName = 'Krishna Vara',
    @ImageUrl = 'https://www.edesk.com/wp-content/uploads/2021/06/changing-face-customer-service.jpg',
    @Email = 'kr@example.com',
    @Phone = '9876543210',
    @Address = 'Hyderabad, India',
	@Password='kr123';


exec [dbo].[PR_Customer_Insert] 'Abc','abc@gmail.com','123456789','abc eaj'
pr_customer_selectall

alter PROCEDURE [dbo].[PR_Customer_UpdateByPK]
@CustomerID INT,
@FullName VARCHAR(255),
@ImageUrl VARCHAR(500),
@Email VARCHAR(255),
@Phone VARCHAR(20),
@Address NVARCHAR(255),
 @Password NVARCHAR(255)
AS
UPDATE [dbo].[Customers]
SET [FullName] = @FullName,
	[ImageUrl]=@ImageUrl,	
    [Email] = @Email,
    [Phone] = @Phone,
    [Address] = @Address,
	[Password]=@Password
WHERE [dbo].[Customers].[CustomerID] = @CustomerID

