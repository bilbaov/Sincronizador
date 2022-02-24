Option Strict On
Option Explicit On
Option Infer On
Imports Eniac

Namespace Reglas
   Public Class Aplicaciones
      Inherits Base

#Region "Constructores"

      Public Sub New()
         Me.NombreEntidad = "Aplicaciones"
         da = New Datos.DataAccess()
      End Sub

      Public Sub New(ByVal accesoDatos As Datos.DataAccess)
         Me.NombreEntidad = "Aplicaciones"
         da = accesoDatos
      End Sub

#End Region

#Region "Overrides"

      Public Overrides Sub Insertar(ByVal entidad As Entidades.Entidad)
         Try
            da.OpenConection()
            da.BeginTransaction()

            Me.EjecutaSP(entidad, TipoSP._I)
            da.CommitTransaction()
         Catch
            da.RollbakTransaction()
            Throw
         Finally
            da.CloseConection()
         End Try
      End Sub

      Public Overrides Sub Actualizar(ByVal entidad As Entidades.Entidad)
         Try
            da.OpenConection()
            da.BeginTransaction()

            Me.EjecutaSP(entidad, TipoSP._U)
            da.CommitTransaction()
         Catch
            da.RollbakTransaction()
            Throw
         Finally
            da.CloseConection()
         End Try
      End Sub

      Public Overrides Sub Borrar(ByVal entidad As Entidades.Entidad)
         Try
            da.OpenConection()
            da.BeginTransaction()

            Me.EjecutaSP(entidad, TipoSP._D)
            da.CommitTransaction()
         Catch
            da.RollbakTransaction()
            Throw
         Finally
            da.CloseConection()
         End Try
      End Sub

      Public Overrides Function Buscar(ByVal entidad As Entidades.Buscar) As DataTable
         Dim sql As SqlServer.Aplicaciones = New SqlServer.Aplicaciones(Me.da)
         Return sql.Buscar(entidad.Columna, entidad.Valor.ToString())
      End Function

      Public Overrides Function GetAll() As System.Data.DataTable
         Return New SqlServer.Aplicaciones(Me.da).Aplicaciones_GA()
      End Function

#End Region

#Region "Metodos Privados"
      Private Sub EjecutaSP(ByVal entidad As Entidades.Entidad, ByVal tipo As TipoSP)
         Dim en As Entidades.Aplicacion = DirectCast(entidad, Entidades.Aplicacion)

         Dim sqlC As SqlServer.Aplicaciones = New SqlServer.Aplicaciones(da)
         Select Case tipo
            Case TipoSP._I
               sqlC.Aplicaciones_I(en.IdAplicacion, en.NombreAplicacion, en.IdAplicacionBase)
            Case TipoSP._U
               sqlC.Aplicaciones_U(en.IdAplicacion, en.NombreAplicacion, en.IdAplicacionBase)
            Case TipoSP._D
               sqlC.Aplicaciones_D(en.IdAplicacion)
         End Select
      End Sub
      Private Sub CargarUno(ByVal o As Entidades.Aplicacion, ByVal dr As DataRow)
         With o
            .IdAplicacion = dr(Entidades.Aplicacion.Columnas.IdAplicacion.ToString()).ToString()
            .NombreAplicacion = dr(Entidades.Aplicacion.Columnas.NombreAplicacion.ToString()).ToString()
            If Not String.IsNullOrEmpty(dr(Entidades.Aplicacion.Columnas.IdAplicacionBase.ToString()).ToString()) Then
               .IdAplicacionBase = dr(Entidades.Aplicacion.Columnas.IdAplicacionBase.ToString()).ToString()
            End If

         End With
      End Sub
#End Region

#Region "Metodos publicos"

      Public Function GetUno(idAplicacion As String) As Entidades.Aplicacion
         Return CargaEntidad(New SqlServer.Aplicaciones(Me.da).Aplicaciones_G1(idAplicacion),
                             Sub(o, dr) CargarUno(o, dr), Function() New Entidades.Aplicacion(),
                             AccionesSiNoExisteRegistro.Vacio, String.Format("No existe Aplicación con IdAplicacion ´{0}´", idAplicacion))
      End Function
      Public Function GetTodos() As List(Of Entidades.Aplicacion)
         Return CargaLista(GetAll(), Sub(o, dr) CargarUno(o, dr), Function() New Entidades.Aplicacion())
      End Function
      Public Function GetFiltradoPorCodigo(idAplicacion As String) As DataTable
         Dim sql = New SqlServer.Aplicaciones(da)
         Dim dt = sql.Aplicaciones_GA(idAplicacion, nombreAplicacion:=String.Empty, idExacto:=True, nombreExacto:=False)
         If dt.Rows.Count = 0 Then
            dt = sql.Aplicaciones_GA(idAplicacion, nombreAplicacion:=String.Empty, idExacto:=False, nombreExacto:=False)
         End If
         Return dt
      End Function
      Public Function GetFiltradoPorNombre(nombreAplicacion As String) As DataTable
         Return New SqlServer.Aplicaciones(da).Aplicaciones_GA(idAplicacion:=String.Empty, nombreAplicacion:=nombreAplicacion, idExacto:=False, nombreExacto:=False)
      End Function

#End Region

   End Class
End Namespace

