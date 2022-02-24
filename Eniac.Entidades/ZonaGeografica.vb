<Serializable()> _
Public Class ZonaGeografica
   Inherits Eniac.Entidades.Entidad

   Public Enum Columnas
      IdZonaGeografica
      NombreZonaGeografica
   End Enum

#Region "Campos"

   Private _IdZonaGeografica As String
   Private _NombreZonaGeografica As String

#End Region

#Region "Propiedades"

   Public Property IdZonaGeografica() As String
      Get
         Return _IdZonaGeografica
      End Get
      Set(ByVal value As String)
         _IdZonaGeografica = value
      End Set
   End Property

   Public Property NombreZonaGeografica() As String
      Get
         Return _NombreZonaGeografica
      End Get
      Set(ByVal value As String)
         _NombreZonaGeografica = value
      End Set
   End Property

#End Region

End Class