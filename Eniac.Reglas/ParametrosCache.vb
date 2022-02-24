Option Strict On
Option Explicit On
Option Infer On
''' <summary>
''' Cache de Parámetros
''' </summary>
''' <remarks></remarks>
Public Class ParametrosCache
#Region "Singleton"
   Private Shared _instancia As ParametrosCache = New ParametrosCache()
   ''' <summary>
   ''' Obtiene la instancia única del Cache de Parámetros (ReadOnly/Singleton)
   ''' </summary>
   ''' <returns>Instancia del Cache de Parámetros</returns>
   ''' <remarks></remarks>
   Public Shared ReadOnly Property Instancia As ParametrosCache
      Get
         Return _instancia
      End Get
   End Property
#End Region

   Private dicParametros As Dictionary(Of String, String)
   Private dicCategoriasCargadas As Dictionary(Of String, String)

   Private _UltimosParametrosAccedidos As ComponentModel.BindingList(Of Tuple(Of DateTime, String, String))
   Public ReadOnly Property UltimosParametrosAccedidos As ComponentModel.BindingList(Of Tuple(Of DateTime, String, String))
      Get
         Return _UltimosParametrosAccedidos
      End Get
   End Property

   Private _cantidadMaximaParametrosMonitoreados As Integer
   Public Property CantidadMaximaParametrosMonitoreados() As Integer
      Get
         Return _cantidadMaximaParametrosMonitoreados
      End Get
      Set(ByVal value As Integer)
         Dim prevValue = _cantidadMaximaParametrosMonitoreados
         _cantidadMaximaParametrosMonitoreados = value
         If prevValue > _cantidadMaximaParametrosMonitoreados Then
            AjustarCantidadRegistro()
         End If
      End Set
   End Property


   Private Sub New()
      dicParametros = New Dictionary(Of String, String)()
      dicCategoriasCargadas = New Dictionary(Of String, String)()
      _UltimosParametrosAccedidos = New ComponentModel.BindingList(Of Tuple(Of DateTime, String, String))()
      CantidadMaximaParametrosMonitoreados = 500
   End Sub

#Region "GetValor"
   ''' <summary>
   ''' Obtiene el valor de un parámetro.
   ''' </summary>
   ''' <param name="idParametro">Identificación del parámetro</param>
   ''' <param name="porDefecto">Valor por defecto si no se encuentra el parámetro</param>
   ''' <returns>Valor del parámetro</returns>
   ''' <remarks>Primero se busca en el cache. En caso de no encontrarse en el cache, lo solicita a la base de datos.</remarks>
   Public Function GetValorPD(idParametro As String, porDefecto As String) As String
      Return GetValorPD(actual.Sucursal.IdEmpresa, idParametro, porDefecto)
   End Function
   ''' <summary>
   ''' Obtiene el valor de un parámetro.
   ''' </summary>
   ''' <param name="idEmpresa">Identificación de la empresa</param>
   ''' <param name="idParametro">Identificación del parámetro</param>
   ''' <param name="porDefecto">Valor por defecto si no se encuentra el parámetro</param>
   ''' <returns>Valor del parámetro</returns>
   ''' <remarks>Primero se busca en el cache. En caso de no encontrarse en el cache, lo solicita a la base de datos.</remarks>
   Public Function GetValorPD(idEmpresa As Integer, idParametro As String, porDefecto As String) As String
      idParametro = idParametro.ToUpper()
      If Not dicParametros.ContainsKey(idParametro) Then
         Dim valorParametro As String

         Dim da As New Eniac.Datos.DataAccess()
         Try
            da.OpenConection()

            Dim paramSuc As Entidades.ParametroSucursal
            If actual.Sucursal IsNot Nothing Then
               paramSuc = New ParametrosSucursales(da).GetUno(idEmpresa, actual.Sucursal.Id, idParametro, Base.AccionesSiNoExisteRegistro.Nulo)
            End If
            If paramSuc IsNot Nothing Then
               valorParametro = paramSuc.ValorParametro
            Else
               valorParametro = New SqlServer.Parametros(da).GetValorPD(idEmpresa, idParametro, porDefecto)
            End If
         Finally
            da.CloseConection()
         End Try

         dicParametros.Add(idParametro, valorParametro)
      End If

      Return AddUltimoParametroAccedido(idParametro)
   End Function

   Private Function AddUltimoParametroAccedido(idParametro As String) As String
      Dim valor = dicParametros(idParametro)
      _UltimosParametrosAccedidos.Insert(0, Tuple.Create(DateTime.Now, idParametro, valor))
      AjustarCantidadRegistro()
      Return valor
   End Function

   Private Sub AjustarCantidadRegistro()
      While UltimosParametrosAccedidos.Count > CantidadMaximaParametrosMonitoreados
         UltimosParametrosAccedidos.RemoveAt(UltimosParametrosAccedidos.Count - 1)
      End While
   End Sub

   ''' <summary>
   ''' Obtiene el valor de un parámetro.
   ''' </summary>
   ''' <param name="idParametro">Identificación del parámetro</param>
   ''' <param name="porDefecto">Valor por defecto si no se encuentra el parámetro</param>
   ''' <returns>Valor del parámetro</returns>
   ''' <remarks>Primero se busca en el cache. En caso de no encontrarse en el cache, lo solicita a la base de datos.</remarks>
   Public Function GetValorPD(idParametro As Entidades.Parametro.Parametros, porDefecto As String) As String
      Return Me.GetValorPD(idParametro.ToString(), porDefecto)
   End Function
#End Region

#Region "CargarTodos"
   ''' <summary>
   ''' Cargo los parámetros de todas las categorías.
   ''' </summary>
   ''' <remarks></remarks>
   Public Sub CargarTodos()
      'CargoTodos sin pasar categoría para que cargue todas las categorías.
      CargarTodos(String.Empty)
   End Sub

   ''' <summary>
   ''' Carga los parámetros de una categoría específica en el cache.
   ''' </summary>
   ''' <param name="categoriaParametro">Categoría a cargar</param>
   ''' <remarks>Si se proporciona String.Empty como categoría se cargan todos los parámetros.</remarks>
   Public Sub CargarTodos(categoriaParametro As String)
      'Si está cargando una categoría específica
      If Not String.IsNullOrWhiteSpace(categoriaParametro) Then
         'Y ya tengo cargada dicha categoría
         If dicCategoriasCargadas.ContainsKey(categoriaParametro) Then
            'No la cargo, ya la cargué antes
            Exit Sub
         End If
      End If

      Dim dt As DataTable
      Dim da As New Eniac.Datos.DataAccess()
      Try
         da.OpenConection()
         Dim sql As SqlServer.Parametros = New SqlServer.Parametros(New Eniac.Datos.DataAccess())
         'Recupero los parámetros de la categoría (o todos si está en blanco la categoría)
         dt = sql.Parametros_GA(actual.Sucursal.IdEmpresa, categoriaParametro)
      Finally
         da.CloseConection()
      End Try

      'Para cada registro recuperado
      For Each dr As DataRow In dt.Rows
         Dim id As String = dr("IdParametro").ToString().ToUpper()
         Dim valor As String = dr("ValorParametro").ToString()
         Dim categoria As String = dr("CategoriaParametro").ToString()

         'Veo si ya existe en el diccionario
         If Not dicParametros.ContainsKey(id) Then
            'Si no fue cargado aún lo cargo en el diccionario
            dicParametros.Add(id, valor)
         End If

         'Si carga todas las categorías, veo si la categoría de este parámetro ya fue cargada
         If String.IsNullOrWhiteSpace(categoriaParametro) AndAlso Not dicCategoriasCargadas.ContainsKey(categoria) Then
            'Si no fue cargada, la agrego.
            dicCategoriasCargadas.Add(categoria, categoria)
         End If
      Next

      'Agrego la categoría específica si no está cargada en el diccionario
      If Not String.IsNullOrWhiteSpace(categoriaParametro) AndAlso Not dicCategoriasCargadas.ContainsKey(categoriaParametro) Then
         'No la cargo, ya la cargué antes
         Exit Sub
      End If
   End Sub
