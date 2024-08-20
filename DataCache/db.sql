DROP TABLE IF EXISTS User;
DROP TABLE IF EXISTS UserAddress;
DROP TABLE IF EXISTS UserLogin;
DROP TABLE IF EXISTS UserSession;

CREATE TABLE UserLogin (
    UserId INTEGER PRIMARY KEY,
    Username TEXT NOT NULL,
	UserToken TEXT,
	Active INT
);

CREATE TABLE User (
    UserId INTEGER PRIMARY KEY,
    UserFirstName TEXT NOT NULL,
    UserMiddleName TEXT,
    UserLastName TEXT NOT NULL,
	UserEmail TEXT,
    Cpf TEXT,
    Rg TEXT,
    DateOfBirth TEXT,
    FOREIGN KEY (UserId) REFERENCES UserLogin(UserId) ON DELETE CASCADE
);

CREATE TABLE UserAddress (
    AddressId INTEGER PRIMARY KEY,
    UserId INTEGER NOT NULL,
    StreetId INTEGER NOT NULL,
    AddressTypeId INTEGER NOT NULL,
    Complement TEXT NOT NULL,
    MainAddress BOOLEAN NOT NULL DEFAULT 0,
    CreatedBy INTEGER NOT NULL,
    CreatedOn TEXT NOT NULL,
    ModifiedBy INTEGER NOT NULL,
    ModifiedOn TEXT NOT NULL,
    FOREIGN KEY (UserId) REFERENCES User(UserId) ON DELETE CASCADE
);
