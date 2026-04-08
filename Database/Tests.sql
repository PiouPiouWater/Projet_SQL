use SalleDeSport;

-- 1-Requete avec sous requete
SELECT * FROM Membre;
select * from Coach;
select * from Administrateur;
-- Trouver les cours qui ont plus de places que la moyenne
SELECT Nom, CapaciteMax, DureeMinutes
FROM Cours
WHERE CapaciteMax > (SELECT AVG(CapaciteMax) FROM Cours);

-- Afficher les membres ayant fait au moins une réservation
SELECT Nom, Prenom, Email
FROM Membre
WHERE ID IN (SELECT MembreID FROM Reservation);


-- 2-Requete ensembliste*
-- Combiner la liste des membres et des coachs
SELECT Nom, Prenom, Email, 'Membre' AS Type
FROM Membre
UNION
SELECT Nom, Prenom, Email, 'Coach' AS Type
FROM Coach;


-- 3-Jointure SQL1
-- Afficher les cours avec les informations du coach (syntaxe WHERE)
SELECT Cours.Nom AS NomCours, Cours.DateHeure, Coach.Nom, Coach.Prenom, Coach.Specialite
FROM Cours, Coach
WHERE Cours.CoachID = Coach.ID;


-- 4-Jointure SQL2
-- Afficher les réservations avec membre et cours (syntaxe JOIN)
SELECT Membre.Nom, Membre.Prenom, Cours.Nom AS NomCours, 
       Reservation.DateReservation, Reservation.Statut
FROM Reservation
INNER JOIN Membre ON Reservation.MembreID = Membre.ID
INNER JOIN Cours ON Reservation.CoursID = Cours.ID;

-- 5-Left join
-- Afficher tous les cours et compter leurs réservations (même ceux sans réservation)
SELECT Cours.Nom, Cours.DateHeure, Cours.CapaciteMax, 
       COUNT(Reservation.ID) AS NombreReservations
FROM Cours
LEFT JOIN Reservation ON Cours.ID = Reservation.CoursID
GROUP BY Cours.ID, Cours.Nom, Cours.DateHeure, Cours.CapaciteMax;



-- 6-Right join
-- Afficher tous les membres avec leurs réservations (même ceux qui n'ont jamais réservé)
SELECT Membre.Nom, Membre.Prenom, Membre.Email, 
       Cours.Nom AS NomCours, Reservation.DateReservation
FROM Reservation
RIGHT JOIN Membre ON Reservation.MembreID = Membre.ID
LEFT JOIN Cours ON Reservation.CoursID = Cours.ID;


-- 7-Fonction d'agregation
-- Compter le nombre total de membres
SELECT COUNT(*) AS TotalMembres
FROM Membre;
-- Calculer le total de places disponibles dans tous les cours
SELECT SUM(CapaciteMax) AS TotalPlacesDisponibles
FROM Cours;
-- Calculer la durée moyenne des cours
SELECT AVG(DureeMinutes) AS DureeMoyenne
FROM Cours;
-- Trouver la capacité minimale parmi tous les cours
SELECT MIN(CapaciteMax) AS CapaciteMinimale
FROM Cours;
-- Trouver la capacité maximale parmi tous les cours
SELECT MAX(CapaciteMax) AS CapaciteMaximale
FROM Cours;
-- Afficher toutes les spécialités des coachs en une seule ligne
SELECT GROUP_CONCAT(DISTINCT Specialite SEPARATOR ', ') AS ListeSpecialites
FROM Coach;