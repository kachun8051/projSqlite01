Imports System.Data.SQLite

Public Class clsDbInit

    ' Reference: https://www.nuget.org/packages/System.Data.SQLite/
    ' PM> Install-Package System.Data.SQLite -Version 1.0.115.5

    Private Const dbName As String = "MyDatabase.sqlite"
    Private Const connStr As String = "Data Source=" & dbName & "; Version=3;"
    ' Private lstSample As List(Of List(Of String))
    Private objParser As clsParser

    Private conn As SQLiteConnection

    Public Function createNewDb() As Boolean
        ' Create Database i.e. sqlite if not exists
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

    Public Function createTable() As Boolean
        Dim isCreated As Boolean = False
        openConn()
        Dim sql1 As String = "Create Table If Not Exists tblFacebook(Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, " &
            "facebookid TEXT, firstname TEXT, surname TEXT, birthdate TEXT, location TEXT, phonenum TEXT, email TEXT);"
        Dim sql2 As String = "Create Table If Not Exists tblSalt(Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, salt TEXT);"
        Dim cmd As SQLiteCommand
        Try
            cmd = New SQLiteCommand(conn)
            cmd.CommandText = sql1
            cmd.ExecuteNonQuery()
            cmd.CommandText = sql2
            cmd.ExecuteNonQuery()
            isCreated = True
        Catch ex As Exception
            isCreated = False
        End Try
        closeConn()
        Return isCreated
    End Function

    Private Function fillTheList() As Boolean
        ' Dim lst01 As New List(Of String)
        objParser = New clsParser
        Return objParser.parseIt

    End Function

    Private Function getNumOfRows() As Int32
        openConn()
        Dim numofrow As Int32 = 0
        Dim sql_string As String = "SELECT Count(0) FROM tblFacebook"
        Using cmd As New SQLiteCommand(connection:=conn, commandText:=sql_string)
            Dim result As Object = cmd.ExecuteScalar()
            If result <> Nothing Then
                numofrow = result.ToString()
            End If
        End Using
        closeConn()
        Return numofrow
    End Function

    Public Function fillTable() As Boolean

        If getNumOfRows() > 0 Then
            ' No need to fill the table if num of record(s) > 0
            Return True
        End If

        If fillTheList() = False Then
            Return False
        End If

        openConn()
        Dim cmd1 As SQLiteCommand = conn.CreateCommand()
        Dim cmd2 As SQLiteCommand = conn.CreateCommand()
        Dim sql1 As String = "INSERT INTO tblFacebook(facebookid, firstname, surname, birthdate, location, phonenum, email) " &
            "VALUES(@fid, @fname, @sname, @bd, @loc, @pnum, @em)"
        Dim sql2 As String = "INSERT INTO tblSalt(salt) VALUES(@salt)"
        cmd1.CommandText = sql1
        cmd2.CommandText = sql2
        For Each obj As clsRow In objParser.lstSample
            cmd1.Parameters.AddWithValue("fid", obj.getEncrypted("facebookid"))
            cmd1.Parameters.AddWithValue("fname", obj.getEncrypted("firstname"))
            cmd1.Parameters.AddWithValue("sname", obj.getEncrypted("surname"))
            cmd1.Parameters.AddWithValue("bd", obj.getEncrypted("birthdate"))
            cmd1.Parameters.AddWithValue("loc", obj.getEncrypted("location"))
            cmd1.Parameters.AddWithValue("pnum", obj.getEncrypted("phonenum"))
            cmd1.Parameters.AddWithValue("em", obj.getEncrypted("email"))
            cmd1.ExecuteNonQuery()
            cmd2.Parameters.AddWithValue("salt", obj.salt)
            cmd2.ExecuteNonQuery()
        Next
        cmd1.Dispose()
        cmd2.Dispose()
        closeConn()
        Return True

    End Function

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
