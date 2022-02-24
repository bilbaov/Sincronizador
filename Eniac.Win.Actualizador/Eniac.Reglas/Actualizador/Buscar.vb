''' <summary>
''' Clase de Busqueda de Actualizaciones.
''' </summary>
Public Class Buscar
   Inherits BaseActualizador

#Region "0 - Overrides"
   Public Sub New(oActualiza As Entidades.Actualizador)
      wActualiza = oActualiza
   End Sub
#End Region

#Region "1 - Propiedades"
   Dim wDatos As Entidades.EmpresaBuscar
#End Region

#Region "2 - Procedimientos"

   ''' <summary>
   ''' Proceso de busqueda de Actualizaciones del Sistema.- 
   ''' </summary>
   Public Sub PrcBuscarActualizaciones()
      '-- Seteo los Valores de visualización de la Progress Bar.- --
      onActualizaBarrPri(New BgwActualizadorSIGA.InicializaBarraProgPriEventArgs(0, _Buscar))
      '--- Actualiza estado del Teclado.- --
      OnActualizaBotons(New BgwActualizadorSIGA.ActualizaTecladoEventArgs(Entidades.Publicos.ActualizaTeclado.DESACTIVOS))
      '-- Ejecuta Proceso de Consultar Versiones.- --
      Try
         '-- Informo Suceso Web.- -------------------------------------------
         wDatos = New Entidades.EmpresaBuscar With {.Aplicacion = wActualiza.Aplicacion,
                                                    .VersionActual = wActualiza.VersionAct,
                                                    .VersionNueva = wActualiza.VersionNew,
                                                    .Base = wActualiza.BaseActual}
         InformarSucesosWeb(Entidades.TipoSuceso.Actualizador_InicioBusqueda,
                           wActualiza.FechaProce,
                           wDatos)
         '-- Informo Avance - Actualizo Mensaje.- --
         OnInformarAvances(New BgwActualizadorSIGA.AvanceActualizadorEventArgs("Consultando sitio " + wActualiza.URLsActual,
                                                                               System.Windows.Forms.Control.DefaultForeColor))
         '-------------------------------------------------------------------
         '-- Asigna URL del Sitio.- -- 
         Dim ws = New WSActualiza.WSActualizador With
               {
                  .Url = wActualiza.URLsActual
               }
         '-- Informo Avance - Actualizo Mensaje.- --
         OnInformarAvances(New BgwActualizadorSIGA.AvanceActualizadorEventArgs("Obtieniendo los datos de la Ultima Version.-",
                                                                                          System.Windows.Forms.Control.DefaultForeColor))
         '-------------------------------------------------------------------
         '-- Obtiene los datos de la Ultima Version.- --
         Dim ver As String = ws.GetUltimaVersion(wActualiza.Aplicacion, wActualiza.ClienteAct(0).Cliente, True, wActualiza.ClientePas, wActualiza.BaseActual)
         '-- Obteniendo Version Actual.- --
         Dim versionActual = New System.Version(wActualiza.VersionAct)
         '-- Verifica Versionado.- --
         If String.IsNullOrEmpty(ver) Then
            wActualiza.VersionNew = versionActual
         Else
            wActualiza.VersionNew = New System.Version(ver)
         End If
         '-- Compara Versionado.- --
         If wActualiza.VersionNew > versionActual Then
            '-- Informo Avance - Actualizo Mensaje.- --
            OnInformarAvances(New BgwActualizadorSIGA.AvanceActualizadorEventArgs(String.Format("Esta disponible la versión {0} para actualizar la aplicación.", wActualiza.VersionNew.ToString()),
                                                                                             Color.Blue))
            '--- Actualiza estado del Teclado.- --
            OnActualizaBotons(New BgwActualizadorSIGA.ActualizaTecladoEventArgs(Entidades.Publicos.ActualizaTeclado.DESCARGAR))
            '-------------------------------------------------------------------
            '-- Informo Avance - Actualizo Mensaje.- --
            OnInformarAvances(New BgwActualizadorSIGA.AvanceActualizadorEventArgs("PROCESO de BUSQUEDA - FINALIZADO.-",
                                                                                             Color.Green))
         Else
            Dim rMensaje = "No se encontro ninguna versión a actualizar pero puede bajar el instalador actual."
            '-- Informo Avance - Actualizo Mensaje.- --
            OnInformarAvances(New BgwActualizadorSIGA.AvanceActualizadorEventArgs(rMensaje,
                                                                                             Color.Red))
            '-- Informo Suceso Web.- -------------------------------------------
            wDatos = New Entidades.EmpresaBuscar With {.Aplicacion = wActualiza.Aplicacion,
                                                       .VersionActual = wActualiza.VersionAct,
                                                       .VersionNueva = wActualiza.VersionNew,
                                                       .Base = wActualiza.BaseActual}

            InformarSucesosWeb(Entidades.TipoSuceso.Actualizador_InicioBusqueda,
                                wActualiza.FechaProce,
                                wDatos)
            '--- Actualiza estado del Teclado.- --
            OnActualizaBotons(New BgwActualizadorSIGA.ActualizaTecladoEventArgs(Entidades.Publicos.ActualizaTeclado.DESACTIVOS))
            '-------------------------------------------------------------------
            '-- Informo Avance - Actualizo Mensaje.- --
            OnInformarAvances(New BgwActualizadorSIGA.AvanceActualizadorEventArgs("PROCESO de BUSQUEDA - FINALIZADO - SIN NOVEDADES.-", Color.Red))
            Exit Sub
         End If
      Catch ex As Exception
         '-- Informo Suceso Web.- -------------------------------------------
         InformarSucesosWeb(Entidades.TipoSuceso.Actualizador_InicioBusqueda_Error, wActualiza.FechaProce, ex.Message)
         '-- Informo Avance - Actualizo Mensaje.- --
         OnInformarAvances(New BgwActualizadorSIGA.AvanceActualizadorEventArgs(String.Format("Sucedio un inconveniente, contacte a sistemas. {0} {1}", System.Environment.NewLine, ex.Message), Color.Red))
         '-------------------------------------------------------------------
      End Try
   End Sub
#End Region

End Class