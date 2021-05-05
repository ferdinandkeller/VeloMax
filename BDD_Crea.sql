-- on supprime la bdd si elle existe déjà
DROP DATABASE IF EXISTS VeloMax;
CREATE DATABASE IF NOT EXISTS VeloMax;
USE VeloMax;

-- on commence par créer les tables entitées
CREATE TABLE Adresse (
  numA INT UNSIGNED NOT NULL UNIQUE AUTO_INCREMENT,
  rue VARCHAR(50) NOT NULL,
  ville VARCHAR(50) NOT NULL,
  codepostal INT NOT NULL,
  province VARCHAR(50) NOT NULL,
  
  PRIMARY KEY (numA)
);

CREATE TABLE Fournisseur (
  siret INT UNSIGNED NOT NULL UNIQUE,
  nomF VARCHAR(50) NOT NULL,
  numA INT UNSIGNED NOT NULL,
  contact VARCHAR(50) NOT NULL,
  reactivite INT UNSIGNED NOT NULL,
  
  PRIMARY KEY (siret),
  FOREIGN KEY (numA) REFERENCES Adresse (numA) ON DELETE CASCADE
);

CREATE TABLE Piece (
  numP INT UNSIGNED NOT NULL UNIQUE,
  descriptionP TEXT NOT NULL,
  dateIntroP DATE NOT NULL,
  dateDiscP DATE NOT NULL,
  prixP INT UNSIGNED NOT NULL,
  quantStockP INT UNSIGNED NOT NULL,
  
  PRIMARY KEY (numP)
);

CREATE TABLE Modele (
  numM INT UNSIGNED NOT NULL UNIQUE,
  nomM VARCHAR(50) NOT NULL,
  descriptionM TEXT NOT NULL,
  tailleM INT UNSIGNED NOT NULL,
  ligne ENUM("VTT", "vélo de course", "classique", "BMX") NOT NULL,
  prixM INT UNSIGNED NOT NULL,
  dateIntroM DATE NOT NULL,
  dateDiscM DATE NOT NULL,
  quantStockM INT UNSIGNED NOT NULL,
  
  PRIMARY KEY (numM)
);

CREATE TABLE Commande (
  numC INT UNSIGNED NOT NULL UNIQUE AUTO_INCREMENT,
  numA INT UNSIGNED NOT NULL,
  dateC DATE NOT NULL,
  dateL DATE NOT NULL,
  
  PRIMARY KEY (numC),
  FOREIGN KEY (numA) REFERENCES Adresse (numA)
);

CREATE TABLE Programme (
  numProg INT UNSIGNED NOT NULL UNIQUE AUTO_INCREMENT,
  nomProg VARCHAR(50) NOT NULL,
  cout INT UNSIGNED NOT NULL,
  rabais INT UNSIGNED NOT NULL,
  duree INT UNSIGNED NOT NULL,
  
  PRIMARY KEY (numProg)
);

CREATE TABLE Boutique (
  numB INT UNSIGNED NOT NULL UNIQUE AUTO_INCREMENT,
  nomB VARCHAR(50) NOT NULL,
  numA INT UNSIGNED NOT NULL,
  telB VARCHAR(10) NOT NULL,
  mailB VARCHAR(50) NOT NULL,
  
  PRIMARY KEY (numB),
  FOREIGN KEY (numA) REFERENCES Adresse (numA)
);

CREATE TABLE Individu (
  numI INT UNSIGNED NOT NULL UNIQUE AUTO_INCREMENT,
  nomI VARCHAR(50) NOT NULL,
  prenomI VARCHAR(50) NOT NULL,
  numA INT UNSIGNED NOT NULL,
  telI CHAR(10) NOT NULL,
  mailI VARCHAR(50) NOT NULL,

  PRIMARY KEY (numI),
  FOREIGN KEY (numA) REFERENCES Adresse (numA)
);

-- on créer les tables associations
CREATE TABLE CatalFournisseur (
  siret INT UNSIGNED NOT NULL,
  numP INT UNSIGNED NOT NULL,
  numPieceF INT UNSIGNED NOT NULL,
  prixPieceF INT UNSIGNED NOT NULL,
  delaiF INT UNSIGNED NOT NULL,
  
  PRIMARY KEY (siret, numP),
  FOREIGN KEY (siret) REFERENCES Fournisseur (siret),
  FOREIGN KEY (numP) REFERENCES Piece (numP)
);

