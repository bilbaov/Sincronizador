


Namespace SqlServer

   Public Class Generales
      Inherits Comunes

      'Dim SQL As New SQLDMO.SQLServer
      'Dim WithEvents BK As New SQLDMO.Backup
      Public Sub New(da As Eniac.Datos.DataAccess)
         MyBase.New(da)
      End Sub

      Public Sub BackupEn(sBaseDatos As String,
                          sPathDestino As String,
                          callback As Action(Of Integer))

         Dim myQuery = New StringBuilder("")

         If String.IsNullOrEmpty(sPathDestino) Then
            sPathDestino = Entidades.Publicos.DriverBase
         End If

         With myQuery
            .Append(" BACKUP DATABASE ")
            .Append(sBaseDatos)
            .Append(" TO DISK= N'")
            .Append(sPathDestino)
            .Append("' WITH INIT")
            .Append(" , COPY_ONLY, NOFORMAT, NAME =  N'test-Full Database Backup', SKIP, NOREWIND, NOUNLOAD , STATS=10 ")
         End With

         With _da
            .Command.CommandText = myQuery.ToString()
            .Command.CommandType = CommandType.Text
            .Command.Connection = _da.Connection
         End With
         AddHandler DirectCast(Me._da.Connection, SqlClient.SqlConnection).InfoMessage,
                                                                            Sub(sender, e)
                                                                               Dim sPorcentaje = 0
                                                                               Select Case e.Errors.Count
                                                                                  Case 0 To 8
                                                                                     Dim oPorcent = e.Errors(e.Errors.Count).ToString()
                                                                                     sPorcentaje = Int32.Parse(oPorcent.Substring(0, 2))
                                                                                  Case > 8
                                                                                     sPorcentaje = 100
                                                                               End Select
                                                                               callback(sPorcentaje)
                                                                            End Sub
         Me._da.ExecuteNonQuery(Me._da.Command)

      End Sub

      Public Sub RetoreEn(ByVal nombreBase As String, ByVal path As String)
         Dim myQuery = New StringBuilder("")
         If String.IsNullOrEmpty(path) Then
            path = Entidades.Publicos.DriverBase
         End If

         With myQuery
            .Append("ALTER DATABASE ")
            .Append(nombreBase)
            .Append(" SET SINGLE_USER WITH ROLLBACK IMMEDIATE")
         End With

         Me.Execute(myQuery.ToString())

         With myQuery
            .Length = 0
            .Append(" RESTORE DATABASE ")
            .Append(nombreBase)
            .Append(" FROM  DISK = N'")
            .Append(path)
            .Append("'")
         End With

         Me.Execute(myQuery.ToString())

         With myQuery
            .Append("ALTER DATABASE ")
            .Append(nombreBase)
            .Append(" SET MULTI_USER")
         End With

         Me.Execute(myQuery.ToString())

      End Sub

      Public Function GetTamanoDB() As Long
         Dim dt = Me.GetDataTable("sp_spaceused")
         Dim val = Decimal.Parse(dt.Rows(0)("database_size").ToString().Replace("MB", ""))
         val *= 1000
         Return Decimal.ToInt64(val)
      End Function

      Public Function GetProcesoBackupRestore(sSessionId As Integer, sTipoProceso As String) As DataTable
         Dim myQuery = New StringBuilder("")
         With myQuery
            .Append(" SELECT session_id, CONVERT(NUMERIC(6,2),	r.percent_complete) AS Porcentaje FROM sys.dm_exec_requests r ")
            Select Case sTipoProceso
               Case "BACKUP"
                  .AppendFormat(" command IN ('backup DATABASE') AND session_id = {0}", sSessionId)
               Case "RESTORE"
                  .AppendFormat(" command IN ('restore DATABASE') AND session_id = {0}", sSessionId)
            End Select
         End With
         Return Me.GetDataTable(myQuery.ToString())
      End Function

      Public Function GetDBs() As DataTable
         Dim myQuery = New StringBuilder("")
         With myQuery
            .Append(" SELECT name FROM master.sys.databases ")
            .Append(" WHERE name LIKE 'SIGA%'")
         End With
         Return Me.GetDataTable(myQuery.ToString())
      End Function

      Public Function GetTotasLasDBs(ByVal filtroNombre As String) As DataTable
         Dim myQuery = New StringBuilder("")
         With myQuery
            .Append(" SELECT name FROM master.sys.databases ")
            .AppendFormat(" WHERE name LIKE '{0}%'", filtroNombre)
         End With
         Return Me.GetDataTable(myQuery.ToString())
      End Function

      Public Function GetServerDBFechaHora() As DateTime
         Dim myQuery = New StringBuilder("")
         With myQuery
            .Append(" Select GETDATE() AS FechaHora ")
         End With
         Dim dt As DataTable = Me.GetDataTable(myQuery.ToString())
         My.Application.Log.WriteEntry("Generales - GetServerDBFechaHora - Dato=" + dt.Rows(0)("FechaHora").ToString(), TraceEventType.Verbose)
         Return DateTime.Parse(dt.Rows(0)("FechaHora").ToString())

      End Function

      Public Function GetMotorDB() As String
         Dim stb = New StringBuilder()
         With stb
            .Append("Select @@version as motor")
         End With
         Return Me.GetDataTable(stb.ToString()).Rows(0)("motor").ToString()
      End Function

      Public Function GetMotorVersion() As String
         Dim stb = New StringBuilder()
         With stb
            .Append("SELECT SERVERPROPERTY('productversion') as version")
         End With
         Return Me.GetDataTable(stb.ToString()).Rows(0)("version").ToString()
      End Function

      Public Function ExisteTabla(nombreBase As String, esquema As String, nombreTabla As String) As Boolean
         If Not String.IsNullOrWhiteSpace(nombreBase) Then
            nombreBase = String.Concat(nombreBase.Trim("."c), ".")
         End If
         If String.IsNullOrWhiteSpace(esquema) Then
            esquema = "dbo"
         End If
         Dim stb = New StringBuilder()
         With stb
            .AppendFormatLine("SELECT *")
            .AppendFormatLine("  FROM {0}INFORMATION_SCHEMA.TABLES", nombreBase)
            .AppendFormatLine(" WHERE TABLE_SCHEMA = '{0}' AND TABLE_NAME = '{1}'", esquema, nombreTabla)
         End With
         Return GetDataTable(stb.ToString()).Rows.Count > 0
      End Function

   End Class

End Namespace
