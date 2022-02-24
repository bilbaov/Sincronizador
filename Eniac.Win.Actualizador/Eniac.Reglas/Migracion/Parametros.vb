Option Explicit On
Option Strict On
#Region "Option"
Imports Eniac
#End Region

Namespace Reglas
    Public Class Parametros
        Inherits Base

#Region "Constructores"

        Public Sub New()
            Me.New(Nothing)
        End Sub

        Public Sub New(ByVal accesoDatos As Datos.DataAccess)
            Me.NombreEntidad = Entidades.Parametro.NombreTabla
            If accesoDatos Is Nothing Then
                da = New Datos.DataAccess()
            Else
                da = accesoDatos
            End If
        End Sub

#End Region

        Public Overloads Function GetValorPD(idParametro As String, porDefecto As String) As String
            Return ParametrosCache.Instancia.GetValorPD(idParametro, porDefecto)
        End Function

        Public Overloads Function GetValorPD(idParametro As Entidades.Parametro.Parametros, porDefecto As String) As String
            Return ParametrosCache.Instancia.GetValorPD(idParametro, porDefecto)
        End Function
        Public Sub SetValor(idEmpresa As Integer, idParametro As String, valorParametro As String)
            Try
                da.OpenConection()
                da.BeginTransaction()

                _SetValor(idEmpresa, idParametro, valorParametro)

                da.CommitTransaction()
            Catch
                da.RollbakTransaction()
                Throw
            Finally
                da.CloseConection()
            End Try
        End Sub
      Public Sub _SetValor(idEmpresa As Integer, idParametro As String, valorParametro As String)
         Dim sql = New SqlServer.Parametros(Me.da)
         sql.Parametros_M1(idEmpresa, idParametro, valorParametro)
      End Sub
      Public Function EmpresaPrincipal(pActualiza As Entidades.Actualizador) As Integer
         '-- Parametros.- --
         Dim oEmpresa = New SqlServer.Parametros(da).BuscarEmpresaPrincipal()
         pActualiza.ClienteAct = New List(Of Entidades.EmpresaClienteSucursal)
         '------------------
         For Each rEmp As DataRow In oEmpresa.Rows
            Dim oReg = New Entidades.EmpresaClienteSucursal
            With oReg
               .IdEmpresa = Integer.Parse(rEmp(Entidades.EmpresaClienteSucursal.Columnas.IdEmpresa.ToString()).ToString())
               .NombreEmpresa = rEmp(Entidades.EmpresaClienteSucursal.Columnas.NombreEmpresa.ToString()).ToString()
               .Cliente = Long.Parse(rEmp(Entidades.EmpresaClienteSucursal.Columnas.Cliente.ToString()).ToString())
               .Central = Integer.Parse(rEmp(Entidades.EmpresaClienteSucursal.Columnas.Central.ToString()).ToString())
            End With
            pActualiza.ClienteAct.Add(oReg)
         Next

         Return pActualiza.ClienteAct(0).IdEmpresa
      End Function

      Public Function IsOnline(sURLControl As String) As Boolean
         Dim Url As New Uri(sURLControl)
         Dim oWebReq = Net.WebRequest.Create(Url)
         oWebReq.Timeout = 500
         Dim oResp As Net.WebResponse = Nothing
         Try
            oResp = oWebReq.GetResponse()
            Return True
         Catch ex As Exception
            Return False
         Finally
            If oResp IsNot Nothing Then
               oResp.Close()
            End If
         End Try
      End Function

   End Class

End Namespace
