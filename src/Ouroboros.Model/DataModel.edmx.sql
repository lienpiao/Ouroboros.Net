
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/17/2018 11:51:47
-- Generated from EDMX file: D:\Users\Documents\GitHub\Ouroboros.Net\src\Ouroboros.Model\DataModel.edmx
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

IF OBJECT_ID(N'[dbo].[FK_SysUserSysUserAction]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SysUserAction] DROP CONSTRAINT [FK_SysUserSysUserAction];
GO
IF OBJECT_ID(N'[dbo].[FK_SysActionSysUserAction]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SysUserAction] DROP CONSTRAINT [FK_SysActionSysUserAction];
GO
IF OBJECT_ID(N'[dbo].[FK_SysUserRole_SysUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SysUserRole] DROP CONSTRAINT [FK_SysUserRole_SysUser];
GO
IF OBJECT_ID(N'[dbo].[FK_SysUserRole_SysRole]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SysUserRole] DROP CONSTRAINT [FK_SysUserRole_SysRole];
GO
IF OBJECT_ID(N'[dbo].[FK_SysRoleAction_SysRole]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SysRoleAction] DROP CONSTRAINT [FK_SysRoleAction_SysRole];
GO
IF OBJECT_ID(N'[dbo].[FK_SysRoleAction_SysAction]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SysRoleAction] DROP CONSTRAINT [FK_SysRoleAction_SysAction];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[SysUser]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SysUser];
GO
IF OBJECT_ID(N'[dbo].[SysUserAction]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SysUserAction];
GO
IF OBJECT_ID(N'[dbo].[SysAction]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SysAction];
GO
IF OBJECT_ID(N'[dbo].[SysRole]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SysRole];
GO
IF OBJECT_ID(N'[dbo].[SysUserRole]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SysUserRole];
GO
IF OBJECT_ID(N'[dbo].[SysRoleAction]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SysRoleAction];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'SysUser'
CREATE TABLE [dbo].[SysUser] (
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

-- Creating table 'SysUserRole'
CREATE TABLE [dbo].[SysUserRole] (
    [SysUser_Id] int  NOT NULL,
    [SysRole_Id] int  NOT NULL
);
GO

-- Creating table 'SysRoleAction'
CREATE TABLE [dbo].[SysRoleAction] (
    [SysRole_Id] int  NOT NULL,
    [SysAction_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'SysUser'
ALTER TABLE [dbo].[SysUser]
ADD CONSTRAINT [PK_SysUser]
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

-- Creating primary key on [SysUser_Id], [SysRole_Id] in table 'SysUserRole'
ALTER TABLE [dbo].[SysUserRole]
ADD CONSTRAINT [PK_SysUserRole]
    PRIMARY KEY CLUSTERED ([SysUser_Id], [SysRole_Id] ASC);
GO

-- Creating primary key on [SysRole_Id], [SysAction_Id] in table 'SysRoleAction'
ALTER TABLE [dbo].[SysRoleAction]
ADD CONSTRAINT [PK_SysRoleAction]
    PRIMARY KEY CLUSTERED ([SysRole_Id], [SysAction_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UserId] in table 'SysUserAction'
ALTER TABLE [dbo].[SysUserAction]
ADD CONSTRAINT [FK_SysUserSysUserAction]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[SysUser]
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

-- Creating foreign key on [SysUser_Id] in table 'SysUserRole'
ALTER TABLE [dbo].[SysUserRole]
ADD CONSTRAINT [FK_SysUserRole_SysUser]
    FOREIGN KEY ([SysUser_Id])
    REFERENCES [dbo].[SysUser]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [SysRole_Id] in table 'SysUserRole'
ALTER TABLE [dbo].[SysUserRole]
ADD CONSTRAINT [FK_SysUserRole_SysRole]
    FOREIGN KEY ([SysRole_Id])
    REFERENCES [dbo].[SysRole]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SysUserRole_SysRole'
CREATE INDEX [IX_FK_SysUserRole_SysRole]
ON [dbo].[SysUserRole]
    ([SysRole_Id]);
GO

-- Creating foreign key on [SysRole_Id] in table 'SysRoleAction'
ALTER TABLE [dbo].[SysRoleAction]
ADD CONSTRAINT [FK_SysRoleAction_SysRole]
    FOREIGN KEY ([SysRole_Id])
    REFERENCES [dbo].[SysRole]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [SysAction_Id] in table 'SysRoleAction'
ALTER TABLE [dbo].[SysRoleAction]
ADD CONSTRAINT [FK_SysRoleAction_SysAction]
    FOREIGN KEY ([SysAction_Id])
    REFERENCES [dbo].[SysAction]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SysRoleAction_SysAction'
CREATE INDEX [IX_FK_SysRoleAction_SysAction]
ON [dbo].[SysRoleAction]
    ([SysAction_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------