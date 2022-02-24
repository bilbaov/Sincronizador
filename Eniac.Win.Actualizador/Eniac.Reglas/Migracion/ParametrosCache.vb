Option Strict On
Option Explicit On
Option Infer On
Imports Entidades.Usuarios

Namespace Reglas
   Public Class ParametrosCache
#Region "Singleton"
      Private Shared _instancia As ParametrosCache = New ParametrosCache()
      ''' <summary>
      ''' Obtiene la instancia única del Cache de Parámetros (ReadOnly/Singleton)
      ''' </summary>
      ''' <returns>Instancia del Cache de Parámetros</returns>
      ''' <remarks></remarks>
      Public Shared ReadOnly Property Instancia As ParametrosCache
         Get
            Return _instancia
         End Get
      End Property
#End Region

      Private dicParametros As Dictionary(Of String, String)

      Private _UltimosParametrosAccedidos As BindingList(Of Tuple(Of DateTime, String, String))
      Public ReadOnly Property UltimosParametrosAccedidos As BindingList(Of Tuple(Of DateTime, String, String))
         Get
            Return _UltimosParametrosAccedidos
         End Get
      End Property

      Private _cantidadMaximaParametrosMonitoreados As Integer

      Public Sub New()
         dicParametros = New Dictionary(Of String, String)()
         _UltimosParametrosAccedidos = New ComponentModel.BindingList(Of Tuple(Of DateTime, String, String))()
      End Sub

      Public Property CantidadMaximaParametrosMonitoreados() As Integer
         Get
            Return _cantidadMaximaParametrosMonitoreados
         End Get
         Set(ByVal value As Integer)
            Dim prevValue = _cantidadMaximaParametrosMonitoreados
            _cantidadMaximaParametrosMonitoreados = value
            If prevValue > _cantidadMaximaParametrosMonitoreados Then
               AjustarCantidadRegistro()
            End If
         End Set
      End Property

#Region "GetValor"
      Public Function GetValorPD(idParametro As String, porDefecto As String) As String
         Return GetValorPDR(Actual.Sucursal.IdEmpresa, idParametro, porDefecto)
      End Function
      Public Function GetValorPDR(idEmpresa As Integer, idParametro As String, porDefecto As String) As String
         idParametro = idParametro.ToUpper()
         If Not dicParametros.ContainsKey(idParametro) Then
            Dim valorParametro As String

            Dim da As New Eniac.Datos.DataAccess()
            Try
               da.OpenConection()

               Dim paramSuc As Entidades.ParametroSucursal = Nothing
               If Actual.Sucursal IsNot Nothing Then
                  paramSuc = New ParametrosSucursales(da).GetUno(idEmpresa, Actual.Sucursal.Id, idParametro, Base.AccionesSiNoExisteRegistro.Nulo)
               End If
               If paramSuc IsNot Nothing Then
                  valorParametro = paramSuc.ValorParametro
               Else
                  valorParametro = New SqlServer.Parametros(da).GetValorPD(idEmpresa, idParametro, porDefecto)
               End If
            Finally
               da.CloseConection()
            End Try

            dicParametros.Add(idParametro, valorParametro)
         End If

         Return AddUltimoParametroAccedido(idParametro)
      End Function

      Private Function AddUltimoParametroAccedido(idParametro As String) As String
         Dim valor = dicParametros(idParametro)
         _UltimosParametrosAccedidos.Insert(0, Tuple.Create(DateTime.Now, idParametro, valor))
         AjustarCantidadRegistro()
         Return valor
      End Function

      Private Sub AjustarCantidadRegistro()
         While UltimosParametrosAccedidos.Count > CantidadMaximaParametrosMonitoreados
            UltimosParametrosAccedidos.RemoveAt(UltimosParametrosAccedidos.Count - 1)
         End While
      End Sub

      Public Function GetValorPD(idParametro As Entidades.Parametro.Parametros, porDefecto As String) As String
         Return Me.GetValorPD(idParametro.ToString(), porDefecto)
      End Function
#End Region

#Region "LimpiarCache"
        ''' <summary>
        ''' Limpia el cache de parámetros.
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub LimpiarCache()
            'Limpio el cache de los parámetros
            dicParametros.Clear()
            ''Limpio las categorías cargadas (si borro todos los parámetros no hay categorías cargadas)
            'dicCategoriasCargadas.Clear()
        End Sub

        ''' <summary>
        ''' Quita del cache un parámetro
        ''' </summary>
        ''' <param name="idParametro">Parámetro a quitar</param>
        ''' <remarks></remarks>
        Public Sub LimpiarCache(idParametro As String)
            'Quito el parámetro del cache
            dicParametros.Remove(idParametro.ToUpper())
        End Sub

        ''' <summary>
        ''' Quita del cache un parámetro
        ''' </summary>
        ''' <param name="idParametro">Parámetro a quitar</param>
        ''' <remarks></remarks>
        Public Sub LimpiarCache(idParametro As Entidades.Parametro.Parametros)
            LimpiarCache(idParametro.ToString())
        End Sub
#End Region

    End Class

End Namespace
