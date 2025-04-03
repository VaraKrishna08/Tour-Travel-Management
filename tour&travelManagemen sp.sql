alter PROCEDURE [dbo].[PR_Customer_SelectAll]
AS
SELECT [dbo].[Customers].[CustomerID]
      ,[dbo].[Customers].[FullName]
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
      ,[dbo].[Customers].[Email]
      ,[dbo].[Customers].[Phone]
      ,[dbo].[Customers].[Address]
      ,[dbo].[Customers].[RegistrationDate]
	  	  ,[dbo].[Customers].[Password]

FROM [dbo].[Customers]
WHERE [dbo].[Customers].[CustomerID] = @CustomerID

[dbo].[PR_Customer_SelectByPK] 1

CREATE PROCEDURE [dbo].[PR_Customer_Insert]
@FullName VARCHAR(255),
@Email VARCHAR(255),
@Phone VARCHAR(20),
@Address NVARCHAR(255)
AS
INSERT INTO [dbo].[Customers]
([FullName], [Email], [Phone], [Address], [RegistrationDate])
VALUES
(@FullName, @Email, @Phone, @Address, GETDATE())

exec [dbo].[PR_Customer_Insert] 'Abc','abc@gmail.com','123456789','abc eaj'
pr_customer_selectall
CREATE PROCEDURE [dbo].[PR_Customer_UpdateByPK]
@CustomerID INT,
@FullName VARCHAR(255),
@Email VARCHAR(255),
@Phone VARCHAR(20),
@Address NVARCHAR(255)
AS
UPDATE [dbo].[Customers]
SET [FullName] = @FullName,
    [Email] = @Email,
    [Phone] = @Phone,
    [Address] = @Address
WHERE [dbo].[Customers].[CustomerID] = @CustomerID

exec [dbo].[PR_Customer_UpdateByPK] 4,'Emily davis','emily@gmail.com','123456789','12 abc Jamnagar,Gujrat'

CREATE PROCEDURE [dbo].[PR_Customer_DeleteByPK]
@CustomerID INT
AS
DELETE FROM [dbo].[Customers]
WHERE [dbo].[Customers].[CustomerID] = @CustomerID


CREATE PROCEDURE [dbo].[PR_Destination_SelectAll]
AS
SELECT [dbo].[Destinations].[DestinationID]
      ,[dbo].[Destinations].[DestinationName]
      ,[dbo].[Destinations].[Country]
      ,[dbo].[Destinations].[State]
      ,[dbo].[Destinations].[Description]
      ,[dbo].[Destinations].[BestTimeToVisit]
FROM [dbo].[Destinations]
ORDER BY [dbo].[Destinations].[DestinationName]


CREATE PROCEDURE [dbo].[PR_Destination_SelectByPK]
@DestinationID INT
AS
SELECT [dbo].[Destinations].[DestinationID]
      ,[dbo].[Destinations].[DestinationName]
      ,[dbo].[Destinations].[Country]
      ,[dbo].[Destinations].[State]
      ,[dbo].[Destinations].[Description]
      ,[dbo].[Destinations].[BestTimeToVisit]
FROM [dbo].[Destinations]
WHERE [dbo].[Destinations].[DestinationID] = @DestinationID
[PR_Destination_SelectByPK] 10

CREATE PROCEDURE [dbo].[PR_Destination_Insert]
@DestinationName VARCHAR(255),
@Country VARCHAR(100),
@State NVARCHAR(100),
@Description NVARCHAR(255),
@BestTimeToVisit VARCHAR(100)
AS
INSERT INTO [dbo].[Destinations]
([DestinationName], [Country], [State], [Description], [BestTimeToVisit])
VALUES
(@DestinationName, @Country, @State, @Description, @BestTimeToVisit)

exec [PR_Destination_Insert] 'Maldives','usa','usa','Tropical paradise with beautiful beaches','Nov to April'

CREATE PROCEDURE [dbo].[PR_Destination_UpdateByPK]
@DestinationID INT,
@DestinationName VARCHAR(255),
@Country VARCHAR(100),
@State NVARCHAR(100),
@Description NVARCHAR(255),
@BestTimeToVisit VARCHAR(100)
AS
UPDATE [dbo].[Destinations]
SET [DestinationName] = @DestinationName,
    [Country] = @Country,
    [State] = @State,
    [Description] = @Description,
    [BestTimeToVisit] = @BestTimeToVisit
WHERE [dbo].[Destinations].[DestinationID] = @DestinationID

exec [PR_Destination_UpdateByPK] 1,'Maldives','Maldives','Mal','Tropical paradise with beautiful beaches','Nov to April'

