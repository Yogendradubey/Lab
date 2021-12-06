******************************************************************************
*	Created By : Dinesh Kumar D                                              *
*	Contact    : dinesh-kumar.d@atos.net                                     *
******************************************************************************
# HCA Lab Test
	This is a Web API application for lab test data handing and reporting developed using .Net Core 5.0

# Problem statement
	Need application that is capable of
	1. Generate authentication token for security access
	2. Creating/Managing Patient securely
	3. Creating/Managing/Reporting Test Reports securely

# Tables (As model classes for In-Memory DB implementation)
	UserCredential
		int Id// PrimaryKey
		string Username //Logged in user name
		string Password //User password
		
	Patient
        	int Id //PrimarKey
		string Name //Name of the patient
		DateTime DOB//Date of birth of patient
		string Gender//Gender of patient
		dateTime CreateOn//patient created on date time

			
	Report
		int Id //PrimarKey
		int PatientId //ForeignKey Patient.Id
		string Type//Test types
		string Result//Test result
		DateTime SampleCollectionDateTime//Sample received on
		dateTime CreatedOn//Report cretaed on date time

	PatientRequest
		string Name //Name of the patient
		DateTime DOB//Date of birth of patient
		GenderType Gender//Gender type as Enum(Male = 0, Female = 1)

	ReportRequest
		int PatientId //ForeignKey Patient.Id
		TestType Type//Test type as Enum(GlucoseTests = 0, CompleteBloodCount = 1,LipidPanel = 2, Urinalysis = 3)
		ResultType Result//Result type as Enum(Negative = 0, Possitive= 1)
		DateTime SampleCollectionDateTime//Sample received on

	UserRequest
		string Username //Logged in user name
		string Password //User password

	ReportFilterRequest
		TestType Type//Test type as Enum(GlucoseTests = 0, CompleteBloodCount = 1,LipidPanel = 2, Urinalysis = 3)
        	DateTime CreatedOnStart//From date time
        	DateTime CreatedOnEnd//To date time

			
# Approach
	Implemention using In-Memory DB
	User credentials in UserCredntial class
	Patient details in Patient class
	Report details in LabReport class
	PatientRequest class is to get the patient details from user
	ReportRequest class is to get the report details from user
	UserRequest class is to get the user details from user
	ReportFilterRequest class is to get the report filter details from user

#Operations Supported with endpoints
	Operations supported with endpoint details, sample URL and payload information 
	
	1. Endpoint User
		* Create: (Post : https://localhost:44385/api/User/CreateUser)
			{
				"username": "Demouser",
				"password": "DemoPassword"
			}	
		* Login: (Post : https://localhost:44385/api/User/Login)
			{
				"username": "Demouser",
				"password": "DemoPassword"
			}				
	2. Endpoint Patient
		* Create : (Post : https://localhost:44367/Patient/Create)
			{
  				"name": "string",
  				"dob": "2021-12-06T13:38:20.497Z",
  				"gender": 0
			}
		* Update : (Put : https://localhost:44385/api/Patient/1)
			{
  				"name": "string",
  				"dob": "2021-12-06T13:38:58.572Z",
  				"gender": 0
			}
			
		* Delete : (Delete : https://localhost:44385/api/Patient/1)
		* GetAll : (Get : https://localhost:44385/api/Patient)
		* GetById : (Get : https://localhost:44385/api/Patient/1)
		* GetByReportType: (Get : https://localhost:44385/api/Patient/GetByReportType/1/2021-01-01/2021-12-31)
		
		
	3. Endpoint LabReport
		* Create : (Post : https://localhost:44385/api/Report)
			{				
  				"type": 0,
  				"result": 0,
  				"sampleCollectionDateTime": "2021-12-06T13:46:03.241Z",
  				"patientId": 0
			}
		* Update : (Put : https://localhost:44385/api/Report/1)
			{
  				"type": 0,
  				"result": 0,
  				"sampleCollectionDateTime": "2021-12-06T13:46:58.593Z",
  				"patientId": 0
			}
		* Delete : (Delete : https://localhost:44385/api/Report/1)
		* GetAll : (Get : https://localhost:44385/api/Report)
		* GetById : (Get : https://localhost:44385/api/Report/1)
	
		
#Installation
	1. Copy code in a folder
	2. Open LabTest soultion using Microsoft Visual Studio (LaboratoryAPI.sln)
	4. Application should run in browser using Swagger UI
	5. Postman can also be configured (as per above url and payload details) for generating and passing token
	
#Steps to run with Swagger
	1. Create new user using CreateUser endpoint
	2. Once new user is created Login (use the credentials which is used to create a user) to generate token
	3. Once token is generated, copy the generated token
	4. Click Authorize button in page header to open "Available Authorizations" dialogue
	5. Enter 'Bearer' [space] and then token in the text input under value
	6. click Authorize and then Close button
	7. Now you are ready to run, follow sequence as below to handle data dependencies 

#Steps to run with Postman
	1. Configure Postman requests as per information above
	2. Create new user using CreateUser endpoint
	3. Once new user is created Login (use the credentials which is used to create a user) to generate token
	4. Once token is generated, copy the generated token to pass with subsequet requests
	5. Follow sequence as below to handle data dependencies 
