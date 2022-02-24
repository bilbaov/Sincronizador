Public Class Parametros
   Inherits Comunes

   Public Sub New(ByVal da As Eniac.Datos.DataAccess)
      MyBase.New(da)
   End Sub

   Public Sub Parametros_I(idEmpresa As Integer,
                           idParametro As String,
                           valorParametro As String,
                           categoriaParametro As String,
                           descripcionParametro As String)
      Dim myQuery As StringBuilder = New StringBuilder()
      With myQuery
         .AppendLine("INSERT INTO Parametros")
         .AppendLine("           (IdEmpresa")
         .AppendLine("           ,IdParametro")
         .AppendLine("           ,ValorParametro")
         .AppendLine("           ,CategoriaParametro")
         .AppendLine("           ,DescripcionParametro")
         .AppendLine(" ) VALUES (")
         .AppendFormatLine("             {0} ", idEmpresa)
         .AppendFormatLine("           ,'{0}'", idParametro)
         .AppendFormatLine("           ,'{0}'", valorParametro)
         .AppendFormatLine("           ,'{0}'", categoriaParametro)
         .AppendFormatLine("           ,'{0}'", descripcionParametro)
         .AppendLine(" )")
      End With

      Me.Execute(myQuery.ToString())
   End Sub

   Public Sub Parametros_U(idEmpresa As Integer,
                           idParametro As String,
                           valorParametro As String,
                           categoriaParametro As String,
                           descripcionParametro As String)
      Dim myQuery As StringBuilder = New StringBuilder()
      With myQuery
         .AppendLine("UPDATE Parametros")
         .AppendFormatLine("   SET DescripcionParametro = '{0}'", descripcionParametro)
         .AppendFormatLine("      ,ValorParametro = '{0}'", valorParametro)
         If Not String.IsNullOrEmpty(categoriaParametro) Then
            .AppendFormatLine("      ,CategoriaParametro = '{0}'", categoriaParametro)
         End If
         .AppendFormatLine(" WHERE IdParametro = '{0}'", idParametro)
         .AppendFormatLine("   AND IdEmpresa = {0}", idEmpresa)
      End With

      Me.Execute(myQuery.ToString())
   End Sub

   Public Sub Parametros_M1(idEmpresa As Integer, idParametro As String, valorParametro As String)
      Dim myQuery As StringBuilder = New StringBuilder()

      With myQuery
         .AppendFormatLine("MERGE INTO Parametros AS P")
         .AppendFormatLine("        USING (SELECT '{0}' AS IdParametro, '{1}' ValorParametro, {2} IdEmpresa) AS PT ON P.IdParametro = PT.IdParametro AND P.IdEmpresa = PT.IdEmpresa", idParametro, valorParametro, idEmpresa)
         .AppendFormatLine("    WHEN MATCHED THEN")
         .AppendFormatLine("        UPDATE SET P.ValorParametro = PT.ValorParametro")
         .AppendFormatLine("    WHEN NOT MATCHED THEN ")
         .AppendFormatLine("        INSERT (   IdEmpresa,    IdParametro,    ValorParametro, DescripcionParametro, CategoriaParametro)")
         .AppendFormatLine("        VALUES (PT.IdEmpresa, PT.IdParametro, PT.ValorParametro, '',                  '')")
         .AppendFormatLine(";")
      End With

      Me.Execute(myQuery.ToString())
   End Sub

   Public Function Parametros_M(idEmpresaOrigen As Integer, idEmpresaDestino As Integer) As Integer
      Dim myQuery As StringBuilder = New StringBuilder()

      With myQuery
         .AppendFormatLine("MERGE INTO Parametros AS P")
         .AppendFormatLine("        USING (SELECT *, {1} AS IdEmpresaDestino FROM Parametros WHERE IdEmpresa = {0}) AS PT ON P.IdParametro = PT.IdParametro AND P.IdEmpresa = PT.IdEmpresaDestino", idEmpresaOrigen, idEmpresaDestino)
         .AppendFormatLine("    WHEN MATCHED THEN")
         .AppendFormatLine("        UPDATE SET P.{0} = PT.IdEmpresaDestino", Entidades.Parametro.Columnas.IdEmpresa.ToString())
         .AppendFormatLine("                  ,P.{0} = PT.{0}", Entidades.Parametro.Columnas.IdParametro.ToString())
         .AppendFormatLine("                  ,P.{0} = PT.{0}", Entidades.Parametro.Columnas.DescripcionParametro.ToString())
         .AppendFormatLine("                  ,P.{0} = PT.{0}", Entidades.Parametro.Columnas.CategoriaParametro.ToString())
         .AppendFormatLine("                  ,P.{0} = PT.{0}", Entidades.Parametro.Columnas.ValorParametro.ToString())
         .AppendFormatLine("    WHEN NOT MATCHED THEN ")
         .AppendFormatLine("        INSERT ({0}, {1}, {2}, {3}, {4}) VALUES (PT.IdEmpresaDestino, PT.{1}, PT.{2}, PT.{3}, PT.{4})",
                           Entidades.Parametro.Columnas.IdEmpresa.ToString(), Entidades.Parametro.Columnas.IdParametro.ToString(),
                           Entidades.Parametro.Columnas.DescripcionParametro.ToString(), Entidades.Parametro.Columnas.CategoriaParametro.ToString(),
                           Entidades.Parametro.Columnas.ValorParametro.ToString())
         .AppendFormatLine(";")
      End With

      Return Me.Execute(myQuery.ToString())
   End Function


   Public Sub Parametros_D(idEmpresa As Integer, idParametro As String)
      Dim myQuery As StringBuilder = New StringBuilder()
      With myQuery
         .AppendFormatLine("DELETE FROM Parametros ")
         .AppendFormatLine(" WHERE IdEmpresa = {0}", idEmpresa)
         If Not String.IsNullOrWhiteSpace(idParametro) Then
            .AppendFormatLine("   AND IdParametro = '{0}'", idParametro)
         End If
      End With
      Me.Execute(myQuery.ToString())
   End Sub

   Public Function Parametros_GA(idEmpresa As Integer) As DataTable
      Return Parametros_G(idEmpresa, idParametro:=String.Empty, categoriaParametro:=String.Empty, idExacto:=True)
   End Function
   Public Function Parametros_GA(idEmpresa As Integer, idParametro As String, idExacto As Boolean) As DataTable
      Return Parametros_G(idEmpresa, idParametro, categoriaParametro:=String.Empty, idExacto:=idExacto)
   End Function
   Public Function Parametros_GA(idEmpresa As Integer, categoriaParametro As String) As DataTable
      Return Parametros_G(idEmpresa, idParametro:=String.Empty, categoriaParametro:=categoriaParametro, idExacto:=True)
   End Function
   Public Function Parametros_G1(idEmpresa As Integer, idParametro As String) As DataTable
      Return Parametros_G(idEmpresa, idParametro, categoriaParametro:=String.Empty, idExacto:=True)
   End Function

   Public Function Parametros_G(idEmpresa As Integer, idParametro As String, categoriaParametro As String, idExacto As Boolean) As DataTable
      Dim myQuery As StringBuilder = New StringBuilder()
      With myQuery
         .AppendLine("SELECT *")
         .AppendLine("  FROM Parametros")
         .AppendLine(" WHERE 1 = 1 ")
         If idEmpresa > 0 Then
            .AppendFormatLine("   AND IdEmpresa = {0}", idEmpresa)
         End If
         If Not String.IsNullOrWhiteSpace(idParametro) Then
            If idExacto Then
               .AppendFormatLine("   AND IdParametro = '{0}'", idParametro)
            Else
               .AppendFormatLine("   AND IdParametro LIKE '%{0}%'", idParametro)
            End If
         End If
         If Not String.IsNullOrWhiteSpace(categoriaParametro) Then
            .AppendFormatLine("   AND CategoriaParametro = '{0}'", categoriaParametro)
         End If
         .AppendLine(" ORDER BY CategoriaParametro, IdParametro, IdEmpresa")
      End With
      Return Me.GetDataTable(myQuery.ToString())
   End Function

   Public Function Existe(idEmpresa As Integer, IdParametro As String) As Boolean

      Dim myQuery As StringBuilder = New StringBuilder()

      With myQuery
         .AppendFormatLine("SELECT * FROM Parametros ")
         .AppendFormatLine(" WHERE IdEmpresa = {0}", idEmpresa)
         .AppendFormatLine("   AND IdParametro = '{0}'", IdParametro)
      End With

      Dim dt As DataTable = Me.GetDataTable(myQuery.ToString())
      If dt.Rows.Count > 0 Then
         Return True
      Else
         Return False
      End If
   End Function

   Public Function GetValor(idEmpresa As Integer, idParametro As String) As String
      Dim stb As StringBuilder = New StringBuilder()
      With stb
         .AppendFormatLine("SELECT ValorParametro ")
         .AppendFormatLine("  FROM Parametros")
         .AppendFormatLine(" WHERE IdEmpresa = {0}", idEmpresa)
         .AppendFormatLine("   AND IdParametro = '{0}'", idParametro.ToUpper.Trim())
      End With
      Dim dt As DataTable = Me.GetDataTable(stb.ToString())
      If dt.Rows.Count > 0 Then
         Return dt.Rows(0)("ValorParametro").ToString().Trim()
      Else
         Throw New Exception("ATENCION: NO existe el Parametro '" & idParametro.ToUpper.Trim() & "' y NO podrá continuar." & vbCrLf & vbCrLf & "Por favor contáctese con Sistemas.")
      End If
   End Function

   Public Overloads Function GetValorPD(idEmpresa As Integer, idParametro As String, porDefecto As String) As String
      Dim dt As DataTable = Parametros_G(idEmpresa, idParametro, categoriaParametro:=String.Empty, idExacto:=True)
      If dt.Rows.Count > 0 Then
         Return dt.Rows(0)("ValorParametro").ToString().Trim()
      Else
         Return porDefecto
      End If
   End Function

   Function GetParametrosDeOrganizarEntregasv2(idEmpresa As Integer) As DataTable
      Dim stb As StringBuilder = New StringBuilder("")
      With stb
         .AppendLine("SELECT DescripcionParametro as 'Parámetro', CategoriaParametro as 'Solapa' ")
         .AppendLine(" FROM Parametros")
         .AppendFormatLine(" WHERE IdEmpresa = {0}", idEmpresa)
         .AppendLine("   AND (IdParametro = 'PEDIDOSPERMITEFECHAENTREGADISTINTAS' AND ValorParametro <> 'False' OR")
         .AppendLine("        IdParametro = 'FACTURARPEDIDOSELECCIONADO' AND ValorParametro <> 'True' OR")
         .AppendLine("        IdParametro = 'PREFACTURACONSUMEPEDIDOS' AND ValorParametro <> 'True')")
      End With
      Dim dt As DataTable = Me.GetDataTable(stb.ToString())
      Return dt
   End Function

   ''' <summary>
   ''' Consulto el grupo de parametros del mail
   ''' </summary>
   ''' <param name="idParametros">Es un String concatenado con , y valores entre comillas para un IN, por ejemplo 'PAR1','PAR2','PAR3'</param>
   ''' <returns>DataTable con la información</returns>
   ''' <remarks></remarks>
   Function GetMailGenerico(idEmpresa As Integer, idParametros As String) As DataTable
      Dim stb As StringBuilder = New StringBuilder("")
      With stb
         .AppendLine("SELECT IdParametro, ValorParametro, CategoriaParametro, DescripcionParametro ")
         .AppendLine(" FROM Parametros")
         .AppendFormatLine(" WHERE IdEmpresa = {0}", idEmpresa)
         .AppendFormatLine("   AND IdParametro IN ({0})", idParametros)
      End With
      Dim dt As DataTable = Me.GetDataTable(stb.ToString())
      Return dt
   End Function

   'nuevo
   Private Sub SelectTexto(ByVal stb As StringBuilder, esAuditoria As Boolean, Optional campoCalculado As String = "")
      With stb
         .AppendFormatLine("SELECT AuditoriaParametros AS AP.* ")

         .AppendFormatLine(campoCalculado)

         .AppendFormatLine("  , AP.FechaAuditoria ")
         .AppendFormatLine("  , AP.OperacionAuditoria")
         .AppendFormatLine("  , AP.UsuarioAuditoria")
         .AppendFormatLine("  , AP.IdEmpresa")
         .AppendFormatLine("  , AP.IdParametro")
         .AppendFormatLine("  , AP.ValorParametro")
         .AppendFormatLine("  , AP.CategoriaParametro")
         .AppendFormatLine("  , AP.DescripcionParametro")

         .AppendFormatLine("  FROM {0}AuditoriasParametros AS AP", If(esAuditoria, "Auditoria", Nothing))
         .AppendFormatLine("  INNER JOIN Usuarios AS U ON AP.UsuarioAuditoria = U.id")
      End With
   End Sub

   Public Function GetAuditoriasParametros(fechaDesde As Date,
                                           fechaHasta As Date,
                                           idParametro As String,
                                           tipoOperacion As String) As DataTable

      Dim query As StringBuilder = New StringBuilder
      With query
         .AppendFormatLine("SELECT ' ' as Modificado")
         .AppendFormatLine("     , AP.FechaAuditoria")
         .AppendFormatLine("     , AP.OperacionAuditoria")
         .AppendFormatLine("     , AP.UsuarioAuditoria")
         .AppendFormatLine("     , AP.IdEmpresa")
         .AppendFormatLine("     , AP.IdParametro")
         .AppendFormatLine("     , AP.ValorParametro")
         .AppendFormatLine("     , AP.CategoriaParametro")
         .AppendFormatLine("     , AP.DescripcionParametro")

         .AppendFormatLine(" FROM AuditoriaParametros AP")
         'deberia haber left joins?

         .AppendFormat("WHERE AP.FechaAuditoria >= '{0} 00:00:00'", fechaDesde.ToString("yyyyMMdd"))
         .AppendFormat("	 AND AP.FechaAuditoria <= '{0} 23:59:59'", fechaHasta.ToString("yyyyMMdd"))

         If idParametro <> "" Then
            .AppendFormat("    AND AP.IdParametro = '{0}'", idParametro)
         End If
         If Not String.IsNullOrEmpty(tipoOperacion) Then
            Select Case tipoOperacion
               Case Entidades.OperacionesAuditorias.Alta.ToString()
                  .AppendFormatLine(" AND AP.OperacionAuditoria = 'A'")
               Case Entidades.OperacionesAuditorias.Modificacion.ToString()
                  .AppendFormatLine(" AND AP.OperacionAuditoria = 'M'")
               Case Entidades.OperacionesAuditorias.Baja.ToString()
                  .AppendFormatLine(" AND AP.OperacionAuditoria = 'B'")
            End Select
         End If

         .AppendLine("     ORDER BY AP.IdParametro, AP.FechaAuditoria")

      End With
      Return Me.GetDataTable(query.ToString())
   End Function

End Class