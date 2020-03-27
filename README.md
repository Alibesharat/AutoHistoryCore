# AutoHistoryCore
AutoSaveChangeHistory

 ![Github tags](https://github.com/Alibesharat/AutoHistoryCore/workflows/.NET%20Core/badge.svg)

An Extention for Microsoft.EntityFrameworkCore to support automatically recording data changes history and some addentianal info such as ip,Os and broswer agent

also, this extension support soft-delete pattern

AutoHistoryCore will recording all the data changing history in each record 

How To Install:

Run the following command in the Package Manager Console 

    PM> Install-Package AutoHistoryCore 

How To Use :

It is easy  to use just following 3 steps :

1: drive your Model from HistoryBaseModel 

    public class MyModel:HistoryBaseModel
    {
      // your property ...
      
    }

 2.Add Migration to affect Database Change
 
      Add-Migration add_changeHistoryCore
      update-database
      

 3: use SaveChageWithHistory Extention insted of SaveChages() Defualt method:
 
    db.SaveChangesWithHistory(httpcContext)
    
    
 Note : drive your model from HistoryBaseModel will be extend that by add two property : hc_change as String,  and Isdeleted as bool,
 
 here the result of hc_change column as json in one record :
 
  ![result](https://github.com/Alibesharat/AutoHistoryCore/blob/master/result.PNG)
 
 Notes :
 
1. SaveChageWithHistory provide softdelete pattern  automatically by change isdelete property as true when you call    db.remove(you model inherited from HistoryBaseModel) before SaveChageWithHistory(httpcContext)
 
2. If you wanna physical delete or doesn't  enable history tracking you must call  default savechange method 

3.if you wanna get undelited recored use this following linq Extention:

 
    db.yourModel.undelited().where(...your statement).tolist() .
    
    
  Powered By:
  
   User Agent Parser for .Net  - Get  User Agent Info By  (https://github.com/ua-parser/uap-csharp )
     
  
    

 