CREATE TABLE CompositionModele (
  numM INT UNSIGNED NOT NULL,
  numP INT UNSIGNED NOT NULL,
  quant INT UNSIGNED NOT NULL,
  
  PRIMARY KEY (numM, numP),
  FOREIGN KEY (numM) REFERENCES Modele (numM),
  FOREIGN KEY (numP) REFERENCES Piece (numP)
);

CREATE TABLE ContenuCommandePiece (
  numC INT UNSIGNED NOT NULL,
  numP INT UNSIGNED NOT NULL,
  quantPieceC INT UNSIGNED NOT NULL,

  PRIMARY KEY (numC, numP),
  FOREIGN KEY (numC) REFERENCES Commande (numC),
  FOREIGN KEY (numP) REFERENCES Piece (numP)
);

CREATE TABLE ContenuCommandeModele (
  numC INT UNSIGNED NOT NULL,
  numM INT UNSIGNED NOT NULL,
  quantModeleC INT UNSIGNED NOT NULL,

  PRIMARY KEY (numC, numM),
  FOREIGN KEY (numC) REFERENCES Commande (numC),
  FOREIGN KEY (numM) REFERENCES Modele (numM)
);

CREATE TABLE Fidelio (
  numI INT UNSIGNED NOT NULL,
  numProg INT UNSIGNED NOT NULL,
  dateAdherence Date NOT NULL,

  PRIMARY KEY (numI, numProg),
  FOREIGN KEY (numI) REFERENCES Individu (numI),
  FOREIGN KEY (numProg) REFERENCES Programme (numProg)
);

CREATE TABLE ExecuteurCommandeBoutique (
  numC INT UNSIGNED NOT NULL,
  numB INT UNSIGNED NOT NULL,

  PRIMARY KEY (numC, numB),
  FOREIGN KEY (numC) REFERENCES Commande (numC),
  FOREIGN KEY (numB) REFERENCES Boutique (numB)
);

CREATE TABLE ExecuteurCommandeIndividus (
  numC INT UNSIGNED NOT NULL,
  numI INT UNSIGNED NOT NULL,

  PRIMARY KEY (numC, numI),
  FOREIGN KEY (numC) REFERENCES Commande (numC),
  FOREIGN KEY (numI) REFERENCES Individu (numI)
);

-- -- on commence la populaiton de la bdd
INSERT INTO programme (nomProg, cout, rabais, duree) VALUES ('Fidélio', 15, 5, 1);
INSERT INTO programme (nomProg, cout, rabais, duree) VALUES ('Fidélio Or', 25, 8, 2);
INSERT INTO programme (nomProg, cout, rabais, duree) VALUES ('Fidélio Platine', 60, 10, 2);
INSERT INTO programme (nomProg, cout, rabais, duree) VALUES ('Fidélio Max', 100, 12, 3);

INSERT INTO adresse (numA, rue, ville, codepostal, province) VALUES (1,"rue Pastourelle","Paris",75003,"Ile-de-France");
INSERT INTO adresse (numA, rue, ville, codepostal, province) VALUES (2,"rue Jussieu","Lyon",69002,"Rhône-Alpes");
INSERT INTO adresse (numA, rue, ville, codepostal, province) VALUES (3,"rue Petit-Hutin","Reims",51100,"Grand Est");
INSERT INTO adresse (numA, rue, ville, codepostal, province) VALUES (4,"rue de Bari","Montpellier",634080,"Occitanie");
INSERT INTO adresse (numA, rue, ville, codepostal, province) VALUES (5,"2 place de la Défense","Paris",92053,"Ile-de-France");

INSERT INTO fournisseur(siret,nomF,numA,contact,reactivite) VALUES (90123,"MaxiPiecesVelo",2,"0123456781",4);

INSERT INTO Boutique(nomB,numA,telB,mailB) VALUES("Decathlon",5,"0149037520","decathlon@cnit.fr");

INSERT INTO Individu (nomI,prenomI,numA,telI,mailI) VALUES("LeBlanco","Inspecteur",3,"","LB.I@hotmail.fr");
INSERT INTO Individu (nomI,prenomI,numA,telI,mailI) VALUES("Hugo","Victor",1,"0678234567","HogoVictor@histoire.com");
