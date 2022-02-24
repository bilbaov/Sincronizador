Namespace Entidades
   Public Class Aplicacion
      Inherits Entidad

      Public Sub New()
         MyBase.New()
      End Sub
      Public Enum Columnas
         IdAplicacion
         NombreAplicacion
         IdAplicacionBase
      End Enum

      Private _idAplicacion As String
      Public Property IdAplicacion As String
         Get
            Return Me._idAplicacion
         End Get
         Set(value As String)
            Me._idAplicacion = value
         End Set
      End Property

      Private _nombreAplicacion As String
      Public Property NombreAplicacion As String
         Get
            Return Me._nombreAplicacion
         End Get
         Set(value As String)
            Me._nombreAplicacion = value
         End Set
      End Property

      Private _idAplicacionBase As String
      Public Property IdAplicacionBase As String
         Get
            Return Me._idAplicacionBase
         End Get
         Set(value As String)
            Me._idAplicacionBase = value
         End Set
      End Property

   End Class
End Namespace
