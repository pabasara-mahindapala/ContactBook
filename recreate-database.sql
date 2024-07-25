-- Command Database

CREATE TABLE Users
(
    Id NVARCHAR(255) PRIMARY KEY,
    FirstName NVARCHAR(255) NOT NULL,
    LastName NVARCHAR(255) NOT NULL
);

CREATE TABLE Contacts
(
    Id NVARCHAR(255) PRIMARY KEY,
    Type NVARCHAR(255) NOT NULL,
    Detail NVARCHAR(255) NOT NULL,
    UserId NVARCHAR(255) NOT NULL,
    FOREIGN KEY(UserId) REFERENCES Users(Id)
);

CREATE TABLE Addresses
(
    Id NVARCHAR(255) PRIMARY KEY,
    City NVARCHAR(255) NOT NULL,
    State NVARCHAR(255) NOT NULL,
    Postcode NVARCHAR(255) NOT NULL,
    UserId NVARCHAR(255) NOT NULL,
    FOREIGN KEY(UserId) REFERENCES Users(Id)
);

-- Query Database

CREATE TABLE UserAddresses
(
    UserId NVARCHAR(255) NOT NULL,
    AddressByStateId NVARCHAR(255) NOT NULL,
    FOREIGN KEY(AddressByStateId) REFERENCES AddressByState(Id),
    PRIMARY KEY(UserId, AddressByStateId)
);

CREATE TABLE AddressByState
(
    Id NVARCHAR(255) PRIMARY KEY,
    State NVARCHAR(255) NOT NULL,
    AddressDetailsId NVARCHAR(255) NOT NULL,
    FOREIGN KEY(AddressDetailsId) REFERENCES AddressDetails(Id)
);

CREATE TABLE AddressDetails
(
    Id NVARCHAR(255) PRIMARY KEY,
    City NVARCHAR(255) NOT NULL,
    Postcode NVARCHAR(255) NOT NULL
);

CREATE TABLE UserContacts
(
    UserId NVARCHAR(255) NOT NULL,
    ContactByTypeId NVARCHAR(255) NOT NULL,
    FOREIGN KEY(ContactByTypeId) REFERENCES ContactByType(Id),
    PRIMARY KEY(UserId, ContactByTypeId)
);

CREATE TABLE ContactByType
(
    Id NVARCHAR(255) PRIMARY KEY,
    Type NVARCHAR(255) NOT NULL,
    ContactDetailsId NVARCHAR(255) NOT NULL,
    FOREIGN KEY(ContactDetailsId) REFERENCES ContactDetails(Id)
);

CREATE TABLE ContactDetails
(
    Id NVARCHAR(255) PRIMARY KEY,
    Detail NVARCHAR(255) NOT NULL
);