Public Class VersionesScripts
   Inherits Comunes

   Private Const CantidadSinTop As Integer = 0

   Public Sub New(ByVal da As Eniac.Datos.DataAccess)
      MyBase.New(da)
   End Sub

   Private Function GetScript(script As String) As String
      script = script.Replace("'", "''")

      Return script
   End Function

   Public Sub VersionesScripts_I(idAplicacion As String,
                          nroVersion As String,
                          orden As Integer,
                          nombre As String,
                          script As String,
                          obligatorio As Boolean,
                          codigoCliente As Long)

      Dim myQuery As StringBuilder = New StringBuilder()

      With myQuery
         .AppendLine("INSERT INTO VersionesScripts")
         .AppendLine("           ([IdAplicacion]")
         .AppendLine("           ,[NroVersion]")
         .AppendLine("           ,[Orden]")
         .AppendLine("           ,[Nombre]")
         .AppendLine("           ,[Script]")
         .AppendLine("           ,[Obligatorio]")
         .AppendLine("           ,[CodigoCliente]")
         .AppendLine(")     VALUES (")
         .AppendFormatLine("           '{0}'", idAplicacion)
         .AppendFormatLine("           ,'{0}'", nroVersion)
         .AppendFormatLine("           ,{0}", orden)
         .AppendFormatLine("           ,'{0}'", nombre)
         ''.AppendFormatLine("           ,'{0}'", Me.GetScript(script))
         .AppendFormatLine("           ,@script")
         .AppendFormatLine("           ,'{0}'", obligatorio)
         If codigoCliente <> 0 Then
            .AppendFormatLine("           ,{0}", codigoCliente)
         Else
            .AppendFormatLine("           ,NULL")
         End If
         .Append(")")

      End With

      ''Aquí defino el parámetro
      Me._da.Command.CommandText = myQuery.ToString()
      Me._da.Command.CommandType = CommandType.Text
      Dim oParameter As Data.Common.DbParameter = Me._da.Command.CreateParameter()
      oParameter.ParameterName = "@script"
      oParameter.DbType = DbType.String
      oParameter.Size = script.Length
      oParameter.Value = script
      Me._da.Command.Parameters.Add(oParameter)
      Me._da.Command.Connection = Me._da.Connection

      ''Me.Execute(myQuery.ToString())
      Me._da.ExecuteNonQuery(Me._da.Command)
   End Sub

   Public Sub VersionesScripts_U(idAplicacion As String,
                          nroVersion As String,
                          orden As Integer,
                          nombre As String,
                          script As String,
                          obligatorio As Boolean,
                          codigoCliente As Long)


      Dim myQuery As StringBuilder = New StringBuilder()

      With myQuery
         .Append("UPDATE VersionesScripts")
         .AppendFormat("   SET ")
         .AppendFormat("      [Nombre] = '{0}'", nombre)
         ''.AppendFormat("      ,[Script] = '{0}'", Me.GetScript(script))
         .AppendFormat("      ,[Script] = @script")
         .AppendFormat("      ,[Obligatorio] = {0}", GetStringFromBoolean(obligatorio))
         If codigoCliente <> 0 Then
            .AppendFormat("      ,[CodigoCliente] = {0}", codigoCliente)
         Else
            .AppendFormat("      ,[CodigoCliente] = NULL")
         End If
         .AppendFormat(" WHERE [IdAplicacion] = '{0}' ", idAplicacion)
         .AppendFormat(" AND [NroVersion] = '{0}' ", nroVersion)
         .AppendFormat(" AND [Orden] = {0} ", orden)

      End With

      ''Aquí defino el parámetro
      Me._da.Command.CommandText = myQuery.ToString()
      Me._da.Command.CommandType = CommandType.Text
      Dim oParameter As Data.Common.DbParameter = Me._da.Command.CreateParameter()
      oParameter.ParameterName = "@script"
      oParameter.DbType = DbType.String
      oParameter.Size = script.Length
      oParameter.Value = script
      Me._da.Command.Parameters.Add(oParameter)
      Me._da.Command.Connection = Me._da.Connection

      ''Me.Execute(myQuery.ToString())
      Me._da.ExecuteNonQuery(Me._da.Command)
   End Sub

   Public Sub VersionesScripts_D(idAplicacion As String, Version As String, orden As Integer)
      Dim myQuery As StringBuilder = New StringBuilder()

      With myQuery
         .AppendFormatLine("DELETE FROM VersionesScripts")
         .AppendFormatLine(" WHERE {0} = '{1}'", Entidades.VersionScript.Columnas.IdAplicacion.ToString(), idAplicacion)
         .AppendFormatLine("   AND {0} = '{1}'", Entidades.VersionScript.Columnas.NroVersion.ToString(), Version)
         .AppendFormatLine("   AND {0} = {1}", Entidades.VersionScript.Columnas.Orden.ToString(), orden)
      End With

      Me.Execute(myQuery.ToString())
   End Sub

   Private Sub SelectTexto(stb As StringBuilder, top As Integer)
      With stb
         'Si requiere traer un TOP (por ejemplo TOP 1) el parametro top será mayor a 0, entonces lo agrego al query
         'Si NO requiere traer un TOP el parametro top será 0, y NO lo agrego al query
         Dim strTop As String = If(top = CantidadSinTop, String.Empty, String.Format("TOP {0} ", top))
         .AppendFormatLine("SELECT {0}V.IdAplicacion", strTop)
         .AppendLine("     , V.NroVersion")
         .AppendLine("     , V.Orden")
         .AppendLine("     , V.Nombre")
         .AppendLine("     , V.Script")
         .AppendLine("     , V.Obligatorio")
         .AppendLine("     , V.CodigoCliente")
         .AppendLine("  FROM VersionesScripts V")
      End With
   End Sub
   Public Function GetOrdenMaximo(idAplicacion As String, version As String) As Integer
      Return Convert.ToInt32(GetCodigoMaximo(Entidades.VersionScript.Columnas.Orden.ToString(), "VersionesScripts",
                                             String.Format("{0} = '{1}' AND {2} = '{3}'",
                                                           Entidades.VersionScript.Columnas.IdAplicacion.ToString(), idAplicacion,
                                                           Entidades.VersionScript.Columnas.NroVersion.ToString(), version)))
   End Function
   Private Function VersionesScripts_G(idAplicacion As String, version As String, orden As Integer, top As Integer, nombre As String) As DataTable
      Dim myQuery As StringBuilder = New StringBuilder()
      With myQuery
         Me.SelectTexto(myQuery, top)
         .AppendLine(" WHERE 1 = 1")
         If Not String.IsNullOrWhiteSpace(idAplicacion) Then
            .AppendFormatLine("   AND V.{0} = '{1}'", Entidades.VersionScript.Columnas.IdAplicacion.ToString(), idAplicacion)
         End If
         If Not String.IsNullOrWhiteSpace(version) Then
            .AppendFormatLine("   AND V.{0} = '{1}'", Entidades.VersionScript.Columnas.NroVersion.ToString(), version)
         End If
         If orden <> 0 Then
            .AppendFormatLine("   AND V.{0} = {1}", Entidades.VersionScript.Columnas.Orden.ToString(), orden)
         End If
         If Not String.IsNullOrWhiteSpace(nombre) Then
            .AppendFormatLine("   AND V.{0} = '{1}'", Entidades.VersionScript.Columnas.Nombre.ToString(), nombre)
         End If

         .AppendLine(" ORDER BY CAST(replace(replace(V.nroversion,'.',''),',','') AS bigint) DESC")
      End With
      Return Me.GetDataTable(myQuery.ToString())
   End Function

   Public Function Versiones_GA() As DataTable
      Return VersionesScripts_G(idAplicacion:="", version:="", orden:=0, top:=CantidadSinTop, nombre:=String.Empty)
   End Function

   Public Function Versiones_GA(idAplicacion As String) As DataTable
      Return VersionesScripts_G(idAplicacion, version:="", orden:=0, top:=CantidadSinTop, nombre:=String.Empty)
   End Function
   Public Function ExisteScriptPorNombre(idAplicacion As String, nombre As String) As DataTable
      Return VersionesScripts_G(idAplicacion, version:="", orden:=0, top:=0, nombre:=nombre)
   End Function
   Public Function ExisteScriptPorOrden(idAplicacion As String, nroVersion As String, orden As Integer) As DataTable
      Return VersionesScripts_G(idAplicacion, version:=nroVersion, orden:=orden, top:=0, nombre:="")
   End Function
   Public Function Versiones_G1(idAplicacion As String, version As String, orden As Integer) As DataTable
      Return VersionesScripts_G(idAplicacion, version, orden, top:=CantidadSinTop, nombre:="")
   End Function

   Public Overloads Function Buscar(columna As String, valor As String) As DataTable
      columna = "V." + columna
      Dim stb As StringBuilder = New StringBuilder()
      With stb
         Me.SelectTexto(stb, top:=CantidadSinTop)
         .AppendFormatLine(" WHERE {0} LIKE '%{1}%'", columna, valor)
      End With
      Return Me.GetDataTable(stb.ToString())
   End Function

   Public Function GetUltimaPorAplicacion(idAplicacion As String) As DataTable
      Return VersionesScripts_G(idAplicacion, version:="", orden:=0, top:=1, nombre:="")
   End Function

   Public Function GetxAplicacionVersion(idAplicacion As String, nroVersion As String) As DataTable
      Return VersionesScripts_G(idAplicacion, nroVersion, orden:=0, top:=0, nombre:="")
   End Function

   ''' <summary>
   ''' Ejecutar cualquier script
   ''' </summary>
   ''' <param name="script"></param>
   ''' <returns>Cantidad de registros afectados.</returns>
   ''' <remarks></remarks>
   Public Function EjecutarScript(script As String) As Integer
      Return Me.ExecuteLimpio(script)
   End Function

   Public Sub EliminarScriptsVersion(idAplicacion As String, versiones As String())
      Dim myQuery As StringBuilder = New StringBuilder()
      Dim cons As String = ""
      For Each vs As String In versiones
         cons += "'" + vs + "', "
      Next
      cons = cons.Substring(0, cons.Length - 2)

      With myQuery
         .AppendFormatLine("DELETE FROM VersionesScripts ")
         .AppendFormatLine(" WHERE {0} = '{1}'", Entidades.VersionScript.Columnas.IdAplicacion.ToString(), idAplicacion)
         .AppendFormatLine("   AND {0} IN ({1})", Entidades.VersionScript.Columnas.NroVersion.ToString(), cons)
      End With

      Me.Execute(myQuery.ToString())
   End Sub

   Public Function GetScriptsAEjecutar(idAplicacion As String, desde As Version, hasta As Version) As DataTable
      Dim myQuery As StringBuilder = New StringBuilder()
      With myQuery
         Me.SelectTexto(myQuery, 0)
         .AppendFormat(" where V.IdAplicacion = '{0}'", idAplicacion.Trim())
         .AppendFormat(" and NroVersion between '{0}.{1}.{2}.{3}'",
                                             desde.Major.ToString("00"),
                                             desde.Minor.ToString("00"),
                                             desde.Build.ToString("00"),
                                             desde.Revision.ToString("0"))
         .AppendFormat(" and '{0}.{1}.{2}.{3}'",
                                             hasta.Major.ToString("00"),
                                             hasta.Minor.ToString("00"),
                                             hasta.Build.ToString("00"),
                                             hasta.Revision.ToString("0"))

         .AppendLine(" ORDER BY idAplicacion, CAST(replace(replace(V.nroversion,'.',''),',','') AS bigint) ASC")
      End With
      Return Me.GetDataTable(myQuery.ToString())
   End Function

End Class
