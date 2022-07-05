# Testcase

1) first of all we should go to solution file and run the terminal 'docker-compose up -d --build' command 

2) weh have 3 services in container
UserServices -->http://localhost:8001/swagger/index.html
CSVServices -->http://localhost:8001/swagger/index.html
AppoinmentServices -->http://localhost:8003/swagger/index.html

3) You should register with using UserAPI at Start of the using app .And after that you have to login get the token access key. it gives you access key fot the other microservices.
if you need user id , userAPI have the nameand id search metods.

4) Appoinment api create appoinment with user id .
if user does not exist in user API ,you can not create apoinment.

5)CSV Services do basically read csv file in API (\Testcase.CSV.API\CSVFiles\) and save data in mongo db. yo need token key using this api.
The data saving condition is that the appoinment api and csv date columns match. 
