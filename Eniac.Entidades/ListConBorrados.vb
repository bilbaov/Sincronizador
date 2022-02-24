Public Class ListConBorrados(Of T)
   Inherits List(Of T)

   Public Sub New()
      Borrados = New List(Of T)()
   End Sub

   Public Sub New(lista As List(Of T))
      Me.New()
      Me.AddRange(lista)
   End Sub

   Public Property Borrados As List(Of T)

   Public Shadows Function Remove(item As T) As Boolean
      Borrados.Add(item)
      Return MyBase.Remove(item)
   End Function

End Class