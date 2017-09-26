CREATE TABLE [dbo].[TestObject] (
    [To_Id]            NVARCHAR (50)   NOT NULL,
    [To_StringValue]   NVARCHAR (MAX)  NOT NULL,
    [To_IntValue]      INT             NOT NULL,
    [To_LongValue]     BIGINT          NOT NULL,
    [To_DecimalValue]  DECIMAL (18, 4) NOT NULL,
    [To_BoolValue]     BIT             NOT NULL,
    [To_DateTimeValue] DATETIME        NOT NULL,
    [To_EnumValue]     SMALLINT        NOT NULL,
    PRIMARY KEY CLUSTERED ([To_Id] ASC)
);

CREATE TABLE [dbo].[TestNullableObject] (
    [To_Id]            NVARCHAR (50)   NOT NULL,
    [To_StringValue]   NVARCHAR (MAX)  NULL,
    [To_IntValue]      INT             NULL,
    [To_LongValue]     BIGINT          NULL,
    [To_DecimalValue]  DECIMAL (18, 4) NULL,
    [To_BoolValue]     BIT             NULL,
    [To_DateTimeValue] DATETIME        NULL,
    [To_EnumValue]     SMALLINT        NULL,
    PRIMARY KEY CLUSTERED ([To_Id] ASC)
);

CREATE TABLE [dbo].[Log] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [Date]      DATETIME      NOT NULL,
    [Thread]    VARCHAR (255) NOT NULL,
    [Level]     VARCHAR (50)  NOT NULL,
    [Logger]    VARCHAR (255) NOT NULL,
    [Message]   TEXT          NOT NULL,
    [Exception] TEXT          NULL
);
