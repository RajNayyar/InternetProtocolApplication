# InternetProtocolApplication
This is an api that returns locational details for an IpAdress

This api consists of a GET API to which an IpAddress is to be passed as URI parameters

URL: {base-url}/api/ip/location/country/{ipAddress}
Headers: None required

You'll have to run the application in order to process the BaseUrl.

----------------------------------

The Project consists of three layers.

1. Controller Layer: Where the request is being recieved.
 
NOTE: This API is executing three more APIs, each has their own extension, hence three extensions.

2. Service/Core Layer: Here a factory resolves the instance (for the extension) by using a strategy that this factory has. This strategy calculates the performance of multiple APIs based on the past TimeTakens that are stored in a JSON file. Everytime you hit the api, further in the end it calculates what each third party api/extension took time to execute.

3. Extension Layer: Each third party extension acts as an adapter to each third party sevice that has been integrated.
