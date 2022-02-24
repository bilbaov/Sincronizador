Public Class Publicos
   Inherits Eniac.Reglas.Base

#Region "Constructores"

   Private Shared _esCuitValido As Boolean

   Public Sub New()
      Me.NombreEntidad = "Publicos"
      da = New Datos.DataAccess()
   End Sub
   Public Sub New(ByVal accesoDatos As Datos.DataAccess)
      Me.NombreEntidad = "Publicos"
      da = accesoDatos
   End Sub

#End Region

#Region "Metodos Publicos"

   Public Shared ReadOnly Property OrigenCotizacionMoneda() As ServiciosRest.CotizacionMonedas.CotizacionMonedasOrigen
      Get
         Dim porDefecto As ServiciosRest.CotizacionMonedas.CotizacionMonedasOrigen = ServiciosRest.CotizacionMonedas.CotizacionMonedasOrigen.GeekLab
         Dim str As String = New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.ORIGENCOTIZACIONMONEDA.ToString(), porDefecto.ToString())
         Dim result As ServiciosRest.CotizacionMonedas.CotizacionMonedasOrigen
         Try
            result = DirectCast([Enum].Parse(GetType(ServiciosRest.CotizacionMonedas.CotizacionMonedasOrigen), str), ServiciosRest.CotizacionMonedas.CotizacionMonedasOrigen)
         Catch ex As Exception
            result = porDefecto
         End Try
         Return result
      End Get
   End Property

   Public Shared ReadOnly Property OperacionCotizacionMonedas() As ServiciosRest.CotizacionMonedas.CotizacionMonedasOperacion
      Get
         Dim porDefecto As ServiciosRest.CotizacionMonedas.CotizacionMonedasOperacion = ServiciosRest.CotizacionMonedas.CotizacionMonedasOperacion.Promedio
         Dim str As String = New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.OPERACIONCOTIZACIONMONEDAS.ToString(), porDefecto.ToString())
         Dim result As ServiciosRest.CotizacionMonedas.CotizacionMonedasOperacion
         Try
            result = DirectCast([Enum].Parse(GetType(ServiciosRest.CotizacionMonedas.CotizacionMonedasOperacion), str), ServiciosRest.CotizacionMonedas.CotizacionMonedasOperacion)
         Catch ex As Exception
            result = porDefecto
         End Try
         Return result
      End Get
   End Property

   Public Shared Function GetEnumString(value As System.Enum) As String
      Dim fi As Reflection.FieldInfo = value.GetType().GetField(value.ToString())
      Dim attributes = DirectCast(fi.GetCustomAttributes(GetType(ComponentModel.DescriptionAttribute), False), ComponentModel.DescriptionAttribute())
      If attributes IsNot Nothing AndAlso attributes.Length > 0 Then
         Return attributes(0).Description
      Else
         Return value.ToString()
      End If
   End Function

   Public Shared Function EsCuitValido(CUIT As String) As Boolean
      Return Not Validaciones.Impositivo.ValidarDigitoVerificador.Instancia.Validar(CUIT).Error
   End Function

   Private Shared dicAnchos As Dictionary(Of String, Integer) = New Dictionary(Of String, Integer)()
   Public Function GetAnchoCampo(tabla As String, campo As String) As Integer
      Dim key = String.Format("{0}.{1}", tabla, campo)
      If Not dicAnchos.ContainsKey(key) Then
         dicAnchos.Add(key, New SqlServer.Comunes(da).GetAnchoCampo(tabla, campo))
      End If
      Return dicAnchos(key)
   End Function

   Public Shared Function ConvierteChequesPropiosEnArray(ByVal ChequesPropios As List(Of Entidades.Cheque)) As ArrayList

      Dim eCheques As ArrayList = New ArrayList()

      For Each cheq As Entidades.Cheque In ChequesPropios
         eCheques.Add(cheq)
      Next

      Return eCheques

   End Function

   Public Shared Function ConvierteChequesTercerosEnArray(ByVal ChequesTerceros As List(Of Entidades.Cheque)) As ArrayList

      Dim eCheques As ArrayList = New ArrayList()

      For Each cheq As Entidades.Cheque In ChequesTerceros
         eCheques.Add(cheq)
      Next

      Return eCheques

   End Function

   Public Shared Function NormalizarNombreArchivo(textoOriginal As String) As String
      Return New RegularExpressions.Regex("[^a-zA-Z0-9._]").Replace(textoOriginal.Normalize(NormalizationForm.FormD), "")
   End Function
   Public Shared Function NormalizarDescripcion(textoOriginal As String) As String
      Return New RegularExpressions.Regex("[^a-zA-Z0-9., ]|[[{}]]").Replace(textoOriginal.Normalize(NormalizationForm.FormD), "")
   End Function

   Public Shared ReadOnly Property OrganizarEntregaFormaDePago() As Integer
      Get
         Return Integer.Parse(New Eniac.Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.ORGANIZARENTREGASFORMADEPAGO.ToString(), "3"))
      End Get
   End Property
   Public Shared ReadOnly Property OrganizarEntregaFiltraFechaEnvio() As Boolean
      Get
         Return New Eniac.Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.ORGANIZARENTREGAFILTRAFECHAENVIO.ToString(), Boolean.TrueString) = Boolean.TrueString
      End Get
   End Property

   Public Shared ReadOnly Property OrganizarEntregaPermiteDistintaFechaEnvio As Boolean
      Get
         Return Boolean.Parse(New Eniac.Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.ORGANIZARENTREGAPERMITEDISTINTAFECHAENVIO, Boolean.FalseString))
      End Get
   End Property

   Public Shared ReadOnly Property OrganizarEntregaFiltroImpreso() As Entidades.Publicos.SiNoTodos
      Get
         Try
            Dim str As String = New Eniac.Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.ORGANIZARENTREGAFILTROIMPRESO.ToString(), Entidades.Publicos.SiNoTodos.NO.ToString())
            Return DirectCast([Enum].Parse(GetType(Entidades.Publicos.SiNoTodos), str), Entidades.Publicos.SiNoTodos)
         Catch ex As Exception
            Return Entidades.Publicos.SiNoTodos.NO
         End Try
      End Get
   End Property
   Public Shared ReadOnly Property OrganizarEntregaFiltroFechaDesde() As Integer
      Get
         Return Integer.Parse(New Eniac.Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.ORGANIZARENTREGAFILTROFECHADESDE.ToString(), "0"))
      End Get
   End Property

   Public Shared ReadOnly Property FTPServidor() As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.FTPSERVIDOR.ToString(), "")
      End Get
   End Property

   Public Shared ReadOnly Property FTPUsuario() As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.FTPUSUARIO.ToString(), "")
      End Get
   End Property

   Public Shared ReadOnly Property FTPPassword() As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.FTPPASSWORD.ToString(), "")
      End Get
   End Property

   Public Shared ReadOnly Property FTPConexionPasiva() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.FTPCONEXIONPASIVA, "True"))
      End Get
   End Property

   Public Shared ReadOnly Property FTPFormato() As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.FTPFORMATO.ToString(), "WebExperto")
      End Get
   End Property

   Public Shared ReadOnly Property FTPNombreArchivo() As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.FTPNOMBREARCHIVO.ToString(), "")
      End Get
   End Property

   Public Shared ReadOnly Property FTPCarpetaRemota() As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.FTPCARPETAREMOTA.ToString(), "")
      End Get
   End Property

   Public Shared ReadOnly Property UbicacionPDFsFE() As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.UBICACIONPDFSFE.ToString(), "C:\ENIAC\AFIP\COMPROBANTES")
      End Get
   End Property

   Public Shared ReadOnly Property MailCopiaOcultaCompElectronico() As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.MAILCOPIAOCULTACOMPELECTRONICO.ToString(), "")
      End Get
   End Property

   Public Shared ReadOnly Property EnviaMailCopiaOcultaComprobanteElectronico() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.ENVIACOMAILCOMPELECTRONICO.ToString(), Boolean.TrueString))
      End Get
   End Property

#Region "Parametros solo para Sinergia"

   Public Shared ReadOnly Property FTPCarpetaRemota2() As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.FTPCARPETAREMOTA2.ToString(), "")
      End Get
   End Property

   Public Shared ReadOnly Property FTPPassword2() As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.FTPPASSWORD2.ToString(), "")
      End Get
   End Property

   Public Shared ReadOnly Property FTPSubeInfoAlservidor2() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.FTPSUBEINFOALSERVIDOR2.ToString(), "False"))
      End Get
   End Property

   Public Shared ReadOnly Property FTPServidor2() As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.FTPSERVIDOR2.ToString(), "")
      End Get
   End Property

   Public Shared ReadOnly Property FTPUsuario2() As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.FTPUSUARIO2.ToString(), "")
      End Get
   End Property

   Public Shared ReadOnly Property FTPConexionPasiva2() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.FTPCONEXIONPASIVA2, "True"))
      End Get
   End Property

#End Region

   Public Shared ReadOnly Property ExportarPrecioProductoUsandoMoneda() As Integer
      Get
         Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.EXPORTARPRECIOPRODUCTOUSANDOMONEDA.ToString(), "-1"))
      End Get
   End Property

   Public Shared ReadOnly Property ExportarProductosDecimalesRedondeoEnPrecio() As Integer
      Get
         Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.EXPORTARPRODUCTOSDECIMALESREDONDEOENPRECIO.ToString(),
                                                                 Facturacion.Redondeo.FacturacionDecimalesRedondeoEnPrecio.ToString()))
      End Get
   End Property

   Public Shared ReadOnly Property SolicitaModificarFormulaLuegoDelProducto() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.SOLICITAMODIFICARFORMULALUEGODELPRODUCTO.ToString(), Boolean.FalseString))
      End Get
   End Property

   Public Shared ReadOnly Property CRMAsuntoGenerarVersion As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.CRMASUNTOGENERARVERSION.ToString(), "{0} - Versión {1} generada")
      End Get
   End Property

   Public Shared ReadOnly Property CRMGenerarVersionPara As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.CRMGENERARVERSIONPARA.ToString(), String.Empty)
      End Get
   End Property

   Public Shared ReadOnly Property CRMGenerarVersionCopiaOculta As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.CRMGENERARVERSIONCOPIAOCULTA.ToString(), String.Empty)
      End Get
   End Property

   Public Shared ReadOnly Property ProcesosCorreoPruebaPara As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.PROCESOSCORREOPRUEBAPARA.ToString(), String.Empty)
      End Get
   End Property


#End Region

