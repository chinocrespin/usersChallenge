# Users Challenge

**How to execute the application**
1. First you should open the project solution in Visual Studio 2017 or Visual Code and build the assemblies to ensure to generate all the dlls correctly.
2. Run the UsersChallenge project. It could ask you to trust in the SSL certificate first.
3. With the api deployed you should be able to send requests to the api's port following this schema: 
[SERVER]:[PORT]/api/[CONTROLLER]
(server should be localhost to test it locally)
4. To test the endpoints I suggest to use Postman (I was going to add Swagger but did not developed it time to do that)

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
