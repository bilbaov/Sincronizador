Imports System.Reflection
Imports System.IO

Namespace Eniac.Ayudantes

   Public Class Functions

      Public Shared Function GetCurrentDirectory() As String
         Return Path.GetDirectoryName([Assembly].GetExecutingAssembly().GetModules()(0).FullyQualifiedName)
      End Function

   End Class

End Namespace