#Region "Propiedades Shared ReadOnly"

   Friend Const ValorDefaultParaUtilizaVencimientoSistema As String = "q4nVrxy20MT6M30GeDskTD58ZEf0Pt9lfvGemgrXCBfWqPVKgJubIClxX0Fn39MLr3q7fjolazYazvcHrhxhTr2BwjF4iPmNv4mS"
   Public Shared ReadOnly Property UtilizaVencimientoSistema As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.FACTURACIONUTILIZAMONEDADOLAR.ToString(), ValorDefaultParaUtilizaVencimientoSistema)
      End Get
   End Property

   Public Shared ReadOnly Property VersionDB() As String
      Get
         ParametrosCache.Instancia.LimpiarCache(Entidades.Parametro.Parametros.VERSIONDB)
         Return ParametrosCache.Instancia.GetValorPD(Entidades.Parametro.Parametros.VERSIONDB, "")
      End Get
   End Property

   Public Shared ReadOnly Property LoginMesesPreservarBitacoraError() As Integer
      Get
         Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.LOGINMESESPRESERVARBITACORAERROR, "3"))
      End Get
   End Property

   Public Shared ReadOnly Property MesesArchivarCRMFinalizados() As Integer
      Get
         Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.MESESARCHIVARCRMFINALIZADOS, "12"))
      End Get
   End Property

   Public Shared ReadOnly Property PreciosConIVA() As Boolean
      Get
         Return (New Reglas.Parametros().GetValorPD("PrecioConIVA", "SI").ToUpper() = "SI")
      End Get
   End Property

   Public Shared ReadOnly Property ExportarPreciosConIVA() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.EXPORTARPRECIOSCONIVA.ToString(), PreciosConIVA.ToString()))
      End Get
   End Property
   Public Shared ReadOnly Property ClientesSubirALaWeb() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.CLIENTESSUBIRALAWEB.ToString(), Boolean.TrueString))
      End Get
   End Property
   Public Shared ReadOnly Property ClientesBajarDeLaWeb() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.CLIENTESBAJARDELAWEB.ToString(), Boolean.TrueString))
      End Get
   End Property
   Public Shared ReadOnly Property ProductoUbicacionPorDefecto() As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.PRODUCTOUBICACIONPORDEFECTO.ToString(), "")
      End Get
   End Property

   Public Shared ReadOnly Property ProductosSubirALaWeb() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.PRODUCTOSSUBIRALAWEB.ToString(), Boolean.TrueString))
      End Get
   End Property
   Public Shared ReadOnly Property ProductosBajarDeLaWeb() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.PRODUCTOSBAJARDELAWEB.ToString(), Boolean.TrueString))
      End Get
   End Property
   Public Shared ReadOnly Property RubrosSubirALaWeb() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.RUBROSSUBIRALAWEB.ToString(), Boolean.TrueString))
      End Get
   End Property
   Public Shared ReadOnly Property RubrosBajarDeLaWeb() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.RUBROSBAJARDELAWEB.ToString(), Boolean.TrueString))
      End Get
   End Property
   Public Shared ReadOnly Property MarcasSubirALaWeb() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.MARCASSUBIRALAWEB.ToString(), Boolean.TrueString))
      End Get
   End Property
   Public Shared ReadOnly Property MarcasBajarDeLaWeb() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.MARCASBAJARDELAWEB.ToString(), Boolean.TrueString))
      End Get
   End Property
   Public Shared ReadOnly Property LocalidadesSubirALaWeb() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.LOCALIDADESSUBIRALAWEB.ToString(), Boolean.TrueString))
      End Get
   End Property
   Public Shared ReadOnly Property LocalidadesBajarDeLaWeb() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.LOCALIDADESBAJARDELAWEB.ToString(), Boolean.TrueString))
      End Get
   End Property
   Public Shared ReadOnly Property ProveedoresSubirALaWeb() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.PROVEEDORESSUBIRALAWEB.ToString(), Boolean.TrueString))
      End Get
   End Property
   Public Shared ReadOnly Property ProveedoresBajarDeLaWeb() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.PROVEEDORESBAJARDELAWEB.ToString(), Boolean.TrueString))
      End Get
   End Property
   Public Shared ReadOnly Property RubrosComprasSubirALaWeb() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.RUBROSCOMPRASSUBIRALAWEB.ToString(), Boolean.TrueString))
      End Get
   End Property
   Public Shared ReadOnly Property RubrosComprasBajarDeLaWeb() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.RUBROSCOMPRASBAJARDELAWEB.ToString(), Boolean.TrueString))
      End Get
   End Property
   Public Shared ReadOnly Property EjecutableTareaProgramada() As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.EJECUTABLETAREAPROGRAMADA.ToString(), String.Empty)
      End Get
   End Property

   Public Shared ReadOnly Property NombreEmpresa() As String
      Get
         Return New Reglas.Parametros().GetValorPD("NombreEmpresa", String.Empty)
      End Get
   End Property

   Public Shared ReadOnly Property NombreFantasia() As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.NOMBREFANTASIA.ToString(), String.Empty)
      End Get
   End Property

   Public Shared ReadOnly Property DireccionEmpresa() As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.SUELDOS_DOMICILIO_EMPRESA.ToString(), String.Empty)
      End Get
   End Property

   Public Shared ReadOnly Property EmpresaCurrentUICulture() As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.EMPRESACURRENTUICULTURE.ToString(), "es-AR")
      End Get
   End Property

   Public Shared ReadOnly Property ForzarListaDePreciosCliente() As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.FORZARLISTADEPRECIOSCLIENTE.ToString(), Boolean.FalseString)
      End Get
   End Property

   Public Shared ReadOnly Property FechaInicioActividades() As DateTime
      Get
         Dim fec As String = New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.FECHAINICIOACTIVIDADES.ToString(), "01/01/1900")
         If IsDate(fec) Then
            Return DateTime.Parse(fec)
         Else
            Return New DateTime(1900, 1, 1)
         End If
      End Get
   End Property

   Public Shared ReadOnly Property IngresosBrutos() As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.INGRESOSBRUTOS.ToString(), String.Empty)
      End Get
   End Property

   Public Shared ReadOnly Property CategoriaFiscalEmpresa() As Short
      Get
         Return Short.Parse(New Reglas.Parametros().GetValorPD("CategoriaFiscalEmpresa", "2"))
      End Get
   End Property

   Public Shared ReadOnly Property CategoriaFiscalEmpresaNombre() As String
      Get
         Return New Reglas.CategoriasFiscales().GetUno(CategoriaFiscalEmpresa).NombreCategoriaFiscal
      End Get
   End Property

   Public Shared ReadOnly Property CuitEmpresa() As String
      Get
         Return New Reglas.Parametros().GetValorPD("CuitEmpresa", String.Empty)
      End Get
   End Property

   Public Shared ReadOnly Property CodigoClienteSinergia() As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.CODIGOCLIENTESINERGIA.ToString(), "")
      End Get
   End Property

   Public Shared ReadOnly Property ClaveClienteSinergia() As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.CLAVECLIENTESINERGIA.ToString(), "")
      End Get
   End Property

   Public Shared Property NroHojaLibroIvaVentas() As Integer
      Get
         Return Integer.Parse(New Reglas.Parametros().GetValorPD("NroHojaLibroIvaVentas", "0"))
      End Get
      Set(ByVal value As Integer)
         Dim oParam As New Reglas.Parametros()
         oParam.SetValor(actual.Sucursal.IdEmpresa, "NroHojaLibroIvaVentas", value.ToString())
      End Set
   End Property

   Public Shared Property NroHojaLibroIvaCompras() As Integer
      Get
         Return Integer.Parse(New Reglas.Parametros().GetValorPD("NroHojaLibroIvaCompras", "0"))
      End Get
      Set(ByVal value As Integer)
         Dim oParam As New Reglas.Parametros()
         oParam.SetValor(actual.Sucursal.IdEmpresa, "NroHojaLibroIvaCompras", value.ToString())
      End Set
   End Property

   Public Shared Function GetSistema() As Entidades.Sistema
      Return GetSistema(actual.Sucursal.IdEmpresa)
   End Function
   Public Shared Function GetSistema(idEmpresa As Integer) As Entidades.Sistema
      Return (New Reglas.Parametros().GetSistema(idEmpresa))
   End Function

   Public Shared ReadOnly Property DiasValidezPresupuesto() As Integer
      Get
         Return Integer.Parse(New Reglas.Parametros().GetValorPD("DiasValidezPresupuesto", "0"))
      End Get
   End Property

   Public Shared ReadOnly Property ListaPreciosPredeterminada() As Integer
      Get
         Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.LISTAPRECIOSPREDETERMINADA, "0"))
      End Get
   End Property

   Public Shared ReadOnly Property IdTicketFiscal() As String
      Get
         Return New Reglas.Parametros().GetValorPD("IdTicketFiscal", String.Empty)
      End Get
   End Property

   Public Shared ReadOnly Property IdTicketFacturaFiscal() As String
      Get
         Return New Reglas.Parametros().GetValorPD("IdTicketFacturaFiscal", String.Empty)
      End Get
   End Property

   Public Shared ReadOnly Property IdNotaDebitoChequeRechazado() As String
      Get
         Return New Reglas.Parametros().GetValorPD("IdNDebCheqRech", String.Empty)
      End Get
   End Property

   Public Shared ReadOnly Property IdNotaDebitoChequeRechazadoCompra() As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.NDEBCHEQRECHCOM.ToString(), "NDEBCHEQRECHCOM")
      End Get
   End Property

   Public Shared ReadOnly Property IdNotaDebitoChequeRechazado2() As String
      Get
         Return New Reglas.Parametros().GetValorPD("IdNDebCheqRech2", String.Empty)
      End Get
   End Property

   Public Shared ReadOnly Property IdNotaDebitoGrabaLibro() As String
      Get
         Return New Reglas.Parametros().GetValorPD("IDNDEBGL", String.Empty)
      End Get
   End Property

   Public Shared ReadOnly Property IdNotaDebitoNoGrabaLibro() As String
      Get
         Return New Reglas.Parametros().GetValorPD("IDNDEBNOGL", String.Empty)
      End Get
   End Property

   Public Shared ReadOnly Property IdNotaCreditoGrabaLibro() As String
      Get
         Return New Reglas.Parametros().GetValorPD("IDNCREDGL", String.Empty)
      End Get
   End Property

   Public Shared ReadOnly Property IdNotaCreditoNoGrabaLibro() As String
      Get
         Return New Reglas.Parametros().GetValorPD("IDNCREDNOGL", String.Empty)
      End Get
   End Property

   Public Shared ReadOnly Property CodigoAutorizacionWS() As String
      Get
         Return New Reglas.Parametros().GetValorPD("CodigoAutorizaWS", String.Empty)
      End Get
   End Property

   Public Shared ReadOnly Property CantMaxItemsComprobFiscales() As Integer
      Get
         Return Integer.Parse(New Reglas.Parametros().GetValorPD("CantMaxItemsComprobFiscales", "1"))
      End Get
   End Property

   Public Shared ReadOnly Property NombreLogo() As String
      Get
         Return Entidades.Publicos.CarpetaEniac + "LOGOEMPRESA.jpg"
      End Get
   End Property

   Public Shared ReadOnly Property Logo(sucursal As Entidades.Sucursal) As System.Drawing.Image
      Get
         Dim nombreLogo As String = Publicos.NombreLogo

         If sucursal IsNot Nothing AndAlso sucursal.LogoSucursal IsNot Nothing Then
            sucursal.LogoSucursal.Save(nombreLogo)
            Return sucursal.LogoSucursal
         End If

         'Si la sucursal no tiene logo, se envía el de la Empresa, o en su defecto una imagen arbitraria (paper_clip_64)
         Dim parI As Reglas.ParametrosImagenes = New Reglas.ParametrosImagenes()
         Dim imglogo As System.Drawing.Image = parI.GetValor(Entidades.ParametroImagen.Parametros.LOGOEMPRESA)
         If imglogo IsNot Nothing Then
            imglogo.Save(nombreLogo)
         Else
            imglogo = My.Resources.paper_clip_64
            imglogo.Save(nombreLogo)
         End If
         Return imglogo
      End Get
   End Property
   Public Shared ReadOnly Property FacturacionVisualizaCantidadConvertida() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.FACTURACIONVISUALIZACANTIDADCONVERTIDA.ToString(), Boolean.FalseString))
      End Get
   End Property
   Public Shared ReadOnly Property FacturacionVisualizaConversion() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.FACTURACIONVISUALIZACONVERSION.ToString(), Boolean.FalseString))
      End Get
   End Property

   Public Shared ReadOnly Property VisualizaComprobantesDeVenta() As Boolean
      Get
         Return (New Reglas.Parametros().GetValorPD("VisualizaComprobantesDeVenta", "SI").ToUpper() = "SI")
      End Get
   End Property

   Public Shared ReadOnly Property VisualizaComprobantesDeCompra() As Boolean
      Get
         Return (New Reglas.Parametros().GetValorPD("VisualizaComprobantesDeCompra", "SI").ToUpper() = "SI")
      End Get
   End Property

   Public Shared ReadOnly Property VisualizaLote() As Boolean
      Get
         Return (New Reglas.Parametros().GetValorPD("VisualizaLote", "SI").ToUpper() = "SI")
      End Get
   End Property

   Public Shared ReadOnly Property VisualizaReciboDeCliente() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD("VisualizaRecibo", Boolean.TrueString))
      End Get
   End Property

   Public Shared ReadOnly Property TieneModuloCuentaCorrienteClientes() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD("MODULOCUENTACORRIENTECLIENTES", Boolean.FalseString))
      End Get
   End Property


   Public Shared ReadOnly Property DebitoAutomaticoSantanderCodigoServicio() As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.DEBITOAUTOMATICOSANTANDERCODIGOSERVICIO.ToString(), String.Empty)
      End Get
   End Property
   Public Shared ReadOnly Property DebitoAutomaticoCajaCtaBancariaTransfBanc() As Integer
      Get
         Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.DEBITOAUTOMATICOCAJACUENTABANCARIATRANSBANC.ToString(), "0"))
      End Get
   End Property
   Public Shared ReadOnly Property DebitoAutomaticoTipoReciboCtaBancariaTransf() As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.DEBITOAUTOMATICOTIPORECIBOCTABANCARIATRANF.ToString(), String.Empty)
      End Get
   End Property
   Public Shared ReadOnly Property DebitoAutomaticoCobradorCtaBancariaTransf() As Integer
      Get
         Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.DEBITOAUTOMATICOCOBRADORCTABANCARIATRANF.ToString(), "0"))
      End Get
   End Property
   Public Shared ReadOnly Property DebitoAutomaticoCuentaBancariaTransfBanc() As Integer
      Get
         Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.DEBITOAUTOMATICOCUENTABANCARIATRANSBANC.ToString(), "0"))
      End Get
   End Property
   Public Shared ReadOnly Property FTPServidorMovil() As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.FTPSERVIDORMOVIL, "")
      End Get
   End Property
   Public Shared ReadOnly Property FTPUsuarioMovil() As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.FTPUSUARIOMOVIL, "")
      End Get
   End Property
   Public Shared ReadOnly Property FTPPasswordMovil() As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.FTPPASSWORDMOVIL, "")
      End Get
   End Property
   Public Shared ReadOnly Property FTPConexionPasivaMovil() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.FTPCONEXIONPASIVAMOVIL.ToString(), Boolean.FalseString))
      End Get
   End Property
   Public Shared ReadOnly Property FTPCarpetaRemotaMovil() As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.FTPCARPETAREMOTAMOVIL, "")
      End Get
   End Property
   Public Shared ReadOnly Property SimovilCobranzaURLBase() As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.SIMOVILCOBRANZAURLBASE, "")
      End Get
   End Property
   Public Shared ReadOnly Property SimovilGestionTurnosURLBase() As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.SIMOVILGESTIONTURNOSURLBASE, "")
      End Get
   End Property
   Public Shared ReadOnly Property SimovilTurnosURLBase() As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.SIMOVILTURNOSURLBASE, "")
      End Get
   End Property
   Public Shared ReadOnly Property SimovilSoporteURLBase() As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.SIMOVILSOPORTEURLBASE, "")
      End Get
   End Property
   Public Shared ReadOnly Property SimovilSoporteUsuarioDefault() As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.SIMOVILSOPORTEUSUARIODEFAULT, "")
      End Get
   End Property

   Public Shared ReadOnly Property SimovilCobranzaTipoDireccion() As Integer
      Get
         Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.SIMOVILCOBRANZATIPODIRECCION, "-1"))
      End Get
   End Property

   Public Shared ReadOnly Property SimovilCobranzaNombreCliente() As ServiciosRest.CobranzasMovil.Clientes.NombreCliente
      Get
         Dim str As String = New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.SIMOVILCOBRANZANOMBRECLIENTE,
                                                                ServiciosRest.CobranzasMovil.Clientes.NombreCliente.NombreCliente.ToString())
         Dim result As ServiciosRest.CobranzasMovil.Clientes.NombreCliente
         Try
            result = DirectCast([Enum].Parse(GetType(ServiciosRest.CobranzasMovil.Clientes.NombreCliente), str), ServiciosRest.CobranzasMovil.Clientes.NombreCliente)
         Catch ex As Exception
            result = ServiciosRest.CobranzasMovil.Clientes.NombreCliente.NombreCliente
         End Try
         Return result
      End Get
   End Property

   Public Shared ReadOnly Property SimovilCobranzaIncluirClientes() As ServiciosRest.CobranzasMovil.Clientes.IncluirClientes
      Get
         Dim str As String = New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.SIMOVILCOBRANZAINCLUIRCLIENTES,
                                                                ServiciosRest.CobranzasMovil.Clientes.IncluirClientes.SoloClientesConDeuda.ToString())
         Dim result As ServiciosRest.CobranzasMovil.Clientes.IncluirClientes
         Try
            result = DirectCast([Enum].Parse(GetType(ServiciosRest.CobranzasMovil.Clientes.IncluirClientes), str), ServiciosRest.CobranzasMovil.Clientes.IncluirClientes)
         Catch ex As Exception
            result = ServiciosRest.CobranzasMovil.Clientes.IncluirClientes.SoloClientesConDeuda
         End Try
         Return result
      End Get
   End Property

   Public Class Simovil
      Public Class Subida
         Public Shared ReadOnly Property IncluirCuentasCorrientes() As Boolean
            Get
               Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.SIMOVILSUBIDAINCLUIRCUENTASCORRIENTES.ToString(), Boolean.TrueString))
            End Get
         End Property
         Public Shared ReadOnly Property IncluirCuentasCorrientesDebeHaber() As Boolean
            Get
               Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.SIMOVILSUBIDAINCLUIRCUENTASCORRIENTESDEBEHABER.ToString(), Boolean.FalseString))
            End Get
         End Property
         Public Shared ReadOnly Property IncluirRubros() As Boolean
            Get
               Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.SIMOVILSUBIDAINCLUIRRUBROS.ToString(), Boolean.FalseString))
            End Get
         End Property
         Public Shared ReadOnly Property IncluirProductos() As Boolean
            Get
               Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.SIMOVILSUBIDAINCLUIRPRODUCTOS.ToString(), Boolean.FalseString))
            End Get
         End Property
         Public Shared ReadOnly Property IncluirProductosPrecios() As Boolean
            Get
               Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.SIMOVILSUBIDAINCLUIRPRODUCTOSPRECIOS.ToString(), Boolean.FalseString))
            End Get
         End Property
         Public Shared ReadOnly Property IncluirProductosClientes() As Boolean
            Get
               Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.SIMOVILSUBIDAINCLUIRPRODUCTOSCLIENTES.ToString(), Boolean.FalseString))
            End Get
         End Property
         Public Shared ReadOnly Property IncluirUsuarios() As Boolean
            Get
               Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.SIMOVILSUBIDAINCLUIRUSUARIOS.ToString(), Boolean.FalseString))
            End Get
         End Property

         Public Shared ReadOnly Property IncluirCalendarios() As Boolean
            Get
               Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.SIMOVILSUBIDAINCLUIRCALENDARIOS, Boolean.FalseString))
            End Get
         End Property
         Public Shared ReadOnly Property IncluirEmbarcaciones() As Boolean
            Get
               Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.SIMOVILSUBIDAINCLUIREMBARCACIONES, Boolean.FalseString))
            End Get
         End Property
         Public Shared ReadOnly Property IncluirDestinos() As Boolean
            Get
               Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.SIMOVILSUBIDAINCLUIRDESTINOS, Boolean.FalseString))
            End Get
         End Property

         Public Shared ReadOnly Property AplicaPreciosOferta() As Boolean
            Get
               Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.SIMOVILSUBIDAAPLICAPRECIOSOFERTA.ToString(), Boolean.FalseString))
            End Get
         End Property
         Public Shared ReadOnly Property IncluirConfiguraciones() As Boolean
            Get
               Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.SIMOVILSUBIDAINCLUIRCONFIGURACIONES.ToString(), Boolean.FalseString))
            End Get
         End Property


         Private Const TamanoPaginaDefault As String = "1000"
         Public Shared ReadOnly Property PaginaRutas() As Integer
            Get
               Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.SIMOVILSUBIDAPAGINARUTAS.ToString(), TamanoPaginaDefault))
            End Get
         End Property
         Public Shared ReadOnly Property PaginaRutasClientes() As Integer
            Get
               Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.SIMOVILSUBIDAPAGINARUTASCLIENTES.ToString(), TamanoPaginaDefault))
            End Get
         End Property

         Public Shared ReadOnly Property PaginaRutasListasPrecios() As Integer
            Get
               Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.SIMOVILSUBIDAPAGINARUTASLISTASPRECIOS.ToString(), TamanoPaginaDefault))
            End Get
         End Property


         Public Shared ReadOnly Property PaginaClientes() As Integer
            Get
               Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.SIMOVILSUBIDAPAGINACLIENTES.ToString(), TamanoPaginaDefault))
            End Get
         End Property
         Public Shared ReadOnly Property PaginaCuentasCorrientes() As Integer
            Get
               Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.SIMOVILSUBIDAPAGINACUENTASCORRIENTES.ToString(), TamanoPaginaDefault))
            End Get
         End Property
         Public Shared ReadOnly Property PaginaCuentasCorrientesDebeHaber() As Integer
            Get
               Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.SIMOVILSUBIDAPAGINACUENTASCORRIENTESDEBEHABER.ToString(), TamanoPaginaDefault))
            End Get
         End Property
         Public Shared ReadOnly Property PaginaRubros() As Integer
            Get
               Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.SIMOVILSUBIDAPAGINARUBROS.ToString(), TamanoPaginaDefault))
            End Get
         End Property
         Public Shared ReadOnly Property PaginaProductos() As Integer
            Get
               Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.SIMOVILSUBIDAPAGINAPRODUCTOS.ToString(), TamanoPaginaDefault))
            End Get
         End Property
         Public Shared ReadOnly Property PaginaProductosPrecios() As Integer
            Get
               Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.SIMOVILSUBIDAPAGINAPRODUCTOSPRECIOS.ToString(), TamanoPaginaDefault))
            End Get
         End Property

      End Class

      Public Shared ReadOnly Property PreciosConIVA() As Boolean
         Get
            Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.SIMOVILPRECIOSCONIVA.ToString(), Publicos.ExportarPreciosConIVA.ToString()))
         End Get
      End Property


      Public Shared ReadOnly Property SoloProductosConStock() As Boolean
         Get
            Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.SIMOVILSOLOPRODUCTOSCONSTOCK.ToString(), Boolean.FalseString))
         End Get
      End Property

      Public Shared ReadOnly Property MesesEnviarCuentasCorrientesDebeHaber() As Integer
         Get
            Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.SIMOVILSUBIDAMESESENVIARCUENTASCORRIENTESDEBEHABER.ToString(), "6"))
         End Get
      End Property

      Public Shared ReadOnly Property MaximoCuotasEnviarCuentasCorrientes() As Integer
         Get
            Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.SIMOVILSUBIDAMAXIMOCUOTASENVIARCUENTASCORRIENTES.ToString(), "4"))
         End Get
      End Property

   End Class

   Public Shared ReadOnly Property TieneModuloCuentaCorrienteProveedores() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.MODULOCUENTACORRIENTEPROVEEDORES, Boolean.TrueString))
      End Get
   End Property

   Public Shared ReadOnly Property PreciosUnificar() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.PRECIOSUNIFICAR, "False").ToString())
      End Get
   End Property

   Public Shared ReadOnly Property PreciosPriorizaCodigoYDescripcion() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.PRECIOSPRIORIZACODIGOYDESCRIPCION, Boolean.TrueString))
      End Get
   End Property

   Public Shared ReadOnly Property UtilizaCuotasVencimientoCtaCteProveedores() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.UTILIZACUOTASOVENCIMIENTOCCPROVEEDORES, Boolean.TrueString))
      End Get
   End Property

   Public Shared ReadOnly Property TieneModuloCaja() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.MODULOCAJA, Boolean.TrueString))
      End Get
   End Property

   Public Shared ReadOnly Property CajaPlanillaNuevaFechaDelDia() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD("CAJAPLANILLAFECHADIA", "False"))
      End Get
   End Property

   Public Shared ReadOnly Property TieneModuloBanco() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.MODULOBANCO, Boolean.TrueString))
      End Get
   End Property

   Public Shared ReadOnly Property TipoDocumentoCliente() As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.TIPODOCUMENTOCLIENTE, "DNI")
      End Get
   End Property

   Public Shared ReadOnly Property TipoDocumentoProveedor() As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.TIPODOCUMENTOPROVEEDOR, Boolean.TrueString)
      End Get
   End Property

   Public Shared ReadOnly Property VentasConProductosEnCero() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.VENTASCONPRODUCTOSENCERO, Boolean.FalseString))
      End Get
   End Property
   Public Shared ReadOnly Property ComprasConProductosEnCero() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.COMPRASCONPRODUCTOSENCERO, Boolean.FalseString))
      End Get
   End Property

   Public Shared ReadOnly Property ImpresiónComprobantesMiraOrdenProductos() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD("FACTURACIONIMPRESIONORDENPRODUCTOS", Boolean.FalseString))
      End Get
   End Property

   Public Shared ReadOnly Property PedidosClientesOcultarRentabilidad() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.PEDIDOCULTARENTABILIDAD.ToString(), Boolean.FalseString))
      End Get
   End Property
   Public Shared ReadOnly Property PedidosMostrarCriticidad() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.PEDIDOSMOSTRARCRITICIDAD.ToString(), Boolean.TrueString))
      End Get
   End Property
   Public Shared ReadOnly Property PedidosPermiteModificarDescRecPedidos() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.PEDIDOSPERMITEMODIFICARDESCREC.ToString(), Boolean.TrueString))
      End Get
   End Property

   Public Shared ReadOnly Property PedidosMuestraProvHabitual() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.PEDIDOSMUESTRAPROVHABITUAL.ToString(), Boolean.FalseString))
      End Get
   End Property

   Public Shared ReadOnly Property ProductoEmbalajeHaciaArriba() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.PRODUCTOUTILIZAEMBALAJEARRIBA.ToString(), "False"))
      End Get
   End Property



   Public Shared ReadOnly Property PreFacturaConsumePedidos() As Boolean
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.PREFACTURACONSUMEPEDIDOS.ToString(), Boolean.FalseString) = Boolean.TrueString
      End Get
   End Property

   Public Shared ReadOnly Property ActualizaPreciosDeVenta() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.ACTUALIZAPRECIOSDEVENTA, Boolean.TrueString))
      End Get
   End Property

   Public Shared ReadOnly Property ActualizaPreciosCalculo() As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.ACTUALIZARPRECIOSCALCULO, "VENTA")
      End Get
   End Property

   Public Shared ReadOnly Property RecalculaValoracionesEstrellas() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.RECALCULAVALORACIONESESTRELLAS.ToString(), Boolean.FalseString))
      End Get
   End Property

   Public Shared ReadOnly Property ClienteMuestraCodigoClienteLetras() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD("CLIENTEMUESTRACODIGOLETRAS", Boolean.FalseString))
      End Get
   End Property

   Public Shared ReadOnly Property ClientePermiteModificarCodigo() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.CLIENTEPERMITEMODIFICARCODIGO.ToString(), Boolean.TrueString))
      End Get
   End Property
   Public Shared ReadOnly Property ClienteTieneTrabajo() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD("ClienteTieneTrabajo", Boolean.FalseString))
      End Get
   End Property

   Public Shared ReadOnly Property ClienteUtilizaOtros() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD("CLIENTEUTILIZAOTROS", Boolean.TrueString))
      End Get
   End Property
   Public Shared ReadOnly Property ClienteUtilizaContactos() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD("CLIENTEUTILIZACONTACTOS", Boolean.TrueString))
      End Get
   End Property

   Public Shared ReadOnly Property ClienteUtilizaAdjuntos() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD("CLIENTEUTILIZAADJUNTOS", Boolean.TrueString))
      End Get
   End Property

   Public Shared ReadOnly Property ClienteUtilizaImpuestos() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD("CLIENTEUTILIZAIMPUESTOS", Boolean.TrueString))
      End Get
   End Property

   Public Shared ReadOnly Property ClienteUtilizaHorarios() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD("CLIENTEUTILIZAHORARIOS", Boolean.TrueString))
      End Get
   End Property

   Public Shared ReadOnly Property ClienteCantidadVisitasPorDefecto() As Integer
      Get
         Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.CLIENTESCANTIDADVISITASPORDEFECTO, "1"))
      End Get
   End Property

   Public Shared ReadOnly Property ProduccionDivideTamano() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.PRODUCCIONDIVIDETAMANO.ToString(), "False"))
      End Get
   End Property

   Public Shared ReadOnly Property ClienteBuscarPorCodigoYNroDocumento() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.CLIENTEBUSCARPORCODIGOYNRODOCUMENTO.ToString(), "False"))
      End Get
   End Property
   Public Shared ReadOnly Property ClienteMostrarProductosAsociados() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.CLIENTEMOSTRARPRODUCTOSASOCIADOS.ToString(), "False"))
      End Get
   End Property
   Public Shared ReadOnly Property BuscaProductoPorProveedorHabitual() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.BUSCAPRODUCTOPORCODIGOPROVEEDORHABITUAL.ToString(), "False"))
      End Get
   End Property
   Public Shared ReadOnly Property ProductoBuscarPorCodigoExacto() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.PRODUCTOBUSCARPORCODIGOEXACTO.ToString(), "True"))
      End Get
   End Property

   Public Shared ReadOnly Property CantidadLineasDeProductosAEvaluarParaDescRecarg() As Integer
      Get
         Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.CANTIDADLINEASDEPRODUCTOAEVALUARPARADESCRECARGO.ToString(), "0"))
      End Get
   End Property

   Public Shared ReadOnly Property PorcentajeDeDescRecargPorLineaDeProducto() As Decimal
      Get
         Return Decimal.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.PORCENTAJEDEDESCRECARGOPORLINEADEPRODUCTO.ToString(), "0"))
      End Get
   End Property

   Public Shared ReadOnly Property EditaProductoNormalModificaHistorial() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.EDITAPRODUCTONORMALMODIFICAHISTORIAL.ToString(), Boolean.FalseString))
      End Get
   End Property
   Public Shared ReadOnly Property ClienteTieneGarante() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD("ClienteTieneGarante", Boolean.FalseString))
      End Get
   End Property

   Public Shared ReadOnly Property ProductoTieneModelo() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD("ProductoTieneModelo", Boolean.TrueString))
      End Get
   End Property

   Public Shared ReadOnly Property ContactosAgendaPrivada() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD("CONTACTOSAGENDAPRIVADA", Boolean.FalseString))
      End Get
   End Property

   Public Shared ReadOnly Property ProductoCodigoNumerico() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD("PRODUCTOCODIGONUMERICO", Boolean.FalseString))
      End Get
   End Property

   Public Shared ReadOnly Property HabilitaCodigoNumericoProducto() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.HABILITACODIGOPRODUCTONUMERICO.ToString(), Boolean.FalseString))
      End Get
   End Property

   Public Shared ReadOnly Property ProductoFormatoLikeBuscarPorCodigo() As Entidades.Publicos.FormatoLike
      Get
         Dim str As String = New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.PRODUCTOFORMATOLIKEBUSCARPORCODIGO.ToString(), Entidades.Publicos.FormatoLike.CONTIENE.ToString())
         Dim result As Entidades.Publicos.FormatoLike
         If [Enum].TryParse(str, result) Then
            Return result
         End If
         Return Entidades.Publicos.FormatoLike.CONTIENE
      End Get
   End Property

   Public Shared ReadOnly Property ProductoFormatoLikeBuscarPorCodigoResuelto() As String
      Get
         Dim formatoLike As String
         Dim usarFormatoLike As Entidades.Publicos.FormatoLike = Publicos.ProductoFormatoLikeBuscarPorCodigo
         Select Case usarFormatoLike
            Case Entidades.Publicos.FormatoLike.CONTIENE
               formatoLike = "%{0}%"
            Case Entidades.Publicos.FormatoLike.COMIENZA
               formatoLike = "{0}%"
            Case Entidades.Publicos.FormatoLike.FINALIZA
               formatoLike = "%{0}"
            Case Else
               Throw New ArgumentException(String.Format("Formato de Like ´{0}´ no implemetado. Por favor verifique la configuración.", usarFormatoLike.ToString()))
         End Select
         Return formatoLike
      End Get
   End Property

   Public Shared ReadOnly Property ProductoFormatoLikeBuscarPorNombre() As Entidades.Publicos.FormatoLike
      Get
         Dim str As String = New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.PRODUCTOFORMATOLIKEBUSCARPORNOMBRE.ToString(), Entidades.Publicos.FormatoLike.CONTIENE.ToString())
         Dim result As Entidades.Publicos.FormatoLike
         If [Enum].TryParse(str, result) Then
            Return result
         End If
         Return Entidades.Publicos.FormatoLike.CONTIENE
      End Get
   End Property

   Public Shared ReadOnly Property ProductoFormatoLikeBuscarPorNombreResuelto() As String
      Get
         Dim formatoLike As String
         Dim usarFormatoLike As Entidades.Publicos.FormatoLike = Publicos.ProductoFormatoLikeBuscarPorNombre
         Select Case usarFormatoLike
            Case Entidades.Publicos.FormatoLike.CONTIENE
               formatoLike = "%{0}%"
            Case Entidades.Publicos.FormatoLike.COMIENZA
               formatoLike = "{0}%"
            Case Entidades.Publicos.FormatoLike.FINALIZA
               formatoLike = "%{0}"
            Case Else
               Throw New ArgumentException(String.Format("Formato de Like ´{0}´ no implemetado. Por favor verifique la configuración.", usarFormatoLike.ToString()))
         End Select
         Return formatoLike
      End Get
   End Property

   Public Shared ReadOnly Property PMCFormato() As Entidades.CuentaCorriente.FormatoPMC
      Get
         Dim str As String = New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.PMCFORMATO.ToString(), Entidades.CuentaCorriente.FormatoPMC.PMC.ToString())
         Dim result As Entidades.CuentaCorriente.FormatoPMC
         If [Enum].TryParse(str, result) Then
            Return result
         End If
         Return Entidades.CuentaCorriente.FormatoPMC.PMC
      End Get
   End Property

   Public Shared ReadOnly Property ProductoCodigoQuitarBlancos() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD("PRODUCTOCODIGOQUITARBLANCOS", "False"))
      End Get
   End Property

   Public Shared ReadOnly Property ProductoBusquedaCombinaCodigoNombre() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.PRODUCTOBUSQUEDACOMBINACODIGONOMBRE.ToString(), Boolean.FalseString))
      End Get
   End Property
   '

   Public Shared ReadOnly Property ProductoUtilizaCantidadesEnteras() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD("ProductoUtilizaCantidadesEnteras", Boolean.TrueString))
      End Get
   End Property

   Public Shared ReadOnly Property ProductoTieneSubRubro() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.PRODUCTOTIENESUBRUBRO.ToString(), "False"))
      End Get
   End Property

   Public Shared ReadOnly Property ProductoTieneSubSubRubro() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.PRODUCTOTIENESUBSUBRUBRO.ToString(), "False"))
      End Get
   End Property

   Public Shared ReadOnly Property ProductoIVA() As Decimal
      Get
         Return Decimal.Parse(New Reglas.Parametros().GetValorPD("ProductoIVA", "21.00"))
      End Get
   End Property

   Public Shared ReadOnly Property ProductoUtilizaAlicuota2() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD("PRODUCTOUTILIZAALICUOTA2", Boolean.FalseString))
      End Get
   End Property

   Public Shared ReadOnly Property PasajeComprasIncluyeImpuestos() As Boolean
      Get
         Return CBool(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.EXPENSASPASAJECOMPRASINCLUYEIMPUESTOS, Boolean.TrueString))
      End Get
   End Property

   Public Shared ReadOnly Property ProductoUtilizaCodigoDeBarras() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD("ProductoUtilizaCodigoDeBarras", Boolean.FalseString))
      End Get
   End Property

   Public Shared ReadOnly Property ProductoUtilizaCodigoLargoProducto() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.PRODUCTOUTILIZACODIGOLARGO, "False"))
      End Get
   End Property

   Public Shared ReadOnly Property UtilizaPrecioDeCompra() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD("UtilizaPrecioDeCompra", Boolean.FalseString))
      End Get
   End Property

   Public Shared ReadOnly Property DiasVisualizacionLibroBanco() As Integer
      Get
         Return Integer.Parse(New Reglas.Parametros().GetValorPD("DiasVisualizacionLibroBanco", "1"))
      End Get
   End Property

   Public Shared ReadOnly Property UnificaLibrosDeBanco() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.UNIFICALIBROSDEBANCO.ToString(), Boolean.FalseString))
      End Get
   End Property

   Public Shared ReadOnly Property TareasPorUsuario() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD("TareasPorUsuario", Boolean.FalseString))
      End Get
   End Property

   Public Shared ReadOnly Property UtilizaOrdenCompraPedidos() As Boolean
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.PEDIDOSUTILIZAORDENCOMPRA.ToString(), Boolean.FalseString) = Boolean.TrueString
      End Get
   End Property

   Public Shared ReadOnly Property ConvertirPedidoEnFacturaConservaPreciosPedido() As Boolean
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.CONVERTIRPEDIDOENFACTURACONSERVAPRECIOSPEDIDO.ToString(), Boolean.FalseString) = Boolean.TrueString
      End Get
   End Property

   Public Shared ReadOnly Property ControlaLimiteDeCreditoDeClientes() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD("CONTROLALIMITEDECREDITODECLIENTES", Boolean.TrueString))
      End Get
   End Property
   Public Shared ReadOnly Property ConsultarPreciosOcultarProdNoAfectanStock() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD("CONSULTPRECOCULTARPRODNOAFECTSTOCK", Boolean.TrueString))
      End Get
   End Property

   Public Shared ReadOnly Property MonedaParaConsultarPrecios() As Integer
      Get
         Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.MONEDAPARACONSULTARPRECIOS.ToString(), "-1"))
      End Get
   End Property

   Public Shared ReadOnly Property OrdenarCobranzaPor() As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.ORDENARCOBRANZAPOR, "Vendedor")
      End Get
   End Property

   Public Shared ReadOnly Property VentasPrecioCosto() As String
      Get
         Return New Reglas.Parametros().GetValorPD("VENTASPRECIOCOSTO", "SI")
      End Get
   End Property

   Public Shared ReadOnly Property VentasPrecioVenta() As String
      Get
         Return New Reglas.Parametros().GetValorPD("VENTASPRECIOVENTA", "SI")
      End Get
   End Property

   Public Shared ReadOnly Property TieneModuloFacturacionElectronica() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD("MODULOFACTURACIONELECTRONICA", Boolean.FalseString))
      End Get
   End Property

   '# Historia Clínica ---
   Public Shared ReadOnly Property FacturacionMuestraHistoriaClinica() As Boolean
      Get
         Return Boolean.Parse(New Eniac.Reglas.Parametros().GetValorPD("FACTURACIONMUESTRAHISTORIACLINICA", "False"))
      End Get
   End Property
   Public Shared ReadOnly Property IdCategoriaPaciente() As String
      Get
         Return New Reglas.Parametros().GetValorPD("IDCATEGORIAPACIENTE", "-1")
      End Get
   End Property
   Public Shared ReadOnly Property IdCategoriaDoctor() As String
      Get
         Return New Reglas.Parametros().GetValorPD("IDCATEGORIADOCTOR", "-1")
      End Get
   End Property
   '-----------------------

   Public Shared ReadOnly Property PoliticasSeguridadClaves() As Boolean
      Get
         Return Boolean.Parse(New Eniac.Reglas.Parametros().GetValorPD("POLITICASSEGURIDADCLAVES", "False"))
      End Get
   End Property

   Public Shared ReadOnly Property TieneModuloProduccion() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD("MODULOPRODUCCION", Boolean.FalseString))
      End Get
   End Property

   Public Shared ReadOnly Property TieneModuloContabilidad() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Eniac.Entidades.Parametro.Parametros.MODULOCONTABILIDAD, Boolean.FalseString))
      End Get
   End Property

   Public Shared ReadOnly Property PermiteConsultarMultipleSucursal() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Eniac.Entidades.Parametro.Parametros.PERMITECONSULTARMULTIPLESUCURSAL.ToString(), Boolean.FalseString))
      End Get
   End Property

   Public Shared ReadOnly Property ConsultarMultipleSucursal() As Boolean
      Get
         If Not PermiteConsultarMultipleSucursal() Then
            Return False
         End If
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Eniac.Entidades.Parametro.Parametros.CONSULTARMULTIPLESUCURSAL.ToString(), Boolean.FalseString))
      End Get
   End Property


   Public Shared ReadOnly Property ContabilidadEjecutarEnLinea() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.CONTABILIDADEJECUTARENLINEA.ToString(), "False"))
      End Get
   End Property

   Public Shared ReadOnly Property SueldosDomicilioEmpresa() As String
      Get
         Return (New Eniac.Reglas.Parametros().GetValorPD("SUELDOS_DOMICILIO_EMPRESA", String.Empty))

      End Get
   End Property

   Public Shared ReadOnly Property SueldosLugarDePago() As String
      Get
         Return (New Eniac.Reglas.Parametros().GetValorPD("SUELDOS_LUGAR_DE_PAGO", String.Empty))

      End Get
   End Property

   Public Shared ReadOnly Property SueldosFechaDePago() As String
      Get
         Return (New Eniac.Reglas.Parametros().GetValorPD("SUELDOS_FECHA_DE_PAGO", String.Empty))

      End Get
   End Property

   Public Shared ReadOnly Property SueldosBancodeDeposito() As String
      Get
         Return (New Eniac.Reglas.Parametros().GetValorPD("SUELDOS_BANCO_DEPOSITO", String.Empty))

      End Get
   End Property

   Public Shared ReadOnly Property SueldosRecibosImpresos() As String
      Get
         Return New Eniac.Reglas.Parametros().GetValorPD("SUELDOS_RECIBOS_IMPRESOS", String.Empty)
      End Get
   End Property

   Public Shared ReadOnly Property ComisionVendedor() As String
      Get
         Return New Eniac.Reglas.Parametros().GetValorPD("COMISIONVENDEDOR", String.Empty)
      End Get
   End Property

   Public Shared ReadOnly Property CalculoComisionVendedor() As String
      Get
         Return New Eniac.Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.CALCULOCOMISIONVENDEDOR, "FACTURACION")
      End Get
   End Property

   Public Shared ReadOnly Property CtaCteProveedoresSeparar() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.CTACTEPROVEEDORESSEPARAR.ToString(), "True"))
      End Get
   End Property

   Public Shared ReadOnly Property EstadoPedidoPendiente() As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.PEDIDOSESTADOAREMITIR.ToString(), "PENDIENTE")
      End Get
   End Property

   Public Shared ReadOnly Property EstadoPedidoFacturado() As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.PEDIDOSESTADOFACTURADO.ToString(), "ENTREGADO")
      End Get
   End Property

   Public Shared ReadOnly Property PedidoProveedorEstadoNuevo() As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.PEDIDOPROVEEDORESTADONUEVO.ToString(), "PENDIENTE")
      End Get
   End Property

   Public Shared ReadOnly Property PedidosProvMuestraProvHabitual() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Eniac.Entidades.Parametro.Parametros.PEDIDOSPROVMUESTRAPROVHABITUAL.ToString(), Boolean.FalseString))
      End Get
   End Property

   Public Shared ReadOnly Property PedidoProveedorEstadoAFacturar() As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.PEDIDOPROVEEDORESTADOAFACTURAR.ToString(), "PENDIENTE")
      End Get
   End Property

   Public Shared ReadOnly Property PedidoProveedorEstadoFacturado() As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.PEDIDOPROVEEDORESTADOFACTURADO.ToString(), "ENTREGADO")
      End Get
   End Property

   Public Shared ReadOnly Property PedidoProveedorEstadoAnulado() As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.PEDIDOPROVEEDORESTADOANULADO.ToString(), "ANULADO")
      End Get
   End Property

   Public Shared ReadOnly Property ImpresionMuestraCodigoProveedorTrue() As Boolean
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.PEDIDOSPROVIMPRESIONMUESTRACODIGOPROV.ToString(), Boolean.TrueString) = Boolean.TrueString
      End Get
   End Property

   Public Shared ReadOnly Property EstadoPedidoPreVinculacion() As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.ESTADOPEDIDOPREVINCULACION.ToString(), "PENDIENTE")
      End Get
   End Property

   Public Shared ReadOnly Property EstadoPedidoPostVinculacion() As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.ESTADOPEDIDOPOSTVINCULACION.ToString(), "RES.FUTURO")
      End Get
   End Property

   Public Shared ReadOnly Property EstadoPedidoProvPreVinculacion() As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.ESTADOPEDIDOPROVPREVINCULACION.ToString(), "EN AGUA")
      End Get
   End Property

   Public Shared ReadOnly Property EstadoPedidoProvPostVinculacion() As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.ESTADOPEDIDOPROVPOSTVINCULACION.ToString(), "RESERVADO")
      End Get
   End Property


   Public Shared ReadOnly Property EstadoPresupuestoAlAnularPedido() As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.ESTADOPRESUPUESTOALANULARPEDIDO.ToString(), Entidades.EstadoPedido.ESTADO_ANULADO)
      End Get
   End Property
   Public Shared ReadOnly Property EstadoListaControlPendiente() As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.LISTASCONTROLESTADOPENDIENTE.ToString(), "1")
      End Get
   End Property

   Public Shared ReadOnly Property EstadoListaControlEnProceso() As Integer
      Get
         Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.LISTASCONTROLESTADOENPROCESO.ToString(), "2"))
      End Get
   End Property

   Public Shared ReadOnly Property EstadoListaControlTerminado() As Integer
      Get
         Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.LISTASCONTROLESTADOTERMINADO.ToString(), "4"))
      End Get
   End Property

   Public Shared ReadOnly Property TurismoCategoriaEstablecimiento() As Integer
      Get
         Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.TURISMOCATEGORIAESTABLECIMIENTO.ToString(), "0"))
      End Get
   End Property

   Public Shared ReadOnly Property TurismoCategoriaPasajero() As Integer
      Get
         Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.TURISMOCATEGORIAPASAJEROS.ToString(), "0"))
      End Get
   End Property

   Public Shared ReadOnly Property TurismoCategoriaResponsable() As Integer
      Get
         Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.TURISMOCATEGORIARESPONSABLE.ToString(), "0"))
      End Get
   End Property
   Public Shared ReadOnly Property TurismoRubroPrograma() As Integer
      Get
         Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.TURISMORUBROPROGRAMA.ToString(), "1"))
      End Get
   End Property

   Public Shared ReadOnly Property TurismoRubroVisita() As Integer
      Get
         Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.TURISMORUBROVISITA.ToString(), "2"))
      End Get
   End Property

   Public Shared ReadOnly Property TurismoRubroGastronomia() As Integer
      Get
         Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.TURISMORUBROGASTRONOMIA.ToString(), "4"))
      End Get
   End Property

   Public Shared ReadOnly Property TurismoFormulaVisitas() As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.TURISMOFORMULAVISITAS.ToString(), "VISITAS")
      End Get
   End Property

   Public Shared ReadOnly Property TurismoFormulaVisitasID() As Integer
      Get
         Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.TURISMOFORMULAVISITASID.ToString(), "1"))
      End Get
   End Property

   Public Shared ReadOnly Property TurismoFormulaGastronomia() As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.TURISMOFORMULAGASTRONOMIA.ToString(), "GASTRONOMIA")
      End Get
   End Property

   Public Shared ReadOnly Property TurismoFormulaGastronomiaID() As Integer
      Get
         Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.TURISMOFORMULAGASTRONOMIAID.ToString(), "2"))
      End Get
   End Property

   Public Shared ReadOnly Property TurismoTipoComprobante() As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.TURISMOTIPOCOMPROBANTE.ToString(), "FICHAS")
      End Get
   End Property

   Public Shared ReadOnly Property SimovilTurismoURLBase() As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.SIMOVILTURISMOURLBASE.ToString(), "")
      End Get
   End Property
   Public Shared ReadOnly Property TurismoRoelaIdentificadorConcepto() As Integer
      Get
         Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.TURISMOROELAIDCONCEPTO.ToString(), "1"))
      End Get
   End Property

   Public Shared ReadOnly Property TurismoRoelaIdentificadorCuenta() As Integer
      Get
         Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.TURISMOROELAIDCUENTA.ToString(), "2"))
      End Get
   End Property

   Public Shared ReadOnly Property TurismoRoelaInteresesVencimiento() As Integer
      Get
         Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.TURISMOROELAINTERESVTO.ToString(), "1"))
      End Get
   End Property

   Public Shared ReadOnly Property ProduccionDescStockComponentesFormulasFacturacion() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.PRODUCCIONDESCUENTAPRODFORMULAFACTURAR.ToString(), "False"))
      End Get
   End Property

   Public Shared ReadOnly Property ProductoUtilizaNombreCorto() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.PRODUCTOUTILIZANOMBRECORTO.ToString(), "False"))
      End Get
   End Property

   Public Shared ReadOnly Property ClienteDRporGrabaLibro() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD("CLIENTEDRGRABALIBRO", "True"))
      End Get
   End Property

   Public Shared ReadOnly Property NombreBaseARBA() As String
      Get
         Dim val As String = New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.NOMBREBASEARBA.ToString(), "")
         If String.IsNullOrEmpty(val) Then
            Return Ayudantes.Conexiones.Base
         Else
            Return val
         End If
      End Get
   End Property
   Public Shared ReadOnly Property CalidadBaseExternaClientes() As String
      Get
         Dim val As String = New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.CALIDADBASEEXTERNACLIENTES.ToString(), "")
         Return val
      End Get
   End Property

   Public Shared ReadOnly Property CalidadPanelDeControlTiempoRefresco() As Integer
      Get
         Return CInt(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.CALIDADPANELDECONTROLTIEMPOREFRESCO.ToString(), "5"))
      End Get
   End Property

   Public Shared ReadOnly Property CalidadPanelDeControlPlantaTiempoPaginado() As Integer
      Get
         Return CInt(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.CALIDADPANELDECONTROLPLANTATIEMPOPAGINADO.ToString(), "15"))
      End Get
   End Property

   Public Shared ReadOnly Property CalidadUtilizaModeloAsignacionListas As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.CALIDADUTILIZAMODELOASIGLISTAS, Boolean.FalseString))
      End Get
   End Property
   Public Shared ReadOnly Property CalidadUtilizaPagAutomaticoPanelControlPlanta As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.CALIDADUTILIZAPAGAUTOMATICOPANELCONTROLPLANTA, Boolean.FalseString))
      End Get
   End Property

   Public Shared ReadOnly Property NombreBaseAdjuntosCRM() As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.NOMBREBASEADJUNTOSCRM.ToString(), "")
      End Get
   End Property

   Public Shared ReadOnly Property NombreBaseAdjuntos() As String
      Get
         Return NombreBaseAdjuntos(Nothing)
      End Get
   End Property

   Public Shared ReadOnly Property NombreBaseAdjuntos(da As Eniac.Datos.DataAccess) As String
      Get
         If da IsNot Nothing Then
            Return New Reglas.Parametros(da).GetValorPD(Entidades.Parametro.Parametros.NOMBREBASEADJUNTOS.ToString(), "")
         Else
            Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.NOMBREBASEADJUNTOS.ToString(), "")
         End If
      End Get
   End Property

   Public Shared ReadOnly Property NombreBaseProductosWeb() As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.NOMBREBASEPRODUCTOWEB.ToString(), "")
      End Get
   End Property

   Public Shared ReadOnly Property TipoComprobanteEnviadoCajero() As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.FACTURACIONTIPOCOMPROBANTEENVIADOCAJERO.ToString(), "PRE-VENTA")
      End Get
   End Property

   Public Shared ReadOnly Property SiTieneDescRecPorCantidadIgnoraOtros() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.SITIENEDESCRECPORCANTIDADIGNORAOTROS.ToString(), Boolean.FalseString))
      End Get
   End Property

   Public Shared ReadOnly Property TipoComprobanteGeneradoCajero() As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.FACTURACIONTIPOCOMPROBANTEGENERADOCAJERO.ToString(), "COTIZACION")
      End Get
   End Property

   Public Shared ReadOnly Property CajeroGenera() As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.FACTURACIONCAJEROGENERA.ToString(), Entidades.VentaCajero.CajeroGenera.Ventas.ToString())
      End Get
   End Property

   Public Shared ReadOnly Property CajeroSeleccionMultiple() As Boolean
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.FACTURACIONCAJEROSELECCIONMULTIPLE.ToString(), Boolean.FalseString) = Boolean.TrueString
      End Get
   End Property
   Public Shared ReadOnly Property CajeroAbrirVariasVentanasDeFactuacion() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.CAJEROABRIRVARIASVENTANASDEFACTURACION.ToString(), Boolean.FalseString))
      End Get
   End Property
   Public Shared ReadOnly Property CajeroPermiteActualizacionAutomatica() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.CAJEROPERMITEACTUALIZACIONAUTOMATICA.ToString(), Boolean.TrueString))
      End Get
   End Property
   Public Shared ReadOnly Property CajeroTiempoDeActualizacionAutomatica() As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.CAJEROTIEMPODEACTUALIZACIONAUTOMATICA.ToString(), "60")
      End Get
   End Property
   Public Shared ReadOnly Property CajeroIgnorarTipoComprobanteCliente() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.CAJEROIGNORARTIPOCOMPROBANTECLIENTE.ToString(), Boolean.FalseString))
      End Get
   End Property


   Public Shared ReadOnly Property ActualizarPreciosExcel As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.ACTUALIZARPRECIOSEXCEL.ToString(), "COSTO")
      End Get
   End Property

   Public Shared ReadOnly Property ActualizarPreciosDesdeComprasPriorizaIVAProducto As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.ACTUALIZARPRECIOSDESDECOMPRASPRIORIZAIVAPRODUCTO.ToString(), Boolean.FalseString))
      End Get
   End Property

   Public Shared ReadOnly Property UtilizaInteresesTarjetas() As Boolean
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.FACTURACIONUTILIZAINTERESESTARJETAS.ToString(), Boolean.TrueString) = Boolean.TrueString
      End Get
   End Property

   Public Shared ReadOnly Property CargaReciboLuegoDeNC() As Boolean
      Get

         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.FACTURACIONCARGARECIBOLUEGONC.ToString(), Boolean.FalseString) = Boolean.TrueString

      End Get
   End Property

   Public Shared ReadOnly Property MantieneVendedorInvocados() As Boolean
      Get

         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.FACTURACIONINVOCADOSMANTIENEVENDEDOR.ToString(), Boolean.FalseString) = Boolean.TrueString

      End Get
   End Property

   Public Shared ReadOnly Property MantieneFormaPagoPedidosInvocados() As Boolean
      Get

         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.FACTURACIONPEDIDOSINVOCADOSMANTIENEFORMAPAGO.ToString(), Boolean.FalseString) = Boolean.TrueString

      End Get
   End Property

   Public Shared ReadOnly Property MantieneFormaPagoPedidosProvInvocados() As Boolean
      Get

         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.COMPRASPEDIDOSPROVINVOCADOSMANTIENEFORMAPAGO.ToString(), Boolean.FalseString) = Boolean.TrueString

      End Get
   End Property

   Public Shared ReadOnly Property ComprasActualizaPreciosUtilizaAjusteADecimales() As Boolean
      Get

         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.ACTUALIZAPRECIOSUTILIZAAJUSTEA.ToString(), Boolean.FalseString) = Boolean.TrueString

      End Get
   End Property

   Public Shared ReadOnly Property ComprasSoloCargaProductosDelProveedor() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.COMPRASSOLOCARGAPRODUCTOSDELPROVEEDOR.ToString(), Boolean.FalseString))
      End Get
   End Property

   Public Shared ReadOnly Property MantieneFormaPagoInvocados() As Boolean
      Get

         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.FACTURACIONINVOCADOSMANTIENEFORMAPAGO.ToString(), Boolean.FalseString) = Boolean.TrueString

      End Get
   End Property

   Public Shared ReadOnly Property EnvioMasivoComprobantesAdjuntaCtaCte() As Boolean
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.ENVIOMASIVOCOMPROBANTESADJUNTACTACTE.ToString(), Boolean.TrueString) = Boolean.TrueString
      End Get
   End Property

   Public Shared ReadOnly Property ExpensasFormaPago As Integer
      Get
         Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.CARGOSEXPENSASFORMAPAGO.ToString(), "0"))
      End Get
   End Property

   Public Shared ReadOnly Property ExpensasTipoComprobantes As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.CARGOSEXPENSASTIPOCOMPROBANTE.ToString(), "FACT-CYO")
      End Get
   End Property
   Public Shared ReadOnly Property ExpensasFechaFactura As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.CARGOSEXPENSASFECHAFACTURA.ToString(), "UltimoDiaPeriodoActual")
      End Get
   End Property

   Public Shared ReadOnly Property ExpensasPeriodoFactura As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.CARGOSEXPENSASPERIODOFACTURA.ToString(), Reglas.Publicos.PeriodoFacturacionEnum.PeriodoActual.ToString())
      End Get
   End Property

   Public Shared ReadOnly Property ExpensasPermiteConSinConcepto As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.EXPENSASPERMITECONSINCONCEPTO.ToString(), Boolean.FalseString))
      End Get
   End Property

   Public Shared ReadOnly Property ExpensasPermitirCargarProductosSinConceptos As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.EXPENSASPERMITIRCARGARPRODUCTOSSINCONCEPTOS.ToString(), Boolean.FalseString))
      End Get
   End Property

   Public Shared ReadOnly Property ComprasSinProductos() As Boolean
      Get
         Return Boolean.Parse(New Parametros().GetValorPD(Entidades.Parametro.Parametros.COMPRASSINPRODUCTOS, Boolean.FalseString))
      End Get
   End Property

   Public Shared ReadOnly Property ComprasModificaDescripcionProducto() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.COMPRASMODIFICADESCRIPCIONPRODUCTO, Boolean.TrueString))
      End Get
   End Property

   Public Shared ReadOnly Property ComprasSolicitaComprador As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.COMPRASSOLICITACOMPRADOR.ToString(), Boolean.FalseString))
      End Get
   End Property

   Public Shared ReadOnly Property CargosPermiteGenerarLiquidacionSinClientes As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.CARGOSPERMITEGENERARLIQUIDACIONSINCLIENTES.ToString(), Boolean.FalseString))
      End Get
   End Property

   Public Shared ReadOnly Property CargosGenerarComprobantePorItem As Boolean
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.CARGOSGENERARCOMPROBANTEPORITEM.ToString(), Boolean.FalseString) = Boolean.TrueString
      End Get
   End Property

   Public Shared ReadOnly Property CargosPasajeMovimientoTomaComprobanteCompleto As Boolean
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.CARGOSPASAJEMOVIMIENTOTOMACOMPROBANTECOMPLETO.ToString(), Boolean.FalseString) = Boolean.TrueString
      End Get
   End Property

   Public Shared ReadOnly Property CargosUtilizaDescuentosRecargos As Boolean
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.CARGOSUTILIZADESCUENTOSRECARGOS.ToString(), Boolean.FalseString) = Boolean.TrueString
      End Get
   End Property

   Public Shared ReadOnly Property PedidosFacturarAutomatico As Boolean
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.PEDIDOSFACTURARAUTOMATICO.ToString(), Boolean.FalseString) = Boolean.TrueString
      End Get
   End Property

   Public Shared ReadOnly Property PedidosPermiteFechaEntregaDistintas As Boolean
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.PEDIDOSPERMITEFECHAENTREGADISTINTAS.ToString(), Boolean.FalseString) = Boolean.TrueString
      End Get
   End Property

   Public Shared ReadOnly Property PedidosModificaDescripSolicitaKilos As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.PEDIDOSMODIFICADESCRIPSOLICITAKILOS.ToString(), Reglas.Publicos.Facturacion.FacturacionModificaDescripSolicitaKilos.ToString()))
      End Get
   End Property

   Public Shared ReadOnly Property PedidosModificaPrecioProducto As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.PEDIDOSMODIFICAPRECIOPRODUCTO.ToString(), Facturacion.FacturacionModificaPrecioProducto.ToString()))
      End Get
   End Property
   Public Shared ReadOnly Property PedidosOcultarRentabilidad As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.PEDIDOCULTARENTABILIDAD.ToString(), PedidosClientesOcultarRentabilidad.ToString()))
      End Get
   End Property
   Public Shared ReadOnly Property PedidosDecimalesMostrarLargoAncho As Integer
      Get
         Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.PEDIDOSDECIMALESMOSTRARLARGOANCHO.ToString(), "2"))
      End Get
   End Property

   Public Shared ReadOnly Property CantidadPadronesARBAaGuardar As Integer
      Get
         Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.CANTIDADPADRONESARBA.ToString(), "3"))
      End Get
   End Property


   Public Shared ReadOnly Property AFIPHabilitaVentasPeriodoAutomaticamente As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.AFIPHABILITAVENTASPERIODOAUTOMATICAMENTE.ToString(), Boolean.TrueString))
      End Get
   End Property

   Public Shared ReadOnly Property AFIPCitiComprasProrrateo As Entidades.EnumAfip.ProrrateoCitiCompras
      Get
         Dim valor As String = New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.AFIPCITICOMPRASPRORRATEO.ToString(), Entidades.EnumAfip.ProrrateoCitiCompras.SI_GLOBAL.ToString())
         Dim result As Entidades.EnumAfip.ProrrateoCitiCompras

         If [Enum].TryParse(valor, result) Then
            Return result
         End If
         Return Entidades.EnumAfip.ProrrateoCitiCompras.SI_GLOBAL
      End Get
   End Property

   Public Shared ReadOnly Property AFIPURLCodigoQR As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.AFIPURLCODIGOQR.ToString(), "https://www.afip.gob.ar/fe/qr/")
      End Get
   End Property
   Public Shared ReadOnly Property AFIPURLControlarComprobante As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.AFIPURLCONTROLARCOMPROBANTE.ToString(), "https://serviciosweb.afip.gob.ar/genericos/comprobantes/cae.aspx")
      End Get
   End Property

   Public Shared ReadOnly Property VencimientoCertificadoFE As DateTime
      Get
         Return DateTime.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.VENCIMIENTOCERTIFICADOFE.ToString(), "01/01/0001"))
      End Get
   End Property

   Public Shared ReadOnly Property CRMNovedadesProductoFacturable() As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.CRMNOVEDADESPRODUCTOFACTURABLE.ToString(), "")
      End Get
   End Property

   Public Shared ReadOnly Property RMACantidadMesesHistorico() As Integer
      Get
         Return CInt(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.RMACANTIDADMESESHISTORICO.ToString(), "3"))
      End Get
   End Property

   Public Shared ReadOnly Property CRMNovedadesObservacionFacturable() As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.CRMNOVEDADESOBSERVACIONFACTURABLE.ToString(), "")
      End Get
   End Property

   Public Shared ReadOnly Property CRMNovedadesDiasAVencerAlarma() As Integer
      Get
         Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.CRMNOVEDADESDIASAVENCERALARMA.ToString(), "0"))
      End Get
   End Property

   Public Shared ReadOnly Property CtaBancoPago() As Integer
      Get
         Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.CTABANCOPAGO.ToString(), "4"))
      End Get
   End Property

   Public Shared ReadOnly Property CtaBancoTransfBancaria() As Integer
      Get
         Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.CTABANCOTRANSFBANCARIA.ToString(), "5"))
      End Get
   End Property

   Public Shared ReadOnly Property CtaBancoDeposito() As Integer
      Get
         Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.CTABANCODEPOSITO.ToString(), "6"))
      End Get
   End Property

   Public Shared ReadOnly Property CtaBancoAcredTarjeta() As Integer
      Get
         Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.CTABANCOACREDTARJETA.ToString(), "7"))
      End Get
   End Property

   Public Shared ReadOnly Property CtaBancoExtraccion() As Integer
      Get
         Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.CTABANCOEXTRACCION.ToString(), "3"))
      End Get
   End Property

   Public Shared ReadOnly Property CtaBancoLiquidacionesTarjetas() As Integer
      Get
         Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.CTABANCOLIQUIDACIONTARJETA.ToString(), "3"))
      End Get
   End Property

   Public Shared ReadOnly Property CtaCajaLiquidacionesTarjetas() As Integer
      Get
         Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.CTACAJALIQUIDACIONTARJETA.ToString(), "1"))
      End Get
   End Property

   Public Shared ReadOnly Property CtaCajaVentas() As Integer
      Get
         Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.CTACAJAVENTAS.ToString(), "1"))
      End Get
   End Property

   Public Shared ReadOnly Property CtaCajaVentasAcumula() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.CTACAJAVENTASACUMULA.ToString(), Boolean.FalseString))
      End Get
   End Property

   Public Shared ReadOnly Property CajaMostrarNCIndividual() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.CAJAMOSTRARNCINDIVIDUAL.ToString(), Boolean.TrueString))
      End Get
   End Property

   Public Shared ReadOnly Property CtaCajaRecibos() As Integer
      Get
         Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.CTACAJARECIBOS.ToString(), "2"))
      End Get
   End Property

   Public Shared ReadOnly Property CtaCajaCompras() As Integer
      Get
         Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.CTACAJACOMPRAS.ToString(), "3"))
      End Get
   End Property

   Public Shared ReadOnly Property CtaCajaPago() As Integer
      Get
         Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.CTACAJAPAGO.ToString(), "4"))
      End Get
   End Property

   Public Shared ReadOnly Property CtaCajaTransfBancaria() As Integer
      Get
         Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.CTACAJATRANSFBANCARIA.ToString(), "5"))
      End Get
   End Property

   Public Shared ReadOnly Property CtaCajaDeposito() As Integer
      Get
         Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.CTACAJADEPOSITO.ToString(), "6"))
      End Get
   End Property

   Public Shared ReadOnly Property CtaCajaMovCheques() As Integer
      Get
         Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.CTACAJAMOVCHEQUES.ToString(), "7"))
      End Get
   End Property

   Public Shared ReadOnly Property CtaCajaDepositoTarjetas() As Integer
      Get
         Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.CTACAJADEPOSITOTARJETAS.ToString(), "8"))
      End Get
   End Property

   Public Shared ReadOnly Property CtaCajaExtraccion() As Integer
      Get
         Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.CTACAJAEXTRACCION.ToString(), "26"))
      End Get
   End Property

   Public Shared ReadOnly Property IDAplicacionSinergia() As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.IDAPLICACION.ToString(), "")
      End Get
   End Property

   Public Shared ReadOnly Property IdEdicionAplicacionSinergia() As Entidades.AplicacionEdicionParaParametros
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.IDEDICIONAPLICACION, Entidades.AplicacionEdicionParaParametros.Plus.ToString()).
                     StringToEnum(Entidades.AplicacionEdicionParaParametros.Plus)
      End Get
   End Property

   Public Shared ReadOnly Property LoteSolicitaDespachoImportacion() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.LOTESOLICITADESPACHOIMPORTACION.ToString(), Boolean.FalseString))
      End Get
   End Property
   Public Shared ReadOnly Property ClaveLoteDespachoImportacion() As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.CLAVELOTEDESPACHOIMPORTACION.ToString(), ClaveLoteDespachoImportacionEnum.DESPACHO.ToString())
      End Get
   End Property

   Public Shared ReadOnly Property UtilizaPrecioCostoPorLote() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.UTILIZAPRECIOCOSTOPORLOTE.ToString(), Boolean.FalseString))
      End Get
   End Property

   Public Shared ReadOnly Property InformeDeStockMostrarPrecioPredeterminado() As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.INFDESTOCKMOSTRARPRECIOPREDETERMINADO.ToString(), "PrecioDeVenta")
      End Get
   End Property

   Public Shared ReadOnly Property UtilizaStockReservado() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.UTILIZASTOCKRESERVADO.ToString(), Boolean.FalseString))
      End Get
   End Property
   Public Shared ReadOnly Property UtilizaStockDefectuoso() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.UTILIZASTOCKDEFECTUOSO.ToString(), Boolean.FalseString))
      End Get
   End Property
   Public Shared ReadOnly Property UtilizaStockFuturo() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.UTILIZASTOCKFUTURO.ToString(), Boolean.FalseString))
      End Get
   End Property
   Public Shared ReadOnly Property UtilizaStockFuturoReservado() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.UTILIZASTOCKFUTURORESERVADO.ToString(), Boolean.FalseString))
      End Get
   End Property

   Public Shared ReadOnly Property ProductoUtilizaProductoWeb() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.PRODUCTOUTILIZAPRODUCTOWEB, Boolean.FalseString))
      End Get
   End Property

   Public Shared ReadOnly Property ImportarPedidosTipoComprobante As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.IMPORTARPEDIDOSTIPOCOMPROBANTE.ToString(), "PEDIDOWEB")
      End Get
   End Property

   Public Shared ReadOnly Property CantidadDiasEntregaImportacion As Integer
      Get
         Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.DIASENTREGAIMPORTACION.ToString(), "0"))
      End Get
   End Property

   Public Shared ReadOnly Property PreventaImprimirPedidos As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.PREVENTAIMPRIMIRPEDIDOS.ToString(), SiempreNuncaPreguntar.SIEMPRE.ToString())
      End Get
   End Property

   Public Shared ReadOnly Property PreventaV2RespetaListaPreciosCliente() As Boolean
      Get
         Return Boolean.Parse(New Eniac.Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.PREVENTAV2RESPETALISTAPRECIOSCLIENTE.ToString(), Boolean.TrueString))
      End Get
   End Property

   Public Shared ReadOnly Property PreventaV2ImportaDescuentosPedidosPDA() As Boolean
      Get
         Return Boolean.Parse(New Eniac.Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.PREVENTAV2IMPORTADESCUENTOSPEDIDOSPDA.ToString(), Boolean.FalseString))
      End Get
   End Property
   Public Shared ReadOnly Property PreventaV2ImportaDescuentosSimovilWeb() As Boolean
      Get
         Return Boolean.Parse(New Eniac.Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.PREVENTAV2IMPORTADESCUENTOSSIMOVILWEB.ToString(), Boolean.FalseString))
      End Get
   End Property

   Public Shared ReadOnly Property PreventaRespetaPrecioDelMovil() As Boolean
      Get
         Return Boolean.Parse(New Eniac.Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.PREVENTARESPETAPRECIODELMOVIL.ToString(), Boolean.FalseString))
      End Get
   End Property

   Public Shared ReadOnly Property PreVentaAgrupaOrdenProductoEnCadaPedido() As Boolean
      Get
         Return Boolean.Parse(New Eniac.Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.PREVENTAAGRUPAORDENPRODUCTOENCADAPEDIDO.ToString(), Boolean.FalseString))
      End Get
   End Property

   Public Shared ReadOnly Property PreVentaAccionSinListaPrecios() As Entidades.PreVenta.AccionSinListaPrecios
      Get
         Dim str As String = New Eniac.Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.PREVENTAACCIONSINLISTAPRECIOS.ToString(), Entidades.PreVenta.AccionSinListaPrecios.CargaManual.ToString())
         Dim result As Entidades.PreVenta.AccionSinListaPrecios
         Try
            result = DirectCast([Enum].Parse(GetType(Entidades.PreVenta.AccionSinListaPrecios), str), Entidades.PreVenta.AccionSinListaPrecios)
         Catch ex As Exception
            result = Entidades.PreVenta.AccionSinListaPrecios.CargaManual
         End Try
         Return result
      End Get
   End Property

   Public Shared ReadOnly Property RegistracionPagosTipoMovimiento() As String
      Get
         Return New Eniac.Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.REGISTRACIONPAGOSTIPOMOVIMIENTO.ToString(), "")
      End Get
   End Property

   Public Shared ReadOnly Property RegistracionPagosModoConsultarComprobantes() As Entidades.RegistracionPagos.ModoConsultarComprobantes
      Get
         Dim str As String = New Eniac.Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.REGISTRACIONPAGOSMODOCONSULTARCOMPROBANTES.ToString(),
                                                                      Entidades.RegistracionPagos.ModoConsultarComprobantes.SoloRepartoSeleccionado.ToString())
         Try
            Return DirectCast([Enum].Parse(GetType(Entidades.RegistracionPagos.ModoConsultarComprobantes), str), Entidades.RegistracionPagos.ModoConsultarComprobantes)
         Catch ex As Exception
            Return Entidades.RegistracionPagos.ModoConsultarComprobantes.SoloRepartoSeleccionado
         End Try
      End Get
   End Property


   Public Shared ReadOnly Property ReemplazarComprobanteReaplicaRecibos() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.REEMPLAZARCOMPROBANTEREAPLICARECIBOS.ToString(), Boolean.FalseString))
      End Get
   End Property
   Public Shared ReadOnly Property ReemplazarComprobanteTipoComprobanteOrigen() As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.REEMPLAZARCOMPROBANTETIPOCOMPROBANTEORIGEN.ToString(), String.Empty)
      End Get
   End Property

   Public Shared ReadOnly Property ReemplazarComprobanteTipoComprobanteDestino() As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.REEMPLAZARCOMPROBANTETIPOCOMPROBANTEDESTINO.ToString(), String.Empty)
      End Get
   End Property

   Public Shared ReadOnly Property FichasActualizaPreciosDeVenta() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.FICHASACTUALIZAPRECIOSDEVENTA, Boolean.TrueString))
      End Get
   End Property

   Public Shared ReadOnly Property FichasPermiteCambiarFormaDePago() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.FICHASPERMITECAMBIARFORMADEPAGO, Boolean.FalseString))
      End Get
   End Property

   Public Shared ReadOnly Property FichasPreguntaReemplazarComprobante() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.FICHASPREGUNTAREEMPLAZARCOMPROBANTE.ToString(), Boolean.FalseString))
      End Get
   End Property

   Public Shared ReadOnly Property FichasImprimeCobrosFormatoRecibo() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.FICHASIMPRIMECOBROSFORMATORECIBO.ToString(), Boolean.FalseString))
      End Get
   End Property

   Public Shared ReadOnly Property ReservaSemaforoAmarillo() As Integer
      Get
         Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.ReservaSemaforoAmarillo.ToString(), "2"))
      End Get
   End Property

