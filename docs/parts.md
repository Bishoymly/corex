Entities can be much complex, it may have lots of properties that are common among multiple types of entities. It may have complex behaviors and logic that needs to be implemented in multiple entity types.
So comes the Parts.

# Parts
Parts are the building blocks of entities. A part contains:
* Some Properties
* Some Actions with implementation

Parts can inherit from each other. A part that inherit from another part, contains the properties and actions of the parent + his own properties and actions. It can override action implementations of the parent part.

When you write action implementation for a part, in your implementation you have access to the properties in this part or parent parts. But when you write implementation to an entity, you have access to all of its properties and actions.

An entity is composed of multiple parts, each part add to this entity his properties and actions. A part may be used multiple times in the same entity with different names, which gives different prefixes for its properties.

### Examples:
#### CMS Example:
A CMS **ContentItem** Part may contain the following:
* Properties: Id, Title, Url, Status, DateCreated, Author
* Actions: Draft, Publish

Another part **ContentWithApproval** can inherit from **ContentItem** and add the following:
* Properties: ApprovalStatus
* Actions: RequestApproval, Approve, Reject

A completely different part **Gallery** may contain:
* Properties: Images (as a list of images)
* Actions: ViewGallery, AddImage, DeleteImage

A **NewsItem** Entity may contain the following:
* Parts: ContentWithApproval, Gallery
* Properties: Header, Body + all the above properties
* Actions: all the above actions

#### Commerce Example:
A commerce website may use a part named **Address** with:
* Properties: Building, Street, City, Country, ZipCode
* Actions: Validate (which implements a validation for ZipCode within the City and Country)

Then an entity for **UserProfile** may use the **Address** part twice for billing and delivery addresses:
* Parts: Address as BillingAddress, Address as DeliveryAddress
* Actions: Validate (now this action is automatically in this entity which will call Validate twice for the 2 Address parts)
* Properties (auto generated): BillingAddressBuilding, BillingAddressStreet, BillingAddressCity, BillingAddressCountry, BillingAddressZipCode, DeliveryAddressBuilding, DeliveryAddressStreet, DeliveryAddressCity, DeliveryAddressCountry, DeliveryAddressZipCode

Note that when implementing the Validate action within the Address part, the developer will access properties with their names like: City or ZipCode. While when implementing actions within the entity level, the developer will access properties with their full name like: BillingAddressCity or BillingAddressZipCode. And both ways will be accessing the same properties.

Continue reading [Repositories](repositories.md)..
