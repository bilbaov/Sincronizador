Namespace Entidades
   ''' <summary>
   ''' Entidad de Empresas Buscador.-
   ''' </summary>
   Public Class EmpresaBuscar
      Public Enum Columnas
         Aplicacion
         VersionActual
         Base
      End Enum

      Public Property Aplicacion As String            '-- Aplicacion de Empresa.- --
      Public Property VersionActual As String         '-- Version Actual de Empresa.- --
      Public Property VersionNueva As System.Version  '-- Version Nueva de Empresa.- --
      Public Property Base As String                  '-- Base de Empresa.- --

   End Class

End Namespace
