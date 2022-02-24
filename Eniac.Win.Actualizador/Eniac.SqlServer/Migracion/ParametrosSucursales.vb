Namespace SqlServer
   Public Class ParametrosSucursales
      Inherits Comunes

      Public Sub New(ByVal da As Eniac.Datos.DataAccess)
         MyBase.New(da)
      End Sub
      Private Sub SelectTexto(stb As StringBuilder)
         With stb
            .AppendFormatLine("SELECT PS.*")
            .AppendFormatLine("      ,P.CategoriaParametro")
            .AppendFormatLine("      ,P.DescripcionParametro")
            .AppendFormatLine("      ,P.ValorParametro AS ValorParametroOriginal")
            .AppendFormatLine("  FROM {0} AS PS", Entidades.ParametroSucursal.NombreTabla)
            .AppendFormatLine(" LEFT JOIN Parametros AS P ON P.IdParametro = PS.IdParametro")
         End With
      End Sub

#Region "Insert/Update/Merge/Delete"
      Public Sub ParametrosSucursales_I(idEmpresa As Integer,
                                     idSucursal As Integer,
                                     idParametro As String,
                                     valorParametro As String)
         Dim myQuery As StringBuilder = New StringBuilder()
         With myQuery
            .AppendFormatLine("INSERT INTO  {0}  (", Entidades.ParametroSucursal.NombreTabla)
            .AppendFormatLine("             {0} ", Entidades.ParametroSucursal.Columnas.IdEmpresa.ToString())
            .AppendFormatLine("           , {0} ", Entidades.ParametroSucursal.Columnas.IdSucursal.ToString())
            .AppendFormatLine("           , {0} ", Entidades.ParametroSucursal.Columnas.IdParametro.ToString())
            .AppendFormatLine("           , {0} ", Entidades.ParametroSucursal.Columnas.ValorParametro.ToString())
            .AppendFormatLine("           ) VALUES (")
            .AppendFormatLine("             {0} ", idEmpresa)
            .AppendFormatLine("           , {0} ", idSucursal)
            .AppendFormatLine("           ,'{0}'", idParametro)
            .AppendFormatLine("           ,'{0}'", valorParametro)
            .AppendFormatLine("           )")
         End With
         Me.Execute(myQuery.ToString())
      End Sub

      Public Sub ParametrosSucursales_U(idEmpresa As Integer,
                                     idSucursal As Integer,
                                     idParametro As String,
                                     valorParametro As String)
         Dim myQuery As StringBuilder = New StringBuilder()
         With myQuery
            .AppendFormatLine("UPDATE {0}", Entidades.ParametroSucursal.NombreTabla)
            .AppendFormatLine("   SET {0} = '{1}'", Entidades.ParametroSucursal.Columnas.ValorParametro.ToString(), valorParametro)
            .AppendFormatLine(" WHERE {0} =  {1} ", Entidades.ParametroSucursal.Columnas.IdEmpresa.ToString(), idEmpresa)
            .AppendFormatLine("   AND {0} =  {1} ", Entidades.ParametroSucursal.Columnas.IdSucursal.ToString(), idSucursal)
            .AppendFormatLine("   AND {0} = '{1}'", Entidades.ParametroSucursal.Columnas.IdParametro.ToString(), idParametro)
         End With
         Me.Execute(myQuery.ToString())
      End Sub

      Public Sub ParametrosSucursales_M(idEmpresa As Integer,
                                     idSucursal As Integer,
                                     idParametro As String,
                                     valorParametro As String)
         Dim myQuery As StringBuilder = New StringBuilder()
         With myQuery
            .AppendFormatLine("MERGE INTO ParametrosSucursales AS D")
            .AppendFormatLine("        USING (SELECT  {1}  AS {0}", Entidades.ParametroSucursal.Columnas.IdEmpresa.ToString(), idEmpresa)
            .AppendFormatLine("                    ,  {1}  AS {0}", Entidades.ParametroSucursal.Columnas.IdSucursal.ToString(), idSucursal)
            .AppendFormatLine("                    , '{1}' AS {0}", Entidades.ParametroSucursal.Columnas.IdParametro.ToString(), idParametro)
            .AppendFormatLine("                    , '{1}' AS {0}", Entidades.ParametroSucursal.Columnas.ValorParametro.ToString(), valorParametro)
            .AppendFormatLine("              ) AS O ON O.IdParametro = D.IdParametro")
            .AppendFormatLine("    WHEN MATCHED THEN")
            .AppendFormatLine("        UPDATE SET D.ValorParametro = O.ValorParametro")
            .AppendFormatLine("    WHEN NOT MATCHED THEN ")
            .AppendFormatLine("        INSERT (IdEmpresa, IdSucursal, IdParametro, ValorParametro) VALUES (O.IdEmpresa, O.IdSucursal, O.IdParametro, O.ValorParametro)")
            .AppendFormatLine(";")
         End With
         Me.Execute(myQuery.ToString())
      End Sub

      Public Sub ParametrosSucursales_D(idEmpresa As Integer,
                                     idSucursal As Integer,
                                     idParametro As String)
         Dim myQuery As StringBuilder = New StringBuilder()
         With myQuery
            .AppendFormat("DELETE FROM {0} ", Entidades.ParametroSucursal.NombreTabla)
            .AppendFormat(" WHERE {0} =  {1} ", Entidades.ParametroSucursal.Columnas.IdEmpresa.ToString(), idEmpresa)
            If idSucursal > 0 Then
               .AppendFormat("   AND {0} =  {1} ", Entidades.ParametroSucursal.Columnas.IdSucursal.ToString(), idSucursal)
            End If
            If Not String.IsNullOrWhiteSpace(idParametro) Then
               .AppendFormat("   AND {0} = '{1}'", Entidades.ParametroSucursal.Columnas.IdParametro.ToString(), idParametro)
            End If
         End With
         Me.Execute(myQuery.ToString())
      End Sub
