Imports System.Data.SQLite
Public Class Form1



    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'Dim strSql As String = "Create Table If Not Exists highscore"
        If IO.File.Exists(Application.StartupPath & "\MyDatabase.sqlite") = False Then
            SQLiteConnection.CreateFile("MyDatabase.sqlite")
        End If
    End Sub
End Class
