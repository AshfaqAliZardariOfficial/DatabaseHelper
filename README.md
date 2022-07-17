# DatabaseHelper
 A C# database helper library to connect with database server and perform actions insert, update, delete, select data and select multiple data from database.

## [Install Package](https://www.nuget.org/packages/AshfaqAliZardariOfficial.Util.DatabaseHelper/)
> ##### Using Package Manager
> ```
> Install-Package AshfaqAliZardariOfficial.Util.DatabaseHelper -Version 1.0.0
> ```
> ##### Using .NET CLI
> 
> ```
> dotnet add package AshfaqAliZardariOfficial.Util.DatabaseHelper --version 1.0.0
> ```
> ##### Using PackageReference
> 
> ```
> <PackageReference Include="AshfaqAliZardariOfficial.Util.DatabaseHelper" Version="1.0.0" />
> ```
> ##### Using Paket CLI
> ```
> paket add AshfaqAliZardariOfficial.Util.DatabaseHelper --version 1.0.0
> ```
> ##### Using Script & Interactive
> ```
> #r "nuget: AshfaqAliZardariOfficial.Util.DatabaseHelper, 1.0.0"
> ```
> ##### Using Cake
> ```
> // Install AshfaqAliZardariOfficial.Util.DatabaseHelper as a Cake Addin
> #addin nuget:?package=AshfaqAliZardariOfficial.Util.DatabaseHelper&version=1.0.0
> 
> // Install AshfaqAliZardariOfficial.Util.DatabaseHelper as a Cake Tool
> #tool nuget:?package=AshfaqAliZardariOfficial.Util.DatabaseHelper&version=1.0.0
> ```

## How do I use
##### Connect with MS Sql Database Server.
```csharp
// Add Namespace
using DatabaseHelper;

// MS Sql Database Server connection string.
string MsSqlCon = "Server=.; Database=MyDatabase;User ID=sa;Password=1234;";

// Init database server connection.
Connect DBHelper = new Connect(server: Connect.DB_SERVERS.SQL_SERVER, connectionString: MsSqlCon);
```

##### Connect with MySql Database Server.
```csharp
// Add Namespace
using DatabaseHelper;

// MySql Database Server connection string.
string MySqlCon = @"Server=localhost; Database=MyDatabase;User ID=root;Password=;";

// Init database server connection.
Connect DBHelper = new Connect(server: Connect.DB_SERVERS.MYSQL_SERVER, connectionString: MySqlCon);
```

##### Insert data.
```csharp
// Insert query.
string query = "insert into users(name, email) values(@name, @email)";

// Query parameters.
IDictionary<string, object> parameters = new Dictionary<string, object>(); // Your dictionary object.
parameters.Add("name", "Ashfaq Ali Zardari"); // Your dictionary key value.
parameters.Add("email", "ashfaqalizardariofficial@gmail.com"); // Your dictionary key value.

// return true, if data inserted. Otherwise return false.
bool IsDataInserted = DBHelper.InsertOrUpdateOrDeleteData(query, parameters);
```

##### Update data.
```csharp
// Update query.
string query = "update users set name = coalesce(@name, users.name), email = coalesce(@email, users.email) where id = @id";

// Query parameters.
IDictionary<string, object> parameters = new Dictionary<string, object>(); // Your dictionary object.
parameters.Add("name", "Ashfaq Ali Zardari Official"); // Your dictionary key value.
parameters.Add("email", "ashfaqalizardariofficial@outlook.com"); // Your dictionary key value.
parameters.Add("id", 1); // Your dictionary key value.

// return true, if data updated. Otherwise return false.
bool IsDataUpdated = DBHelper.InsertOrUpdateOrDeleteData(query, parameters);
```

##### Delete data.
```csharp
// Delete query.
string query = "delete from users where id = @id";

// Query parameters.
IDictionary<string, object> parameters = new Dictionary<string, object>(); // Your dictionary object.
parameters.Add("id", 1); // Your dictionary key value.

// return true, if data deleted. Otherwise return false.
bool IsDataDeleted = DBHelper.InsertOrUpdateOrDeleteData(query, parameters);
```

