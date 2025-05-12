CMMCore

The base for a command, command Handler, Services, Managers, Entities, Repository and Models.

All command Handler, Managers and Repository are registered in a dependency injection container.

Services :: 
When a command is passed to the Service it will add the word "Handler" to the command name then retrive an instance of the command handler from DI.

Managers :: 
Managers are used for returning Enities which should be Domain accurate.

Command Handler :: 
When the command is passed to the handler it is proccesed by the respective Managers which are injected in the constructor
