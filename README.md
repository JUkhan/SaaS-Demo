# SaaS-Demo

Dot Net SaaS Demo

`If parameter included into the request (key) is indicating that the user is paying user (as in not a free trial user), then it should get created independent database for usage of the SaaS offering. If free (non paying) user – then it should get records created in a shared database`

`For user management, information on registration there should be a separate database. Design solution with having in mind that user data will have to be migrated from private container to the shared and potentially backwards (from shared to private) as well.`
