Imports System.Data.SQLite

Public Class clsDbInit

    ' Reference: https://www.nuget.org/packages/System.Data.SQLite/
    ' PM> Install-Package System.Data.SQLite -Version 1.0.115.5

    Private Const dbName As String = "MyDatabase.sqlite"
    Private Const connStr As String = "Data Source=" & dbName & "; Version=3;"
    Private lstSample As List(Of List(Of String))

    Private conn As SQLiteConnection

    Public Function createNewDb() As Boolean
        Dim dbpath As String = IO.Path.Combine(Application.StartupPath, dbName)
        Try
            If IO.File.Exists(dbpath) = False Then
                SQLiteConnection.CreateFile("MyDatabase.sqlite")
            End If
            Return True
        Catch ex As Exception
            Diagnostics.Debug.WriteLine("clsDbInit.createNewDb: " & vbCrLf & ex.Message)
            Return False
        End Try
    End Function

    Public Sub createTable()
        openConn()
        Dim sql As String = "Create Table If Not Exists tblFacebook(Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, " &
            "facebookid TEXT, firstname TEXT, surname TEXT, birthdate TEXT, location TEXT, phonenum TEXT, email TEXT);"
        Dim cmd As SQLiteCommand = New SQLiteCommand(sql, conn)
        cmd.ExecuteNonQuery()
        closeConn()
    End Sub

    Public Sub fillTheList()
        Dim lst01 As New List(Of String)
        lst01.
    End Sub

    Public Sub fillTable()
        openConn()
        Dim cmd As SQLiteCommand = conn.CreateCommand()
        cmd.CommandText = "INSERT INTO tblFacebook(Id, facebookid, firstname, surname, birthdate, location, phonenum, email) " &
            "VALUES(@id, @fid, @fname, @sname, @bd, @loc, @pnum, @em)"

    End Sub

    Private Sub openConn()
        conn = New SQLiteConnection(connStr)
        conn.Open()
    End Sub

    Private Sub closeConn()
        If conn IsNot Nothing Then
            conn.Close()
        End If
    End Sub

End Class
