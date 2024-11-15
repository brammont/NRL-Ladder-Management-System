﻿/*
Deployment script for NRLLadderDatabase

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "NRLLadderDatabase"
:setvar DefaultFilePrefix "NRLLadderDatabase"
:setvar DefaultDataPath "C:\Users\mazue\AppData\Local\Microsoft\VisualStudio\SSDT\"
:setvar DefaultLogPath "C:\Users\mazue\AppData\Local\Microsoft\VisualStudio\SSDT\"

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO
PRINT N'Dropping Foreign Key unnamed constraint on [dbo].[LadderPositions]...';


GO
ALTER TABLE [dbo].[LadderPositions] DROP CONSTRAINT [FK__LadderPos__team___3A81B327];


GO
PRINT N'Dropping Foreign Key unnamed constraint on [dbo].[Matches]...';


GO
ALTER TABLE [dbo].[Matches] DROP CONSTRAINT [FK__Matches__team_1___3B75D760];


GO
PRINT N'Dropping Foreign Key unnamed constraint on [dbo].[Matches]...';


GO
ALTER TABLE [dbo].[Matches] DROP CONSTRAINT [FK__Matches__team_2___3C69FB99];


GO
PRINT N'Starting rebuilding table [dbo].[Teams]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_Teams] (
    [team_id]     INT           IDENTITY (1, 1) NOT NULL,
    [team_name]   NVARCHAR (50) NOT NULL,
    [home_ground] NVARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([team_id] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[Teams])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Teams] ON;
        INSERT INTO [dbo].[tmp_ms_xx_Teams] ([team_id], [team_name], [home_ground])
        SELECT   [team_id],
                 [team_name],
                 [home_ground]
        FROM     [dbo].[Teams]
        ORDER BY [team_id] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Teams] OFF;
    END

DROP TABLE [dbo].[Teams];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_Teams]', N'Teams';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Creating Foreign Key unnamed constraint on [dbo].[LadderPositions]...';


GO
ALTER TABLE [dbo].[LadderPositions] WITH NOCHECK
    ADD FOREIGN KEY ([team_id]) REFERENCES [dbo].[Teams] ([team_id]);


GO
PRINT N'Creating Foreign Key unnamed constraint on [dbo].[Matches]...';


GO
ALTER TABLE [dbo].[Matches] WITH NOCHECK
    ADD FOREIGN KEY ([team_1_id]) REFERENCES [dbo].[Teams] ([team_id]);


GO
PRINT N'Creating Foreign Key unnamed constraint on [dbo].[Matches]...';


GO
ALTER TABLE [dbo].[Matches] WITH NOCHECK
    ADD FOREIGN KEY ([team_2_id]) REFERENCES [dbo].[Teams] ([team_id]);


GO
PRINT N'Checking existing data against newly created constraints';


GO
USE [$(DatabaseName)];


GO
CREATE TABLE [#__checkStatus] (
    id           INT            IDENTITY (1, 1) PRIMARY KEY CLUSTERED,
    [Schema]     NVARCHAR (256),
    [Table]      NVARCHAR (256),
    [Constraint] NVARCHAR (256)
);

SET NOCOUNT ON;

DECLARE tableconstraintnames CURSOR LOCAL FORWARD_ONLY
    FOR SELECT SCHEMA_NAME([schema_id]),
               OBJECT_NAME([parent_object_id]),
               [name],
               0
        FROM   [sys].[objects]
        WHERE  [parent_object_id] IN (OBJECT_ID(N'dbo.LadderPositions'), OBJECT_ID(N'dbo.Matches'))
               AND [type] IN (N'F', N'C')
                   AND [object_id] IN (SELECT [object_id]
                                       FROM   [sys].[check_constraints]
                                       WHERE  [is_not_trusted] <> 0
                                              AND [is_disabled] = 0
                                       UNION
                                       SELECT [object_id]
                                       FROM   [sys].[foreign_keys]
                                       WHERE  [is_not_trusted] <> 0
                                              AND [is_disabled] = 0);

DECLARE @schemaname AS NVARCHAR (256);

DECLARE @tablename AS NVARCHAR (256);

DECLARE @checkname AS NVARCHAR (256);

DECLARE @is_not_trusted AS INT;

DECLARE @statement AS NVARCHAR (1024);

BEGIN TRY
    OPEN tableconstraintnames;
    FETCH tableconstraintnames INTO @schemaname, @tablename, @checkname, @is_not_trusted;
    WHILE @@fetch_status = 0
        BEGIN
            PRINT N'Checking constraint: ' + @checkname + N' [' + @schemaname + N'].[' + @tablename + N']';
            SET @statement = N'ALTER TABLE [' + @schemaname + N'].[' + @tablename + N'] WITH ' + CASE @is_not_trusted WHEN 0 THEN N'CHECK' ELSE N'NOCHECK' END + N' CHECK CONSTRAINT [' + @checkname + N']';
            BEGIN TRY
                EXECUTE [sp_executesql] @statement;
            END TRY
            BEGIN CATCH
                INSERT  [#__checkStatus] ([Schema], [Table], [Constraint])
                VALUES                  (@schemaname, @tablename, @checkname);
            END CATCH
            FETCH tableconstraintnames INTO @schemaname, @tablename, @checkname, @is_not_trusted;
        END
END TRY
BEGIN CATCH
    PRINT ERROR_MESSAGE();
END CATCH

IF CURSOR_STATUS(N'LOCAL', N'tableconstraintnames') >= 0
    CLOSE tableconstraintnames;

IF CURSOR_STATUS(N'LOCAL', N'tableconstraintnames') = -1
    DEALLOCATE tableconstraintnames;

SELECT N'Constraint verification failed:' + [Schema] + N'.' + [Table] + N',' + [Constraint]
FROM   [#__checkStatus];

IF @@ROWCOUNT > 0
    BEGIN
        DROP TABLE [#__checkStatus];
        RAISERROR (N'An error occurred while verifying constraints', 16, 127);
    END

SET NOCOUNT OFF;

DROP TABLE [#__checkStatus];


GO
PRINT N'Update complete.';


GO
