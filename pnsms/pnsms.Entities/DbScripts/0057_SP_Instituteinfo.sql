alter procedure SprInstituteInfo
     @InstituteId int

	 as
	 select ins.Name,ins.ContactText,img.ImageBinaryData from Institutes as ins
	  inner join [dbo].[Images] as img on img.RefPrimaryKey=ins.Id where ins.Id=@InstituteId and img.RefPrimaryKey=@InstituteId and RefTypeId=12 

	--exec SprInstituteInfo 5