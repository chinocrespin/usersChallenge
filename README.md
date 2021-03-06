# Users Challenge

**How to execute the application**
1. First you should open the project solution in Visual Studio 2017 or Visual Code and build the assemblies to ensure to generate all the dlls correctly.
2. Run the UsersChallenge project. It could ask you to trust in the SSL certificate first.
3. With the api deployed you should be able to send requests to the api's port following this schema: 
[SERVER]:[PORT]/api/[CONTROLLER]
(server should be localhost to test it locally)
4. To test the endpoints I suggest to use Postman (I was going to add Swagger but did not developed it time to do that)

**Endpoints**
- GetUsers (Paged): `GET /api/users?pageSize=XX&pageNumber=YY` where XX is the count of element per page we want to get and YY the page number we want to consult.
- GetUserByIdValue: `GET /api/users/XXXX` where XXXX is the idValue of the user we are requesting
- Delete user: `DELETE /api/users/XXXX` where XXXX is the idValue of the user we want to delete from the database
- Update user:
~~~~
PUT /api/users
Content-Type: application/json
{
    "gender": "female",
    "name": "miss noemie blanchard",
    "email": "noemie.blanchard@example.com",
    "birthDate": "1987-10-11T02:46:12",
    "uuid": "57f41dd3-38ce-475b-9194-89dfdb7cac57",
    "userName": "smallwolf654",
    "location": {
        "state": "bas-rhin",
        "street": "8795 rue dugas-montbel",
        "city": "strasbourg",
        "postCode": "14298",
        "idValue": "662b56da-ab36-44ca-9fef-8c0d8547d397"
    },
    "idValue": "6459ef44-9bae-4233-a193-a12e18e297ee"
}
~~~~

**Implementation**
This API Rest was implemented following Onion Architecture and resulting in 3 big layers that are Infraestructure layer, Business layer and Data layer.
- **Business Layer:** in this layer there are all assemblies that refers specifically to the challenge, with it's domain and services. In this case these are the Users Domain and services and the RandomUsers http communication domain and services.
- **Infraestructure Layer:** here are all the common used models, utils, configurations. I added also a specific core for Entity Framework configuration and for a Logger (developed by myself).
- **Data Layer:** Database context and repositories are in this layer.
Each layer communicates with the others using interfaces.

**Ordering algorithm**
As the challenge requests, I developed an algorithm to get from among some users the oldest one. To achieve that, I remembered the heapsort sorting algorithm and the heap data structure wich is like a tree that keeps in the root node the element that is bigger or minor respect to a defined property.
So I defined the comparable property to be the birthDate of the users so that when each element is added to the heap it orders itself by comparing the fathers nodes with their sons and siblings nodes by the birthDate, keeping always in the root node the oldest user.
After adding all the users in the heap, I only have to get the first user of the array and asign him the property of "isSenior".

**Time spent**
Including several breaks, it took me 6 hours and 40 minutes approximately to develop the challenge
