# TennisPlayers
- Dans cette solution, il y a deux projets :

TennisPlayers (.NET 6) : L'API REST qui expose les informations sur les joueurs de tennis. Pour tester l'API, il vous suffit de lancer l'application et d'utiliser la documentation Swagger.

TestUnitaire (xUnit) : Projet pour les tests unitaires de l'API. Pour exécuter les tests, effectuez un clic droit sur le projet et sélectionnez "Lancer les tests".

- Fonctionnalités supplémentaires ajoutées :

GetPlayersByCountry : Récupère les joueurs de tennis d'un pays donné via le code du pays.

GetTopRankedPlayers : Récupère les joueurs de tennis les mieux classés, limités à un nombre spécifique.


- Outils à installer pour chaque projet :
TennisPlayers:
Dans le projet TennisPlayers, vous devez installer les dépendances suivantes :

dotnet add package Microsoft.AspNetCore.Mvc

dotnet add package Microsoft.Extensions.Http

dotnet add package System.Text.Json


TestUnitaire:
dotnet add package xunit

dotnet add package Moq

dotnet add package Microsoft.AspNetCore.Mvc.Testing

dotnet add package xunit.runner.visualstudio
