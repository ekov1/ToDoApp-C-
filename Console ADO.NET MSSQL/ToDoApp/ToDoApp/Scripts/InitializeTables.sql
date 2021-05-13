USE [todo_db]

-- INSERT USERROLES
INSERT INTO [todo_db].[dbo].[UserRole] ( [UserRole] )
VALUES
  ('Admin'), 
  ('RegularUser')

-- INSERT ADMIN USER
INSERT INTO [todo_db].[dbo].[Entity]([DateCreated], [IdOfCreator], [LastModifiedAt], [LastModifiedBy])
VALUES
(GETDATE(),1,GETDATE(),1)

INSERT INTO [todo_db].[dbo].[User]([EntityId],[Username],[Password],[FirstName],[LastName],[UserRoleId] )
VALUES
(1,'admin','adminpassword','Chuck','Norris',1)



