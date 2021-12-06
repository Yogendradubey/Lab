# Lab

# API Development
The API has been developed using
•	ASP.NET Core 3.1.0 as base platform; <br/>
•	EF Core 5.0.6 as primary ORM;<br/>
•	ASP.NET Core Identity 3.1.0 for authentication and authorization;<br/>
•	JWTBearer  3.1.11 for OAuth authorization;<br/>
•	Swashbuckle.AspNetCore.SwaggerGen for Swagger docs and UI;<br/>
•	SQL Server 2012 <br/>
•	AutoMapper 10.1.1 for object to object mapping <br/>
•	Aspnetcore.Mvc.versioning 5.0 for API Versioning <br/>
•	Repository design Pattern. <br/>


## Overview

To use this API, please clone the code from repository, build it and run on local. 

The JWT basic authentication has been implemented to and needs to generate token with valid credential and pass this token as bearer token, to use any API endpoints. The API auth endpoint can be used to generate token by passing credential details as Basic-Auth or using authorization header.



