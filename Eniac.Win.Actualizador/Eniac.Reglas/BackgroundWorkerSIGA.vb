''' <summary>
''' Clase con todos los Objetos de Comunicacion.- 
''' BackgroundWorker
''' </summary>
Public Class BgwActualizadorSIGA
#Region "Event Args"
   ''' <summary>
   ''' Informa Avance sobre RichText del Formulario.-
   ''' </summary>
   Public Class AvanceActualizadorEventArgs
      Inherits EventArgs
      Public Sub New(oMensaje As String, ocolores As Color)
         ClsMensaje = oMensaje
         ClsColores = ocolores
      End Sub
      Public Property ClsMensaje As String
      Public Property ClsColores As Color
   End Class
   ''' <summary>
   ''' Inicializa la Barra de Proceso Principal.- 
   ''' </summary>
   Public Class InicializaBarraProgPriEventArgs
      Inherits EventArgs
      Public Sub New(oValorMin As Integer, oValorMax As Integer)
         ClsValMin = oValorMin
         ClsValMax = oValorMax
      End Sub
      Public Property ClsValMin As Integer
      Public Property ClsValMax As Integer
   End Class
   ''' <summary>
   ''' Inicializa la Barra de Proceso Sucndaria.- 
   ''' </summary>
   Public Class InicializaBarraProgSecEventArgs
      Inherits EventArgs
      Public Sub New(oVisibles As Boolean, oValorMin As Integer, oValorMax As Integer, oValorVal As Integer)
         ClsVisibl = oVisibles
         ClsValMin = oValorMin
         ClsValMax = oValorMax
         ClsValVal = oValorVal
      End Sub
      Public Property ClsVisibl As Boolean
      Public Property ClsValMin As Integer
      Public Property ClsValMax As Integer
      Public Property ClsValVal As Integer
   End Class
   ''' <summary>
   ''' Actualiza la Barra de Proceso Secundaria.- 
   ''' </summary>
   Public Class ActualizaBarraProgSecEventArgs
      Inherits EventArgs
      Public Sub New(oValorVal As Integer)
         ClsValSec = oValorVal
      End Sub
      Public Property ClsValSec As Integer
   End Class
   ''' <summary>
   ''' Actualiza la Barra de Proceso Secundaria.- 
   ''' </summary>
   Public Class ReiniciaBarraProgSecEventArgs
      Inherits EventArgs
      Public Sub New(oValorVal As Integer)
         ClsValSec = oValorVal
      End Sub
      Public Property ClsValSec As Integer
   End Class
   ''' <summary>
   ''' Actualiza el estado del Teclado.- 
   ''' </summary>
   Public Class ActualizaTecladoEventArgs
      Inherits EventArgs
      Public Sub New(oValorTecla As Entidades.Publicos.ActualizaTeclado)
         ClsValTec = oValorTecla
      End Sub
      Public Property ClsValTec As Entidades.Publicos.ActualizaTeclado
   End Class
   ''' <summary>
   ''' Mensaje de Proceso Finalizado.-
   ''' </summary>
   Public Class ProcesosFinalizadosEventArgs
      Inherits EventArgs
      Public Sub New(Estado As Boolean)
         ClsEstado = Estado
      End Sub

      Public Property ClsEstado As Boolean
   End Class
#End Region
End Class
