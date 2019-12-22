CREATE SCHEMA [Users];

GO

CREATE TABLE [Users].[UserStates]
(
    [UserStateId] TINYINT      NOT NULL,
    [UserState]   VARCHAR(255) NOT NULL,

    CONSTRAINT [PK_Users_UserStates_UserStateId] PRIMARY KEY CLUSTERED ([UserStateId] ASC)
)

GO

CREATE TABLE [Users].[UserTypes]
(
    [UserTypeId] TINYINT      NOT NULL,
    [UserType]   VARCHAR(255) NOT NULL,

    CONSTRAINT [PK_Users_UserTypes_UserTypeId] PRIMARY KEY CLUSTERED ([UserTypeId] ASC)
)

GO

CREATE TABLE [Users].[Users]
(
    [UserId]            INT IDENTITY(1, 1)  NOT NULL,
    [UserTypeId]        TINYINT             NOT NULL,
    [UserStateId]       TINYINT             NOT NULL,
    [Username]          VARCHAR(64)         NOT NULL,
    [PasswordHash]      BINARY(32)          NOT NULL,
    [PasswordSalt]      BINARY(16)          NOT NULL,
    [FirstName]         VARCHAR(64)         NOT NULL,
    [LastName]          VARCHAR(64)         NOT NULL,
    [RegistrationDate]  DATETIMEOFFSET(0)   NOT NULL

    CONSTRAINT [PK_Users_Users_UserId]           PRIMARY KEY CLUSTERED (UserId ASC),
    CONSTRAINT [UK_Users_Users_Username]         UNIQUE (Username),
    CONSTRAINT [UK_Users_Users_FirstLastName]    UNIQUE (FirstName, LastName),
    CONSTRAINT [FK_Users_UserTypes_UserTypeId]   FOREIGN KEY (UserTypeId) REFERENCES Users.UserTypes(UserTypeId),
    CONSTRAINT [FK_Users_UserStates_UserStateId] FOREIGN KEY (UserStateId) REFERENCES Users.UserStates(UserStateId)
);

GO

INSERT INTO [Users].[UserStates] 
    (UserStateId, UserState)
VALUES 
    (0, 'Active'),
    (1, 'Disabled');
    
GO

INSERT INTO [Users].[UserTypes]
    (UserTypeId, UserType)
VALUES
    (0, 'SimpleUser'),
    (1, 'Administrator')

GO