CREATE PROCEDURE [dbo].[PR_Destination_DeleteByPK]
@DestinationID INT
AS
DELETE FROM [dbo].[Destinations]
WHERE [dbo].[Destinations].[DestinationID] = @DestinationID

exec [PR_Destination_DeleteByPK] 11

--Packages--

CREATE PROCEDURE [dbo].[PR_Package_SelectAll]
AS
SELECT [dbo].[Packages].[PackageID]
      ,[dbo].[Packages].[PackageName]
      ,[dbo].[Packages].[Description]
      ,[dbo].[Packages].[DestinationID]
      ,[dbo].[Packages].[Price]
      ,[dbo].[Packages].[Duration]
      ,[dbo].[Packages].[Status]
FROM [dbo].[Packages]
INNER JOIN [dbo].[Destinations]
ON [dbo].[Destinations].[DestinationID] = [dbo].[Packages].[DestinationID]
ORDER BY [dbo].[Packages].[PackageName]


alter PROCEDURE [dbo].[PR_Package_SelectAll]
AS
SELECT [dbo].[Packages].[PackageID]
      ,[dbo].[Packages].[PackageName]
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

CREATE PROCEDURE [dbo].[PR_Package_SelectByPK]
@PackageID INT
AS
SELECT [dbo].[Packages].[PackageID]
      ,[dbo].[Packages].[PackageName]
      ,[dbo].[Packages].[Description]
      ,[dbo].[Packages].[DestinationID]
      ,[dbo].[Packages].[Price]
      ,[dbo].[Packages].[Duration]
      ,[dbo].[Packages].[Status]
FROM [dbo].[Packages]
INNER JOIN [dbo].[Destinations]
ON [dbo].[Destinations].[DestinationID] = [dbo].[Packages].[DestinationID]
WHERE [dbo].[Packages].[PackageID] = @PackageID
[PR_Package_SelectByPK] 11

alter PROCEDURE [dbo].[PR_Package_SelectByPK]
@PackageID INT
AS
SELECT [dbo].[Packages].[PackageID]
      ,[dbo].[Packages].[PackageName]
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



CREATE PROCEDURE [dbo].[PR_Package_Insert]
@PackageName VARCHAR(255),
@Description NVARCHAR(255),
@DestinationID INT,
@Price DECIMAL(10, 2),
@Duration INT,
@Status VARCHAR(20)
AS
INSERT INTO [dbo].[Packages]
([PackageName], [Description], [DestinationID], [Price], [Duration], [Status])
VALUES
(@PackageName, @Description, @DestinationID, @Price, @Duration, @Status)
exec [PR_Package_Insert] 'Beach','Relax',1,200.2,1,'Not'

CREATE PROCEDURE [dbo].[PR_Package_UpdateByPK]
@PackageID INT,
@PackageName VARCHAR(255),
@Description NVARCHAR(255),
@DestinationID INT,
@Price DECIMAL(10, 2),
@Duration INT,
@Status VARCHAR(20)
AS
UPDATE [dbo].[Packages]
SET [PackageName] = @PackageName,
    [Description] = @Description,
    [DestinationID] = @DestinationID,
    [Price] = @Price,
    [Duration] = @Duration,
    [Status] = @Status
WHERE [dbo].[Packages].[PackageID] = @PackageID
exec [PR_Package_UpdateByPK] 1,'Beach l','Relax',1,200.2,1,'Not'

CREATE PROCEDURE [dbo].[PR_Package_DeleteByPK]
@PackageID INT
AS
DELETE FROM [dbo].[Packages]
WHERE [dbo].[Packages].[PackageID] = @PackageID
[PR_Package_DeleteByPK] 11

-- 1. Bookings Procedures

create PROC [dbo].[PR_Booking_SelectAll]
AS
BEGIN
    SELECT 
        [dbo].[Bookings].BookingID, 
        [dbo].[Customers].CustomerID, 
		[dbo].[Customers].FullName,
        [dbo].[Packages].PackageID,
		[dbo].[Packages].PackageName,
        [dbo].[Bookings].BookingDate, 
        [dbo].[Bookings].TravelDate, 
        [dbo].[Bookings].NumberOfPeople, 
        [dbo].[Bookings].TotalAmount, 
        [dbo].[Bookings].Status
    FROM 
        [dbo].[Bookings]
    INNER JOIN 
        [dbo].[Customers] ON [dbo].[Bookings].CustomerID = [dbo].[Customers].CustomerID
    INNER JOIN 
        [dbo].[Packages] ON [dbo].[Bookings].PackageID = [dbo].[Packages].PackageID
    ORDER BY 
        [dbo].[Bookings].BookingDate;
END;
EXEC [dbo].[PR_Booking_SelectAll];

