CREATE TABLE Customers (
    CustomerID INT IDENTITY(1,1) PRIMARY KEY,
    FullName VARCHAR(255) NOT NULL,
    Email VARCHAR(255) NOT NULL UNIQUE,
    Phone VARCHAR(20),
    Address NVARCHAR(255),
    RegistrationDate DATETIME DEFAULT GETDATE()
);

-- Insert sample data
INSERT INTO Customers (FullName, Email, Phone, Address) VALUES
('John Doe', 'john@example.com', '1234567890', '123 Main St, City, Country'),
('Jane Smith', 'jane@example.com', '9876543210', '456 Oak St, City, Country'),
('Michael Johnson', 'michael@example.com', '2345678901', '789 Pine St, City, Country'),
('Emily Davis', 'emily@example.com', '3456789012', '101 Maple St, City, Country'),
('Chris Lee', 'chris@example.com', '4567890123', '202 Birch St, City, Country'),
('Sarah White', 'sarah@example.com', '5678901234', '303 Cedar St, City, Country'),
('David Brown', 'david@example.com', '6789012345', '404 Elm St, City, Country'),
('Laura Wilson', 'laura@example.com', '7890123456', '505 Pine St, City, Country'),
('Daniel Harris', 'daniel@example.com', '8901234567', '606 Oak St, City, Country'),
('Olivia Clark', 'olivia@example.com', '9012345678', '707 Maple St, City, Country');

SELECT * FROM Customers;


CREATE TABLE Destinations (
    DestinationID INT IDENTITY(1,1) PRIMARY KEY,
    DestinationName VARCHAR(255) NOT NULL,
    Country VARCHAR(100) NOT NULL,
    State NVARCHAR(100),
    Description NVARCHAR(255),
    BestTimeToVisit VARCHAR(100)
);

-- Insert sample data
INSERT INTO Destinations (DestinationName, Country, State, Description, BestTimeToVisit) VALUES
('Maldives', 'Maldives', 'Malé', 'Tropical paradise with beautiful beaches', 'November to April'),
('Swiss Alps', 'Switzerland', 'Valais', 'Majestic snow-capped mountains', 'December to February'),
('New York City', 'USA', 'New York', 'Vibrant city life and cultural landmarks', 'Spring and Fall'),
('Sahara Desert', 'Egypt', 'Sahara', 'Endless dunes and thrilling desert adventures', 'October to March'),
('Amazon Rainforest', 'Brazil', 'Amazonas', 'Wildlife and lush forests', 'November to May'),
('Rome', 'Italy', 'Lazio', 'Ancient ruins and historical landmarks', 'April to June'),
('Santorini', 'Greece', 'Cyclades', 'Iconic white buildings with blue rooftops', 'May to October'),
('Grand Canyon', 'USA', 'Arizona', 'Spectacular natural beauty', 'March to May'),
('Machu Picchu', 'Peru', 'Cusco', 'Ancient Inca city in the mountains', 'May to September'),
('Sydney', 'Australia', 'New South Wales', 'Famous landmarks like the Opera House', 'November to February');

SELECT * FROM Destinations;


CREATE TABLE Packages (
    PackageID INT IDENTITY(1,1) PRIMARY KEY,
    PackageName VARCHAR(255) NOT NULL,
    Description NVARCHAR(255),
    DestinationID INT NOT NULL,
    Price DECIMAL(10, 2) NOT NULL,
    Duration INT NOT NULL,
    Status VARCHAR(20) NOT NULL,
    FOREIGN KEY (DestinationID) REFERENCES Destinations(DestinationID)
);

