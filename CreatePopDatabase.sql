USE MASTER

IF EXISTS (SELECT "name"
           FROM Sysdatabases
           WHERE "name" = 'Population')    
    BEGIN
        PRINT '>>> Yes, an Population database already exists';
        PRINT '>>> Rolling back pending Population transactions';
        PRINT '>>> Dropping the existing Population database';   
        
        DROP DATABASE Population;
    END
ELSE
    BEGIN
        PRINT '>>> No, there is no Population database';    
    END
GO

PRINT '>>> Creating a new Population database';

GO

CREATE DATABASE Population;

GO

USE Population;

CREATE TABLE City (
    CityName NVARCHAR(50) PRIMARY KEY,
    Population FLOAT
);

GO
 
INSERT INTO City(cityName, population) VALUES ('Beijing', 12500000);
INSERT INTO City(cityName, population) VALUES ('Buenos Aires', 13170000);
INSERT INTO City(cityName, population) VALUES ('Cairo', 14450000);
INSERT INTO City(cityName, population) VALUES ('Calcutta', 15100000);
INSERT INTO City(cityName, population) VALUES ('Delhi', 18680000);
INSERT INTO City(cityName, population) VALUES ('Jakarta', 18900000);
INSERT INTO City(cityName, population) VALUES ('Karachi', 11800000);
INSERT INTO City(cityName, population) VALUES ('Lagos', 13488000);
INSERT INTO City(cityName, population) VALUES ('London', 12875000);
INSERT INTO City(cityName, population) VALUES ('Los Angeles', 15250000);
INSERT INTO City(cityName, population) VALUES ('Manila', 16300000);
INSERT INTO City(cityName, population) VALUES ('Mexico City', 20450000);
INSERT INTO City(cityName, population) VALUES ('Moscow', 15000000);
INSERT INTO City(cityName, population) VALUES ('Mumbai', 19200000);
INSERT INTO City(cityName, population) VALUES ('New York City', 19750000);
INSERT INTO City(cityName, population) VALUES ('Osaka', 17350000);
INSERT INTO City(cityName, population) VALUES ('Sau Paulo', 18850000);
INSERT INTO City(cityName, population) VALUES ('Seoul', 20550000);
INSERT INTO City(cityName, population) VALUES ('Shanghai', 16650000);
INSERT INTO City(cityName, population) VALUES ('Tokyo', 32450000);

SELECT * FROM dbo.City