create PROC [dbo].[PR_Booking_SelectByPK]
@BookingID INT
AS
BEGIN
    SELECT 
        [dbo].[Bookings].BookingID, 
        [dbo].[Customers].CustomerID, 
		[dbo].[Customers].FullName,
        [dbo].[Packages].PackageID, 
		[dbo].[Packages].PackageName,
        [dbo].[Bookings].BookingDate, 
        [dbo].[Bookings].TravelDate, 
        [dbo].[Bookings].NumberOfPeople, 
        [dbo].[Bookings].TotalAmount, 
        [dbo].[Bookings].Status
    FROM 
        [dbo].[Bookings]
    INNER JOIN 
        [dbo].[Customers] ON [dbo].[Bookings].CustomerID = [dbo].[Customers].CustomerID
    INNER JOIN 
        [dbo].[Packages] ON [dbo].[Bookings].PackageID = [dbo].[Packages].PackageID
    WHERE 
        [dbo].[Bookings].BookingID = @BookingID;
END;
EXEC [dbo].[PR_Booking_SelectByPK] 1;

CREATE PROC [dbo].[PR_Booking_Insert]
@CustomerID INT,
@PackageID INT,
@TravelDate DATE,
@NumberOfPeople INT,
@TotalAmount DECIMAL(10, 2),
@Status NVARCHAR(50)
AS
BEGIN
    INSERT INTO [dbo].[Bookings]
	([CustomerID], [PackageID], [BookingDate], [TravelDate], [NumberOfPeople], [TotalAmount], [Status])
    VALUES (@CustomerID, @PackageID, GETDATE(), @TravelDate, @NumberOfPeople, @TotalAmount, @Status);
END;
EXEC [dbo].[PR_Booking_Insert] 1, 2, '2025-01-05', 4, 1000.00, 'Confirmed';

CREATE PROC [dbo].[PR_Booking_UpdateByPK]
@BookingID INT,
@CustomerID INT,
@PackageID INT,
@TravelDate DATETIME,
@NumberOfPeople INT,
@TotalAmount DECIMAL(10, 2),
@Status NVARCHAR(50)
AS
BEGIN
    UPDATE [dbo].[Bookings]
    SET 
        [CustomerID] = @CustomerID,
        [PackageID] = @PackageID,
        [TravelDate] = @TravelDate,
        [NumberOfPeople] = @NumberOfPeople,
        [TotalAmount] = @TotalAmount,
        [Status] = @Status
    WHERE [dbo].[Bookings].[BookingID] = @BookingID;
END;
EXEC [dbo].[PR_Booking_UpdateByPK] 1, 1, 1, '2025-02-15', 3, 2500.00, 'Pending';

CREATE PROC [dbo].[PR_Booking_DeleteByPK]
@BookingID INT
AS
BEGIN
    DELETE FROM [dbo].[Bookings] WHERE [dbo].[Bookings].[BookingID] = @BookingID;
END;
EXEC [dbo].[PR_Booking_DeleteByPK] 1;
[dbo].[PR_Booking_SelectAll]

select * from destinations
create PROCEDURE [dbo].[PR_Itinerary_SelectAll] 
AS
BEGIN
    SELECT 
        [dbo].[Itinerary].ItineraryID,
        [dbo].[Packages].PackageID,
		[dbo].[Packages].[PackageName],
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

create PROCEDURE [dbo].[PR_Itinerary_SelectByPK]
@ItineraryID INT
AS
BEGIN
    SELECT 
        [dbo].[Itinerary].ItineraryID,
        [dbo].[Packages].PackageID,
		[dbo].[Packages].PackageName,
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
[PR_Itinerary_SelectByPK] 2
select * from Packages

ALTER TABLE Itinerary
DROP CONSTRAINT IF EXISTS DF_Itinerary_Time;
ALTER TABLE Itinerary
ALTER COLUMN Time DATETIME;

ALTER TABLE Itinerary
ADD DEFAULT GETDATE() FOR Time;

-- Step 1: Drop the default constraint (replace constraint name if different)
ALTER TABLE Itinerary
DROP CONSTRAINT DF__Itinerary__Time__52593CB8;
SELECT name
FROM sys.default_constraints
WHERE parent_object_id = OBJECT_ID('Itinerary')
  AND COL_NAME(parent_object_id, parent_column_id) = 'Time';
  ALTER TABLE Itinerary
DROP CONSTRAINT DF__Itinerary__Time__44FF419A;

-- Step 2: Alter the column type to DATETIME
ALTER TABLE Itinerary
ALTER COLUMN Time DATETIME;

-- Step 3: Add the default value for the Time column
ALTER TABLE Itinerary
ADD CONSTRAINT DF_Itinerary_Time DEFAULT GETDATE() FOR Time;

