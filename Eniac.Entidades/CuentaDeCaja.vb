<Serializable()>
Public Class CuentaDeCaja
   Inherits Eniac.Entidades.Entidad

   Public Enum Columnas
      IdCuentaCaja
      NombreCuentaCaja
      IdTipoCuenta
      EsPosdatado
      PideCheque
      IdGrupoCuenta
      IdCuentaContable
      IdCentroCosto
      GeneraContabilidad
   End Enum

#Region "Propiedades"

   Public Property IdCuentaContable() As Long

   Public Property IdCuentaCaja() As Integer

   Public Property NombreCuentaCaja() As String

   Public Property IdTipoCuenta() As String

   Public Property EsPosdatado() As Boolean

   Public Property PideCheque() As Boolean

   Public Property IdGrupoCuenta() As String

   Public Property CentroCosto As ContabilidadCentroCosto
   Public Property GeneraContabilidad As Boolean

#Region "Propiedades ReadOnly"
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

#End Region

   Public Function GetCopia() As Entidades.CuentaDeCaja
      Dim copia As Entidades.CuentaDeCaja = New Entidades.CuentaDeCaja()
      With copia
         .EsPosdatado = Me._EsPosdatado
         .IdCuentaCaja = Me._IdCuentaCaja
         .IdGrupoCuenta = Me._IdGrupoCuenta
         .IdSucursal = Me.IdSucursal
         .IdTipoCuenta = Me._IdTipoCuenta
         .NombreCuentaCaja = Me._NombreCuentaCaja
         .Password = Me.Password
         .PideCheque = Me._PideCheque
         .Usuario = Me.Usuario

         .IdCuentaContable = Me._IdCuentaContable

         .CentroCosto = CentroCosto.Clonar(CentroCosto)
         .GeneraContabilidad = Me.GeneraContabilidad
      End With
      Return copia
   End Function

End Class