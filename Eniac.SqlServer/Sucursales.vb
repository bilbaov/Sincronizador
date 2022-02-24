Public Class Sucursales
   Inherits Comunes

   Public Sub New(ByVal da As Eniac.Datos.DataAccess)
      MyBase.New(da)
   End Sub

   Public Sub Sucursales_I(idSucursal As Integer,
                           nombre As String,
                           direccion As String,
                           idLocalidad As Integer,
                           telefono As String,
                           correo As String,
                           fechaInicioActividad As Date,
                           estoyAca As Boolean,
                           soyLaCentral As Boolean,
                           idSucursalAsociada As Integer,
                           colorSucursal As Integer,
                           logoSucursal As System.Drawing.Image,
                           direccionComercial As String,
                           idLocalidadComercial As Integer,
                           redesSociales As String,
                           idSucursalAsociadaPrecios As Integer,
                           publicarEnWeb As Boolean,
                           idEmpresa As Integer)
      Dim myQuery As StringBuilder = New StringBuilder()
      With myQuery
         .AppendLine("INSERT INTO Sucursales (")
         .AppendLine("      IdSucursal ")
         .AppendLine("     ,Id ")
         .AppendLine("     ,Nombre ")
         .AppendLine("     ,Direccion ")
         .AppendLine("     ,IdLocalidad ")
         .AppendLine("     ,Telefono ")
         .AppendLine("     ,Correo ")
         .AppendLine("     ,FechaInicioActiv ")
         .AppendLine("     ,EstoyAca ")
         .AppendLine("     ,SoyLaCentral ")
         .AppendLine("     ,IdSucursalAsociada ")
         .AppendLine("     ,ColorSucursal ")
         .AppendLine("     ,DireccionComercial ")
         .AppendLine("     ,IdLocalidadComercial ")
         .AppendLine("     ,RedesSociales ")
         .AppendLine("     ,IdSucursalAsociadaPrecios")
         .AppendLine("     ,PublicarEnWeb")
         .AppendLine("     ,IdEmpresa")
         .AppendLine(" ) VALUES (")
         .AppendLine(idSucursal.ToString() & ", ")
         .AppendLine(idSucursal.ToString() & ", ")
         .AppendLine("'" & nombre & "', ")
         .AppendLine("'" & direccion & "', ")
         .AppendLine(idLocalidad.ToString() & ", ")
         .AppendLine("'" & telefono & "', ")
         .AppendLine("'" & correo & "', ")
         .AppendLine("'" & Me.ObtenerFecha(fechaInicioActividad, False) & "', ")
         .AppendLine("'" & estoyAca.ToString() & "', ")
         .AppendLine("'" & soyLaCentral.ToString() & "', ")
         If idSucursalAsociada > 0 Then
            .AppendLine(idSucursalAsociada.ToString() & ", ")
         Else
            .AppendLine("NULL ,")
         End If

         .AppendLine(colorSucursal.ToString() & ", ")

         .AppendLine("'" & direccionComercial & "', ")
         .AppendLine(idLocalidadComercial.ToString() & ", ")
         .AppendLine("'" & redesSociales & "'")
         .AppendFormatLine(" ,{0}", GetStringFromNumber(idSucursalAsociadaPrecios))
         .AppendFormatLine(" ,{0}", GetStringFromBoolean(publicarEnWeb))
         .AppendFormatLine(" ,{0}", idEmpresa)
         .AppendLine(")")
      End With

      Me.Execute(myQuery.ToString())
      Me.Sincroniza_I(myQuery.ToString(), "Sucursales")
   End Sub

   Public Sub Sucursales_U(idSucursal As Integer,
                           nombre As String,
                           direccion As String,
                           idLocalidad As Integer,
                           telefono As String,
                           correo As String,
                           fechaInicioActividad As Date,
                           estoyAca As Boolean,
                           soyLaCentral As Boolean,
                           idSucursalAsociada As Integer,
                           colorSucursal As Integer,
                           logoSucursal As System.Drawing.Image,
                           direccionComercial As String,
                           idLocalidadComercial As Integer,
                           redesSociales As String,
                           idSucursalAsociadaPrecios As Integer,
                           publicarEnWeb As Boolean,
                           idEmpresa As Integer)
      Dim myQuery As StringBuilder = New StringBuilder()
      With myQuery
         .AppendLine("UPDATE Sucursales SET ")

         .AppendLine("  Nombre = '" & nombre & "', ")
         .AppendLine("  Direccion = '" & direccion & "', ")
         .AppendLine("  IdLocalidad = " & idLocalidad.ToString() & ", ")
         .AppendLine("  Telefono = '" & telefono & "', ")
         .AppendLine("  Correo = '" & correo & "', ")
         .AppendLine("  FechaInicioActiv = '" & Me.ObtenerFecha(fechaInicioActividad, False) & "', ")
         .AppendLine("  EstoyAca = '" & estoyAca.ToString() & "', ")
         .AppendLine("  SoyLaCentral = '" & soyLaCentral.ToString() & "', ")
         If idSucursalAsociada > 0 Then
            .AppendLine("  idSucursalAsociada = " & idSucursalAsociada.ToString() & ", ")
         Else
            .AppendLine("  idSucursalAsociada = NULL ,")
         End If
         .AppendLine("  ColorSucursal = " & colorSucursal.ToString() & ", ")
         .AppendLine("  DireccionComercial = '" & direccionComercial & "', ")
         .AppendLine("  IdLocalidadComercial = " & idLocalidadComercial.ToString() & ", ")
         .AppendLine("  RedesSociales = '" & redesSociales & "'")
         .AppendFormatLine(" ,IdSucursalAsociadaPrecios = {0}", GetStringFromNumber(idSucursalAsociadaPrecios))
         .AppendFormatLine(" ,PublicarEnWeb = {0}", GetStringFromBoolean(publicarEnWeb))
         .AppendFormatLine(" ,IdEmpresa = {0}", idEmpresa)
         .AppendLine(" WHERE IdSucursal = " & idSucursal.ToString())
      End With

      Me.Execute(myQuery.ToString())
      Me.GrabarLogo(logoSucursal, idSucursal)
      Me.Sincroniza_I(myQuery.ToString(), "Sucursales")
   End Sub

   Public Sub Sucursales_D(idSucursal As Integer)
      Dim myQuery As StringBuilder = New StringBuilder()
      With myQuery
         .AppendLine("DELETE FROM Sucursales")
         .AppendFormatLine("   WHERE IdSucursal = {0}", idSucursal)
      End With

      Me.Execute(myQuery.ToString())
      Me.Sincroniza_I(myQuery.ToString(), "Sucursales")
   End Sub

   Private Sub SelectTexto(stb As StringBuilder, joinFuncion As Boolean, incluirLogo As Boolean)
      With stb
         .AppendLine("SELECT S.IdSucursal")
         .AppendLine("      ,S.Id")
         .AppendLine("      ,S.Nombre")
         .AppendLine("      ,S.Direccion")
         .AppendLine("      ,S.IdLocalidad")
         .AppendLine("      ,L.NombreLocalidad")
         .AppendLine("      ,S.Telefono")
         .AppendLine("      ,S.Correo")
         .AppendLine("      ,S.FechaInicioActiv")
         .AppendLine("      ,S.EstoyAca")
         .AppendLine("      ,S.SoyLaCentral")
         .AppendLine("      ,S.IdSucursalAsociada")
         .AppendLine("      ,S.ColorSucursal")
         If incluirLogo Then
            .AppendLine("      ,S.LogoSucursal")
         Else
            .AppendLine("      ,NULL LogoSucursal")
         End If
         .AppendLine("      ,SA.Nombre AS NombreSucursalAsociada")
         .AppendLine("      ,S.DireccionComercial")
         .AppendLine("      ,S.IdLocalidadComercial")
         .AppendLine("      ,L1.NombreLocalidad AS NombreLocalidadComercial")
         .AppendLine("      ,S.RedesSociales")
         .AppendLine("      ,S.IdSucursalAsociadaPrecios")
         .AppendLine("      ,SAP.Nombre AS NombreSucursalAsociadaPrecios")
         .AppendLine("      ,S.PublicarEnWeb")
         .AppendLine("      ,S.IdEmpresa")
         .AppendLine("      ,EM.NombreEmpresa")
         .AppendLine("  FROM Sucursales S")
         .AppendLine(" INNER JOIN Empresas EM ON EM.IdEmpresa = S.IdEmpresa")
         .AppendLine("  LEFT JOIN Localidades L ON S.IdLocalidad = L.IdLocalidad")
         .AppendLine("  LEFT JOIN Localidades L1 ON S.IdLocalidadComercial = L1.IdLocalidad")
         .AppendLine("  LEFT OUTER JOIN Sucursales SA ON S.IdSucursalAsociada = SA.IdSucursal")
         .AppendLine("  LEFT OUTER JOIN Sucursales SAP ON S.IdSucursalAsociadaPrecios = SAP.IdSucursal")
         If joinFuncion Then
            .AppendFormatLine(" INNER JOIN UsuariosRoles UR ON UR.IdSucursal = S.IdSucursal")
            .AppendFormatLine(" INNER JOIN RolesFunciones RF ON RF.IdRol = UR.IdRol")
         End If
      End With
   End Sub

   Public Function Sucursales_GA(incluirLogo As Boolean) As DataTable
      Return Sucursales_G(id:=0, nombre:=String.Empty, idFuncion:=String.Empty, estoyAca:=Nothing, soyLaCentral:=Nothing, idSucursalExcluir:=0, publicarEnWeb:=Entidades.Publicos.SiNoTodos.TODOS, incluirLogo:=incluirLogo)
   End Function
   Public Function Sucursales_GA(idFuncion As String, publicarEnWeb As Entidades.Publicos.SiNoTodos, incluirLogo As Boolean) As DataTable
      Return Sucursales_G(id:=0, nombre:=String.Empty, idFuncion:=idFuncion, estoyAca:=Nothing, soyLaCentral:=Nothing, idSucursalExcluir:=0, publicarEnWeb:=publicarEnWeb, incluirLogo:=incluirLogo)
   End Function
   Public Function Sucursales_GA_Excluye(idSucursalExcluir As Integer, incluirLogo As Boolean) As DataTable
      Return Sucursales_G(id:=0, nombre:=String.Empty, idFuncion:=String.Empty, estoyAca:=Nothing, soyLaCentral:=Nothing, idSucursalExcluir:=idSucursalExcluir, publicarEnWeb:=Entidades.Publicos.SiNoTodos.TODOS, incluirLogo:=incluirLogo)
   End Function

   Public Function Sucursales_G1(idSucursal As Integer, incluirLogo As Boolean) As DataTable
      Return Sucursales_G(idSucursal, nombre:=String.Empty, idFuncion:=String.Empty, estoyAca:=Nothing, soyLaCentral:=Nothing, idSucursalExcluir:=0, publicarEnWeb:=Entidades.Publicos.SiNoTodos.TODOS, incluirLogo:=incluirLogo)
   End Function

   Public Function Sucursales_GA(estoyAca As Boolean?, soyLaCentral As Boolean?, incluirLogo As Boolean) As DataTable
      Return Sucursales_G(id:=0, nombre:=String.Empty, idFuncion:=String.Empty, estoyAca:=estoyAca, soyLaCentral:=soyLaCentral, idSucursalExcluir:=0, publicarEnWeb:=Entidades.Publicos.SiNoTodos.TODOS, incluirLogo:=incluirLogo)
   End Function

   Public Function GetPorCodigoNombre(id As Integer, nombre As String, idFuncion As String, incluirLogo As Boolean) As DataTable
      Return Sucursales_G(id, nombre, idFuncion, estoyAca:=Nothing, soyLaCentral:=Nothing, idSucursalExcluir:=0, publicarEnWeb:=Entidades.Publicos.SiNoTodos.TODOS, incluirLogo:=incluirLogo)
   End Function
   Private Function Sucursales_G(id As Integer, nombre As String, idFuncion As String, estoyAca As Boolean?, soyLaCentral As Boolean?, idSucursalExcluir As Integer, publicarEnWeb As Entidades.Publicos.SiNoTodos, incluirLogo As Boolean) As DataTable
      Dim myQuery As StringBuilder = New StringBuilder()

      With myQuery
         Me.SelectTexto(myQuery, Not String.IsNullOrWhiteSpace(idFuncion), incluirLogo)
         .AppendLine(" WHERE 1 = 1")
         If id > 0 Then
            .AppendFormat("   AND S.IdSucursal = {0}", id).AppendLine()
         End If
         If Not String.IsNullOrWhiteSpace(nombre) Then
            .AppendFormat("   AND S.Nombre LIKE '%{0}%'", nombre).AppendLine()
         End If

         If Not String.IsNullOrWhiteSpace(idFuncion) Then
            .AppendFormatLine("   AND RF.IdFuncion = '{0}'", idFuncion)
            .AppendFormatLine("   AND UR.IdUsuario = '{0}'", Entidades.Usuario.Actual.Nombre)
         End If

         If estoyAca.HasValue Then
            .AppendFormatLine("   AND S.EstoyAca = {0}", GetStringFromBoolean(estoyAca.Value))
         End If

         If soyLaCentral.HasValue Then
            .AppendFormatLine("   AND S.SoyLaCentral = {0}", GetStringFromBoolean(soyLaCentral.Value))
         End If

         If idSucursalExcluir > 0 Then
            .AppendFormatLine("   AND S.IdSucursal <> {0}", idSucursalExcluir)
         End If

         If publicarEnWeb <> Entidades.Publicos.SiNoTodos.TODOS Then
            .AppendFormatLine("   AND S.PublicarEnWeb = {0}", GetStringFromBoolean(publicarEnWeb = Entidades.Publicos.SiNoTodos.SI))
         End If

         .AppendLine("  ORDER BY S.Nombre")
      End With

      Return Me.GetDataTable(myQuery.ToString())

   End Function

   Public Sub GrabarLogo(imagen As System.Drawing.Image, idSucursal As Integer)
      If Not System.IO.Directory.Exists(Entidades.Publicos.DriverBase + "Eniac\") Then
         System.IO.Directory.CreateDirectory(Entidades.Publicos.DriverBase + "Eniac\")
      End If
      Dim path As String = String.Format("{0}{1}\{2}{3}", Entidades.Publicos.DriverBase, "Eniac", idSucursal, ".jpg")

      Dim stbQuery As StringBuilder = New StringBuilder("")

      If imagen IsNot Nothing Then
         imagen.Save(path)

         Dim fsArchivo As System.IO.FileStream = New System.IO.FileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.Read)

         Dim iFileLength As Integer = Convert.ToInt32(fsArchivo.Length)

         Dim logo(Int32.Parse(fsArchivo.Length.ToString())) As Byte

         fsArchivo.Read(logo, 0, iFileLength)

         fsArchivo.Close()

         With stbQuery
            .Append(" UPDATE Sucursales ")
            .Append(" SET LogoSucursal = ")
            .Append(" @Logo ")
            .AppendFormat(" WHERE idSucursal = '{0}'", idSucursal)
         End With

         Me._da.Command.CommandText = stbQuery.ToString()
         Me._da.Command.CommandType = CommandType.Text
         Dim oParameter As Data.Common.DbParameter = Me._da.Command.CreateParameter()
         oParameter.ParameterName = "@Logo"
         oParameter.DbType = DbType.Binary
         oParameter.Size = logo.Length
         oParameter.Value = logo
         Me._da.Command.Parameters.Add(oParameter)
         Me._da.Command.Connection = Me._da.Connection
         Me._da.ExecuteNonQuery(Me._da.Command)
      Else
         With stbQuery
            .Append(" UPDATE Sucursales ")
            .Append(" SET LogoSucursal = ")
            .Append(" null ")
            .AppendFormat(" WHERE idSucursal = '{0}'", idSucursal)
         End With

         Me.Execute(stbQuery.ToString())
      End If

   End Sub

   Public Function GetSoloIdsDeTodas() As List(Of Integer)

      Dim stbQuery As StringBuilder = New StringBuilder("")

      With stbQuery
         .Length = 0
         .AppendLine("SELECT S.IdSucursal")
         .AppendLine("  FROM Sucursales S")
         .AppendLine("  ORDER BY S.IdSucursal")
      End With

      Dim dt As DataTable = Me.GetDataTable(stbQuery.ToString())

      Dim ids As List(Of Integer) = New List(Of Integer)()
      For Each dr As DataRow In dt.Rows
         ids.Add(Int32.Parse(dr("IdSucursal").ToString()))
      Next

      Return ids

   End Function

   Public Sub EjecutaLimpiezaDeSucursales(idSucursal As Integer)
      Dim stb As StringBuilder = New StringBuilder()
      With stb
         .AppendFormat("EXECUTE dbo.LimpiaSucursal @IdSucursal")
      End With

      Me._da.Command.CommandText = stb.ToString()
      Me._da.Command.CommandType = CommandType.Text
      Dim oParameter As Data.Common.DbParameter = Me._da.Command.CreateParameter()
      oParameter.ParameterName = "@IdSucursal"
      oParameter.DbType = DbType.Int32
      oParameter.Value = idSucursal
      Me._da.Command.Parameters.Add(oParameter)
      Me._da.Command.Connection = Me._da.Connection
      Me._da.ExecuteNonQuery(Me._da.Command)
   End Sub

   Public Function Buscar(ByVal columna As String, ByVal valor As String) As DataTable
      If columna = "NombreLocalidad" Then
         columna = "L." + columna
      ElseIf columna = "NombreLocalidadComercial" Then
         columna = "L1." + columna
      ElseIf columna = "NombreSucursalAsociada" Then
         columna = "SA." + columna
      ElseIf columna = "NombreSucursalAsociadaPrecios" Then
         columna = "SAP." + columna
      Else
         columna = "S." + columna
      End If
      Dim stb As StringBuilder = New StringBuilder()
      With stb
         Me.SelectTexto(stb, False, incluirLogo:=False)
         .AppendFormatLine(FormatoBuscar, columna, valor)
         .AppendFormatLine(" ORDER BY S.Nombre")
      End With
      Return Me.GetDataTable(stb.ToString())
   End Function

   Public Overloads Function GetCodigoMaximo() As Integer
      Return Convert.ToInt32(GetCodigoMaximo("IdSucursal", "Sucursales"))
   End Function

End Class