CREATE PROCEDURE [dbo].[PR_Itinerary_Insert]
@PackageID INT,
@DayNumber INT,
@Activity NVARCHAR(255),
@Location VARCHAR(255)
AS
INSERT INTO [dbo].[Itinerary]
([PackageID], [DayNumber], [Activity], [Location], [Time])
VALUES
(@PackageID, @DayNumber, @Activity, @Location, GETDATE())
[PR_Itinerary_Insert] 1,3,'Island','Abc'

PR_Itinerary_SelectAll
CREATE PROCEDURE [dbo].[PR_Itinerary_UpdateByPK]
@ItineraryID INT,
@PackageID INT,
@DayNumber INT,
@Activity NVARCHAR(255),
@Location VARCHAR(255)
AS
UPDATE [dbo].[Itinerary]
SET [PackageID] = @PackageID,
    [DayNumber] = @DayNumber,
    [Activity] = @Activity,
    [Location] = @Location,
    [Time] = GETDATE()
WHERE [dbo].[Itinerary].[ItineraryID] = @ItineraryID
[PR_Itinerary_UpdateByPK] 1,3,2,'Island','Abc'

CREATE PROCEDURE [dbo].[PR_Itinerary_DeleteByPK] 
@ItineraryID INT
AS
DELETE FROM [dbo].[Itinerary]
WHERE [dbo].[Itinerary].[ItineraryID] = @ItineraryID

-- Procedure to select all payments
create PROCEDURE [dbo].[PR_Payment_SelectAll] 
AS
BEGIN
    SELECT 
        [dbo].[Payment].PaymentID,
        [dbo].[Bookings].BookingID,
		[dbo].[Bookings].BookingDate,
		[dbo].[Payment].PaymentDate,
        [dbo].[Payment].PaymentMode,
        [dbo].[Payment].AmountPaid,
        [dbo].[Payment].PaymentStatus
    FROM 
        [dbo].[Payment]
    INNER JOIN 
        [dbo].[Bookings] ON [dbo].[Payment].BookingID = [dbo].[Bookings].BookingID
END;
select * from payment
EXEC [dbo].[PR_Payment_SelectAll];


CREATE PROCEDURE [dbo].[PR_Payment_SelectByPK]
@PaymentID INT
AS
BEGIN
    SELECT 
        [dbo].[Payment].PaymentID,
        [dbo].[Bookings].BookingID,
		[dbo].[Payment].[PaymentDate],
        [dbo].[Payment].PaymentMode,
        [dbo].[Payment].AmountPaid,
        [dbo].[Payment].PaymentStatus
    FROM 
        [dbo].[Payment]
    INNER JOIN 
        [dbo].[Bookings] ON [dbo].[Payment].BookingID = [dbo].[Bookings].BookingID
    WHERE 
        [dbo].[Payment].PaymentID = @PaymentID;
END;

EXEC [dbo].[PR_Payment_SelectByPK] 1;

alter PROCEDURE [dbo].[PR_Payment_SelectByPK]
@PaymentID INT
AS
BEGIN
    SELECT 
        [dbo].[Payment].PaymentID,
        [dbo].[Bookings].BookingID,
		[dbo].[Bookings].BookingDate,
		[dbo].[Payment].[PaymentDate],
        [dbo].[Payment].PaymentMode,
        [dbo].[Payment].AmountPaid,
        [dbo].[Payment].PaymentStatus
    FROM 
        [dbo].[Payment]
    INNER JOIN 
        [dbo].[Bookings] ON [dbo].[Payment].BookingID = [dbo].[Bookings].BookingID
    WHERE 
        [dbo].[Payment].PaymentID = @PaymentID;
END;

-- Procedure to insert a new payment
CREATE PROCEDURE [dbo].[PR_Payment_Insert]
@BookingID INT,
@PaymentMode NVARCHAR(50),
@AmountPaid DECIMAL(18, 2),
@PaymentStatus NVARCHAR(50)
AS
INSERT INTO [dbo].[Payment]
([BookingID],[PaymentDate],[PaymentMode], [AmountPaid], [PaymentStatus])
VALUES
(@BookingID,GETDATE(), @PaymentMode, @AmountPaid, @PaymentStatus);

EXEC [dbo].[PR_Payment_Insert] 1, 'Card', 500.00, 'Completed';
[PR_Payment_SelectAll]
-- Procedure to update a payment by PaymentID
CREATE PROCEDURE [dbo].[PR_Payment_UpdateByPK]
@PaymentID INT,
@BookingID INT,
@PaymentMode NVARCHAR(50),
@AmountPaid DECIMAL(18, 2),
@PaymentStatus NVARCHAR(50)
AS
UPDATE [dbo].[Payment]
SET [BookingID] = @BookingID,
	[PaymentDate]=GETDATE(),
    [PaymentMode] = @PaymentMode,
    [AmountPaid] = @AmountPaid,
    [PaymentStatus] = @PaymentStatus
