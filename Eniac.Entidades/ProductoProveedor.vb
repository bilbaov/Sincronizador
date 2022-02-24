<Serializable()>
Public Class ProductoProveedor
   Inherits Eniac.Entidades.Entidad

   Public Const NombreTabla As String = "ProductosProveedores"

   Public Enum Columnas
      IdProveedor
      IdProducto
      CodigoProductoProveedor
      UltimoPrecioCompra
      UltimaFechaCompra
      UltimoPrecioFabrica
      DescuentoRecargoPorc1
      DescuentoRecargoPorc2
      DescuentoRecargoPorc3
      DescuentoRecargoPorc4
      DescuentoRecargoPorc5
      DescuentoRecargoPorc6
   End Enum

#Region "Campos"

   Private _idProveedor As Long = 0
   Private _idProducto As String = ""
   Private _codigoProductoProveedor As String = ""
   Private _ultimoPrecioCompra As Decimal = 0
   Private _ultimaFechaCompra As Date = Date.Now

   Private _ultimoPrecioFabrica As Decimal = 0
   Private _descuentoRecargoPorc1 As Decimal = 0
   Private _descuentoRecargoPorc2 As Decimal = 0
   Private _descuentoRecargoPorc3 As Decimal = 0
   Private _descuentoRecargoPorc4 As Decimal = 0
   Private _descuentoRecargoPorc5 As Decimal = 0
   Private _descuentoRecargoPorc6 As Decimal = 0
#End Region

#Region "Propiedades"

   Public Property IdProveedor() As Long
      Get
         Return Me._idProveedor
      End Get
      Set(ByVal value As Long)
         Me._idProveedor = value
      End Set
   End Property

   Public Property IdProducto() As String
      Get
         Return Me._idProducto
      End Get
      Set(ByVal value As String)
         Me._idProducto = value.Trim()
      End Set
   End Property

   Public Property CodigoProductoProveedor() As String
      Get
         Return Me._codigoProductoProveedor
      End Get
      Set(ByVal value As String)
         Me._codigoProductoProveedor = value.Trim()
      End Set
   End Property

   Public Property UltimoPrecioCompra() As Decimal
      Get
         Return Me._ultimoPrecioCompra
      End Get
      Set(ByVal value As Decimal)
         Me._ultimoPrecioCompra = value
      End Set
   End Property

   Public Property UltimaFechaCompra() As Date
      Get
         Return Me._ultimaFechaCompra
      End Get
      Set(ByVal value As Date)
         Me._ultimaFechaCompra = value
      End Set
   End Property


   Public Property UltimoPrecioFabrica() As Decimal
      Get
         Return _ultimoPrecioFabrica
      End Get
      Set(ByVal value As Decimal)
         _ultimoPrecioFabrica = value
      End Set
   End Property

   Public Property DescuentoRecargoPorc1() As Decimal
      Get
         Return _descuentoRecargoPorc1
      End Get
      Set(ByVal value As Decimal)
         _descuentoRecargoPorc1 = value
      End Set
   End Property

   Public Property DescuentoRecargoPorc2() As Decimal
      Get
         Return _descuentoRecargoPorc2
      End Get
      Set(ByVal value As Decimal)
         _descuentoRecargoPorc2 = value
      End Set
   End Property

   Public Property DescuentoRecargoPorc3() As Decimal
      Get
         Return _descuentoRecargoPorc3
      End Get
      Set(ByVal value As Decimal)
         _descuentoRecargoPorc3 = value
      End Set
   End Property

   Public Property DescuentoRecargoPorc4() As Decimal
      Get
         Return _descuentoRecargoPorc4
      End Get
      Set(ByVal value As Decimal)
         _descuentoRecargoPorc4 = value
      End Set
   End Property

   Public Property DescuentoRecargoPorc5() As Decimal
      Get
         Return _descuentoRecargoPorc5
      End Get
      Set(ByVal value As Decimal)
         _descuentoRecargoPorc5 = value
      End Set
   End Property

   Public Property DescuentoRecargoPorc6() As Decimal
      Get
         Return _descuentoRecargoPorc6
      End Get
      Set(ByVal value As Decimal)
         _descuentoRecargoPorc6 = value
      End Set
   End Property

#End Region

End Class