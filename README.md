# Back End Exam
> As an imperial programmer your receive the order of implement a new web service that allows to register any rebeld identification.

> The empire has conquered all universe, but it still remains some group of hidden rebelds. Dark forces, soldiers for the empire, distributed over all know galaxies and 
solar systems forces empire's citizens to be identified. They need you to develop a distributed service that would be able to be called from any location over the universe.

> As interoperability is a must, you decide to use an old fashioned and very extended technology called **wcf webservices/webapi**. The service/api has to expose *a method* 
that accept a **list of strings** with the name of the rebeld and name of the planet, and response **true** if register goes fine. The register has to be added to a **file** 
with a **datetime** with the string **_rebeld (name) on (planet) at (datetime)_**.


### Separate responsabilities in separated layers:

> 1. distributed services
> 2. application
> 3. domain
> 4. infrastructure

* Implement **error logging** and manage exceptions in every layer, you can fake it and pretend this is just ocurring.
* Implement **unit test** for any layer, too.
* Take care of **proper naming** convections and **SOLID principles**.
* You can use any **Dependency Injection, Unit Testing, Mocking frameworks** or any other that you consider necessary.


### Back End Exam Criteria
Exams take profit of coding kata technics to give the scenario and tools for evaluation. Concerns to check on exam are:

* Instrumentalization
* Exception management
* Error and Warning logging
* Configuration definition
* Software Design
* Algorithm design
* Practices and Patterns
* Naming
* SOLID principles
* Scalability
* Testeability
* Unit Testing
* Integration Testing


❗ ❗ ❗ ❗ ❗ ❗ ❗ ❗ ❗ ❗ ❗ ❗ ❗ ❗ ❗ ❗ ❗ ❗ ❗ ❗ ❗ ❗ ❗ ❗ ❗ ❗ ❗ ❗ ❗ ❗ ❗ ❗

> YOU HAVE **5 HOURS** TO DELIVER THE FOLLOWING SOLUTION **READY TO RUN** INTO A BRANCH


------

 ** README from Erik Locutura Pozo **

Tried putting all code in folder src/ and all tests in tests/ and adding this to the .sln but couldnt make it work so didn't commit that version

Sanitizing the input of name should be done so only alphabet characters are allowed and would return badRequest just like null or empty input name.


																												// Done in the Business Service

Path to file is saved in the appSettings created by C#'s webapi that is instantly loaded on building the webapi

Rebeld service in business manages logging and cache, and just maps Dtos to entities for the repository to work with.
DomainEntities are not necessary on this exam but is good to have them already there if we needed to process the data in some way in the business layer.

Repository saves entries as **{name},_rebeld (name) on (planet) at (datetime)_**
Repository gets the value of such entry and passes it back as a **RebeldSightingDto** for easier reading/mantenibility instead of a plain string


        // This is not optimal for performance, if we wanted optimal performance we would check names in the parsing from file as we are
        // reading the file to avoid creating the dictionary and avoid parsing all lines beyond the name we are searching for
        // I chose this implementation since this way methods are compact and easy to read
		
		
The optimized function is made right below to show implementation but its not used
		
I made unit tests for the repository searches that check errors returned and normal flow when finds a name
I dont remember of the top of my head how to mock the webhostbuilder so I didn't do integration testing


	// but if I could look up internet I'd mock the webServiceBuilder so I have a premade file with some data
	// then attack the endpoints and check if HttpGet returns all responseTypes established in the Controller
	// and if HttpPost returns all responseTypes established in the Controller
	
	
	This can already be tested by hand using the premade swagger from the WebApi
