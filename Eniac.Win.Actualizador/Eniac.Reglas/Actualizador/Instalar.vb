Imports Entidades
''' <summary>
''' Clase de Instalacion de Actualizaciones.
''' </summary>
Public Class Instalar
   Inherits BaseActualizador
#Region "0 - Overrides"
   Public Sub New(oActualiza As Entidades.Actualizador)
      wActualiza = oActualiza
   End Sub
#End Region

#Region "1 - Propiedades"
   '-- Variable de Reglas de Version.- --
   Public WithEvents regVerScr As Reglas.VersionesScripts
#End Region

#Region "2 - Procedimientos"
   Public Sub InstalarActualizaciones()

      '-- Seteo los Valores de visualización de la Progress Bar.- --
      OnInicializaBarPri(New BgwActualizadorSIGA.InicializaBarraProgPriEventArgs(0, _Instalar))
      '--- Actualiza estado del Teclado.- --
      OnActualizaBotons(New BgwActualizadorSIGA.ActualizaTecladoEventArgs(Entidades.Publicos.ActualizaTeclado.DESACTIVOS))
      Try
         '----------------------------------------------------------------------
#Region "Ejecucion de Scripts.- "
         '-- Define Variables de Coleccion de Script.- --
         Dim colScript = New List(Of Entidades.VersionScript)()
         Dim scriptsOk As Boolean = True
         '-- Define nueva Regla de Versiones de Scripts.- --
         regVerScr = New Reglas.VersionesScripts()
         '-- Informo Avance - Actualizo Mensaje.- --
         OnInformarAvances(New BgwActualizadorSIGA.AvanceActualizadorEventArgs(String.Format("Obteniendo los Script desde la Version actual {0}", wActualiza.VersionAct), System.Windows.Forms.Control.DefaultForeColor))
         '-------------------------------------------------------------------
         '-- tengo que obtener los scripts desde la versión actual a la versión que se bajo (ultima)
         colScript = regVerScr.GetScriptsAEjecutar(idAplicacion:=wActualiza.Aplicacion,
         desde:=New System.Version(wActualiza.VersionAct),
         hasta:=wActualiza.VersionNew)

         '-- Valido Cantidad de Script Localizados.- --
         If colScript.Count > 0 Then
            '-- Informo Avance - Actualizo Mensaje.- --
            OnInformarAvances(New BgwActualizadorSIGA.AvanceActualizadorEventArgs(String.Format("Se obtubieron {0} Script desde la Version actual, para su ejecucion", colScript.Count), System.Windows.Forms.Control.DefaultForeColor))
            '----------------------------------------------------------------------
#Region "Proceso de Backup Backup Scripts.-.-"
            Try
               '-- Informo Avance - Actualizo Mensaje.- --
               OnInformarAvances(New BgwActualizadorSIGA.AvanceActualizadorEventArgs("Haciendo backup de la base de datos.-", System.Windows.Forms.Control.DefaultForeColor))
               '-- Inicializa Barra de Descarga.- --
               OnInicializaBarSec(New BgwActualizadorSIGA.InicializaBarraProgSecEventArgs(False, 0, 100, 0))
               '-------------------------------------------------------------------
               '-- Backup de Base de Datos.- --
               Ejec_BackupBaseDatos()
               '-- Informo Avance - Actualizo Mensaje.- --
               OnInformarAvances(New BgwActualizadorSIGA.AvanceActualizadorEventArgs("Proceso de backup Finalizado.-", Color.Green))
               '-- Informo Avance - Actualizo Mensaje.- --
               OnInformarAvances(New BgwActualizadorSIGA.AvanceActualizadorEventArgs("Cargando los scripts localizados...-", System.Windows.Forms.Control.DefaultForeColor))
               '-- Inicializa Barra de Descarga.- --
               OnInicializaBarSec(New BgwActualizadorSIGA.InicializaBarraProgSecEventArgs(False, 0, colScript.Count, 0))
               '-- Actualizo Mensaje.- --
               InformarSucesosWeb(Entidades.TipoSuceso.Actualizador_EjecScripts_Inicio, wActualiza.FechaProce, String.Format("Se ejecutarán {0} scripts", colScript.Count))
               '------------------------------------------------------------------------------------------------------------------------
               '-- Actualizar la Progress Bar Secundaria.- --
               OnReiniciaBarraSec(New BgwActualizadorSIGA.ReiniciaBarraProgSecEventArgs(colScript.Count))
               '-- Ejecuta Listado de Versiones Script Ejecucion.- --
               Dim res As List(Of Entidades.VersionScriptEjecucion) = regVerScr.EjecutarScripts(idAplicacion:=wActualiza.Aplicacion,
                                                                                                   codigoCliente:=wActualiza.ClienteAct(0).Cliente,
                                                                                                   base:=wActualiza.BaseActual,
                                                                                                   versionScripts:=colScript)
               '-- Actualizo Mensaje.- -------------------------------------------------------------------------------------------------
               OnInformarAvances(New BgwActualizadorSIGA.AvanceActualizadorEventArgs(String.Format("Se ejecutaron {1} scripts{0}Exitosos: {2}{0}Fallidos: {3}", " - ",
                                                                                     res.Count,
                                                                                     res.LongCount(Function(x) x.Exitoso),
                                                                                     res.LongCount(Function(x) Not x.Exitoso)), Color.Green))
               '-- Actualizo Mensaje.- --
               InformarSucesosWeb(TipoSuceso.Actualizador_EjecScripts_Fin, wActualiza.FechaProce, String.Format("Se ejecutaron {1} scripts{0}Exitosos: {2}{0}Fallidos: {3}", " - ",
                                                                                                                res.Count,
                                                                                                                res.LongCount(Function(x) x.Exitoso),
                                                                                                                res.LongCount(Function(x) Not x.Exitoso)))
               '-------------------------------------------------------------------------------------------------------------------------
            Catch ex As Exception
               '-- Informo Avance - Actualizo Mensaje.- --
               OnInformarAvances(New BgwActualizadorSIGA.AvanceActualizadorEventArgs(ex.Message, Color.Red))
               '-- Informo Suceso Web.- -------------------------------------------
               InformarSucesosWeb(Entidades.TipoSuceso.Actualizador_EjecScripts_Error, wActualiza.FechaProce, ex.Message)
               '--- Marca de Fin de Proceso.- --
               OnProcesoFinalizado(New BgwActualizadorSIGA.ProcesosFinalizadosEventArgs(False))
               '-------------------------------------------------------------------
               Exit Sub
            End Try
#End Region
            '----------------------------------------------------------------------
            '-- Inicializa Barra de Descarga.- --
            OnInicializaBarSec(New BgwActualizadorSIGA.InicializaBarraProgSecEventArgs(False, 0, 0, 0))
            '-- Verifica Ejecucion de Script.- --
            If Not scriptsOk Then
               '-- Informo Avance - Actualizo Mensaje.- --
               OnInformarAvances(New BgwActualizadorSIGA.AvanceActualizadorEventArgs(String.Format("Sucedio un ERROR durante la ejecucion de los Scripts {0} {1}", Environment.NewLine, "Contactese con Soporte Tecnico."), Color.Red))
               '--- Actualiza estado del Teclado.- --
               OnActualizaBotons(New BgwActualizadorSIGA.ActualizaTecladoEventArgs(Entidades.Publicos.ActualizaTeclado.DESACTIVOS))
               '--- Marca de Fin de Proceso.- --
               OnProcesoFinalizado(New BgwActualizadorSIGA.ProcesosFinalizadosEventArgs(False))
               '--------------------------------------
               Exit Sub
            End If
         End If
#End Region
         '----------------------------------------------------------------------
#Region "Instalacion de MSI-Nuevo"
         ''-- Informo Avance - Actualizo Mensaje.- --
         OnInformarAvances(New BgwActualizadorSIGA.AvanceActualizadorEventArgs("Iniciando Proceso de Instalacion MSI.", System.Windows.Forms.Control.DefaultForeColor))

         '------------------------------------------
         Dim oPathSetup = wActualiza.PathDownlo + wActualiza.FilesNames
         Dim strtInfo = New ProcessStartInfo(oPathSetup)
         With strtInfo
            .WindowStyle = ProcessWindowStyle.Normal
         End With

         Using oProcess = New Process With
                           {
                              .StartInfo = strtInfo
                           }

            '-- Informo Avance - Actualizo Mensaje.- --
            OnInformarAvances(New BgwActualizadorSIGA.AvanceActualizadorEventArgs("Se inicio la Instalacion del MSI.", System.Windows.Forms.Control.DefaultForeColor))
            '-- Informo Suceso Web.- -------------------------------------------
            InformarSucesosWeb(TipoSuceso.Actualizador_InstalandoMSI_Inicio, wActualiza.FechaProce, "Se inicio la Instalacion del MSI.")
            '------------------------------------------
            oProcess.Start()
            oProcess.WaitForExit()
            '------------------------------------------
            If Not oProcess.ExitCodeSuccess Then
               '-- Informo Suceso Web.- -------------------------------------------
               InformarSucesosWeb(TipoSuceso.Actualizador_InstalandoMSI_Error, wActualiza.FechaProce, oProcess.ExitCodeToString())
               '-- Informo Avance - Actualizo Mensaje.- --
               OnInformarAvances(New BgwActualizadorSIGA.AvanceActualizadorEventArgs(oProcess.ExitCodeToString(), Color.Red))
               '-------------------------------------------------------------------
               '-- Cierro la Aplicación.- --
               oProcess.Close()
               '--- Marca de Fin de Proceso.- --
               OnProcesoFinalizado(New BgwActualizadorSIGA.ProcesosFinalizadosEventArgs(False))
               Exit Sub
            Else
               '-- Informo Suceso Web.- -------------------------------------------
               InformarSucesosWeb(TipoSuceso.Actualizador_InstalandoMSI_Fin, wActualiza.FechaProce, "Aplicacion Instalada con Exito!!!")
               '-------------------------------------------------------------------
            End If
            '-- Cierro la Aplicación.- --
            oProcess.Close()
         End Using
         '-- Informo Avance - Actualizo Mensaje.- --
         OnInformarAvances(New BgwActualizadorSIGA.AvanceActualizadorEventArgs("Aplicacion Instalada con Exito!!!", Color.Green))
         '-------------------------------------------------------------------
#End Region
         '----------------------------------------------------------------------
#Region "Actualiza Parametros"
         '-- Informo Avance - Actualizo Mensaje.- --
         OnInformarAvances(New BgwActualizadorSIGA.AvanceActualizadorEventArgs("Actualizando Valores de Parametros", Color.Green))
         '-- Informo Suceso Web.- -------------------------------------------
         InformarSucesosWeb(Entidades.TipoSuceso.Actualizador_Generico, wActualiza.FechaProce, "Actualizando Valores de Parametros")
         '-------------------------------------------------------------------
         '-- Si todo fue bien actualizo la base de datos con la versión que bajaron para que les actualice a los clientes.- --
         Dim pars = New Reglas.Parametros()
         pars.SetValor(Entidades.Usuarios.Actual.Sucursal.IdEmpresa, "VERSIONDB", wActualiza.VersionNew.ToString())
         pars.SetValor(Entidades.Usuarios.Actual.Sucursal.IdEmpresa, "FECHAVERSIONDB", Now.ToString("yyyy-MM-dd HH:mm:ss"))
         pars.SetValor(Entidades.Usuarios.Actual.Sucursal.IdEmpresa, "VERSIONDB_RESPONSABLE", String.Format("{0} actualizó de {1} a {2}", "Actualizador Automático", wActualiza.VersionAct, wActualiza.VersionNew))
         '-- Informo Suceso Web.- -------------------------------------------
         InformarSucesosWeb(Entidades.TipoSuceso.Actualizador_Finalizado, wActualiza.FechaProce, "PROCESO DE ACTUALIZACION EXITOSO.-")
         '-- Informo Avance - Actualizo Mensaje.- --
         OnInformarAvances(New BgwActualizadorSIGA.AvanceActualizadorEventArgs("PROCESO DE ACTUALIZACION EXITOSO.-", Color.Green))
         '-------------------------------------------------------------------
#End Region
         '----------------------------------------------------------------------

         '--- Actualiza estado del Teclado.- --
         OnActualizaBotons(New BgwActualizadorSIGA.ActualizaTecladoEventArgs(Entidades.Publicos.ActualizaTeclado.BUSCAR))
         '--- Marca de Fin de Proceso.- --
         OnProcesoFinalizado(New BgwActualizadorSIGA.ProcesosFinalizadosEventArgs(True))
         '--------------------------------------
      Catch ex As Exception
         '-- Informo Suceso Web.- -------------------------------------------
         InformarSucesosWeb(Entidades.TipoSuceso.Actualizador_InstalandoMSI_Error, wActualiza.FechaProce, ex.Message)
         '-- Informo Avance - Actualizo Mensaje.- --
         OnInformarAvances(New BgwActualizadorSIGA.AvanceActualizadorEventArgs("Proceso de Instalacion Finalizado Con Errores", Color.Red))
         '-------------------------------------------------------------------
      End Try
   End Sub

   Private Sub Ejec_BackupBaseDatos()

      Dim oGene = New Reglas.Generales()
      '-- Define Variable de EntidadesGenerales.- --
      Dim en = New Entidades.General()

      '-- Informo Suceso Web.- -------------------------------------------
      InformarSucesosWeb(TipoSuceso.Actualizador_Backup_Inicio, wActualiza.FechaProce, en.Path)
      '-------------------------------------------------------------------
      '-- Informa Base de Datos.- ----------------------------------------
      en.Base = wActualiza.BaseActual
      en.Path = wActualiza.PathDownlo + "\" + wActualiza.BaseActual + DateTime.Now.ToString("yyyyMMddHHmmss") + ".bak"
      '-------------------------------------------------------------------
      Try
         '-- Proceso de Generacion de Backup de Base de Datos.- --
         oGene.BackupEn(en, AddressOf ActualizaBarraSecundaria)
      Catch ex As Exception
         '-- Actualizo Mensaje.- --
         InformarSucesosWeb(Entidades.TipoSuceso.Actualizador_Backup_Error, wActualiza.FechaProce, ex.Message)
         Throw
      End Try
      '-- Informo Suceso Web.- -------------------------------------------
      InformarSucesosWeb(Entidades.TipoSuceso.Actualizador_Backup_Fin, wActualiza.FechaProce, en.Path)
      '-------------------------

   End Sub
#End Region

#Region "3 - Sub y Funciones.-"
   Private Sub ActualizaBarraSecundaria(oVal As Integer)
      '-- Va indicando en el Progressbar el porcentaje descargado.- --
      OnActualizaBarrSec(New BgwActualizadorSIGA.ActualizaBarraProgSecEventArgs(oVal))
   End Sub
   Private Sub regVerScr_EjecutandoScript(vse As VersionScriptEjecucion) Handles regVerScr.EjecutandoScript
      '-- Informo Avance - Actualizo Mensaje.- --
      OnInformarAvances(New BgwActualizadorSIGA.AvanceActualizadorEventArgs(String.Format("{0} - {1} - {2}", vse.VersionScript.Nombre, vse.VersionScript.Version.NroVersion, vse.VersionScript.Orden), System.Windows.Forms.Control.DefaultForeColor))
      '-------------------------------------------------------------------
      Try
         If Not vse.Exitoso Then
            '-- Reporto el error.- --
            InformarSucesosWeb(Entidades.TipoSuceso.Actualizador_EjecScripts_Error, wActualiza.FechaProce, vse.GetInfoCompleta())
         Else
            '-- Reporto el Exito.- --
            InformarSucesosWeb(Entidades.TipoSuceso.Actualizador_EjecScripts_Exitoso, wActualiza.FechaProce, vse.GetInfoCompleta())
         End If
      Catch ex As Exception
         '-- Si sucede cualquier error prosigo.- --
      End Try
   End Sub
#End Region

End Class