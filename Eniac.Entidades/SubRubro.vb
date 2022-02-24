<Serializable()> _
Public Class SubRubro
   Inherits Eniac.Entidades.Entidad

   Public Const NombreTabla As String = "SubRubros"

   Public Enum Columnas
      IdSubRubro
      NombreSubRubro
      DescuentoRecargoPorc1
      IdRubro
      FechaActualizacionWeb
      UnidHasta1
      UnidHasta2
      UnidHasta3
      UnidHasta4
      UnidHasta5
      UHPorc1
      UHPorc2
      UHPorc3
      UHPorc4
      UHPorc5
      DescuentoRecargoPorc2
      IdSubRubroTiendaNube
      IdSubRubroMercadoLibre
      CodigoExportacion
   End Enum

#Region "Campos"

   Private _idRubro As Integer
   Private _idSubRubro As Integer
   Private _nombreSubRubro As String

#End Region

#Region "Propiedades"

   Public Property CodigoExportacion As String
   Public Property IdRubro() As Integer
      Get
         Return _idRubro
      End Get
      Set(ByVal value As Integer)
         _idRubro = value
      End Set
   End Property

   Public Property NombreRubro As String

   Public Property IdSubRubro() As Integer
      Get
         Return _idSubRubro
      End Get
      Set(ByVal value As Integer)
         _idSubRubro = value
      End Set
   End Property
   Public Property NombreSubRubro() As String
      Get
         Return _nombreSubRubro
      End Get
      Set(ByVal value As String)
         _nombreSubRubro = value
      End Set
   End Property
   Public Property DescuentoRecargoPorc1() As Decimal

   Public Property UnidHasta1 As Decimal
   Public Property UnidHasta2 As Decimal
   Public Property UnidHasta3 As Decimal
   Public Property UnidHasta4 As Decimal
   Public Property UnidHasta5 As Decimal
   Public Property UHPorc1 As Decimal
   Public Property UHPorc2 As Decimal
   Public Property UHPorc3 As Decimal
   Public Property UHPorc4 As Decimal
   Public Property UHPorc5 As Decimal
   Public Property IdSubRubroTiendaNube As String
   Public Property IdSubRubroMercadoLibre As String
   Public Property DescuentoRecargoPorc2() As Decimal

   Private _subRubroComisionPorDescuento As List(Of SubRubroComisionPorDescuento)
   Public Property SubRubroComisionPorDescuento As List(Of SubRubroComisionPorDescuento)
      Get
         If _subRubroComisionPorDescuento Is Nothing Then _subRubroComisionPorDescuento = New List(Of SubRubroComisionPorDescuento)()
         Return _subRubroComisionPorDescuento
      End Get
      Set(value As List(Of SubRubroComisionPorDescuento))
         _subRubroComisionPorDescuento = value
      End Set
   End Property

#End Region

#Region "Propiedades ReadOnly"
   Public ReadOnly Property NombreRubroSubRubro As String
      Get
         If String.IsNullOrWhiteSpace(NombreRubro) Then Return NombreSubRubro
         Return String.Format("{0} - {1}", NombreRubro, NombreSubRubro)
      End Get
   End Property
#End Region

End Class