-- Insert sample data
INSERT INTO Packages (PackageName, Description, DestinationID, Price, Duration, Status) VALUES
('Beach Paradise', 'Relax at the beautiful beaches', 1, 499.99, 5, 'Available'),
('Mountain Adventure', 'Explore the best mountain ranges', 2, 799.99, 7, 'Available'),
('City Explorer', 'Discover the vibrant city life', 3, 299.99, 4, 'Unavailable'),
('Desert Safari', 'Experience the vast deserts', 4, 599.99, 6, 'Available'),
('Jungle Safari', 'Discover wildlife in the jungle', 5, 699.99, 8, 'Available'),
('Cultural Heritage', 'Visit historic monuments and museums', 6, 450.00, 5, 'Unavailable'),
('Island Getaway', 'Escape to a serene island paradise', 1, 899.99, 7, 'Available'),
('Historical Tour', 'Tour ancient civilizations and ruins', 7, 550.00, 6, 'Available'),
('Adventure Expedition', 'Hike through uncharted territories', 8, 999.99, 10, 'Available'),
('Luxury Cruise', 'Enjoy the luxurious cruise experience', 9, 1500.00, 12, 'Available');

SELECT * FROM Packages;


CREATE TABLE Bookings (
    BookingID INT IDENTITY(1,1) PRIMARY KEY,
    CustomerID INT NOT NULL,
    PackageID INT NOT NULL,
    BookingDate DATETIME  DEFAULT GETDATE(),
    TravelDate DATETIME,
    NumberOfPeople INT NOT NULL,
    TotalAmount DECIMAL(10, 2) NOT NULL,
    Status NVARCHAR(50) NOT NULL,
    FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID),
    FOREIGN KEY (PackageID) REFERENCES Packages(PackageID)
);

-- Insert sample data
INSERT INTO Bookings (CustomerID, PackageID, TravelDate, NumberOfPeople, TotalAmount, Status) VALUES
(1, 1, '2025-01-15', 2, 999.98, 'Confirmed'),
(2, 2, '2025-02-10', 3, 2399.97, 'Pending'),
(3, 3, '2025-03-05', 1, 299.99, 'Cancelled'),
(4, 4, '2025-04-20', 4, 2399.96, 'Confirmed'),
(5, 5, '2025-05-18', 2, 1399.98, 'Pending'),
(6, 6, '2025-06-10', 5, 2250.00, 'Confirmed'),
(7, 7, '2025-07-15', 3, 2699.97, 'Confirmed'),
(8, 8, '2025-08-25', 1, 999.99, 'Pending'),
(9, 9, '2025-09-10', 6, 8999.94, 'Confirmed'),
(10, 10, '2025-10-05', 2, 2999.98, 'Confirmed');

SELECT * FROM Bookings;


CREATE TABLE Itinerary (
    ItineraryID INT IDENTITY(1,1) PRIMARY KEY,
    PackageID INT NOT NULL,
    DayNumber INT NOT NULL,
    Activity NVARCHAR(255) NOT NULL,
    Location VARCHAR(255),
    Time  TIME DEFAULT GETDATE(),
    FOREIGN KEY (PackageID) REFERENCES Packages(PackageID)
);

-- Insert sample data
INSERT INTO Itinerary (PackageID, DayNumber, Activity, Location) VALUES
(1, 1, 'Beach relaxation', 'Sunset Beach'),
(1, 2, 'Snorkeling', 'Coral Reef'),
(2, 1, 'Mountain hike', 'Rocky Mountains'),
(2, 2, 'Cave exploration', 'Limestone Cave'),
(3, 1, 'City tour', 'Downtown'),
(3, 2, 'Museum visit', 'City Museum'),
(4, 1, 'Desert safari', 'Sahara Desert'),
(4, 2, 'Camel ride', 'Sahara Desert'),
(5, 1, 'Jungle walk', 'Amazon Jungle'),
(5, 2, 'Wildlife watching', 'Jungle Park');

SELECT * FROM Itinerary;


CREATE TABLE Payment (
    PaymentID INT IDENTITY(1,1) PRIMARY KEY,
    BookingID INT NOT NULL,
    PaymentDate DATETIME NOT NULL DEFAULT GETDATE(),
    PaymentMode VARCHAR(50) NOT NULL,
    AmountPaid DECIMAL(10, 2) NOT NULL,
    PaymentStatus VARCHAR(20) NOT NULL,
    FOREIGN KEY (BookingID) REFERENCES Bookings(BookingID)
);

