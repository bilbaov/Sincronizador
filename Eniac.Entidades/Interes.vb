Public Class Interes
   Inherits Eniac.Entidades.Entidad

   Public Enum Columnas
      IdInteres
      NombreInteres
      AdicionalProporcinalDias
      MetodoParaDeterminarRango
   End Enum

   Public Sub New()
      InteresesDias = New List(Of InteresDias)()
   End Sub

#Region "Propiedades"
   Public Property IdInteres() As Integer
   Public Property NombreInteres() As String
   Public Property AdicionalProporcinalDias() As Decimal
   Public Property MetodoParaDeterminarRango As String
   Public Property InteresesDias() As List(Of InteresDias)
#End Region

End Class

Public Enum InteresesMetodoParaDeterminarRangoEnum As Integer
   <Description("Día del Mes")>
   DIAMES = 0
   <Description("Días Corridos Desde Fecha Emisión")>
   DIASCORRIDOSEMISION = 1
   <Description("Días Corridos Desde Fecha Vencimiento")>
   DIASCORRIDOSVENCIMIENTO = 2
   <Description("Día del Mes de Vencimiento")>
   DIAMESVENCIMIENTO = 3
End Enum