Namespace Extensions
   Public Module StringExtensions
      ''' <summary>
      ''' Trunca la longitud de una cadena de caracteres a un máximo suminstrado.
      ''' </summary>
      ''' <param name="value">String a truncar</param>
      ''' <param name="maxLength">Máximo de caracteres</param>
      ''' <returns>Cadena de caracteres truncada al ancho suministrado.</returns>
      ''' <remarks></remarks>
      <Extension()>
      Public Function Truncar(value As String, maxLength As Integer) As String
         If String.IsNullOrWhiteSpace(value) Then Return String.Empty
         Return value.Substring(0, Math.Min(value.Length, maxLength)).Trim()
      End Function
   End Module
End Namespace