<Serializable()> _
Public Class SubSubRubro
   Inherits Eniac.Entidades.Entidad

   Public Const NombreTabla As String = "SubSubRubros"
   Public Enum Columnas
      IdSubSubRubro
      NombreSubSubRubro
      IdSubRubro
      FechaActualizacionWeb
      IdSubSubRubroTiendaNube
      IdSubSubRubroMercadoLibre
   End Enum

   Public Property IdRubro As Integer
   Public Property IdSubRubro As Integer
   Public Property IdSubSubRubro As Integer
   Public Property NombreSubSubRubro As String

   Public Property NombreRubro As String
   Public Property NombreSubRubro As String
   Public Property IdSubSubRubroTiendaNube As String
   Public Property IdSubSubRubroMercadoLibre As String

   Public ReadOnly Property NombreConcatenado As String
      Get
         Return String.Format("{0} - {1}", NombreRubro, NombreSubRubro)
      End Get
   End Property

End Class