Namespace SqlServer
   Public Class Parametros
      Inherits Comunes
      Public Sub New(da As Eniac.Datos.DataAccess)
         MyBase.New(da)
      End Sub
      Public Function GetValor(idEmpresa As Integer, idParametro As String) As String
         Dim stb = New StringBuilder()
         With stb
            .AppendFormatLine("SELECT ValorParametro ")
            .AppendFormatLine("  FROM Parametros")
            .AppendFormatLine(" WHERE IdEmpresa = {0}", idEmpresa)
            .AppendFormatLine("   AND IdParametro = '{0}'", idParametro.ToUpper().Trim())
         End With
         Dim dt As DataTable = Me.GetDataTable(stb.ToString())
         If dt.Rows.Count > 0 Then
            Return dt.Rows(0)("ValorParametro").ToString().Trim()
         Else
            Throw New Exception(String.Format("ATENCION: NO existe el Parametro '{1}' y NO podrá continuar.{0}{0}Por favor contáctese con Sistemas.", Environment.NewLine, idParametro.ToUpper().Trim()))
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
      Public Function Parametros_G(idEmpresa As Integer, idParametro As String, categoriaParametro As String, idExacto As Boolean) As DataTable
         Dim myQuery = New StringBuilder()
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

      Public Sub Parametros_M1(idEmpresa As Integer, idParametro As String, valorParametro As String)
         Dim myQuery = New StringBuilder()

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

      ''' <summary>
      ''' Funcion de recuperacion de Sucursales Empresas Clientes.- --
      ''' </summary>
      ''' <returns></returns>
      Public Function BuscarEmpresaPrincipal() As DataTable
         Dim result As Integer = 0

         Dim stb = New StringBuilder()
         With stb
            .AppendFormat("SELECT EMP.IdEmpresa AS IdEmpresa, PAR.ValorParametro AS Cliente, EMP.NombreEmpresa as NombreEmpresa")
            .AppendFormat("    ,CASE WHEN SoyLaCentral IS NULL THEN 0 ELSE SoyLaCentral END AS Central ")
            .AppendFormat("   FROM EMPRESAS AS EMP")
            .AppendFormat("    INNER JOIN PARAMETROS AS PAR ON EMP.IdEmpresa = PAR.IdEmpresa")
            .AppendFormat("    LEFT JOIN (SELECT * FROM Sucursales WHERE SoyLaCentral = 1) AS SUC")
            .AppendFormat("         ON SUC.IdEmpresa = EMP.IdEmpresa")
            .AppendFormat("   WHERE IdParametro = 'CODIGOCLIENTESINERGIA'")
            .AppendFormat("   ORDER BY Central DESC")
         End With
         Return GetDataTable(stb.ToString())
      End Function

   End Class


End Namespace
