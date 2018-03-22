# Validations
## On Properties
Each property has a list of validations associated with it. These validations can be chosen from an already provided types of validations that are implemented in CoreX (like Required, MaxLength, RegularExpression, Range, Compare...) or can be a custom implemented validation that is extending the framework.

A typical validation has the following attributes in design:
* Type: Required, MaxLength...
* ErrorMessage: a localizable error message that will appear in case of validation failed
* Other parameters: like MaxLength value, or property name to compare

## On Parts or Entities
A more logic related to entity or part can be implemented to validate a group of property values together. This can be done by implementing the default action **Validate**. The **Validate** action will be executed before **Create** or **Update** actions and is responsible to validate the object before being stored.
