# CMMCore

The base for a **Command**, **CommandHandler**, **Services**, **Manager**, **Entity**, **Repository** and **Model**.<br>

All **CommandHandler**'s, **Manager**'s and **Repository** are registered in a dependency injection container.

**Services**<br> 
When a command is passed to the **Service** it will add the word "Handler" to the command name then retrive an instance of the **CommandHandler** from DI.<br>

**Managers**<br>
**Manager**'s are used for returning **Enities** which should be Domain accurate.

**Command Handler**<br> 
When the **Command** is passed to the handler it is proccesed by the respective **Manager**'s which are injected in the constructor.
