/*Le nom de la base données est <<Arts>>
 Le script qui suit represente tout les requetes de creation des tables de la BD */


CREATE TABLE [dbo].[Artists] (
    [Id]              INT            IDENTITY (1, 1) NOT NULL,
    [ArtistName]      NVARCHAR (50)  NULL,
    [ArtistEmail]     NVARCHAR (50)  NOT NULL,
    [ArtistUrl]       NVARCHAR (50)  NOT NULL,
    [ArtistThumbnail] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Artists] PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Bills] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [PaymentMode]      NVARCHAR (60)  NOT NULL,
    [TotalPrice]       FLOAT (53)     NULL,
    [CreateAt]         DATETIME       NOT NULL DEFAULT getdate(),
    [Country]          NVARCHAR (60)  NOT NULL,
    [City]             NVARCHAR (60)  NOT NULL,
    [CardName]         NVARCHAR (100) NULL,
    [CardCreditNumber] INT            NULL,
    [id_user]          INT            NOT NULL,
    [Adress]           NVARCHAR (50)  NOT NULL,
    [DetailAdress]     NVARCHAR (50)  NULL,
    CONSTRAINT [PK_Bills] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Bills_Users] FOREIGN KEY ([id_user]) REFERENCES [dbo].[Users] ([Id])
);

CREATE TABLE [dbo].[Carts] (
    [Id]       INT IDENTITY (1, 1) NOT NULL,
    [id_user]  INT NOT NULL,
    [id_items] INT NOT NULL,
    CONSTRAINT [PK_Carts] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Carts_Items] FOREIGN KEY ([id_items]) REFERENCES [dbo].[Items] ([Id]),
    CONSTRAINT [FK_Carts_Users] FOREIGN KEY ([id_user]) REFERENCES [dbo].[Users] ([Id])
);

CREATE TABLE [dbo].[Forums] (
    [Id]           INT             IDENTITY (1, 1) NOT NULL,
    [ForumComment] NVARCHAR (4000) NULL,
    [CreateAt]     DATETIME        DEFAULT (getdate()) NOT NULL,
    [id_user]      INT             NOT NULL,
    [id_item]      INT             NOT NULL,
    CONSTRAINT [PK_Forums] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Forums_Items] FOREIGN KEY ([id_item]) REFERENCES [dbo].[Items] ([Id]),
    CONSTRAINT [FK_Forums_Users] FOREIGN KEY ([id_user]) REFERENCES [dbo].[Users] ([Id])
);

CREATE TABLE [dbo].[Gallery] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [GalleryThumbnail] NVARCHAR (MAX) NOT NULL,
    [CreateAt]         DATETIME       CONSTRAINT [DF__Gallery__CreateA__18EBB532] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK__Gallery__3214EC076C38F44E] PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Items] (
    [Id]              INT             IDENTITY (1, 1) NOT NULL,
    [ItemCategory]    NVARCHAR (50)   NOT NULL,
    [ItemName]        NVARCHAR (70)   NOT NULL,
    [ItemDescription] NVARCHAR (1000) NULL,
    [ItemThumbnail]   NVARCHAR (MAX)  NULL,
    [ItemPrice]       FLOAT (53)      DEFAULT ((0)) NULL,
    [CreateAt]        DATETIME        DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Items] PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Users] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [Username]     NVARCHAR (70) NOT NULL,
    [Useremail]    NVARCHAR (70) NOT NULL,
    [Userpassword] NVARCHAR (20) NOT NULL,
    [IsAdmin]      INT           CONSTRAINT [DF_Users_IsAdmin] DEFAULT ((1)) NOT NULL,
    [CreateAt]     DATETIME      CONSTRAINT [DF_Users_CreateAt] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Id] ASC)
);

GO
CREATE NONCLUSTERED INDEX [IX_Users]
    ON [dbo].[Users]([Useremail] ASC);

