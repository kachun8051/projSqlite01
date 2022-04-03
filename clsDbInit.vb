Imports System.Data.SQLite

Public Class clsDbInit

    ' Reference: https://www.nuget.org/packages/System.Data.SQLite/
    ' PM> Install-Package System.Data.SQLite -Version 1.0.115.5

    Private Const dbName As String = "MyDatabase.sqlite"
    Private Const connStr As String = "Data Source=" & dbName & "; Version=3;"

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
