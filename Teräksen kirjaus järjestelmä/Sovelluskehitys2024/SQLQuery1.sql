create table tuotteet (id INTEGER IDENTITY(1,1) PRIMARY KEY, materiaali VARCHAR(50), muoto VARCHAR(50), mitat VARCHAR(50), määrä VARCHAR(50));
create table asiakkaat (id INTEGER IDENTITY(1,1) PRIMARY KEY, yrityksen_nimi VARCHAR(50), yhteys_henkilö VARCHAR(50), puhelin_numero VARCHAR(50));
create table varasto (id INTEGER IDENTITY(1,1) PRIMARY KEY, hylly_paikka varchar(50));
create table myyty (id INTEGER IDENTITY(1,1) PRIMARY KEY, asiakas varchar(50), tuote_id int references tuotteet(id), määrä int)

drop table varasto;
UPDATE tuotteet SET määrä = 1 where id = 1;
ALTER TABLE tuotteet ADD hylly_paikka varchar(50);
--drop table asiakkaat;
UPDATE tuotteet SET määrä=0 WHERE materiaali='ALU' AND muoto = 'neliöputki' AND mitat = '30x30x2';
--DELETE FROM tuotteet WHERE id=13;
--Select määrä from tuotteet WHERE materiaali = 'ALU' AND muoto = 'neliöputki' AND mitat = '50x50x3';

ALTER TABLE myyty ADD määrä int;

--drop table asiakkaat;
select * from Tuotteet;
SELECT id FROM tuotteet

select tuotteet.id,tuotteet.materiaali, tuotteet.muoto, tuotteet.mitat, varasto.hylly_paikka from tuotteet, varasto where tuotteet.id=varasto.palkin_id;
select * from tuotteet;
select * from varasto;



INSERT INTO varasto (hylly_paikka) VALUES ('B4');



SELECT hylly_paikka FROM varasto;















--CREATE TABLE tuotteet (id INTEGER IDENTITY(1,1) PRIMARY KEY, nimi VARCHAR(50), hinta INTEGER);

--CREATE TABLE asiakkaat (id INTEGER IDENTITY(1,1) PRIMARY KEY, nimi VARCHAR(50), osoite VARCHAR(150), puhelin VARCHAR(50));

--CREATE TABLE tilaukset (id INTEGER IDENTITY(1,1) PRIMARY KEY, asiakas_id INTEGER REFERENCES asiakkaat ON DELETE CASCADE, tuote_id INTEGER REFERENCES tuotteet ON DELETE CASCADE, toimitettu BIT DEFAULT 0);

--INSERT INTO tuotteet (nimi, hinta) VALUES ('juusto', 6);
--INSERT INTO asiakkaat (nimi, osoite, puhelin) VALUES ('Masa', 'Kuusikuja 6', '050882682');
--INSERT INTO tilaukset (asiakas_id, tuote_id) VALUES (1,1); 

--DELETE FROM tuotteet WHERE id=5;

--SELECT * FROM tuotteet;
--SELECT * FROM asiakkaat;
--SELECT * FROM tilaukset;

--UPDATE tilaukset SET toimitettu=1 WHERE id=1

--SELECT ti.id as id, a.nimi as asiakas, tu.nimi as tuote FROM tilaukset ti, asiakkaat a, tuotteet tu WHERE a.id=ti.asiakas_id AND tu.id=ti.tuote_id

--DELETE FROM tuotteet WHERE nimi="kinkku";

--DROP TABLE tilaukset;