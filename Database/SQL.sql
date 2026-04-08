DROP DATABASE IF EXISTS SalleDeSport;
CREATE DATABASE SalleDeSport;
USE SalleDeSport;

CREATE TABLE Administrateur (
  ID int NOT NULL auto_increment,
  Nom varchar(50) default null,
  Prenom varchar(30) DEFAULT NULL,
  Tel varchar(30) DEFAULT NULL,
  Email varchar(50) default null,
  Mdp varchar(50) DEFAULT NULL,
  Role varchar(30) DEFAULT NULL,
  PRIMARY KEY (ID)
);

CREATE TABLE Membre (
  ID int NOT NULL auto_increment,
  Nom varchar(30) DEFAULT NULL,
  Prenom varchar(30) DEFAULT NULL,
  Email varchar(50) DEFAULT NULL,
  Telephone varchar(30) DEFAULT NULL,
  Adresse varchar(50) DEFAULT NULL,
  Mdp varchar(50) default null,
  DateInscription DateTime DEFAULT NULL,
  PRIMARY KEY (ID)
);

CREATE TABLE Coach (
  ID int NOT NULL auto_increment,
  Nom varchar(30) DEFAULT NULL,
  Prenom varchar(30) DEFAULT NULL,
  Telephone varchar(30) DEFAULT NULL,
  Specialite varchar(30) DEFAULT NULL,
  Email varchar(50) DEFAULT NULL,
  PRIMARY KEY (ID)
);


CREATE TABLE Cours (
  ID int NOT NULL auto_increment,
  Nom varchar(30) DEFAULT NULL,
  Description varchar(50) DEFAULT NULL,
  DateHeure DateTime DEFAULT NULL,
  DureeMinutes int NOT NULL,
  CapaciteMax int NOT NULL,
  NiveauDifficulte int NOT NULL,
  CoachID int NOT NULL,
  CoachNom varchar(30) DEFAULT NULL,
  PRIMARY KEY (ID),
  FOREIGN KEY (CoachID) REFERENCES Coach(ID)
);


CREATE TABLE Reservation (
  ID int NOT NULL auto_increment,
  MembreID int NOT NULL,
  CoursID int NOT NULL,
  DateReservation DateTime DEFAULT NULL,
  Statut varchar(50) DEFAULT NULL,
  PRIMARY KEY (ID),
  FOREIGN KEY (MembreID) REFERENCES Membre(ID),
  FOREIGN KEY (CoursID) REFERENCES Cours(ID)
);



CREATE TABLE Inscription (
  ID int NOT NULL auto_increment,
  Statut varchar(50) DEFAULT NULL,
  MembreID int NOT NULL,
  AdminID int NULL,
  PRIMARY KEY (ID),
  FOREIGN KEY (MembreID) REFERENCES Membre(ID),
  FOREIGN KEY (AdminID) REFERENCES Administrateur(ID)
);

INSERT INTO Administrateur (Nom, Prenom, Tel, Email, Mdp, Role) VALUES
('Dubois','Julien','0701010101','julien.dubois@salledesport.fr','DuboisJulien','AdminGerant1'),
('Moreau','Sophie','0702020202','sophie.moreau@salledesport.fr','MoreauSophie','AdminGerant2');
INSERT INTO Coach (Nom, Prenom, Telephone, Specialite, Email) VALUES
('Martin','Lucas','0710101010','Cardio','lucas.martin@gmail.com'),
('Bernard','Claire','0720202020','Yoga','claire.bernard@gmail.com'),
('Petit','Antoine','0730303030','Musculation','antoine.petit@gmail.com'),
('Robert','Marie','0740404040','Pilates','marie.robert@gmail.com'),
('Richard','Thomas','0750505050','Crossfit','thomas.richard@gmail.com');
INSERT INTO Cours (Nom, Description, DateHeure, DureeMinutes, CapaciteMax, NiveauDifficulte, CoachID, CoachNom) VALUES
('Yoga Matinal','Cours doux pour bien commencer la journée', '2025-12-22 08:00:00', 60, 15, 1, 1, 'Lucas Martin'),
('Cardio Intense','Cours pour brûler un maximum de calories', '2025-12-22 10:00:00', 45, 20, 3, 2, 'Claire Bernard'),
('Pilates Avancé','Renforcement musculaire et posture', '2025-12-23 09:00:00', 60, 12, 2, 4, 'Marie Robert'),
('Crossfit Débutant','Initiation au crossfit', '2025-12-23 11:00:00', 50, 10, 1, 5, 'Thomas Richard'),
('Musculation','Renforcement général', '2025-12-24 14:00:00', 70, 18, 2, 3, 'Antoine Petit');

