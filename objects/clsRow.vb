Public Class clsData
    Property datalist As List(Of clsRow)
    Sub New()
        datalist = New List(Of clsRow)
    End Sub
End Class

Public Class clsRow
    Property facebookid As String
    Property firstname As String
    Property surname As String
    Property gender As String
    Property birthdate As String
    Property location As String
    Property phonenum As String
    Property email As String
    Property salt As String

    Private objCypher As Simple3Des

    Public Function getDecrypted(field As String) As String
        If objCypher Is Nothing Then
            objCypher = New Simple3Des(salt)
        End If
        Select Case field.ToLower
            Case "facebookid"
                Return objCypher.Decrypt(facebookid)
            Case "firstname"
                Return objCypher.Decrypt(firstname)
            Case "surname"
                Return objCypher.Decrypt(surname)
            Case "gender"
                Return objCypher.Decrypt(gender)
            Case "birthdate"
                Return objCypher.Decrypt(birthdate)
            Case "location"
                Return objCypher.Decrypt(location)
            Case "phonenum"
                Return objCypher.Decrypt(phonenum)
            Case "email"
                Return objCypher.Decrypt(email)
            Case Else
                Return "unknown"
        End Select
    End Function

    Public Function getEncrypted(field As String) As String
        If objCypher Is Nothing Then
            objCypher = New Simple3Des(salt)
        End If
        Select Case field.ToLower
            Case "facebookid"
                Return objCypher.Encrypt(facebookid)
            Case "firstname"
                Return objCypher.Encrypt(firstname)
            Case "surname"
                Return objCypher.Encrypt(surname)
            Case "gender"
                Return objCypher.Encrypt(gender)
            Case "birthdate"
                Return objCypher.Encrypt(birthdate)
            Case "location"
                Return objCypher.Encrypt(location)
            Case "phonenum"
                Return objCypher.Encrypt(phonenum)
            Case "email"
                Return objCypher.Encrypt(email)
            Case Else
                Return "unknown"
        End Select

    End Function


    Sub New()

    End Sub
End Class
