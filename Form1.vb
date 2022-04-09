Imports System.Data.SQLite
Public Class Form1
    ' Timer used to display the result after the form i.e. ui loaded
    Private WithEvents myTimer As New System.Windows.Forms.Timer()
    Private isDbInited As Boolean

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.Text = "Database with encrypted values"
        Label1.Text = "Plain Text value can be Encrypted when saved into table." & vbCrLf &
            "Encrypted value can be Decrypted when fetched from table."
        Label2.Text = "Demo how to decrypted value from table"
        Button1.Text = "Refresh"
        Button2.Text = "Fetch Table Directly"
        Button3.Text = "Fetch Table With Decrypting"
        ' Sets the timer interval to 0.2 seconds.
        myTimer.Interval = 200
        isDbInited = initTheDb()
        myTimer.Start()

    End Sub

    ' This is the method to run when the timer is raised.
    Private Sub TimerEventProcessor(myObject As Object, ByVal myEventArgs As EventArgs) Handles myTimer.Tick
        myTimer.Stop()
        If Me.isDbInited Then
            MsgBox("Successfully init the database")
        Else
            MsgBox("Fail to init the database")
        End If
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

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        initTheDb()
        Button2.PerformClick()
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'Me.DataGridView1.Rows.Clear()
        Dim objDgv As New clsDatagridview
        objDgv.fillTheDgv(Me.DataGridView1)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        'Me.DataGridView1.Rows.Clear()
        Dim objDgv As New clsDatagridview
        objDgv.fillTheDgv2(Me.DataGridView1)
    End Sub
End Class
