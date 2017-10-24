INSERT INTO dbo.Product ([Title], [Description], [DealerId], [Image], [Price], [QuantitySold], [AvgStars], [Rowid], [CreatedOn], [CreatedBy], [ChangedOn], [ChangedBy])
VALUES(@Title, @Description, @DealerId, @Image, @Price, @QuantitySold, @AvgStars, @Rowid, @CreatedOn, @CreatedBy, @ChangedOn, @ChangedBy); SELECT SCOPE_IDENTITY();


UPDATE dbo.Product SET [Title]=@Title, [Description]=@Description, [DealerId]=@DealerId, [Image]=@Image, [Price]=@Price, [QuantitySold]=@QuantitySold, [AvgStars]=@AvgStars, [Rowid]=@Rowid, [ChangedOn]=@ChangedOn, [ChangedBy]=@ChangedBy
WHERE Id = @Id;
