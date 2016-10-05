CREATE TABLE [dbo].[Goods]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(50) NULL, 
    [Price] DECIMAL NULL, 
    [CategoryId] INT NULL, 
    [ProducerId] INT NULL, 
    CONSTRAINT [FK_Goods_Categories] FOREIGN KEY ([Id]) REFERENCES [Categories]([Id]), 
    CONSTRAINT [FK_Goods_Producers] FOREIGN KEY ([Id]) REFERENCES [Producers]([Id])
)
