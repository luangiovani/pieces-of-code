/*
PROCEDURE AND TRIGGER CREATED when worked in CBL Project.
*/

/****** Object:  StoredProcedure [dbo].[SP_INS_UP_SERVICEORDER]    Script Date: 24/10/2017 15:59:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_INS_UP_SERVICEORDER]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SP_INS_UP_SERVICEORDER] AS' 
END
GO


ALTER PROCEDURE [dbo].[SP_INS_UP_SERVICEORDER]
 @SERVICEORDER_ID DECIMAL(18,0) NULL
,@USER_ID VARCHAR(128)
,@CUSTOMER_ID INT NULL
,@REFERREDBY VARCHAR(250) NULL
,@DATEASSIGNED VARCHAR(15) NULL
,@USERASSIGNED_ID VARCHAR(128) NULL
,@STATUS VARCHAR(150)
,@EXTENSIONSTATUS VARCHAR(500) NULL
,@TAKENBY VARCHAR(250) NULL	
,@CSR VARCHAR(250) NULL
,@TYPEOFSERVICE VARCHAR(250)
,@SERVICETOEXECUTE VARCHAR(2000)
,@ESTIMATE DECIMAL(18,2) NULL
,@LOCATIONRECEIVED_ID INT
,@LOCATION_ID INT NULL
,@ARRIVEDBY VARCHAR(250) NULL
,@WAYBILLNUMBER VARCHAR(100) NULL
,@PACKAGECONDIDITION VARCHAR(250) NULL
,@SMARTNUMBER VARCHAR(100) NULL
,@TECHSNAME VARCHAR(250) NULL
,@ID_OLD INT NULL
,@ORIGINOFSERVICEORDER VARCHAR(500) NULL
,@MOSTIMPORTANTFILESTORECOVERY VARCHAR(2000) NULL
,@NOTE VARCHAR(5000) NULL
,@CUSTOMERCONTACT_ID INT NULL
,@CUSTOMERCONTACTNAME VARCHAR(200) NULL
,@CUSTOMERCONTACTEMAIL VARCHAR(150) NULL
,@CUSTOMERCONTACTTELEPHONE VARCHAR(20) NULL
,@CUSTOMERCONTACTMOBILE VARCHAR(20) NULL
,@APPROVALDATE VARCHAR(15) NULL
,@SUBSTATUS VARCHAR(150) NULL
AS
SET NOCOUNT ON;  

IF @SERVICEORDER_ID IS NULL OR @SERVICEORDER_ID = 0
	BEGIN
		BEGIN TRY
			INSERT INTO [dbo].[ServiceOrder]
           ([date]
           ,[user_id]
           ,[customer_id]
           ,[customerContact_id]
           ,[customerContactName]
           ,[customerContactEmail]
           ,[customerContactTelephone]
           ,[customerContactMobile]
           ,[referredBy]
           ,[dateAssigned]
           ,[userAssigned_id]
           ,[status]
           ,[extensionStatus]
           ,[takenBy]
           ,[CSR]
           ,[typeOfService]
           ,[serviceToExecute]
           ,[mostImportantFilesToRecovery]
           ,[estimate]
           ,[locationReceived_id]
           ,[location_id]
           ,[arrivedBy]
           ,[wayBillNumber]
           ,[packageCondidition]
           ,[smartNumber]
           ,[techsName]
           ,[id_old]
           ,[note]
           ,[originOfServiceOrder]
           ,[approvalDate]
           ,[statusDate]
           ,[subStatus]
           ,[inTransfer])
     VALUES
           (GETDATE()
           ,@USER_ID
           ,@CUSTOMER_ID
           ,@CUSTOMERCONTACT_ID
           ,@CUSTOMERCONTACTNAME
           ,@CUSTOMERCONTACTEMAIL
           ,@CUSTOMERCONTACTTELEPHONE
           ,@CUSTOMERCONTACTMOBILE
           ,@REFERREDBY
           ,CONVERT(DATETIME, @DATEASSIGNED, 103)
           ,@USERASSIGNED_ID
           ,@STATUS
           ,@EXTENSIONSTATUS
           ,@TAKENBY
           ,@CSR
           ,@TYPEOFSERVICE
           ,@SERVICETOEXECUTE
           ,@MOSTIMPORTANTFILESTORECOVERY
           ,@ESTIMATE
           ,@LOCATIONRECEIVED_ID
           ,@LOCATION_ID
           ,@ARRIVEDBY
           ,@WAYBILLNUMBER
           ,@PACKAGECONDIDITION
           ,@SMARTNUMBER
           ,@TECHSNAME
           ,@ID_OLD
           ,@NOTE
           ,@ORIGINOFSERVICEORDER
           ,CONVERT(DATETIME, @APPROVALDATE, 103)
           ,GETDATE()
           ,@SUBSTATUS
           ,0);
			SET @SERVICEORDER_ID = IDENT_CURRENT('ServiceOrder');
		END TRY  
		BEGIN CATCH  
			 SET @SERVICEORDER_ID = -1;
		END CATCH  
	END
ELSE
	BEGIN
		BEGIN TRY  
			DECLARE @SavedStatus VARCHAR(250);

			SELECT @SavedStatus = STATUS FROM ServiceOrder WHERE serviceOrder_id = @SERVICEORDER_ID;

			UPDATE [dbo].[ServiceOrder]
			   SET [customer_id] = @CUSTOMER_ID
				  ,[referredBy] = @REFERREDBY
				  ,[dateAssigned] = CONVERT(DATETIME, @DATEASSIGNED, 103)
				  ,[userAssigned_id] = @USERASSIGNED_ID
				  ,[status] = @STATUS
				  ,[extensionStatus] = @EXTENSIONSTATUS
				  ,[takenBy] = @TAKENBY
				  ,[CSR] = @CSR
				  ,[typeOfService] = @TYPEOFSERVICE
				  ,[serviceToExecute] = @SERVICETOEXECUTE
				  ,[estimate] = @ESTIMATE
				  ,[locationReceived_id] = @LOCATIONRECEIVED_ID
				  ,[location_id] = @LOCATION_ID
				  ,[arrivedBy] = @ARRIVEDBY
				  ,[wayBillNumber] = @WAYBILLNUMBER
				  ,[packageCondidition] = @PACKAGECONDIDITION
				  ,[smartNumber] = @SMARTNUMBER
				  ,[techsName] = @TECHSNAME
				  ,[id_old] = @ID_OLD
				  ,[originOfServiceOrder] = @ORIGINOFSERVICEORDER
				  ,[mostImportantFilesToRecovery] = @MOSTIMPORTANTFILESTORECOVERY
				  ,[note] = @NOTE
				  ,[customerContact_id] = @CUSTOMERCONTACT_ID
				  ,[customerContactName] = @CUSTOMERCONTACTNAME
				  ,[customerContactEmail] = @CUSTOMERCONTACTEMAIL
				  ,[customerContactTelephone] = @CUSTOMERCONTACTTELEPHONE
				  ,[customerContactMobile] = @CUSTOMERCONTACTMOBILE,
				  approvalDate = CONVERT(DATETIME, @APPROVALDATE, 103),
				  statusDate = CASE WHEN statusDate IS NULL THEN GETDATE() ELSE (CASE WHEN ISNULL(@SavedStatus, '') <> @STATUS THEN GETDATE() ELSE statusDate END) END,
				  subStatus = @SUBSTATUS
			 WHERE serviceOrder_id = @SERVICEORDER_ID
		END TRY  
		BEGIN CATCH  
			 SET @SERVICEORDER_ID = -2;
		END CATCH  
	END

SELECT @SERVICEORDER_ID;



GO
/****** Object:  Trigger [dbo].[TRG_CUSTOMER]    Script Date: 24/10/2017 15:59:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[TRG_CUSTOMER]'))
EXEC dbo.sp_executesql @statement = N'CREATE TRIGGER [dbo].[TRG_CUSTOMER]
   ON [dbo].[Customer]
   AFTER INSERT,UPDATE
AS 
BEGIN
	
	SET NOCOUNT ON;

	UPDATE C
       SET C.cpfCnpj = REPLACE(REPLACE(REPLACE(REPLACE(C.cpfCnpj, '' '',''''),''.'',''''),''-'',''''),''/'',''''),
	   C.postalZipCode = REPLACE(REPLACE(REPLACE(REPLACE(C.postalZipCode, '' '',''''),''.'',''''),''-'',''''),''/'','''')
      FROM Customer C
      JOIN inserted i on i.customer_id = C.customer_id;

END

' 
GO
/****** Object:  Trigger [dbo].[TRG_SERVICEORDER]    Script Date: 24/10/2017 15:59:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[TRG_SERVICEORDER]'))
EXEC dbo.sp_executesql @statement = N'
CREATE TRIGGER [dbo].[TRG_SERVICEORDER]
   ON [dbo].[ServiceOrder]
   AFTER INSERT,UPDATE
AS 
BEGIN
	
	SET NOCOUNT ON;

	UPDATE S
       SET S.statusDate = GETDATE()
      FROM ServiceOrder S
      JOIN inserted i on i.serviceOrder_id = S.serviceOrder_id
	 WHERE i.status <> s.status;

END
' 
GO