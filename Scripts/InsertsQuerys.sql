use BonoCorpAleman;

insert into Entidad (ID_email,Password,Nombre,Apellido) VALUES ('lagh3.30@gmail.com','admin','luis','Galindo Honores')
insert into Entidad (ID_email,Password,Nombre,Apellido) VALUES ('drugoalex@gmail.com','admin','omar','Chavez Olivera')

select * from Entidad

insert into Capitalizacion values ('Diaria')
insert into Capitalizacion values ('Quincenal')
insert into Capitalizacion values ('Mensual')
insert into Capitalizacion values ('Bimestral')
insert into Capitalizacion values ('Trimestral')
insert into Capitalizacion values ('Cuatrimestral')
insert into Capitalizacion values ('Semestral')
insert into Capitalizacion values ('Anual')

select * from Capitalizacion

insert into TipoTasa values ('Nominal')
insert into TipoTasa values ('Efectiva')

select * from TipoTasa

insert into PlazoGracia (Nombre, Descripcion) values ('Sin Plazo', 'Periodo se paga con normalidad.')
insert into PlazoGracia (Nombre, Descripcion) values ('Parcial', 'Solo se pagan intereses.')
insert into PlazoGracia (Nombre, Descripcion) values ('Total', 'No se paga nada.')

select * from PlazoGracia

select * from Bono
select * from Bono_Tasa
select * from PlazoBono
select * from Inflacion
select * from Costes_Gastos