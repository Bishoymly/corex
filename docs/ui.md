# UI
## For Properties
Each property has a list of attributes to define how will it appear in a UI form. Like:
* Label: a friendly label to appear instead of the property name
* Hint: an optional tool tip to provide more help
* Visible: by default true, can be set to false to hide this property from UI
* ReadOnly: true if this control is not available for edit
* UIControl: a UI control to be used to display this property

The UIControl is dependant on the property type, for example:
* Text types allow: TextBox or RichEditor
* Number types allow: TextBox or Slider

Each property type will have a default UIControl in case this is left empty in the property designer.

## For Entity or Parts
Here the designer has the option to provide his own HTML view/control that render this entity/part. For example an image gallery part that contains a list of image paths may be rendered using multiple views/plugins, in this case the part UIControl is set to the view that will be used to render this component, in this case this view will override the default rendering of properties designed above.

## Views
Each entity may have more than one view. The above options are always available when designing entities to provide default values if views are not designed. But let's say we want to design the Create form different than the Edit form for the same entity. In this case we have 2 Views:
* Create: which uses default values provided by entity designer but override some of them (for example hides some properties during Create)
* Edit: again override different variations of the default values (like disable the Title property only in Edit)

Note that in different views, properties can be ordered differently, can be displayed using different UIControl

Some more examples of views are:
* List: this default view defines which properties appear on listing page, in which order, and using which controls
* Filter: which properties are available in search/filters

If an action like Edit is looking for a view named Edit and it wasn't available it will fall back to display the default view.

## UIControls (HTML Templates)
A developer may choose to create new HTML templates to render anything instead of the default rendering.
A template may override the rendering of the following:
* Property: in this case it will always bind the single value of the property and usually is restricted to a certain type
* Part: in this case it render html binding properties within this Part
* Entity: in this case it will override the entity rendering and it has access to bind on any property within this entity
* View of Part/Entity: in this case it will override the rendering of a specific View (for example Edit view of Address part)
