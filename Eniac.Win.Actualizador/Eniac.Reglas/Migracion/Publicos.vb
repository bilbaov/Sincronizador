Namespace Reglas
   Public Class Publicos
      Public Shared ReadOnly Property NombreEmpresa() As String
         Get
            Return New Reglas.Parametros().GetValorPD("NombreEmpresa", String.Empty)
         End Get
      End Property
      Public Shared ReadOnly Property CodigoClienteSinergia() As String
         Get
            Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.CODIGOCLIENTESINERGIA.ToString(), "")
         End Get
      End Property

   End Class

End Namespace
