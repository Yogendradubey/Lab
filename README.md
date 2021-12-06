# Lab

# API Development
•	ASP.NET Core 6.0 as base platform <br/>
•	EF Core 6.0 as primary ORM <br/>
•	JWT tokaen Authentication and authorization implemented <br/>
•	Swashbuckle.AspNetCore.SwaggerGen for Swagger docs and UI <br/>
•	AutoMapper.Extensions.Microsoft.DependencyInjection 8.1.1 for object to object mapping <br/>
•	Repository design Pattern <br/>
•	Used in memory Database using EF core <br/> 



## Overview


******************************************************************************
*	Created By : Shrikrishna Chopade                                           *
*	Contact    : shrikrishna.chopade@atos.net                                     *
******************************************************************************
# HCA Lab Test
	This is a Web API application for lab test data handing and reporting developed using .Net Core 6.0

# Problem statement
	Need application that is capable of
	1. Generate authentication token for security access
	2. Creating/Managing Patient securely
	3. Creating/Managing/Reporting Test Reports securely

# Tables (As model classes for In-Memory DB implementation)
	Login
		string UserName //Logged in User Name
		string Password //User password
		
	Patient
                int PId //PrimarKey
		string Name //Patient Name
		DateTime DOB//Date of birth of patient
                Enum Gender //Gender of patient ( 1 - Male, 2 - Female, 3 - Other)
		string Email//Patient email id
		string Mobile//Patient contact number
		string Address //Patient address
	
	LabReport
		int LRId //PrimarKey
		Enum Type //Type of test (1- Glocose, 2 - BloodCount)
		string Result //Result of the test
		DateTime TestTime//TestTime
		DateTime EnteredTime//EnteredTime
	
		
# Approach
	Implemention using In-Memory DB
	User credentials in Login model class
	Patient details in Patient class	
	Report details in LabReport class
	
	
#Operations Supported with endpoints
	Operations supported with endpoint details, sample URL and payload information 
	
           1. Endpoint Register Admin User
		* Login : (Post : https://localhost:7120/Register)
			{
                            "userId": 0,
                            "userName": "Admin",
                            "password": "Admin",
                            "email": "admin@labtest.com",
                            "role": "Admin"
                  }			
	2. Endpoint Login
		* Login : (Post : https://localhost:7120/Login)
			{
				"username": "Admin",
				"password": "Admin"
			}			
	3. Endpoint Patient
		* Create : (Post : https://localhost:7120/Patients)
			{
				"pid": 0,
				"Name": "Test Patient 1",
				"dateOfBirth": "1980-08-09T00:00:00",
				"gender": 1,
				"email": "testpatient1@gmail.com",
				"mobile": "(+91) 98235xxxxx",
				"address": "Pune, Maharashtra, India - 411018"
			}
		* Update : (Put : https://localhost:44367/Patients/1)
			{
				"pid": 1,
				"Name": "Test Patient 1",
				"dateOfBirth": "1980-08-09T00:00:00",
				"gender": 1,
				"email": "testpatient1@gmail.com",
				"mobile": "(+91) 98235xxxxx",
				"address": "Pune, Maharashtra, India - 411018"
			}
		* Delete : (Delete : https://localhost:7120/Patients/1)
		* GetAll : (Get : https://localhost:7120/Patients)
		* GetById : (Get : https://localhost:7120/Patients/1)
		
	
		
	4. Endpoint LabReport
		* Create : (Post : https://localhost:44367/LabReports)
			{
                              "lrId": 0,
                              "type": "Glocose",
                              "result": "test result",
                              "testTime": "2021-12-06T13:50:34.908Z",
                               "enteredTime": "2021-12-06T13:50:34.908Z",
                             "pId": 1
                      }
		* Update : (Put : https://localhost:7120/LabReports/1)
			{
				 "lrId": 1,
                              "type": "Glocose",
                              "result": "test result",
                              "testTime": "2021-12-06T13:50:34.908Z",
                               "enteredTime": "2021-12-06T13:50:34.908Z",
                             "pId": 1
			}
		* Delete : (Delete : https://localhost:7120/LabReports/1)
		* GetAll : (Get : https://localhost:7120/LabReports)
		* GetById : (Get : https://localhost:7120/LabReports/1)
		* GetByLabTest : (Get : https://localhost:7120/LabReports/1/2021-01-01/2021-12-31)
		
**#Installation**  <br/> 
	1. Copy code in a folder  <br/> 
	2. Open LabTest soultion using Microsoft Visual Studio (Lab.sln)  <br/> 
	3. Build and Run project LabDemo.Api  <br/> 
	4. Application should run in browser using Swagger UI  <br/> 
	5. Postman can also be configured (as per above url and payload details) for generating and passing token  <br/> 
	
**#Steps to run with Swagger**
        1. Execute endpoint Register user using postman with user details
	2. Execute endpoint Login (Credentials as in Login endpoint details above) to generate token
	3. Once token is generated, copy the generated token
	4. Click Authorize button in page header to open "Available Authorizations" dialogue
	5. Enter 'Bearer' [space] and then token in the text input under value
	6. click Authorize and then Close button
	7. Now you are ready to run, follow sequence as below to handle data dependencies 
	8. Create Patient (if executed Get(), Will get Patient table data)	
	9. Create LabReport (if executed Get(), Will get LabReport table data)

**#Steps to run with Postman** 
	1. Configure Postman requests as per information above
	2. Execute Login (Credentials as above) to generate token
	3. Once token is generated, copy the generated token to pass with subsequet requests
	4. Follow sequence as below to handle data dependencies 
	5. Create Patient (if executed Get(), Will get Patient table data)	
	6. Create LabReport (if executed Get(), Will get LabReport table data)