#End Region

#Region "LimpiarCache"
   ''' <summary>
   ''' Limpia el cache de parámetros.
   ''' </summary>
   ''' <remarks></remarks>
   Public Sub LimpiarCache()
      'Limpio el cache de los parámetros
      dicParametros.Clear()
      'Limpio las categorías cargadas (si borro todos los parámetros no hay categorías cargadas)
      dicCategoriasCargadas.Clear()
   End Sub

   ''' <summary>
   ''' Quita del cache un parámetro
   ''' </summary>
   ''' <param name="idParametro">Parámetro a quitar</param>
   ''' <remarks></remarks>
   Public Sub LimpiarCache(idParametro As String)
      'Quito el parámetro del cache
      dicParametros.Remove(idParametro.ToUpper())
   End Sub

   ''' <summary>
   ''' Quita del cache un parámetro
   ''' </summary>
   ''' <param name="idParametro">Parámetro a quitar</param>
   ''' <remarks></remarks>
   Public Sub LimpiarCache(idParametro As Entidades.Parametro.Parametros)
      LimpiarCache(idParametro.ToString())
   End Sub
#End Region


   Public Class Editor
#Region "Singleton"
      Private Shared _instancia As ParametrosCache.Editor = New ParametrosCache.Editor()
      ''' <summary>
      ''' Obtiene la instancia única del Cache de Parámetros (ReadOnly/Singleton)
      ''' </summary>
      ''' <returns>Instancia del Cache de Parámetros</returns>
      ''' <remarks></remarks>
      Public Shared ReadOnly Property Instancia As ParametrosCache.Editor
         Get
            Return _instancia
         End Get
      End Property
#End Region

      Private dicParametros As Dictionary(Of String, Entidades.Parametro)

      Private Sub New()
         dicParametros = New Dictionary(Of String, Entidades.Parametro)()
      End Sub

      Public ReadOnly Property CountActualizar As Integer
         Get
            Return dicParametros.Count
         End Get
      End Property

      Public Function Actualizar(idParametro As String, valorParametro As String, categoriaParametro As String, descripcionParametro As String) As Boolean
         Return Actualizar(actual.Sucursal.IdEmpresa, idParametro, valorParametro, categoriaParametro, descripcionParametro)
      End Function

      Public Function Actualizar(idEmpresa As Integer, idParametro As String, valorParametro As String, categoriaParametro As String, descripcionParametro As String) As Boolean
         Return Actualizar(New Entidades.Parametro() With {.IdEmpresa = idEmpresa,
                                                           .IdParametro = idParametro,
                                                           .ValorParametro = valorParametro,
                                                           .CategoriaParametro = categoriaParametro,
                                                           .DescripcionParametro = descripcionParametro})
      End Function

      Public Function Actualizar(parametro As Entidades.Parametro) As Boolean
         If parametro.ValorParametro <> ParametrosCache.Instancia.GetValorPD(parametro.IdParametro, String.Empty) Then
            dicParametros.Add(parametro.IdParametro, parametro)
            Return True
         End If
         Return False
      End Function

      Public Sub Commit()
         Dim pars As Reglas.Parametros = New Reglas.Parametros()
         pars.Actualizar(dicParametros.Values.ToList())
         Clear()
      End Sub

      Public Sub Clear()
         dicParametros.Clear()
      End Sub

   End Class

End Class