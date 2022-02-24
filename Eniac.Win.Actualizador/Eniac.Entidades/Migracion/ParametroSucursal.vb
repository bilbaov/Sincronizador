Namespace Entidades
   Public Class ParametroSucursal
      Inherits Entidad
      Public Const NombreTabla As String = "ParametrosSucursales"
      Public Enum Columnas
         IdSucursal
         IdParametro
         ValorParametro
         IdEmpresa

      End Enum
      Public Property IdEmpresa As Integer
      Public Property IdParametro As String
      Public Property ValorParametro As String
   End Class

End Namespace
