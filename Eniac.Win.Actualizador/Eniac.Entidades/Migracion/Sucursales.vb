Imports System.ComponentModel

Namespace Entidades
   Public Class Sucursales
      Inherits Entidad

      Public Const NombreTabla As String = "Sucursales"

      Public Enum Columnas
         IdSucursal
         Nombre
         Direccion
         IdLocalidad
         Telefono
         Correo
         FechaInicioActiv
         EstoyAca
         SoyLaCentral
         Id
         IdSucursalAsociada
         ColorSucursal
         LogoSucursal
         DireccionComercial
         IdLocalidadComercial
         RedesSociales
         IdSucursalAsociadaPrecios
         PublicarEnWeb
         IdEmpresa
      End Enum

      Public Enum ValoresFijosIdSucursal As Integer
         <Description("(Selección Multiple)")> SeleccionMultiple = -1
         <Description("(Todos)")> Todos = -2
      End Enum

#Region "Campos"

#End Region

#Region "Propiedades"

      Public Property Id() As Int32
      Public Property Nombre() As String
      Public Property Direccion() As String
      Public Property Telefono() As String
      Public Property Correo() As String
      Public Property FechaInicioActiv() As DateTime
      Public Property EstoyAca() As Boolean
      Public Property SoyLaCentral() As Boolean
      Public Property IdSucursalAsociada() As Int32
      Public Property ColorSucursal() As Int32
      Public Property LogoSucursal() As System.Drawing.Image
      Public Property DireccionComercial() As String
      Public Property RedesSociales() As String
      Public Property IdSucursalAsociadaPrecios As Integer
      Public Property PublicarEnWeb As Boolean
      Private _Empresa As Entidades.Empresa
      Public Property Empresa As Entidades.Empresa
         Get
            If _Empresa Is Nothing Then _Empresa = New Empresa()
            Return _Empresa
         End Get
         Set(value As Entidades.Empresa)
            _Empresa = value
         End Set
      End Property

#End Region

#Region "Propiedades ReadOnly"
      Public Property IdEmpresa As Integer
         Get
            Return Empresa.IdEmpresa
         End Get
         Set(value As Integer)
            Empresa.IdEmpresa = value
         End Set
      End Property

      Public ReadOnly Property NombreEmpresa As String
         Get
            Return Empresa.NombreEmpresa
         End Get
      End Property
#End Region

#Region "Overrides"
      Public Overrides Function ToString() As String
         Return Me.Nombre & " - " & Me.Id.ToString()
      End Function
#End Region

   End Class

End Namespace
