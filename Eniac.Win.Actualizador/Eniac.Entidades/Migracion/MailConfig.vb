Namespace Entidades
   Public Class MailConfig
      Inherits Entidad

      Public Property ServidorSMTP() As String
      Public Property PuertoSalida() As Integer
      Public Property Direccion() As String
      Public Property UsuarioMail() As String
      Public Property Clave() As String
      Public Property RequiereSSL() As Boolean
      Public Property RequiereAutenticacion() As Boolean

      Public Property CantidadXHora() As Integer
      Public Property CantidadXMinuto() As Integer
      Public Property UtilizaComoPredeterminado() As Boolean

   End Class
End Namespace