WHERE [dbo].[Payment].[PaymentID] = @PaymentID;

EXEC [dbo].[PR_Payment_UpdateByPK] 11, 1, 'Debit Card', 600.00, 'Pending';

-- Procedure to delete a payment by PaymentID
CREATE PROCEDURE [dbo].[PR_Payment_DeleteByPK]
@PaymentID INT
AS
DELETE FROM [dbo].[Payment]
WHERE [dbo].[Payment].[PaymentID] = @PaymentID;

EXEC [dbo].[PR_Payment_DeleteByPK] 1;

-- Select All Records from Transportation Table
create PROCEDURE [dbo].[PR_Transportation_SelectAll]
AS
BEGIN
    SELECT 
        [dbo].[Transportation].TransportID,
        [dbo].[Bookings]. BookingID,
		[dbo].[Bookings].BookingDate,
        [dbo].[Transportation].TransportMode,
        [dbo].[Transportation].TransportDetails,
        [dbo].[Transportation].DepartureTime,
        [dbo].[Transportation].ArrivalTime,
        [dbo].[Transportation].Cost
    FROM 
        [dbo].[Transportation]
    INNER JOIN 
        [dbo].[Bookings] ON [dbo].[Transportation].BookingID = [dbo].[Bookings].BookingID
    ORDER BY 
        [dbo].[Transportation].TransportMode;
END;

EXEC [dbo].[PR_Transportation_SelectAll];

-- Select a Single Record by Primary Key
create PROCEDURE [dbo].[PR_Transportation_SelectByPK]
@TransportID INT
AS
BEGIN
    SELECT 
        [dbo].[Transportation].TransportID,
        [dbo].[Bookings].BookingID,
				[dbo].[Bookings].BookingDate,

        [dbo].[Transportation].TransportMode,
        [dbo].[Transportation].TransportDetails,
        [dbo].[Transportation].DepartureTime,
        [dbo].[Transportation].ArrivalTime,
        [dbo].[Transportation].Cost
    FROM 
        [dbo].[Transportation]
    INNER JOIN 
        [dbo].[Bookings] ON [dbo].[Transportation].BookingID = [dbo].[Bookings].BookingID
    WHERE 
        [dbo].[Transportation].TransportID = @TransportID;
END;

EXEC [dbo].[PR_Transportation_SelectByPK] 1;

-- Insert a New Record into Transportation Table
CREATE PROCEDURE [dbo].[PR_Transportation_Insert]
@BookingID INT,
@TransportMode NVARCHAR(100),
@TransportDetails NVARCHAR(255),
@DepartureTime TIME,
@ArrivalTime DATETIME,
@Cost DECIMAL(10, 2)
AS
INSERT INTO [dbo].[Transportation]
([BookingID], [TransportMode], [TransportDetails], [DepartureTime], [ArrivalTime], [Cost])
VALUES
(@BookingID, @TransportMode, @TransportDetails, @DepartureTime, @ArrivalTime, @Cost);

EXEC [dbo].[PR_Transportation_Insert] 3, 'Bus', 'AC Sleeper', '2025-01-20 08:00:00', '2025-01-20 14:00:00', 500.00;

-- Update an Existing Record by Primary Key
CREATE PROCEDURE [dbo].[PR_Transportation_UpdateByPK]
@TransportID INT,
@BookingID INT,
@TransportMode NVARCHAR(100),
@TransportDetails NVARCHAR(255),
@DepartureTime TIME,
@ArrivalTime DATETIME,
@Cost DECIMAL(10, 2)
AS
UPDATE [dbo].[Transportation]
SET [BookingID] = @BookingID,
    [TransportMode] = @TransportMode,
    [TransportDetails] = @TransportDetails,
    [DepartureTime] = @DepartureTime,
    [ArrivalTime] = @ArrivalTime,
    [Cost] = @Cost
WHERE [dbo].[Transportation].[TransportID] = @TransportID;

EXEC [dbo].[PR_Transportation_UpdateByPK] 3, 1, 'Train', 'First Class', '2025-01-20 07:00:00', '2025-01-20 12:00:00', 1200.00;

-- Delete a Record by Primary Key
CREATE PROCEDURE [dbo].[PR_Transportation_DeleteByPK]
@TransportID INT
AS
DELETE FROM [dbo].[Transportation]
WHERE [dbo].[Transportation].[TransportID] = @TransportID;

EXEC [dbo].[PR_Transportation_DeleteByPK] 1;

