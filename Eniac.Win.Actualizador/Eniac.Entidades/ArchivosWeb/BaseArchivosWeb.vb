#Region "Options/Imports"

Option Strict On
Option Explicit On
Imports System.IO
Imports System.Net

#End Region

Public Class BaseArchivosWeb

   Public Const FormatoFechas As String = "yyyy-MM-dd HH:mm:ss"

#Region "POST"
   Private respuestaServicioRest As String

   Public Function Post(paginaJSon As Entidades.Sucesos, url As String) As WebSigaServiceResponse
      Return Post(paginaJSon, url, Nothing)
   End Function

   Public Function Post(paginaJSon As Entidades.Sucesos, url As String, headers As Dictionary(Of String, String)) As WebSigaServiceResponse
      Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
      Dim clientesAsJson As String = serializer.Serialize(paginaJSon)

      GetPOSTResponse(New Uri(url), "POST", clientesAsJson, headers,
                      Sub(x)
                         respuestaServicioRest = x
                      End Sub)

      Dim webSigaServiceResponse As WebSigaServiceResponse
      webSigaServiceResponse = TryCast(serializer.Deserialize(respuestaServicioRest, GetType(WebSigaServiceResponse)), WebSigaServiceResponse)

      Return webSigaServiceResponse
   End Function

   Private Sub GetPOSTResponse(uri As Uri, metodo As String, data As String, headers As Dictionary(Of String, String), callback As Action(Of String))
      Dim request As HttpWebRequest = DirectCast(HttpWebRequest.Create(uri), HttpWebRequest)

      request.Method = metodo
      request.ContentType = "application/json; charset=utf-8"

      Dim encoding As New System.Text.UTF8Encoding()
      Dim bytes As Byte() = encoding.GetBytes(data)

      request.ContentLength = bytes.Length
      If headers IsNot Nothing Then
         For Each header As KeyValuePair(Of String, String) In headers
            If Not String.IsNullOrWhiteSpace(header.Key) Then
               request.Headers.Add(header.Key, header.Value)
            End If
         Next
      End If

      Using requestStream As Stream = request.GetRequestStream()
         ' Send the data.
         requestStream.Write(bytes, 0, bytes.Length)
      End Using

      request.BeginGetResponse(
          Function(x)
             Using response As HttpWebResponse = DirectCast(request.EndGetResponse(x), HttpWebResponse)
                If callback IsNot Nothing Then

                      Using stream = response.GetResponseStream()
                          Dim reader = New StreamReader(stream, encoding)
                          Dim responseString As String = reader.ReadToEnd()
                          callback(responseString)
                      End Using
                  End If
             End Using
             Return 0
          End Function, Nothing)
   End Sub

#End Region

   Public Class WebSigaServiceResponse
      Public Property status As Boolean
      Public Property message As String
   End Class

   Public Class WebSigaCountServiceResponse
      Public Property cantidadRegistros As Long
   End Class

   Public Class WebSigaMaxFechaServiceResponse
      Public Property ultimaFechaActualizacion As String
      Public ReadOnly Property ultimaFechaActualizacionAsDate As DateTime?
         Get
            If String.IsNullOrWhiteSpace(ultimaFechaActualizacion) Then Return Nothing
            Try
               Return DateTime.ParseExact(ultimaFechaActualizacion, FormatoFechas, Globalization.CultureInfo.InvariantCulture)
            Catch ex As Exception
               Return Nothing
            End Try
         End Get
      End Property
   End Class

End Class
