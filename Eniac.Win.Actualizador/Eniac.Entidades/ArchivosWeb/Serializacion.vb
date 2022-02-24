Public Class Serializacion

   Private ReadOnly _urlServicioWeb As String
   Private ReadOnly _codigoCliente As List(Of Entidades.EmpresaClienteSucursal)
   Private ReadOnly _baseDatos As String

#Region "Singleton"
   'Private Shared _wActualiza As Entidades.Actualizador
   Private Shared _instancia As Serializacion
   Private Shared ReadOnly _obj As New Object()
   Public Shared Function Instancia(wActualiza As Entidades.Actualizador) As Serializacion
      If _instancia Is Nothing Then
         SyncLock _obj
            If _instancia Is Nothing Then
               _instancia = New Serializacion(wActualiza.URLsApiRes, wActualiza.ClienteAct, wActualiza.BaseActual)
            End If
         End SyncLock
      End If
      Return _instancia
   End Function
#End Region

   Private Sub New(urlServicioWeb As String, codigoCliente As List(Of Entidades.EmpresaClienteSucursal), baseDatos As String)
      _urlServicioWeb = urlServicioWeb
      _codigoCliente = codigoCliente
      _baseDatos = baseDatos
   End Sub

   Private Sub SubirIndividual(suceso As Entidades.Sucesos)
      Try
         Dim archWeb As New BaseArchivosWeb()
         Dim uri As New Uri(New Uri(_urlServicioWeb), "Sucesos") ''  "http://sinergia-pc-04/SSSServicioWeb/api/Sucesos")

         archWeb.Post(suceso, uri.ToString())
      Catch ex As System.Net.WebException
         System.IO.File.WriteAllText("Error", String.Format("Error sucesos {0}{1}", System.Environment.NewLine, ex.ToString()))
         'no hago nada
      End Try
   End Sub

   ''' <summary>
   ''' Informa sucesos convencionales.- --
   ''' </summary>
   ''' <param name="tipoSuceso">Tipo de Suceso</param>
   ''' <param name="fechaEjecucion">Fecha de ejecucion del suceso</param>
   ''' <param name="datos">String de Dato-Mensaje</param>
   Public Sub InformarSuceso(tipoSuceso As Entidades.TipoSuceso, fechaEjecucion As Date, datos As String)
      Dim suceso As Entidades.Sucesos

      For Each scs As Entidades.EmpresaClienteSucursal In _codigoCliente
         suceso = New Entidades.Sucesos With
           {
               .TipoSuceso = tipoSuceso,
               .CodigoCliente = scs.Cliente,
               .BaseDatos = _baseDatos,
               .NombreServidor = My.Computer.Name,
               .FechaEjecucion = fechaEjecucion,
               .Datos = datos
           }
         SubirIndividual(suceso)
      Next
   End Sub
   ''' <summary>
   ''' Informa suceso con serializacion de Mensaje.- --
   ''' </summary>
   ''' <param name="tipoSuceso">Tipo de Suceso</param>
   ''' <param name="fechaEjecucion">Fecha de ejecucion del suceso</param>
   ''' <param name="datos">Serializacion de Mensaje</param>
   Public Sub InformarSuceso(tipoSuceso As Entidades.TipoSuceso, fechaEjecucion As Date, oDatos As Entidades.EmpresaBuscar)
      Dim suceso As Entidades.Sucesos

      For Each scs As Entidades.EmpresaClienteSucursal In _codigoCliente
         Dim datos = New Web.Script.Serialization.JavaScriptSerializer().Serialize(New With {.NombreEmpresa = scs.NombreEmpresa,
                                                                                             .Aplicacion = oDatos.Aplicacion,
                                                                                             .VersionActual = oDatos.VersionActual,
                                                                                             .VersionNueva = oDatos.VersionNueva,
                                                                                             .CodigoCliente = scs.Cliente,
                                                                                             .Base = oDatos.Base})
         suceso = New Entidades.Sucesos With
           {
               .TipoSuceso = tipoSuceso,
               .CodigoCliente = scs.Cliente,
               .BaseDatos = _baseDatos,
               .NombreServidor = My.Computer.Name,
               .FechaEjecucion = fechaEjecucion,
               .Datos = datos
           }
         SubirIndividual(suceso)
      Next
   End Sub

End Class