<Serializable()>
Public Class ListaDePrecios
   Inherits Eniac.Entidades.Entidad

   Public Enum Columnas
      IdListaPrecios
      NombreListaPrecios
      FechaVigencia
      DescuentoRecargoPorc
      Orden
      Activa
      NombreCortoListaPrecios
      IdFormasPago
      PublicarEnWeb
      PermiteUtilidadEnCero
   End Enum

   Private _idListaPrecios As Integer
   Public Property IdListaPrecios() As Integer
      Get
         Return _idListaPrecios
      End Get
      Set(ByVal value As Integer)
         _idListaPrecios = value
      End Set
   End Property

   Private _nombreListaPrecios As String
   Public Property NombreListaPrecios() As String
      Get
         Return _nombreListaPrecios
      End Get
      Set(ByVal value As String)
         _nombreListaPrecios = value
      End Set
   End Property

   Private _fechaVigencia As DateTime
   Public Property FechaVigencia() As DateTime
      Get
         Return _fechaVigencia
      End Get
      Set(ByVal value As DateTime)
         _fechaVigencia = value
      End Set
   End Property

   Private _idListaPreciosCopiar As Integer
   Public Property IdListaPreciosCopiar() As Integer
      Get
         Return _idListaPreciosCopiar
      End Get
      Set(ByVal value As Integer)
         _idListaPreciosCopiar = value
      End Set
   End Property

   Private _descuentoRecargoPorc As Decimal = 0
   Public Property DescuentoRecargoPorc() As Decimal
      Get
         Return Me._descuentoRecargoPorc
      End Get
      Set(ByVal value As Decimal)
         Me._descuentoRecargoPorc = value
      End Set
   End Property

   Private _Orden As Integer
   Public Property Orden() As Integer
      Get
         Return _Orden
      End Get
      Set(ByVal value As Integer)
         _Orden = value
      End Set
   End Property

   Private _Activa As Boolean
   Public Property Activa() As Boolean
      Get
         Return _Activa
      End Get
      Set(ByVal value As Boolean)
         _Activa = value
      End Set
   End Property
   Private _PublicarEnWeb As Boolean
   Public Property PublicarEnWeb() As Boolean
      Get
         Return _PublicarEnWeb
      End Get
      Set(ByVal value As Boolean)
         _PublicarEnWeb = value
      End Set
   End Property
   Private _PermiteUtilidadEnCero As Boolean
   Public Property PermiteUtilidadEnCero() As Boolean
      Get
         Return _PermiteUtilidadEnCero
      End Get
      Set(ByVal value As Boolean)
         _PermiteUtilidadEnCero = value
      End Set
   End Property
   Public Property NombreCortoListaPrecios As String

   Public Property FormaPago As VentaFormaPago

#Region "Propiedades ReadOnly"
   Public ReadOnly Property IdFormasPago As Integer
      Get
         If FormaPago Is Nothing Then Return 0
         Return FormaPago.IdFormasPago
      End Get
   End Property

   Public ReadOnly Property DescripcionFormasPago As String
      Get
         If FormaPago Is Nothing Then Return String.Empty
         Return FormaPago.DescripcionFormasPago
      End Get
   End Property
#End Region

   Public Function GetCopia() As Entidades.ListaDePrecios
      Dim copia As Entidades.ListaDePrecios = New Entidades.ListaDePrecios()
      With copia
         .FechaVigencia = Me.FechaVigencia
         .IdListaPrecios = Me.IdListaPrecios
         .IdSucursal = Me.IdSucursal
         .NombreListaPrecios = Me.NombreListaPrecios
         .DescuentoRecargoPorc = Me.DescuentoRecargoPorc
         .Orden = Me.Orden
         .Activa = Me.Activa
         .NombreCortoListaPrecios = NombreCortoListaPrecios
         If Me.FormaPago IsNot Nothing Then
            .FormaPago = .Clonar(Me.FormaPago)
         End If
         .Password = Me.Password
         .Usuario = Me.Usuario
      End With

      Return copia
   End Function

End Class