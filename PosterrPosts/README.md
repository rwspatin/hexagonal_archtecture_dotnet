# Stride Challange - Posterr Api

Autor: Renan Winter Spatin

## Tech Infos

    C# -> .Net 6
    EF6
    REST Api
    Swagger
    Memory Cache
    PostgreSql
    XUnit

## Archtecture

* Layers Archtecture
    - The choose of this kind of archtecture was to make easies to segregate the responsabilities of each part of the solution
* Choosen layers
  - Presentation: Contains the Api
  - Application: Contains the services and the business rules
  - Domain: Contains the applications domains of business and of database
  - Infra: Contains the logic to save data on the DataBase
  - Test: Contains the automated tests

## Tests
* Technologies
  - XUnit
  - In-Memory database
  - Moq
* Layers Tested
  - Repositories: With this layer is possible to test the database save directly
  - Services: With this layer is possible to garantee the business rules and the test cases

## Run the application
* To run this api you can use the Docker file existent on folder PosterrPosts.Api with name 'Dockerfile'
* The other way to run this application is openning on VisualStudio and run the PosterrPost.Api project

OBS: The application as default will show the swagger page as 'https://localhost:7094/index.html' for example, without the 'swagger' term

OBS2: The config of the application contains the connection string to conect with the PostgreSql created on Heroku

## Be thorough
To this application be a better solution could have
* Cache with redis to be more effective with all of searchs
* Maybe change the ORM to Dapper for the writing and use the EF6 just to read infos
* Create a Yaml pipeline to be easier to publish the application
* Create a Integration test to garantee the funcionalities after publish
* Create more unit tests to have a big test covered
* Use ILogger to log the application execution and to be easier to finder possible errors

Questions:
* If this project were to grow and have many users and posts, which parts do you think would fail first?
  - I Think the part that get most of posts because this is the main of this application, and when have many posts could be difficult to cache all of this posts, so will have to cache only the dayle posts or some strategy like this
* In a real-life situation, what steps would you take to scale this product? What other types of technology and infrastructure might you need to use?
  - Publish this application using Kubernets with Docker to create a auto scale
  - Use a Yaml Pipeline to have a best continuous delivery
  - Use the SQL Server to have better performance on the queries
  - Create index on the post table
  - Use Datadogs to monitoring the application