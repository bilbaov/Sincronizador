Namespace Entidades
   Public Class Usuarios
      Inherits Entidades.Entidad
      Public Class Actual
         Public Shared Property Sistema As String
         Public Shared Property Nombre() As String
         Public Shared Property Pwd() As String
         Public Shared Property NivelAutorizacion As Short = 1
         Public Shared Property CurrentUICulture As String
         Public Shared Property Sucursal() As Sucursales
         Public Shared Property Empresa() As String
         Public Shared Property NombreLogo() As String
         Public Shared Property Logo() As System.Drawing.Image
         Public Shared Property MailConfig() As MailConfig
      End Class
   End Class

End Namespace
