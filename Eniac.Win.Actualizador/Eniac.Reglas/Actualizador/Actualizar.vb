Imports System.IO
Imports System.Net

Public Class Actualizar
   Inherits BaseActualizador

#Region "Metodos Privados"
   ''' <summary>
   ''' Verifica Existencia y recupera datos de Servidor/Base Datos.- --
   ''' </summary>
   Public Sub InicializaSistema()
      If System.IO.File.Exists("BaseDefecto.txt") Then
         Dim base As String = System.IO.File.ReadAllText("BaseDefecto.txt")
         If base.Trim.Length <> 0 Then
            Eniac.Ayudantes.Conexiones.Base = base
            Eniac.Ayudantes.Conexiones.BaseSegura = base
         End If
      End If

      If System.IO.File.Exists("Servidor.txt") Then
         Dim fileContents As String = My.Computer.FileSystem.ReadAllText("Servidor.txt")
         Eniac.Ayudantes.Conexiones.Servidor = fileContents
         'Eniac.Ayudantes.Conexiones.ServidorWS = fileContents.Substring(0, fileContents.IndexOf("\"c))
      End If

      '-- Busco en el disco donde esta instalado y lo seteo como DriverBase.- --
      For Each di As System.IO.DriveInfo In My.Computer.FileSystem.Drives
         If di.DriveType = IO.DriveType.Fixed Then
            Entidades.Publicos.DriverBase = di.Name
            Exit For
         End If
      Next
   End Sub


   ''' <summary>
   ''' Cargando Parametros necesarios del Sistema.-
   ''' </summary>
   ''' <returns>Retorna Entidad de Actualizador.-</returns>
   Public Sub CargarInformacionAtual(eActualiza As Entidades.Actualizador)
      '-- Define Variable de Parametros.- --
      Dim rParametro = New Reglas.Parametros()
      '-- Limpia los parametros del Sistema.- --
      Reglas.ParametrosCache.Instancia.LimpiarCache()

      Entidades.Usuarios.Actual.Sucursal = New Entidades.Sucursales With
         {
            .IdEmpresa = rParametro.EmpresaPrincipal(eActualiza)
         }
      '-- Obtiene Valores de Parametros.- --
      With rParametro
         '-- Nombre de la Aplicacion.- --
         eActualiza.Aplicacion = .GetValorPD("APLICACION", "SIGA")
         '-- Version de Actualizador.- --
         eActualiza.VersionAct = .GetValorPD("VersionDB", "1.0.0")
         '-- Datos del Cliente Actual.- --
         eActualiza.ClientePas = .GetValorPD("CLAVECLIENTESINERGIA", "ClaveCliente")
         '-- Base de Datos Actual.- --
         eActualiza.BaseActual = If(Not String.IsNullOrEmpty(Eniac.Ayudantes.Conexiones.Base), Eniac.Ayudantes.Conexiones.Base, "SIGA")
         '-- Path de Origen Descarga de los Archivos y Script.- --
         eActualiza.URLsActual = .GetValorPD("URLACTUALIZADOR", "http://sinergiamovil.com.ar/actualizador/WSActualizador.svc")
         '-- URL del API-REST.- --
         eActualiza.URLsApiRes = eActualiza.URLsActual.Replace("actualizador/WSActualizador.svc", "SSSServicioWeb/api/Sucesos")
         '-- Path de Destino Descarga de los Archivos y Script.- --
         eActualiza.PathDownlo = .GetValorPD("UBICACIONMSI", My.Application.Info.DirectoryPath)
         '-- Carga Nombre de la Empresa.- --
         eActualiza.NombreEmpr = Reglas.Publicos.NombreEmpresa
      End With
      '-- Despues de obtener la carpeta del MSI me fijo si existe y sino la creo.- --
      If Not System.IO.Directory.Exists(eActualiza.PathDownlo) Then
         System.IO.Directory.CreateDirectory(eActualiza.PathDownlo)
      End If
   End Sub
#End Region

End Class
