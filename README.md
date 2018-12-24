# AutoHistoryCore
AutoSaveChangeHistory


An Extention for Microsoft.EntityFrameworkCore to support automatically recording data changes history and some addentianal info such as ip,Os,and broswer agent

also, this extension support soft-delete pattern

AutoHistoryCore will recording all the data changing history in each record 

How To Install:

Run the following command in the Package Manager Console 

    PM> Install-Package AutoHistoryCore 

How To Use :
It is easy  to use jsust following 3 steps :

1: drive your Model from HistoryBaseModel 

    public class MyModel:HistoryBaseModel
    {
      // your property ...
      
    }

 2.Add Migration to affect Database Change
 
      Add-Migration add_changeHistoryCore
      update-database
      

 3: use SaveChageWithHistory Extention insted of SaveChages() Defualt methode:
 
    db.SaveChangesWithHistory()
    
    
 Note : drive your model from HistoryBaseModel will be extend that by add two property : hc_change as String,  and Isdeleted as bool,
 
 here the result of ht_change column as json in one record :
 
  ![result](https://github.com/Alibesharat/AutoHistoryCore/blob/master/result.PNG)
 
 Note : SaveChageWithHistory provide softdelete pattern  automatically by change isdelete property as true when you call    db.remove(you model inherited from HistoryBaseModel) before SaveChageWithHistory()
 
 Note : If you wanna physical delete or doesn't  enable history tracking you must call  default savechange method 