##### Select data.
```csharp
// Select query.
string query = "select * from users";

// return DataTable, if data selected. Otherwise return null.
DataTable UsersTable = DBHelper.GetData(query, null);
```

Optional
```csharp
// Select query with parameters.
string query = "select * from users where id = @id";

// Query parameters.
IDictionary<string, object> parameters = new Dictionary<string, object>(); // Your dictionary object.
parameters.Add("id", 1); // Your dictionary key value.

// return DataTable, if data selected. Otherwise return null.
DataTable UsersTable = DBHelper.GetData(query, parameters);
```

##### Select multiple data (Two or more tables data).
```csharp
// Select multiple data query.
string query = "select * from users; select * from roles;";

// return DataSet, if data selected. Otherwise return null.
DataSet TablesDataSet = DBHelper.GetMultipleData(query, null);

// Users DataTable.
DataTable UsersTable = TablesDataSet != null && TablesDataSet.Tables[0] != null ? TablesDataSet.Tables[0] : null;

// Roles DataTable.
DataTable RolesTable = TablesDataSet != null && TablesDataSet.Tables[1] != null ? TablesDataSet.Tables[1] : null;
```

Optional
```csharp
// Select multiple data with parameters query.
string query = "select * from users id = @userid; select * from roles where role = @role;";

// Queries parameters.
IDictionary<string, object> parameters = new Dictionary<string, object>(); // Your dictionary object.
parameters.Add("userid", 1); // Your dictionary key value.
parameters.Add("role", "Admin"); // Your dictionary key value.

// return DataSet, if data selected. Otherwise return null.
DataSet TablesDataSet = DBHelper.GetMultipleData(query, parameters);

// Users DataTable.
DataTable UsersTable = TablesDataSet != null && TablesDataSet.Tables[0] != null ? TablesDataSet.Tables[0] : null;

// Roles DataTable.
DataTable RolesTable = TablesDataSet != null && TablesDataSet.Tables[1] != null ? TablesDataSet.Tables[1] : null;
```

## :clock3: Versions
| Version | Last updated |
| --- | --- |
| [1.0.0](https://www.nuget.org/packages/AshfaqAliZardariOfficial.Util.DatabaseHelper/1.0.0) | Nov 23, 2021, 5:35 PM GMT+5 |

## :book: Release Notes
**v1.0.0**
- Connect with MS Sql Database Server.
- Connect with MySql Database Server.
- Insert data.
- Update data.
- Delete data.
- Select data.
- Select Multiple data.

## Contact and Supporting Info
Feel free to contact me on <a href="mailto:ashfaqalizardariofficial@gmail.com" target="_blank" title="ashfaqalizardariofficial@gmail.com"><img src="https://ssl.gstatic.com/ui/v1/icons/mail/rfr/logo_gmail_lockup_default_1x_r2.png" alt="ashfaqalizardariofficial@gmail.com" width="70" /></a>  
  
  <a href="https://paypal.me/ashfaqalizardari247?country.x=CA&locale.x=en_US" target="_blank" title="paypal.me/ashfaqalizardari247"><img src="https://www.paypalobjects.com/paypal-ui/logos/svg/paypal-color.svg" alt="PayPalMe" width="160" /></a>    <a href="https://www.buymeacoffee.com/ashfaqalizardari" target="_blank" title="buymeacoffee.com/ashfaqalizardari"><img src="https://www.buymeacoffee.com/assets/img/custom_images/orange_img.png" alt="Buy Me A Coffee" width="160" /></a>

## :balance_scale: License
  Copyright (c) Ashfaq Ali Zardari. All rights reserved.
  
  Licensed under the [MIT](https://github.com/AshfaqAliZardariOfficial/DatabaseHelper/blob/master/LICENSE) License.