#End Region

#Region "GetAll"
      Public Function ParametrosSucursales_GA(idEmpresa As Integer, idSucursal As Integer) As DataTable
         Return ParametrosSucursales_G(idEmpresa, idSucursal, idParametro:=String.Empty, idExacto:=False)
      End Function
      Public Function ParametrosSucursales_GA(idEmpresa As Integer, idSucursal As Integer, idParametro As String, idExacto As Boolean) As DataTable
         Return ParametrosSucursales_G(idEmpresa, idSucursal, idParametro, idExacto)
      End Function
      Private Function ParametrosSucursales_G(idEmpresa As Integer,
                                           idSucursal As Integer,
                                           idParametro As String,
                                           idExacto As Boolean) As DataTable
         Dim myQuery As StringBuilder = New StringBuilder()
         With myQuery
            Me.SelectTexto(myQuery)
            .AppendLine(" WHERE 1 = 1")
            If idEmpresa > 0 Then
               .AppendFormatLine("   AND PS.{0} = {1}", Entidades.ParametroSucursal.Columnas.IdEmpresa.ToString(), idEmpresa)
            End If
            If idSucursal > 0 Then
               .AppendFormatLine("   AND PS.{0} = {1}", Entidades.ParametroSucursal.Columnas.IdSucursal.ToString(), idSucursal)
            End If
            If Not String.IsNullOrWhiteSpace(idParametro) Then
               If idExacto Then
                  .AppendFormatLine("   AND PS.{0} = '{1}'", Entidades.ParametroSucursal.Columnas.IdParametro.ToString(), idParametro)
               Else
                  .AppendFormatLine("   AND PS.{0} LIKE '%{1}%'", Entidades.ParametroSucursal.Columnas.IdParametro.ToString(), idParametro)
               End If
            End If
         End With
         Return Me.GetDataTable(myQuery.ToString())
      End Function
      Public Function ParametrosSucursales_G1(idEmpresa As Integer,
                                           idSucursal As Integer,
                                           idParametro As String) As DataTable
         Return ParametrosSucursales_G(idEmpresa, idSucursal, idParametro, idExacto:=True)
      End Function

      Public Overloads Function Buscar(columna As String, valor As String) As DataTable
         If columna = "CategoriaParametro" Or columna = "DescripcionParametro" Then
            columna = "P." + columna
         ElseIf columna = "ValorParametroOriginal" Then
            columna = "P.ValorParametro"
         Else
            columna = "PS." + columna
         End If
         Dim stb As StringBuilder = New StringBuilder()
         With stb
            Me.SelectTexto(stb)
            .AppendFormatLine(FormatoBuscar, columna, valor)
         End With
         Return Me.GetDataTable(stb.ToString())
      End Function
#End Region
   End Class

End Namespace