-- Insert sample data
INSERT INTO Payment (BookingID, PaymentMode, AmountPaid, PaymentStatus) VALUES
(1, 'Credit Card', 999.98, 'Success'),
(2, 'Debit Card', 2399.97, 'Pending'),
(3, 'UPI', 299.99, 'Failed'),
(4, 'Credit Card', 2399.96, 'Success'),
(5, 'Debit Card', 1399.98, 'Pending'),
(6, 'UPI', 2250.00, 'Success'),
(7, 'Credit Card', 2699.97, 'Success'),
(8, 'Debit Card', 999.99, 'Pending'),
(9, 'UPI', 8999.94, 'Success'),
(10, 'Credit Card', 2999.98, 'Success');

SELECT * FROM Payment;

CREATE TABLE Transportation (
    TransportID INT IDENTITY(1,1) PRIMARY KEY,
    BookingID INT NOT NULL,
    TransportMode NVARCHAR(50) NOT NULL,
    TransportDetails NVARCHAR(255),
    DepartureTime DATETIME NOT NULL,
    ArrivalTime DATETIME NOT NULL,
    Cost DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (BookingID) REFERENCES Bookings(BookingID)
);

--ALTER TABLE Transportation
ALTER TABLE Transportation
ALTER COLUMN DepartureTime DATETIME NOT NULL;

-- Insert sample data
INSERT INTO Transportation (BookingID, TransportMode, TransportDetails, DepartureTime, ArrivalTime, Cost) VALUES
(1, 'Flight', 'Airline XYZ, Flight 123', '2025-01-15 06:00', '2025-01-15 08:00', 500.00),
(2, 'Bus', 'Bus 45', '2025-02-10 07:00', '2025-02-10 15:00', 200.00),
(3, 'Train', 'Train A1', '2025-03-05 10:00', '2025-03-05 16:00', 150.00),
(4, 'Flight', 'Airline ABC, Flight 456', '2025-04-20 09:00', '2025-04-20 11:00', 800.00),
(5, 'Bus', 'Bus 78', '2025-05-18 06:00', '2025-05-18 18:00', 250.00),
(6, 'Train', 'Train B2', '2025-06-10 08:00', '2025-06-10 17:00', 300.00),
(7, 'Flight', 'Airline DEF, Flight 789', '2025-07-15 05:00', '2025-07-15 07:00', 600.00),
(8, 'Bus', 'Bus 99', '2025-08-25 10:00', '2025-08-25 16:00', 180.00),
(9, 'Train', 'Train C3', '2025-09-10 09:00', '2025-09-10 17:00', 350.00),
(10, 'Flight', 'Airline XYZ, Flight 101', '2025-10-05 11:00', '2025-10-05 13:00', 400.00);

SELECT * FROM Transportation;

CREATE TABLE Feedback (
    FeedbackID INT IDENTITY(1,1) PRIMARY KEY,
    CustomerID INT NOT NULL,
    PackageID INT NOT NULL,
    Rating INT CHECK(Rating BETWEEN 1 AND 5),
    Comments NVARCHAR(255),
    FeedbackDate DATETIME NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID),
    FOREIGN KEY (PackageID) REFERENCES Packages(PackageID)
);

-- Insert sample data
INSERT INTO Feedback (CustomerID, PackageID, Rating, Comments) VALUES
(1, 1, 5, 'Excellent experience, would highly recommend!'),
(2, 2, 4, 'Great trip, but the mountain trek was difficult.'),
(3, 3, 3, 'City tour was okay, not much to see.'),
(4, 4, 5, 'Amazing desert safari, had a blast!'),
(5, 5, 4, 'Jungle safari was fun, saw lots of animals.'),
(6, 6, 2, 'Historical tour was too short, not enough time for exploration.'),
(7, 7, 5, 'Beautiful islands, the best vacation!'),
(8, 8, 4, 'Wonderful cultural experience, but a bit too hot.'),
(9, 9, 5, 'The cruise was luxurious, an unforgettable experience!'),
(10, 10, 4, 'Great adventure, but the expedition was challenging.');

