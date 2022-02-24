Public Class ProductoNroSerie
   Inherits Eniac.Entidades.Entidad

   Public Enum Columnas
      IdProducto
      NroSerie
      IdSucursal
      Vendido
      OrdenVenta
      OrdenCompra
      FechaDevolucionVenta
   End Enum

#Region "Campos"
   Private _tipoComprobante As Entidades.TipoComprobante
   Private _letra As String
   Private _centroEmisor As Short
   Private _numero As Long
   Private _proveedor As Entidades.Proveedor

#End Region
#Region "Propiedades"

   Private _producto As Entidades.Producto
   Public Property Producto() As Entidades.Producto
      Get
         If Me._producto Is Nothing Then
            Me._producto = New Entidades.Producto()
         End If
         Return _producto
      End Get
      Set(ByVal value As Entidades.Producto)
         _producto = value
      End Set
   End Property

   Private _sucursal As Entidades.Sucursal
   Public Property Sucursal() As Entidades.Sucursal
      Get
         If Me._sucursal Is Nothing Then
            Me._sucursal = New Entidades.Sucursal()
         End If
         Return _sucursal
      End Get
      Set(ByVal value As Entidades.Sucursal)
         _sucursal = value
      End Set
   End Property

   Private _NroSerie As String
   Public Property NroSerie() As String
      Get
         Return _NroSerie
      End Get
      Set(ByVal value As String)
         _NroSerie = value
      End Set
   End Property

   Private _vendido As Boolean
   Public Property Vendido() As Boolean
      Get
         Return _vendido
      End Get
      Set(ByVal value As Boolean)
         _vendido = value
      End Set
   End Property
   Public Property CentroEmisor() As Short
      Get
         Return _centroEmisor
      End Get
      Set(ByVal value As Short)
         _centroEmisor = value
      End Set
   End Property
   Public Property Letra() As String
      Get
         Return _letra
      End Get
      Set(ByVal value As String)
         _letra = value
      End Set
   End Property

   Public Property Proveedor() As Entidades.Proveedor
      Get
         If Me._proveedor Is Nothing Then
            Me._proveedor = New Entidades.Proveedor()

         End If
         Return Me._proveedor
      End Get
      Set(ByVal value As Entidades.Proveedor)
         Me._proveedor = value
      End Set
   End Property
   Public Property TipoComprobante() As Entidades.TipoComprobante
      Get
         If Me._tipoComprobante Is Nothing Then
            Me._tipoComprobante = New Entidades.TipoComprobante()
         End If
         Return _tipoComprobante
      End Get
      Set(ByVal value As Entidades.TipoComprobante)
         _tipoComprobante = value
      End Set
   End Property
   Public Property NumeroComprobante() As Long
      Get
         Return Me._numero
      End Get
      Set(ByVal value As Long)
         Me._numero = value
      End Set
   End Property
   Public Property OrdenVenta As Integer?
   Public Property OrdenCompra As Integer?
   Public Property FechaDevolucionVenta As Date?

#End Region

   Public Function GetCopia() As Entidades.ProductoNroSerie
      Dim copia As Entidades.ProductoNroSerie = New Entidades.ProductoNroSerie()
      With copia
         .CentroEmisor = Me._centroEmisor
         .IdSucursal = Me.IdSucursal
         .Letra = Me._letra
         .NroSerie = Me._NroSerie
         .NumeroComprobante = Me.NumeroComprobante
         .Password = Me.Password
         .Producto = Me._producto.GetCopia()
         .Proveedor = Me._proveedor.GetCopia()
         .Sucursal = Me._sucursal.GetCopia()
         .TipoComprobante = Me._tipoComprobante.GetCopia()
         .Usuario = Me.Usuario
         .Vendido = Me._vendido
         .OrdenVenta = _OrdenVenta
         .OrdenCompra = _OrdenCompra
         .FechaDevolucionVenta = _FechaDevolucionVenta
      End With
      Return copia
   End Function

End Class
