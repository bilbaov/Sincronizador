#Region "Option/Imports"
Option Strict On
Option Explicit On
Imports Entidades.Usuarios
Imports Eniac

#End Region

Namespace Reglas
   Public Class ParametrosSucursales
      Inherits Base
#Region "Constructores"

      Public Sub New()
         Me.New(New Datos.DataAccess())
      End Sub
      Public Sub New(ByVal accesoDatos As Datos.DataAccess)
         Me.NombreEntidad = Entidades.ParametroSucursal.NombreTabla
         da = accesoDatos
      End Sub

#End Region

#Region "Overrides"
      Public Overrides Sub Insertar(ByVal entidad As Entidades.Entidad)
         Try
            da.OpenConection()
            da.BeginTransaction()
            Me.Inserta(entidad)
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
            Me.Actualiza(entidad)
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
            Me.Borra(entidad)
            da.CommitTransaction()
         Catch
            da.RollbakTransaction()
            Throw
         Finally
            da.CloseConection()
         End Try
      End Sub

      Public Overrides Function GetAll() As System.Data.DataTable
         Return New SqlServer.ParametrosSucursales(Me.da).ParametrosSucursales_GA(Actual.Sucursal.IdEmpresa, Actual.Sucursal.Id)
      End Function
#End Region

#Region "Metodos Privados"

      Public Sub Inserta(ByVal entidad As Entidades.Entidad)
         Me.EjecutaSP(DirectCast(entidad, Entidades.ParametroSucursal), TipoSP._I)
      End Sub

      Public Sub Actualiza(ByVal entidad As Entidades.Entidad)
         Me.EjecutaSP(DirectCast(entidad, Entidades.ParametroSucursal), TipoSP._U)
      End Sub

      Public Sub Merge(ByVal entidad As Entidades.Entidad)
         Me.EjecutaSP(DirectCast(entidad, Entidades.ParametroSucursal), TipoSP._M)
      End Sub

      Public Sub Borra(ByVal entidad As Entidades.Entidad)
         Me.EjecutaSP(DirectCast(entidad, Entidades.ParametroSucursal), TipoSP._D)
      End Sub

      Private Sub EjecutaSP(ByVal en As Entidades.ParametroSucursal, ByVal tipo As TipoSP)

         Dim sqlC = New SqlServer.ParametrosSucursales(da)
         Select Case tipo
            Case TipoSP._I
               sqlC.ParametrosSucursales_I(en.IdEmpresa, en.IdSucursal, en.IdParametro, en.ValorParametro)
            Case TipoSP._U
               sqlC.ParametrosSucursales_U(en.IdEmpresa, en.IdSucursal, en.IdParametro, en.ValorParametro)
            Case TipoSP._M
               sqlC.ParametrosSucursales_M(en.IdEmpresa, en.IdSucursal, en.IdParametro, en.ValorParametro)
            Case TipoSP._D
               sqlC.ParametrosSucursales_D(en.IdEmpresa, en.IdSucursal, en.IdParametro)
         End Select

      End Sub

      Private Sub CargarUno(ByVal o As Entidades.ParametroSucursal, ByVal dr As DataRow)
         With o
            .IdEmpresa = Integer.Parse(dr(Entidades.ParametroSucursal.Columnas.IdEmpresa.ToString()).ToString())
            .IdSucursal = Integer.Parse(dr(Entidades.ParametroSucursal.Columnas.IdSucursal.ToString()).ToString())
            .IdParametro = dr(Entidades.ParametroSucursal.Columnas.IdParametro.ToString()).ToString()
            .ValorParametro = dr(Entidades.ParametroSucursal.Columnas.ValorParametro.ToString()).ToString()
         End With
      End Sub
#End Region

#Region "Metodos publicos"
      Public Function GetPorCodigo(idParametro As String) As DataTable
         Return New SqlServer.ParametrosSucursales(Me.da).ParametrosSucursales_GA(actual.Sucursal.IdEmpresa, actual.Sucursal.Id, idParametro, False)
      End Function

      Public Function Get1(idParametro As String) As DataTable
         Return Get1(actual.Sucursal.IdEmpresa, actual.Sucursal.Id, idParametro)
      End Function
      Public Function Get1(idEmpresa As Integer, idSucursal As Integer, idParametro As String) As DataTable
         Return New SqlServer.ParametrosSucursales(Me.da).ParametrosSucursales_G1(idEmpresa, idSucursal, idParametro)
      End Function

      Public Function GetUno(idParametro As String) As Entidades.ParametroSucursal
         Return GetUno(actual.Sucursal.IdEmpresa, actual.Sucursal.Id, idParametro, AccionesSiNoExisteRegistro.Vacio)
      End Function
      Public Function GetUno(idEmpresa As Integer, idSucursal As Integer, idParametro As String, accion As AccionesSiNoExisteRegistro) As Entidades.ParametroSucursal
         Dim dt As DataTable = Get1(idEmpresa, idSucursal, idParametro)
         Dim o As Entidades.ParametroSucursal = New Entidades.ParametroSucursal()
         If dt.Rows.Count > 0 Then
            Dim dr As DataRow = dt.Rows(0)
            Me.CargarUno(o, dt.Rows(0))
         Else
            If accion = AccionesSiNoExisteRegistro.Nulo Then
               Return Nothing
            ElseIf accion = AccionesSiNoExisteRegistro.Excepcion Then
               Throw New Exception(String.Format("No se encontró el parámetro {1} en la sucursal {0}", idSucursal, idParametro))
            End If
         End If
         Return o
      End Function

      Public Function GetTodos() As List(Of Entidades.ParametroSucursal)
         Return CargaLista(GetAll(), Sub(o, dr) CargarUno(DirectCast(o, Entidades.ParametroSucursal), dr), Function() New Entidades.ParametroSucursal())
      End Function

      Public Overloads Sub Borrar(idEmpresa As Integer)
         Dim sql As SqlServer.ParametrosSucursales = New SqlServer.ParametrosSucursales(da)
         sql.ParametrosSucursales_D(idEmpresa, idSucursal:=0, idParametro:=String.Empty)
      End Sub

#End Region
   End Class

End Namespace