create PROCEDURE [dbo].[PR_Feedback_SelectAll]
AS
BEGIN
    SELECT 
        [dbo].[Feedback].FeedbackID,
        [dbo].[Customers].CustomerID,
		[dbo].[Customers].FullName,
        [dbo].[Packages].PackageID,
		[dbo].[Packages].PackageName,
        [dbo].[Feedback].Rating,
        [dbo].[Feedback].Comments,
        [dbo].[Feedback].FeedbackDate
    FROM 
        [dbo].[Feedback]
    INNER JOIN 
        [dbo].[Customers] ON [dbo].[Feedback].CustomerID = [dbo].[Customers].CustomerID
    INNER JOIN 
        [dbo].[Packages] ON [dbo].[Feedback].PackageID = [dbo].[Packages].PackageID
    ORDER BY 
        [dbo].[Feedback].FeedbackDate DESC;
END;

EXEC [dbo].[PR_Feedback_SelectAll];

create PROCEDURE [dbo].[PR_Feedback_SelectByPK]
@FeedbackID INT
AS
BEGIN
    SELECT 
        [dbo].[Feedback].FeedbackID,
        [dbo].[Customers].CustomerID,
				[dbo].[Customers].FullName,

        [dbo].[Packages].PackageID,
				[dbo].[Packages].PackageName,

        [dbo].[Feedback].Rating,
        [dbo].[Feedback].Comments,
        [dbo].[Feedback].FeedbackDate
    FROM 
        [dbo].[Feedback]
    INNER JOIN 
        [dbo].[Customers] ON [dbo].[Feedback].CustomerID = [dbo].[Customers].CustomerID
    INNER JOIN 
        [dbo].[Packages] ON [dbo].[Feedback].PackageID = [dbo].[Packages].PackageID
    WHERE 
        [dbo].[Feedback].FeedbackID = @FeedbackID;
END;

EXEC [dbo].[PR_Feedback_SelectByPK] 1;

CREATE PROCEDURE [dbo].[PR_Feedback_Insert]
@PackageID INT,
@CustomerID INT,
@Rating INT,
@Comments NVARCHAR(500)
AS
INSERT INTO [dbo].[Feedback]
([PackageID], [CustomerID] ,[Rating], [Comments], [FeedbackDate])
VALUES
(@PackageID,@CustomerID , @Rating, @Comments, GETDATE());

EXEC [dbo].[PR_Feedback_Insert] 1, 1, 5, 'Great experience!';
[PR_Feedback_SelectAll]
CREATE PROCEDURE [dbo].[PR_Feedback_UpdateByPK]
@FeedbackID INT,
@CustomerID INT,
@PackageID INT,
@Rating INT,
@Comments NVARCHAR(500)
AS
UPDATE [dbo].[Feedback]
SET [CustomerID] = @CustomerID,
    [PackageID] = @PackageID,
    [Rating] = @Rating,
    [Comments] = @Comments,
	[FeedbackDate]= GETDATE()
WHERE [dbo].[Feedback].[FeedbackID] = @FeedbackID;

EXEC [dbo].[PR_Feedback_UpdateByPK] 1, 1, 2, 4, 'Good but could be improved!';

CREATE PROCEDURE [dbo].[PR_Feedback_DeleteByPK]
@FeedbackID INT
AS
DELETE FROM [dbo].[Feedback]
WHERE [dbo].[Feedback].[FeedbackID] = @FeedbackID;

EXEC [dbo].[PR_Feedback_DeleteByPK] 1;

-- Procedure to select all employees
CREATE PROCEDURE [dbo].[PR_Employee_SelectAll]
AS
SELECT [dbo].[Employees].[EmployeeID]
      ,[dbo].[Employees].[FullName]
      ,[dbo].[Employees].[Position]
      ,[dbo].[Employees].[ContactNumber]
      ,[dbo].[Employees].[Email]
      ,[dbo].[Employees].[Salary]
FROM [dbo].[Employees]
ORDER BY [dbo].[Employees].[FullName]

EXEC [dbo].[PR_Employee_SelectAll]

-- Procedure to select an employee by primary key
CREATE PROCEDURE [dbo].[PR_Employee_SelectByPK]
@EmployeeID INT
AS
SELECT [dbo].[Employees].[EmployeeID]
      ,[dbo].[Employees].[FullName]
      ,[dbo].[Employees].[Position]
      ,[dbo].[Employees].[ContactNumber]
      ,[dbo].[Employees].[Email]
      ,[dbo].[Employees].[Salary]
FROM [dbo].[Employees]
WHERE [dbo].[Employees].[EmployeeID] = @EmployeeID

-- Example usage
EXEC [dbo].[PR_Employee_SelectByPK] 1

