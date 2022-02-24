Imports Eniac

Namespace Reglas
   Public Class Clientes

#Region "Constructores"

      Public Sub New(ByVal accesoDatos As Datos.DataAccess, modo As Entidades.Cliente.ModoClienteProspecto)
         Me.New(accesoDatos)
         'ModoClienteProspecto = modo
         'Me.NombreEntidad = modo.ToString() + "s"
      End Sub

      Public Sub New(modo As Entidades.Cliente.ModoClienteProspecto)
         Me.New()
         'ModoClienteProspecto = modo
         'Me.NombreEntidad = modo.ToString() + "s"
      End Sub

      Public Sub New()
         Me.New(New Datos.DataAccess())
      End Sub

      Public Sub New(ByVal accesoDatos As Datos.DataAccess)
         'ModoClienteProspecto = Entidades.Cliente.ModoClienteProspecto.Cliente
         'Me.NombreEntidad = "Clientes"
         'da = accesoDatos
         '_puedeVerDetalleValoracionEstrellas = New Usuarios().TienePermisos("PuedeVerDetalleEstrellas")
         '_recalculaValoracionesEstrellas = Reglas.Publicos.RecalculaValoracionesEstrellas
      End Sub

#End Region
      Public Function GetUnoPorCodigo(codigoCliente As Long,
                                   Optional incluirFoto As Boolean = False,
                                   Optional soloActivos As Boolean = True) As Entidades.Cliente
         'Dim dt As DataTable = New SqlServer.Clientes(da, ModoClienteProspecto).GetFiltradoPorCodigo(codigoCliente, False, String.Empty, soloActivos, False, actual.NivelAutorizacion, idCategoria:=0)
         Dim oCli As Entidades.Cliente = Nothing
         'If dt.Rows.Count > 0 Then
         '   oCli = New Entidades.Cliente()
         '   Me.CargarUno(oCli, dt.Rows(0), incluirAdjuntos)
         'End If

         Return oCli
      End Function
   End Class



End Namespace
