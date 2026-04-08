use SalleDeSport;

DROP USER IF EXISTS 'AdminGerant1'@'localhost';
DROP USER IF EXISTS 'AdminGerant2'@'localhost';
DROP USER IF EXISTS 'Membre'@'localhost';
DROP USER IF EXISTS 'Evaluation'@'localhost';

-- Gérant principale 
CREATE USER 'AdminGerant1'@'localhost' IDENTIFIED BY 'Root';
GRANT ALL PRIVILEGES ON SalleDeSport.* TO 'AdminGerant1'@'localhost';

-- Gérant secondaire
CREATE USER 'AdminGerant2'@'localhost' IDENTIFIED BY 'root';
GRANT SELECT,INSERT,DELETE,UPDATE ON SalleDeSport.Coach TO 'AdminGerant2'@'localhost';
GRANT SELECT,INSERT,DELETE,UPDATE ON SalleDeSport.Cours TO 'AdminGerant2'@'localhost';
GRANT SELECT,INSERT,DELETE,UPDATE ON SalleDeSport.Membre TO 'AdminGerant2'@'localhost';
GRANT SELECT,INSERT,DELETE,UPDATE ON SalleDeSport.Reservation TO 'AdminGerant2'@'localhost';

-- Membre
CREATE USER 'Membre'@'localhost' IDENTIFIED BY 'membre';
GRANT INSERT ON SalleDeSport.Membre TO 'Membre'@'localhost';
GRANT SELECT ON SalleDeSport.Cours TO 'Membre'@'localhost';
GRANT SELECT ON SalleDeSport.Coach TO 'Membre'@'localhost';
GRANT SELECT,INSERT,DELETE ON SalleDeSport.Reservation TO 'Membre'@'localhost';

-- Interface évaluation 
CREATE USER 'Evaluation'@'localhost' IDENTIFIED BY 'evaluation';
GRANT SELECT ON SalleDeSport.* TO 'Evaluation'@'localhost';

FLUSH PRIVILEGES;