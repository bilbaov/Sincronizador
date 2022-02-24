Imports System.ComponentModel

Public Class Publicos

   Public Enum SiteTiendaWeb
      MLA
   End Enum

   Public Enum TiendasWeb As Integer
      TiendaNube = 1
      MercadoLibre = 2
   End Enum

   Public Enum TipoPublicacion
      gold_pro
      gold_premium
      gold_special
      gold
      silver
      bronze
      free
   End Enum

   Public Enum TiposEmpleados
      COMPRADOR
      VENDEDOR
      COBRADOR
      TODOS
   End Enum

   Public Enum AfectaCaja
      TODOS = -1
      NO = 0
      SI = 1
   End Enum

   Public Enum SiNo As Integer
      SI = 1
      NO = 0
   End Enum

   Public Enum SiNoTodos As Integer
      TODOS = 0
      SI = 1
      NO = 2
   End Enum

   Public Enum AndOr
      [And]
      [Or]
   End Enum

   Public Enum Dias As Integer
      Domingo = 0
      Lunes = 1
      Martes = 2
      Miercoles = 3
      Jueves = 4
      Viernes = 5
      Sabado = 6
      Otros = 7
   End Enum

   Public Enum OperadoresRelacionales
      <Description("MAYOR IGUAL QUE")> MAYORIGUAL
      <Description("MENOR IGUAL QUE")> MENORIGUAL
      <Description("MAYOR QUE")> MAYOR
      <Description("MENOR QUE")> MENOR
      <Description("IGUAL QUE")> IGUAL
      <Description("DISTINTO QUE")> DISTINTO
   End Enum

   Public Enum SiNoDefault As Integer
      <Description("Por Defecto")> [DEFAULT] = 0
      <Description("Si")> SI = 1
      <Description("No")> NO = 2
   End Enum

   Public Enum InformesCtaCte
      <Description("Informe de Cta. Cte. de Clientes")> CTACTE
      <Description("Informe de Cta. Cte. - Debe Haber")> CTACTEDH
   End Enum


   Private Shared _driverBase As String = "C:\"
   Public Shared Property DriverBase() As String
      Get
         Return _driverBase
      End Get
      Set(ByVal value As String)
         _driverBase = value
      End Set
   End Property

   Public Shared ReadOnly Property CarpetaEniac() As String
      Get
         Return DriverBase + "Eniac\"
      End Get
   End Property

   Public Shared ReadOnly Property CarpetaLOGs() As String
      Get
         Return DriverBase & "Eniac\" & My.Application.Info.ProductName & "\LOGs\"
      End Get
   End Property

   Public Enum ComparativoDiario_CampoTotalizar
      <Description("Cantidad")> CANTIDAD
      <Description("Importe")> IMPORTE
   End Enum
   Public Enum ComparativoDiario_ColumnasMostrar
      <Description("Ventas y Devoluciones")> SEPARADO
      <Description("Consolidado")> CONSOLIDADO
   End Enum

   Public Enum FormatoLike
      <Description("Contiene")> CONTIENE
      <Description("Comienza Con")> COMIENZA
      <Description("Finaliza Con")> FINALIZA
   End Enum


   Public Enum SemaforoCalculoMuestra
      Rentabilidad
      <Description("Contribución Marginal")> ContribucionMarginal
   End Enum

   Public Enum FacturacionOrdenesDeTitulo
      <Description("Vendedor - Caja")> VENDEDORCAJA
      <Description("Caja - Vendedor")> CAJAVENDEDOR
   End Enum

   Public Enum FacturacionOrdenesDeColor
      <Description("Vendedor - Caja")> VENDEDORCAJA
      <Description("Caja - Vendedor")> CAJAVENDEDOR
   End Enum

   Public Enum PeriodosCalendarios
      <Description("Día")> Dia
      <Description("Mes")> Mes
      <Description("Año")> Anio
   End Enum

End Class