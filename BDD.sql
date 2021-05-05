USE VeloMax;

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