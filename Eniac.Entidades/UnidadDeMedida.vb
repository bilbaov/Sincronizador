<Serializable()> _
<Description("UnidadesDeMedidas")> _
Public Class UnidadDeMedida
   Inherits Eniac.Entidades.Entidad

#Region "Campos"

   Private _idUnidadDeMedida As String = ""
   Private _nombreUnidadDeMedida As String = ""
   Private _conversionAKilos As Decimal = 0

#End Region

#Region "Propiedades"

   Public Property IdUnidadDeMedida() As String
      Get
         Return Me._idUnidadDeMedida
      End Get
      Set(ByVal value As String)
         Me._idUnidadDeMedida = value
      End Set
   End Property

   Public Property NombreUnidadDeMedida() As String
      Get
         Return _nombreUnidadDeMedida
      End Get
      Set(ByVal value As String)
         _nombreUnidadDeMedida = value
      End Set
   End Property

   Public Property ConversionAKilos() As Decimal
      Get
         Return _conversionAKilos
      End Get
      Set(ByVal value As Decimal)
         _conversionAKilos = value
      End Set
   End Property

   Private _idAFIP As Integer
   ''' <summary>
   ''' Es el Id que se utiliza para identificar una unidad de Medida en el AFIP.
   ''' </summary>
   ''' <value></value>
   ''' <returns></returns>
   ''' <remarks></remarks>
   Public Property IdAFIP() As Integer
      Get
         Return _idAFIP
      End Get
      Set(ByVal value As Integer)
         _idAFIP = value
      End Set
   End Property


#End Region

   Public Function GetCopia() As Entidades.UnidadDeMedida
      Dim copia As Entidades.UnidadDeMedida = New Entidades.UnidadDeMedida()
      With copia
         .ConversionAKilos = Me._conversionAKilos
         .IdSucursal = Me.IdSucursal
         .IdUnidadDeMedida = Me._idUnidadDeMedida
         .NombreUnidadDeMedida = Me._nombreUnidadDeMedida
         .Password = Me.Password
         .Usuario = Me.Usuario
      End With
      Return copia
   End Function

End Class