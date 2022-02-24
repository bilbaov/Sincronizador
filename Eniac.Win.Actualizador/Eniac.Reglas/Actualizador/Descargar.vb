''' <summary>
''' Clase de Descarga de Actualizaciones.
''' </summary>
Public Class Descargar
   Inherits BaseActualizador

#Region "0 - Overrides"
   Public Sub New(oActualiza As Entidades.Actualizador)
      wActualiza = oActualiza
   End Sub
#End Region

#Region "1 - Propiedades"

#End Region

#Region "2 - Procedimientos"
   ''' <summary>
   ''' Proceso General de Descarga de Actualizaciones.- 
   ''' </summary>
   Public Sub DescargarActualizacion()
      '-- Seteo los Valores de visualización de la Progress Bar.- --
      OnInicializaBarPri(New BgwActualizadorSIGA.InicializaBarraProgPriEventArgs(0, _Descargar))
      '--- Actualiza estado del Teclado.- --
      OnActualizaBotons(New BgwActualizadorSIGA.ActualizaTecladoEventArgs(Entidades.Publicos.ActualizaTeclado.DESACTIVOS))
      '-- Ejecuta Proceso de Descarga de Versiones.- --
      Try
         '-- Informo Avance - Actualizo Mensaje.- --
         OnInformarAvances(New BgwActualizadorSIGA.AvanceActualizadorEventArgs("Consultando información en el servidor...", System.Windows.Forms.Control.DefaultForeColor))
         '-------------------------------------------------------------------
         '-- Localizando Informacion del Servidor.- --
         Dim _resp As WSActualiza.Versiones
         _resp = GetVersiones(wActualiza)
         '-- Prepara Variables de Procesos.- --
         Dim versio = New List(Of Entidades.VersionScript)()
         Dim reg As Reglas.VersionesScripts
         Dim vs As Entidades.VersionScript

#Region "Descarga - Grabacion de Script"
         '-- Defino Variable para indicar la baja de Script.- --
         Dim scriptsOk As Boolean = True
         Try
            If _resp Is Nothing OrElse _resp.ListadoVersiones.Length = 0 Then
               '-- Informo Avance - Actualizo Mensaje.- --
               OnInformarAvances(New BgwActualizadorSIGA.AvanceActualizadorEventArgs("No se obtuvo ninguna versión del servidor a actualizar.", System.Windows.Forms.Control.DefaultForeColor))
               '--- Actualiza estado del Teclado.- --
               OnActualizaBotons(New BgwActualizadorSIGA.ActualizaTecladoEventArgs(Entidades.Publicos.ActualizaTeclado.DESCARGAR))
               '-------------------------------------------------------------------
               Exit Sub
            End If

            '-- Cargar Scripts ----------------------------------------------------
            '-- Informo Avance - Actualizo Mensaje.- --
            OnInformarAvances(New BgwActualizadorSIGA.AvanceActualizadorEventArgs("Obteniendo paquete de scripts nuevos.", System.Windows.Forms.Control.DefaultForeColor))
            '-------------------------------------------------------------------
            reg = New Reglas.VersionesScripts()
            For Each ve As WSActualiza.Version In _resp.ListadoVersiones
               For Each sc As WSActualiza.VersionScript In ve.Scripts.Items
                  vs = New Entidades.VersionScript()
                  vs.Aplicacion.IdAplicacion = wActualiza.Aplicacion
                  vs.Version = New Entidades.Version() _
                                       With {.NroVersion = sc.NroVersion,
                                             .IdAplicacion = vs.Aplicacion.IdAplicacion,
                                             .VersionFecha = New DateTime(1900, 1, 1)}

                  vs.Orden = sc.Orden
                  vs.Nombre = sc.Nombre
                  vs.Obligatorio = sc.Obligatorio
                  vs.Script = sc.Script
                  versio.Add(vs)
               Next
            Next
            '-- Se informa la cantida de Script Recolectados.- --
            InformarSucesosWeb(Entidades.TipoSuceso.Actualizador_Scripts_Grabando,
                               wActualiza.FechaProce,
                               String.Format("Se descargaron {0} scripts para ejecutar.", versio.Count.ToString()))
            '-------------------------------------------------------------------
            If (versio.Count > 0) Then
               '-- Informo Avance - Actualizo Mensaje.- --
               OnInformarAvances(New BgwActualizadorSIGA.AvanceActualizadorEventArgs("Grabando scripts en tabla de versiones.", System.Windows.Forms.Control.DefaultForeColor))
               '-------------------------------------------------------------------
               '-- Desacarga de todos los Scripts.- --
               reg.ProcesarBajadasDeScripts(wActualiza.Aplicacion, versio)
            End If
            '-- Se informa la cantida de Script Recolectados.- --
            InformarSucesosWeb(Entidades.TipoSuceso.Actualizador_Scripts_Grabados,
                               wActualiza.FechaProce,
                               String.Format("Se guardaron {0} scripts para ejecutar.", versio.Count.ToString()))

            '-- Actualizo Mensaje.- --
            OnInformarAvances(New BgwActualizadorSIGA.AvanceActualizadorEventArgs(String.Format("Se grabaron {0} scripts para ejecutar.", versio.Count.ToString()), Color.Green))
            '-------------------------------------------------------------------
         Catch ex As Exception
            '-- Seta Flack de Script en False.- --
            scriptsOk = False
            '-- Se informa la cantida de Script Recolectados.- --
            InformarSucesosWeb(Entidades.TipoSuceso.Actualizador_Scripts_ErrorBajando,
                               wActualiza.FechaProce,
                               ex.Message)
            '--- Actualiza estado del Teclado.- --
            OnActualizaBotons(New BgwActualizadorSIGA.ActualizaTecladoEventArgs(Entidades.Publicos.ActualizaTeclado.DESCARGAR))
            '-- Actualizo Mensaje.- --
            OnInformarAvances(New BgwActualizadorSIGA.AvanceActualizadorEventArgs("Sucedio un inconveniente al bajar los scripts, contacte a Sistemas.", Color.Red))
            '-------------------------------------------------------------------
         End Try
