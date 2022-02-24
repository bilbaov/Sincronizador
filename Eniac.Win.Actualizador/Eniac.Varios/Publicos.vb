Public Class Publicos
   Public Shared ReadOnly Property CodigoClienteSinergia() As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.CODIGOCLIENTESINERGIA.ToString(), "")
      End Get
   End Property

   ''' <summary>
   ''' Crea una Instancia de un Objeto en base a su nombre.-
   ''' </summary>
   ''' <param name="nombreObjecto">Nombre del Objeto</param>
   ''' <returns></returns>
   Public Shared ReadOnly Property CrearUcDash(nombreObjecto As String) As ucBaseSistema
      Get
         Try
            If Not String.IsNullOrWhiteSpace(nombreObjecto) Then
               Dim assemblyName As String = String.Empty
               Dim className As String
               Dim splitController = nombreObjecto.Split(":"c)
               If splitController.Length > 1 Then
                  assemblyName = splitController(0)
                  className = splitController(1)
               Else
                  If splitController(0).Contains(".") Then
                     Dim posicionUltimoPunto = splitController(0).LastIndexOf("."c)
                     assemblyName = splitController(0).Substring(0, posicionUltimoPunto)
                  End If
                  className = splitController(0)
               End If

               Dim assembly As Reflection.Assembly = Nothing
               If Not String.IsNullOrWhiteSpace(assemblyName) Then
                  Try
                     assembly = AppDomain.CurrentDomain.GetAssemblies().SingleOrDefault(Function(a) a.GetName().Name = assemblyName)
                  Catch ex As Exception
                     assembly = Nothing
                  End Try
               End If
               If assembly Is Nothing Then
                  assembly = Reflection.Assembly.GetExecutingAssembly()
               End If

               If Not className.Contains(".") Then
                  className = String.Concat(assembly.GetName().Name, ".", className)
               End If

               Dim type = assembly.GetType(className)
               Dim obj = Activator.CreateInstance(type)

               If TypeOf (obj) Is ucBaseSistema Then
                  Return DirectCast(obj, ucBaseSistema)
               End If
            End If
         Catch ex As Exception

         End Try
         Return Nothing
      End Get
   End Property

End Class
