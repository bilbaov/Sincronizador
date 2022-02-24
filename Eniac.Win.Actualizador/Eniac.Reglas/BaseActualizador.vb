''' <summary>
''' Clase Base de Actualizador.- 
''' </summary>
Public Class BaseActualizador
#Region "Propiedades Clase Base.-"
   ''' <summary>
   ''' Propiedades de Seteo de Barras de Tareas.- 
   ''' </summary>
   Public Const _Buscar As Integer = 4
   Public Const _Descargar As Integer = 11
   Public Const _Instalar As Integer = 30
   ''' <summary>
   ''' Clase de Actualizacion.
   ''' </summary>
   Public Property wActualiza As Entidades.Actualizador
#End Region
#Region "Serializaciones.-"

   ''' <summary>
   ''' Proceso de Informe de sucesos a la Web
   ''' </summary>
   ''' <param name="oTipoSuceso">Codigo de Tipo de Suceso.-</param>
   ''' <param name="oFechaEject">Fecha de Suceso.-</param>
   ''' <param name="oDatos">Dato o Mensaje a Informar.-</param>
   Public Sub InformarSucesosWeb(oTipoSuceso As Entidades.TipoSuceso, oFechaEject As Date, oDatos As String)
      Serializacion.Instancia(wActualiza).InformarSuceso(oTipoSuceso, oFechaEject, oDatos)
   End Sub
   Public Sub InformarSucesosWeb(oTipoSuceso As Entidades.TipoSuceso, oFechaEject As Date, oDatos As Entidades.EmpresaBuscar)
      Serializacion.Instancia(wActualiza).InformarSuceso(oTipoSuceso, oFechaEject, oDatos)
   End Sub
#End Region
#Region "Eventos - Backgroundworker"
   ''' <summary>
   ''' Envento de Informe de Avances de Proceso.- 
   ''' </summary>
   Public Event InformarAvances As EventHandler(Of BgwActualizadorSIGA.AvanceActualizadorEventArgs)
   Protected Overridable Sub OnInformarAvances(e As BgwActualizadorSIGA.AvanceActualizadorEventArgs)
      RaiseEvent InformarAvances(Me, e)
   End Sub
   ''' <summary>
   ''' Actualizador de Teclado del Formulario.- 
   ''' </summary>
   Public Event ActualizaBotons As EventHandler(Of BgwActualizadorSIGA.ActualizaTecladoEventArgs)
   Protected Overridable Sub OnActualizaBotons(e As BgwActualizadorSIGA.ActualizaTecladoEventArgs)
      RaiseEvent ActualizaBotons(Me, e)
   End Sub
   ''' <summary>
   ''' Inicializa Barra de Progreso Principal.- 
   ''' </summary>
   Public Event InicializaBarPri As EventHandler(Of BgwActualizadorSIGA.InicializaBarraProgPriEventArgs)
   Protected Overridable Sub OnInicializaBarPri(e As BgwActualizadorSIGA.InicializaBarraProgPriEventArgs)
      RaiseEvent InicializaBarPri(Me, e)
   End Sub
   ''' <summary>
   ''' Inicializa Barra de Progreso Secundaria.- 
   ''' </summary>
   Public Event InicializaBarSec As EventHandler(Of BgwActualizadorSIGA.InicializaBarraProgSecEventArgs)
   Protected Overridable Sub OnInicializaBarSec(e As BgwActualizadorSIGA.InicializaBarraProgSecEventArgs)
      RaiseEvent InicializaBarSec(Me, e)
   End Sub
   ''' <summary>
   ''' Actualiza el Avance de la Barra de Progreso Principal.- 
   ''' </summary>
   Public Event ActualizaBarrPri As EventHandler(Of BgwActualizadorSIGA.InicializaBarraProgPriEventArgs)
   Protected Overridable Sub OnActualizaBarrPri(e As BgwActualizadorSIGA.InicializaBarraProgPriEventArgs)
      RaiseEvent ActualizaBarrPri(Me, e)
   End Sub
   ''' <summary>
   ''' Actualiza el Avance de la Barra de Progreso Secundaria.- 
   ''' </summary>
   Public Event ActualizaBarrSec As EventHandler(Of BgwActualizadorSIGA.ActualizaBarraProgSecEventArgs)
   Protected Overridable Sub OnActualizaBarrSec(e As BgwActualizadorSIGA.ActualizaBarraProgSecEventArgs)
      RaiseEvent ActualizaBarrSec(Me, e)
   End Sub
   ''' <summary>
   ''' Reinicia Barra de Progreso Secundaria.- 
   ''' </summary>
   Public Event ReiniciaBarraSec As EventHandler(Of BgwActualizadorSIGA.ReiniciaBarraProgSecEventArgs)
   Protected Overridable Sub OnReiniciaBarraSec(e As BgwActualizadorSIGA.ReiniciaBarraProgSecEventArgs)
      RaiseEvent ReiniciaBarraSec(Me, e)
   End Sub
   ''' <summary>
   ''' Marca de Finalizacion de Proceso.- 
   ''' </summary>
   Public Event ProcesoFinalizado As EventHandler(Of BgwActualizadorSIGA.ProcesosFinalizadosEventArgs)
   Protected Overridable Sub OnProcesoFinalizado(e As BgwActualizadorSIGA.ProcesosFinalizadosEventArgs)
      RaiseEvent ProcesoFinalizado(Me, e)
   End Sub
#End Region
End Class
