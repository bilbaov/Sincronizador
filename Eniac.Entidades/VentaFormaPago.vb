Imports System.ComponentModel

<Serializable()>
Public Class VentaFormaPago
   Inherits Eniac.Entidades.Entidad

   Public Const NombreTabla As String = "VentasFormasPago"

   Public Enum Columnas
      IdFormasPago
      DescripcionFormasPago
      Dias
      EsTarjeta
      OrdenVentas
      OrdenCompras
      OrdenFichas
      CantidadCuotas
      DiasPrimerCuota
      FechaBaseProximaCuota
      ExcluyeSabados
      ExcluyeDomingos
      ExcluyeFeriados
      Porcentaje

      RequierePesos
      RequiereDolares
      RequiereTickets
      RequiereTransferencia
      RequiereCheques
      RequiereTarjetaDebito
      RequiereTarjetaCredito
      RequiereTarjetaCredito1Pago

   End Enum

   Public Enum ProximaCuota
      <Description("Según Fecha Teórica")> TEORICA
      <Description("Según Fecha Real")> REAL
   End Enum
   Public Enum ValoresFijosIdFormasPago As Integer
      <Description("(Selección Multiple)")> SeleccionMultiple = -1
      <Description("(Todos)")> Todos = -2
   End Enum

   Public Property IdFormasPago() As Integer

   Private _descripcionFormasPago As String
   Public Property DescripcionFormasPago() As String
      Get
         Return Me._descripcionFormasPago
      End Get
      Set(ByVal value As String)
         If value Is Nothing Then value = String.Empty
         If value.Length <= 50 Then
            Me._descripcionFormasPago = value
         Else
            Throw New Exception("La descripción de la Forma de Pago no puede tener mas de 50 caracteres")
         End If
      End Set
   End Property

   Public Property Dias As Integer
   Public Property EsTarjeta As Boolean
   Public Property OrdenVentas As Integer
   Public Property OrdenCompras As Integer
   Public Property OrdenFichas As Integer
   Public Property CantidadCuotas As Integer
   Public Property DiasPrimerCuota As Integer
   Public Property FechaBaseProximaCuota As ProximaCuota
   Public Property ExcluyeSabados As Boolean
   Public Property ExcluyeDomingos As Boolean
   Public Property ExcluyeFeriados As Boolean
   Public Property Porcentaje As Decimal

   Public Property RequierePesos As Boolean
   Public Property RequiereDolares As Boolean
   Public Property RequiereTickets As Boolean
   Public Property RequiereTransferencia As Boolean
   Public Property RequiereCheques As Boolean
   Public Property RequiereTarjetaDebito As Boolean
   Public Property RequiereTarjetaCredito As Boolean
   Public Property RequiereTarjetaCredito1Pago As Boolean

   Public ReadOnly Property RequiereAlgo As Boolean
      Get
         Return RequierePesos Or RequiereDolares Or RequiereTickets Or
                RequiereTransferencia Or RequiereCheques Or
                RequiereTarjetaDebito Or RequiereTarjetaCredito Or RequiereTarjetaCredito1Pago
      End Get
   End Property

End Class