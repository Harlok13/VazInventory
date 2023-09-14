# VAZ Inventory Excel Parser

A program for parsing an Excel file of a given structure and committing data to the database,
for further work with the data.

The structure of the Excel file is strictly defined:

![ExcelStructure.png](images%2FExcelStructure.png)

How to use the program:

1. You need to clone the repository to your local machine:
    ```shell
    git clone https://github.com/Harlok13/VazInventory.git
    ```
   
2. Create an App.config file in the project root using the example App.config.example, which is located
   at the root of the project. \
   P.S. in future versions of the program App.config will be replaced by settings.json

3. Set the line to `add key="PathToXlsxFiles" value="/Path/To/Xlsx/Files"`
   value, which will contain the path to the directory with Excel files.

    ![PathToDirectoryExample.png](images%2FPathToDirectoryExample.png)

    In this case, the path will look like: \
    `/Users/Harlok/Desktop/Inventory`

    If your OS is Windows, then the path must contain backslashes.

    The program will only read xlsx files. It is important to ensure that the files match
    given structure. \
    In the next version the program will check the structure automatically and warn
    user if the file does not match it.

4. Set the database connection string in the App.config file as specified in App.config.example

5. Open a terminal and make sure that you are in the same directory as the program's entry point.
   For example, in my case it would be `/Users/Harlok/RiderProjects/Cartography/Inventory`.
   You must have .NET SDK 7.0 installed
   After this you need to run the command:
```shell
dotnet run
```

![RunProgram.png](images%2FRunProgram.png)

The program will compile and run. Tables will be created in the database and data from Excel will be parsed
into tables.