-- Procedure to insert a new employee
CREATE PROCEDURE [dbo].[PR_Employee_Insert]
@FullName VARCHAR(255),
@Position VARCHAR(100),
@ContactNumber VARCHAR(20),
@Email VARCHAR(255),
@Salary DECIMAL(18, 2)
AS
INSERT INTO [dbo].[Employees]
([FullName], [Position], [ContactNumber], [Email], [Salary])
VALUES
(@FullName, @Position, @ContactNumber, @Email, @Salary)

EXEC [dbo].[PR_Employee_Insert] 'John Doe', 'Manager', '1234567890', 'john.doe@example.com', 55000.00

-- Procedure to update an employee by primary key
CREATE PROCEDURE [dbo].[PR_Employee_UpdateByPK]
@EmployeeID INT,
@FullName VARCHAR(255),
@Position VARCHAR(100),
@ContactNumber VARCHAR(20),
@Email VARCHAR(255),
@Salary DECIMAL(18, 2)
AS
UPDATE [dbo].[Employees]
SET [FullName] = @FullName,
    [Position] = @Position,
    [ContactNumber] = @ContactNumber,
    [Email] = @Email,
    [Salary] = @Salary
WHERE [dbo].[Employees].[EmployeeID] = @EmployeeID
EXEC [dbo].[PR_Employee_UpdateByPK] 11, 'John Doe', 'Director', '0987654321', 'john.doe@example.com', 65000.00

-- Procedure to delete an employee by primary key
CREATE PROCEDURE [dbo].[PR_Employee_DeleteByPK]
@EmployeeID INT
AS
DELETE FROM [dbo].[Employees]
WHERE [dbo].[Employees].[EmployeeID] = @EmployeeID

-- Example usage
EXEC [dbo].[PR_Employee_DeleteByPK] 1

CREATE PROCEDURE [dbo].[PR_Offers_SelectAll]
AS
SELECT [dbo].[Offers].[OfferID],
       [dbo].[Offers].[OfferName],
       [dbo].[Offers].[Description],
       [dbo].[Offers].[DiscountPercentage],
       [dbo].[Offers].[StartDate],
       [dbo].[Offers].[EndDate],
       [dbo].[Offers].[ApplicablePackages]
FROM [dbo].[Offers]
ORDER BY [dbo].[Offers].[OfferName];

CREATE PROCEDURE [dbo].[PR_Offers_SelectByPK]
@OfferID INT
AS
SELECT [dbo].[Offers].[OfferID],
       [dbo].[Offers].[OfferName],
       [dbo].[Offers].[Description],
       [dbo].[Offers].[DiscountPercentage],
       [dbo].[Offers].[StartDate],
       [dbo].[Offers].[EndDate],
       [dbo].[Offers].[ApplicablePackages]
FROM [dbo].[Offers]
WHERE [dbo].[Offers].[OfferID] = @OfferID;
[PR_Offers_SelectByPK] 2

CREATE PROCEDURE [dbo].[PR_Offers_Insert]
@OfferName NVARCHAR(255),
@Description NVARCHAR(255),
@DiscountPercentage DECIMAL(5,2),
@EndDate DATETIME,
@ApplicablePackages NVARCHAR(255)
AS
INSERT INTO [dbo].[Offers]
([OfferName], [Description], [DiscountPercentage], [StartDate], [EndDate], [ApplicablePackages])
VALUES
(@OfferName, @Description, @DiscountPercentage,GETDATE(), @EndDate, @ApplicablePackages);
[PR_Offers_Insert] 'Promotion','Summer package','10.11','2025-06-30 00:00:00.000','Standard'
[PR_Offers_SelectAll]
CREATE PROCEDURE [dbo].[PR_Offers_UpdateByPK]
@OfferID INT,
@OfferName NVARCHAR(255),
@Description NVARCHAR(MAX),
@DiscountPercentage DECIMAL(5,2),
@EndDate DATETIME,
@ApplicablePackages NVARCHAR(MAX)
AS
UPDATE [dbo].[Offers]
SET [OfferName] = @OfferName,
    [Description] = @Description,
    [DiscountPercentage] = @DiscountPercentage,
    [StartDate] = GETDATE(),
    [EndDate] = @EndDate,
    [ApplicablePackages] = @ApplicablePackages
WHERE [dbo].[Offers].[OfferID] = @OfferID;
[PR_Offers_UpdateByPK] 11,'Summer Promotion','Summer package','10.11','2025-06-30 00:00:00.000','Standard'

CREATE PROCEDURE [dbo].[PR_Offers_DeleteByPK]
@OfferID INT
AS
DELETE FROM [dbo].[Offers]
WHERE [dbo].[Offers].[OfferID] = @OfferID;

