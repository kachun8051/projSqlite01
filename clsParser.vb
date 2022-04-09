Imports Newtonsoft.Json
Imports Newtonsoft.Json.Serialization
Imports Newtonsoft.Json.Linq

Public Class clsParser

    Private Const fName As String = "sample.json"

    Property lstSample As List(Of clsRow)

    Private Function checkJsonFileExist() As Boolean
        Dim fpath As String = IO.Path.Combine(Application.StartupPath, fName)
        If IO.File.Exists(fpath) = False Then
            Return False
        End If
        Return True
    End Function

    Public Function parseIt() As Boolean

        If checkJsonFileExist() = False Then
            Return False
        End If
        Try
            Dim fpath As String = IO.Path.Combine(Application.StartupPath, fName)
            Dim objData As clsData = JsonConvert.DeserializeObject(Of clsData)(IO.File.ReadAllText(fpath))
            lstSample = objData.datalist
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

End Class

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
    Sub New()

    End Sub
End Class