INSERT INTO Membre (Nom, Prenom, Email, Telephone, Adresse, Mdp, DateInscription) VALUES
('Lefevre','Jean','jean.lefevre@gmail.com','0601000001','10 rue A','LefevreJean',NOW()),
('Morel','Claire','claire.morel@gmail.com','0601000002','11 rue B','MorelClaire',NOW()),
('Laurent','Paul','paul.laurent@gmail.com','0601000003','12 rue C','LaurentPaul',NOW()),
('Simon','Lucie','lucie.simon@gmail.com','0601000004','13 rue D','SimonLucie',NOW()),
('Michel','Pierre','pierre.michel@gmail.com','0601000005','14 rue E','MichelPierre',NOW()),
('Garcia','Emma','emma.garcia@gmail.com','0601000006','15 rue F','GarciaEmma',NOW()),
('David','Louis','louis.david@gmail.com','0601000007','16 rue G','DavidLouis',NOW()),
('Bertrand','Julie','julie.bertrand@gmail.com','0601000008','17 rue H','BertrandJulie',NOW()),
('Roux','Antoine','antoine.roux@gmail.com','0601000009','18 rue I','RouxAntoine',NOW()),
('Fournier','Marie','marie.fournier@gmail.com','0601000010','19 rue J','FournierMarie',NOW()),

('Morel','Thomas','thomas.morel@gmail.com','0601000011','20 rue K','MorelThomas',NOW()),
('Blanc','Alice','alice.blanc@gmail.com','0601000012','21 rue L','BlancAlice',NOW()),
('Henry','Lucas','lucas.henry@gmail.com','0601000013','22 rue M','HenryLucas',NOW()),
('Vidal','Emma','emma.vidal@gmail.com','0601000014','23 rue N','VidalEmma',NOW()),
('Perrin','Maxime','maxime.perrin@gmail.com','0601000015','24 rue O','PerrinMaxime',NOW()),

('Garcia','Sophie','sophie.garcia@gmail.com','0601000016','25 rue P','GarciaSophie',NOW()),
('Jean','Lucas','lucas.jean@gmail.com','0601000017','26 rue Q','JeanLucas',NOW()),
('Louis','Marie','marie.louis@gmail.com','0601000018','27 rue R','LouisMarie',NOW()),
('Robert','Paul','paul.robert@gmail.com','0601000019','28 rue S','RobertPaul',NOW()),
('Richard','Emma','emma.richard@gmail.com','0601000020','29 rue T','RichardEmma',NOW()),

('Petit','Clement','clement.petit@gmail.com','0601000021','30 rue U','PetitClement',NOW()),
('Martin','Alice','alice.martin@gmail.com','0601000022','31 rue V','MartinAlice',NOW()),
('Dubois','Lucas','lucas.dubois@gmail.com','0601000023','32 rue W','DuboisLucas',NOW()),
('Moreau','Julie','julie.moreau@gmail.com','0601000024','33 rue X','MoreauJulie',NOW()),
('Laurent','Emma','emma.laurent@gmail.com','0601000025','34 rue Y','LaurentEmma',NOW()),

('Simon','Paul','paul.simon@gmail.com','0601000026','35 rue Z','SimonPaul',NOW()),
('Michel','Claire','claire.michel@gmail.com','0601000027','36 rue AA','MichelClaire',NOW()),
('Garcia','Lucas','lucas.garcia@gmail.com','0601000028','37 rue AB','GarciaLucas',NOW()),
('David','Sophie','sophie.david@gmail.com','0601000029','38 rue AC','DavidSophie',NOW()),
('Bertrand','Louis','louis.bertrand@gmail.com','0601000030','39 rue AD','BertrandLouis',NOW()),

('Roux','Marie','marie.roux@gmail.com','0601000031','40 rue AE','RouxMarie',NOW()),
('Fournier','Antoine','antoine.fournier@gmail.com','0601000032','41 rue AF','FournierAntoine',NOW()),
('Morel','Alice','alice.morel@gmail.com','0601000033','42 rue AG','MorelAlice',NOW()),
('Blanc','Lucas','lucas.blanc@gmail.com','0601000034','43 rue AH','BlancLucas',NOW()),
('Henry','Marie','marie.henry@gmail.com','0601000035','44 rue AI','HenryMarie',NOW()),

('Vidal','Paul','paul.vidal@gmail.com','0601000036','45 rue AJ','VidalPaul',NOW()),
('Perrin','Emma','emma.perrin@gmail.com','0601000037','46 rue AK','PerrinEmma',NOW()),
('Garcia','Maxime','maxime.garcia@gmail.com','0601000038','47 rue AL','GarciaMaxime',NOW()),
('Jean','Sophie','sophie.jean@gmail.com','0601000039','48 rue AM','JeanSophie',NOW()),
('Louis','Lucas','lucas.louis@gmail.com','0601000040','49 rue AN','LouisLucas',NOW());
