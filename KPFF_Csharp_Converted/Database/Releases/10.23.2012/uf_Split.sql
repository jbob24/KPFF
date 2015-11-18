/****** Object:  UserDefinedFunction [dbo].[uf_Split]    Script Date: 10/23/2012 20:46:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uf_Split]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[uf_Split]
GO

/****** Object:  UserDefinedFunction [dbo].[uf_Split]    Script Date: 10/23/2012 20:46:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[uf_Split]
(    
    @RowData NVARCHAR(MAX),
    @Delimeter NVARCHAR(MAX)
)
RETURNS @RtnValue TABLE 
(
    ID INT IDENTITY(1,1),
    Data NVARCHAR(MAX)
) 
AS
BEGIN 
    DECLARE @Iterator INT
    SET @Iterator = 1

    DECLARE @FoundIndex INT
    SET @FoundIndex = CHARINDEX(@Delimeter,@RowData)

    WHILE (@FoundIndex>0)
    BEGIN
        INSERT INTO @RtnValue (data)
        SELECT 
            Data = LTRIM(RTRIM(SUBSTRING(@RowData, 1, @FoundIndex - 1)))

        SET @RowData = SUBSTRING(@RowData,
                @FoundIndex + DATALENGTH(@Delimeter) / 2,
                LEN(@RowData))

        SET @Iterator = @Iterator + 1
        SET @FoundIndex = CHARINDEX(@Delimeter, @RowData)
    END
    
    INSERT INTO @RtnValue (Data)
    SELECT Data = LTRIM(RTRIM(@RowData))

    RETURN
END
GO


