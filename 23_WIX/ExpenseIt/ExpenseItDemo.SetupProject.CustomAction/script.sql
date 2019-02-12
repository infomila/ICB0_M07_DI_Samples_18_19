create table prova (
	id number primary key,
	nom varchar(30)
);
create table persona (
	id number primary key,
	nom varchar(30) not null, 
	cognom varchar(30)  not null,
);
