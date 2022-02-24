
Namespace Entidades
   ''' <summary>
   ''' Entidad de Datos de Actualizador.-
   ''' </summary>
   Public Class Actualizador
      Public Property Aplicacion As String                           '-- Detalle de Aplicacion.- --
      Public Property VersionAct As String                           '-- Version de Actual del Sistema.- --
      Public Property VersionNew As System.Version                   '-- Nueva Version del Sistema.- --
      Public Property ClienteAct As List(Of EmpresaClienteSucursal)  '-- Cliente del Sistema Numero.- --
      Public Property ClientePas As String                           '-- Password del Cliente de Sistema.- --  
      Public Property BaseActual As String                           '-- Base de Datos Actual.- --
      Public Property PathDownlo As String                           '-- Path de Descarga del Sistema.- --
      Public Property URLsActual As String                           '-- Url de Descarga Actual.- --
      Public Property URLsApiRes As String                           '-- Url de Api Rest.- --
      Public Property URLsApiMsi As String                           '-- Url Descarga del MSI.- --
      Public Property FilesNames As String                           '-- Nombre del Archivo.- --
      Public Property NombreEmpr As String                           '-- Nombre de la Empresa.- --
      ''' <summary>
      ''' Establece la Fecha de Proceso de Actualizacion.- 
      ''' </summary>
      ''' <returns></returns>
      Public Property FechaProce As DateTime
      Public Sub New(fecha As Date)
         FechaProce = fecha
      End Sub
   End Class

End Namespace
