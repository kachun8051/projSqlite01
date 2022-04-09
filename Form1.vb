Imports System.Data.SQLite
Public Class Form1



    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        initTheDb()

    End Sub

    Private Function initTheDb() As Boolean
        Dim objDbInit As New clsDbInit
        ' Step one, create database if not exists
        Dim isDbCreated As Boolean = objDbInit.createNewDb()
        If isDbCreated = False Then
            Return False
        End If
        ' Step two, create table if not exists
        Dim isTableCreated As Boolean = objDbInit.createTable
        If isTableCreated = False Then
            Return False
        End If
        ' Step three, fill the table if there is no record
        Dim isFilled As Boolean = objDbInit.fillTable
        If isFilled = False Then
            Return False
        End If
        Return True
    End Function

End Class