SELECT * FROM Feedback;


CREATE TABLE Employees (
    EmployeeID INT IDENTITY(1,1) PRIMARY KEY,
    FullName VARCHAR(255) NOT NULL,
    Position VARCHAR(100),
    ContactNumber NVARCHAR(20),
    Email VARCHAR(255) UNIQUE,
    Salary DECIMAL(10, 2) NOT NULL
);

-- Insert sample data
INSERT INTO Employees (FullName, Position, ContactNumber, Email, Salary) VALUES
('Alice Brown', 'Tour Guide', '1234567890', 'alice@touragency.com', 3500.00),
('Bob White', 'Manager', '2345678901', 'bob@touragency.com', 5000.00),
('Charlie Green', 'Driver', '3456789012', 'charlie@touragency.com', 2500.00),
('David Blue', 'Tour Guide', '4567890123', 'david@touragency.com', 3300.00),
('Eve Black', 'Manager', '5678901234', 'eve@touragency.com', 4500.00),
('Frank Yellow', 'Driver', '6789012345', 'frank@touragency.com', 2300.00),
('Grace Red', 'Tour Guide', '7890123456', 'grace@touragency.com', 3700.00),
('Hank Purple', 'Manager', '8901234567', 'hank@touragency.com', 5200.00),
('Ivy White', 'Driver', '9012345678', 'ivy@touragency.com', 2400.00),
('Jack Gray', 'Tour Guide', '0123456789', 'jack@touragency.com', 3400.00);

SELECT * FROM Employees;


CREATE TABLE Offers (
    OfferID INT IDENTITY(1,1) PRIMARY KEY,
    OfferName VARCHAR(255) NOT NULL,
    Description NVARCHAR(255),
    DiscountPercentage DECIMAL(5, 2),
    StartDate DATETIME NOT NULL DEFAULT GETDATE(),
    EndDate DATETIME NOT NULL,
    ApplicablePackages NVARCHAR(255)
);

-- Insert sample data
INSERT INTO Offers (OfferName, Description, DiscountPercentage, EndDate, ApplicablePackages) VALUES
('Winter Sale', 'Discount for winter trips', 10.00,  '2025-01-31', 'Premium, Standard'),
('Summer Promotion', 'Summer package discount', 15.00, '2025-06-30', 'Standard'),
('Holiday Special', 'Special offers for holidays', 20.00, '2025-12-31', 'Basic'),
('Early Bird Offer', 'Early booking discount', 5.00,'2025-02-28', 'Premium'),
('Group Discount', 'Discount for group bookings', 10.00,'2025-03-31', 'Premium, Standard'),
('Exclusive Offer', 'Exclusive offers for members', 25.00,'2025-05-31', 'Basic, Standard'),
('Family Pack', 'Family package offers', 15.00,'2025-07-31', 'Premium, Standard'),
('Autumn Sale', 'Autumn discounts', 20.00,'2025-09-30', 'Premium, Standard'),
('New Year Offer', 'Special New Year package', 30.00, '2025-01-15', 'All'),
('Flash Deal', 'Limited-time offer for early bookings', 10.00, '2025-11-10', 'Premium');

SELECT * FROM Offers;


select * FROM [dbo].[Transportation];
DBCC CHECKIDENT ('[dbo].[Transportation]', RESEED, 0);
INSERT INTO [dbo].[Transportation] (BookingID, TransportMode, TransportDetails, DepartureTime, ArrivalTime, Cost)
VALUES (2, 'Train', 'AC Sleeper', '2025-01-20 08:00:00', '2025-01-20 14:00:00', 500.00);
