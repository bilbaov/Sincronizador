<Serializable()>
Public Class Sistema
   Inherits Eniac.Entidades.Entidad

   Public Property NombreEmpresa As String = String.Empty
   Public Property FechaVencimiento As DateTime
   Public Property Habilitado As Boolean
   Public Property AvisarAlCliente As Boolean
   Public Property ClaveActual As String
   Public Property CantidadEmpresasContratadas As Integer


   Public Sub ControlaValidezDeFecha(fecha As DateTime)
      If DateDiff(DateInterval.Day, fecha, FechaVencimiento) < 0 Then
         Throw New Exception(String.Format("La fecha es mayor a la validez del sistema, el {0:dd/MM/yyyy}", FechaVencimiento))
      End If
   End Sub

End Class