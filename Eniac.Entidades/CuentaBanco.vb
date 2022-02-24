<Serializable()> _
Public Class CuentaBanco
   Inherits Eniac.Entidades.Entidad

   Public Enum Columnas
      IdCuentaBanco
      NombreCuentaBanco
      IdTipoCuenta
      EsPosdatado
      PideCheque
      IdCuentaContable
      IdGrupoCuenta
      IdCentroCosto
   End Enum

#Region "Campos"

   Private _IdCuentaBanco As Integer
   Private _NombreCuentaBanco As String
   Private _IdTipoCuenta As String
   Private _EsPosdatado As Boolean
   Private _PideCheque As Boolean
   Private _IdCuentaContable As Integer
   Private _IdGrupoCuenta As String

#End Region

#Region "Propiedades"

   Public Property IdCuentaBanco() As Integer
      Get
         Return _IdCuentaBanco
      End Get
      Set(ByVal value As Integer)
         _IdCuentaBanco = value
      End Set
   End Property

   Public Property NombreCuentaBanco() As String
      Get
         Return _NombreCuentaBanco
      End Get
      Set(ByVal value As String)
         _NombreCuentaBanco = value
      End Set
   End Property

   Public Property IdTipoCuenta() As String
      Get
         Return _IdTipoCuenta
      End Get
      Set(ByVal value As String)
         _IdTipoCuenta = value
      End Set
   End Property
   Public Property EsPosdatado() As Boolean
      Get
         Return _EsPosdatado
      End Get
      Set(ByVal value As Boolean)
         _EsPosdatado = value
      End Set
   End Property
   Public Property PideCheque() As Boolean
      Get
         Return _PideCheque
      End Get
      Set(ByVal value As Boolean)
         Me._PideCheque = value
      End Set
   End Property
   Public Property IdCuentaContable() As Integer
      Get
         Return _IdCuentaContable
      End Get
      Set(ByVal value As Integer)
         _IdCuentaContable = value
      End Set
   End Property

   Public Property IdGrupoCuenta() As String
      Get
         Return _IdGrupoCuenta
      End Get
      Set(ByVal value As String)
         _IdGrupoCuenta = value
      End Set
   End Property

   Public Property CentroCosto As ContabilidadCentroCosto

   Public ReadOnly Property IdCentroCosto As Integer?
      Get
         If CentroCosto Is Nothing Then Return Nothing
         Return CentroCosto.IdCentroCosto
      End Get
   End Property

   Public ReadOnly Property NombreCentroCosto As String
      Get
         If CentroCosto Is Nothing Then Return String.Empty
         Return CentroCosto.NombreCentroCosto
      End Get
   End Property
#End Region

End Class