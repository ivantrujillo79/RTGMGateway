IF EXISTS (SELECT * 
  FROM sys.foreign_keys 
   WHERE object_id = OBJECT_ID(N'dbo.FK_Llamada_Pedido')
   AND parent_object_id = OBJECT_ID(N'dbo.llamada')
)
alter table llamada drop constraint FK_Llamada_Pedido