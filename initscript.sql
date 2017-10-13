CREATE DATABASE IF NOT EXISTS OnlineStore;

USE OnlineStore;
CREATE TABLE IF NOT EXISTS Customers (
    CustomerId int NOT NULL AUTO_INCREMENT,
    FirstName VARCHAR(255) NOT NULL,
    LastName VARCHAR(255) NOT NULL,    
    EmailAddress VARCHAR(255) NOT NULL,
    NotifyMe TINYINT(1) DEFAULT 0,
    PRIMARY KEY (CustomerId)
);
