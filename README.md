# AutoHistoryCore
AutoSaveChangeHistory


An Extention for Microsoft.EntityFrameworkCore to support automatically recording data changes history.


AutoHistoryCore will recording all the data changing history in Your tables

you must drive your models from HistoryBaseModel

HistoryBasemodel Will Add some property to track Model Changes

How To Install:

Run the following command in the Package Manager Console to install Microsoft.EntityFrameworkCore.AutoHistory

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
 
    bloggingContext.SaveChageWithHistory()