#Region "Pedidos Web"
   Public Shared ReadOnly Property PedidosURLWebPOST As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.PEDIDOSURLWEBPOST.ToString(), String.Empty)
      End Get
   End Property
   Public Shared ReadOnly Property EstadoPedidoPendienteWebPOST() As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.ESTADOPEDIDOPENDIENTEWEBPOST.ToString(), "PENDIENTE")
      End Get
   End Property

   Public Shared ReadOnly Property EstadoPedidoEnviadoWebPOST() As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.ESTADOPEDIDOENVIADOWEBPOST.ToString(), "ENTREGADO")
      End Get
   End Property

   Public Shared ReadOnly Property PedidosURLWebGET As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.PEDIDOSURLWEBGET.ToString(), String.Empty)
      End Get
   End Property
   Public Shared ReadOnly Property PedidosWebFormato As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.PEDIDOSWEBFORMATO.ToString(), Entidades.PreVenta.FormatoWebPeridos.SiWeb.ToString())
      End Get
   End Property

   Public Shared ReadOnly Property PedidosURLWebPUT As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.PEDIDOSURLWEBPUT.ToString(), String.Empty)
      End Get
   End Property

#End Region

   Public Shared ReadOnly Property TurnosPermiteFacturarDesdeCalendario As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.TURNOSPERMITEFACTURARDESDECALENDARIO, Boolean.FalseString))
      End Get
   End Property

   Public Shared ReadOnly Property TurnosProductoZona As Integer
      Get
         Try
            Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.TURNOSPRODUCTOZONA.ToString(), "0"))
         Catch ex As Exception
            Return 0
         End Try
      End Get
   End Property
   Public Shared ReadOnly Property LibroBancoColorConciliado As Drawing.Color
      Get
         Return Drawing.Color.FromArgb(Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.COLORCONCILIADO, "-1")))
      End Get
   End Property
   Public Shared ReadOnly Property LibroBancoColorNoConciliado As Drawing.Color
      Get
         Return Drawing.Color.FromArgb(Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.COLORNOCONCILIADO, "-1")))
      End Get
   End Property

   Public Enum AbrirArchivoExportadoModo
      Nunca
      Preguntar
      Siempre
   End Enum
   Public Shared ReadOnly Property AbrirArchivoExportado As AbrirArchivoExportadoModo
      Get
         Dim str As String = New Eniac.Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.ABRIRARCHIVOEXPORTADO.ToString(), AbrirArchivoExportadoModo.Nunca.ToString())
         Dim result As AbrirArchivoExportadoModo
         Try
            result = DirectCast([Enum].Parse(GetType(AbrirArchivoExportadoModo), str), AbrirArchivoExportadoModo)
         Catch ex As Exception
            result = AbrirArchivoExportadoModo.Nunca
         End Try
         Return result
      End Get
   End Property

   Public Class BejermanMetalsur
      Public Shared ReadOnly Property FechaModificacionProveedor() As DateTime?
         Get
            Return New Eniac.Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.BEJERMANFECHAMODIFICACIONPROVEEDOR.ToString(), "").ToDateTime()
         End Get
      End Property
      Public Shared ReadOnly Property FechaModificacionOrdenCompra() As DateTime?
         Get
            Return New Eniac.Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.BEJERMANFECHAMODIFICACIONORDENCOMPRA.ToString(), "").ToDateTime()
         End Get
      End Property
      Public Shared ReadOnly Property MetalsurUrlBaseWebOC() As String
         Get
            Return New Eniac.Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.METALSURURLBASEWEBOC.ToString(), "").TrimEnd("/"c)
         End Get
      End Property
   End Class

   '-- REG-31725.- --
   Public Class InformeVentas
      Public Class VentasPirelli
         Public Shared ReadOnly Property VentasPirelliURLBase As String
            Get
               Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.VENTASPIRELLIURLBASE.ToString(), String.Empty)
            End Get
         End Property
         Public Shared ReadOnly Property VentasPirelliIdMarca As String
            Get
               Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.VENTASPIRELLIIDMARCA.ToString(), String.Empty)
            End Get
         End Property
         Public Shared ReadOnly Property VentasPirelliIdSucursal As String
            Get
               Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.VENTASPIRELLIIDSUCURSAL.ToString(), String.Empty)
            End Get
         End Property

      End Class

   End Class


   Public Class TiendasWeb
      Public Class TiendaNube
         Public Shared ReadOnly Property TiendaNubeToken As String
            Get
               Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.TIENDANUBETOKEN.ToString(), String.Empty)
            End Get
         End Property
         Public Shared ReadOnly Property TiendaNubeURLBase As String
            Get
               Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.TIENDANUBEURLBASE.ToString(), String.Empty)
            End Get
         End Property
         Public Shared ReadOnly Property TiendaNubeCorreoNotificaciones As String
            Get
               Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.TIENDANUBECORREONOTIFICACIONES.ToString(), String.Empty)
            End Get
         End Property
         Public Shared ReadOnly Property TiendaNubePedidosTipoComprobante As String
            Get
               Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.TIENDANUBEPEDIDOSTIPOCOMPROBANTE.ToString(), String.Empty)
            End Get
         End Property
         Public Shared ReadOnly Property TiendaNubeIdProductoEnvio As String
            Get
               Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.TIENDANUBEIDPRODUCTOENVIO.ToString(), String.Empty)
            End Get
         End Property
         Public Shared ReadOnly Property TiendaNubeListaDePrecios As Integer
            Get
               Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.TIENDANUBELISTAPRECIOS.ToString(), "-1"))
            End Get
         End Property
         Public Shared ReadOnly Property TiendaNubePedidosFormaDePago As Integer
            Get
               Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.TIENDANUBEPEDIDOSFORMADEPAGO.ToString(), "-1"))
            End Get
         End Property
         Public Shared ReadOnly Property TiendaNubePedidosCriticidad As String
            Get
               Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.TIENDANUBEPEDIDOSCRITICIDAD.ToString(), "Normal")
            End Get
         End Property
         Public Shared ReadOnly Property TiendaNubeCategoriaCliente As Integer
            Get
               Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.TIENDANUBECATEGORIACLIENTE.ToString(), "-1"))
            End Get
         End Property
         Public Shared ReadOnly Property TiendaNubeCategoriaFiscalCliente As Short
            Get
               Return Short.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.TIENDANUBECATEGORIAFISCALCLIENTE.ToString(), "-1"))
            End Get
         End Property
         Public Shared ReadOnly Property TiendaNubeVendedor As Integer
            Get
               Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.TIENDANUBEVENDEDOR.ToString(), "-1"))
            End Get
         End Property
      End Class

      Public Class MercadoLibre
         Public Shared ReadOnly Property MercadoLibreToken As String
            Get
               Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.MERCADOLIBRETOKEN.ToString(), String.Empty)
            End Get
         End Property

         Public Shared ReadOnly Property MercadoLibreCodigoRefreshTokenML As String
            Get
               Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.MERCADOLIBREREFRESHTOKEN.ToString(), String.Empty)
            End Get
         End Property
         Public Shared ReadOnly Property MercadoLibreFechaRefreshTokenML As String
            Get
               Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.MERCADOLIBREFECHATOKEN.ToString(), String.Empty)
            End Get
         End Property
         Public Shared ReadOnly Property MercadoLibreImagenDefaultML As String
            Get
               Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.MERCADOLIBREIMAGENDEFAULT.ToString(), String.Empty)
            End Get
         End Property
         Public Shared ReadOnly Property MercadoLibreCodigoAppIdML As String
            Get
               Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.MERCADOLIBREAPPID.ToString(), String.Empty)
            End Get
         End Property

         Public Shared ReadOnly Property MercadoLibreCodigoSecretML As String
            Get
               Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.MERCADOLIBRESECRETKEY.ToString(), String.Empty)
            End Get
         End Property

         Public Shared ReadOnly Property MercadoLibreCategoriaDefaultML As String
            Get
               Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.MERCADOLIBRECATEGORIADEFAULT.ToString(), String.Empty)
            End Get
         End Property

         Public Shared ReadOnly Property MercadoLibreCodigoResellerML As String
            Get
               Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.MERCADOLIBRECODIGORESELLER.ToString(), String.Empty)
            End Get
         End Property


         Public Shared ReadOnly Property MercadoLibreURLBase As String
            Get
               Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.MERCADOLIBREURLBASE.ToString(), String.Empty)
            End Get
         End Property
         Public Shared ReadOnly Property MercadoLibreCorreoNotificaciones As String
            Get
               Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.MERCADOLIBRECORREONOTIFICACIONES.ToString(), String.Empty)
            End Get
         End Property
         Public Shared ReadOnly Property MercadoLibrePedidosTipoComprobante As String
            Get
               Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.MERCADOLIBREPEDIDOSTIPOCOMPROBANTE.ToString(), String.Empty)
            End Get
         End Property
         Public Shared ReadOnly Property MercadoLibreIdProductoEnvio As String
            Get
               Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.MERCADOLIBREIDPRODUCTOENVIO.ToString(), String.Empty)
            End Get
         End Property
         Public Shared ReadOnly Property MercadoLibreListaDePrecios As Integer
            Get
               Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.MERCADOLIBRELISTAPRECIOS.ToString(), "-1"))
            End Get
         End Property
         Public Shared ReadOnly Property MercadoLibrePedidosFormaDePago As Integer
            Get
               Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.MERCADOLIBREPEDIDOSFORMADEPAGO.ToString(), "-1"))
            End Get
         End Property
         Public Shared ReadOnly Property MercadoLibrePedidosCriticidad As String
            Get
               Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.MERCADOLIBREPEDIDOSCRITICIDAD.ToString(), "Normal")
            End Get
         End Property
         Public Shared ReadOnly Property MercadoLibreCategoriaCliente As Integer
            Get
               Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.MERCADOLIBRECATEGORIACLIENTE.ToString(), "-1"))
            End Get
         End Property
         Public Shared ReadOnly Property MercadoLibreCategoriaFiscalCliente As Short
            Get
               Return Short.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.MERCADOLIBRECATEGORIAFISCALCLIENTE.ToString(), "-1"))
            End Get
         End Property
         Public Shared ReadOnly Property MercadoLibreVendedor As Integer
            Get
               Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.MERCADOLIBREVENDEDOR.ToString(), "-1"))
            End Get
         End Property
         Public Shared ReadOnly Property MercadoLibreCajaDefault As Integer
            Get
               Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.MERCADOLIBRECAJADEFAULT.ToString(), "-1"))
            End Get
         End Property

      End Class

   End Class

   Public Class WebArchivos
      Public Shared Sub GuardarFechaUltimaDescarga(fecha As DateTime?, idParametro As Entidades.Parametro.Parametros, da As Datos.DataAccess)
         GuardarFechaUltimaDescarga(fecha, idParametro.ToString(), da)
      End Sub
      Private Shared Sub GuardarFechaUltimaDescarga(fecha As DateTime?, idParametro As String, da As Datos.DataAccess)
         Dim rParametros As Eniac.Reglas.Parametros = New Eniac.Reglas.Parametros(da)
         Dim parametro As Entidades.Parametro = rParametros.GetUno(actual.Sucursal.IdEmpresa, idParametro, AccionesSiNoExisteRegistro.Nulo)
         If parametro Is Nothing Then
            parametro = New Entidades.Parametro()
            parametro.IdEmpresa = actual.Sucursal.IdEmpresa
            parametro.IdParametro = idParametro
            parametro.CategoriaParametro = "WEB-ARCHIVOS"
         End If
         If fecha.HasValue Then
            parametro.ValorParametro = fecha.Value.ToString(Entidades.JSonEntidades.AyudanteJson.FormatoFechasMilisegundos)
         Else
            parametro.ValorParametro = String.Empty
         End If
         rParametros._Actualizar(parametro)

         Reglas.ParametrosCache.Instancia.LimpiarCache(idParametro)

      End Sub

      Public Shared ReadOnly Property CarpetaExportacion As String
         Get
            Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSCARPETAEXPORTACION.ToString(), "C:\Eniac\Sincronizacion\")
         End Get
      End Property

      Public Shared ReadOnly Property HabilitarTLS1_1 As Boolean
         Get
            Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSHABILITARTLS1_1.ToString(), Boolean.FalseString))
         End Get
      End Property
      Public Shared ReadOnly Property HabilitarTLS1_2 As Boolean
         Get
            Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSHABILITARTLS1_2.ToString(), Boolean.FalseString))
         End Get
      End Property

      Public Class Clientes
         Public Shared ReadOnly Property UrlDELETE As String
            Get
               Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSCLIENTESURLDELETE.ToString(), String.Empty)
            End Get
         End Property
         Public Shared ReadOnly Property UrlGET As String
            Get
               Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSCLIENTESURLGET.ToString(), String.Empty)
            End Get
         End Property
         Public Shared ReadOnly Property UrlGETCount As String
            Get
               Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSCLIENTESURLGETCOUNT.ToString(), String.Empty)
            End Get
         End Property
         Public Shared ReadOnly Property UrlGETMaxFecha As String
            Get
               Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSCLIENTESURLGETMAXFECHA.ToString(), String.Empty)
            End Get
         End Property
         Public Shared ReadOnly Property UrlPOST As String
            Get
               Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSCLIENTESURLPOST.ToString(), String.Empty)
            End Get
         End Property
         Public Shared ReadOnly Property TamanoPaginaPost As Integer
            Get
               Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSCLIENTESTAMANOPAGINAPOST.ToString(), "500"))
            End Get
         End Property
         Public Shared ReadOnly Property TamanoPaginaGet As Integer
            Get
               Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSCLIENTESTAMANOPAGINAGET.ToString(), "500"))
            End Get
         End Property
         Public Shared ReadOnly Property FechaUltimaDescarga As DateTime?
            Get
               Dim strFechaUltimaDescarga As String = New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSCLIENTESULTIMADESCARGA.ToString(), String.Empty)
               If IsDate(strFechaUltimaDescarga) Then
                  Return DateTime.Parse(strFechaUltimaDescarga)
               End If
               Return Nothing
            End Get
         End Property
         Public Shared Sub GuardarFechaUltimaDescarga(fecha As DateTime?, da As Datos.DataAccess)
            WebArchivos.GuardarFechaUltimaDescarga(fecha, Entidades.Parametro.Parametros.WEBARCHIVOSCLIENTESULTIMADESCARGA.ToString(), da)
         End Sub
      End Class
      Public Class Productos
         Public Shared ReadOnly Property UrlDELETE As String
            Get
               Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSPRODUCTOSURLDELETE.ToString(), String.Empty)
            End Get
         End Property
         Public Shared ReadOnly Property UrlGET As String
            Get
               Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSPRODUCTOSURLGET.ToString(), String.Empty)
            End Get
         End Property
         Public Shared ReadOnly Property UrlGETCount As String
            Get
               Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSPRODUCTOSURLGETCOUNT.ToString(), String.Empty)
            End Get
         End Property
         Public Shared ReadOnly Property UrlGETMaxFecha As String
            Get
               Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSPRODUCTOSURLGETMAXFECHA.ToString(), String.Empty)
            End Get
         End Property
         Public Shared ReadOnly Property UrlPOST As String
            Get
               Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSPRODUCTOSURLPOST.ToString(), String.Empty)
            End Get
         End Property
         Public Shared ReadOnly Property TamanoPaginaPost As Integer
            Get
               Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSPRODUCTOSTAMANOPAGINAPOST.ToString(), "1000"))
            End Get
         End Property
         Public Shared ReadOnly Property TamanoPaginaGet As Integer
            Get
               Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSPRODUCTOSTAMANOPAGINAGET.ToString(), "1000"))
            End Get
         End Property
         Public Shared ReadOnly Property FechaUltimaDescarga As DateTime?
            Get
               Dim strFechaUltimaDescarga As String = New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSPRODUCTOSULTIMADESCARGA.ToString(), String.Empty)
               If IsDate(strFechaUltimaDescarga) Then
                  Return DateTime.Parse(strFechaUltimaDescarga)
               End If
               Return Nothing
            End Get
         End Property
         Public Shared Sub GuardarFechaUltimaDescarga(fecha As DateTime?, da As Datos.DataAccess)
            WebArchivos.GuardarFechaUltimaDescarga(fecha, Entidades.Parametro.Parametros.WEBARCHIVOSPRODUCTOSULTIMADESCARGA.ToString(), da)
         End Sub

         Public Shared ReadOnly Property PublicarEnWeb As Entidades.Publicos.SiNoTodos
            Get
               Dim strValor As String = New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSPRODUCTOSPUBLICARENWEB.ToString(), Entidades.Publicos.SiNoTodos.TODOS.ToString())
               Dim result As Entidades.Publicos.SiNoTodos
               If [Enum].TryParse(strValor, result) Then
                  Return result
               End If
               Return Entidades.Publicos.SiNoTodos.TODOS
            End Get
         End Property

         Public Shared ReadOnly Property IncluirImagenes As Boolean
            Get
               Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSPRODUCTOSINCLUIRIMAGENES.ToString(), Boolean.FalseString))
            End Get
         End Property

      End Class

      Public Class ProductosSucursales
         Public Shared ReadOnly Property UrlDELETE As String
            Get
               Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSPRODSUCURURLDELETE.ToString(), String.Empty)
            End Get
         End Property
         Public Shared ReadOnly Property UrlGET As String
            Get
               Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSPRODSUCURURLGET.ToString(), String.Empty)
            End Get
         End Property
         Public Shared ReadOnly Property UrlGETCount As String
            Get
               Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSPRODSUCURURLGETCOUNT.ToString(), String.Empty)
            End Get
         End Property
         Public Shared ReadOnly Property UrlGETMaxFecha As String
            Get
               Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSPRODSUCURURLGETMAXFECHA.ToString(), String.Empty)
            End Get
         End Property
         Public Shared ReadOnly Property UrlPOST As String
            Get
               Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSPRODSUCURURLPOST.ToString(), String.Empty)
            End Get
         End Property
         Public Shared ReadOnly Property TamanoPaginaPost As Integer
            Get
               Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSPRODSUCURTAMANOPAGINAPOST.ToString(), "5000"))
            End Get
         End Property
         Public Shared ReadOnly Property TamanoPaginaGet As Integer
            Get
               Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSPRODSUCURTAMANOPAGINAGET.ToString(), "5000"))
            End Get
         End Property
         Public Shared ReadOnly Property FechaUltimaDescarga As DateTime?
            Get
               Dim strFechaUltimaDescarga As String = New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSPRODSUCURULTIMADESCARGA.ToString(), String.Empty)
               If IsDate(strFechaUltimaDescarga) Then
                  Return DateTime.Parse(strFechaUltimaDescarga)
               End If
               Return Nothing
            End Get
         End Property
         Public Shared ReadOnly Property ActualizaStockEstoyAca As Boolean
            Get
               Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSPRODSUCURACTUALIZASTOCKESTOYACA.ToString(), Boolean.TrueString))
            End Get
         End Property
         Public Shared Sub GuardarFechaUltimaDescarga(fecha As DateTime?, da As Datos.DataAccess)
            WebArchivos.GuardarFechaUltimaDescarga(fecha, Entidades.Parametro.Parametros.WEBARCHIVOSPRODSUCURULTIMADESCARGA.ToString(), da)
         End Sub
      End Class
      Public Class ProductosSucursalesPrecios
         Public Shared ReadOnly Property UrlDELETE As String
            Get
               Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSPRODSUCURPRECIOSURLDELETE.ToString(), String.Empty)
            End Get
         End Property
         Public Shared ReadOnly Property UrlGET As String
            Get
               Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSPRODSUCURPRECIOSURLGET.ToString(), String.Empty)
            End Get
         End Property
         Public Shared ReadOnly Property UrlGETCount As String
            Get
               Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSPRODSUCURPRECIOSURLGETCOUNT.ToString(), String.Empty)
            End Get
         End Property
         Public Shared ReadOnly Property UrlGETMaxFecha As String
            Get
               Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSPRODSUCURPRECIOSURLGETMAXFECHA.ToString(), String.Empty)
            End Get
         End Property
         Public Shared ReadOnly Property UrlPOST As String
            Get
               Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSPRODSUCURPRECIOSURLPOST.ToString(), String.Empty)
            End Get
         End Property
         Public Shared ReadOnly Property TamanoPaginaPost As Integer
            Get
               Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSPRODSUCURPRECIOSTAMANOPAGINAPOST.ToString(), "10000"))
            End Get
         End Property
         Public Shared ReadOnly Property TamanoPaginaGet As Integer
            Get
               Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSPRODSUCURPRECIOSTAMANOPAGINAGET.ToString(), "10000"))
            End Get
         End Property
         Public Shared ReadOnly Property FechaUltimaDescarga As DateTime?
            Get
               Dim strFechaUltimaDescarga As String = New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSPRODSUCURPRECIOSULTIMADESCARGA.ToString(), String.Empty)
               If IsDate(strFechaUltimaDescarga) Then
                  Return DateTime.Parse(strFechaUltimaDescarga)
               End If
               Return Nothing
            End Get
         End Property
         Public Shared Sub GuardarFechaUltimaDescarga(fecha As DateTime?, da As Datos.DataAccess)
            WebArchivos.GuardarFechaUltimaDescarga(fecha, Entidades.Parametro.Parametros.WEBARCHIVOSPRODSUCURPRECIOSULTIMADESCARGA.ToString(), da)
         End Sub
      End Class

      Public Class Localidades
         Public Shared ReadOnly Property UrlDELETE As String
            Get
               Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSLOCALIDADESURLDELETE.ToString(), String.Empty)
            End Get
         End Property
         Public Shared ReadOnly Property UrlGET As String
            Get
               Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSLOCALIDADESURLGET.ToString(), String.Empty)
            End Get
         End Property
         Public Shared ReadOnly Property UrlGETCount As String
            Get
               Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSLOCALIDADESURLGETCOUNT.ToString(), String.Empty)
            End Get
         End Property
         Public Shared ReadOnly Property UrlGETMaxFecha As String
            Get
               Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSLOCALIDADESURLGETMAXFECHA.ToString(), String.Empty)
            End Get
         End Property
         Public Shared ReadOnly Property UrlPOST As String
            Get
               Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSLOCALIDADESURLPOST.ToString(), String.Empty)
            End Get
         End Property
         Public Shared ReadOnly Property TamanoPaginaPost As Integer
            Get
               Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSLOCALIDADESTAMANOPAGINAPOST.ToString(), "500"))
            End Get
         End Property
         Public Shared ReadOnly Property TamanoPaginaGet As Integer
            Get
               Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSLOCALIDADESTAMANOPAGINAGET.ToString(), "500"))
            End Get
         End Property
         Public Shared ReadOnly Property FechaUltimaDescarga As DateTime?
            Get
               Dim strFechaUltimaDescarga As String = New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSLOCALIDADESULTIMADESCARGA.ToString(), String.Empty)
               If IsDate(strFechaUltimaDescarga) Then
                  Return DateTime.Parse(strFechaUltimaDescarga)
               End If
               Return Nothing
            End Get
         End Property
         Public Shared Sub GuardarFechaUltimaDescarga(fecha As DateTime?, da As Datos.DataAccess)
            WebArchivos.GuardarFechaUltimaDescarga(fecha, Entidades.Parametro.Parametros.WEBARCHIVOSLOCALIDADESULTIMADESCARGA.ToString(), da)
         End Sub
      End Class

      Public Class Rubros
         Public Shared ReadOnly Property UrlDELETE As String
            Get
               Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSRUBROSURLDELETE.ToString(), String.Empty)
            End Get
         End Property
         Public Shared ReadOnly Property UrlGET As String
            Get
               Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSRUBROSURLGET.ToString(), String.Empty)
            End Get
         End Property
         Public Shared ReadOnly Property UrlGETCount As String
            Get
               Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSRUBROSURLGETCOUNT.ToString(), String.Empty)
            End Get
         End Property
         Public Shared ReadOnly Property UrlGETMaxFecha As String
            Get
               Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSRUBROSURLGETMAXFECHA.ToString(), String.Empty)
            End Get
         End Property
         Public Shared ReadOnly Property UrlPOST As String
            Get
               Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSRUBROSURLPOST.ToString(), String.Empty)
            End Get
         End Property
         Public Shared ReadOnly Property TamanoPaginaPost As Integer
            Get
               Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSRUBROSTAMANOPAGINAPOST.ToString(), "500"))
            End Get
         End Property
         Public Shared ReadOnly Property TamanoPaginaGet As Integer
            Get
               Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSRUBROSTAMANOPAGINAGET.ToString(), "500"))
            End Get
         End Property
         Public Shared ReadOnly Property FechaUltimaDescarga As DateTime?
            Get
               Dim strFechaUltimaDescarga As String = New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSRUBROSULTIMADESCARGA.ToString(), String.Empty)
               If IsDate(strFechaUltimaDescarga) Then
                  Return DateTime.Parse(strFechaUltimaDescarga)
               End If
               Return Nothing
            End Get
         End Property
         Public Shared Sub GuardarFechaUltimaDescarga(fecha As DateTime?, da As Datos.DataAccess)
            WebArchivos.GuardarFechaUltimaDescarga(fecha, Entidades.Parametro.Parametros.WEBARCHIVOSRUBROSULTIMADESCARGA.ToString(), da)
         End Sub
      End Class

      Public Class SubRubros
         Public Shared ReadOnly Property UrlDELETE As String
            Get
               Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSSUBRUBROSURLDELETE.ToString(), String.Empty)
            End Get
         End Property
         Public Shared ReadOnly Property UrlGET As String
            Get
               Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSSUBRUBROSURLGET.ToString(), String.Empty)
            End Get
         End Property
         Public Shared ReadOnly Property UrlGETCount As String
            Get
               Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSSUBRUBROSURLGETCOUNT.ToString(), String.Empty)
            End Get
         End Property
         Public Shared ReadOnly Property UrlGETMaxFecha As String
            Get
               Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSSUBRUBROSURLGETMAXFECHA.ToString(), String.Empty)
            End Get
         End Property
         Public Shared ReadOnly Property UrlPOST As String
            Get
               Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSSUBRUBROSURLPOST.ToString(), String.Empty)
            End Get
         End Property
         Public Shared ReadOnly Property TamanoPaginaPost As Integer
            Get
               Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSSUBRUBROSTAMANOPAGINAPOST.ToString(), "500"))
            End Get
         End Property
         Public Shared ReadOnly Property TamanoPaginaGet As Integer
            Get
               Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSSUBRUBROSTAMANOPAGINAGET.ToString(), "500"))
            End Get
         End Property
         Public Shared ReadOnly Property FechaUltimaDescarga As DateTime?
            Get
               Dim strFechaUltimaDescarga As String = New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSSUBRUBROSULTIMADESCARGA.ToString(), String.Empty)
               If IsDate(strFechaUltimaDescarga) Then
                  Return DateTime.Parse(strFechaUltimaDescarga)
               End If
               Return Nothing
            End Get
         End Property
         Public Shared Sub GuardarFechaUltimaDescarga(fecha As DateTime?, da As Datos.DataAccess)
            WebArchivos.GuardarFechaUltimaDescarga(fecha, Entidades.Parametro.Parametros.WEBARCHIVOSSUBRUBROSULTIMADESCARGA.ToString(), da)
         End Sub
      End Class

      Public Class SubSubRubros
         Public Shared ReadOnly Property UrlDELETE As String
            Get
               Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSSUBSUBRUBROSURLDELETE.ToString(), String.Empty)
            End Get
         End Property
         Public Shared ReadOnly Property UrlGET As String
            Get
               Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSSUBSUBRUBROSURLGET.ToString(), String.Empty)
            End Get
         End Property
         Public Shared ReadOnly Property UrlGETCount As String
            Get
               Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSSUBSUBRUBROSURLGETCOUNT.ToString(), String.Empty)
            End Get
         End Property
         Public Shared ReadOnly Property UrlGETMaxFecha As String
            Get
               Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSSUBSUBRUBROSURLGETMAXFECHA.ToString(), String.Empty)
            End Get
         End Property
         Public Shared ReadOnly Property UrlPOST As String
            Get
               Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSSUBSUBRUBROSURLPOST.ToString(), String.Empty)
            End Get
         End Property
         Public Shared ReadOnly Property TamanoPaginaPost As Integer
            Get
               Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSSUBSUBRUBROSTAMANOPAGINAPOST.ToString(), "500"))
            End Get
         End Property
         Public Shared ReadOnly Property TamanoPaginaGet As Integer
            Get
               Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSSUBSUBRUBROSTAMANOPAGINAGET.ToString(), "500"))
            End Get
         End Property
         Public Shared ReadOnly Property FechaUltimaDescarga As DateTime?
            Get
               Dim strFechaUltimaDescarga As String = New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSSUBSUBRUBROSULTIMADESCARGA.ToString(), String.Empty)
               If IsDate(strFechaUltimaDescarga) Then
                  Return DateTime.Parse(strFechaUltimaDescarga)
               End If
               Return Nothing
            End Get
         End Property
         Public Shared Sub GuardarFechaUltimaDescarga(fecha As DateTime?, da As Datos.DataAccess)
            WebArchivos.GuardarFechaUltimaDescarga(fecha, Entidades.Parametro.Parametros.WEBARCHIVOSSUBSUBRUBROSULTIMADESCARGA.ToString(), da)
         End Sub
      End Class

      Public Class Marcas
         Public Shared ReadOnly Property UrlDELETE As String
            Get
               Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSMARCASURLDELETE.ToString(), String.Empty)
            End Get
         End Property
         Public Shared ReadOnly Property UrlGET As String
            Get
               Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSMARCASURLGET.ToString(), String.Empty)
            End Get
         End Property
         Public Shared ReadOnly Property UrlGETCount As String
            Get
               Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSMARCASURLGETCOUNT.ToString(), String.Empty)
            End Get
         End Property
         Public Shared ReadOnly Property UrlGETMaxFecha As String
            Get
               Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSMARCASURLGETMAXFECHA.ToString(), String.Empty)
            End Get
         End Property
         Public Shared ReadOnly Property UrlPOST As String
            Get
               Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSMARCASURLPOST.ToString(), String.Empty)
            End Get
         End Property
         Public Shared ReadOnly Property TamanoPaginaPost As Integer
            Get
               Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSMARCASTAMANOPAGINAPOST.ToString(), "500"))
            End Get
         End Property
         Public Shared ReadOnly Property TamanoPaginaGet As Integer
            Get
               Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSMARCASTAMANOPAGINAGET.ToString(), "500"))
            End Get
         End Property
         Public Shared ReadOnly Property FechaUltimaDescarga As DateTime?
            Get
               Dim strFechaUltimaDescarga As String = New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSMARCASULTIMADESCARGA.ToString(), String.Empty)
               If IsDate(strFechaUltimaDescarga) Then
                  Return DateTime.Parse(strFechaUltimaDescarga)
               End If
               Return Nothing
            End Get
         End Property
         Public Shared Sub GuardarFechaUltimaDescarga(fecha As DateTime?, da As Datos.DataAccess)
            WebArchivos.GuardarFechaUltimaDescarga(fecha, Entidades.Parametro.Parametros.WEBARCHIVOSMARCASULTIMADESCARGA.ToString(), da)
         End Sub
      End Class

      Public Class RubrosCompras
         Public Shared ReadOnly Property UrlDELETE As String
            Get
               Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSRUBROSCOMPRASURLDELETE.ToString(), String.Empty)
            End Get
         End Property
         Public Shared ReadOnly Property UrlGET As String
            Get
               Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSRUBROSCOMPRASURLGET.ToString(), String.Empty)
            End Get
         End Property
         Public Shared ReadOnly Property UrlGETCount As String
            Get
               Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSRUBROSCOMPRASURLGETCOUNT.ToString(), String.Empty)
            End Get
         End Property
         Public Shared ReadOnly Property UrlGETMaxFecha As String
            Get
               Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSRUBROSCOMPRASURLGETMAXFECHA.ToString(), String.Empty)
            End Get
         End Property
         Public Shared ReadOnly Property UrlPOST As String
            Get
               Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSRUBROSCOMPRASURLPOST.ToString(), String.Empty)
            End Get
         End Property
         Public Shared ReadOnly Property TamanoPaginaPost As Integer
            Get
               Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSRUBROSCOMPRASTAMANOPAGINAPOST.ToString(), "500"))
            End Get
         End Property
         Public Shared ReadOnly Property TamanoPaginaGet As Integer
            Get
               Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSRUBROSCOMPRASTAMANOPAGINAGET.ToString(), "500"))
            End Get
         End Property
         Public Shared ReadOnly Property FechaUltimaDescarga As DateTime?
            Get
               Dim strFechaUltimaDescarga As String = New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSRUBROSCOMPRASULTIMADESCARGA.ToString(), String.Empty)
               If IsDate(strFechaUltimaDescarga) Then
                  Return DateTime.Parse(strFechaUltimaDescarga)
               End If
               Return Nothing
            End Get
         End Property
         Public Shared Sub GuardarFechaUltimaDescarga(fecha As DateTime?, da As Datos.DataAccess)
            WebArchivos.GuardarFechaUltimaDescarga(fecha, Entidades.Parametro.Parametros.WEBARCHIVOSRUBROSCOMPRASULTIMADESCARGA.ToString(), da)
         End Sub
      End Class

      Public Class Proveedores
         Public Shared ReadOnly Property UrlDELETE As String
            Get
               Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSPROVEEDORESURLDELETE.ToString(), String.Empty)
            End Get
         End Property
         Public Shared ReadOnly Property UrlGET As String
            Get
               Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSPROVEEDORESURLGET.ToString(), String.Empty)
            End Get
         End Property
         Public Shared ReadOnly Property UrlGETCount As String
            Get
               Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSPROVEEDORESURLGETCOUNT.ToString(), String.Empty)
            End Get
         End Property
         Public Shared ReadOnly Property UrlGETMaxFecha As String
            Get
               Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSPROVEEDORESURLGETMAXFECHA.ToString(), String.Empty)
            End Get
         End Property
         Public Shared ReadOnly Property UrlPOST As String
            Get
               Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSPROVEEDORESURLPOST.ToString(), String.Empty)
            End Get
         End Property
         Public Shared ReadOnly Property TamanoPaginaPost As Integer
            Get
               Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSPROVEEDORESTAMANOPAGINAPOST.ToString(), "500"))
            End Get
         End Property
         Public Shared ReadOnly Property TamanoPaginaGet As Integer
            Get
               Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSPROVEEDORESTAMANOPAGINAGET.ToString(), "500"))
            End Get
         End Property
         Public Shared ReadOnly Property FechaUltimaDescarga As DateTime?
            Get
               Dim strFechaUltimaDescarga As String = New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.WEBARCHIVOSPROVEEDORESULTIMADESCARGA.ToString(), String.Empty)
               If IsDate(strFechaUltimaDescarga) Then
                  Return DateTime.Parse(strFechaUltimaDescarga)
               End If
               Return Nothing
            End Get
         End Property
         Public Shared Sub GuardarFechaUltimaDescarga(fecha As DateTime?, da As Datos.DataAccess)
            WebArchivos.GuardarFechaUltimaDescarga(fecha, Entidades.Parametro.Parametros.WEBARCHIVOSPROVEEDORESULTIMADESCARGA.ToString(), da)
         End Sub
      End Class

   End Class

   Public Class CuentasCorrientes
      Public Class Informes
         Public Shared ReadOnly Property ModalidadCoeficienteCobranzas As Entidades.EnumSql.GetCoeficienteCobranzasModalidad
            Get
               Dim strValor As String = New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.MODALIDADCOEFICIENTECOBRANZAS.ToString(), Entidades.EnumSql.GetCoeficienteCobranzasModalidad.VencidosCobrados.ToString())
               Dim result As Entidades.EnumSql.GetCoeficienteCobranzasModalidad
               If [Enum].TryParse(strValor, result) Then
                  Return result
               End If
               Return Entidades.EnumSql.GetCoeficienteCobranzasModalidad.VencidosCobrados
            End Get
         End Property
      End Class
   End Class

   Public Class ParametrosSiMovil
      Public Shared ReadOnly Property CorreoMovil1() As String
         Get
            Return CorreoMovil1(Nothing)
         End Get
      End Property

      Public Shared ReadOnly Property CorreoMovil1(da As Datos.DataAccess) As String
         Get
            Return New Eniac.Reglas.Parametros(da).GetValorPD(Entidades.Parametro.Parametros.CORREOMOVIL1.ToString(), "")
         End Get
      End Property

      Public Shared ReadOnly Property CorreoMovil2() As String
         Get
            Return CorreoMovil2(Nothing)
         End Get
      End Property

      Public Shared ReadOnly Property CorreoMovil2(da As Datos.DataAccess) As String
         Get
            Return New Eniac.Reglas.Parametros(da).GetValorPD(Entidades.Parametro.Parametros.CORREOMOVIL2.ToString(), "")
         End Get
      End Property

      Public Shared ReadOnly Property CorreoMovil3() As String
         Get
            Return CorreoMovil3(Nothing)
         End Get
      End Property

      Public Shared ReadOnly Property CorreoMovil3(da As Datos.DataAccess) As String
         Get
            Return New Eniac.Reglas.Parametros(da).GetValorPD(Entidades.Parametro.Parametros.CORREOMOVIL3.ToString(), "")
         End Get
      End Property

      Public Shared ReadOnly Property BusquedaClientes() As String
         Get
            Return BusquedaClientes(Nothing)
         End Get
      End Property

      Public Shared ReadOnly Property BusquedaClientes(da As Datos.DataAccess) As String
         Get
            Return New Eniac.Reglas.Parametros(da).GetValorPD(Entidades.Parametro.Parametros.BUSQUEDACLIENTES.ToString(), Entidades.JSonEntidades.CobranzasMovil.BusquedaClientes.direccion.ToString())
         End Get
      End Property

      Public Shared ReadOnly Property OrdenarProductos() As String
         Get
            Return OrdenarProductos(Nothing)
         End Get
      End Property

      Public Shared ReadOnly Property OrdenarProductos(da As Datos.DataAccess) As String
         Get
            Return New Eniac.Reglas.Parametros(da).GetValorPD(Entidades.Parametro.Parametros.ORDENARPRODUCTOS.ToString(), Entidades.JSonEntidades.CobranzasMovil.OrdenarProductos.codigo.ToString())
         End Get
      End Property

      Public Shared ReadOnly Property PorcMaxDescuento() As Decimal
         Get
            Return PorcMaxDescuento(Nothing)
         End Get
      End Property

      Public Shared ReadOnly Property PorcMaxDescuento(da As Datos.DataAccess) As Decimal
         Get
            Return Decimal.Parse(New Eniac.Reglas.Parametros(da).GetValorPD(Entidades.Parametro.Parametros.SIMOVILPORCMAXDESCUENTO.ToString(), "99"))
         End Get
      End Property

      Public Shared ReadOnly Property PorcMaxRecargo() As Decimal
         Get
            Return PorcMaxRecargo(Nothing)
         End Get
      End Property

      Public Shared ReadOnly Property PorcMaxRecargo(da As Datos.DataAccess) As Decimal
         Get
            Return Decimal.Parse(New Eniac.Reglas.Parametros(da).GetValorPD(Entidades.Parametro.Parametros.SIMOVILPORCMAXRECARGO.ToString(), "99999999"))
         End Get
      End Property

      Public Shared ReadOnly Property SolicitaCierrePedidos() As Boolean
         Get
            Return SolicitaCierrePedidos(Nothing)
         End Get
      End Property

      Public Shared ReadOnly Property SolicitaCierrePedidos(da As Datos.DataAccess) As Boolean
         Get
            Return Boolean.Parse(New Eniac.Reglas.Parametros(da).GetValorPD(Entidades.Parametro.Parametros.SOLICITACIERREPEDIDOS.ToString(), Boolean.TrueString))
         End Get
      End Property

      Public Shared ReadOnly Property UsarFechaExportacion() As Boolean
         Get
            Return UsarFechaExportacion(Nothing)
         End Get
      End Property

      Public Shared ReadOnly Property UsarFechaExportacion(da As Datos.DataAccess) As Boolean
         Get
            Return Boolean.Parse(New Eniac.Reglas.Parametros(da).GetValorPD(Entidades.Parametro.Parametros.USARFECHAEXPORTACION.ToString(), Boolean.FalseString))
         End Get
      End Property

      Public Shared ReadOnly Property OcultarCompartirVentasPorMail() As Boolean
         Get
            Return OcultarCompartirVentasPorMail(Nothing)
         End Get
      End Property

      Public Shared ReadOnly Property OcultarCompartirVentasPorMail(da As Datos.DataAccess) As Boolean
         Get
            Return Boolean.Parse(New Eniac.Reglas.Parametros(da).GetValorPD(Entidades.Parametro.Parametros.OCULTARCOMPARTIRVENTASPORMAIL.ToString(), Boolean.FalseString))
         End Get
      End Property

      Public Shared ReadOnly Property OcultarResumenPromedio() As Boolean
         Get
            Return OcultarResumenPromedio(Nothing)
         End Get
      End Property

      Public Shared ReadOnly Property OcultarResumenPromedio(da As Datos.DataAccess) As Boolean
         Get
            Return Boolean.Parse(New Eniac.Reglas.Parametros(da).GetValorPD(Entidades.Parametro.Parametros.OCULTARRESUMENPROMEDIO.ToString(), Boolean.FalseString))
         End Get
      End Property

      Public Shared ReadOnly Property EnviaMailCliente() As Boolean
         Get
            Return EnviaMailCliente(Nothing)
         End Get
      End Property

      Public Shared ReadOnly Property EnviaMailCliente(da As Datos.DataAccess) As Boolean
         Get
            Return Boolean.Parse(New Eniac.Reglas.Parametros(da).GetValorPD(Entidades.Parametro.Parametros.ENVIAMAILCLIENTE.ToString(), Boolean.TrueString))
         End Get
      End Property

      Public Shared ReadOnly Property EnviaMailEmpresa() As Boolean
         Get
            Return EnviaMailEmpresa(Nothing)
         End Get
      End Property

      Public Shared ReadOnly Property EnviaMailEmpresa(da As Datos.DataAccess) As Boolean
         Get
            Return Boolean.Parse(New Eniac.Reglas.Parametros(da).GetValorPD(Entidades.Parametro.Parametros.ENVIAMAILEMPRESA.ToString(), Boolean.FalseString))
         End Get
      End Property
   End Class

   Public Class Logistica
      Public Shared ReadOnly Property RutaArchivosPalm() As String
         Get
            Return New Eniac.Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.RUTAARCHIVOSPALM.ToString(), System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop))
         End Get
      End Property

      Public Shared ReadOnly Property LogisticaFormatoExportacionDefault As String
         Get
            Return New Eniac.Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.LOGISTICAFORMATOEXPORTACIONDEFAULT.ToString(), Entidades.GenerarArchivo.FormatosExportacion.Sync.ToString())
         End Get
      End Property

      Public Shared ReadOnly Property LogisticaFormatoExportacionDefaultEnum As Entidades.GenerarArchivo.FormatosExportacion
         Get
            Dim dv As String = LogisticaFormatoExportacionDefault
            Dim val As Entidades.GenerarArchivo.FormatosExportacion
            Try
               val = DirectCast([Enum].Parse(GetType(Entidades.GenerarArchivo.FormatosExportacion), dv), Entidades.GenerarArchivo.FormatosExportacion)
            Catch ex As Exception
               val = Entidades.GenerarArchivo.FormatosExportacion.Sync
            End Try
            Return val
         End Get
      End Property

      Public Shared ReadOnly Property IncluirEsCambiableEsBonificable() As Boolean
         Get
            Return Boolean.Parse(New Eniac.Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.INCLUIRESCAMBIABLEESBONIFICABLE.ToString(), Boolean.FalseString))
         End Get
      End Property

   End Class

   Partial Class ParametrosSiMovil
      Public Shared ReadOnly Property SolicitaTipoComprobante() As Boolean
         Get
            Return SolicitaTipoComprobante(Nothing)
         End Get
      End Property

      Public Shared ReadOnly Property SolicitaTipoComprobante(da As Datos.DataAccess) As Boolean
         Get
            Return Boolean.Parse(New Eniac.Reglas.Parametros(da).GetValorPD(Entidades.Parametro.Parametros.SOLICITATIPOCOMPROBANTE.ToString(), Boolean.FalseString))
         End Get
      End Property

   End Class

   Public Shared ReadOnly Property URLActualizacion() As String
      Get
         Return New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.URLACTUALIZADOR.ToString(), "http://www.sinergiatest.com.ar/WSActualizador.svc")
      End Get
   End Property

