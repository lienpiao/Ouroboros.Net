
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/09/2018 13:56:31
-- Generated from EDMX file: D:\Users\Lilb\Documents\GitHub\Ouroboros.Net\src\Ouroboros.Model\DataModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [OuroborosDb];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'SysUserSet'
CREATE TABLE [dbo].[SysUserSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserName] nvarchar(32)  NOT NULL,
    [ShowName] nvarchar(32)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [IsDeleted] bit  NOT NULL
);
GO

-- Creating table 'SysUserAction'
CREATE TABLE [dbo].[SysUserAction] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] int  NOT NULL,
    [ActionId] int  NOT NULL,
    [HasPermission] bit  NOT NULL,
    [IsDeleted] bit  NOT NULL
);
GO

-- Creating table 'SysAction'
CREATE TABLE [dbo].[SysAction] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ActionName] nvarchar(32)  NOT NULL,
    [ActionCode] nvarchar(32)  NULL,
    [ActionType] int  NOT NULL,
    [ParentId] int  NOT NULL,
    [Url] nvarchar(max)  NOT NULL,
    [Icon] nvarchar(32)  NOT NULL,
    [Sort] int  NOT NULL,
    [Area] nvarchar(32)  NULL,
    [Controller] nvarchar(32)  NOT NULL,
    [Action] nvarchar(32)  NOT NULL,
    [HttpMethd] int  NOT NULL,
    [IsDeleted] bit  NOT NULL
);
GO

-- Creating table 'SysRole'
CREATE TABLE [dbo].[SysRole] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [RoleName] nvarchar(32)  NOT NULL,
    [IsActlve] bit  NOT NULL,
    [IsDeleted] bit  NOT NULL
);
GO

-- Creating table 'SysUserSysRole'
CREATE TABLE [dbo].[SysUserSysRole] (
    [SysUser_Id] int  NOT NULL,
    [SysRole_Id] int  NOT NULL
);
GO

-- Creating table 'SysRoleSysAction'
CREATE TABLE [dbo].[SysRoleSysAction] (
    [SysRole_Id] int  NOT NULL,
    [SysAction_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'SysUserSet'
ALTER TABLE [dbo].[SysUserSet]
ADD CONSTRAINT [PK_SysUserSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SysUserAction'
ALTER TABLE [dbo].[SysUserAction]
ADD CONSTRAINT [PK_SysUserAction]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SysAction'
ALTER TABLE [dbo].[SysAction]
ADD CONSTRAINT [PK_SysAction]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SysRole'
ALTER TABLE [dbo].[SysRole]
ADD CONSTRAINT [PK_SysRole]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [SysUser_Id], [SysRole_Id] in table 'SysUserSysRole'
ALTER TABLE [dbo].[SysUserSysRole]
ADD CONSTRAINT [PK_SysUserSysRole]
    PRIMARY KEY CLUSTERED ([SysUser_Id], [SysRole_Id] ASC);
GO

-- Creating primary key on [SysRole_Id], [SysAction_Id] in table 'SysRoleSysAction'
ALTER TABLE [dbo].[SysRoleSysAction]
ADD CONSTRAINT [PK_SysRoleSysAction]
    PRIMARY KEY CLUSTERED ([SysRole_Id], [SysAction_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UserId] in table 'SysUserAction'
ALTER TABLE [dbo].[SysUserAction]
ADD CONSTRAINT [FK_SysUserSysUserAction]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[SysUserSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SysUserSysUserAction'
CREATE INDEX [IX_FK_SysUserSysUserAction]
ON [dbo].[SysUserAction]
    ([UserId]);
GO

-- Creating foreign key on [ActionId] in table 'SysUserAction'
ALTER TABLE [dbo].[SysUserAction]
ADD CONSTRAINT [FK_SysActionSysUserAction]
    FOREIGN KEY ([ActionId])
    REFERENCES [dbo].[SysAction]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SysActionSysUserAction'
CREATE INDEX [IX_FK_SysActionSysUserAction]
ON [dbo].[SysUserAction]
    ([ActionId]);
GO

-- Creating foreign key on [SysUser_Id] in table 'SysUserSysRole'
ALTER TABLE [dbo].[SysUserSysRole]
ADD CONSTRAINT [FK_SysUserSysRole_SysUser]
    FOREIGN KEY ([SysUser_Id])
    REFERENCES [dbo].[SysUserSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [SysRole_Id] in table 'SysUserSysRole'
ALTER TABLE [dbo].[SysUserSysRole]
ADD CONSTRAINT [FK_SysUserSysRole_SysRole]
    FOREIGN KEY ([SysRole_Id])
    REFERENCES [dbo].[SysRole]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SysUserSysRole_SysRole'
CREATE INDEX [IX_FK_SysUserSysRole_SysRole]
ON [dbo].[SysUserSysRole]
    ([SysRole_Id]);
GO

-- Creating foreign key on [SysRole_Id] in table 'SysRoleSysAction'
ALTER TABLE [dbo].[SysRoleSysAction]
ADD CONSTRAINT [FK_SysRoleSysAction_SysRole]
    FOREIGN KEY ([SysRole_Id])
    REFERENCES [dbo].[SysRole]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [SysAction_Id] in table 'SysRoleSysAction'
ALTER TABLE [dbo].[SysRoleSysAction]
ADD CONSTRAINT [FK_SysRoleSysAction_SysAction]
    FOREIGN KEY ([SysAction_Id])
    REFERENCES [dbo].[SysAction]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SysRoleSysAction_SysAction'
CREATE INDEX [IX_FK_SysRoleSysAction_SysAction]
ON [dbo].[SysRoleSysAction]
    ([SysAction_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------