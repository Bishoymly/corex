# Repositories (Data Storage)
After designing your entities, you need to configure each entity to use specific repositories for storage.
A repository is responsible for saving and loading of an entity (all properties of an entity) from a data store. 
A repository is not responsible for applying validation or logic to entities (this should be in actions).

Here are the types of repositories:
* SQL Database
* NoSQL like MongoDB or Azure DocumentDB
* Files like Azure blobs or Azure Tables
* Cache like in Memory or RedisCache

An entity can have multiple configurations of repositories and you can change it as the entity grows. For example:
* Check in RedisCache
* Then Get from SQL Database

The entity properties/actions/parts and any logic inside them are not concerned with the way an entity is stored. And all entity APIs will have the same interface no matter which repository is used.

When you create a new entity, a default repository will be assigned to this entity, and it will automatically map the properties with their current name to the fields in database. 
