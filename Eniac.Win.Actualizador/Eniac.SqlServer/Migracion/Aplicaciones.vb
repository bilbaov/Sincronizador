Option Strict On
Option Explicit On
Imports Entidades

Namespace SqlServer
   Public Class Aplicaciones
      Inherits Comunes

      Public Sub New(ByVal da As Eniac.Datos.DataAccess)
         MyBase.New(da)
      End Sub

      Public Sub Aplicaciones_I(idAplicacion As String,
                          nombreAplicacion As String,
                          IdAplicacionBase As String)

         Dim myQuery As StringBuilder = New StringBuilder()

         With myQuery
            .AppendLine("INSERT INTO Aplicaciones")
            .Append("           (IdAplicacion")
            .Append("           ,NombreAplicacion")
            .Append("           ,IdAplicacionBase")
            .AppendLine(")     VALUES (")

            .AppendFormatLine("  '{0}'", idAplicacion)
            .AppendFormatLine("  ,'{0}'", nombreAplicacion)

            If Not String.IsNullOrEmpty(IdAplicacionBase) Then
               .AppendFormatLine("  ,'{0}'", IdAplicacionBase)
            Else
               .AppendFormatLine("  ,NULL")
            End If

            .Append(")")
         End With
         Me.Execute(myQuery.ToString())
      End Sub

      Public Sub Aplicaciones_U(idAplicacion As String,
                          nombreAplicacion As String,
                          IdAplicacionBase As String)

         Dim myQuery As StringBuilder = New StringBuilder()

         With myQuery
            .Append("UPDATE Aplicaciones")
            .Append("   SET ")
            .AppendFormat("      [NombreAplicacion] = '{0}'", nombreAplicacion)

            If Not String.IsNullOrEmpty(IdAplicacionBase) Then
               .AppendFormat("      ,[IdAplicacionBase] = '{0}'", IdAplicacionBase)
            Else
               .AppendFormat("      ,[IdAplicacionBase] = NULL")
            End If

            .AppendFormat(" WHERE [IdAplicacion] = '{0}'", idAplicacion)
         End With

         Me.Execute(myQuery.ToString())

      End Sub

      Public Sub Aplicaciones_D(IdAplicacion As String)
         Dim myQuery As StringBuilder = New StringBuilder()

         With myQuery
            .AppendFormat("DELETE FROM Aplicaciones WHERE {0} = '{1}'", Aplicacion.Columnas.IdAplicacion.ToString(), IdAplicacion)
         End With

         Me.Execute(myQuery.ToString())
      End Sub

      Private Sub SelectTexto(ByVal stb As StringBuilder)
         With stb
            .Append("SELECT P.IdAplicacion")
            .Append("      ,P.NombreAplicacion")
            .Append("      ,P.IdAplicacionBase")
            .Append("  FROM Aplicaciones P ")
         End With
      End Sub

      Public Function Aplicaciones_GA() As DataTable
         Dim myQuery As StringBuilder = New StringBuilder()
         With myQuery
            Me.SelectTexto(myQuery)
            .AppendLine(" ORDER BY P.NombreAplicacion")
         End With
         Return Me.GetDataTable(myQuery.ToString())
      End Function


      Public Function Aplicaciones_GA(idAplicacion As String, nombreAplicacion As String, idExacto As Boolean, nombreExacto As Boolean) As DataTable
         Dim myQuery As StringBuilder = New StringBuilder()
         With myQuery
            Me.SelectTexto(myQuery)
            .AppendFormatLine(" WHERE 1 = 1")
            If Not String.IsNullOrWhiteSpace(idAplicacion) Then
               If idExacto Then
                  .AppendFormatLine("   AND P.{0} = '{1}'", Entidades.Aplicacion.Columnas.IdAplicacion.ToString(), idAplicacion)
               Else
                  .AppendFormatLine("   AND P.{0} LIKE '%{1}%'", Entidades.Aplicacion.Columnas.IdAplicacion.ToString(), idAplicacion)
               End If
            End If
            If Not String.IsNullOrWhiteSpace(nombreAplicacion) Then
               If nombreExacto Then
                  .AppendFormatLine("   AND P.{0} = '{1}'", Entidades.Aplicacion.Columnas.NombreAplicacion.ToString(), nombreAplicacion)
               Else
                  .AppendFormatLine("   AND P.{0} LIKE '%{1}%'", Entidades.Aplicacion.Columnas.NombreAplicacion.ToString(), nombreAplicacion)
               End If
            End If
            .AppendLine(" ORDER BY P.NombreAplicacion")
         End With
         Return Me.GetDataTable(myQuery.ToString())
      End Function

      Public Function Aplicaciones_G1(IdAplicacion As String) As DataTable
         Dim stb As StringBuilder = New StringBuilder()
         With stb
            Me.SelectTexto(stb)
            .AppendFormat(" WHERE {0} = '{1}'", Aplicacion.Columnas.IdAplicacion.ToString(), IdAplicacion)
         End With
         Return Me.GetDataTable(stb.ToString())
      End Function

      Public Overloads Function Buscar(ByVal columna As String, ByVal valor As String) As DataTable
         Dim stb As StringBuilder = New StringBuilder()
         With stb
            Me.SelectTexto(stb)
            .AppendLine(" WHERE " & columna & " LIKE '%" & valor & "%'")
         End With
         Return Me.GetDataTable(stb.ToString())
      End Function

      Public Function GetFiltradoPorCodigoNombre(IdAplicacion As String, nombre As String, idAplicacionBase As String) As DataTable
         Dim stb As StringBuilder = New StringBuilder()

         Me.SelectTexto(stb)
         With stb
         End With
         Return Me.GetDataTable(stb.ToString())
      End Function

   End Class

End Namespace