#End Region

#Region "Descarga - Ejecucion de MSI.-"
         If scriptsOk Then
            Try
               '-- Ejecuta Proceso de descarga de MSI.- --
               If Not DescargarMSI(_resp) Then
                  '--- Actualiza estado del Teclado.- --
                  OnActualizaBotons(New BgwActualizadorSIGA.ActualizaTecladoEventArgs(Entidades.Publicos.ActualizaTeclado.DESCARGAR))
                  '-- Fuerza Salida.- --
                  Return
               End If
            Catch ex As Exception
               '-- Se informa la cantida de Script Recolectados.- --
               InformarSucesosWeb(Entidades.TipoSuceso.Actualizador_MSI_Error_BajandoInstaladores,
                                  wActualiza.FechaProce,
                                  ex.Message)
               '--- Actualiza estado del Teclado.- --
               OnActualizaBotons(New BgwActualizadorSIGA.ActualizaTecladoEventArgs(Entidades.Publicos.ActualizaTeclado.DESCARGAR))
               '-- Actualizo Mensaje.- --
               OnInformarAvances(New BgwActualizadorSIGA.AvanceActualizadorEventArgs("Sucedio un inconveniente al bajar los scripts, contacte a Sistemas.", Color.Red))
               '-------------------------------------------------------------------
            End Try
         End If
#End Region
      Catch ex As Exception
         '-- Informo Suceso Web.- -------------------------------------------
         InformarSucesosWeb(Entidades.TipoSuceso.Actualizador_Scripts_ErrorBajando, wActualiza.FechaProce, ex.Message)
         '-- Informo Avance - Actualizo Mensaje.- --
         OnInformarAvances(New BgwActualizadorSIGA.AvanceActualizadorEventArgs(String.Format("Sucedio un inconveniente, contacte a sistemas. {0} {1}", System.Environment.NewLine, ex.Message), Color.Red))
         '-------------------------------------------------------------------
      End Try
   End Sub
#End Region

