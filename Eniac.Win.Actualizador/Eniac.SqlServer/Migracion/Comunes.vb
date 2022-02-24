Imports Eniac
Imports Entidades.Usuarios

Namespace SqlServer
   Public Class Comunes
      Protected _da As Datos.DataAccess
      Protected Const ColumnAlias As String = "Maximo"
      Protected Const FormatoBuscar As String = " WHERE {0} LIKE '%{1}%'"
      Protected Const FormatoBuscarAnd As String = " AND {0} LIKE '%{1}%'"

      Public Sub New(da As Datos.DataAccess)
         Me._da = da
      End Sub

      Protected Function Execute(query As String) As Integer
         Try
            With _da
               .Command.Connection = .Connection
               If .Transaction IsNot Nothing Then
                  .Command.Transaction = .Transaction
               End If
               .Command.CommandText = query
               .Command.CommandType = CommandType.Text
               Dim cantidadRegistrosAfectados As Integer
               cantidadRegistrosAfectados = .Command.ExecuteNonQuery()
               Debug.WriteLine(String.Format("Cantidad de registros {0}", cantidadRegistrosAfectados))
               Return cantidadRegistrosAfectados
            End With
         Catch ex1 As SqlClient.SqlException
            LgoError(ex1, query)
            Dim erro As New Entidades.EniacException(ex1.Message & vbNewLine & query)

            Select Case ex1.Number
               Case 547
                  If query.Trim.Substring(0, 6) = "UPDATE" Then
                     Throw New Exception("No se puede Actualizar el registro porque alguna caracteristica esta inconsistente.", ex1)
                  ElseIf query.Trim.Substring(0, 6) = "INSERT" Then
                     Throw New Exception("No se puede Agregar el registro porque alguna caracteristica esta inconsistente.", ex1)
                  ElseIf query.Contains("INSERT INTO") Then
                     Throw New Exception(ex1.Message + Convert.ToChar(13) + query.ToString(), ex1)
                  Else
                     'controlo el error y trato de mandar mas información
                     Dim men As String
                     Dim peda As String
                     Dim comi As Integer
                     men = "No se puede Eliminar porque esta siendo utilizado"
                     If ex1.Message.Contains("FK_") Then
                        men += " en "
                        comi = ex1.Message.IndexOf("dbo.") + 4
                        peda = ex1.Message.Substring(comi, ex1.Message.Length - comi)
                        If peda.IndexOf(",") > 1 Then
                           peda = peda.Substring(0, peda.IndexOf(",") - 1)
                        End If
                        men += peda
                     End If
                     Throw New Exception(men, ex1)
                  End If

               Case 2627
                  Dim nombrePK = String.Empty
                  If ex1.Message.Contains("PK_") Then
                     nombrePK = String.Format(" ({0})", ex1.Message.Substring(ex1.Message.IndexOf("PK_"), ex1.Message.IndexOf("'.", ex1.Message.IndexOf("PK_")) - ex1.Message.IndexOf("PK_")))
                  ElseIf ex1.Message.Contains("AK_") Then
                     nombrePK = String.Format(" ({0})", ex1.Message.Substring(ex1.Message.IndexOf("AK_"), ex1.Message.IndexOf("'.", ex1.Message.IndexOf("AK_")) - ex1.Message.IndexOf("AK_")))
                  Else

                  End If

                  Throw New Exception(String.Format("El código ingresado ya existe.{0}", nombrePK), ex1)
               Case Else
                  Throw
            End Select
         Catch
            Throw
         End Try
      End Function

      Protected Function ExecuteLimpio(query As String) As Integer
         Try
            With Me._da
               .Command.Connection = .Connection
               If .Transaction IsNot Nothing Then
                  .Command.Transaction = .Transaction
               End If
               .Command.CommandText = query
               .Command.CommandType = CommandType.Text
               Dim cantidadRegistrosAfectados As Integer
               cantidadRegistrosAfectados = .Command.ExecuteNonQuery()
               Debug.WriteLine(String.Format("Cantidad de registros {0}", cantidadRegistrosAfectados))
               Return cantidadRegistrosAfectados
            End With
         Catch
            Throw
         End Try
      End Function
      Public Function AgregarParametro(nombreParametro As String, valor As Byte()) As Data.Common.DbParameter
         Return AgregarParametro(nombreParametro, DbType.Binary, valor.Length, valor)
      End Function
      Public Function AgregarParametro(nombreParametro As String, valor As String) As Data.Common.DbParameter
         Return AgregarParametro(nombreParametro, DbType.String, Nothing, valor)
      End Function
      Public Function AgregarParametro(nombreParametro As String, valor As Long) As Data.Common.DbParameter
         Return AgregarParametro(nombreParametro, DbType.Int64, Nothing, valor)
      End Function
      Public Function AgregarParametro(nombreParametro As String, valor As Integer) As Data.Common.DbParameter
         Return AgregarParametro(nombreParametro, DbType.Int32, Nothing, valor)
      End Function
      Public Function AgregarParametro(nombreParametro As String, dataType As System.Data.DbType, size As Integer?, valor As Object) As Data.Common.DbParameter
         Dim oParameter As Data.Common.DbParameter = Me._da.Command.CreateParameter()
         oParameter.ParameterName = nombreParametro
         oParameter.DbType = dataType
         If size.HasValue Then
            oParameter.Size = size.Value
         End If
         oParameter.Value = valor
         Me._da.Command.Parameters.Add(oParameter)
         Return oParameter
      End Function

      Private Sub LgoError(Problema As Exception, Comentario As String)
         Try
            Dim mensaje As String = String.Format("Error: {1}{0}{2}{0} Stack Trace: {3}", Environment.NewLine, Problema.Message, Comentario, Problema.StackTrace).Replace("'", "´")
            Try
               My.Application.Log.WriteEntry(mensaje, TraceEventType.Error)
            Catch ex As Exception
            End Try
            GrabarErrorEnBitacora(mensaje)
         Catch ex As Exception

         End Try
      End Sub

      Protected Overridable Sub GrabarErrorEnBitacora(mensaje As String)
         'Try
         '   Dim da As Eniac.Datos.DataAccess = New Datos.DataAccess()
         '   Try
         '      da.OpenConection()

         '      Dim aplicacion As String = New Parametros(da).GetValorPD(Actual.Sucursal.IdEmpresa, "IDAPLICACION", "SIGA")
         '      Dim sqlBitacora As Bitacora = New Bitacora(da)
         '      sqlBitacora.Bitacora_I(Actual.Sucursal.Id, -1, Now, aplicacion, Actual.Nombre, My.Computer.Name, Entidades.Bitacora.TipoBitacora.SucedioError.ToString(), mensaje)

         '   Finally
         '      da.CloseConection()
         '   End Try

         'Catch ex As Exception
         'End Try
      End Sub

      Protected Function ExecuteReadear(query As String) As System.Data.Common.DbDataReader
         Me._da.Command.Connection = Me._da.Connection
         Me._da.Command.Transaction = Me._da.Transaction
         Me._da.Command.CommandText = query
         Return Me._da.Command.ExecuteReader()
      End Function
      Protected Function ExecuteScalar(query As String) As Object
         Me._da.Command.Connection = Me._da.Connection
         Me._da.Command.CommandText = query
         Return Me._da.Command.ExecuteScalar()
      End Function
      Protected Function GetDataTable(query As String) As DataTable
         Return Me._da.GetDataTable(query)
      End Function
      Protected Function GetDataTable(query As StringBuilder) As DataTable
         Return GetDataTable(query.ToString())
      End Function

      Protected Sub Sincroniza_I(query As String, tabla As String)
         Exit Sub
      End Sub

      Protected Function ObtenerFecha(fecha As DateTime?, traerHoraReal As Boolean, Optional conMilisegundos As Boolean = False) As String
         If fecha.HasValue Then
            Return GetStringParaQueryConComillas(ObtenerFecha(fecha.Value, traerHoraReal, conMilisegundos))
         End If
         Return "NULL"
      End Function
      Protected Function ObtenerFecha(fecha As DateTime, traerHoraReal As Boolean, Optional conMilisegundos As Boolean = False) As String
         Dim myQuery As System.Text.StringBuilder = New System.Text.StringBuilder()
         With myQuery
            .Append(fecha.Year.ToString("0000"))
            .Append(fecha.Month.ToString("00"))
            .Append(fecha.Day.ToString("00"))
            If traerHoraReal Then
               .Append(" ")
               .Append(fecha.Hour.ToString("00"))
               .Append(":")
               .Append(fecha.Minute.ToString("00"))
               .Append(":")
               .Append(fecha.Second.ToString("00"))
               If conMilisegundos Then
                  .Append(".")
                  .Append(fecha.Millisecond.ToString("000"))
               End If
            Else
               .Append(" 00:00:00")
               If conMilisegundos Then
                  .Append(".000")
               End If
            End If
         End With
         Return myQuery.ToString()
      End Function

      Public Function GetAnchoCampo(tabla As String, campo As String) As Integer
         Dim myQuery As StringBuilder = New StringBuilder()
         With myQuery
            .AppendFormatLine("SELECT DATA_TYPE, CHARACTER_MAXIMUM_LENGTH, NUMERIC_PRECISION, NUMERIC_SCALE ")
            .AppendFormatLine(" FROM INFORMATION_SCHEMA.COLUMNS")
            .AppendFormatLine(" WHERE TABLE_NAME = '{0}'", tabla)
            .AppendFormatLine("   AND COLUMN_NAME = '{0}'", campo)
         End With

         Dim dt As DataTable = Me.GetDataTable(myQuery.ToString())
         If dt.Rows.Count > 0 Then
            Dim dr As DataRow = dt.Rows(0)
            Dim dataType As String = dr("DATA_TYPE").ToString()
            Select Case dataType
               Case "bigint", "Int", "smallint"
                  Return Integer.Parse(dr("NUMERIC_PRECISION").ToString())
               Case "decimal"
                  Return Integer.Parse(dr("NUMERIC_PRECISION").ToString()) - Integer.Parse(dr("NUMERIC_SCALE").ToString())
               Case "char", "nvarchar", "varchar"
                  Return Integer.Parse(dr("CHARACTER_MAXIMUM_LENGTH").ToString())
               Case "bit"
                  Return 1
               Case "DateTime"
                  Return 19
               Case Else
                  Throw New Exception(String.Format("ATENCION: el campo '{1}' de la Tabla '{2}' tiene el tipo de dato '{3}' y NO podrá continuar.{0}{0}Por favor contáctese con Sistemas.",
                                                 Environment.NewLine, campo, tabla, dataType))
            End Select
         Else
            Throw New Exception(String.Format("ATENCION: NO existe el campo '{1}' de la Tabla '{2}' y NO podrá continuar.{0}{0}Por favor contáctese con Sistemas.",
                                           Environment.NewLine, campo, tabla))
         End If
      End Function

      Protected Function GetStringFromNumber(valor As Long?) As String
         If valor.HasValue Then
            Return valor.ToString()
         Else
            Return "NULL"
         End If
      End Function

      Protected Function GetStringFromNumber(valor As Long) As String
         Return GetStringFromNumber(valor, 0)
      End Function

      Protected Function GetStringFromNumber(valor As Long, valorParaNull As Long) As String
         If valor = valorParaNull Then
            Return "NULL"
         Else
            Return valor.ToString()
         End If
      End Function

      Protected Function GetStringFromDecimal(valor As Decimal?) As String
         If valor.HasValue Then
            Return valor.ToString()
         Else
            Return "NULL"
         End If
      End Function

      Protected Function GetStringFromBoolean(valor As Boolean?) As String
         If valor.HasValue Then
            Return GetStringFromBoolean(valor.Value)
         Else
            Return "NULL"
         End If
      End Function
      Protected Function GetStringFromBoolean(valor As Boolean) As String
         If valor Then
            Return "1"
         Else
            Return "0"
         End If
      End Function

      Protected Function ConvertirBitSiNo(aliasTabla As String, nombreColumna As String) As String
         Return String.Format("CASE WHEN {0}.{1} IS NULL THEN '' ELSE CASE WHEN {0}.{1} = 0 THEN 'NO' ELSE 'SI' END END AS {1}_SN", aliasTabla, nombreColumna)
      End Function

      Public Sub SincronizaRegistros_I(fechaHora As DateTime,
                          sucursalOrigen As Int32,
                          sucursalDestino As Int32,
                          query As String,
                          tabla As String,
                          fechaHoraProceso As DateTime,
                          estado As String)
         Dim myQuery As StringBuilder = New StringBuilder("")

         With myQuery
            .Append("INSERT INTO  ")
            .Append(" SincronizaRegistros  ")
            .Append(" (FechaHora ")
            .Append(" ,SucursalOrigen  ")
            .Append(" ,SucursalDestino  ")
            .Append(" ,Query  ")
            .Append(" ,Tabla  ")
            .Append(" ,FechaHoraProceso  ")
            .Append(" ,Estado)  ")
            .Append(" VALUES( ")
            If fechaHora = Nothing Then
               .Append("null, ")
            Else
               .Append("'" & Me.ObtenerFecha(fechaHora, True) & "', ")
            End If
            .Append(sucursalOrigen & ", ")
            .Append(sucursalDestino & ", ")
            .Append(" '" & query & "', ")
            .Append(" '" & tabla & "', ")
            If fechaHoraProceso <= New DateTime(1900, 1, 1) Then
               .Append(" null, ")
            Else
               .Append(" '" & Me.ObtenerFecha(fechaHoraProceso, True) & "', ")
            End If
            .Append(" '" & estado & "') ")
         End With

         Me.Execute(myQuery.ToString())
      End Sub

      Public Sub SincronizaRegistros_D(idSucursalDestino As Integer)
         Dim myQuery As StringBuilder = New StringBuilder("")

         With myQuery
            .Append("DELETE FROM  ")
            .Append("SincronizaRegistros  ")
            .Append("WHERE  ")
            .Append("SucursalDestino = " & idSucursalDestino)
         End With

         Me.Execute(myQuery.ToString())
      End Sub

      Public Sub SincronizaRegistros_UEstado(idSucursalDestino As Integer)
         Dim myQuery As StringBuilder = New StringBuilder("")

         With myQuery
            .Append("UPDATE SincronizaRegistros ")
            .Append("   SET Estado = 'T'")
            .Append("WHERE  ")
            .Append("SucursalDestino = " & idSucursalDestino)
         End With

         Me.Execute(myQuery.ToString())
      End Sub

      Public Sub SincronizaRegistro_D(id As Long)
         Dim myQuery As StringBuilder = New StringBuilder("")

         With myQuery
            .Append("DELETE FROM  ")
            .Append("SincronizaRegistros  ")
            .Append("WHERE  ")
            .Append("Id = " & id)
         End With

         Me.Execute(myQuery.ToString())
      End Sub

      Public Sub SincronizaQuery_I(query As String)
         Me.Execute(query)
      End Sub

      Public Function GetStringParaQueryConComillas(cadena As String) As String
         Dim cad As String = Me.GetStringParaQuery(cadena)
         If cad = "NULL" Then
            Return cad
         Else
            Return "'" + cad + "'"
         End If
      End Function

      Public Function GetStringParaQuery(cadena As String) As String
         If String.IsNullOrEmpty(cadena) Then
            Return "NULL"
         End If
         cadena.Replace("'", "´")
         Return cadena
      End Function

      Public Function GetInt32ParaQuery(entero As Int32) As String
         If entero = 0 Then
            Return "NULL"
         End If
         Return entero.ToString()
      End Function
      Public Function GetCodigoMinimo(campo As String, tabla As String) As Long
         Return GetCodigoMinimo(campo, tabla, String.Empty)
      End Function
      Public Function GetCodigoMinimo(campo As String, tabla As String, condicion As String) As Long
         Dim result As Long = 0

         Dim stb As StringBuilder = New StringBuilder()
         With stb
            .AppendFormat("SELECT MIN({0}) AS {1} FROM {2}", campo, ColumnAlias, tabla)
            If Not String.IsNullOrWhiteSpace(condicion) Then
               .AppendFormat(" WHERE {0}", condicion)
            End If
         End With
         Dim dt As DataTable = Me.GetDataTable(stb.ToString())
         If dt.Rows.Count > 0 AndAlso dt.Columns.Contains(ColumnAlias) Then
            If Not IsDBNull(dt.Rows(0)(ColumnAlias)) AndAlso Not String.IsNullOrWhiteSpace(dt.Rows(0)(ColumnAlias).ToString()) Then
               If Not Long.TryParse(dt.Rows(0)(ColumnAlias).ToString(), result) Then
                  result = 0
               End If
            End If
         End If
         Return result
      End Function

      Public Function GetCodigoMaximo(campo As String, tabla As String) As Long
         Return GetCodigoMaximo(campo, tabla, String.Empty)
      End Function
      Public Function GetCodigoMaximo(campo As String, tabla As String, condicion As String) As Long
         Dim result As Long = 0

         Dim stb As StringBuilder = New StringBuilder()
         With stb
            .AppendFormat("SELECT MAX({0}) AS {1} FROM {2}", campo, ColumnAlias, tabla)
            If Not String.IsNullOrWhiteSpace(condicion) Then
               .AppendFormat(" WHERE {0}", condicion)
            End If
         End With
         Dim dt As DataTable = Me.GetDataTable(stb.ToString())
         If dt.Rows.Count > 0 AndAlso dt.Columns.Contains(ColumnAlias) Then
            If Not IsDBNull(dt.Rows(0)(ColumnAlias)) AndAlso Not String.IsNullOrWhiteSpace(dt.Rows(0)(ColumnAlias).ToString()) Then
               If Not Long.TryParse(dt.Rows(0)(ColumnAlias).ToString(), result) Then
                  result = 0
               End If
            End If
         End If
         Return result
      End Function

      Protected Sub NivelAutorizacionClienteTipoComprobante(stb As StringBuilder, aliasCliente As String, aliasCategoria As String, aliasTipoComprobante As String, nivelAutorizacion As Short)
         NivelAutorizacionCliente(stb, aliasCliente, aliasCategoria, nivelAutorizacion)
         NivelAutorizacionTipoComprobante(stb, aliasTipoComprobante, nivelAutorizacion)
      End Sub
      Protected Sub NivelAutorizacionProveedorTipoComprobante(stb As StringBuilder, aliasProveedor As String, aliasTipoComprobante As String, nivelAutorizacion As Short)
         NivelAutorizacionProveedor(stb, aliasProveedor, nivelAutorizacion)
         NivelAutorizacionTipoComprobante(stb, aliasTipoComprobante, nivelAutorizacion)
      End Sub

      Protected Sub NivelAutorizacionCliente(stb As StringBuilder, aliasCliente As String, aliasCategoria As String, nivelAutorizacion As Short)
         stb.AppendFormatLine("    AND CASE WHEN {0}.NivelAutorizacion > 0 THEN {0}.NivelAutorizacion ELSE {1}.NivelAutorizacion END <= {2}",
                           aliasCliente, aliasCategoria, nivelAutorizacion)
      End Sub
      Protected Sub NivelAutorizacionTipoComprobante(stb As StringBuilder, aliasTipoComprobante As String, nivelAutorizacion As Short)
         stb.AppendFormatLine("    AND {0}.NivelAutorizacion <= {1}", aliasTipoComprobante, nivelAutorizacion)
      End Sub
      Protected Sub NivelAutorizacionProveedor(stb As StringBuilder, aliasProveedor As String, nivelAutorizacion As Short)
         stb.AppendFormatLine("    AND {0}.NivelAutorizacion <= {1}", aliasProveedor, nivelAutorizacion)
      End Sub

      Private Function GetInsertSelectQuery(nombreTabla As String, valoresReemplazar As Dictionary(Of String, String), whereClause As String) As String
         Dim dtColumnas As DataTable = GetDataTable(String.Format("SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME='{0}'", nombreTabla))

         Dim camposInsert As StringBuilder = New StringBuilder()
         Dim camposSelect As StringBuilder = New StringBuilder()

         Dim primero As Boolean = True

         For Each drColumna As DataRow In dtColumnas.Rows
            Dim nombreColumna As String = drColumna("COLUMN_NAME").ToString()

            camposInsert.AppendFormat(If(primero, " {0}", ",{0}"), nombreColumna)

            If valoresReemplazar.ContainsKey(nombreColumna) Then
               camposSelect.AppendFormat(If(primero, " '{0}'", ",'{0}'"), valoresReemplazar(nombreColumna))
            Else
               camposSelect.AppendFormat(If(primero, " {0}", ",{0}"), nombreColumna)
            End If

            primero = False
         Next

         Dim qry As StringBuilder = New StringBuilder()
         qry.AppendFormatLine("INSERT INTO {0} ({1}) SELECT {2} FROM {0} {3}", nombreTabla, camposInsert.ToString(), camposSelect.ToString(), whereClause)
         Return qry.ToString()
      End Function
      Public Function InsertSelect(nombreTabla As String, valoresReemplazar As Dictionary(Of String, String), whereClause As String) As Integer
         Return Execute(GetInsertSelectQuery(nombreTabla, valoresReemplazar, whereClause))
      End Function


      Private Function GetUpdatePrimaryKeyQuery(nombreTabla As String, valoresReemplazar As Dictionary(Of String, String), whereClause As String) As String
         Dim camposUpdate As StringBuilder = New StringBuilder()

         Dim primero As Boolean = True

         For Each valores As KeyValuePair(Of String, String) In valoresReemplazar
            camposUpdate.AppendFormat(If(primero, "   SET", "     ,"))
            camposUpdate.AppendFormatLine(" {0} = '{1}'", valores.Key, valores.Value)

            primero = False
         Next

         Dim qry As StringBuilder = New StringBuilder()
         qry.AppendFormatLine("UPDATE {0} {1} {2}", nombreTabla, camposUpdate.ToString(), whereClause)

         Return qry.ToString()
      End Function
      Public Function UpdatePrimaryKey(nombreTabla As String, valoresReemplazar As Dictionary(Of String, String), whereClause As String) As Integer
         Return Execute(GetUpdatePrimaryKeyQuery(nombreTabla, valoresReemplazar, whereClause))
      End Function

      Public Function DeleteGenerico(nombreTabla As String, valoresReemplazar As Dictionary(Of String, String), whereClause As String) As Integer
         Return Execute(String.Format("DELETE {0} {1}", nombreTabla, whereClause))
      End Function

      Public Function ObtenerOperadorRelacional(valor As Entidades.Publicos.OperadoresRelacionales) As String
         Select Case valor
            Case Entidades.Publicos.OperadoresRelacionales.MAYOR
               Return ">"
            Case Entidades.Publicos.OperadoresRelacionales.MENOR
               Return "<"
            Case Entidades.Publicos.OperadoresRelacionales.MAYORIGUAL
               Return ">="
            Case Entidades.Publicos.OperadoresRelacionales.MENORIGUAL
               Return "<="
            Case Entidades.Publicos.OperadoresRelacionales.DISTINTO
               Return "<>"
            Case Entidades.Publicos.OperadoresRelacionales.IGUAL
               Return "="
         End Select
         Return Nothing
      End Function

      Protected Function ArmarOrderBy(stb As StringBuilder, camposOrderBy As String()) As StringBuilder
         Dim primero As Boolean = True
         For Each campo As String In camposOrderBy
            If primero Then
               stb.AppendFormat(" ORDER BY {0}", campo)
               primero = False
            Else
               stb.AppendFormat(", {0}", campo)
            End If
         Next

         If Not primero Then
            stb.AppendLine()
         End If

         Return stb
      End Function

      Protected Function ValidarRango(desde As Integer,
                                   hasta As Integer,
                                   nombreTabla As String,
                                   nombreCampoDesde As String,
                                   nombreCampoHasta As String) As DataTable
         Return ValidarRango(desde, hasta, nombreTabla, nombreCampoDesde, nombreCampoHasta, condicion:=Nothing)
      End Function

      Protected Function ValidarRango(desde As Integer,
                                   hasta As Integer,
                                   nombreTabla As String,
                                   nombreCampoDesde As String,
                                   nombreCampoHasta As String,
                                   condicion As String) As DataTable
         Dim query As StringBuilder = New StringBuilder
         With query
            .AppendFormatLine("SELECT * FROM {0} ", nombreTabla)
            .AppendFormatLine("	WHERE (({0} >= {2} AND {0} <= {3}) OR ({1} >= {2} AND {1} <= {3}) OR ({0} <= {2} AND {1} >= {3}))", desde, hasta, nombreCampoDesde, nombreCampoHasta)

            If Not String.IsNullOrEmpty(condicion) Then .AppendFormatLine("	AND {0}", condicion)

         End With
         Return Me.GetDataTable(query.ToString())
      End Function

      Public Function GetIdByNumeroDeCheque(idSucursal As Integer,
                                         numeroCheque As Long,
                                         idBanco As Integer,
                                         idBancoSucursal As Integer,
                                         idLocalidad As Integer) As DataTable
         Dim query As StringBuilder = New StringBuilder
         With query
            .AppendFormatLine("SELECT C.IdCheque FROM Cheques C")
            .AppendFormatLine("	WHERE C.IdSucursal = {0} AND C.NumeroCheque = {1} AND C.IdBanco = {2} AND C.IdBancoSucursal = {3} AND C.IdLocalidad = {4}",
                                    idSucursal, numeroCheque, idBanco, idBancoSucursal, idLocalidad)
         End With
         Return Me.GetDataTable(query.ToString())
      End Function

      Protected Overridable Function Buscar(columna As String, valor As String, selectTexto As Action(Of StringBuilder),
                                         prefijoTabla As String) As DataTable
         Return Buscar(columna, valor, selectTexto, prefijoTabla, mapColumnas:=Nothing, mapearColumnaCustom:=Nothing)
      End Function
      Protected Overridable Function Buscar(columna As String, valor As String, selectTexto As Action(Of StringBuilder),
                                         prefijoTabla As String, mapColumnas As Dictionary(Of String, String)) As DataTable
         Return Buscar(columna, valor, selectTexto, prefijoTabla, mapColumnas, mapearColumnaCustom:=Nothing)
      End Function

      Protected Overridable Function Buscar(columna As String, valor As String, selectTexto As Action(Of StringBuilder),
                                         prefijoTabla As String, mapColumnas As Dictionary(Of String, String),
                                         mapearColumnaCustom As Func(Of String, String)) As DataTable

         If mapearColumnaCustom Is Nothing Then
            mapearColumnaCustom = Function(col) col
         End If

         Dim columnaMapeada = mapearColumnaCustom(columna)
         If columna.Equals(columnaMapeada) Then
            If mapColumnas IsNot Nothing AndAlso mapColumnas.ContainsKey(columna) Then
               columnaMapeada = mapColumnas(columna)
            Else
               columnaMapeada = String.Concat(prefijoTabla, columna)
            End If
         End If

         Dim stb = New StringBuilder()
         With stb
            selectTexto(stb)
            .AppendFormatLine(FormatoBuscar, columnaMapeada, valor)
         End With
         Return Me.GetDataTable(stb.ToString())


         Return Nothing
      End Function

   End Class


End Namespace
