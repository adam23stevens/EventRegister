Assumptions made:

- The database always stores UK based times for the events. This would require a conversion on the entered dates for an event
each time they were created and saved.
- Every time a person logs in to register to an event, it's assumed they would be passing their localised times to the server.
This means that the GET requests for the registration would need to conver the UK stored time for the event to the local
time on the client.
- It's assumed the static class TimeZoneInfo alone would suffice to handle the conversions of times and dates

If I had more Time (in order of importance to my mind)
- I haven't had time to handle localised date types. ie - for a US person they input MM/DD/YY whereas Uk for example do DD/MM/YYYY
- I wanted to show custom filter attributes, particularly to handling a request for events not existing. You can 
see in my comments in my Repo class that i've had to put that validation there instead. 
- I would have liked to revise my exception handling. I don't think it's quite perfect. 
- I admit a slight error with the RegistrationRequest object. They have a single string for Error but it should be a list of string
or even better, an error class on its own


Generally, I've opted to showcase my knowledge of overall structure of a project, with particular attention to DI handling 
and Unit tests. I have remained loyal to SOLID principles and done my best to show decent OOP practices.

Eagerly awaiting feedback.

Many thanks,
Adam
