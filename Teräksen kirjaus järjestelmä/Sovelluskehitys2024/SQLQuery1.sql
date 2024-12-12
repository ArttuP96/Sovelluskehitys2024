create table tuotteet (id INTEGER IDENTITY(1,1) PRIMARY KEY, materiaali VARCHAR(50), muoto VARCHAR(50), mitat VARCHAR(50), määrä VARCHAR(50), hylly_paikka VARCHAR(50));
create table asiakkaat (id INTEGER IDENTITY(1,1) PRIMARY KEY, yrityksen_nimi VARCHAR(50), yhteys_henkilö VARCHAR(50), puhelin_numero VARCHAR(50));
create table varasto (id INTEGER IDENTITY(1,1) PRIMARY KEY, hylly_paikka varchar(50));
create table myyty (id INTEGER IDENTITY(1,1) PRIMARY KEY, asiakas varchar(50), tuote_id int references tuotteet(id), määrä int, aika varchar(50))

drop table myyty;
