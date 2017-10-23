INSERT INTO dbo.Dealer ([FirstName], [LastName], [CategoryId], [CountryId], [Description], [TotalProducts], [Rowid], [CreatedBy], [ChangedOn], [ChangedBy]) 
VALUES(@Name, @CreatedOn, @CreatedBy, @ChangedOn, @ChangedBy); SELECT SCOPE_IDENTITY();
