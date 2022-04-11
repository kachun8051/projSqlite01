Imports System.Data.SQLite

Public Class clsDatagridview

    Private Const dbName As String = "MyDatabase.sqlite"
    Private Const connStr As String = "Data Source=" & dbName & "; Version=3;"
    Private conn As SQLiteConnection

    Private Function getEmptyDataTable() As DataTable
        Dim lst As New List(Of String)({"facebookid", "firstname", "surname", "birthdate", "location", "phonenum", "email"})
        Dim dt As New DataTable
        For Each col As String In lst
            dt.Columns.Add(col, GetType(String))
        Next
        Return dt
    End Function

    Private Function getSaltList() As List(Of String)
        Dim lst As New List(Of String)
        openConn()
        Dim sql_string As String = "Select salt From tblSalt"
        Try
            Using cmd As New SQLiteCommand(connection:=conn, commandText:=sql_string)
                Dim dr As SQLiteDataReader = cmd.ExecuteReader
                While dr.Read
                    lst.Add(dr.Item("salt"))
                End While
            End Using
        Catch ex As Exception
            Diagnostics.Debug.WriteLine("clsDatagridview.getSaltList: " & ex.Message)
        End Try
        closeConn()
        Return lst
    End Function
    Public Function fillTheDgv(ByRef dgv As DataGridView) As Boolean
        ' Dim lstSalt As List(Of String) = getSaltList()

        Dim isFilled As Boolean = False
        openConn()
        Dim sql_string As String = "Select facebookid, firstname, surname, birthdate, location, phonenum, email From tblfacebook"
        Try
            Dim dt As DataTable = getEmptyDataTable()
            Using cmd As New SQLiteCommand(connection:=conn, commandText:=sql_string)
                Dim dr As SQLiteDataReader = cmd.ExecuteReader
                While dr.Read
                    Dim dr2 As DataRow = dt.NewRow
                    dr2.Item("facebookid") = dr.Item("facebookid")
                    dr2.Item("firstname") = dr.Item("firstname")
                    dr2.Item("surname") = dr.Item("surname")
                    dr2.Item("birthdate") = dr.Item("birthdate")
                    dr2.Item("location") = dr.Item("location")
                    dr2.Item("phonenum") = dr.Item("phonenum")
                    dr2.Item("email") = dr.Item("email")
                    dt.Rows.Add(dr2)
                End While
            End Using
            dgv.DataSource = dt
            isFilled = True
        Catch ex As Exception
            Diagnostics.Debug.WriteLine("clsDatagridview.fillTheDgv: " & ex.Message)
            isFilled = False
        End Try
        closeConn()
        Return isFilled
    End Function

    Public Function fillTheDgv2(ByRef dgv As DataGridView) As Boolean
        Dim lstSalt As List(Of String) = getSaltList()
        Dim isFilled As Boolean = False
        openConn()
        Dim sql_string As String = "Select facebookid, firstname, surname, birthdate, location, phonenum, email From tblfacebook"
        Try
            Dim dt As DataTable = getEmptyDataTable()
            Using cmd As New SQLiteCommand(connection:=conn, commandText:=sql_string)
                Dim dr As SQLiteDataReader = cmd.ExecuteReader
                Dim idx As Int32 = 0
                While dr.Read
                    Dim objCrypted As New Simple3Des(lstSalt(idx))
                    Dim dr2 As DataRow = dt.NewRow
                    dr2.Item("facebookid") = objCrypted.Decrypt(dr.Item("facebookid"))
                    dr2.Item("firstname") = objCrypted.Decrypt(dr.Item("firstname"))
                    dr2.Item("surname") = objCrypted.Decrypt(dr.Item("surname"))
                    dr2.Item("birthdate") = objCrypted.Decrypt(dr.Item("birthdate"))
                    dr2.Item("location") = objCrypted.Decrypt(dr.Item("location"))
                    dr2.Item("phonenum") = objCrypted.Decrypt(dr.Item("phonenum"))
                    dr2.Item("email") = objCrypted.Decrypt(dr.Item("email"))
                    dt.Rows.Add(dr2)
                    idx += 1
                End While
            End Using
            dgv.DataSource = dt
            isFilled = True
        Catch ex As Exception
            Diagnostics.Debug.WriteLine("clsDatagridview.fillTheDgv: " & ex.Message)
            isFilled = False
        End Try
        closeConn()
        Return isFilled
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
