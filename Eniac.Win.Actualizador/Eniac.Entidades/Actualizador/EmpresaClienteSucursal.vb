Namespace Entidades
   ''' <summary>
   ''' Entidad de Empresad Clientes Sucursales.-
   ''' </summary>
   Public Class EmpresaClienteSucursal
      Public Enum Columnas
         IdEmpresa
         NombreEmpresa
         Cliente
         Central
      End Enum

      Public Property IdEmpresa As Integer                '-- Id de Empresa .- --
      Public Property NombreEmpresa As String             '-- Nombre de Empresa.- --
      Public Property Cliente As Long                     '-- Codigo de Cliente Siga.- --
      Public Property Central As Integer                  '-- Soy la Central.- --
   End Class

End Namespace




