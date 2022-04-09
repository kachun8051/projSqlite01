Imports System.Security.Cryptography
Imports System.Text
Public Class Simple3Des
    Private tdDes As New TripleDESCryptoServiceProvider

    Sub New(ByVal strKey As String)
        tdDes.Key = Truncate(strKey, tdDes.KeySize \ 8)
        tdDes.IV = Truncate("", tdDes.BlockSize \ 8)
    End Sub

    Public Function Encrypt(ByVal strInput As String) As String
        Dim btInputBytes() As Byte =
           Text.Encoding.Unicode.GetBytes(strInput)
        Dim msInput As New IO.MemoryStream
        Dim csEncrypt As New CryptoStream(msInput,
           tdDes.CreateEncryptor(), CryptoStreamMode.Write)

        csEncrypt.Write(btInputBytes, 0, btInputBytes.Length)
        csEncrypt.FlushFinalBlock()

        Return Convert.ToBase64String(msInput.ToArray)

    End Function
    Public Function Decrypt(ByVal strOutput As String) As String

        Dim btOutputBytes() As Byte =
           Convert.FromBase64String(strOutput)
        Dim msOutput As New IO.MemoryStream
        Dim csDecrypt As New CryptoStream(msOutput,
           tdDes.CreateDecryptor(), CryptoStreamMode.Write)

        csDecrypt.Write(btOutputBytes, 0, btOutputBytes.Length)
        csDecrypt.FlushFinalBlock()

        Return Text.Encoding.Unicode.GetString(msOutput.ToArray)

    End Function
    Private Function Truncate(ByVal strKey As String,
          ByVal intLength As Integer) As Byte()

        Dim shaCrypto As New SHA1CryptoServiceProvider
        Dim btKeyBytes() As Byte = Encoding.Unicode.GetBytes(strKey)
        Dim btHash() As Byte = shaCrypto.ComputeHash(btKeyBytes)

        ReDim Preserve btHash(intLength - 1)
        Return btHash

    End Function
End Class
