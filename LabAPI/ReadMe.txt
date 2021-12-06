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
	3. Creating/Managing Test securely
	4. Creating/Managing/Reporting Test Reports securely

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
	
	1. Endpoint Login
		* Login : (Post : https://localhost:44385/Login)
			{
				"username": "Demouser",
				"password": "DemoPassword"
			}			
	2. Endpoint Patient
		* Create : (Post : https://localhost:44367/Patient/Create)
			{
				"id": 0,
				"patientName": "Test Patient 1",
				"dateOfBirth": "1980-05-25T00:00:00",
				"patientGender": 1,
				"emailId": "testpatient1@gmail.com",
				"contactNumber": "(+91) 98235xxxxx",
				"address": "Pune, Maharashtra, India - 411018"
			}
		* Update : (Put : https://localhost:44367/Patient/Update/1)
			{
				"id": 1,
				"patientName": "Test Patient 1 Modified",
				"dateOfBirth": "1980-05-25T00:00:00",
				"patientGender": 1,
				"emailId": "testpatient1@gmail.com",
				"contactNumber": "(+91) 98235xxxxx",
				"address": "Pune, Maharashtra, India - 411018"
			}
		* Delete : (Delete : https://localhost:44367/Patient/Delete/1)
		* Restore : (Put : https://localhost:44367/Patient/Restore/1)
		* GetAll : (Get : https://localhost:44367/Patient/Get)
		* GetById : (Get : https://localhost:44367/Patient/Get/1)
		
	3. Endpoint LabTest
		* Create : (Post : https://localhost:44367/LabTest/Create)
			{
				"id": 0,
				"testType": 1,
				"description": "Blood Count",
				"sampleType": 1,
				"minimumRequiredQty": 50,
				"minLimit": 100,
				"maxLimit": 1000
			}		
		* Update : (Put : https://localhost:44367/LabTest/Update/1)
			{
				"id": 1,
				"testType": 1,
				"description": "Blood Count Modified",
				"sampleType": 1,
				"minimumRequiredQty": 500,
				"minLimit": 500,
				"maxLimit": 5000
			}		
		* Delete : (Delete : https://localhost:44367/LabTest/Delete/1)
		* Restore : (Put : https://localhost:44367/LabTest/Restore/1)
		* GetAll : (Get : https://localhost:44367/LabTest/Get)
		* GetById : (Get : https://localhost:44367/LabTest/Get/1)
		
	4. Endpoint LabReport
		* Create : (Post : https://localhost:44367/LabReport/Create)
			{
				"id": 0,
				"patientId": 1,
				"labTestId": 1,
				"sampleReceivedOn": "2021-01-10T00:00:00",
				"sampleTestedOn": "2021-01-11T00:00:00",
				"reportCreatedOn": "2021-01-12T00:00:00",
				"testResult": 125,
				"refferredBy": "Dr. Physician 1"
			}
		* Update : (Put : https://localhost:44367/LabReport/Update/1)
			{
				"id": 1,
				"patientId": 1,
				"labTestId": 1,
				"sampleReceivedOn": "2021-01-10T00:00:00",
				"sampleTestedOn": "2021-01-11T00:00:00",
				"reportCreatedOn": "2021-01-12T00:00:00",
				"testResult": 125,
				"refferredBy": "Dr. Physician 1 Modified"
			}
		* Delete : (Delete : https://localhost:44367/LabReport/Delete/1)
		* Restore : (Put : https://localhost:44367/LabReport/Restore/1)
		* GetAll : (Get : https://localhost:44367/LabReport/Get)
		* GetById : (Get : https://localhost:44367/LabReport/Get/1)
		* GetByLabTest : (Get : https://localhost:44367/LabReport/GetByLabTest/1/2021-01-01/2021-12-31)
		
#Installation
	1. Copy code in a folder
	2. Open LabTest soultion using Microsoft Visual Studio (LabTests.sln)
	3. Build and Run project HCA.API.LabTests
	4. Application should run in browser using Swagger UI
	5. Postman can also be configured (as per above url and payload details) for generating and passing token
	
#Steps to run with Swagger
	1. Execute endpoint Login (Credentials as in Login endpoint details above) to generate token
	2. Once token is generated, copy the generated token
	3. Click Authorize button in page header to open "Available Authorizations" dialogue
	4. Enter 'Bearer' [space] and then token in the text input under value
	5. click Authorize and then Close button
	6. Now you are ready to run, follow sequence as below to handle data dependencies 
	7. Create Patient (if executed Get(), will create hardcoded Patients from backed if Patient table is empty)
	8. Create LabTest (if executed Get(), will create hardcoded Tests from backed if LabTest table is empty)
	9. Create LabReport (if executed Get(), will create hardcoded LabReports from if when LabReport table is empty)

#Steps to run with Postman
	1. Configure Postman requests as per information above
	2. Execute Login (Credentials as above) to generate token
	3. Once token is generated, copy the generated token to pass with subsequet requests
	4. Follow sequence as below to handle data dependencies 
	5. Create Patient (if executed Get(), will create hardcoded Patients from backed if Patient table is empty)
	6. Create LabTest (if executed Get(), will create hardcoded Tests from backed if LabTest table is empty)
	7. Create LabReport (if executed Get(), will create hardcoded LabReports from backed if LabReport table is empty)