#End Region

   Public Shared Function HayInternet() As Boolean
      Dim Url As System.Uri = New System.Uri("http://www.google.com")
      Dim peticion As System.Net.WebRequest
      Dim respuesta As System.Net.WebResponse

      Try
         'Creamos la peticion
         peticion = System.Net.WebRequest.Create(Url)
         'POnemos un tiempo limite
         peticion.Timeout = 5000

         'ejecutamos la peticion
         respuesta = peticion.GetResponse()
         respuesta.Close()
         peticion = Nothing

         Return True

      Catch ex As Exception
         'si ocurre un error, devolvemos error
         peticion = Nothing
         Return False

      End Try

   End Function

   Public Shared Sub VerificaConfiguracionRegional(Optional separadorDecimal As String = Nothing)
      Dim MiCultura = DirectCast(Globalization.CultureInfo.InvariantCulture.Clone(), Globalization.CultureInfo)
      MiCultura.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy"
      MiCultura.DateTimeFormat.ShortTimePattern = "HH:mm:ss"
      MiCultura.NumberFormat.CurrencySymbol = "$"

      If Not String.IsNullOrWhiteSpace(separadorDecimal) Then
         Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = separadorDecimal
         Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator = separadorDecimal
      End If

      Threading.Thread.CurrentThread.CurrentCulture = MiCultura

   End Sub

   Public Shared _fechaUltimoLogin As DateTime = Today
   Public Shared Function VerificaFechaUltimoLogin() As Boolean
      If Today > _fechaUltimoLogin.Date Then
         System.Windows.Forms.MessageBox.Show("Su sesión ha caducado", "Cierre de sesión", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Stop)
         Environment.Exit(666)
      End If
   End Function

   Public Shared Function ObtenerFechaHoraAfip() As DateTime
      Return ntpHelper.GetNetworkTime("time.afip.gov.ar")
   End Function

   Public Function ExisteParametroCuenta(id As String, Modulo As String) As Boolean

   End Function
   Public Function ExisteParametroCuenta(id As Integer, Modulo As String) As Boolean

      If Modulo = "BANCO" Then
         Return (id = CtaBancoAcredTarjeta Or id = CtaBancoDeposito Or id = CtaBancoExtraccion Or id = CtaBancoPago Or id = CtaBancoTransfBancaria)
      End If

      If Modulo = "CAJA" Then
         Return (id = CtaCajaCompras Or id = CtaCajaDeposito Or id = CtaCajaDepositoTarjetas Or id = CtaCajaExtraccion Or id = CtaCajaMovCheques Or id = CtaCajaPago Or id = CtaCajaRecibos Or id = CtaCajaTransfBancaria Or id = CtaCajaVentas)
      End If

   End Function

   'Public Shared Function GetNombreMes(mes As Integer) As String
   '   Dim nombre As String
   '   Select Case mes
   '      Case 1
   '         nombre = "Enero"
   '      Case 2
   '         nombre = "Febrero"
   '      Case 3
   '         nombre = "Marzo"
   '      Case 4
   '         nombre = "Abril"
   '      Case 5
   '         nombre = "Mayo"
   '      Case 6
   '         nombre = "Junio"
   '      Case 7
   '         nombre = "Julio"
   '      Case 8
   '         nombre = "Agosto"
   '      Case 9
   '         nombre = "Septiembre"
   '      Case 10
   '         nombre = "Octubre"
   '      Case 11
   '         nombre = "Noviembre"
   '      Case 12
   '         nombre = "Diciembre"
   '      Case Else
   '         nombre = String.Empty
   '   End Select
   '   Return nombre
   'End Function

   Public Shared Function EstaActivaLaURL(url As String) As Boolean
      Dim Url2 As System.Uri = New System.Uri(url)
      Dim peticion As System.Net.WebRequest
      Dim respuesta As System.Net.WebResponse

      Try
         'Creamos la peticion
         peticion = System.Net.WebRequest.Create(Url2)
         'POnemos un tiempo limite
         peticion.Timeout = 5000

         'ejecutamos la peticion
         respuesta = peticion.GetResponse()
         respuesta.Close()
         peticion = Nothing

         Return True

      Catch ex As Exception
         'si ocurre un error, devolvemos error
         peticion = Nothing
         Return False

      End Try

   End Function


   'PE-30973
   Public Shared ReadOnly Property DiasAlertaClientesCategoriaModificar As Integer
      Get
         Return Integer.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.DIASALERTACLIENTESCATEGORIAMODIFICAR, "5"))
      End Get
   End Property

   'PE-31388
   Public Shared ReadOnly Property ProductoPublicarWebCarrito() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.PRODUCTOPUBLICARWEBCARRITO.ToString(), "False"))
      End Get
   End Property
   Public Shared ReadOnly Property ProductoPublicarBalanza() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.PRODUCTOPUBLICARBALANZA.ToString(), "False"))
      End Get
   End Property
   Public Shared ReadOnly Property ProductoPublicarListaPrecioClientes() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.PRODUCTOPUBLICARLISTAPRECIOCLIENTES.ToString(), "False"))
      End Get
   End Property
   Public Shared ReadOnly Property ProductoPublicarAppGestion() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.PRODUCTOPUBLICARAPPGESTION.ToString(), "False"))
      End Get
   End Property
   Public Shared ReadOnly Property ProductoPublicarSincronizacionSucursal() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.PRODUCTOPUBLICARSINCRONIZACIONSUCURSAL.ToString(), "False"))
      End Get
   End Property
   Public Shared ReadOnly Property ProductoPublicarAppEmpresarial() As Boolean
      Get
         Return Boolean.Parse(New Reglas.Parametros().GetValorPD(Entidades.Parametro.Parametros.PRODUCTOPUBLICARAPPEMPRESARIAL.ToString(), "False"))
      End Get
   End Property
End Class