--Dropdown Package
CREATE PROCEDURE [dbo].[PR_PACKAGE_DROPDOWN]
AS
BEGIN
    SELECT
		[dbo].[Packages].[PackageID],
        [dbo].[Packages].[PackageName]
    FROM
        [dbo].[Packages]
END;

create PROCEDURE [dbo].[PR_DESTINATION_DROPDOWN]
AS
BEGIN
    SELECT
		[dbo].[Destinations].[DestinationID],
        [dbo].[Destinations].[DestinationName]
    FROM
        [dbo].[Destinations]
END;

create PROCEDURE [dbo].[PR_Customer_DROPDOWN]
AS
BEGIN
    SELECT
		[dbo].[Customers].[CustomerID],
        [dbo].[Customers].[FullName]
    FROM
        [dbo].[Customers]
END;

create PROCEDURE [dbo].[PR_Booking_DROPDOWN]
AS
BEGIN
    SELECT
		[dbo].[Bookings].[BookingID],
        [dbo].[Bookings].[BookingDate]
    FROM
        [dbo].[Bookings]
END;
ALTER TABLE Customers 
ADD Password NVARCHAR(255);

select * from customers
create PROCEDURE PR_Customer_Register 
    @FullName NVARCHAR(100),
    @Email NVARCHAR(100),
    @Phone NVARCHAR(20),
    @Address NVARCHAR(255),
    @Password NVARCHAR(255)
AS
BEGIN
    INSERT INTO Customers (FullName, Email, Phone, Address, Password, RegistrationDate)
    VALUES (@FullName, @Email, @Phone, @Address, @Password, GETDATE());
END

SELECT * FROM Customers WHERE Email = 'a@gmail.com';

--ALTER PROCEDURE PR_Customer_Login
--    @Email NVARCHAR(100),
--    @Password NVARCHAR(255)
--AS
--BEGIN
--    SELECT Email, Password
--    FROM Customers
--    WHERE Email = @Email AND Password = @Password;
--END
EXEC PR_Customer_Login @Email = 'a@gmail.com', @Password = 'abcd';
EXEC PR_Customer_Login 'a@gmail.com', 'abcd'
select * from customers

alter PROCEDURE PR_Customer_Login 
    @Email NVARCHAR(50),
    @Password NVARCHAR(20)
AS
BEGIN
    SELECT 
	 [dbo].[Customers].[CustomerID]
      ,[dbo].[Customers].[FullName]
      ,[dbo].[Customers].[Email]
      ,[dbo].[Customers].[Phone]
      ,[dbo].[Customers].[Address]
      ,[dbo].[Customers].[RegistrationDate]
	  ,[dbo].[Customers].[Password] 
	  FROM Customers 
    WHERE Email = @Email AND Password = @Password;
END


select * from customers
alter PROCEDURE PR_Customer_Insert 
    @FullName NVARCHAR(255),
    @Email NVARCHAR(255),
    @Phone NVARCHAR(20),
    @Address NVARCHAR(255),
    @Password NVARCHAR(255)
AS
INSERT INTO Customers (FullName, Email, Phone, Address, Password, RegistrationDate)
VALUES (@FullName, @Email, @Phone, @Address, @Password, GETDATE());

alter PROCEDURE PR_Customer_UpdateByPK 
    @CustomerID INT,
    @FullName NVARCHAR(255),
    @Email NVARCHAR(255),
    @Phone NVARCHAR(20),
    @Address NVARCHAR(255),
    @Password NVARCHAR(255) -- Added password update
AS
UPDATE Customers
SET FullName = @FullName,
    Email = @Email,
    Phone = @Phone,
    Address = @Address,
    Password = @Password -- Now updates password
WHERE CustomerID = @CustomerID;

EXEC PR_Customer_UpdateByPK @CustomerID='4',@FullName = 'Emily Davis ', @Email = 'emily@example.com', 
                          @Phone = '1234567890', @Address = '101 Maple St, City, Country ', 
                          @Password = 'emily'
select * from feedback

pr_Itinerary_selectall
delete customers where customerid=12

EXEC PR_Customer_Register @FullName = 'Test ', @Email = 't@example.com', 
                          @Phone = '1234567890', @Address = 'Test ', 
                          @Password = 'test'


alter PROCEDURE PR_Customer_Register 
    @FullName NVARCHAR(100),
    @Email NVARCHAR(100),
    @Phone NVARCHAR(20),
    @Address NVARCHAR(255),
    @Password NVARCHAR(255)
AS
BEGIN
    BEGIN TRANSACTION;
    
    INSERT INTO Customers (FullName, Email, Phone, Address, Password, RegistrationDate)
    VALUES (@FullName, @Email, @Phone, @Address, @Password, GETDATE());

    COMMIT TRANSACTION;
END
