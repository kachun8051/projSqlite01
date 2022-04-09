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


