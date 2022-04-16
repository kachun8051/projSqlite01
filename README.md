# Encrypted before saving and Decrypted before display 

## (A) Encryption method:
<font size="3">Triple Data Encryption Algorithm (3DEA)</font>


## (B) Before inserting data into table, **encrypted** it:
![Encrypted data](./images/Encrypted_Values_In_Table_1.png)
![Encrypted data](./images/Encrypted_Values_In_Table_2.png)


## (C) Before fetching data from table, **decrypted** it:
![Decrypted data](./images/Decrypted_Values_From_Table.png)


## (D) When you download and build projSqlite01 in Visual Studio:

Go to: Tools -> NuGet Package Manager -> Package Manager Settings

<font size="3">Make sure tick all checkboxes in Package restore.</font>
![General](./images/NuGet_Package_Manager_Setting_1.png)

<font size="3">Make sure package sources includes http://nuget.org/api/v2/</font>
![Package Sources](./images/NuGet_Package_Manager_Setting_2.png)