#Region "3 - Sub y Funciones.-"
   ''' <summary>
   ''' Recupera los Datos de la Version Completa del Sistema Actual.-
   ''' </summary>
   ''' <param name="oActualiza">Entidad Actualizador con DAtos Actuales.-</param>
   ''' <returns></returns>
   Public Function GetVersiones(oActualiza As Entidades.Actualizador) As WSActualiza.Versiones
      '-- Define Clase de Actualizador.- --
      Dim ws = New WSActualiza.WSActualizador With {
         .Url = oActualiza.URLsActual
      }

      Return ws.GetVersionCompleta(oActualiza.Aplicacion,
                                   oActualiza.VersionAct,
                                   oActualiza.VersionNew.ToString(),
                                   oActualiza.ClienteAct(0).Cliente,
                                   True,
                                   oActualiza.ClientePas,
                                   oActualiza.BaseActual)
   End Function
   ''' <summary>
   ''' Procedimiento Obtencion Ultima Version del Sistema.-
   ''' </summary>
   ''' <param name="versiones">Listado de Versiones Actuales.-</param>
   ''' <returns></returns>
   Public Function GetUltimaVersion(versiones As WSActualiza.Versiones) As WSActualiza.Version
      Dim ultVrs = New WSActualiza.Version With
        {
            .NroVersion = ""
        }
      For Each ve As WSActualiza.Version In versiones.ListadoVersiones
         If ve.NroVersion > ultVrs.NroVersion Then
            ultVrs = ve
         End If
      Next
      Return ultVrs
   End Function

   ''' <summary>
   ''' Procedimeinto de Desacarga de MSI.- --
   ''' </summary>
   ''' <param name="oResp">Variable de Versiones.-</param>
   ''' <returns></returns>
   Public Function DescargarMSI(oResp As WSActualiza.Versiones) As Boolean
      '-- Informo Avance - Actualizo Mensaje.- --
      OnInformarAvances(New BgwActualizadorSIGA.AvanceActualizadorEventArgs("Obteniendo URL de ultima version para Descarga de Archivo MSI...", System.Windows.Forms.Control.DefaultForeColor))
      '-------------------------------------------------------------------
      '-- Obtienen la Ultima Version MSI.- --
      wActualiza.URLsApiMsi = GetUltimaVersion(oResp).URLMSI
      '-- Informo Suceso Web.- -------------------------------------------
      InformarSucesosWeb(Entidades.TipoSuceso.Actualizador_MSI_IniciandoBajada, wActualiza.FechaProce, "Iniciando proceso de Descarga de Archivo MSI...")
      '-- Informo Avance - Actualizo Mensaje.- --
      OnInformarAvances(New BgwActualizadorSIGA.AvanceActualizadorEventArgs("Iniciando proceso de Descarga de Archivo MSI...", System.Windows.Forms.Control.DefaultForeColor))
      '-------------------------------------------------------------------
      wActualiza.FilesNames = System.IO.Path.GetFileName(wActualiza.URLsApiMsi)

      Dim nomSinExt As String = System.IO.Path.GetFileNameWithoutExtension(wActualiza.URLsApiMsi)
      Dim exten As String = System.IO.Path.GetExtension(wActualiza.URLsApiMsi)

      '-- Haciendo backup del MSI Actual.---------------------------------------------------------
      '-- Informo Avance - Actualizo Mensaje.- --
      OnInformarAvances(New BgwActualizadorSIGA.AvanceActualizadorEventArgs("Procediendo a realizar backup previo del MSI Actual.", System.Windows.Forms.Control.DefaultForeColor))
      '-------------------------------------------------------------------
      Dim archivoOrigen As String = IO.Path.Combine(wActualiza.PathDownlo, wActualiza.FilesNames)
      If System.IO.File.Exists(archivoOrigen) Then
         Dim archivoDestino As String = IO.Path.Combine(wActualiza.PathDownlo, String.Format("{0}_{1:yyyyMMddHHmmss}{2}", nomSinExt, DateTime.Now, exten))
         Try
            System.IO.File.Copy(archivoOrigen, archivoDestino)
         Catch ex As Exception
            Throw New Exception(String.Format("Error respaldando el instalador: {0}", ex.Message), ex)
         End Try
         System.IO.File.Delete(archivoOrigen)
      End If
      '------------------------------------------------------------------------------------------
      '-- Informo Avance - Actualizo Mensaje.- --
      OnInformarAvances(New BgwActualizadorSIGA.AvanceActualizadorEventArgs("Fin del backup del MSI Actual...", Color.Green))
      '-- Informo Suceso Web.- -------------------------------------------
      InformarSucesosWeb(Entidades.TipoSuceso.Actualizador_MSI_FinBajada, wActualiza.FechaProce, "Proceso de Descarga Exitoso de Archivo MSI...")
      '-------------------------------------------------------------------
      '-- Proceso de Descarga de MSI.- --
      Return True
      '-------------------------------------------------------------------
   End Function

#End Region

End Class